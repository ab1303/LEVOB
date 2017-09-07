namespace Glossary.WebApi.Models
{
    /// <summary>
    /// Base Api Response, including the message to be sent back over the pipeline
    /// </summary>
    public class BaseApiResponse
    {
        /// <summary>
        /// Message Object of Response
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Code of type Internal Api Status Codes
        /// </summary>
        public InternalApiStatusCode Code { get; set; }

    }


    /// <summary>
    /// List of internal status codes to be sent back in response
    /// Convention of each enum code to prefixed with 7000
    /// </summary>
    public enum InternalApiStatusCode
    {
        /// <summary>
        /// Some of the parameters passed to request are invalid
        /// </summary>
        FailedRequestValidation = 70001,
        /// <summary>
        /// Response sent back is error; All encompassing error code when no other status code is applicable
        /// </summary>
        Error = 70002,
        /// <summary>
        /// Response sent back is successful; All encompassing success code when no other status code is applicable
        /// </summary>
        Success = 70003,
        /// <summary>
        /// Response sent when there is a possible conflict with a resource
        /// </summary>
        Conflict = 70004

    }
}