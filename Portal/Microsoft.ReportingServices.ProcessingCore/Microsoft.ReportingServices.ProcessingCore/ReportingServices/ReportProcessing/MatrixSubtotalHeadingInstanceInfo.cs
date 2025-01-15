using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200073C RID: 1852
	[Serializable]
	internal sealed class MatrixSubtotalHeadingInstanceInfo : MatrixHeadingInstanceInfo
	{
		// Token: 0x060066F4 RID: 26356 RVA: 0x001929FC File Offset: 0x00190BFC
		internal MatrixSubtotalHeadingInstanceInfo(ReportProcessing.ProcessingContext pc, int headingCellIndex, MatrixHeading matrixHeadingDef, MatrixHeadingInstance owner, bool isSubtotal, int reportItemDefIndex, VariantList groupExpressionValues, out NonComputedUniqueNames nonComputedUniqueNames)
			: base(pc, headingCellIndex, matrixHeadingDef, owner, isSubtotal, reportItemDefIndex, groupExpressionValues, out nonComputedUniqueNames)
		{
			Global.Tracer.Assert(isSubtotal);
			Global.Tracer.Assert(matrixHeadingDef.Subtotal != null);
			Global.Tracer.Assert(matrixHeadingDef.Subtotal.StyleClass != null);
			if (matrixHeadingDef.Subtotal.StyleClass.ExpressionList != null)
			{
				this.m_styleAttributeValues = new object[matrixHeadingDef.Subtotal.StyleClass.ExpressionList.Count];
				ReportProcessing.RuntimeRICollection.EvaluateStyleAttributes(ObjectType.Subtotal, matrixHeadingDef.Grouping.Name, matrixHeadingDef.Subtotal.StyleClass, owner.UniqueName, this.m_styleAttributeValues, pc);
			}
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x00192AB0 File Offset: 0x00190CB0
		internal MatrixSubtotalHeadingInstanceInfo()
		{
		}

		// Token: 0x17002467 RID: 9319
		// (get) Token: 0x060066F6 RID: 26358 RVA: 0x00192AB8 File Offset: 0x00190CB8
		// (set) Token: 0x060066F7 RID: 26359 RVA: 0x00192AC0 File Offset: 0x00190CC0
		internal object[] StyleAttributeValues
		{
			get
			{
				return this.m_styleAttributeValues;
			}
			set
			{
				this.m_styleAttributeValues = value;
			}
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x00192ACC File Offset: 0x00190CCC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.StyleAttributeValues, ObjectType.Variant)
			});
		}

		// Token: 0x04003328 RID: 13096
		private object[] m_styleAttributeValues;
	}
}
