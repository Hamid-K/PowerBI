using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000098 RID: 152
	internal class FilterDataShapeVisitor : FilterVisitor<FilterCondition>
	{
		// Token: 0x06000393 RID: 915 RVA: 0x000070DD File Offset: 0x000052DD
		internal FilterDataShapeVisitor(VisitDataShapeDelegate visitDataShape)
			: base(visitDataShape)
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000070E6 File Offset: 0x000052E6
		public static void Visit(Filter filter, VisitDataShapeDelegate visitDataShape)
		{
			new FilterDataShapeVisitor(visitDataShape).Visit(filter);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000070F5 File Offset: 0x000052F5
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			return null;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000070F8 File Offset: 0x000052F8
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			return null;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000070FB File Offset: 0x000052FB
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			return null;
		}
	}
}
