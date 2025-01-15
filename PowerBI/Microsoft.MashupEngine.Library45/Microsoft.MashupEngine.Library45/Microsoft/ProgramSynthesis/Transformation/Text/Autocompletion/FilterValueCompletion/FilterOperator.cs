using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion
{
	// Token: 0x02001E1F RID: 7711
	public enum FilterOperator
	{
		// Token: 0x0400613A RID: 24890
		EqualTo,
		// Token: 0x0400613B RID: 24891
		NotEqualTo,
		// Token: 0x0400613C RID: 24892
		IsNull,
		// Token: 0x0400613D RID: 24893
		IsNotNull,
		// Token: 0x0400613E RID: 24894
		IsError,
		// Token: 0x0400613F RID: 24895
		IsNotError,
		// Token: 0x04006140 RID: 24896
		IsEmpty,
		// Token: 0x04006141 RID: 24897
		IsNotEmpty,
		// Token: 0x04006142 RID: 24898
		StartsWith,
		// Token: 0x04006143 RID: 24899
		NotStartsWith,
		// Token: 0x04006144 RID: 24900
		EndsWith,
		// Token: 0x04006145 RID: 24901
		NotEndsWith,
		// Token: 0x04006146 RID: 24902
		Contains,
		// Token: 0x04006147 RID: 24903
		NotContains
	}
}
