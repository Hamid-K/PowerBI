using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000677 RID: 1655
	internal enum BuiltInKind
	{
		// Token: 0x04001C99 RID: 7321
		And,
		// Token: 0x04001C9A RID: 7322
		Or,
		// Token: 0x04001C9B RID: 7323
		Not,
		// Token: 0x04001C9C RID: 7324
		Cast,
		// Token: 0x04001C9D RID: 7325
		OfType,
		// Token: 0x04001C9E RID: 7326
		Treat,
		// Token: 0x04001C9F RID: 7327
		IsOf,
		// Token: 0x04001CA0 RID: 7328
		Union,
		// Token: 0x04001CA1 RID: 7329
		UnionAll,
		// Token: 0x04001CA2 RID: 7330
		Intersect,
		// Token: 0x04001CA3 RID: 7331
		Overlaps,
		// Token: 0x04001CA4 RID: 7332
		AnyElement,
		// Token: 0x04001CA5 RID: 7333
		Element,
		// Token: 0x04001CA6 RID: 7334
		Except,
		// Token: 0x04001CA7 RID: 7335
		Exists,
		// Token: 0x04001CA8 RID: 7336
		Flatten,
		// Token: 0x04001CA9 RID: 7337
		In,
		// Token: 0x04001CAA RID: 7338
		NotIn,
		// Token: 0x04001CAB RID: 7339
		Distinct,
		// Token: 0x04001CAC RID: 7340
		IsNull,
		// Token: 0x04001CAD RID: 7341
		IsNotNull,
		// Token: 0x04001CAE RID: 7342
		Like,
		// Token: 0x04001CAF RID: 7343
		Equal,
		// Token: 0x04001CB0 RID: 7344
		NotEqual,
		// Token: 0x04001CB1 RID: 7345
		LessEqual,
		// Token: 0x04001CB2 RID: 7346
		LessThan,
		// Token: 0x04001CB3 RID: 7347
		GreaterThan,
		// Token: 0x04001CB4 RID: 7348
		GreaterEqual,
		// Token: 0x04001CB5 RID: 7349
		Plus,
		// Token: 0x04001CB6 RID: 7350
		Minus,
		// Token: 0x04001CB7 RID: 7351
		Multiply,
		// Token: 0x04001CB8 RID: 7352
		Divide,
		// Token: 0x04001CB9 RID: 7353
		Modulus,
		// Token: 0x04001CBA RID: 7354
		UnaryMinus,
		// Token: 0x04001CBB RID: 7355
		UnaryPlus,
		// Token: 0x04001CBC RID: 7356
		Between,
		// Token: 0x04001CBD RID: 7357
		NotBetween
	}
}
