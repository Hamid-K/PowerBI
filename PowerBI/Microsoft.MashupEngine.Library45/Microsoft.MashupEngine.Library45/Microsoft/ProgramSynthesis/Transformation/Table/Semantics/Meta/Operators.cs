using System;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta
{
	// Token: 0x02001AF4 RID: 6900
	[Flags]
	public enum Operators
	{
		// Token: 0x04005616 RID: 22038
		None = 0,
		// Token: 0x04005617 RID: 22039
		All = -1,
		// Token: 0x04005618 RID: 22040
		SplitText = 1,
		// Token: 0x04005619 RID: 22041
		TransformationFormula = 2,
		// Token: 0x0400561A RID: 22042
		LabelEncoding = 4,
		// Token: 0x0400561B RID: 22043
		OneHotEncoding = 8,
		// Token: 0x0400561C RID: 22044
		DropEmptyColumn = 16,
		// Token: 0x0400561D RID: 22045
		DropDuplicateColumn = 32,
		// Token: 0x0400561E RID: 22046
		DropConstantColumn = 64,
		// Token: 0x0400561F RID: 22047
		DropIndexColumn = 128,
		// Token: 0x04005620 RID: 22048
		CastColumn = 256,
		// Token: 0x04005621 RID: 22049
		DropDuplicateRows = 512,
		// Token: 0x04005622 RID: 22050
		DropEmptyRows = 1024,
		// Token: 0x04005623 RID: 22051
		DropOutlierRows = 2048,
		// Token: 0x04005624 RID: 22052
		FillMissingValues = 4096,
		// Token: 0x04005625 RID: 22053
		MultiLabelBinarizer = 8192,
		// Token: 0x04005626 RID: 22054
		AddColumnsFromJson = 16384
	}
}
