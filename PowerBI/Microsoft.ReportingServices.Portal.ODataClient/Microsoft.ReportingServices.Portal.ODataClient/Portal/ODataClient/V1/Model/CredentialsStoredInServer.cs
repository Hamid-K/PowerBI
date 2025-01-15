using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000ED RID: 237
	[OriginalName("CredentialsStoredInServer")]
	public class CredentialsStoredInServer : INotifyPropertyChanged
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x00015015 File Offset: 0x00013215
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CredentialsStoredInServer CreateCredentialsStoredInServer(bool useAsWindowsCredentials, bool impersonateAuthenticatedUser)
		{
			return new CredentialsStoredInServer
			{
				UseAsWindowsCredentials = useAsWindowsCredentials,
				ImpersonateAuthenticatedUser = impersonateAuthenticatedUser
			};
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0001502A File Offset: 0x0001322A
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00015032 File Offset: 0x00013232
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UserName")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
				this.OnPropertyChanged("UserName");
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00015046 File Offset: 0x00013246
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0001504E File Offset: 0x0001324E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Password")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				this.OnPropertyChanged("Password");
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00015062 File Offset: 0x00013262
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0001506A File Offset: 0x0001326A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UseAsWindowsCredentials")]
		public bool UseAsWindowsCredentials
		{
			get
			{
				return this._UseAsWindowsCredentials;
			}
			set
			{
				this._UseAsWindowsCredentials = value;
				this.OnPropertyChanged("UseAsWindowsCredentials");
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001507E File Offset: 0x0001327E
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00015086 File Offset: 0x00013286
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ImpersonateAuthenticatedUser")]
		public bool ImpersonateAuthenticatedUser
		{
			get
			{
				return this._ImpersonateAuthenticatedUser;
			}
			set
			{
				this._ImpersonateAuthenticatedUser = value;
				this.OnPropertyChanged("ImpersonateAuthenticatedUser");
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06000AA2 RID: 2722 RVA: 0x0001509C File Offset: 0x0001329C
		// (remove) Token: 0x06000AA3 RID: 2723 RVA: 0x000150D4 File Offset: 0x000132D4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00015109 File Offset: 0x00013309
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004DC RID: 1244
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _UserName;

		// Token: 0x040004DD RID: 1245
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Password;

		// Token: 0x040004DE RID: 1246
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UseAsWindowsCredentials;

		// Token: 0x040004DF RID: 1247
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ImpersonateAuthenticatedUser;
	}
}
