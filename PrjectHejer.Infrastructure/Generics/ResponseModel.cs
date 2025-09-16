namespace PrjectHejer.Infrastructure.Generics
{
    public class ResponseModel<T>
    {
        /// <summary>
        /// Indicates whether the request was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A message describing the result of the request.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The data returned in the response, can be any type.
        /// </summary>
        public T Data { get; set; }

        ///// <summary>
        ///// Default Constructor
        ///// </summary>
        //public ResponseModel()
        //{

        //}

        /// <summary>
        /// Can Pass Arguments to the constructor
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ResponseModel(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Create Success Response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResponseModel<T> CreateSuccessResponse(T data, string message = "Request successful")
        {
            return new ResponseModel<T>(true, message, data);
        }

        /// <summary>
        /// Create Error Response
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResponseModel<T> CreateErrorResponse(string message, T data = default)
        {
            return new ResponseModel<T>(false, message, data);
        }
    }
}
