using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x02000175 RID: 373
	public enum EdmExpressionKind
	{
		// Token: 0x040002DA RID: 730
		None,
		// Token: 0x040002DB RID: 731
		BinaryConstant,
		// Token: 0x040002DC RID: 732
		BooleanConstant,
		// Token: 0x040002DD RID: 733
		DateTimeOffsetConstant,
		// Token: 0x040002DE RID: 734
		DecimalConstant,
		// Token: 0x040002DF RID: 735
		FloatingConstant,
		// Token: 0x040002E0 RID: 736
		GuidConstant,
		// Token: 0x040002E1 RID: 737
		IntegerConstant,
		// Token: 0x040002E2 RID: 738
		StringConstant,
		// Token: 0x040002E3 RID: 739
		DurationConstant,
		// Token: 0x040002E4 RID: 740
		Null,
		// Token: 0x040002E5 RID: 741
		Record,
		// Token: 0x040002E6 RID: 742
		Collection,
		// Token: 0x040002E7 RID: 743
		Path,
		// Token: 0x040002E8 RID: 744
		ParameterReference,
		// Token: 0x040002E9 RID: 745
		OperationReference,
		// Token: 0x040002EA RID: 746
		PropertyReference,
		// Token: 0x040002EB RID: 747
		ValueTermReference,
		// Token: 0x040002EC RID: 748
		EntitySetReference,
		// Token: 0x040002ED RID: 749
		EnumMemberReference,
		// Token: 0x040002EE RID: 750
		If,
		// Token: 0x040002EF RID: 751
		Cast,
		// Token: 0x040002F0 RID: 752
		IsType,
		// Token: 0x040002F1 RID: 753
		OperationApplication,
		// Token: 0x040002F2 RID: 754
		LabeledExpressionReference,
		// Token: 0x040002F3 RID: 755
		Labeled,
		// Token: 0x040002F4 RID: 756
		PropertyPath,
		// Token: 0x040002F5 RID: 757
		NavigationPropertyPath,
		// Token: 0x040002F6 RID: 758
		DateConstant,
		// Token: 0x040002F7 RID: 759
		TimeOfDayConstant,
		// Token: 0x040002F8 RID: 760
		EnumMember
	}
}
