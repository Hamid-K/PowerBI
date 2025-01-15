using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200001F RID: 31
	[Key("Id")]
	[OriginalName("ItemPolicy")]
	public class ItemPolicy : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00003C53 File Offset: 0x00001E53
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ItemPolicy CreateItemPolicy(Guid ID, bool inheritParentPolicy)
		{
			return new ItemPolicy
			{
				Id = ID,
				InheritParentPolicy = inheritParentPolicy
			};
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003C68 File Offset: 0x00001E68
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00003C70 File Offset: 0x00001E70
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00003C84 File Offset: 0x00001E84
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00003C8C File Offset: 0x00001E8C
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00003CA0 File Offset: 0x00001EA0
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00003CA8 File Offset: 0x00001EA8
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

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000140 RID: 320 RVA: 0x00003CBC File Offset: 0x00001EBC
		// (remove) Token: 0x06000141 RID: 321 RVA: 0x00003CF4 File Offset: 0x00001EF4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000142 RID: 322 RVA: 0x00003D29 File Offset: 0x00001F29
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000B2 RID: 178
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040000B3 RID: 179
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _InheritParentPolicy;

		// Token: 0x040000B4 RID: 180
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Policy> _Policies = new ObservableCollection<Policy>();
	}
}
