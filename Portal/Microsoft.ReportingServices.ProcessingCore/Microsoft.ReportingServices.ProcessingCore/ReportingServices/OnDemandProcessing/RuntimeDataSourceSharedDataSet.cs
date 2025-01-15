using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000807 RID: 2055
	internal sealed class RuntimeDataSourceSharedDataSet : RuntimeAtomicDataSource
	{
		// Token: 0x06007277 RID: 29303 RVA: 0x001DC3EB File Offset: 0x001DA5EB
		internal RuntimeDataSourceSharedDataSet(DataSetDefinition dataSetDefinition, OnDemandProcessingContext odpContext)
			: base(null, new DataSource(-1, dataSetDefinition.SharedDataSourceReferenceId, dataSetDefinition.DataSetCore), odpContext, false)
		{
			this.m_dataSetDefinition = dataSetDefinition;
		}

		// Token: 0x170026CD RID: 9933
		// (get) Token: 0x06007278 RID: 29304 RVA: 0x001DC40F File Offset: 0x001DA60F
		protected override bool CreatesDataChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007279 RID: 29305 RVA: 0x001DC414 File Offset: 0x001DA614
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			List<RuntimeDataSet> list = new List<RuntimeDataSet>(1);
			DataSet dataSet = base.DataSourceDefinition.DataSets[0];
			DataSetInstance dataSetInstance = new DataSetInstance(dataSet);
			this.m_runtimeDataSet = new RuntimeSharedDataSet(base.DataSourceDefinition, dataSet, dataSetInstance, base.OdpContext);
			list.Add(this.m_runtimeDataSet);
			return list;
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x001DC465 File Offset: 0x001DA665
		protected override void OpenInitialConnectionAndTransaction()
		{
		}

		// Token: 0x170026CE RID: 9934
		// (get) Token: 0x0600727B RID: 29307 RVA: 0x001DC467 File Offset: 0x001DA667
		internal override bool NoRows
		{
			get
			{
				return base.CheckNoRows(this.m_runtimeDataSet);
			}
		}

		// Token: 0x04003ABC RID: 15036
		private RuntimeSharedDataSet m_runtimeDataSet;

		// Token: 0x04003ABD RID: 15037
		private readonly DataSetDefinition m_dataSetDefinition;
	}
}
