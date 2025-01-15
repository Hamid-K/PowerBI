using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000086 RID: 134
	internal sealed class RdmReport
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000C62A File Offset: 0x0000A82A
		internal RdmReport(List<ReportSection> sections, List<DataSet> dataSets, List<DataSource> dataSources)
		{
			this._sections = sections;
			this._dataSets = dataSets;
			this._dataSources = dataSources;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000C647 File Offset: 0x0000A847
		public List<ReportSection> ReportSections
		{
			get
			{
				return this._sections;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C64F File Offset: 0x0000A84F
		public List<DataSet> DataSets
		{
			get
			{
				return this._dataSets;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000C657 File Offset: 0x0000A857
		public List<DataSource> DataSources
		{
			get
			{
				return this._dataSources;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C65F File Offset: 0x0000A85F
		public DataSource FindDataSourceForSection(ReportSection section)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C668 File Offset: 0x0000A868
		public DataSet FindDataSet(string name)
		{
			return this._dataSets.FirstOrDefault((DataSet dataSet) => dataSet.Name == name);
		}

		// Token: 0x040001A4 RID: 420
		private readonly List<ReportSection> _sections;

		// Token: 0x040001A5 RID: 421
		private readonly List<DataSet> _dataSets;

		// Token: 0x040001A6 RID: 422
		private readonly List<DataSource> _dataSources;
	}
}
