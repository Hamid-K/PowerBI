using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000010 RID: 16
	internal sealed class RelationalSAPHANADirectQueryConnectionStringBuilder : DirectQueryConnectionStringBuilder
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002D10 File Offset: 0x00000F10
		public override void AddCredential(Credential credential)
		{
			if (credential == null)
			{
				return;
			}
			if (!(credential.AuthenticationKind == "UsernamePassword"))
			{
				throw EngineException.PFE_M_ENGINE_DQ_AUTHENTICATION_NOT_SUPPORTED(credential.AuthenticationKind, base.DataSourceName);
			}
			object obj;
			credential.AuthenticationProperties.TryGetValue("Username", out obj);
			object obj2;
			credential.AuthenticationProperties.TryGetValue("Password", out obj2);
			base.Add("UID", obj);
			base.Add("PWD", obj2);
			object obj3;
			if (credential.AuthenticationProperties.TryGetValue("EncryptConnection", out obj3) && obj3.ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				throw EngineException.PFE_M_ENGINE_DQ_ENCRYPTION_NOT_SUPPORTED(base.DataSourceName);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public override void AddDataSource(DataSourceReference dsr)
		{
			if (dsr.Address.Count<AddressPart>() > 2)
			{
				throw EngineException.PFE_M_ENGINE_DQ_INVALID_DATA_SOURCE_FORMAT(base.DataSourceName);
			}
			string text = base.GetDSRAddressField(dsr, "server");
			if (!text.Contains(":"))
			{
				text += ":30015";
			}
			this["ServerNode"] = text;
			string dsraddressField = base.GetDSRAddressField(dsr, "database");
			if (dsraddressField != null)
			{
				this["Database"] = dsraddressField;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E2D File Offset: 0x0000102D
		protected override void AddOptions(Dictionary<string, object> options)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E2F File Offset: 0x0000102F
		protected override void InferProviderAndDriver()
		{
			this["Provider"] = "MSDASQL.1";
			this["Driver"] = (Environment.Is64BitProcess ? "HDBODBC" : "HDBODBC32");
		}
	}
}
