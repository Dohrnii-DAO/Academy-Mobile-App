using Xamarin.Forms;

namespace DohrniiFoundation.Helpers
{
    /// <summary>
    /// This class create for define the constant values
    /// </summary> 
    public class StringConstant : ObservableObject
    {
        #region API Constant Values
        public const string APIKeyURL = "http://3.131.169.249"; //"https://dohrniifoundation-qa.chetu.com"; 
        public const string DeepLinkingAPIKeyURL = "3.143.244.210";  //"dohrniifoundation-qa-react.chetu.com";
        public const string APIKeyHttpAuthorization = "Authorization";
        public const string APIKeyBearer = "bearer";
        public const string AccessType = "app";
        public const string SignUpAPIEndPoint = "/api/user/create";
        public const string LoginAPIEndPoint = "/api/user/login";
        public const string ForgotPasswordAPIEndPoint = "/api/user/forgot_password";
        public const string ResetPasswordAPIEndPoint = "/api/user/reset_password";
        public const string ChangePasswordAPIEndPoint = "/api/user/change_password";
        public const string RefreshTokenAPIEndPoint = "/api/auth/refresh";
        public const string LogoutAPIEndPoint = "/api/auth/logout";
        public const string GetQuestionAPIEndPoint = "/api/questionnaire/get_question";
        public const string GetAnswerAPIEndPoint = "/api/questionnaire/answer/";
        public const string GetAllAnswerAPIEndPoint = "/api/questionnaire/answers/all";
        public const string SubmitAllAnswerAPIEndPoint = "/api/questionnaire/save_questionnaire";
        public const string GetUserProfileAPIEndPoint = "/api/user/profile";
        public const string GetUserQuestionnaireAPIEndPoint = "/api/questionnaire/user_questionnaire";
        public const string GetAllStrategiesAPIEndPoint = "/api/strategy/all";
        public const string GetStrategyConceptsEndPoint = "/api/strategy/concept/";
        public const string GetAllLongTermConceptsEndPoint = "/api/concepts/all";
        public const string GetUserStatusApi = "/api/user/status/get";
        public const string GetAllChaptersApiEndPoint = "/api/chapter/all";
        public const string GetLessonsApiEndPoint = "/api/chapter/lesson/relation/";
        public const string GetAllLessonsApiEndPoint = "/api/chapter/lesson/all";
        public const string GetAllChaptersCategoryApiEndPoint = "/api/chapter/category/all";
        public const string GetChaptersLessonsCategoryApiEndPoint = "/api/chapter/categories/";
        public const string GetClassesLessonsIdApiEndPoint = "/api/class/lesson/get?lessonid=";
        public const string GetClassesChapterIdApiEndPoint = "&chapterid=";
        public const string GetQuestionsClassIdApiEndPoint = "/api/question/class/";
        public const string SubmitQuestionsAnswersClassApiEndPoint = "/api/question/attampt";
        public const string GetChapterQuizApiEndPoint = "/api/chapter/quiz/";
        public const string UnlockChapterQuizApiEndPoint = "/api/chapter/quiz/unlock";
        public const string GetChapterProgressApiEndPoint = "/api/chapter/progress/";
        public const string SubmitChapterQuizApiEndPoint = "/api/chapter/quiz/attampt";
        public const string ConvertToCryptoJellyApiEndPoint = "/api/convert/xp/cryptojelly";
        public const string UpdateProfileApiEndPoint = "/api/user/update/";


        public const string APIAndroidBaseURL =  "http://10.0.2.2:8000/backend/api";
        public const string APIiOSBaseURL = "http://127.0.0.1:8000/backend/api";
        public const string APIBaseBearer = "Bearer ";
        public const string LoginEndPoint = "/auth/token/";
        public const string UsersEndPoint = "/users/";
        public const string UserEndPoint = "/users/{id}";
        public const string AppUserEndPoint = "/users/me/";
        public const string SendFriendRequestEndPoint = "/social/friends/request/{user_id}/";

        #endregion

