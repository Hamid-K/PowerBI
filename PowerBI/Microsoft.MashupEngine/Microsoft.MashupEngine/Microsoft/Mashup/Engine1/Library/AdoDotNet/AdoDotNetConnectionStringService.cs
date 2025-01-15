using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.AdoDotNet
{
	// Token: 0x02000F3F RID: 3903
	internal sealed class AdoDotNetConnectionStringService : IConnectionStringService
	{
		// Token: 0x06006732 RID: 26418 RVA: 0x00163336 File Offset: 0x00161536
		private AdoDotNetConnectionStringService(DbProviderFactory factory)
		{
			this.factory = factory;
		}

		// Token: 0x06006733 RID: 26419 RVA: 0x00163345 File Offset: 0x00161545
		private AdoDotNetConnectionStringService(string providerName)
		{
			this.providerName = providerName;
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x00163354 File Offset: 0x00161554
		public bool ValidateSourceConnectionString(string connectionString, out string errorMessage)
		{
			bool flag;
			try
			{
				AdoDotNetEnvironment.ConnectionString.ValidateSource(connectionString);
				AdoDotNetEnvironment.ConnectionString.ValidateSourceWithPermission(connectionString, null);
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

		// Token: 0x06006735 RID: 26421 RVA: 0x001633A0 File Offset: 0x001615A0
		public bool TryBuildConnectionString(Dictionary<string, string> keyValuePairs, out string connectionString)
		{
			bool flag;
			try
			{
				connectionString = AdoDotNetEnvironment.ConnectionString.GetString(keyValuePairs);
				flag = true;
			}
			catch (FormatException)
			{
				connectionString = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x001633D8 File Offset: 0x001615D8
		public bool TryParseConnectionString(string connectionString, out Dictionary<string, string> keyValuePairs)
		{
			bool flag;
			try
			{
				keyValuePairs = AdoDotNetEnvironment.ConnectionString.GetKeyValuePairs(connectionString);
				flag = true;
			}
			catch (FormatException)
			{
				keyValuePairs = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x040038BC RID: 14524
		public static readonly IConnectionStringService Instance = new AdoDotNetConnectionStringService(string.Empty);

		// Token: 0x040038BD RID: 14525
		public static readonly IConnectionStringServiceHandler Handler = new AdoDotNetConnectionStringService.ServiceHandler();

		// Token: 0x040038BE RID: 14526
		private readonly string providerName;

		// Token: 0x040038BF RID: 14527
		private readonly DbProviderFactory factory;

		// Token: 0x02000F40 RID: 3904
		private class ServiceHandler : IConnectionStringServiceHandler
		{
			// Token: 0x06006738 RID: 26424 RVA: 0x0016342C File Offset: 0x0016162C
			public bool TryGetConnectionStringService(string providerName, bool validateProvider, out IConnectionStringService service)
			{
				if (providerName.StartsWith(AdoDotNetConnectionStringService.ServiceHandler.prefix, StringComparison.Ordinal))
				{
					DbProviderFactory dbProviderFactory = null;
					try
					{
						dbProviderFactory = DbProviderFactories.GetFactory(providerName.Substring(AdoDotNetConnectionStringService.ServiceHandler.prefix.Length));
					}
					catch (ArgumentException)
					{
					}
					catch (ConfigurationErrorsException)
					{
					}
					catch (TargetInvocationException)
					{
					}
					if (dbProviderFactory != null)
					{
						service = new AdoDotNetConnectionStringService(dbProviderFactory);
						return true;
					}
				}
				if (!validateProvider)
				{
					service = new AdoDotNetConnectionStringService(providerName);
					return true;
				}
				service = null;
				return false;
			}

			// Token: 0x040038C0 RID: 14528
			private static readonly string prefix = AdoDotNetResourceKindInfo.Instance.Kind + "/";
		}
	}
}
