using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x02001899 RID: 6297
	internal enum PowerQueryPrecedence
	{
		// Token: 0x04005079 RID: 20601
		Primary = 24,
		// Token: 0x0400507A RID: 20602
		Unary = 22,
		// Token: 0x0400507B RID: 20603
		Metadata = 20,
		// Token: 0x0400507C RID: 20604
		Multiplicative = 18,
		// Token: 0x0400507D RID: 20605
		Additive = 16,
		// Token: 0x0400507E RID: 20606
		Relational = 14,
		// Token: 0x0400507F RID: 20607
		Equality = 12,
		// Token: 0x04005080 RID: 20608
		TypeAssertion = 10,
		// Token: 0x04005081 RID: 20609
		TypeConformance = 8,
		// Token: 0x04005082 RID: 20610
		LogicalAnd = 6,
		// Token: 0x04005083 RID: 20611
		LogicalOr = 4,
		// Token: 0x04005084 RID: 20612
		Coalesce = 2
	}
}
