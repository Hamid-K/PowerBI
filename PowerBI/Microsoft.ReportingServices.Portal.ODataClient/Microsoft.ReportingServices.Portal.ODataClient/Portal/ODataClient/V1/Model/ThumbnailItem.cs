using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000110 RID: 272
	[OriginalName("ThumbnailItem")]
	public class ThumbnailItem : INotifyPropertyChanged
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x00016F21 File Offset: 0x00015121
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ThumbnailItem CreateThumbnailItem(MobileReportThumbnailType type, Guid ID)
		{
			return new ThumbnailItem
			{
				Type = type,
				Id = ID
			};
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00016F36 File Offset: 0x00015136
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00016F3E File Offset: 0x0001513E
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00016F52 File Offset: 0x00015152
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x00016F5A File Offset: 0x0001515A
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00016F6E File Offset: 0x0001516E
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00016F76 File Offset: 0x00015176
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00016F8A File Offset: 0x0001518A
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00016F92 File Offset: 0x00015192
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00016FA6 File Offset: 0x000151A6
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00016FAE File Offset: 0x000151AE
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

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06000BDA RID: 3034 RVA: 0x00016FC4 File Offset: 0x000151C4
		// (remove) Token: 0x06000BDB RID: 3035 RVA: 0x00016FFC File Offset: 0x000151FC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000BDC RID: 3036 RVA: 0x00017031 File Offset: 0x00015231
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400055C RID: 1372
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportThumbnailType _Type;

		// Token: 0x0400055D RID: 1373
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400055E RID: 1374
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x0400055F RID: 1375
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000560 RID: 1376
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
