using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005D8 RID: 1496
	internal sealed class OdbcConnectionStringService : IConnectionStringService
	{
		// Token: 0x06002E9B RID: 11931 RVA: 0x000020FD File Offset: 0x000002FD
		private OdbcConnectionStringService()
		{
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x0008E118 File Offset: 0x0008C318
		public bool ValidateSourceConnectionString(string connectionString, out string errorMessage)
		{
			bool flag;
			try
			{
				OdbcConnectionStringHandler.Windows.ValidateSource(connectionString);
				OdbcConnectionStringHandler.Windows.ValidateSourceWithPermission(connectionString, null);
				errorMessage = null;
				flag = true;
			}
			catch (FormatException ex)
			{
				errorMessage = ex.Message;
				flag = false;
			}
			catch (ValueException ex2)
			{
				if (!(ex2.ReasonString == "DataSource.MissingClientLibrary"))
				{
					throw;
				}
				errorMessage = ex2.MessageString;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0008E190 File Offset: 0x0008C390
		public bool TryBuildConnectionString(Dictionary<string, string> keyValuePairs, out string connectionString)
		{
			bool flag;
			try
			{
				connectionString = OdbcConnectionStringHandler.Windows.GetString(keyValuePairs);
				flag = true;
			}
			catch (FormatException)
			{
				connectionString = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x0008E1C8 File Offset: 0x0008C3C8
		public bool TryParseConnectionString(string connectionString, out Dictionary<string, string> keyValuePairs)
		{
			bool flag;
			try
			{
				keyValuePairs = OdbcConnectionStringHandler.Windows.GetKeyValuePairs(connectionString);
				flag = true;
			}
			catch (FormatException)
			{
				keyValuePairs = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001496 RID: 5270
		public static readonly OdbcConnectionStringService Instance = new OdbcConnectionStringService();

		// Token: 0x04001497 RID: 5271
		public static readonly IConnectionStringServiceHandler Handler = new OdbcConnectionStringService.ServiceHandler();

		// Token: 0x020005D9 RID: 1497
		private class ServiceHandler : IConnectionStringServiceHandler
		{
			// Token: 0x06002EA0 RID: 11936 RVA: 0x0008E216 File Offset: 0x0008C416
			public bool TryGetConnectionStringService(string providerName, bool validateProvider, out IConnectionStringService service)
			{
				if (providerName == "Odbc")
				{
					service = OdbcConnectionStringService.Instance;
					return true;
				}
				service = null;
				return false;
			}
		}
	}
}
