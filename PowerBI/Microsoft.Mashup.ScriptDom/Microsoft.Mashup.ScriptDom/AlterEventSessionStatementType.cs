using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	internal enum AlterEventSessionStatementType
	{
		// Token: 0x040000B4 RID: 180
		Unknown,
		// Token: 0x040000B5 RID: 181
		AddEventDeclarationOptionalSessionOptions,
		// Token: 0x040000B6 RID: 182
		DropEventSpecificationOptionalSessionOptions,
		// Token: 0x040000B7 RID: 183
		AddTargetDeclarationOptionalSessionOptions,
		// Token: 0x040000B8 RID: 184
		DropTargetSpecificationOptionalSessionOptions,
		// Token: 0x040000B9 RID: 185
		RequiredSessionOptions,
		// Token: 0x040000BA RID: 186
		AlterStateIsStart,
		// Token: 0x040000BB RID: 187
		AlterStateIsStop
	}
}
