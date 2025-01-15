using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000AE RID: 174
	[OriginalName("CatalogItemDrillthroughTarget")]
	public class CatalogItemDrillthroughTarget : DrillthroughTarget
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0000EE3D File Offset: 0x0000D03D
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0000EE59 File Offset: 0x0000D059
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0000EE61 File Offset: 0x0000D061
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

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0000EE75 File Offset: 0x0000D075
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0000EE7D File Offset: 0x0000D07D
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

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0000EE91 File Offset: 0x0000D091
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0000EE99 File Offset: 0x0000D099
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

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0000EEB5 File Offset: 0x0000D0B5
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

		// Token: 0x04000381 RID: 897
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemType _CatalogItemType;

		// Token: 0x04000382 RID: 898
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000383 RID: 899
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000384 RID: 900
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<CatalogItemParameter> _Parameters = new ObservableCollection<CatalogItemParameter>();
	}
}
