using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    /// <summary>
    /// Returns a list of files that belong to the user's organization.
    /// https://platform.openai.com/docs/api-reference/files/list
    /// </summary>
    /// <returns></returns>
    public async Task<FileListResponseModel> ListFiles()
    {
        return (await _httpClient.GetFromJsonAsync<FileListResponseModel>("https://api.openai.com/v1/files"))!;
    }

    /// <summary>
    /// Upload a file that contains document(s) to be used across various endpoints/features. Currently, the size of all the files uploaded by one organization can be up to 1 GB. Please contact us if you need to increase the storage limit.
    /// https://platform.openai.com/docs/api-reference/files/upload
    /// </summary>
    /// <param name="purpose">The intended purpose of the uploaded documents. Use "fine-tune" for Fine-tuning. This allows us to validate the format of the uploaded file.</param>
    /// <param name="file">Name of the JSON Lines file to be uploaded. If the purpose is set to "fine-tune", each line is a JSON record with "prompt" and "completion" fields representing your training examples.</param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<FileUploadResponseModel> UploadFile(string purpose, byte[] file, string fileName)
    {
        var multipartContent = new MultipartFormDataContent
        {
            {new StringContent(purpose), "purpose"},
            {new ByteArrayContent(file), "file", fileName}
        };

        return await _httpClient.PostFileAndReadAsAsync<FileUploadResponseModel>("https://api.openai.com/v1/files", multipartContent);
    }

    /// <summary>
    /// Delete a file.
    /// https://platform.openai.com/docs/api-reference/files/delete
    /// </summary>
    /// <param name="fileId">The ID of the file to use for this request</param>
    /// <returns></returns>
    public async Task<DeleteResponseModel> DeleteFile(string fileId)
    {
        return await _httpClient.DeleteAndReadAsAsync<DeleteResponseModel>($"https://api.openai.com/v1/files/{fileId}");
    }

    /// <summary>
    /// Returns information about a specific file.
    /// https://platform.openai.com/docs/api-reference/files/retrieve
    /// </summary>
    /// <param name="fileId">The ID of the file to use for this request</param>
    /// <returns></returns>
    public async Task<FileResponseModel> RetrieveFile(string fileId)
    {
        return (await _httpClient.GetFromJsonAsync<FileResponseModel>($"https://api.openai.com/v1/files/{fileId}"))!;
    }

    /// <summary>
    /// Returns the contents of the specified file.
    /// https://platform.openai.com/docs/api-reference/files/retrieve-content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileId">The ID of the file to use for this request</param>
    /// <returns></returns>
    public async Task<FileContentResponseModel<T?>> RetrieveFileContent<T>(string fileId)
    {
        var response = await _httpClient.GetAsync($"https://api.openai.com/v1/files/{fileId}/content");

        if (!response.IsSuccessStatusCode)
        {
            return new FileContentResponseModel<T?>
            {
                Error = new Error
                {
                    Message = $"Api returned Status Code: {(int)response.StatusCode} {response.StatusCode}",
                    Code = ((int)response.StatusCode).ToString()
                }
            };
        }

        if (typeof(T) == typeof(string))
        {
            return new FileContentResponseModel<T?>
            {
                Content = (T)(object)await response.Content.ReadAsStringAsync()
            };
        }

        if (typeof(T) == typeof(byte[]))
        {
            return new FileContentResponseModel<T?>
            {
                Content = (T)(object)await response.Content.ReadAsByteArrayAsync()
            };
        }

        if (typeof(T) == typeof(Stream))
        {
            return new FileContentResponseModel<T?>
            {
                Content = (T)(object)await response.Content.ReadAsStreamAsync()
            };
        }

        return new FileContentResponseModel<T?>
        {
            Content = await response.Content.ReadFromJsonAsync<T>()
        };
    }
}
