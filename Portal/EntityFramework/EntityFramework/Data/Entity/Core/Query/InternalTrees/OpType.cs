using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C9 RID: 969
	internal enum OpType
	{
		// Token: 0x04000F67 RID: 3943
		Constant,
		// Token: 0x04000F68 RID: 3944
		InternalConstant,
		// Token: 0x04000F69 RID: 3945
		NullSentinel,
		// Token: 0x04000F6A RID: 3946
		Null,
		// Token: 0x04000F6B RID: 3947
		ConstantPredicate,
		// Token: 0x04000F6C RID: 3948
		VarRef,
		// Token: 0x04000F6D RID: 3949
		GT,
		// Token: 0x04000F6E RID: 3950
		GE,
		// Token: 0x04000F6F RID: 3951
		LE,
		// Token: 0x04000F70 RID: 3952
		LT,
		// Token: 0x04000F71 RID: 3953
		EQ,
		// Token: 0x04000F72 RID: 3954
		NE,
		// Token: 0x04000F73 RID: 3955
		Like,
		// Token: 0x04000F74 RID: 3956
		Plus,
		// Token: 0x04000F75 RID: 3957
		Minus,
		// Token: 0x04000F76 RID: 3958
		Multiply,
		// Token: 0x04000F77 RID: 3959
		Divide,
		// Token: 0x04000F78 RID: 3960
		Modulo,
		// Token: 0x04000F79 RID: 3961
		UnaryMinus,
		// Token: 0x04000F7A RID: 3962
		And,
		// Token: 0x04000F7B RID: 3963
		Or,
		// Token: 0x04000F7C RID: 3964
		In,
		// Token: 0x04000F7D RID: 3965
		Not,
		// Token: 0x04000F7E RID: 3966
		IsNull,
		// Token: 0x04000F7F RID: 3967
		Case,
		// Token: 0x04000F80 RID: 3968
		Treat,
		// Token: 0x04000F81 RID: 3969
		IsOf,
		// Token: 0x04000F82 RID: 3970
		Cast,
		// Token: 0x04000F83 RID: 3971
		SoftCast,
		// Token: 0x04000F84 RID: 3972
		Aggregate,
		// Token: 0x04000F85 RID: 3973
		Function,
		// Token: 0x04000F86 RID: 3974
		RelProperty,
		// Token: 0x04000F87 RID: 3975
		Property,
		// Token: 0x04000F88 RID: 3976
		NewEntity,
		// Token: 0x04000F89 RID: 3977
		NewInstance,
		// Token: 0x04000F8A RID: 3978
		DiscriminatedNewEntity,
		// Token: 0x04000F8B RID: 3979
		NewMultiset,
		// Token: 0x04000F8C RID: 3980
		NewRecord,
		// Token: 0x04000F8D RID: 3981
		GetRefKey,
		// Token: 0x04000F8E RID: 3982
		GetEntityRef,
		// Token: 0x04000F8F RID: 3983
		Ref,
		// Token: 0x04000F90 RID: 3984
		Exists,
		// Token: 0x04000F91 RID: 3985
		Element,
		// Token: 0x04000F92 RID: 3986
		Collect,
		// Token: 0x04000F93 RID: 3987
		Deref,
		// Token: 0x04000F94 RID: 3988
		Navigate,
		// Token: 0x04000F95 RID: 3989
		ScanTable,
		// Token: 0x04000F96 RID: 3990
		ScanView,
		// Token: 0x04000F97 RID: 3991
		Filter,
		// Token: 0x04000F98 RID: 3992
		Project,
		// Token: 0x04000F99 RID: 3993
		InnerJoin,
		// Token: 0x04000F9A RID: 3994
		LeftOuterJoin,
		// Token: 0x04000F9B RID: 3995
		FullOuterJoin,
		// Token: 0x04000F9C RID: 3996
		CrossJoin,
		// Token: 0x04000F9D RID: 3997
		CrossApply,
		// Token: 0x04000F9E RID: 3998
		OuterApply,
		// Token: 0x04000F9F RID: 3999
		Unnest,
		// Token: 0x04000FA0 RID: 4000
		Sort,
		// Token: 0x04000FA1 RID: 4001
		ConstrainedSort,
		// Token: 0x04000FA2 RID: 4002
		GroupBy,
		// Token: 0x04000FA3 RID: 4003
		GroupByInto,
		// Token: 0x04000FA4 RID: 4004
		UnionAll,
		// Token: 0x04000FA5 RID: 4005
		Intersect,
		// Token: 0x04000FA6 RID: 4006
		Except,
		// Token: 0x04000FA7 RID: 4007
		Distinct,
		// Token: 0x04000FA8 RID: 4008
		SingleRow,
		// Token: 0x04000FA9 RID: 4009
		SingleRowTable,
		// Token: 0x04000FAA RID: 4010
		VarDef,
		// Token: 0x04000FAB RID: 4011
		VarDefList,
		// Token: 0x04000FAC RID: 4012
		Leaf,
		// Token: 0x04000FAD RID: 4013
		PhysicalProject,
		// Token: 0x04000FAE RID: 4014
		SingleStreamNest,
		// Token: 0x04000FAF RID: 4015
		MultiStreamNest,
		// Token: 0x04000FB0 RID: 4016
		MaxMarker,
		// Token: 0x04000FB1 RID: 4017
		NotValid = 73
	}
}
