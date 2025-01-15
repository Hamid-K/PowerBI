using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009C RID: 156
	public enum EdmExpressionKind
	{
		// Token: 0x04000123 RID: 291
		None,
		// Token: 0x04000124 RID: 292
		BinaryConstant,
		// Token: 0x04000125 RID: 293
		BooleanConstant,
		// Token: 0x04000126 RID: 294
		DateTimeOffsetConstant,
		// Token: 0x04000127 RID: 295
		DecimalConstant,
		// Token: 0x04000128 RID: 296
		FloatingConstant,
		// Token: 0x04000129 RID: 297
		GuidConstant,
		// Token: 0x0400012A RID: 298
		IntegerConstant,
		// Token: 0x0400012B RID: 299
		StringConstant,
		// Token: 0x0400012C RID: 300
		DurationConstant,
		// Token: 0x0400012D RID: 301
		Null,
		// Token: 0x0400012E RID: 302
		Record,
		// Token: 0x0400012F RID: 303
		Collection,
		// Token: 0x04000130 RID: 304
		Path,
		// Token: 0x04000131 RID: 305
		If,
		// Token: 0x04000132 RID: 306
		Cast,
		// Token: 0x04000133 RID: 307
		IsType,
		// Token: 0x04000134 RID: 308
		FunctionApplication,
		// Token: 0x04000135 RID: 309
		LabeledExpressionReference,
		// Token: 0x04000136 RID: 310
		Labeled,
		// Token: 0x04000137 RID: 311
		PropertyPath,
		// Token: 0x04000138 RID: 312
		NavigationPropertyPath,
		// Token: 0x04000139 RID: 313
		DateConstant,
		// Token: 0x0400013A RID: 314
		TimeOfDayConstant,
		// Token: 0x0400013B RID: 315
		EnumMember,
		// Token: 0x0400013C RID: 316
		AnnotationPath
	}
}
