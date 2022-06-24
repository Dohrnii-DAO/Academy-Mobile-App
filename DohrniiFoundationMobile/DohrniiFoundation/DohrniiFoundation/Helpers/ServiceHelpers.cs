using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DohrniiFoundation.Enum;
using DohrniiFoundation.Models.APIResponseModels;

namespace DohrniiFoundation.Helpers
{
    /// <summary>
    /// This class use for handle get and post service request
    /// </summary>
    public sealed class ServiceHelpers
    {
        #region Private Members      
        private static readonly object lockInstance = new object();
        private static ServiceHelpers instance = null;
        ResponseModel responseModel = null;
        #endregion

        #region Properties
        public static ServiceHelpers Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockInstance)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceHelpers();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for Post Request
        /// </summary>
        /// <param name="strSerializedData"></param>
        /// <param name="strMethod"></param>
        /// <param name="isHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel> PostRequest(string strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Add(StringConstant.APIKeyHttpAuthorization, string.Concat(StringConstant.APIKeyBearer, string.Empty, token));
                    }
                    string baseUrl = StringConstant.APIKeyURL;
                    string serviceUrl = string.Empty;
                    serviceUrl = baseUrl + strMethod;

                    httpClient.Timeout = TimeSpan.FromSeconds(100);
                    var uri = new Uri(serviceUrl);
                    using (StringContent content = new StringContent(strSerializedData, Encoding.UTF8, StringConstant.JSONContentType))
                    {
                        HttpResponseMessage response = null;
                        response = await httpClient.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                            this.responseModel = new ResponseModel
                            {
                                IsSuccess = true,
                                Data = result
                            };
                            return this.responseModel;
                        }
                        else
                        {
                            switch (response.StatusCode)
                            {
                                case (HttpStatusCode)ResponseStatus.NotAcceptable:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case (HttpStatusCode)ResponseStatus.BadRequest:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case (HttpStatusCode)ResponseStatus.Unauthorized:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case (HttpStatusCode)ResponseStatus.NotFound:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case HttpStatusCode.BadRequest:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                default:
                                    break;
                            }
                        }
                        this.responseModel = new ResponseModel
                        {
                            IsSuccess = false,
                            Data = result
                        };
                        return this.responseModel;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
                this.responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return this.responseModel;
            }
        }

        /// <summary>
        /// Method for Get Request
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="isHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel> GetRequest(string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Add(StringConstant.APIKeyHttpAuthorization, string.Concat(StringConstant.APIKeyBearer, string.Empty, token));
                    }
                    string baseUrl = StringConstant.APIKeyURL;
                    string serviceUrl = baseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(200);
                    var uri = new Uri(serviceUrl);
                    HttpResponseMessage response = null;
                    response = httpClient.GetAsync(uri).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                        this.responseModel = new ResponseModel
                        {
                            IsSuccess = true,
                            Data = result
                        };
                        return this.responseModel;
                    }

                    {
                        switch (response.StatusCode)
                        {
                            case (HttpStatusCode)ResponseStatus.NotAcceptable:
                                result = response.Content.ReadAsStringAsync().Result;
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case (HttpStatusCode)ResponseStatus.BadRequest:
                                result = response.Content.ReadAsStringAsync().Result;
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case (HttpStatusCode)ResponseStatus.Unauthorized:
                                result = response.Content.ReadAsStringAsync().Result;
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case (HttpStatusCode)ResponseStatus.NotFound:
                                result = response.Content.ReadAsStringAsync().Result;
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case HttpStatusCode.BadRequest:
                                result = response.Content.ReadAsStringAsync().Result;
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case HttpStatusCode.InternalServerError:
                                result = response.Content.ReadAsStringAsync().Result;
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            default:
                                break;
                        }
                    }

                    this.responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = string.Empty
                    };
                    return this.responseModel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return this.responseModel;
            }
        }
        #endregion



        #region Methods
        /// <summary>
        /// Method for Post Request
        /// </summary>
        /// <param name="strSerializedData"></param>
        /// <param name="strMethod"></param>
        /// <param name="isHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel> PostRequestAsync(string strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Add(StringConstant.APIKeyHttpAuthorization, string.Concat(StringConstant.APIBaseBearer, string.Empty, token));
                    }
                    string baseUrl = StringConstant.APIBaseURL;
                    string serviceUrl = string.Empty;
                    serviceUrl = baseUrl + strMethod;

                    httpClient.Timeout = TimeSpan.FromSeconds(100);
                    var uri = new Uri(serviceUrl);
                    using (StringContent content = new StringContent(strSerializedData, Encoding.UTF8, StringConstant.JSONContentType))
                    {
                        HttpResponseMessage response = null;
                        response = await httpClient.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsStringAsync();
                            this.responseModel = new ResponseModel
                            {
                                IsSuccess = true,
                                Data = result
                            };
                            return this.responseModel;
                        }
                        else
                        {
                            switch (response.StatusCode)
                            {
                                case (HttpStatusCode)ResponseStatus.NotAcceptable:
                                    result = await response.Content.ReadAsStringAsync();
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case (HttpStatusCode)ResponseStatus.BadRequest:
                                    result = await response.Content.ReadAsStringAsync();
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case (HttpStatusCode)ResponseStatus.Unauthorized:
                                    result = await response.Content.ReadAsStringAsync();
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case (HttpStatusCode)ResponseStatus.NotFound:
                                    result = await response.Content.ReadAsStringAsync();
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                case HttpStatusCode.BadRequest:
                                    result = await response.Content.ReadAsStringAsync();
                                    this.responseModel = new ResponseModel
                                    {
                                        IsSuccess = false,
                                        Data = result
                                    };
                                    break;
                                default:
                                    break;
                            }
                        }
                        this.responseModel = new ResponseModel
                        {
                            IsSuccess = false,
                            Data = result
                        };
                        return this.responseModel;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
                this.responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return this.responseModel;
            }
        }

        /// <summary>
        /// Method for Get Request
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="isHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel> GetRequestAsync(string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Add(StringConstant.APIKeyHttpAuthorization, string.Concat(StringConstant.APIBaseBearer, string.Empty, token));
                    }
                    string baseUrl = StringConstant.APIBaseURL;
                    string serviceUrl = baseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(200);
                    var uri = new Uri(serviceUrl);
                    HttpResponseMessage response = null;
                    response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        this.responseModel = new ResponseModel
                        {
                            IsSuccess = true,
                            Data = result
                        };
                        return this.responseModel;
                    }

                    {
                        switch (response.StatusCode)
                        {
                            case (HttpStatusCode)ResponseStatus.NotAcceptable:
                                result = await response.Content.ReadAsStringAsync();
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case (HttpStatusCode)ResponseStatus.BadRequest:
                                result = await response.Content.ReadAsStringAsync();
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case (HttpStatusCode)ResponseStatus.Unauthorized:
                                result = await response.Content.ReadAsStringAsync();
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case (HttpStatusCode)ResponseStatus.NotFound:
                                result = await response.Content.ReadAsStringAsync();
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case HttpStatusCode.BadRequest:
                                result = await response.Content.ReadAsStringAsync();
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            case HttpStatusCode.InternalServerError:
                                result = await response.Content.ReadAsStringAsync();
                                this.responseModel = new ResponseModel
                                {
                                    IsSuccess = false,
                                    Data = result
                                };
                                break;
                            default:
                                break;
                        }
                    }

                    this.responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = string.Empty
                    };
                    return this.responseModel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return this.responseModel;
            }
        }
        #endregion



    }
}
