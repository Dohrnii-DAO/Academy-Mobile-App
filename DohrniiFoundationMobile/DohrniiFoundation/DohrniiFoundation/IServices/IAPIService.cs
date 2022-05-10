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
using System.Threading.Tasks;

namespace DohrniiFoundation.IServices
{
    /// <summary>
    /// This interface is used for define service methods
    /// </summary>
    public interface IAPIService
    {
        /// <summary>
        /// This API is for the login into the app
        /// </summary>
        /// <param name="loginAPIRequest"></param>
        /// <returns></returns>
        Task<LoginResponseModel> LoginService(LoginRequestModel loginAPIRequest);
        /// <summary>
        /// This API is used for user sign up in the app
        /// </summary>
        /// <param name="signUpRequestModel"></param>
        /// <returns></returns>
        Task<ResponseModel> SignUpService(SignUpRequestModel signUpRequestModel);
        /// <summary>
        /// This API is used for forgot password in the app
        /// </summary>
        /// <param name="forgotPasswordRequestModel"></param>
        /// <returns></returns>
        Task<ResponseModel> ForgotPasswordService(ForgotPasswordRequestModel forgotPasswordRequestModel);
        /// <summary>
        /// This API is used for reset password in the app
        /// </summary>
        /// <param name="resetPasswordRequestModel"></param>
        /// <returns></returns>
        Task<ResponseModel> ResetPasswordService(ResetPasswordRequestModel resetPasswordRequestModel);
        /// <summary>
        /// This API is used for change password in the app
        /// </summary>
        /// <param name="changePasswordRequestModel"></param>
        /// <returns></returns>
        Task<ResponseModel> ChangePasswordService(ChangePasswordRequestModel changePasswordRequestModel);
        /// <summary>
        /// This API is used to logout the app
        /// </summary>
        /// <returns></returns>
        Task<ResponseModel> LogoutService();
        /// <summary>
        /// This API is used for get questions for the questionnaire in the app
        /// </summary>
        /// <returns></returns>
        Task<QuestionnaireResponseModel> GetQuestionService();
        /// <summary>
        /// This API is used to get answer of the question in questionnaire depends on the ques id in the app
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<AnswersResponseModel> GetAnswersService(int questionId);
        /// <summary>
        /// This API is used tp get all the answers of the questionnaire in the app
        /// </summary>
        /// <returns></returns>
        Task<AnswersResponseModel> GetAllAnswersService();
        /// <summary>
        ///  This API is used to submit the questionnaire in the app
        /// </summary>
        /// <param name="submitQuestionnaireRequestModel"></param>
        /// <returns></returns>
        Task<SubmitQuestionnaireResponseModel> SubmitSelectedAnswersService(SubmitQuestionnaireRequestModel submitQuestionnaireRequestModel);
        /// <summary>
        ///  This API is used to get the user profile details in the app
        /// </summary>
        /// <returns></returns>
        Task<UserProfileResponseModel> GetUserProfileService();
        /// <summary>
        /// This API is used to update the user profile in the app
        /// </summary>
        /// <param name="updateProfileRequestModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UpdateProfileResponseModel> UpdateProfileService(UpdateProfileRequestModel updateProfileRequestModel, int userId);
        /// <summary>
        /// This API is used to get the user questions of the investor's profile in the app
        /// </summary>
        /// <returns></returns>
        Task<SubmitQuestionnaireResponseModel> GetUserQuestionnaireService();
        /// <summary>
        /// This API is used all strategies of the investor's profile in the app
        /// </summary>
        /// <returns></returns>
        Task<StrategiesResponseModel> GetAllStrategiesService();
        /// <summary>
        /// This API is used to get the strategy concepts based on the strategy id in the app
        /// </summary>
        /// <param name="strategyId"></param>
        /// <returns></returns>
        Task<StrategyConceptsResponseModel> GetStrategyConceptsService(int strategyId);
        /// <summary>
        /// This API is used to get all the long terms concepts of the strategy in the app
        /// </summary>
        /// <returns></returns>
        Task<LongTermConceptsResponseModel> GetAllLongTermConceptsService();
        /// <summary>
        /// This API is used to get the user status in the app
        /// </summary>
        /// <returns></returns>
        Task<StrategyStatusResponseModel> GetUserStatusService();
        /// <summary>
        /// This API is used to get the chapters and lessons depends upon the chapters category in the app
        /// </summary>
        /// <param name="chaptersCategoryRequestModel"></param>
        /// <returns></returns>
        Task<LessonsResponseModel> GetChaptersLessonsCategoryService(ChaptersCategoryRequestModel chaptersCategoryRequestModel);
        /// <summary>
        /// This API is used to get all the chapters category in the app
        /// </summary>
        /// <returns></returns>
        Task<ChaptersCategoryResponseModel> GetAllChaptersCategoryService();
        /// <summary>
        /// This API is used to get the classes depends on the selected lesson in the app
        /// </summary>
        /// <param name="selectedLessonRequestModel"></param>
        /// <returns></returns>
        Task<ClassesResponseModel> GetClassesLessonIdService(SelectedLessonRequestModel selectedLessonRequestModel);
        /// <summary>
        /// This API is used to get the questions of the selected class in the app
        /// </summary>
        /// <param name="selectedClassRequestModel"></param>
        /// <returns></returns>
        Task<ClassesQuestionsResponseModel> GetQuestionsClassIdService(SelectedClassRequestModel selectedClassRequestModel);
        /// <summary>
        /// This API is used to submit the selected class questions with answers in the app
        /// </summary>
        /// <param name="selectedClassRequestModel"></param>
        /// <returns></returns>
        Task<SubmitClassQuestionsResponseModel> SubmitSelectedClassAnswersService(SubmitClassQuestionsRequestModel selectedClassRequestModel);
        /// <summary>
        /// This API is used to get the chapters quiz depends on the chapters in the app
        /// </summary>
        /// <param name="chapterQuizRequestModel"></param>
        /// <returns></returns>
        Task<ChapterQuizResponseModel> GetChapterQuizService(UnlockChapterQuizRequestModel chapterQuizRequestModel);
        /// <summary>
        /// This API is used to unlock the chapters quiz in the app
        /// </summary>
        /// <param name="unlockChapterQuizRequestModel"></param>
        /// <returns></returns>
        Task<ResponseModel> UnlockChapterQuizService(UnlockQuizRequestModel unlockChapterQuizRequestModel);
        /// <summary>
        /// This API is used to submit the selected chapters quiz questions in the app
        /// </summary>
        /// <param name="submitChapterQuizRequestModel"></param>
        /// <returns></returns>
        Task<SubmitChapterQuizResponseModel> SubmitSelectedChapterQuizService(SubmitChapterQuizRequestModel submitChapterQuizRequestModel);
        /// <summary>
        /// This API is used to get the chapters progress in the app
        /// </summary>
        /// <param name="chapterProgressRequestModel"></param>
        /// <returns></returns>
        Task<ChapterProgressResponseModel> GetChapterProgressService(ChapterProgressRequestModel chapterProgressRequestModel);
        /// <summary>
        /// This API is used to convert the XP to crypto jelly in the app
        /// </summary>
        /// <param name="convertXPToCryptojellyRequestModel"></param>
        /// <returns></returns>
        Task<ResponseModel> ConvertXPToCryptoJellyService(ConvertXPToCryptojellyRequestModel convertXPToCryptojellyRequestModel);
    }
}
