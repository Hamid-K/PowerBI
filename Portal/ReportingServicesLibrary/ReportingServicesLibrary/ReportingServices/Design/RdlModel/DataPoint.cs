using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B0 RID: 944
	public class DataPoint
	{
		// Token: 0x06001EBB RID: 7867 RVA: 0x0007D878 File Offset: 0x0007BA78
		public DataPoint()
		{
			this.DataElementOutput = DataPoint.DataElementOutputs.Output;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0007D888 File Offset: 0x0007BA88
		public DataPoint(ICollection<string> values, DataLabel label, Marker marker, string dataName, DataPoint.DataElementOutputs dataOutput, Action action)
		{
			if (values != null)
			{
				this.DataValues = new List<DataValue>(values.Count);
				foreach (string text in values)
				{
					this.DataValues.Add(new DataValue(text));
				}
			}
			this.DataLabel = label;
			this.Marker = marker;
			this.DataElementName = dataName;
			this.DataElementOutput = dataOutput;
			this.Action = action;
		}

		// Token: 0x04000D2A RID: 3370
		public List<DataValue> DataValues;

		// Token: 0x04000D2B RID: 3371
		public DataLabel DataLabel;

		// Token: 0x04000D2C RID: 3372
		[DefaultValue("")]
		public Action Action;

		// Token: 0x04000D2D RID: 3373
		public Style Style;

		// Token: 0x04000D2E RID: 3374
		public Marker Marker;

		// Token: 0x04000D2F RID: 3375
		[DefaultValue("")]
		public string DataElementName;

		// Token: 0x04000D30 RID: 3376
		[DefaultValue(DataPoint.DataElementOutputs.Output)]
		public DataPoint.DataElementOutputs DataElementOutput;

		// Token: 0x0200050B RID: 1291
		public enum DataElementOutputs
		{
			// Token: 0x04001235 RID: 4661
			Output,
			// Token: 0x04001236 RID: 4662
			NoOutput
		}
	}
}
