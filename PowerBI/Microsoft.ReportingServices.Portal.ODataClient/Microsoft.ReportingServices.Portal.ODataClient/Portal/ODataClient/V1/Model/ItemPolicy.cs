using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000BA RID: 186
	[OriginalName("ItemPolicy")]
	public class ItemPolicy : INotifyPropertyChanged
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x0001014D File Offset: 0x0000E34D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ItemPolicy CreateItemPolicy(Guid ID, bool inheritParentPolicy)
		{
			return new ItemPolicy
			{
				Id = ID,
				InheritParentPolicy = inheritParentPolicy
			};
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00010162 File Offset: 0x0000E362
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0001016A File Offset: 0x0000E36A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001017E File Offset: 0x0000E37E
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00010186 File Offset: 0x0000E386
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("InheritParentPolicy")]
		public bool InheritParentPolicy
		{
			get
			{
				return this._InheritParentPolicy;
			}
			set
			{
				this._InheritParentPolicy = value;
				this.OnPropertyChanged("InheritParentPolicy");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001019A File Offset: 0x0000E39A
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000101A2 File Offset: 0x0000E3A2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public ObservableCollection<Policy> Policies
		{
			get
			{
				return this._Policies;
			}
			set
			{
				this._Policies = value;
				this.OnPropertyChanged("Policies");
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060007F8 RID: 2040 RVA: 0x000101B8 File Offset: 0x0000E3B8
		// (remove) Token: 0x060007F9 RID: 2041 RVA: 0x000101F0 File Offset: 0x0000E3F0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060007FA RID: 2042 RVA: 0x00010225 File Offset: 0x0000E425
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040003D1 RID: 977
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040003D2 RID: 978
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _InheritParentPolicy;

		// Token: 0x040003D3 RID: 979
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Policy> _Policies = new ObservableCollection<Policy>();
	}
}
