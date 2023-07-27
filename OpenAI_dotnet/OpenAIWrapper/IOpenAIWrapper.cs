using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public interface IOpenAIWrapper
{
    string GetVersion();
    Task<ModelListResponseModel> ListModels();
    Task<ModelResponseModel> RetrieveModel(string model);
    Task<ChatCompletionResponseModel> CreateChatCompletion(ChatCompletionRequestModel requestContent);
    Task<CompletionResponseModel> CreateCompletion(CompletionRequestModel requestContent);
    Task<EditResponseModel> CreateEdit(EditRequestModel requestContent);
    Task<EmbeddingResponseModel> CreateEmbedding(EmbeddingRequestModel requestContent);
    Task<AudioTranscriptionResponseModel> CreateAudioTranscription(AudioTranscriptionRequestModel requestContent);
    Task<AudioTranscriptionResponseModel> CreateAudioTranslation(AudioTranscriptionRequestModel requestContent);
    Task<FileListResponseModel> ListFiles();
    Task<FileUploadResponseModel> UploadFile(string purpose, byte[] file, string fileName);
    Task<DeleteResponseModel> DeleteFile(string fileId);
    Task<FileResponseModel> RetrieveFile(string fileId);
    Task<FileContentResponseModel<T?>> RetrieveFileContent<T>(string fileId);
    Task<FineTuneResponseModel> CreateFineTune(FineTuneRequestModel requestContent);
    Task<FineTuneListResponseModel> ListFineTunes();
    Task<FineTuneResponseModel> RetrieveFineTune(string fineTuneId);
    Task<FineTuneResponseModel> CancelFineTune(string fineTuneId);
    Task<EventListResponseModel> ListFineTuneEvents(string fineTuneId);
    Task<DeleteResponseModel> DeleteFineTune(string finetuneId);
    Task<ModerationResponseModel> CreateModeration(ModerationRequestModel requestContent);
    Task<ImageCreateResponseModel> CreateImage(ImageCreateRequestModel requestContent);
    Task<ImageCreateResponseModel> EditImage(ImageEditRequestModel requestContent);
    Task<ImageCreateResponseModel> CreateImageVariation(ImageVariationRequestModel requestModel);
}