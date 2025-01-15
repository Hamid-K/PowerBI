using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A7 RID: 679
	internal sealed class FieldCollection : ReportElementCollectionBase<Microsoft.ReportingServices.OnDemandReportRendering.Field>
	{
		// Token: 0x06001A1E RID: 6686 RVA: 0x00069B2E File Offset: 0x00067D2E
		internal FieldCollection(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSetDef)
		{
			this.m_dataSetdef = dataSetDef;
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00069B3D File Offset: 0x00067D3D
		public override int Count
		{
			get
			{
				return this.m_dataSetdef.NonCalculatedFieldCount;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		public override Microsoft.ReportingServices.OnDemandReportRendering.Field this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_collection == null)
				{
					this.m_collection = new Microsoft.ReportingServices.OnDemandReportRendering.Field[this.Count];
				}
				if (this.m_collection[index] == null)
				{
					this.m_collection[index] = new Microsoft.ReportingServices.OnDemandReportRendering.Field(this.m_dataSetdef.Fields[index]);
				}
				return this.m_collection[index];
			}
		}

		// Token: 0x17000EE9 RID: 3817
		public Microsoft.ReportingServices.OnDemandReportRendering.Field this[string name]
		{
			get
			{
				return this[this.GetFieldIndex(name)];
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00069BF0 File Offset: 0x00067DF0
		public int GetFieldIndex(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return -1;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (string.Equals(name, this.m_dataSetdef.Fields[i].Name, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000D04 RID: 3332
		private Microsoft.ReportingServices.OnDemandReportRendering.Field[] m_collection;

		// Token: 0x04000D05 RID: 3333
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSetdef;
	}
}
