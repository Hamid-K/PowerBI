using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A5 RID: 677
	internal sealed class DataSetCollection : ReportElementCollectionBase<Microsoft.ReportingServices.OnDemandReportRendering.DataSet>
	{
		// Token: 0x06001A12 RID: 6674 RVA: 0x00069965 File Offset: 0x00067B65
		internal DataSetCollection(Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDef, RenderingContext renderingContext)
		{
			this.m_reportDef = reportDef;
			this.m_rendringContext = renderingContext;
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0006997B File Offset: 0x00067B7B
		public override int Count
		{
			get
			{
				return this.m_reportDef.DataSetCount;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		public override Microsoft.ReportingServices.OnDemandReportRendering.DataSet this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_collection == null)
				{
					this.m_collection = new Microsoft.ReportingServices.OnDemandReportRendering.DataSet[this.Count];
				}
				if (this.m_collection[index] == null)
				{
					this.m_collection[index] = new Microsoft.ReportingServices.OnDemandReportRendering.DataSet(this.m_reportDef.MappingDataSetIndexToDataSet[index], this.m_rendringContext);
				}
				return this.m_collection[index];
			}
		}

		// Token: 0x17000EE1 RID: 3809
		public Microsoft.ReportingServices.OnDemandReportRendering.DataSet this[string name]
		{
			get
			{
				if (this.m_reportDef.MappingNameToDataSet.ContainsKey(name))
				{
					return this[this.m_reportDef.MappingDataSetIndexToDataSet.IndexOf(this.m_reportDef.MappingNameToDataSet[name])];
				}
				return null;
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00069A60 File Offset: 0x00067C60
		internal void SetNewContext()
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					Microsoft.ReportingServices.OnDemandReportRendering.DataSet dataSet = this.m_collection[i];
					if (dataSet != null)
					{
						dataSet.SetNewContext();
					}
				}
			}
		}

		// Token: 0x04000CFD RID: 3325
		private Microsoft.ReportingServices.OnDemandReportRendering.DataSet[] m_collection;

		// Token: 0x04000CFE RID: 3326
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_reportDef;

		// Token: 0x04000CFF RID: 3327
		private RenderingContext m_rendringContext;
	}
}
