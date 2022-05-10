using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.APIRequestModel.Lessons;
using DohrniiFoundation.Models.APIRequestModel.User;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.APIResponseModels.Lessons;
using DohrniiFoundation.Models.APIResponseModels.Strategies;
using DohrniiFoundation.Models.APIResponseModels.User;
using DohrniiFoundation.Models.InvestorsProfileRequestModels;
using DohrniiFoundation.Models.InvestorsProfileResponseModels;
using DohrniiFoundation.Models.Lessons;
using DohrniiFoundation.Resources;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DohrniiFoundation.Services
{
    /// <summary>
    /// This class use for implement the interfaces
    /// </summary>
    public class APIServices : IAPIService
    {
        #region Private Members
        private static ServiceHelpers serviceHelpers;
        #endregion

        #region Constructor
        public APIServices()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        #endregion

        #region Methods 
        /// <summary>
        /// API implementation to login in app with request
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns></returns>
        public async Task<LoginResponseModel> LoginService(LoginRequestModel loginRequestModel)
        {
            LoginResponseModel loginResponse = new LoginResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(loginRequestModel);
                ResponseModel response = await ServiceHelpers.Instance.PostRequest(serializedRequest, StringConstant.LoginAPIEndPoint, true, null);
                loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return loginResponse;
        }

        /// <summary>
        /// API implementation to sign up in app with request
        /// </summary>
        /// <param name="signUpRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> SignUpService(SignUpRequestModel signUpRequestModel)
        {
            ResponseModel signUpResponse = new ResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(signUpRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.SignUpAPIEndPoint, true, string.Empty);
                if (response != null)
                {
                    signUpResponse = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return signUpResponse;
        }

        /// <summary>
        ///  API implementation to forgot password with request
        /// </summary>
        /// <param name="forgotPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> ForgotPasswordService(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            ResponseModel forgotPasswordResponse = new ResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(forgotPasswordRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.ForgotPasswordAPIEndPoint, true, string.Empty);
                if (response != null)
                {
                    forgotPasswordResponse = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return forgotPasswordResponse;
        }

        /// <summary>
        /// API implementation to reset the password in app with request
        /// </summary>
        /// <param name="resetPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> ResetPasswordService(ResetPasswordRequestModel resetPasswordRequestModel)
        {
            ResponseModel resetPasswordResponse = new ResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(resetPasswordRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.ResetPasswordAPIEndPoint, true, string.Empty);
                if (response != null)
                {
                    resetPasswordResponse = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return resetPasswordResponse;
        }
        /// <summary>
        /// API integration to change the password using old and new password
        /// </summary>
        /// <param name="changePasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> ChangePasswordService(ChangePasswordRequestModel changePasswordRequestModel)
        {
            ResponseModel changePasswordResponse = new ResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(changePasswordRequestModel);
                ResponseModel response = await ServiceHelpers.Instance.PostRequest(serializedRequest, StringConstant.ChangePasswordAPIEndPoint, true, Utilities.BearerToken);
                changePasswordResponse = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return changePasswordResponse;
        }
        /// <summary>
        /// API integration to logout from the app with header
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel> LogoutService()
        {
            ResponseModel refreshTokenResponse = new ResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.PostRequest(string.Empty, StringConstant.LogoutAPIEndPoint, true, Utilities.BearerToken);
                if (response != null)
                {
                    refreshTokenResponse = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return refreshTokenResponse;
        }

        /// <summary>
        /// API integration to get the questions list of the investors profile
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionnaireResponseModel> GetQuestionService()
        {
            QuestionnaireResponseModel questionnaireResponse = new QuestionnaireResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetQuestionAPIEndPoint, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    questionnaireResponse = JsonConvert.DeserializeObject<QuestionnaireResponseModel>(response.Data);
                }
                else
                {
                    questionnaireResponse = JsonConvert.DeserializeObject<QuestionnaireResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, questionnaireResponse.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return questionnaireResponse;
        }

        /// <summary>
        /// API integration to get the answer of the question id which is passed in API
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<AnswersResponseModel> GetAnswersService(int questionId)
        {
            AnswersResponseModel answerResponse = new AnswersResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetAnswerAPIEndPoint + questionId, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    answerResponse = JsonConvert.DeserializeObject<AnswersResponseModel>(response.Data);
                }
                else
                {
                    answerResponse = JsonConvert.DeserializeObject<AnswersResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, answerResponse.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return answerResponse;
        }

        /// <summary>
        /// API integration to get all the answer of the question of the questionnaire
        /// </summary>
        /// <returns></returns>
        public async Task<AnswersResponseModel> GetAllAnswersService()
        {
            AnswersResponseModel answerResponse = new AnswersResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetAllAnswerAPIEndPoint, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    answerResponse = JsonConvert.DeserializeObject<AnswersResponseModel>(response.Data);
                }
                else
                {
                    answerResponse = JsonConvert.DeserializeObject<AnswersResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, answerResponse.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return answerResponse;
        }

        /// <summary>
        /// API integration to submit the selected answers of all the questions of the questionnaire when all answers are passed
        /// </summary>
        /// <param name="submitQuestionnaireRequestModel"></param>
        /// <returns></returns>
        public async Task<SubmitQuestionnaireResponseModel> SubmitSelectedAnswersService(SubmitQuestionnaireRequestModel submitQuestionnaireRequestModel)
        {
            SubmitQuestionnaireResponseModel submittedAnswerResponse = new SubmitQuestionnaireResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(submitQuestionnaireRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.SubmitAllAnswerAPIEndPoint, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    submittedAnswerResponse = JsonConvert.DeserializeObject<SubmitQuestionnaireResponseModel>(response.Data);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, response.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return submittedAnswerResponse;
        }

        /// <summary>
        /// API integration to get the user details when the user gets login in app
        /// </summary>
        /// <returns></returns>
        public async Task<UserProfileResponseModel> GetUserProfileService()
        {
            UserProfileResponseModel userProfileResponseModel = new UserProfileResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetUserProfileAPIEndPoint, true, Utilities.BearerToken);
                userProfileResponseModel = JsonConvert.DeserializeObject<UserProfileResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return userProfileResponseModel;
        }
        /// <summary>
        /// API integration to update the user Profile
        /// </summary>
        /// <param name="updateProfileRequestModel"></param>
        /// <returns></returns>
        public async Task<UpdateProfileResponseModel> UpdateProfileService(UpdateProfileRequestModel updateProfileRequestModel, int userId)
        {
            UpdateProfileResponseModel updateProfileResponseModel = new UpdateProfileResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(updateProfileRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.UpdateProfileApiEndPoint + userId, true, Utilities.BearerToken);
                updateProfileResponseModel = JsonConvert.DeserializeObject<UpdateProfileResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return updateProfileResponseModel;
        }
        /// <summary>
        /// API integration to get the user questions with answers if questionnaire given by user
        /// </summary>
        /// <returns></returns>
        public async Task<SubmitQuestionnaireResponseModel> GetUserQuestionnaireService()
        {
            SubmitQuestionnaireResponseModel userQuestionnaireResponseModel = new SubmitQuestionnaireResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetUserQuestionnaireAPIEndPoint, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    userQuestionnaireResponseModel = JsonConvert.DeserializeObject<SubmitQuestionnaireResponseModel>(response.Data);
                }
                else
                {
                    userQuestionnaireResponseModel = JsonConvert.DeserializeObject<SubmitQuestionnaireResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, userQuestionnaireResponseModel.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return userQuestionnaireResponseModel;
        }

        /// <summary>
        /// API integration to get all the strategies of the user
        /// </summary>
        /// <returns></returns>
        public async Task<StrategiesResponseModel> GetAllStrategiesService()
        {
            StrategiesResponseModel strategiesResponseModel = new StrategiesResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetAllStrategiesAPIEndPoint, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    strategiesResponseModel = JsonConvert.DeserializeObject<StrategiesResponseModel>(response.Data);
                }
                else
                {
                    strategiesResponseModel = JsonConvert.DeserializeObject<StrategiesResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, strategiesResponseModel.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return strategiesResponseModel;
        }

        /// <summary>
        /// API integration to get the concepts by passing the strategy id
        /// </summary>
        /// <param name="strategyId"></param>
        /// <returns></returns>
        public async Task<StrategyConceptsResponseModel> GetStrategyConceptsService(int strategyId)
        {
            StrategyConceptsResponseModel strategyConceptsResponse = new StrategyConceptsResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetStrategyConceptsEndPoint + strategyId, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    strategyConceptsResponse = JsonConvert.DeserializeObject<StrategyConceptsResponseModel>(response.Data);
                }
                else
                {
                    strategyConceptsResponse = JsonConvert.DeserializeObject<StrategyConceptsResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, strategyConceptsResponse.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return strategyConceptsResponse;
        }

        /// <summary>
        /// API integration to get the all long term concepts of the strategy
        /// </summary>
        /// <returns></returns>
        public async Task<LongTermConceptsResponseModel> GetAllLongTermConceptsService()
        {
            LongTermConceptsResponseModel longTermConceptsResponseModel = new LongTermConceptsResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetAllLongTermConceptsEndPoint, true, Utilities.BearerToken);
                if (response.IsSuccess)
                {
                    longTermConceptsResponseModel = JsonConvert.DeserializeObject<LongTermConceptsResponseModel>(response.Data);
                }
                else
                {
                    longTermConceptsResponseModel = JsonConvert.DeserializeObject<LongTermConceptsResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, longTermConceptsResponseModel.Message, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return longTermConceptsResponseModel;
        }

        /// <summary>
        /// API integration to get the strategy assigned to the user bases on the token
        /// </summary>
        /// <returns></returns>
        public async Task<StrategyStatusResponseModel> GetUserStatusService()
        {
            StrategyStatusResponseModel strategyStatusResponseModel = new StrategyStatusResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetUserStatusApi, true, Utilities.BearerToken);
                strategyStatusResponseModel = JsonConvert.DeserializeObject<StrategyStatusResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return strategyStatusResponseModel;
        }
        /// <summary>
        ///  API integration to get the all chapters and lessons to the user bases on the token 
        /// </summary>
        /// <param name="chaptersCategoryRequestModel"></param>
        /// <returns></returns>
        public async Task<LessonsResponseModel> GetChaptersLessonsCategoryService(ChaptersCategoryRequestModel chaptersCategoryRequestModel)
        {
            LessonsResponseModel lessonResponseModel = new LessonsResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetChaptersLessonsCategoryApiEndPoint + chaptersCategoryRequestModel.SelectedCategory, true, Utilities.BearerToken);
                lessonResponseModel = JsonConvert.DeserializeObject<LessonsResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return lessonResponseModel;
        }
        /// <summary>
        /// API integration to get the chapters category based on the user token
        /// </summary>
        /// <returns></returns>
        public async Task<ChaptersCategoryResponseModel> GetAllChaptersCategoryService()
        {
            ChaptersCategoryResponseModel chaptersCategoryResponseModel = new ChaptersCategoryResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetAllChaptersCategoryApiEndPoint, true, Utilities.BearerToken);
                chaptersCategoryResponseModel = JsonConvert.DeserializeObject<ChaptersCategoryResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return chaptersCategoryResponseModel;
        }
        /// <summary>
        /// API integration to get the classes when lessons id parameter is passed
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public async Task<ClassesResponseModel> GetClassesLessonIdService(SelectedLessonRequestModel selectedLessonRequestModel)
        {
            ClassesResponseModel chaptersCategoryResponseModel = new ClassesResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetClassesLessonsIdApiEndPoint + selectedLessonRequestModel.SelectedLessonId + StringConstant.GetClassesChapterIdApiEndPoint + selectedLessonRequestModel.SelectedChapterId, true, Utilities.BearerToken);
                chaptersCategoryResponseModel = JsonConvert.DeserializeObject<ClassesResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return chaptersCategoryResponseModel;
        }
        /// <summary>
        /// API integration to get the classes questions with answers of the selected class
        /// </summary>
        /// <param name="selectedClassRequestModel"></param>
        /// <returns></returns>
        public async Task<ClassesQuestionsResponseModel> GetQuestionsClassIdService(SelectedClassRequestModel selectedClassRequestModel)
        {
            ClassesQuestionsResponseModel classesQuestionsResponseModel = new ClassesQuestionsResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetQuestionsClassIdApiEndPoint + selectedClassRequestModel.ClassId, true, Utilities.BearerToken);
                classesQuestionsResponseModel = JsonConvert.DeserializeObject<ClassesQuestionsResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return classesQuestionsResponseModel;
        }
        /// <summary>
        /// API integration to submit the class questions with answers of the selected class
        /// </summary>
        /// <param name="submitClassQuestionsRequestModel"></param>
        /// <returns></returns>
        public async Task<SubmitClassQuestionsResponseModel> SubmitSelectedClassAnswersService(SubmitClassQuestionsRequestModel submitClassQuestionsRequestModel)
        {
            SubmitClassQuestionsResponseModel submitClassQuestionsResponseModel = new SubmitClassQuestionsResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(submitClassQuestionsRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.SubmitQuestionsAnswersClassApiEndPoint, true, Utilities.BearerToken);
                submitClassQuestionsResponseModel = JsonConvert.DeserializeObject<SubmitClassQuestionsResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return submitClassQuestionsResponseModel;
        }
        /// <summary>
        /// API integration to get all chapters quiz of the completed chapter
        /// </summary>
        /// <param name="chapterQuizRequestModel"></param>
        /// <returns></returns>
        public async Task<ChapterQuizResponseModel> GetChapterQuizService(UnlockChapterQuizRequestModel chapterQuizRequestModel)
        {
            ChapterQuizResponseModel chapterQuizResponseModel = new ChapterQuizResponseModel();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetChapterQuizApiEndPoint + chapterQuizRequestModel.ChapterId, true, Utilities.BearerToken);
                chapterQuizResponseModel = JsonConvert.DeserializeObject<ChapterQuizResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return chapterQuizResponseModel;
        }
        /// <summary>
        /// API integration to unlock chapters quiz of the completed chapter
        /// </summary>
        /// <param name="unlockChapterQuizRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> UnlockChapterQuizService(UnlockQuizRequestModel unlockChapterQuizRequestModel)
        {
            ResponseModel unlockChapterQuizResponseModel = new ResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(unlockChapterQuizRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.UnlockChapterQuizApiEndPoint, true, Utilities.BearerToken);
                unlockChapterQuizResponseModel = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return unlockChapterQuizResponseModel;
        }
        /// <summary>
        /// API integration to submit the chapters quiz questions with answers of the selected completed chapter
        /// </summary>
        /// <param name="submitChapterQuizRequestModel"></param>
        /// <returns></returns>
        public async Task<SubmitChapterQuizResponseModel> SubmitSelectedChapterQuizService(SubmitChapterQuizRequestModel submitChapterQuizRequestModel)
        {
            SubmitChapterQuizResponseModel submitChapterQuizResponseModel = new SubmitChapterQuizResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(submitChapterQuizRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.SubmitChapterQuizApiEndPoint, true, Utilities.BearerToken);
                submitChapterQuizResponseModel = JsonConvert.DeserializeObject<SubmitChapterQuizResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return submitChapterQuizResponseModel;
        }
        /// <summary>
        /// API integration to get the progress of the chapter category and depends on the classes
        /// </summary>
        /// <param name="submitChapterQuizRequestModel"></param>
        /// <returns></returns>
        public async Task<ChapterProgressResponseModel> GetChapterProgressService(ChapterProgressRequestModel chapterProgressRequestModel)
        {
            ChapterProgressResponseModel chapterProgressResponseModel = new ChapterProgressResponseModel();
            try
            {               
                ResponseModel response = await serviceHelpers.GetRequest(StringConstant.GetChapterProgressApiEndPoint + chapterProgressRequestModel.CategoryId, true, Utilities.BearerToken);
                chapterProgressResponseModel = JsonConvert.DeserializeObject<ChapterProgressResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return chapterProgressResponseModel;
        }
        /// <summary>
        /// API integration to convert XP to jellyfish
        /// </summary>
        /// <param name="convertXPToCryptojellyRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> ConvertXPToCryptoJellyService(ConvertXPToCryptojellyRequestModel convertXPToCryptojellyRequestModel)
        {
            ResponseModel convertXPToCryptojellyResponseModel = new ResponseModel();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(convertXPToCryptojellyRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(serializedRequest, StringConstant.ConvertToCryptoJellyApiEndPoint, true, Utilities.BearerToken);
                convertXPToCryptojellyResponseModel = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return convertXPToCryptojellyResponseModel;
        }
       
        #endregion
    }
}
