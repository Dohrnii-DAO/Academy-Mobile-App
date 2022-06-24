using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AppCenter.Crashes;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Services;
using Xamarin.Essentials;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Models.APIResponseModels;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Xamarin.Forms;
using DohrniiFoundation.Views;
using System.Collections.ObjectModel;
using DohrniiFoundation.Models.InvestorsProfileRequestModels;
using DohrniiFoundation.Models.InvestorsProfileModel;
using DohrniiFoundation.Models.Lessons;
using System.Threading.Tasks;

namespace DohrniiFoundation.Helpers
{
    /// <summary>
    /// This class use for define reusable method
    /// </summary>
    public class Utilities
    {
        #region Private Properties Variables
        private static IAPIService aPIService;
        private static string bearerToken;
        private static DateTime apiCallTime;
        #endregion       

        #region Public Properties
        public static string AccessToken
        {
            get
            {
                return Preferences.Get("accessToken", string.Empty);
            }
        }
        /// <summary>
        /// Property to handle the refresh token API when the access token expires
        /// </summary>
        public static string BearerToken
        {
            get
            {
                try
                {
                    double minutes = ApiCallTime.Subtract(DateTime.UtcNow).TotalMinutes;
                    if (minutes < 5)
                    {
                        var accessToken = RefreshTokenTimer(bearerToken);
                        return accessToken.ToString();
                    }
                    else
                    {
                        return bearerToken;
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }

                return bearerToken;
            }
            set
            {
                bearerToken = value;
            }
        }
        /// <summary>
        /// This is the api call time property where stores the value when app gets login
        /// </summary>
        public static DateTime ApiCallTime
        {
            get
            {
                return apiCallTime;
            }
            set { apiCallTime = value; }
        }
        public static SubmitQuestionnaireRequestModel SubmittedSelectedAnswerList;
        public static AnswersModel SelectedAnswerAdd;
        public static bool StrategyAssigned { get; set; }
        public static bool IsFromStrategyPage { get; set; }
        public static int PlayedVideoTime { get; set; }
        public static int PlayedVideoZoomTime { get; set; }
        public static string VimeoVideoUrl { get; set; }
        public static string SelectedLessonTypePoints { get; set; }
        public static ChaptersCategoryModel ChaptersCategorySelected { get; set; }
        public static ObservableCollection<ClassModel> ClassesSelectedList { get; set; }
        public static ObservableCollection<ClassesProgressBarModel> SelectedClassesProgressBarList { get; set; }
        public static bool IsFromLessonsPopUp { get; set; }
        public static QuestionModel SelectedClassAns { get; set; }
        public static Lessons SelectedLesson { get; set; }
        public static ChaptersModel SelectedChapter { get; set; }
        public static ChaptersModel QuizSelectedChapter { get; set; }
        public static ClassModel SelectedClass { get; set; }
        public static string QuestionType { get; set; }
        public static int ClassesXPCollected { get; set; }
        public static string TotalXPCollected { get; set; }
        public static int UserTotalCryptoJelly { get; set; }
        public static int XPPerCryptoJelly { get; set; }
        public static int TotalXP { get; set; }
        public static bool IsAllClassCompleted { get; set; }
        public static bool IsFromClassCompletePage { get; set; }
        public static bool IsFromQuizCompletePage { get; set; }
        public static int XPClassRewarded { get; set; }
        public static string CollectDHN { get; set; }
        public static ObservableCollection<ChaptersModel> ChaptersListData { get; set; }
        public static decimal ChaptersProgress { get; set; }
        public static string UploadedProfileImage { get; set; }
        public static string UserName { get; set; }
        public static string UploadedBase64Image { get; set; }
        public static int ChapterQuizRandom { get; set; }
        public static int ClassQuestionsRandom { get; set; }
        public static string ClassXPTokensCollected { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This contructor is used for initialize 
        /// </summary>
        public Utilities()
        {
            aPIService = new APIServices();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method is used to Encrypt password
        /// </summary>
        /// <param name="clearText">Password</param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            string result;
            try
            {
                string textToEncrypt = plainText;
                string ToReturn = string.Empty;
                string publickey = StringConstant.Publickey;
                string secretkey =StringConstant.Secretkey;
                byte[] secretkeyByte = { };
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                result = ToReturn;
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// Method is used to Decrypt password
        /// </summary>
        /// <param name="cipherText">password</param>
        /// <returns></returns>        
        public static string Decrypt(string cipherText)
        {
            string result;
            try
            {
                string textToDecrypt = cipherText;
                string ToReturn = string.Empty;
                string publickey = StringConstant.Publickey;
                string secretkey = StringConstant.Secretkey;
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                result = ToReturn;
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// Method to check if email is valid or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(string value)
        {
            try
            {
                bool isEmail = Regex.IsMatch(value.Trim(), StringConstant.EmailRegex);
                return isEmail;
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }

        /// <summary>
        /// Method to check if phone is valid or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidPhoneNumber(string value)
        {
            try
            {
                bool isPhone = Regex.IsMatch(value.Trim(), StringConstant.PhoneRegex);
                return isPhone;
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }

        /// <summary>
        /// Method to check if phone is valid or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string value)
        {
            try
            {
                bool isPassword = Regex.IsMatch(value.Trim(), StringConstant.passwordRegex);
                return isPassword;
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }
        /// <summary>
        /// Method to implement the refresh token API when the access token expires
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>        
        public static async Task<string> RefreshTokenTimer(string accessToken)
        {
            ResponseModel refreshTokenResponse = new ResponseModel();
            try
            {
                ResponseModel response = await ServiceHelpers.Instance.PostRequest(string.Empty, StringConstant.RefreshTokenAPIEndPoint, true, accessToken);
                if (response != null)
                {
                    refreshTokenResponse = JsonConvert.DeserializeObject<ResponseModel>(response.Data);
                    Preferences.Set(DFResources.AccessTokenText, refreshTokenResponse.Access_token);
                    Preferences.Set(DFResources.AccessTokenExpiryText, refreshTokenResponse.Expires_in);
                }                
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            return refreshTokenResponse.Access_token;
        }
        #endregion   
    }
}
