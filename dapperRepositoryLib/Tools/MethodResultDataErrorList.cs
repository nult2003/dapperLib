using System.Collections.Generic;

namespace BTL.CDP.Tools
{
    /// <summary>
    /// Class containing the result of a method - with a complex error object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MethodResultDataErrorList<T> : MethodResultData<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result"></param>
        /// <param name="errors"></param>
        /// <param name="errorMessage"></param>
        public MethodResultDataErrorList(T result, IEnumerable<ErrorItem> errors = null, string errorMessage = null) : base(result, errorMessage)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Dictionary containing errors in format : property name - error message
        /// </summary>
        public IEnumerable<ErrorItem> Errors { get; protected set; }
    }
}