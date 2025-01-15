using System;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010CA RID: 4298
	internal class GenericConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
	{
		// Token: 0x0600709B RID: 28827 RVA: 0x0018342C File Offset: 0x0018162C
		public GenericConnectionStringBuilder(IEngineHost host, string dataSourceName, ConnectionStringHandler connectionStringHandler, string sourceConnectionString, IResource resource, bool sqlCompatibleWindowsAuth)
			: base(host, resource, connectionStringHandler.NewBuilder())
		{
			this.sourceConnectionString = sourceConnectionString;
			this.connectionStringHandler = connectionStringHandler;
			this.dataSourceName = dataSourceName;
			this.sqlCompatibleWindowsAuth = sqlCompatibleWindowsAuth;
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x0018345C File Offset: 0x0018165C
		protected override Func<IDisposable> GetImpersonationWrapper(WindowsCredential credential)
		{
			if (!this.connectionStringHandler.SupportsImpersonation && credential != null && credential.OverrideCurrentUser)
			{
				string text = Strings.GenericProviders_WindowsAlternateAuthNotSupported(base.Resource.Kind);
				throw DataSourceException.NewInvalidCredentialsError(base.Host, base.Resource, text, text, null);
			}
			return base.GetImpersonationWrapper(credential);
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x0600709D RID: 28829 RVA: 0x001834B3 File Offset: 0x001816B3
		protected override string UserNameKey
		{
			get
			{
				return this.connectionStringHandler.UserNameKey;
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x0600709E RID: 28830 RVA: 0x001834C0 File Offset: 0x001816C0
		protected override string PasswordKey
		{
			get
			{
				return this.connectionStringHandler.PasswordKey;
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x0600709F RID: 28831 RVA: 0x000020FA File Offset: 0x000002FA
		protected override string ServerKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x060070A0 RID: 28832 RVA: 0x000020FA File Offset: 0x000002FA
		protected override string PortKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x060070A1 RID: 28833 RVA: 0x000020FA File Offset: 0x000002FA
		protected override string DatabaseKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x060070A2 RID: 28834 RVA: 0x001834CD File Offset: 0x001816CD
		protected override string IntegratedSecurityKey
		{
			get
			{
				return this.connectionStringHandler.IntegratedSecurityKey;
			}
		}

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x060070A3 RID: 28835 RVA: 0x000020FA File Offset: 0x000002FA
		protected override string EncryptKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x060070A4 RID: 28836 RVA: 0x001834DA File Offset: 0x001816DA
		protected override object AuthenticationTypeValue
		{
			get
			{
				return "yes";
			}
		}

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x060070A5 RID: 28837 RVA: 0x000020FA File Offset: 0x000002FA
		protected override string ConnectionTimeoutKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060070A6 RID: 28838 RVA: 0x001834E4 File Offset: 0x001816E4
		public override bool Apply(ResourceCredentialCollection credentials)
		{
			foreach (IResourceCredential resourceCredential in credentials)
			{
				if (!base.Apply(resourceCredential))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x00183538 File Offset: 0x00181738
		protected override void AddOptions()
		{
			ConnectionStringHandler.HandleFormatExceptions(this.dataSourceName, TextValue.New(this.sourceConnectionString), delegate
			{
				this.connectionStringHandler.ValidateSourceWithPermission(this.sourceConnectionString, base.Resource);
				DbConnectionStringBuilder dbConnectionStringBuilder = this.connectionStringHandler.NewBuilder(this.sourceConnectionString);
				this.SetConnectionStringProperties(dbConnectionStringBuilder);
			});
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x00002105 File Offset: 0x00000305
		protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
		{
			return false;
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x0018355C File Offset: 0x0018175C
		protected override bool Apply(BasicAuthCredential credential)
		{
			return this.Apply(new SqlAuthCredential(credential.Username, credential.Password));
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x00183578 File Offset: 0x00181778
		protected override bool Apply(ConnectionStringAdornment credential)
		{
			bool flag;
			try
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = this.connectionStringHandler.NewBuilder(credential.ConnectionString);
				this.connectionStringHandler.ValidateCredential(dbConnectionStringBuilder);
				this.SetConnectionStringProperties(dbConnectionStringBuilder);
				flag = true;
			}
			catch (FormatException ex)
			{
				string text = Strings.GenericProviders_InvalidConnectionString(ex.Message);
				throw DataSourceException.NewInvalidCredentialsError(base.Host, base.Resource, text, text, ex);
			}
			return flag;
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x00002105 File Offset: 0x00000305
		protected override bool Apply(SharedKeyAuthCredential credential)
		{
			return false;
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x001835E8 File Offset: 0x001817E8
		protected override bool ApplyWindowsCredential(WindowsCredential credential)
		{
			return !this.sqlCompatibleWindowsAuth || base.ApplyWindowsCredential(credential);
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x001835FC File Offset: 0x001817FC
		private void SetConnectionStringProperties(DbConnectionStringBuilder builder)
		{
			foreach (object obj in builder.Keys)
			{
				string text = (string)obj;
				this.builder[text] = builder[text];
			}
		}

		// Token: 0x04003E1E RID: 15902
		private readonly string dataSourceName;

		// Token: 0x04003E1F RID: 15903
		private readonly ConnectionStringHandler connectionStringHandler;

		// Token: 0x04003E20 RID: 15904
		private readonly string sourceConnectionString;

		// Token: 0x04003E21 RID: 15905
		private readonly bool sqlCompatibleWindowsAuth;
	}
}
