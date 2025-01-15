using System;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x0200019D RID: 413
	internal enum Precedence : byte
	{
		// Token: 0x0400048F RID: 1167
		None,
		// Token: 0x04000490 RID: 1168
		Conditional,
		// Token: 0x04000491 RID: 1169
		Coalesce,
		// Token: 0x04000492 RID: 1170
		Or,
		// Token: 0x04000493 RID: 1171
		And,
		// Token: 0x04000494 RID: 1172
		Compare,
		// Token: 0x04000495 RID: 1173
		Concat,
		// Token: 0x04000496 RID: 1174
		Add,
		// Token: 0x04000497 RID: 1175
		Mul,
		// Token: 0x04000498 RID: 1176
		Error,
		// Token: 0x04000499 RID: 1177
		PrefixUnary,
		// Token: 0x0400049A RID: 1178
		Power,
		// Token: 0x0400049B RID: 1179
		Postfix,
		// Token: 0x0400049C RID: 1180
		Primary,
		// Token: 0x0400049D RID: 1181
		Atomic
	}
}
