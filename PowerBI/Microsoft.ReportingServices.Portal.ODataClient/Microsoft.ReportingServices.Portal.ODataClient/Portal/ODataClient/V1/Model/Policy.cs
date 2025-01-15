using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000DC RID: 220
	[OriginalName("Policy")]
	public class Policy : INotifyPropertyChanged
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00014154 File Offset: 0x00012354
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0001415C File Offset: 0x0001235C
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

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00014170 File Offset: 0x00012370
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00014178 File Offset: 0x00012378
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

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060009E8 RID: 2536 RVA: 0x0001418C File Offset: 0x0001238C
		// (remove) Token: 0x060009E9 RID: 2537 RVA: 0x000141C4 File Offset: 0x000123C4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060009EA RID: 2538 RVA: 0x000141F9 File Offset: 0x000123F9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000499 RID: 1177
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _GroupUserName;

		// Token: 0x0400049A RID: 1178
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Role> _Roles = new ObservableCollection<Role>();
	}
}
