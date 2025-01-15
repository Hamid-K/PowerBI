using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000BE RID: 190
	public class Query : QueryBase
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001BDB2 File Offset: 0x00019FB2
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0001BDC5 File Offset: 0x00019FC5
		public string DataSourceName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		[XmlIgnore]
		public DataSource DataSource
		{
			get
			{
				DataSource dataSource = null;
				if (!string.IsNullOrEmpty(this.DataSourceName))
				{
					Report ancestor = base.GetAncestor<Report>();
					if (ancestor != null)
					{
						dataSource = ancestor.GetDataSourceByName(this.DataSourceName);
					}
				}
				return dataSource;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001BE08 File Offset: 0x0001A008
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0001BE1B File Offset: 0x0001A01B
		[XmlElement(typeof(RdlCollection<QueryParameter>))]
		public IList<QueryParameter> QueryParameters
		{
			get
			{
				return (IList<QueryParameter>)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001BE2A File Offset: 0x0001A02A
		public Query()
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001BE32 File Offset: 0x0001A032
		internal Query(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001BE3B File Offset: 0x0001A03B
		public override void Initialize()
		{
			base.Initialize();
			this.DataSourceName = "";
			this.QueryParameters = new RdlCollection<QueryParameter>();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001BE59 File Offset: 0x0001A059
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.DataSourceName = nameChanges.GetNewName(NameChanges.EntryType.DataSource, this.DataSourceName);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001BE78 File Offset: 0x0001A078
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			if (!string.IsNullOrEmpty(this.DataSourceName))
			{
				Report ancestor = base.GetAncestor<Report>();
				if (ancestor != null)
				{
					DataSource dataSourceByName = ancestor.GetDataSourceByName(this.DataSourceName);
					if (dataSourceByName != null && !dependencies.Contains(dataSourceByName))
					{
						dependencies.Add(dataSourceByName);
					}
				}
			}
		}

		// Token: 0x0200036A RID: 874
		internal new class Definition : DefinitionStore<Query, Query.Definition.Properties>
		{
			// Token: 0x02000487 RID: 1159
			public enum Properties
			{
				// Token: 0x04000B16 RID: 2838
				CommandType,
				// Token: 0x04000B17 RID: 2839
				CommandText,
				// Token: 0x04000B18 RID: 2840
				Timeout,
				// Token: 0x04000B19 RID: 2841
				DataSourceName,
				// Token: 0x04000B1A RID: 2842
				QueryParameters
			}
		}
	}
}
