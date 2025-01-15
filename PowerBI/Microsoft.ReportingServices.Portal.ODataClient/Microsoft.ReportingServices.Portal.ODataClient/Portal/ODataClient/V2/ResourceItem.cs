using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000080 RID: 128
	[OriginalName("ResourceItem")]
	public class ResourceItem : INotifyPropertyChanged
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0000B28C File Offset: 0x0000948C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ResourceItem CreateResourceItem(Guid ID)
		{
			return new ResourceItem
			{
				Id = ID
			};
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000B29A File Offset: 0x0000949A
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0000B2A2 File Offset: 0x000094A2
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000B2B6 File Offset: 0x000094B6
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0000B2BE File Offset: 0x000094BE
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

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000B2D2 File Offset: 0x000094D2
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0000B2DA File Offset: 0x000094DA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Path")]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				this._Path = value;
				this.OnPropertyChanged("Path");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000B2EE File Offset: 0x000094EE
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000B2F6 File Offset: 0x000094F6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Name")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.OnPropertyChanged("Name");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000B30A File Offset: 0x0000950A
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000B312 File Offset: 0x00009512
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Hash")]
		public string Hash
		{
			get
			{
				return this._Hash;
			}
			set
			{
				this._Hash = value;
				this.OnPropertyChanged("Hash");
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600059D RID: 1437 RVA: 0x0000B328 File Offset: 0x00009528
		// (remove) Token: 0x0600059E RID: 1438 RVA: 0x0000B360 File Offset: 0x00009560
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600059F RID: 1439 RVA: 0x0000B395 File Offset: 0x00009595
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000282 RID: 642
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Key;

		// Token: 0x04000283 RID: 643
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000284 RID: 644
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000285 RID: 645
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000286 RID: 646
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