        #region Images and Gif 
        public const string LessonImage = "Group_4824.png";
        public const string InvestorImage = "Group_4780.png";
        public const string StrategiesImage = "Group_4817.png";
        public const string Selected = "Selectedimg";
        public const string Unselected = "Unselectedimg";
        public const string EyeUncut = "eye";
        public const string EyeCut = "eye_cut";
        public const string AppIcon = "AppIconImage.png";
        public const string Applogo = "applogo.png";
        public const string Facebook = "Facebook.png";
        public const string Insta = "Insta.png";
        public const string Twitter = "Twitter.png";
        public const string Back = "back.png";
        public const string Loginbg = "Loginbg.png";
        public const string Dashboardbg = "Dashboardbg.png";
        public const string Mail = "mail.png";
        public const string Lock = "lock.png";
        public const string emailverify = "emailverify.png";
        public const string Verified = "Verified.png";
        public const string Dashboardheader = "Dashboardheader.png";
        public const string Dashboardheaderimg = "Dashboardheaderimg.png";
        public const string Dashboardlearn = "Dashboardlearn.png";
        public const string Dashboardlearnimg = "Dashboardlearnimg.png";
        public const string Dashboardinvest = "Dashboardinvest.png";
        public const string Dashboardinvestimg = "Dashboardinvestimg.png";
        public const string LessonTypeText = "Lesson_Text.png";
        public const string LessonTypeVideo = "Lesson_Video.png";
        public const string LessonTypeImage = "Lesson_Image.png";
        public const string StrategiesDetailText = "StrategiesDetailText.png";
        public const string StrategiesDetailImage = "StrategiesDetailImage.png";
        public const string StrategiesDetailVideo = "StrategiesDetailVideo.png";
        public const string LessonTextTick = "LessonTextTick.png";
        public const string LessonVideoTick = "LessonVideoTick.png";
        public const string HomeProgressIcon = "HomeProgressIcon.png";
        public const string Earned = "Earned.png";
        public const string Bitcoin = "Bitcoin.png";
        public const string NextLesson = "NextLesson.png";
        public const string PleaseComplete = "PleaseComplete.png";
        public const string LongTermNext = "LongTermNext.png";
        public const string MenuHome = "MenuHome.png";
        public const string Learn = "Learn.png";
        public const string Account = "Account.png";
        public const string SelectedAccount = "SelectedAccount.png";
        public const string SelectedSettings = "SelectedSettings.png";
        public const string Settings = "Settings.png";
        public const string dropup = "dropup.png";
        public const string dropdown = "dropdown.png";
        public const string menuheader = "menuheader.png";
        public const string TabIdKey = "TabId";
        public const string EditIcon = "EditIcon.png";
        public const string AnswerSelected = "AnswerSelected.png";
        public const string LongTermConcept1 = "LongTermConcept1.png";
        public const string LongTermConcept2 = "LongTermConcept2.png";
        public const string LongTermConcept3 = "LongTermConcept3.png";
        public const string LongTermConcept5 = "LongTermConcept5.png";
        public const string HomeTabSelected = "HomeSelected.png";
        public const string HomeTabUnselected = "HomeTabUnselected.png";
        public const string LessonTabSelected = "LessonsTabSelected.png";
        public const string LessonTabUnselected = "LessonsTabUnselected.png";
        public const string InvestorTabSelected = "InvestorTabSelected.png";
        public const string InvestorTabUnselected = "InvestorTabUnselected.png";
        public const string StrategiesTabSelected = "StrategiesTabSelected.png";
        public const string StrategiesTabUnselected = "StrategiesTabUnselected.png";
        public const string MoreSelected = "MoreSelected.png";
        public const string MoreUnselected = "MoreUnselected.png";
        public const string Collapse = "Collapse.png";
        public const string Expand = "Expand.png";
        public const string Jellyfish = "Jellyfish.png";
        public const string LessonsDropup = "LessonsDropup.png";
        public const string LessonsBg = "LessonsBg.png";
        public const string IsLessonOnboarding = "IsLessonOnboarding.png";
        public const string LessonsDropdown = "LessonsDropdown.png";
        public const string BackArrow = "BackArrow.png";
        public const string ChaptersBg = "ChaptersBg.png";
        public const string LessonsTick = "LessonsTick.png";
        public const string LessonLock = "LessonLock.png";
        public const string EmailIcon = "EmailIcon.png";
        public const string PasswordIcon = "PasswordIcon.png";
        public const string UserIcon = "UserIcon.png";
        public const string Google = "Google.png";
        public const string Fb = "Fb.png";
        public const string Apple = "Apple.png";
        public const string CheckBox = "CheckBox.png";
        public const string UncheckBox = "UncheckBox.png";
        public const string ClassCompletedFish = "ClassCompletedFish.png";
        public const string OnboardingJellyfish = "OnboardingJellyfish.png";
        public const string OnboardingXPJellyfish = "OnboardingXPJellyfish.png";
        public const string OnboardingCryptoJellyfish = "OnboardingCryptoJellyfish.png";
        public const string OnboardingDhnJellyfish = "OnboardingDhnJellyfish.png";
        public const string BottomXPJellyfish = "BottomXPJellyfish.png";
        public const string QuizBackgrondImage = "QuizBackgrondImage.png";
        public const string Cancel = "Cancel.png";
        public const string QuizCompleteJelly = "QuizCompleteJelly.png";
        public const string EditProfileIcon = "EditProfileIcon.png";
        public const string NextIcon = "NextIcon.png";
        public const string LockIcon = "LockIcon.png";
        public const string FingerPrint = "FingerPrint.png";
        public const string FaqIcon = "FaqIcon.png";
        public const string HeadphonesIcon = "HeadphonesIcon.png";
        public const string PolicyIcon = "PolicyIcon.png";
        public const string LogoutIcon = "LogoutIcon.png";
        public const string SupportFlag = "SupportFlag.png";
        public const string ClassCompleteJelly1 = "ClassCompleteJelly1.png";
        public const string ClassCompleteJelly4 = "ClassCompleteJelly4.png";
        public const string ClassCompleteJelly2 = "ClassCompleteJelly2.png";
        public const string PassedTick = "PassedTick.png";
        public const string CameraIcon = "CameraIcon.png";        
        public const string ClassCompleteGif = "ClassComplete.gif";
        #endregion

