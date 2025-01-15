using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B1 RID: 177
	public enum FunctionName : ushort
	{
		// Token: 0x04000406 RID: 1030
		Add,
		// Token: 0x04000407 RID: 1031
		Subtract,
		// Token: 0x04000408 RID: 1032
		Multiply,
		// Token: 0x04000409 RID: 1033
		Divide,
		// Token: 0x0400040A RID: 1034
		Negate,
		// Token: 0x0400040B RID: 1035
		Mod,
		// Token: 0x0400040C RID: 1036
		Power,
		// Token: 0x0400040D RID: 1037
		Equals,
		// Token: 0x0400040E RID: 1038
		NotEquals,
		// Token: 0x0400040F RID: 1039
		GreaterThan,
		// Token: 0x04000410 RID: 1040
		GreaterThanOrEquals,
		// Token: 0x04000411 RID: 1041
		LessThan,
		// Token: 0x04000412 RID: 1042
		LessThanOrEquals,
		// Token: 0x04000413 RID: 1043
		And,
		// Token: 0x04000414 RID: 1044
		Or,
		// Token: 0x04000415 RID: 1045
		Not,
		// Token: 0x04000416 RID: 1046
		Truncate,
		// Token: 0x04000417 RID: 1047
		Round,
		// Token: 0x04000418 RID: 1048
		Integer,
		// Token: 0x04000419 RID: 1049
		Decimal,
		// Token: 0x0400041A RID: 1050
		Float,
		// Token: 0x0400041B RID: 1051
		String,
		// Token: 0x0400041C RID: 1052
		Length,
		// Token: 0x0400041D RID: 1053
		Find,
		// Token: 0x0400041E RID: 1054
		Substring,
		// Token: 0x0400041F RID: 1055
		Left,
		// Token: 0x04000420 RID: 1056
		Right,
		// Token: 0x04000421 RID: 1057
		Concat,
		// Token: 0x04000422 RID: 1058
		Lower,
		// Token: 0x04000423 RID: 1059
		Upper,
		// Token: 0x04000424 RID: 1060
		LTrim,
		// Token: 0x04000425 RID: 1061
		RTrim,
		// Token: 0x04000426 RID: 1062
		Replace,
		// Token: 0x04000427 RID: 1063
		Date,
		// Token: 0x04000428 RID: 1064
		Time = 67,
		// Token: 0x04000429 RID: 1065
		DateTime = 34,
		// Token: 0x0400042A RID: 1066
		Year,
		// Token: 0x0400042B RID: 1067
		Quarter,
		// Token: 0x0400042C RID: 1068
		Month,
		// Token: 0x0400042D RID: 1069
		Day,
		// Token: 0x0400042E RID: 1070
		Hour,
		// Token: 0x0400042F RID: 1071
		Minute,
		// Token: 0x04000430 RID: 1072
		Second,
		// Token: 0x04000431 RID: 1073
		DayOfYear,
		// Token: 0x04000432 RID: 1074
		Week,
		// Token: 0x04000433 RID: 1075
		DayOfWeek,
		// Token: 0x04000434 RID: 1076
		Now,
		// Token: 0x04000435 RID: 1077
		Today,
		// Token: 0x04000436 RID: 1078
		DateDiff,
		// Token: 0x04000437 RID: 1079
		DateAdd,
		// Token: 0x04000438 RID: 1080
		Sum,
		// Token: 0x04000439 RID: 1081
		Avg,
		// Token: 0x0400043A RID: 1082
		Max,
		// Token: 0x0400043B RID: 1083
		Min,
		// Token: 0x0400043C RID: 1084
		Count,
		// Token: 0x0400043D RID: 1085
		CountDistinct,
		// Token: 0x0400043E RID: 1086
		StDev,
		// Token: 0x0400043F RID: 1087
		StDevP,
		// Token: 0x04000440 RID: 1088
		Var,
		// Token: 0x04000441 RID: 1089
		VarP,
		// Token: 0x04000442 RID: 1090
		If,
		// Token: 0x04000443 RID: 1091
		Switch,
		// Token: 0x04000444 RID: 1092
		In,
		// Token: 0x04000445 RID: 1093
		Filter,
		// Token: 0x04000446 RID: 1094
		Evaluate,
		// Token: 0x04000447 RID: 1095
		Aggregate,
		// Token: 0x04000448 RID: 1096
		GetUserID,
		// Token: 0x04000449 RID: 1097
		GetUserCulture
	}
}
