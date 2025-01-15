using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200005C RID: 92
	[Key("Id")]
	[EntitySet("SystemResourceItems")]
	[OriginalName("SystemResourceItem")]
	public class SystemResourceItem : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00009253 File Offset: 0x00007453
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static SystemResourceItem CreateSystemResourceItem(Guid ID)
		{
			return new SystemResourceItem
			{
				Id = ID
			};
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00009261 File Offset: 0x00007461
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00009269 File Offset: 0x00007469
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

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000927D File Offset: 0x0000747D
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00009285 File Offset: 0x00007485
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

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00009299 File Offset: 0x00007499
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x000092A1 File Offset: 0x000074A1
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

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600042D RID: 1069 RVA: 0x000092B8 File Offset: 0x000074B8
		// (remove) Token: 0x0600042E RID: 1070 RVA: 0x000092F0 File Offset: 0x000074F0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600042F RID: 1071 RVA: 0x00009325 File Offset: 0x00007525
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001FB RID: 507
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040001FC RID: 508
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Key;

		// Token: 0x040001FD RID: 509
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItem _ItemContent;
	}
}
