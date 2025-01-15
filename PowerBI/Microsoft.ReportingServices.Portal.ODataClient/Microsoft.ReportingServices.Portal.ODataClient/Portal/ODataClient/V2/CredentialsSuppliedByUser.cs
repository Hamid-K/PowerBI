using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200006E RID: 110
	[OriginalName("CredentialsSuppliedByUser")]
	public class CredentialsSuppliedByUser : INotifyPropertyChanged
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000A275 File Offset: 0x00008475
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CredentialsSuppliedByUser CreateCredentialsSuppliedByUser(bool useAsWindowsCredentials)
		{
			return new CredentialsSuppliedByUser
			{
				UseAsWindowsCredentials = useAsWindowsCredentials
			};
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000A283 File Offset: 0x00008483
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0000A28B File Offset: 0x0000848B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DisplayText")]
		public string DisplayText
		{
			get
			{
				return this._DisplayText;
			}
			set
			{
				this._DisplayText = value;
				this.OnPropertyChanged("DisplayText");
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000A29F File Offset: 0x0000849F
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000A2A7 File Offset: 0x000084A7
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

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060004E9 RID: 1257 RVA: 0x0000A2BC File Offset: 0x000084BC
		// (remove) Token: 0x060004EA RID: 1258 RVA: 0x0000A2F4 File Offset: 0x000084F4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004EB RID: 1259 RVA: 0x0000A329 File Offset: 0x00008529
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400023F RID: 575
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DisplayText;

		// Token: 0x04000240 RID: 576
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UseAsWindowsCredentials;
	}
}
