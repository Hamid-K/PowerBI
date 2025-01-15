using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000EC RID: 236
	[OriginalName("CredentialsSuppliedByUser")]
	public class CredentialsSuppliedByUser : INotifyPropertyChanged
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x00014F45 File Offset: 0x00013145
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CredentialsSuppliedByUser CreateCredentialsSuppliedByUser(bool useAsWindowsCredentials)
		{
			return new CredentialsSuppliedByUser
			{
				UseAsWindowsCredentials = useAsWindowsCredentials
			};
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00014F53 File Offset: 0x00013153
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00014F5B File Offset: 0x0001315B
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

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00014F6F File Offset: 0x0001316F
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x00014F77 File Offset: 0x00013177
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

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06000A95 RID: 2709 RVA: 0x00014F8C File Offset: 0x0001318C
		// (remove) Token: 0x06000A96 RID: 2710 RVA: 0x00014FC4 File Offset: 0x000131C4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A97 RID: 2711 RVA: 0x00014FF9 File Offset: 0x000131F9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004D9 RID: 1241
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DisplayText;

		// Token: 0x040004DA RID: 1242
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UseAsWindowsCredentials;
	}
}
