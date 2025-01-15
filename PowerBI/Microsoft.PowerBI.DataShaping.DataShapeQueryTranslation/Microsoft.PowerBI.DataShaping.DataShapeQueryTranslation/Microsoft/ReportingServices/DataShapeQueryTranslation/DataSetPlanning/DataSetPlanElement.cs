using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E0 RID: 224
	internal abstract class DataSetPlanElement : IStructuredToString
	{
		// Token: 0x0600092D RID: 2349
		public abstract void Accept(DataSetPlanElementVisitor visitor);

		// Token: 0x0600092E RID: 2350
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}
