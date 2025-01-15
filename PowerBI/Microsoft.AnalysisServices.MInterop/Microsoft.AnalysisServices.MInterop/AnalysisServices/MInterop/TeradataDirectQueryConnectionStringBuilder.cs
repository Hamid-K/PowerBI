using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200000F RID: 15
	internal sealed class TeradataDirectQueryConnectionStringBuilder : DirectQueryConnectionStringBuilder
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public override void AddCredential(Credential credential)
		{
			if (credential == null)
			{
				return;
			}
			base.AddCredentialCommon(credential);
			object obj;
			if (credential.AuthenticationProperties.TryGetValue("EncryptConnection", out obj) && obj.ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				this["Data Encryption"] = true;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public override void AddDataSource(DataSourceReference dsr)
		{
			if (dsr.Address.Count<AddressPart>() > 2)
			{
				throw EngineException.PFE_M_ENGINE_DQ_INVALID_DATA_SOURCE_FORMAT(base.DataSourceName);
			}
			string dsraddressField = base.GetDSRAddressField(dsr, "server");
			this.SetPathAndPort(dsraddressField, "Data Source", "Port Number", null);
			string dsraddressField2 = base.GetDSRAddressField(dsr, "database");
			if (dsraddressField2 != null)
			{
				this["Database"] = dsraddressField2;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C5E File Offset: 0x00000E5E
		protected override void AddOptions(Dictionary<string, object> options)
		{
			this["UseXViews"] = true;
			this["SESSIONCHARACTERSET"] = "UTF16";
			this["Connection Timeout"] = 60;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C93 File Offset: 0x00000E93
		protected override void InferProviderAndDriver()
		{
			base.ManagedProvider = "Teradata.Client.Provider";
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002CA0 File Offset: 0x00000EA0
		private void SetPathAndPort(string fullPath, string serverKey, string portKey, int? defaultPort)
		{
			if (fullPath.Contains(":"))
			{
				string[] array = fullPath.Split(new char[] { ':' });
				this[serverKey] = array[0];
				this[portKey] = array[1];
				return;
			}
			this[serverKey] = fullPath;
			if (defaultPort != null)
			{
				this[portKey] = defaultPort.Value;
			}
		}
	}
}
