using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000082 RID: 130
	[OriginalName("ThumbnailItem")]
	public class ThumbnailItem : INotifyPropertyChanged
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0000B539 File Offset: 0x00009739
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ThumbnailItem CreateThumbnailItem(MobileReportThumbnailType type, Guid ID)
		{
			return new ThumbnailItem
			{
				Type = type,
				Id = ID
			};
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000B54E File Offset: 0x0000974E
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000B556 File Offset: 0x00009756
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public MobileReportThumbnailType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.OnPropertyChanged("Type");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000B56A File Offset: 0x0000976A
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000B572 File Offset: 0x00009772
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000B586 File Offset: 0x00009786
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000B58E File Offset: 0x0000978E
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

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000B5A2 File Offset: 0x000097A2
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000B5AA File Offset: 0x000097AA
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

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000B5BE File Offset: 0x000097BE
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000B5C6 File Offset: 0x000097C6
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

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060005C1 RID: 1473 RVA: 0x0000B5DC File Offset: 0x000097DC
		// (remove) Token: 0x060005C2 RID: 1474 RVA: 0x0000B614 File Offset: 0x00009814
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000B649 File Offset: 0x00009849
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000291 RID: 657
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportThumbnailType _Type;

		// Token: 0x04000292 RID: 658
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000293 RID: 659
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000294 RID: 660
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000295 RID: 661
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
