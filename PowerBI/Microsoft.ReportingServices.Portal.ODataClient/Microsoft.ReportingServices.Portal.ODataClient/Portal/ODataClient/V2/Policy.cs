using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200006C RID: 108
	[OriginalName("Policy")]
	public class Policy : INotifyPropertyChanged
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000A0DD File Offset: 0x000082DD
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000A0E5 File Offset: 0x000082E5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("GroupUserName")]
		public string GroupUserName
		{
			get
			{
				return this._GroupUserName;
			}
			set
			{
				this._GroupUserName = value;
				this.OnPropertyChanged("GroupUserName");
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000A0F9 File Offset: 0x000082F9
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0000A101 File Offset: 0x00008301
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Roles")]
		public ObservableCollection<Role> Roles
		{
			get
			{
				return this._Roles;
			}
			set
			{
				this._Roles = value;
				this.OnPropertyChanged("Roles");
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060004D8 RID: 1240 RVA: 0x0000A118 File Offset: 0x00008318
		// (remove) Token: 0x060004D9 RID: 1241 RVA: 0x0000A150 File Offset: 0x00008350
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004DA RID: 1242 RVA: 0x0000A185 File Offset: 0x00008385
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000239 RID: 569
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _GroupUserName;

		// Token: 0x0400023A RID: 570
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Role> _Roles = new ObservableCollection<Role>();
	}
}
