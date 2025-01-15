using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Model
{
	// Token: 0x02000018 RID: 24
	public class PowerBIReport : CatalogItem
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002352 File Offset: 0x00000552
		public PowerBIReport()
			: base(CatalogItemType.PowerBIReport)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000235C File Offset: 0x0000055C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002364 File Offset: 0x00000564
		[ReadOnly(true)]
		public bool HasDataSources { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x0000236D File Offset: 0x0000056D
		public Stream GetOriginalPbiStream()
		{
			return this._originalPbix ?? base.GetContentStream();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000237F File Offset: 0x0000057F
		public Stream GetPbixStream()
		{
			return this._pbix ?? this._originalPbix;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002391 File Offset: 0x00000591
		public void SetModelStream(Stream value)
		{
			this._model = value;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000239A File Offset: 0x0000059A
		public Stream GetModelStream()
		{
			return this._model;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000023A2 File Offset: 0x000005A2
		public void SetPreShreddedReadStreams(Stream originalPbix, Stream pbix, Stream model)
		{
			this._originalPbix = originalPbix;
			this._pbix = pbix;
			this._model = model;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000023B9 File Offset: 0x000005B9
		public void ClearContent()
		{
			this._originalPbix = null;
			this._pbix = null;
			this._model = null;
			base.Content = new byte[0];
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000023DC File Offset: 0x000005DC
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002402 File Offset: 0x00000602
		public IList<DataSource> DataSources
		{
			get
			{
				IList<DataSource> list;
				if ((list = this._dataSources) == null)
				{
					list = (this._dataSources = this.LoadDataSources());
				}
				return list;
			}
			set
			{
				this._dataSources = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002432 File Offset: 0x00000632
		public IList<DataModelParameter> DataModelParameters
		{
			get
			{
				IList<DataModelParameter> list;
				if ((list = this._dataModelParameters) == null)
				{
					list = (this._dataModelParameters = this.LoadDataModelParameters());
				}
				return list;
			}
			set
			{
				this._dataModelParameters = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000243C File Offset: 0x0000063C
		public IList<CacheRefreshPlan> CacheRefreshPlans
		{
			get
			{
				IList<CacheRefreshPlan> list;
				if ((list = this._cacheRefreshPlans) == null)
				{
					list = (this._cacheRefreshPlans = this.LoadCacheRefreshPlans());
				}
				return list;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002464 File Offset: 0x00000664
		public IList<DataModelRoleAssignment> DataModelRoleAssignments
		{
			get
			{
				IList<DataModelRoleAssignment> list;
				if ((list = this._dataModelRoleAssignments) == null)
				{
					list = (this._dataModelRoleAssignments = this.LoadDataModelRoleAssignments());
				}
				return list;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000248C File Offset: 0x0000068C
		public IList<DataModelRole> DataModelRoles
		{
			get
			{
				IList<DataModelRole> list;
				if ((list = this._dataModelRoles) == null)
				{
					list = (this._dataModelRoles = this.LoadDataModelRoles());
				}
				return list;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000024B2 File Offset: 0x000006B2
		protected virtual IList<DataSource> LoadDataSources()
		{
			return new List<DataSource>();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000024B9 File Offset: 0x000006B9
		protected virtual IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			return new List<CacheRefreshPlan>();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000024C0 File Offset: 0x000006C0
		protected virtual IList<DataModelRole> LoadDataModelRoles()
		{
			return new List<DataModelRole>();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000024C7 File Offset: 0x000006C7
		protected virtual IList<DataModelRoleAssignment> LoadDataModelRoleAssignments()
		{
			return new List<DataModelRoleAssignment>();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000024CE File Offset: 0x000006CE
		protected virtual IList<DataModelParameter> LoadDataModelParameters()
		{
			return new List<DataModelParameter>();
		}

		// Token: 0x04000097 RID: 151
		public const string IsMobileOptimizedPropertyName = "IsMobileOptimized";

		// Token: 0x04000098 RID: 152
		public const string PbixShredderVersionPropertyName = "PbixShredderVersion";

		// Token: 0x04000099 RID: 153
		public const string HasEmbeddedModels = "HasEmbeddedModels";

		// Token: 0x0400009A RID: 154
		public const string ModelRefreshAllowed = "ModelRefreshAllowed";

		// Token: 0x0400009B RID: 155
		public const string HasDirectQuery = "HasDirectQuery";

		// Token: 0x0400009C RID: 156
		public const string ModelVersion = "ModelVersion";

		// Token: 0x0400009D RID: 157
		private IList<DataSource> _dataSources;

		// Token: 0x0400009E RID: 158
		private IList<CacheRefreshPlan> _cacheRefreshPlans;

		// Token: 0x0400009F RID: 159
		private IList<DataModelRole> _dataModelRoles;

		// Token: 0x040000A0 RID: 160
		private IList<DataModelRoleAssignment> _dataModelRoleAssignments;

		// Token: 0x040000A1 RID: 161
		private IList<DataModelParameter> _dataModelParameters;

		// Token: 0x040000A3 RID: 163
		private Stream _originalPbix;

		// Token: 0x040000A4 RID: 164
		private Stream _pbix;

		// Token: 0x040000A5 RID: 165
		private Stream _model;
	}
}
