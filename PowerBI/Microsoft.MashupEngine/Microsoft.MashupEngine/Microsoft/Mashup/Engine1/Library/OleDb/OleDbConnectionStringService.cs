using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x02000570 RID: 1392
	internal sealed class OleDbConnectionStringService : IConnectionStringService
	{
		// Token: 0x06002C4F RID: 11343 RVA: 0x000020FD File Offset: 0x000002FD
		private OleDbConnectionStringService()
		{
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0008704C File Offset: 0x0008524C
		public bool ValidateSourceConnectionString(string connectionString, out string errorMessage)
		{
			bool flag;
			try
			{
				OleDbEnvironment.ConnectionString.ValidateSource(connectionString);
				OleDbEnvironment.ConnectionString.ValidateSourceWithPermission(connectionString, null);
				errorMessage = null;
				flag = true;
			}
			catch (FormatException ex)
			{
				errorMessage = ex.Message;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x00087098 File Offset: 0x00085298
		public bool TryBuildConnectionString(Dictionary<string, string> keyValuePairs, out string connectionString)
		{
			bool flag;
			try
			{
				connectionString = OleDbEnvironment.ConnectionString.GetString(keyValuePairs);
				flag = true;
			}
			catch (FormatException)
			{
				connectionString = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000870D0 File Offset: 0x000852D0
		public bool TryParseConnectionString(string connectionString, out Dictionary<string, string> keyValuePairs)
		{
			bool flag;
			try
			{
				keyValuePairs = OleDbEnvironment.ConnectionString.GetKeyValuePairs(connectionString);
				flag = true;
			}
			catch (FormatException)
			{
				keyValuePairs = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001346 RID: 4934
		public static readonly OleDbConnectionStringService Instance = new OleDbConnectionStringService();

		// Token: 0x04001347 RID: 4935
		public static readonly IConnectionStringServiceHandler Handler = new OleDbConnectionStringService.ServiceHandler();

		// Token: 0x02000571 RID: 1393
		private class ServiceHandler : IConnectionStringServiceHandler
		{
			// Token: 0x06002C54 RID: 11348 RVA: 0x0008711E File Offset: 0x0008531E
			public bool TryGetConnectionStringService(string providerName, bool validateProvider, out IConnectionStringService service)
			{
				if (providerName == "OleDb")
				{
					service = OleDbConnectionStringService.Instance;
					return true;
				}
				service = null;
				return false;
			}
		}
	}
}
