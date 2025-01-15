using System;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B7 RID: 1719
	public enum DbExpressionKind
	{
		// Token: 0x04001D50 RID: 7504
		All,
		// Token: 0x04001D51 RID: 7505
		And,
		// Token: 0x04001D52 RID: 7506
		Any,
		// Token: 0x04001D53 RID: 7507
		Case,
		// Token: 0x04001D54 RID: 7508
		Cast,
		// Token: 0x04001D55 RID: 7509
		Constant,
		// Token: 0x04001D56 RID: 7510
		CrossApply,
		// Token: 0x04001D57 RID: 7511
		CrossJoin,
		// Token: 0x04001D58 RID: 7512
		Deref,
		// Token: 0x04001D59 RID: 7513
		Distinct,
		// Token: 0x04001D5A RID: 7514
		Divide,
		// Token: 0x04001D5B RID: 7515
		Element,
		// Token: 0x04001D5C RID: 7516
		EntityRef,
		// Token: 0x04001D5D RID: 7517
		Equals,
		// Token: 0x04001D5E RID: 7518
		Except,
		// Token: 0x04001D5F RID: 7519
		Filter,
		// Token: 0x04001D60 RID: 7520
		FullOuterJoin,
		// Token: 0x04001D61 RID: 7521
		Function,
		// Token: 0x04001D62 RID: 7522
		GreaterThan,
		// Token: 0x04001D63 RID: 7523
		GreaterThanOrEquals,
		// Token: 0x04001D64 RID: 7524
		GroupBy,
		// Token: 0x04001D65 RID: 7525
		InnerJoin,
		// Token: 0x04001D66 RID: 7526
		Intersect,
		// Token: 0x04001D67 RID: 7527
		IsEmpty,
		// Token: 0x04001D68 RID: 7528
		IsNull,
		// Token: 0x04001D69 RID: 7529
		IsOf,
		// Token: 0x04001D6A RID: 7530
		IsOfOnly,
		// Token: 0x04001D6B RID: 7531
		LeftOuterJoin,
		// Token: 0x04001D6C RID: 7532
		LessThan,
		// Token: 0x04001D6D RID: 7533
		LessThanOrEquals,
		// Token: 0x04001D6E RID: 7534
		Like,
		// Token: 0x04001D6F RID: 7535
		Limit,
		// Token: 0x04001D70 RID: 7536
		Minus,
		// Token: 0x04001D71 RID: 7537
		Modulo,
		// Token: 0x04001D72 RID: 7538
		Multiply,
		// Token: 0x04001D73 RID: 7539
		NewInstance,
		// Token: 0x04001D74 RID: 7540
		Not,
		// Token: 0x04001D75 RID: 7541
		NotEquals,
		// Token: 0x04001D76 RID: 7542
		Null,
		// Token: 0x04001D77 RID: 7543
		OfType,
		// Token: 0x04001D78 RID: 7544
		OfTypeOnly,
		// Token: 0x04001D79 RID: 7545
		Or,
		// Token: 0x04001D7A RID: 7546
		OuterApply,
		// Token: 0x04001D7B RID: 7547
		ParameterReference,
		// Token: 0x04001D7C RID: 7548
		Plus,
		// Token: 0x04001D7D RID: 7549
		Project,
		// Token: 0x04001D7E RID: 7550
		Property,
		// Token: 0x04001D7F RID: 7551
		Ref,
		// Token: 0x04001D80 RID: 7552
		RefKey,
		// Token: 0x04001D81 RID: 7553
		RelationshipNavigation,
		// Token: 0x04001D82 RID: 7554
		Scan,
		// Token: 0x04001D83 RID: 7555
		Skip,
		// Token: 0x04001D84 RID: 7556
		Sort,
		// Token: 0x04001D85 RID: 7557
		Treat,
		// Token: 0x04001D86 RID: 7558
		UnaryMinus,
		// Token: 0x04001D87 RID: 7559
		UnionAll,
		// Token: 0x04001D88 RID: 7560
		VariableReference,
		// Token: 0x04001D89 RID: 7561
		Lambda,
		// Token: 0x04001D8A RID: 7562
		In
	}
}
