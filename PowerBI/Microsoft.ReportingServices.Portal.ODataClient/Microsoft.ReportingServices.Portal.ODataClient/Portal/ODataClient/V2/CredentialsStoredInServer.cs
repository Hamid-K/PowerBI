using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200006F RID: 111
	[OriginalName("CredentialsStoredInServer")]
	public class CredentialsStoredInServer : INotifyPropertyChanged
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000A345 File Offset: 0x00008545
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CredentialsStoredInServer CreateCredentialsStoredInServer(bool useAsWindowsCredentials, bool impersonateAuthenticatedUser)
		{
			return new CredentialsStoredInServer
			{
				UseAsWindowsCredentials = useAsWindowsCredentials,
				ImpersonateAuthenticatedUser = impersonateAuthenticatedUser
			};
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000A35A File Offset: 0x0000855A
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0000A362 File Offset: 0x00008562
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000A376 File Offset: 0x00008576
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0000A37E File Offset: 0x0000857E
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

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000A392 File Offset: 0x00008592
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000A39A File Offset: 0x0000859A
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

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000A3AE File Offset: 0x000085AE
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000A3B6 File Offset: 0x000085B6
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

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060004F6 RID: 1270 RVA: 0x0000A3CC File Offset: 0x000085CC
		// (remove) Token: 0x060004F7 RID: 1271 RVA: 0x0000A404 File Offset: 0x00008604
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000A439 File Offset: 0x00008639
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000242 RID: 578
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _UserName;

		// Token: 0x04000243 RID: 579
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Password;

		// Token: 0x04000244 RID: 580
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UseAsWindowsCredentials;

		// Token: 0x04000245 RID: 581
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ImpersonateAuthenticatedUser;
	}
}
