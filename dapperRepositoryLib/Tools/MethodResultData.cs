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
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CDP.Tools.MethodResult" />
    public class MethodResultData<T> : MethodResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodResultData{T}"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="errorMessage">The error message.</param>
        public MethodResultData(T result, string errorMessage = null) : base(errorMessage)
        {
            Result = result;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public T Result { get; protected set; }
    }
}
