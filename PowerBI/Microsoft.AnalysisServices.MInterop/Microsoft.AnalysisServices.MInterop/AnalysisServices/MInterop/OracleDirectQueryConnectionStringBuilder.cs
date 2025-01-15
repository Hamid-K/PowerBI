using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200000E RID: 14
	internal sealed class OracleDirectQueryConnectionStringBuilder : DirectQueryConnectionStringBuilder
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002A8C File Offset: 0x00000C8C
		public override void AddCredential(Credential credential)
		{
			if (credential == null)
			{
				return;
			}
			if (credential.AuthenticationKind == "UsernamePassword")
			{
				base.AddUserNamePasswordCredentials(credential);
			}
			else
			{
				if (credential.ImpersonationMode == ImpersonationMode.Default)
				{
					throw EngineException.PFE_M_ENGINE_DQ_AUTHENTICATION_NOT_SUPPORTED(credential.AuthenticationKind, base.DataSourceName);
				}
				base.Add("User ID", "/");
			}
			object obj;
			if (credential.AuthenticationProperties.TryGetValue("EncryptConnection", out obj) && obj.ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				throw EngineException.PFE_M_ENGINE_DQ_ENCRYPTION_NOT_SUPPORTED(base.DataSourceName);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B18 File Offset: 0x00000D18
		public override void AddDataSource(DataSourceReference dsr)
		{
			if (dsr.Address.Count<AddressPart>() > 2)
			{
				throw EngineException.PFE_M_ENGINE_DQ_INVALID_DATA_SOURCE_FORMAT(base.DataSourceName);
			}
			this["Data Source"] = base.GetDSRAddressField(dsr, "server");
			string dsraddressField = base.GetDSRAddressField(dsr, "database");
			if (dsraddressField != null)
			{
				this["Database"] = dsraddressField;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B72 File Offset: 0x00000D72
		protected override void AddOptions(Dictionary<string, object> options)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B74 File Offset: 0x00000D74
		protected override void InferProviderAndDriver()
		{
			if (MInteropHelperImpl.UseManagedOracleProvider)
			{
				base.ManagedProvider = "Oracle.DataAccess.Client";
				return;
			}
			this["Provider"] = "OraOLEDB.Oracle";
		}
	}
}
