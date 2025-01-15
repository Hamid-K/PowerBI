using System;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001134 RID: 4404
	internal abstract class ConnectionStringResourceCredentialDispatcher : SqlResourceCredentialDispatcher
	{
		// Token: 0x06007343 RID: 29507 RVA: 0x0018CF7A File Offset: 0x0018B17A
		protected ConnectionStringResourceCredentialDispatcher(IEngineHost host, IResource resource)
			: this(host, resource, new DbConnectionStringBuilder())
		{
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x0018CF89 File Offset: 0x0018B189
		protected ConnectionStringResourceCredentialDispatcher(IEngineHost host, IResource resource, DbConnectionStringBuilder builder)
			: base(host, resource)
		{
			this.builder = builder;
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x06007345 RID: 29509
		protected abstract string UserNameKey { get; }

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x06007346 RID: 29510
		protected abstract string PasswordKey { get; }

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x06007347 RID: 29511
		protected abstract string ServerKey { get; }

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x06007348 RID: 29512
		protected abstract string PortKey { get; }

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x06007349 RID: 29513
		protected abstract string DatabaseKey { get; }

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x0600734A RID: 29514
		protected abstract string IntegratedSecurityKey { get; }

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x0600734B RID: 29515
		protected abstract string EncryptKey { get; }

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x0600734C RID: 29516
		protected abstract object AuthenticationTypeValue { get; }

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x0600734D RID: 29517 RVA: 0x000020FA File Offset: 0x000002FA
		protected virtual string PortSeparator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x0600734E RID: 29518
		protected abstract string ConnectionTimeoutKey { get; }

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x0600734F RID: 29519 RVA: 0x0018CF9C File Offset: 0x0018B19C
		protected virtual int? DefaultConnectionTimeout
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007350 RID: 29520 RVA: 0x0018CFB2 File Offset: 0x0018B1B2
		protected override bool Apply(SqlAuthCredential credential)
		{
			this.builder[this.UserNameKey] = credential.Username;
			this.builder[this.PasswordKey] = credential.Password;
			return true;
		}

		// Token: 0x06007351 RID: 29521 RVA: 0x0018CFE3 File Offset: 0x0018B1E3
		protected override bool ApplyWindowsCredential(WindowsCredential credential)
		{
			this.builder[this.IntegratedSecurityKey] = this.AuthenticationTypeValue;
			return true;
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void AddOptions()
		{
		}

		// Token: 0x06007353 RID: 29523 RVA: 0x0018D000 File Offset: 0x0018B200
		public ConnectionInfo ConstructConnectionString(string server, string database, ResourceCredentialCollection credentials, int? connectionTimeout)
		{
			this.builder.Clear();
			if (connectionTimeout != null)
			{
				this.builder[this.ConnectionTimeoutKey] = connectionTimeout.Value;
			}
			else if (this.DefaultConnectionTimeout != null)
			{
				this.builder[this.ConnectionTimeoutKey] = this.DefaultConnectionTimeout.Value;
			}
			this.AddOptions();
			if (server != null)
			{
				this.builder[this.ServerKey] = server;
				string[] array = server.Split(new char[] { ':' });
				int num = 0;
				if (array.Length == 2 && int.TryParse(array[1], out num))
				{
					if (this.PortKey != null)
					{
						this.builder[this.ServerKey] = array[0];
						this.builder[this.PortKey] = array[1];
					}
					else if (this.PortSeparator != null)
					{
						this.builder[this.ServerKey] = array[0] + this.PortSeparator + array[1];
					}
				}
			}
			if (database != null)
			{
				this.builder[this.DatabaseKey] = database;
			}
			if (!this.Apply(credentials))
			{
				throw DataSourceException.NewInvalidCredentialsError(base.Host, base.Resource, null, null, null);
			}
			return new ConnectionInfo(this.builder.ConnectionString, base.AlternateIdentity, base.Impersonate, base.RequireEncryption);
		}

		// Token: 0x04003F8A RID: 16266
		protected readonly DbConnectionStringBuilder builder;
	}
}
