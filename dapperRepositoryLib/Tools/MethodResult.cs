using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL.CDP.Tools
{
    /// <summary>
    /// Class for return a method
    /// </summary>
    public class MethodResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        public MethodResult(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodResult"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public MethodResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Success = errorMessage.IsNullOrEmpty();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MethodResult"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

    }
}
