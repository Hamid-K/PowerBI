using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200000B RID: 11
	[OriginalName("CatalogItemDrillthroughTarget")]
	public class CatalogItemDrillthroughTarget : DrillthroughTarget
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002A01 File Offset: 0x00000C01
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CatalogItemDrillthroughTarget CreateCatalogItemDrillthroughTarget(DrillthroughTargetType type, CatalogItemType catalogItemType, Guid ID)
		{
			return new CatalogItemDrillthroughTarget
			{
				Type = type,
				CatalogItemType = catalogItemType,
				Id = ID
			};
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002A1D File Offset: 0x00000C1D
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002A25 File Offset: 0x00000C25
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CatalogItemType")]
		public CatalogItemType CatalogItemType
		{
			get
			{
				return this._CatalogItemType;
			}
			set
			{
				this._CatalogItemType = value;
				this.OnPropertyChanged("CatalogItemType");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002A39 File Offset: 0x00000C39
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002A41 File Offset: 0x00000C41
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002A55 File Offset: 0x00000C55
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002A5D File Offset: 0x00000C5D
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002A71 File Offset: 0x00000C71
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002A79 File Offset: 0x00000C79
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Parameters")]
		public ObservableCollection<CatalogItemParameter> Parameters
		{
			get
			{
				return this._Parameters;
			}
			set
			{
				this._Parameters = value;
				this.OnPropertyChanged("Parameters");
			}
		}

		// Token: 0x0400005F RID: 95
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemType _CatalogItemType;

		// Token: 0x04000060 RID: 96
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000061 RID: 97
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000062 RID: 98
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<CatalogItemParameter> _Parameters = new ObservableCollection<CatalogItemParameter>();
	}
}
