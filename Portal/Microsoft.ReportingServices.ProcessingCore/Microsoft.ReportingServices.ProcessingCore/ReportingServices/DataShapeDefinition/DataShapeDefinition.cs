using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200058D RID: 1421
	[DataContract]
	internal sealed class DataShapeDefinition
	{
		// Token: 0x060051A4 RID: 20900 RVA: 0x00159D77 File Offset: 0x00157F77
		internal DataShapeDefinition(IEnumerable<DataSource> dataSources, IEnumerable<DataSet> dataSets, IEnumerable<DataShape> dataShapes)
		{
			this.m_dataSources = dataSources.ToReadOnlyCollection<DataSource>();
			this.m_dataSets = dataSets.ToReadOnlyCollection<DataSet>();
			this.m_dataShapes = dataShapes.ToReadOnlyCollection<DataShape>();
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x060051A5 RID: 20901 RVA: 0x00159DA3 File Offset: 0x00157FA3
		internal IEnumerable<DataSource> DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x00159DAB File Offset: 0x00157FAB
		internal IEnumerable<DataSet> DataSets
		{
			get
			{
				return this.m_dataSets;
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x00159DB3 File Offset: 0x00157FB3
		internal IEnumerable<DataShape> DataShapes
		{
			get
			{
				return this.m_dataShapes;
			}
		}

		// Token: 0x04002938 RID: 10552
		[DataMember(Name = "DataSources", Order = 1)]
		private readonly IEnumerable<DataSource> m_dataSources;

		// Token: 0x04002939 RID: 10553
		[DataMember(Name = "DataSets", Order = 2)]
		private readonly IEnumerable<DataSet> m_dataSets;

		// Token: 0x0400293A RID: 10554
		[DataMember(Name = "DataShapes", Order = 3)]
		private readonly IEnumerable<DataShape> m_dataShapes;
	}
}