        #region Constant Values
        public const string LongTermConcept = "Lorem ipsum is a there place common the not form?";
        public const string PageType = "PageType";
        public const string QuestionnairePage = "QuestionnairePage";
        public const string InvestorsProfilePage = "InvestorsProfilePage";
        public const string ResetPasswordToken = "ResetPasswordToken";
        public const string IsRemember = "IsRemember";
        public const string UserId = "UserId";
        public const string QuestionnaireTaken = "QuestionnaireTaken";
        public const string RetakeRequired = "RetakeRequired";
        public const string TextType = "text";
        public const string ImageType = "image";
        public const string VideoType = "video";
        public const string AssignedStrategyId = "AssignedStrategyId";
        public const string Watch = "watch";
        public const string Youtube = "youtube";
        public const string Upload = "upload";
        public const string JSONContentType = "application/json";
        public const string EmailRegex = @"\A(?:[a-z0-9]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public const string passwordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z])\\S{8,16}$";
        public const string PhoneRegex = "\\A[0-9]{3}-[0-9]{3}-[0-9]{4}\\z";
        public const string ContactNumberRegex = "^(?!0+$)\\d{10,}$";
        public const string AlphabetsRegex = @"^[a-zA-Z]+$";
        public const string UserNameRegex = @"^[a-zA-Z0-9-]+$";
        public const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/|youtube\\/|)([a-zA-Z0-9_-]{11})+";
        public const int UnFocusedEntryFontSize = 14;
        public const double UnFocusedEntryLableScaleX = 1;
        public const double UnFocusedEntryLableScaleY = 1;
        public const double UnFocusedEntryLableTranslationX = 0;
        public const double UnFocusedEntryLableTranslationY = 0;
        public const int FocusedEntryFontSize = 18;
        public const double FocusedEntryLableScaleX = 0.8;
        public const double FocusedEntryLableScaleY = 0.8;
        public const double FocusedEntryLableTranslationX = 0;
        public const double FocusedEntryLableTranslationY = 30;
        public const int FocusedEntryPasswordFontSize = 18;
        public const double FocusedEntryPasswordLableScaleX = 0.8;
        public const double FocusedEntryPasswordLableScaleY = 0.8;
        public const double FocusedEntryPasswordLableTranslationX = 0;
        public const double FocusedEntryPasswordLableTranslationY = 30;
        public static Color EntryLableTextColor = (Color)Application.Current.Resources["LightGrayText"];
        public static Color FocusedEntryPasswordLableTextColor = (Color)Application.Current.Resources["BlackText"];
        public static Color FocusedEntryLableTextColor = (Color)Application.Current.Resources["BlackText"];
        public const char AnswerAlphabet = 'a';
        public const string LastItemText = "Last Item";
        public const string CorrectAnswerValueOne = "1";
        public const string WrongAnswerValueZero = "0";
        public const string QuestionClassType = "class";
        public const string QuestionQuizType = "quiz";
        public const string ClassUnlocked = "0";
        public const string ClassPassed = "1";
        public const string ClassLocked = "2";
        public const string LessonInprogress = "0";
        public const string LessonComplete = "1";
        public const string LessonsLocked = "2";
        public const string Unlocked = "Unlocked";
        public const string Locked = "Locked";
        public const string Complete = "Complete";
        public const string UpdateProfilePrefix = "data:image/png;base64,";
        public const string UpdateProfileRefresh = "UpdateProfileRefresh";
        public const string Symbol = "Symbol";
        public const string UpdateClassesApi = "UpdateClassesApi";
        public const string UpdateUserXPRefresh = "UpdateUserXPRefresh";
        public const string DashSymbol = "-";
        public const string DHNValue = "50";
        public const string PercentSymbol = "%";
        public const string LessonsPage = "LessonsPage";
        public const string LessonChaptersPage = "LessonChaptersPage";
        public const string Publickey = "12345678";
        public const string Secretkey = "87654321";
        public const string reset_password = "reset_password";


