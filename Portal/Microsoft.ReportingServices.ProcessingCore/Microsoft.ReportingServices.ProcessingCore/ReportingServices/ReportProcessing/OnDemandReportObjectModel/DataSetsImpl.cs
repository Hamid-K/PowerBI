using System;
using System.Collections;
using System.Threading;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007BC RID: 1980
	internal sealed class DataSetsImpl : DataSets
	{
		// Token: 0x06007054 RID: 28756 RVA: 0x001D4295 File Offset: 0x001D2495
		internal DataSetsImpl(int size)
		{
			this.m_lockAdd = size > 1;
			this.m_collection = new Hashtable(size);
		}

		// Token: 0x06007055 RID: 28757 RVA: 0x001D42B4 File Offset: 0x001D24B4
		internal void AddOrUpdate(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSetDef, DataSetInstance dataSetInstance, DateTime reportExecutionTime)
		{
			try
			{
				if (this.m_lockAdd)
				{
					Monitor.Enter(this.m_collection);
				}
				DataSetImpl dataSetImpl = this.m_collection[dataSetDef.Name] as DataSetImpl;
				if (dataSetImpl == null)
				{
					this.m_collection.Add(dataSetDef.Name, new DataSetImpl(dataSetDef, dataSetInstance, reportExecutionTime));
				}
				else
				{
					dataSetImpl.Update(dataSetInstance, reportExecutionTime);
				}
			}
			finally
			{
				if (this.m_lockAdd)
				{
					Monitor.Exit(this.m_collection);
				}
			}
		}

		// Token: 0x17002646 RID: 9798
		public override Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSet this[string key]
		{
			get
			{
				if (key == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingDataSetReference(key);
				}
				Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSet dataSet = this.m_collection[key] as Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSet;
				if (dataSet == null)
				{
					throw new ReportProcessingException_NonExistingDataSetReference(key);
				}
				return dataSet;
			}
		}

		// Token: 0x04003A04 RID: 14852
		private bool m_lockAdd;

		// Token: 0x04003A05 RID: 14853
		private Hashtable m_collection;
	}
}
