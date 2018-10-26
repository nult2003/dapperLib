using System.Collections.Generic;

namespace BTL.CDP.Tools
{
    /// <summary>
    /// Error Item containing property errors
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        /// <param name="message"></param>
        public ErrorItem(string property, string message)
        {
            this.Property = property;
            this.Messages = new string[] { message };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        /// <param name="messages"></param>
        public ErrorItem(string property, IEnumerable<string> messages)
        {
            this.Property = property;
            this.Messages = messages;
        }

        /// <summary>
        /// Property name that has the error
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// List of errors of the property
        /// </summary>
        public IEnumerable<string> Messages { get; set; }
    }
}