using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E0 RID: 224
	[Key("Id")]
	[EntitySet("SystemResourceItems")]
	[OriginalName("SystemResourceItem")]
	public class SystemResourceItem : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000A0A RID: 2570 RVA: 0x000144BB File Offset: 0x000126BB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static SystemResourceItem CreateSystemResourceItem(Guid ID)
		{
			return new SystemResourceItem
			{
				Id = ID
			};
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000144C9 File Offset: 0x000126C9
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x000144D1 File Offset: 0x000126D1
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

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000144E5 File Offset: 0x000126E5
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x000144ED File Offset: 0x000126ED
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Key")]
		public string Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				this._Key = value;
				this.OnPropertyChanged("Key");
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00014501 File Offset: 0x00012701
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00014509 File Offset: 0x00012709
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ItemContent")]
		public CatalogItem ItemContent
		{
			get
			{
				return this._ItemContent;
			}
			set
			{
				this._ItemContent = value;
				this.OnPropertyChanged("ItemContent");
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06000A11 RID: 2577 RVA: 0x00014520 File Offset: 0x00012720
		// (remove) Token: 0x06000A12 RID: 2578 RVA: 0x00014558 File Offset: 0x00012758
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001458D File Offset: 0x0001278D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004A8 RID: 1192
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040004A9 RID: 1193
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Key;

		// Token: 0x040004AA RID: 1194
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItem _ItemContent;
	}
}
