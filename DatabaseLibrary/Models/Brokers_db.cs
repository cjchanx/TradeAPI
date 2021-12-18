using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Brokers_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Brokers_db() { }

        /// <summary>
        /// Account constructor with parameters.
        /// </summary>
        /// <param name="active"></param>
        /// <param name="broker"></param>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Brokers_db(string name, string website)
        {
            Name = name;
            Website = website;
        }

        #endregion

        #region Properties


        public string Name { get; set; }

        public string Website { get; set; }

        #endregion
    }
}
