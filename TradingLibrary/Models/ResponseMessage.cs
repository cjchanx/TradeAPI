using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    /// <summary>
    ///  REFERENCE : The following class is based on a class example from CPSC471F2021 Week 8 Lectures.
    /// This class is a response message for data.
    /// </summary>
    public class ResponseMessage
    {
        #region Constructors
        /// <summary>
        ///  Default constructor for serialization.
        /// </summary>
        public ResponseMessage() {}

        /// <summary>
        /// Account constructor with parameters.
        /// </summary>
        public ResponseMessage(bool success, string message, object data)
        {
            Success = success;
            Message = message;
               Data = data;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Response status.
        /// True = Response was successful, data usable as is.
        /// False = Response was not successful, data unusable.
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// Server message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Requested data
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }


        #endregion

        #region Methods

        #endregion
    }
}
