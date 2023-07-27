using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenAI_dotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace OpenAI_dotnet.Tests
{
    [TestClass()]
    public class OpenAIWrapperTests
    {
        IConfiguration Configuration { get; set; }
        private OpenAIWrapper _openAIWrapper;

        public OpenAIWrapperTests()
        {
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<OpenAIWrapperTests>();

            Configuration = builder.Build();

            string apiKey = Configuration["OpenAI_API_Key"];
            string organizationID = Configuration["OpenAI_Organization_ID"];
            var httpClient = new HttpClient();
            _openAIWrapper = new OpenAIWrapper(httpClient, apiKey, organizationID);
        }


        [TestMethod()]
        public async Task CreateChatCompletion_ReturnsChatCompletionResponseModel_WithValidRequest()
        {
            // Arrange
            var requestContent = new ChatCompletionRequestModel
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = new List<ChatMessageModel>
                {
                    new ChatMessageModel() { Role = "system", Content = "You are a helpful assistant." },
                    new ChatMessageModel() { Role = "user", Content = "Who was the Prime Minister of New Zealand in 2000?" }
                },
                Temperature = 0.5
            };

            // Act
            var result = await _openAIWrapper.CreateChatCompletion(requestContent);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ChatCompletionResponseModel));
            Assert.AreEqual("chat.completion", result.ObjectTypeName);
            Assert.IsNotNull(result.Created);
            Assert.AreEqual("gpt-3.5-turbo-0301", result.Model);
            Assert.IsNotNull(result.Usage);
            Assert.IsNotNull(result.Choices);
            Assert.IsTrue(result.Choices.Count > 0);
        }

        [TestMethod]
        public async Task CreateChatCompletion_ReturnsErrorMessage_WithNullRequest()
        {
            // Arrange
            ChatCompletionRequestModel requestContent = null;

            // Assert
            var result = await _openAIWrapper.CreateChatCompletion(requestContent);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(ChatCompletionResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateChatCompletion_ReturnsErrorMessage_WithInvalidRequest()
        {
            // Arrange
            var requestContent = new ChatCompletionRequestModel
            {
                Model = "foo",
                Messages = new List<ChatMessageModel>
                {
                    new ChatMessageModel() { Role = "foo", Content = "foo" },
                },
                Temperature = 10
            };

            // Act and Assert
            var result = await _openAIWrapper.CreateChatCompletion(requestContent);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(ChatCompletionResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateChatCompletion_ReturnsErrorMessage_WithInvalidAPIKey()
        {
            // Arrange
            var requestContent = new ChatCompletionRequestModel
            {
                Model = "text-davinci-003",
                Messages = new List<ChatMessageModel>
                {
                    new ChatMessageModel { Role = "user", Content = "Hello!" },
                },
                Temperature = 0.5
            };
            var invalidApiKey = "1234567890";
            var invalidOrinizationId = "1234567890";
            var openAIWrapper = new OpenAIWrapper(new HttpClient(), invalidApiKey, invalidOrinizationId);

            // Act and Assert
            var result = await openAIWrapper.CreateChatCompletion(requestContent);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(ChatCompletionResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateCompletion_ReturnsCompletionResponseModel_WithValidRequest()
        {
            // Arrange
            var request = new CompletionRequestModel
            {
                Model = "text-davinci-003",
                Prompt = "Hello, are you ChatGPT?",
                MaxTokens = 50
            };

            // Act
            var response = await _openAIWrapper.CreateCompletion(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("text_completion", response.ObjectTypeName);
            Assert.IsNotNull(response.Id);
            Assert.IsNotNull(response.Choices);
            Assert.IsTrue(response.Choices.Count > 0);
            Assert.IsNotNull(response.Usage);
        }

        [TestMethod]
        public async Task CreateCompletion_ReturnsErrorMessage_WithNullRequest()
        {
            // Arrange
            CompletionRequestModel request = null;

            // Assert
            var result = await _openAIWrapper.CreateCompletion(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(CompletionResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateCompletion_ReturnsErrorMessage_WithInvalidAPIKey()
        {
            // Arrange
            var requestContent = new CompletionRequestModel
            {
                Model = "davinci",
                Prompt = "Hello, my name is ChatGPT.",
                MaxTokens = 50
            };
            var invalidApiKey = "1234567890";
            var invalidOrinizationId = "1234567890";
            var openAIWrapper = new OpenAIWrapper(new HttpClient(), invalidApiKey, invalidOrinizationId);

            // Act and Assert
            var result = await openAIWrapper.CreateCompletion(requestContent);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(CompletionResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateCompletion_ThrowsValidationException_WithInvalidPromptAndPromptAsList()
        {
            // Arrange
            var request = new CompletionRequestModel
            {
                Model = "davinci",
                Prompt = "Hello, my name is ChatGPT.",
                PromptAsList = new List<string> { "Nice to meet you." },
                MaxTokens = 50
            };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => _openAIWrapper.CreateCompletion(request));
        }

        [TestMethod]
        public async Task CreateCompletion_ThrowsValidationException_WithInvalidStopAndStopAsList()
        {
            // Arrange
            var request = new CompletionRequestModel
            {
                Model = "davinci",
                Prompt = "Hello, my name is ChatGPT.",
                Stop = "Goodbye!",
                StopAsList = new List<string> { "See you later." },
                MaxTokens = 50
            };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => _openAIWrapper.CreateCompletion(request));
        }

        [TestMethod]
        public async Task CreateEdit_ReturnsEditResponseModel_WithValidRequest()
        {
            // Arrange
            var request = new EditRequestModel
            {
                Input = "TToday is Wesday.",
                Instruction = "Correct the grammar",
                Model = "text-davinci-edit-001"
            };

            // Act
            var response = await _openAIWrapper.CreateEdit(request);

            Console.WriteLine(response.ToString());

            // Assert
            Assert.AreEqual("edit", response.ObjectTypeName);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Choices);
            Assert.IsNotNull(response.CreatedAt);
        }

        [TestMethod]
        public async Task CreateEdit_ReturnsErrorMessage_WithInvalidRequest()
        {
            // Arrange
            EditRequestModel request = null;

            // Assert
            var result = await _openAIWrapper.CreateEdit(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(EditResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateEdit_ReturnsErrorMessage_WithInvalidModel()
        {
            // Arrange
            var request = new EditRequestModel
            {
                Input = "Hello, this is a test document.",
                Model = "invalid-model"
            };

            // Assert
            var result = await _openAIWrapper.CreateEdit(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(EditResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateEdit_ReturnsErrorMessage_WithInvalidApiKey()
        {
            // Arrange
            var invalidApiKey = "invalid-api-key";
            var httpClient = new HttpClient();
            var openAIWrapper = new OpenAIWrapper(httpClient, invalidApiKey, "test-org-id");

            var request = new EditRequestModel
            {
                Input = "Hello, this is a test document.",
                Instruction = "Make it sound like ChatGPT",
                Model = "text-davinci-edit-001"
            };

            // Assert
            var result = await openAIWrapper.CreateEdit(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(EditResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateEmbedding_ReturnsEmbeddingResponseModel_WithValidRequest()
        {
            // Arrange
            var request = new EmbeddingRequestModel
            {
                Input = "Hello, world!",
                Model = "text-embedding-ada-002"
            };

            // Act
            var response = await _openAIWrapper.CreateEmbedding(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(response.ObjectTypeName, "list");
        }

        [TestMethod]
        public async Task CreateEmbedding_ReturnsErrorMessage_WithNullRequest()
        {
            // Arrange
            EmbeddingRequestModel request = null;

            // Assert
            var result = await _openAIWrapper.CreateEmbedding(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(EmbeddingResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateEmbedding_ReturnsErrorMessage_WithNoInput()
        {
            // Arrange
            var request = new EmbeddingRequestModel
            {
                Model = "text-embedding-ada-002"
            };

            // Assert
            var result = await _openAIWrapper.CreateEmbedding(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(EmbeddingResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateEmbedding_ReturnsErrorMessage_WithNoModel()
        {
            // Arrange
            var request = new EmbeddingRequestModel
            {
                Input = "Hello, world!"
            };

            // Assert
            var result = await _openAIWrapper.CreateEmbedding(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(EmbeddingResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task CreateEmbedding_ThrowsValidationException_WithBothInputAndInputAsList()
        {
            // Arrange
            var request = new EmbeddingRequestModel
            {
                Input = "Hello, world!",
                InputAsList = new List<string> { "Goodbye, world!" },
                Model = "text-embedding-ada-002"
            };

            // Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => _openAIWrapper.CreateEmbedding(request));
        }

        [TestMethod]
        public async Task CreateEmbedding_ReturnsErrorMessage_WithInvalidApiKey()
        {
            // Arrange
            var invalidApiKey = "invalid-api-key";
            var request = new EmbeddingRequestModel
            {
                Model = "text-davinci-002",
                InputAsList = new List<string> { "Hello", "World" }
            };
            var httpClient = new HttpClient();
            var openAIWrapper = new OpenAIWrapper(httpClient, invalidApiKey, "");

            // Act & Assert
            var result = await _openAIWrapper.CreateEmbedding(request);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(EmbeddingResponseModel));
            Assert.IsNotNull(result.Error);
        }

        // string audioFilePath = "C:\\Users\\nlxli\\Downloads\\aimer-romeo-et-juliette.mp3";
        string audioFilePath = "C:\\Users\\XiaohanLiu\\Downloads\\daoxiang.mp3";
        [TestMethod]
        public async Task CreateAudioTranscription_ReturnsAudioTranscriptionResponseModel_WithValidRequest()
        {
            // Arrange
            var request = new AudioTranscriptionRequestModel
            {
                File = File.ReadAllBytes(audioFilePath),
                FileName = audioFilePath,
                Model = "whisper-1"
            };

            // Act
            var response = await _openAIWrapper.CreateAudioTranscription(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Text);
        }

        [TestMethod]
        public async Task CreateAudioTranscription_ThrowsException_WithNullRequest()
        {
            // Arrange
            AudioTranscriptionRequestModel request = null;

            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _openAIWrapper.CreateAudioTranscription(request));
        }

        [TestMethod]
        public async Task CreateAudioTranscription_ThrowsException_WithNoFile()
        {
            // Arrange
            var request = new AudioTranscriptionRequestModel
            {
                Model = "whisper-1"
            };

            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _openAIWrapper.CreateAudioTranscription(request));
        }

        [TestMethod]
        public async Task CreateAudioTranscription_ErrorMessage_WithUnknownModel()
        {
            // Arrange
            var request = new AudioTranscriptionRequestModel
            {
                File = File.ReadAllBytes(audioFilePath),
                FileName = audioFilePath,
                Model = "unknown-model"
            };

            // Assert
            AudioTranscriptionResponseModel resp = await _openAIWrapper.CreateAudioTranscription(request);
            Console.WriteLine(resp.ToString());
            Assert.IsNotNull(resp.Text);
        }

        [TestMethod]
        public async Task CreateAudioTranscription_ErrorMessage_WithInvalidResponseFormat()
        {
            // Arrange
            var request = new AudioTranscriptionRequestModel
            {
                File = File.ReadAllBytes(audioFilePath),
                FileName = audioFilePath,
                Model = "whisper-1",
                ResponseFormat = "invalid-response-format"
            };

            // Assert
            AudioTranscriptionResponseModel resp = await _openAIWrapper.CreateAudioTranscription(request);
            Console.WriteLine(resp.ToString());
            Assert.IsNotNull(resp.Text);
        }

        [TestMethod]
        public async Task ListFiles_ReturnsFileListResponseModel_WithValidRequest()
        {
            // Act
            var response = await _openAIWrapper.ListFiles();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(response.ObjectTypeName, "list");
        }

        [TestMethod]
        public async Task ListFiles_ThrowsHttpRequestException_WithInvalidAuthorization()
        {
            // Arrange
            var openAIWrapper = new OpenAIWrapper(new HttpClient(), "INVALID_API_KEY", "INVALID_ORGANIZATION_ID");

            // Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => openAIWrapper.ListFiles());
        }

        [TestMethod]
        public async Task UploadFile_ReturnsFileUploadResponseModel_WithValidRequest()
        {
            // Arrange
            var purpose = "fine-tune";
            var fileBytes = Encoding.UTF8.GetBytes("{\"prompt\": \"Test prompt 1\", \"completion\": \"Test completion 1\"}\n" +
                                                   "{\"prompt\": \"Test prompt 2\", \"completion\": \"Test completion 2\"}\n");
            var fileName = "test-file.jsonl";

            // Act
            var response = await _openAIWrapper.UploadFile(purpose, fileBytes, fileName);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Purpose, purpose);
            Assert.AreEqual(response.FileName, fileName);
        }

        [TestMethod]
        public async Task UploadFile_ReturnsErrorMessage_WithInvalidPurpose()
        {
            // Arrange
            var purpose = "test-purpose";
            var fileBytes = Encoding.UTF8.GetBytes("{\"prompt\": \"Test prompt 1\", \"completion\": \"Test completion 1\"}\n");
            var fileName = "test-file.jsonl";

            // Assert
            var result = await _openAIWrapper.UploadFile(purpose, fileBytes, fileName);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOfType(result, typeof(FileUploadResponseModel));
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task UploadAndDeleteFile_ReturnsFileDeleteResponseModel_WithValidRequest()
        {
            // Arrange
            var fileContent = Encoding.UTF8.GetBytes("{\"prompt\": \"<prompt text>\", \"completion\": \"<ideal generated text>\"}\n");
            var fileName = "test.jsonl";
            var purpose = "fine-tune";

            // Act - Upload file
            var uploadResponse = await _openAIWrapper.UploadFile(purpose, fileContent, fileName);

            // Wait for 10 seconds
            await Task.Delay(10000);

            // Act - Delete file
            var deleteResponse = await _openAIWrapper.DeleteFile(uploadResponse.Id);

            // Assert
            Assert.IsNotNull(deleteResponse);
            Assert.IsTrue(deleteResponse.Deleted);
            Assert.AreEqual(deleteResponse.Id, uploadResponse.Id);
        }

        [TestMethod]
        public async Task UploadAndDeleteFile_ReturnsErrorMessage_WithInvalidPurpose()
        {
            // Arrange
            var fileContent = Encoding.UTF8.GetBytes("{\"prompt\": \"<prompt text>\", \"completion\": \"<ideal generated text>\"}\n");
            var fileName = "test.jsonl";
            var purpose = "invalid-purpose";

            // Act
            var uploadResponse = await _openAIWrapper.UploadFile(purpose, fileContent, fileName);

            // Wait for 10 seconds
            await Task.Delay(10000);

            // Act - Delete file
            var deleteResponse = await _openAIWrapper.DeleteFile(uploadResponse.Id);
            Console.WriteLine(deleteResponse.ToString());

            // Assert
            Assert.IsNotNull(uploadResponse.Error);
            Assert.AreNotEqual(deleteResponse.Error.Code, "200");
            Assert.IsNotNull(deleteResponse.Error);
        }

        [TestMethod]
        public async Task UploadFileAndRetrieveFile_ReturnsFileContentResponseModel_Success()
        {
            // Arrange
            var fileContent = "{\"prompt\": \"Hello\", \"completion\": \"Hi!\"}\n{\"prompt\": \"Bye\", \"completion\": \"See you later!\"}\n";
            var uploadResponse = await _openAIWrapper.UploadFile("fine-tune", Encoding.UTF8.GetBytes(fileContent), "test_file.jsonl");
            await Task.Delay(TimeSpan.FromSeconds(10));

            // Act
            var retrieveResponse = await _openAIWrapper.RetrieveFile(uploadResponse.Id);

            // Assert
            Assert.AreEqual(uploadResponse.Id, retrieveResponse.Id);
            Assert.AreEqual(uploadResponse.Bytes, retrieveResponse.Bytes);
            Assert.AreEqual(uploadResponse.FileName, retrieveResponse.FileName);
            Assert.AreEqual(uploadResponse.Purpose, retrieveResponse.Purpose);
            Assert.AreEqual(uploadResponse.CreatedAt, retrieveResponse.CreatedAt);
        }

        [TestMethod]
        public async Task RetrieveFile_ReturnsErrorResponse_InvalidFileId()
        {
            // Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => _openAIWrapper.RetrieveFile("invalid_file_id"));
        }

        [TestMethod]
        public async Task RetrieveFileContent_Successful()
        {
            // Arrange
            var fileContent = Encoding.UTF8.GetBytes("{\"prompt\": \"<prompt text>\", \"completion\": \"<ideal generated text>\"}\n");

            // Upload file
            var uploadResponse = await _openAIWrapper.UploadFile("fine-tune", fileContent, "test_file.jsonl");

            // Wait 10 seconds
            await Task.Delay(TimeSpan.FromSeconds(10));

            // Act
            var retrieveResponse = await _openAIWrapper.RetrieveFileContent<string>(uploadResponse.Id);
            Console.WriteLine(retrieveResponse);

            // Assert
            Assert.IsTrue(retrieveResponse.Successful);
            Assert.IsNull(retrieveResponse.Error);
            Assert.IsNotNull(retrieveResponse.Content);
        }

        [TestMethod]
        public async Task RetrieveFileContent_Failure()
        {
            // Arrange
            var fileId = "invalid_file_id";

            // Act
            var getContentResponse = await _openAIWrapper.RetrieveFileContent<string>(fileId);

            // Assert
            Assert.IsFalse(getContentResponse.Successful);
            Assert.IsNotNull(getContentResponse.Error);
            Assert.AreNotEqual("200", getContentResponse.Error.Code);
        }

        [TestMethod]
        public async Task CreateFineTune_ReturnsFineTuneResponse_ValidRequest()
        {
            // Arrange
            var requestContent = new FineTuneRequestModel
            {
                Model = "curie",
                TrainingFile = "file-Z0qMD8ySKxnXyioTmx9GvPqv",
                NEpochs = 10,
                BatchSize = 4,
                PromptLossWeight = 1,
                Suffix = "test-model"
            };

            // Act
            var result = await _openAIWrapper.CreateFineTune(requestContent);
            Console.WriteLine(result.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Successful);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task CreateFineTune_ReturnsErrorMessage_InvalidRequest()
        {
            // Arrange
            var requestContent = new FineTuneRequestModel
            {
                TrainingFile = "invalid.jsonl",
            };

            // Act
            var result = await _openAIWrapper.CreateFineTune(requestContent);
            Console.WriteLine(result.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public async Task ListFineTunes_Success()
        {
            // Act
            var response = await _openAIWrapper.ListFineTunes();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
            Assert.IsNull(response.Error);
        }

        [TestMethod]
        public async Task ListFineTunes_ThrowsHttpRequestException_WithInvalidAPIKey()
        {
            // Arrange
            var openai = new OpenAIWrapper(new HttpClient(), "api", "org");

            // Act
            var ex = await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await openai.ListFineTunes());

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
        }

        [TestMethod]
        public async Task RetrieveFineTune_Success()
        {
            // Arrange
            string fineTuneId = "ft-nezTD7eVRSUQGjJudpuWDF80";

            // Act
            var result = await _openAIWrapper.RetrieveFineTune(fineTuneId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fineTuneId, result.Id);
            Assert.IsNotNull(result.CreatedAt);
            Assert.IsNotNull(result.HyperParams);
            Assert.IsNotNull(result.TrainingFiles);
        }

        [TestMethod]
        public async Task RetrieveFineTune_ThrowsHttpRequestException_InvalidFineTuneId()
        {
            // Arrange
            var invalidFineTuneId = "invalid_id";

            // Act
            var ex = await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await _openAIWrapper.RetrieveFineTune(invalidFineTuneId));
        }

        [TestMethod]
        public async Task CancelFineTune_Success()
        {
            // Create fine-tune
            var createResponse = await _openAIWrapper.CreateFineTune(
                new FineTuneRequestModel
                {
                    Model = "curie",
                    TrainingFile = "file-Z0qMD8ySKxnXyioTmx9GvPqv",
                    NEpochs = 10,
                    BatchSize = 4,
                    PromptLossWeight = 1,
                    Suffix = "test-model"
                });
            var fineTuneId = createResponse.Id;

            await Task.Delay(1000);

            // Cancel fine-tune
            var cancelResponse = await _openAIWrapper.CancelFineTune(fineTuneId);
            Console.WriteLine(cancelResponse.ToString());

            // Assert
            Assert.IsNotNull(cancelResponse.Events);
        }

        [TestMethod]
        public async Task CancelFineTune_Failure()
        {
            var cancelResponse = await _openAIWrapper.CancelFineTune("ft-thisIsAnInvalidFineTuneId");
            Console.WriteLine(cancelResponse.ToString());

            Assert.IsNotNull(cancelResponse.Error);
        }


    }
}