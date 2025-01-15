using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200000C RID: 12
	internal abstract class DirectQueryConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000260A File Offset: 0x0000080A
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002612 File Offset: 0x00000812
		public string DataSourceName { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000261B File Offset: 0x0000081B
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002623 File Offset: 0x00000823
		public string ManagedProvider { get; protected set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000262C File Offset: 0x0000082C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002634 File Offset: 0x00000834
		public string ResourceKind { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x0000263D File Offset: 0x0000083D
		protected DirectQueryConnectionStringBuilder()
			: base(false)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002648 File Offset: 0x00000848
		public virtual void AddDataSource(DataSourceReference dsr)
		{
			this["Data Source"] = this.GetDSRAddressField(dsr, "server");
			string dsraddressField = this.GetDSRAddressField(dsr, "database");
			if (dsraddressField != null)
			{
				this["Initial Catalog"] = dsraddressField;
			}
		}

		// Token: 0x0600001C RID: 28
		public abstract void AddCredential(Credential credential);

		// Token: 0x0600001D RID: 29
		protected abstract void AddOptions(Dictionary<string, object> options);

		// Token: 0x0600001E RID: 30
		protected abstract void InferProviderAndDriver();

		// Token: 0x0600001F RID: 31 RVA: 0x00002688 File Offset: 0x00000888
		public void Init(DataSourceReference dsr, string dataSourceName, Credential credential, Dictionary<string, object> options)
		{
			if (dsr == null || dsr.DataSource == null)
			{
				throw new ArgumentNullException("DirectQueryConnectionString cannot have empty DataSourceReference.");
			}
			if (string.IsNullOrEmpty(dataSourceName))
			{
				throw new ArgumentNullException("DirectQueryConnectionString cannot have empty DataSource.");
			}
			this.DataSourceName = dataSourceName;
			this.ResourceKind = dsr.DataSource.Kind;
			this.AddOptions(options);
			this.AddDataSource(dsr);
			this.AddCredential(credential);
			this.InferProviderAndDriver();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026F4 File Offset: 0x000008F4
		protected void AddUserNamePasswordCredentials(Credential credential)
		{
			object obj;
			credential.AuthenticationProperties.TryGetValue("Username", out obj);
			object obj2;
			credential.AuthenticationProperties.TryGetValue("Password", out obj2);
			base.Add("User ID", obj);
			base.Add("Password", obj2);
			base.Add("Persist Security Info", true);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002750 File Offset: 0x00000950
		protected void AddCredentialCommon(Credential credential)
		{
			if (credential.AuthenticationKind == "UsernamePassword")
			{
				this.AddUserNamePasswordCredentials(credential);
				return;
			}
			if (credential.ImpersonationMode != ImpersonationMode.Default)
			{
				base.Add("Integrated Security", "SSPI");
				base.Add("Persist Security Info", false);
				return;
			}
			throw EngineException.PFE_M_ENGINE_DQ_AUTHENTICATION_NOT_SUPPORTED(credential.AuthenticationKind, this.DataSourceName);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027B4 File Offset: 0x000009B4
		protected void AddEncryptionCommon(Credential credential)
		{
			object obj;
			if (credential.AuthenticationProperties.TryGetValue("EncryptConnection", out obj) && obj.ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				this["Encrypt"] = true;
				this["TrustServerCertificate"] = false;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000280A File Offset: 0x00000A0A
		protected virtual void AddEncryptionOptionForTrustedSqlServers(Credential credential)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000280C File Offset: 0x00000A0C
		protected string GetDSRAddressField(DataSourceReference dsr, string fieldName)
		{
			return (from ap in dsr.Address
				where ap.Name.ToLowerInvariant() == fieldName
				select ap.Value).FirstOrDefault<string>();
		}
	}
}
