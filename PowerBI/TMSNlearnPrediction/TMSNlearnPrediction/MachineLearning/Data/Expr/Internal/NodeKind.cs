using System;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A1 RID: 417
	internal enum NodeKind
	{
		// Token: 0x040004B2 RID: 1202
		Lambda,
		// Token: 0x040004B3 RID: 1203
		Param,
		// Token: 0x040004B4 RID: 1204
		Conditional,
		// Token: 0x040004B5 RID: 1205
		BinaryOp,
		// Token: 0x040004B6 RID: 1206
		UnaryOp,
		// Token: 0x040004B7 RID: 1207
		Compare,
		// Token: 0x040004B8 RID: 1208
		Call,
		// Token: 0x040004B9 RID: 1209
		List,
		// Token: 0x040004BA RID: 1210
		With,
		// Token: 0x040004BB RID: 1211
		WithLocal,
		// Token: 0x040004BC RID: 1212
		Name,
		// Token: 0x040004BD RID: 1213
		Ident,
		// Token: 0x040004BE RID: 1214
		BoolLit,
		// Token: 0x040004BF RID: 1215
		NumLit,
		// Token: 0x040004C0 RID: 1216
		StrLit
	}
}
