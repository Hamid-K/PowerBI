using System;
using System.Data.Common;
using System.Globalization;
using System.Security;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportingServicesHost.Utils;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x0200000A RID: 10
	internal sealed class ASConnectionInfoForRS : IASConnectionInfo
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000028A1 File Offset: 0x00000AA1
		private string ServerName
		{
			get
			{
				return this._rsDataSourceConnection.ServerName ?? "localhost";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000028B7 File Offset: 0x00000AB7
		private int PortNumber
		{
			get
			{
				return this._rsDataSourceConnection.PortNumber;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000028C4 File Offset: 0x00000AC4
		private string EffectiveUserName
		{
			get
			{
				return this._rsDataSourceConnection.EffectiveUserName;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000028D1 File Offset: 0x00000AD1
		private string CustomData
		{
			get
			{
				return this._rsDataSourceConnection.CustomData;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000028DE File Offset: 0x00000ADE
		private string Roles
		{
			get
			{
				return this._rsDataSourceConnection.Roles;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000028EB File Offset: 0x00000AEB
		private string ActAsUser
		{
			get
			{
				return this._rsDataSourceConnection.ActAsUser;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028F8 File Offset: 0x00000AF8
		private RSDataSourceConnection.ConnectionCredential Credential
		{
			get
			{
				return this._rsDataSourceConnection.Credential;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002905 File Offset: 0x00000B05
		public string CubeName
		{
			get
			{
				return this._rsDataSourceConnection.CubeName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002912 File Offset: 0x00000B12
		public string DatabaseName
		{
			get
			{
				return this._rsDataSourceConnection.DataBaseName;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000291F File Offset: 0x00000B1F
		public string DatabaseID
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002922 File Offset: 0x00000B22
		public ModelLocation ModelLocation
		{
			get
			{
				if (!this.IsExternalConnection())
				{
					return ModelLocation.Internal;
				}
				return ModelLocation.ExternalOnPrem;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000292F File Offset: 0x00000B2F
		public bool IgnoreTranslations
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002932 File Offset: 0x00000B32
		public string ModelMetadataCatalogName
		{
			get
			{
				return this._rsDataSourceConnection.DataBaseName;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000293F File Offset: 0x00000B3F
		public ASConnectionInfoForRS(RSDataSourceConnection rsDataSourceConnection)
		{
			this._rsDataSourceConnection = rsDataSourceConnection;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000294E File Offset: 0x00000B4E
		public string GetConnectionString()
		{
			return this.BuildConnectionString();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002956 File Offset: 0x00000B56
		public SecureString GetSecureConnectionString()
		{
			return Encryption.ConvertToSecureString(this.BuildConnectionString());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002963 File Offset: 0x00000B63
		private bool IsExternalConnection()
		{
			return this.PortNumber == 0;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002970 File Offset: 0x00000B70
		private string BuildConnectionString()
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.Add("data source", string.Format(CultureInfo.InvariantCulture, this.IsExternalConnection() ? this.ServerName : "{0}:{1}", this.ServerName, this.PortNumber));
			dbConnectionStringBuilder.Add("initial catalog", this.DatabaseName);
			if (this.CubeName != null)
			{
				dbConnectionStringBuilder.Add("cube", this.CubeName);
			}
			if (!string.IsNullOrEmpty(this.EffectiveUserName))
			{
				dbConnectionStringBuilder.Add("EffectiveUserName", this.EffectiveUserName);
			}
			if (!string.IsNullOrEmpty(this.CustomData))
			{
				dbConnectionStringBuilder.Add("CustomData", this.CustomData);
			}
			if (!string.IsNullOrEmpty(this.Roles))
			{
				dbConnectionStringBuilder.Add("Roles", this.Roles);
			}
			if (this.IsAzureConnection())
			{
				this.AddCredentialToConnectionString(dbConnectionStringBuilder, this.Credential);
			}
			else if (!string.IsNullOrEmpty(this.ActAsUser))
			{
				dbConnectionStringBuilder["Restrict Catalog"] = "true";
				dbConnectionStringBuilder["Authentication Scheme"] = "ActAs";
				dbConnectionStringBuilder["Bypass authorization"] = "true";
				dbConnectionStringBuilder["User ID"] = this.ActAsUser;
			}
			return dbConnectionStringBuilder.ToString();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AB0 File Offset: 0x00000CB0
		private void AddCredentialToConnectionString(DbConnectionStringBuilder connectionStringBuilder, RSDataSourceConnection.ConnectionCredential credential)
		{
			if (credential != null)
			{
				if (credential.IsIntegratedCredential || credential.UseAsWindowsCredentials)
				{
					connectionStringBuilder.Add("UseADALCache", 2);
					return;
				}
				if (!string.IsNullOrEmpty(credential.UserName))
				{
					connectionStringBuilder.Add("User ID", credential.UserName);
				}
				if (credential.Password != null)
				{
					connectionStringBuilder.Add("Password", SecureStringWrapper.GetDecryptedString(credential.Password));
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B1E File Offset: 0x00000D1E
		public bool IsAzureConnection()
		{
			return this.ServerName.StartsWith("asazure://", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000039 RID: 57
		private const string LocalServer = "localhost";

		// Token: 0x0400003A RID: 58
		private const string ASAzurePrefix = "asazure://";

		// Token: 0x0400003B RID: 59
		private readonly RSDataSourceConnection _rsDataSourceConnection;
	}
}