        public const string PrimaryBtnColor = "#7B98FE";
        public const string Transparent = "#00000000";


        #endregion

        #region Html Content
        public const string NotBlurHtmlClassContent = "<p><span style ='color: rgb(255, 0, 0)'> We’ve taken Lorem Ipsum to the next level with our HTML - Ipsum tool.As you can see,this Lorem Ipsum is tailor - made for websites and other online projects that are based in HTML.Most <a href = 'https://www.webfx.com/web-design/pricing/website-costs/' rel = 'nofollow' > web design projects </a>use Lorem Ipsum excerpts to begin with, but you always have to spend an annoying extra minute or two formatting it properly for the web.</span></p><p><span style = 'color: rgb(255, 0, 0)' ><br></span></p><p><span style = 'color: rgb(255, 0, 0)'><img src = 'https://myawsdhorniibucket.s3.us-east-2.amazonaws.com/dohrnii/images/classes/12/illustration_image/illustration_12.jpeg' width = '220px' height = '123px' style = 'float: left; width: 220px; height: 123px;' ></span></p>";
        public const string BlurHtmlClassContent1 = "<div style ='filter:blur(5px)'>";
        public const string BlurHtmlClassContent2 = "</div>";
        public const string BlurClassTitleContent1 = "<p><span style ='color:rgb(0, 0, 0);filter:blur(5px);font-size: 24px'>";
        public const string BlurClassTitleContent2 = "</span ></p>";
        public const string BlurClassNumberContent1 = "<p><span style ='color:rgb(120, 146, 253);filter:blur(5px);font-size: 18px'>";
        public const string BlurClassNumberContent2 = "</span ></p>";
        public const string BlurStartButtonContent1 = "<button style='border-color: #7892FD;height: 50px;border-radius: 8px;width: -webkit-fill-available;margin-left: 32px;margin-right: 32px;margin-top: 24px;margin-bottom: 8px;background-color: #7892FD;color: white;font-size: 12px;font-family: MonumentExtended-Regular;filter: blur(5px)'>";
        public const string BlurStartButtonContent2 = "</button>";

        public const string HtmlContentWithFont =
            @"
<html>
<head>
    <style>
        @font-face {
        font-family: monument_extended_regular;
        src: url('[[fontpath]]MonumentExtended-Regular.otf');
        }

        @font-face {
        font-family: poppins_regular;
        src: url('[[fontpath]]PoppinsRegular.ttf');
        }
        
        p {
            font-family: poppins_regular;
            font-size: 14px;
            font-weight: 400;
            line-height: 141%;
            letter-spacing: -0.01em;
            color: #070707;
        }
        span {
            font-family: monument_extended_regular;
            color: #7892FD;
            font-size: 14px;
            font-weight: 400;
            line-height: 141%;
            letter-spacing: -0.01em;
        }
    </style>
</head>
<body>
    <p>The internet was meant to be free of censorship and free of centralised control. But nowadays we notice that doesn't come true.</p>

    <p>
        There are powerful <br>
        <span>GATEKEEPERS</span> that ban and
        censor what we can read online.
    </p>


    <p>
        Governments  <br>
        Censorship is not generally bad i.e protecting children from unsuitable content or blocking hateful and violent content. A lot of democratic countries are doing so.

    </p>
    <p>
        Tech Giants  <br>
        Some companies delete information from their platform or ban people so they can’t share information or “fake” news. If you make a search with a search engine like google their algorithm determines what kind of Information you will see.

    </p>

    <p>
        Most blockchains are <span>CENSORSHIP-FREE</span>
        People can add infromation in the blockchain without going through intermediary platforms. Some crypto projects use blockchain technology to build a decentralised social network.
    </p>


    <p>
        <span>
            Redactable <br>
            Blockchain <br />
        </span>

        There are discussions
        if it should be possible
        to delete information
        on a blockchain and how
        it would work.
    </p>

    <p>One idea is to include a consensus-based <span>VOTING</span> into the blockchain protocol which allows to delete information if the majority agrees. </p>

    <p>Another approach is filtering data <span>BEFORE</span> it is added to the blockchain.</p>

</body>
</html>
";
        #endregion

    }
}
