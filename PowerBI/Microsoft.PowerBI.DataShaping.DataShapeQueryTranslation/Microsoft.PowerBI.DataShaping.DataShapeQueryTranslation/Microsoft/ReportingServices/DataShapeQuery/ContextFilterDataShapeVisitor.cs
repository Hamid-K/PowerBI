using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x02000007 RID: 7
	internal sealed class ContextFilterDataShapeVisitor : FilterVisitor<FilterCondition>
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002189 File Offset: 0x00000389
		internal ContextFilterDataShapeVisitor(VisitDataShapeDelegate visitDataShape)
			: base(visitDataShape)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002192 File Offset: 0x00000392
		public static void Visit(Filter filter, VisitDataShapeDelegate visitDataShape)
		{
			new ContextFilterDataShapeVisitor(visitDataShape).Visit(filter);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A1 File Offset: 0x000003A1
		internal override FilterCondition Visit(ContextFilterCondition condition)
		{
			base.Visit(condition);
			return condition;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021AC File Offset: 0x000003AC
		internal override FilterCondition Visit(ApplyFilterCondition condition)
		{
			base.Visit(condition);
			return condition;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B7 File Offset: 0x000003B7
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021BA File Offset: 0x000003BA
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021BD File Offset: 0x000003BD
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			return null;
		}
	}
}
