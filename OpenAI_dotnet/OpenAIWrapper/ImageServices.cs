using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    /// <summary>
    /// Creates an image given a prompt.
    /// https://platform.openai.com/docs/api-reference/images/create
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    public async Task<ImageCreateResponseModel> CreateImage(ImageCreateRequestModel requestContent)
    {
        return await _httpClient.PostAndReadAsAsync<ImageCreateResponseModel>("https://api.openai.com/v1/images/generations", requestContent);
    }

    /// <summary>
    /// Creates an edited or extended image given an original image and a prompt.
    /// https://platform.openai.com/docs/api-reference/images/create-edit
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    public async Task<ImageCreateResponseModel> EditImage(ImageEditRequestModel requestContent)
    {
        var multipartContent = new MultipartFormDataContent();
        if (requestContent.User != null)
        {
            multipartContent.Add(new StringContent(requestContent.User), "user");
        }

        if (requestContent.ResponseFormat != null)
        {
            multipartContent.Add(new StringContent(requestContent.ResponseFormat), "response_format");
        }

        if (requestContent.Size != null)
        {
            multipartContent.Add(new StringContent(requestContent.Size), "size");
        }

        if (requestContent.N != null)
        {
            multipartContent.Add(new StringContent(requestContent.N.ToString()!), "n");
        }

        if (requestContent.Mask != null)
        {
            multipartContent.Add(new ByteArrayContent(requestContent.Mask), "mask", requestContent.MaskName);
        }

        multipartContent.Add(new StringContent(requestContent.Prompt), "prompt");
        multipartContent.Add(new ByteArrayContent(requestContent.Image), "image", requestContent.ImageName);

        return await _httpClient.PostFileAndReadAsAsync<ImageCreateResponseModel>("https://api.openai.com/v1/images/edits", multipartContent);
    }

    /// <summary>
    /// Creates a variation of a given image.
    /// https://platform.openai.com/docs/api-reference/images/create-variation
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<ImageCreateResponseModel> CreateImageVariation(ImageVariationRequestModel requestModel)
    {
        var multipartContent = new MultipartFormDataContent();
        if (requestModel.User != null)
        {
            multipartContent.Add(new StringContent(requestModel.User), "user");
        }

        if (requestModel.ResponseFormat != null)
        {
            multipartContent.Add(new StringContent(requestModel.ResponseFormat), "response_format");
        }

        if (requestModel.Size != null)
        {
            multipartContent.Add(new StringContent(requestModel.Size), "size");
        }

        if (requestModel.N != null)
        {
            multipartContent.Add(new StringContent(requestModel.N.ToString()!), "n");
        }

        multipartContent.Add(new ByteArrayContent(requestModel.Image), "image", requestModel.ImageName);

        return await _httpClient.PostFileAndReadAsAsync<ImageCreateResponseModel>("https://api.openai.com/v1/images/variations", multipartContent);
    }
}
