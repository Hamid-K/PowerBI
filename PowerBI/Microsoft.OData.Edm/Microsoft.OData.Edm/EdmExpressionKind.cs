using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002C RID: 44
	public enum EdmExpressionKind
	{
		// Token: 0x0400002D RID: 45
		None,
		// Token: 0x0400002E RID: 46
		BinaryConstant,
		// Token: 0x0400002F RID: 47
		BooleanConstant,
		// Token: 0x04000030 RID: 48
		DateTimeOffsetConstant,
		// Token: 0x04000031 RID: 49
		DecimalConstant,
		// Token: 0x04000032 RID: 50
		FloatingConstant,
		// Token: 0x04000033 RID: 51
		GuidConstant,
		// Token: 0x04000034 RID: 52
		IntegerConstant,
		// Token: 0x04000035 RID: 53
		StringConstant,
		// Token: 0x04000036 RID: 54
		DurationConstant,
		// Token: 0x04000037 RID: 55
		Null,
		// Token: 0x04000038 RID: 56
		Record,
		// Token: 0x04000039 RID: 57
		Collection,
		// Token: 0x0400003A RID: 58
		Path,
		// Token: 0x0400003B RID: 59
		If,
		// Token: 0x0400003C RID: 60
		Cast,
		// Token: 0x0400003D RID: 61
		IsType,
		// Token: 0x0400003E RID: 62
		FunctionApplication,
		// Token: 0x0400003F RID: 63
		LabeledExpressionReference,
		// Token: 0x04000040 RID: 64
		Labeled,
		// Token: 0x04000041 RID: 65
		PropertyPath,
		// Token: 0x04000042 RID: 66
		NavigationPropertyPath,
		// Token: 0x04000043 RID: 67
		DateConstant,
		// Token: 0x04000044 RID: 68
		TimeOfDayConstant,
		// Token: 0x04000045 RID: 69
		EnumMember,
		// Token: 0x04000046 RID: 70
		AnnotationPath
	}
}
