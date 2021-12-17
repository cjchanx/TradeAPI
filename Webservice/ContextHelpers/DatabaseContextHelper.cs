using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService;
using DatabaseLibrary.Core;
using DatabaseLibrary.Providers;

namespace Webservice.ContextHelpers
{
    /// <summary>
    ///  REFERENCE : The following class is based on a class example from CPSC471F2021 Week 8 Lectures.
    /// This class sets up the database.
    /// </summary>
    public class DatabaseContextHelper
    {

        #region Initialization

        /// <summary>
        /// Reference to the app settings helper instance added in the Startup.cs.
        /// </summary>
        private readonly AppSettingsHelper AppSettings;

        /// <summary>
        /// Constructor called by the service provider.
        /// Using injection to get the parameters.
        /// </summary>
        public DatabaseContextHelper(AppSettingsHelper appSettings)
        {
            AppSettings = appSettings;
        }

        #endregion

        #region Variables

        /// <summary>
        /// Retrieves a database context.
        /// </summary>
        public DBContext DBContext
        {
            get
            {
                return new DatabaseLibrary.Providers.MySQLContext(AppSettings.DATABASE_CONNECTION_STRING);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the databases and applies migrations.
        /// </summary>
        public bool Initialize(out string message)
        {
            return DBContext.Initialize(out message);
        }

        #endregion

    }
}
