using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000CA RID: 202
	[ImmutableObject(true)]
	public sealed class SpanishPosTagSet : PosTagSet
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000723B File Offset: 0x0000543B
		private SpanishPosTagSet()
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00007243 File Offset: 0x00005443
		public override bool IsNoun(PosTagKind kind)
		{
			return kind.HasFeature(SpanishPosTagSet.POSMask, SpanishPosTagSet.Noun);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00007256 File Offset: 0x00005456
		public override bool IsVerb(PosTagKind kind)
		{
			return kind.HasFeature(SpanishPosTagSet.POSMask, SpanishPosTagSet.Verb);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00007269 File Offset: 0x00005469
		public override bool IsModal(PosTagKind kind)
		{
			return false;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000726C File Offset: 0x0000546C
		public override bool IsAdjective(PosTagKind kind)
		{
			return kind.HasFeature(SpanishPosTagSet.POSMask, SpanishPosTagSet.Adj);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000727F File Offset: 0x0000547F
		public override bool IsAdverb(PosTagKind kind)
		{
			return kind.HasFeature(SpanishPosTagSet.POSMask, SpanishPosTagSet.Adv);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00007292 File Offset: 0x00005492
		public override bool IsPreposition(PosTagKind kind)
		{
			return kind.HasFeature(SpanishPosTagSet.POSMask, SpanishPosTagSet.Prep);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000072A5 File Offset: 0x000054A5
		public override bool IsNounPlural(PosTagKind kind)
		{
			return kind.HasFeature(SpanishPosTagSet.NounPluralMask, SpanishPosTagSet.NounPluralValue);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000072B8 File Offset: 0x000054B8
		public override Func<PosTagKind, bool> IsNonNoun()
		{
			return SpanishPosTagSet._isNonNoun;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000072BF File Offset: 0x000054BF
		public override Func<StemmerSuggestion, bool> StemIsNoun()
		{
			return SpanishPosTagSet._stemIsNoun;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000072C6 File Offset: 0x000054C6
		public override Func<StemmerSuggestion, bool> StemIsNounPlural()
		{
			return SpanishPosTagSet._stemIsNounPlural;
		}

		// Token: 0x0400042D RID: 1069
		public static readonly PosTagKind POSMask = new PosTagKind(15L);

		// Token: 0x0400042E RID: 1070
		public static readonly PosTagKind Abbrev = new PosTagKind(0L);

		// Token: 0x0400042F RID: 1071
		public static readonly PosTagKind Adj = new PosTagKind(1L);

		// Token: 0x04000430 RID: 1072
		public static readonly PosTagKind AdjAllomorphMask = new PosTagKind(16L);

		// Token: 0x04000431 RID: 1073
		public static readonly PosTagKind AdjDefinitenessMask = new PosTagKind(96L);

		// Token: 0x04000432 RID: 1074
		public static readonly PosTagKind AdjDetTypeMask = new PosTagKind(896L);

		// Token: 0x04000433 RID: 1075
		public static readonly PosTagKind AdjAdjFormMask = new PosTagKind(3072L);

		// Token: 0x04000434 RID: 1076
		public static readonly PosTagKind AdjSuperl = new PosTagKind(2048L);

		// Token: 0x04000435 RID: 1077
		public static readonly PosTagKind AdjGenderMask = new PosTagKind(28672L);

		// Token: 0x04000436 RID: 1078
		public static readonly PosTagKind AdjFem = new PosTagKind(8192L);

		// Token: 0x04000437 RID: 1079
		public static readonly PosTagKind AdjNumberMask = new PosTagKind(98304L);

		// Token: 0x04000438 RID: 1080
		public static readonly PosTagKind AdjPlur = new PosTagKind(65536L);

		// Token: 0x04000439 RID: 1081
		public static readonly PosTagKind AdjPronTypeMask = new PosTagKind(393216L);

		// Token: 0x0400043A RID: 1082
		public static readonly PosTagKind AdjMasc = new PosTagKind(4096L);

		// Token: 0x0400043B RID: 1083
		public static readonly PosTagKind AdjSing = new PosTagKind(32768L);

		// Token: 0x0400043C RID: 1084
		public static readonly PosTagKind AdjApoc = new PosTagKind(1024L);

		// Token: 0x0400043D RID: 1085
		public static readonly PosTagKind AdjIndef = new PosTagKind(64L);

		// Token: 0x0400043E RID: 1086
		public static readonly PosTagKind AdjDet = new PosTagKind(131072L);

		// Token: 0x0400043F RID: 1087
		public static readonly PosTagKind AdjDef = new PosTagKind(32L);

		// Token: 0x04000440 RID: 1088
		public static readonly PosTagKind AdjDem = new PosTagKind(128L);

		// Token: 0x04000441 RID: 1089
		public static readonly PosTagKind AdjNeut = new PosTagKind(12288L);

		// Token: 0x04000442 RID: 1090
		public static readonly PosTagKind AdjNum = new PosTagKind(256L);

		// Token: 0x04000443 RID: 1091
		public static readonly PosTagKind AdjPoss = new PosTagKind(384L);

		// Token: 0x04000444 RID: 1092
		public static readonly PosTagKind AdjWh = new PosTagKind(512L);

		// Token: 0x04000445 RID: 1093
		public static readonly PosTagKind AdjAllomorph = new PosTagKind(16L);

		// Token: 0x04000446 RID: 1094
		public static readonly PosTagKind Adv = new PosTagKind(2L);

		// Token: 0x04000447 RID: 1095
		public static readonly PosTagKind Conj = new PosTagKind(3L);

		// Token: 0x04000448 RID: 1096
		public static readonly PosTagKind Contr = new PosTagKind(4L);

		// Token: 0x04000449 RID: 1097
		public static readonly PosTagKind ContrGenderMask = new PosTagKind(112L);

		// Token: 0x0400044A RID: 1098
		public static readonly PosTagKind ContrMasc = new PosTagKind(16L);

		// Token: 0x0400044B RID: 1099
		public static readonly PosTagKind ContrNumberMask = new PosTagKind(384L);

		// Token: 0x0400044C RID: 1100
		public static readonly PosTagKind ContrSing = new PosTagKind(128L);

		// Token: 0x0400044D RID: 1101
		public static readonly PosTagKind Ij = new PosTagKind(5L);

		// Token: 0x0400044E RID: 1102
		public static readonly PosTagKind Noun = new PosTagKind(6L);

		// Token: 0x0400044F RID: 1103
		public static readonly PosTagKind NounGenderMask = new PosTagKind(112L);

		// Token: 0x04000450 RID: 1104
		public static readonly PosTagKind NounFem = new PosTagKind(32L);

		// Token: 0x04000451 RID: 1105
		public static readonly PosTagKind NounNumberMask = new PosTagKind(384L);

		// Token: 0x04000452 RID: 1106
		public static readonly PosTagKind NounSing = new PosTagKind(128L);

		// Token: 0x04000453 RID: 1107
		public static readonly PosTagKind NounNounTypeMask = new PosTagKind(512L);

		// Token: 0x04000454 RID: 1108
		public static readonly PosTagKind NounMasc = new PosTagKind(16L);

		// Token: 0x04000455 RID: 1109
		public static readonly PosTagKind NounUnknown = new PosTagKind(64L);

		// Token: 0x04000456 RID: 1110
		public static readonly PosTagKind NounPlur = new PosTagKind(256L);

		// Token: 0x04000457 RID: 1111
		public static readonly PosTagKind NounProper = new PosTagKind(512L);

		// Token: 0x04000458 RID: 1112
		public static readonly PosTagKind NounFemMasc = new PosTagKind(80L);

		// Token: 0x04000459 RID: 1113
		public static readonly PosTagKind NounNeut = new PosTagKind(48L);

		// Token: 0x0400045A RID: 1114
		public static readonly PosTagKind Prep = new PosTagKind(7L);

		// Token: 0x0400045B RID: 1115
		public static readonly PosTagKind Pron = new PosTagKind(8L);

		// Token: 0x0400045C RID: 1116
		public static readonly PosTagKind PronAdjFormMask = new PosTagKind(48L);

		// Token: 0x0400045D RID: 1117
		public static readonly PosTagKind PronCaseMask = new PosTagKind(960L);

		// Token: 0x0400045E RID: 1118
		public static readonly PosTagKind PronDefinitenessMask = new PosTagKind(3072L);

		// Token: 0x0400045F RID: 1119
		public static readonly PosTagKind PronDetTypeMask = new PosTagKind(28672L);

		// Token: 0x04000460 RID: 1120
		public static readonly PosTagKind PronEncliticCaseMask = new PosTagKind(491520L);

		// Token: 0x04000461 RID: 1121
		public static readonly PosTagKind PronEncliticGenderMask = new PosTagKind(3670016L);

		// Token: 0x04000462 RID: 1122
		public static readonly PosTagKind PronEncliticNumberMask = new PosTagKind(12582912L);

		// Token: 0x04000463 RID: 1123
		public static readonly PosTagKind PronEncliticPersonMask = new PosTagKind(50331648L);

		// Token: 0x04000464 RID: 1124
		public static readonly PosTagKind PronEnclitic2CaseMask = new PosTagKind(1006632960L);

		// Token: 0x04000465 RID: 1125
		public static readonly PosTagKind PronEnclitic2GenderMask = new PosTagKind(7516192768L);

		// Token: 0x04000466 RID: 1126
		public static readonly PosTagKind PronEnclitic2NumberMask = new PosTagKind(25769803776L);

		// Token: 0x04000467 RID: 1127
		public static readonly PosTagKind PronEnclitic2PersonMask = new PosTagKind(103079215104L);

		// Token: 0x04000468 RID: 1128
		public static readonly PosTagKind PronGenderMask = new PosTagKind(962072674304L);

		// Token: 0x04000469 RID: 1129
		public static readonly PosTagKind PronNumberMask = new PosTagKind(3298534883328L);

		// Token: 0x0400046A RID: 1130
		public static readonly PosTagKind PronSing = new PosTagKind(1099511627776L);

		// Token: 0x0400046B RID: 1131
		public static readonly PosTagKind PronPersonMask = new PosTagKind(13194139533312L);

		// Token: 0x0400046C RID: 1132
		public static readonly PosTagKind PronPolarityMask = new PosTagKind(17592186044416L);

		// Token: 0x0400046D RID: 1133
		public static readonly PosTagKind PronPolitenessMask = new PosTagKind(105553116266496L);

		// Token: 0x0400046E RID: 1134
		public static readonly PosTagKind PronPossessorMask = new PosTagKind(985162418487296L);

		// Token: 0x0400046F RID: 1135
		public static readonly PosTagKind PronPronTypeMask = new PosTagKind(3377699720527872L);

		// Token: 0x04000470 RID: 1136
		public static readonly PosTagKind PronAbbrev = new PosTagKind(3377699720527872L);

		// Token: 0x04000471 RID: 1137
		public static readonly PosTagKind PronDem = new PosTagKind(4096L);

		// Token: 0x04000472 RID: 1138
		public static readonly PosTagKind PronFem = new PosTagKind(274877906944L);

		// Token: 0x04000473 RID: 1139
		public static readonly PosTagKind PronNeut = new PosTagKind(412316860416L);

		// Token: 0x04000474 RID: 1140
		public static readonly PosTagKind PronMasc = new PosTagKind(137438953472L);

		// Token: 0x04000475 RID: 1141
		public static readonly PosTagKind PronPlur = new PosTagKind(2199023255552L);

		// Token: 0x04000476 RID: 1142
		public static readonly PosTagKind PronApoc = new PosTagKind(16L);

		// Token: 0x04000477 RID: 1143
		public static readonly PosTagKind PronIndef = new PosTagKind(2048L);

		// Token: 0x04000478 RID: 1144
		public static readonly PosTagKind PronNeg = new PosTagKind(17592186044416L);

		// Token: 0x04000479 RID: 1145
		public static readonly PosTagKind PronNum = new PosTagKind(8192L);

		// Token: 0x0400047A RID: 1146
		public static readonly PosTagKind PronAcc = new PosTagKind(128L);

		// Token: 0x0400047B RID: 1147
		public static readonly PosTagKind PronPers1 = new PosTagKind(4398046511104L);

		// Token: 0x0400047C RID: 1148
		public static readonly PosTagKind PronDat = new PosTagKind(192L);

		// Token: 0x0400047D RID: 1149
		public static readonly PosTagKind PronNom = new PosTagKind(64L);

		// Token: 0x0400047E RID: 1150
		public static readonly PosTagKind PronReflex = new PosTagKind(320L);

		// Token: 0x0400047F RID: 1151
		public static readonly PosTagKind PronObl = new PosTagKind(256L);

		// Token: 0x04000480 RID: 1152
		public static readonly PosTagKind PronOblCon = new PosTagKind(384L);

		// Token: 0x04000481 RID: 1153
		public static readonly PosTagKind PronPers2 = new PosTagKind(8796093022208L);

		// Token: 0x04000482 RID: 1154
		public static readonly PosTagKind PronFormal = new PosTagKind(35184372088832L);

		// Token: 0x04000483 RID: 1155
		public static readonly PosTagKind PronVoseo = new PosTagKind(70368744177664L);

		// Token: 0x04000484 RID: 1156
		public static readonly PosTagKind PronPers3 = new PosTagKind(13194139533312L);

		// Token: 0x04000485 RID: 1157
		public static readonly PosTagKind PronPoss = new PosTagKind(12288L);

		// Token: 0x04000486 RID: 1158
		public static readonly PosTagKind PronPers1Plur = new PosTagKind(562949953421312L);

		// Token: 0x04000487 RID: 1159
		public static readonly PosTagKind PronPers1Sing = new PosTagKind(140737488355328L);

		// Token: 0x04000488 RID: 1160
		public static readonly PosTagKind PronPers2Plur = new PosTagKind(703687441776640L);

		// Token: 0x04000489 RID: 1161
		public static readonly PosTagKind PronPers2Sing = new PosTagKind(281474976710656L);

		// Token: 0x0400048A RID: 1162
		public static readonly PosTagKind PronPers3Plur = new PosTagKind(844424930131968L);

		// Token: 0x0400048B RID: 1163
		public static readonly PosTagKind PronPers3Sing = new PosTagKind(422212465065984L);

		// Token: 0x0400048C RID: 1164
		public static readonly PosTagKind PronRel = new PosTagKind(2251799813685248L);

		// Token: 0x0400048D RID: 1165
		public static readonly PosTagKind PronWh = new PosTagKind(16384L);

		// Token: 0x0400048E RID: 1166
		public static readonly PosTagKind Verb = new PosTagKind(9L);

		// Token: 0x0400048F RID: 1167
		public static readonly PosTagKind VerbEncliticCaseMask = new PosTagKind(240L);

		// Token: 0x04000490 RID: 1168
		public static readonly PosTagKind VerbEncliticGenderMask = new PosTagKind(1792L);

		// Token: 0x04000491 RID: 1169
		public static readonly PosTagKind VerbEncliticNumberMask = new PosTagKind(6144L);

		// Token: 0x04000492 RID: 1170
		public static readonly PosTagKind VerbEncliticPersonMask = new PosTagKind(24576L);

		// Token: 0x04000493 RID: 1171
		public static readonly PosTagKind VerbEnclitic2CaseMask = new PosTagKind(491520L);

		// Token: 0x04000494 RID: 1172
		public static readonly PosTagKind VerbEnclitic2GenderMask = new PosTagKind(3670016L);

		// Token: 0x04000495 RID: 1173
		public static readonly PosTagKind VerbEnclitic2NumberMask = new PosTagKind(12582912L);

		// Token: 0x04000496 RID: 1174
		public static readonly PosTagKind VerbEnclitic2PersonMask = new PosTagKind(50331648L);

		// Token: 0x04000497 RID: 1175
		public static readonly PosTagKind VerbGenderMask = new PosTagKind(469762048L);

		// Token: 0x04000498 RID: 1176
		public static readonly PosTagKind VerbMoodMask = new PosTagKind((long)((ulong)(-536870912)));

		// Token: 0x04000499 RID: 1177
		public static readonly PosTagKind VerbNonTenseMask = new PosTagKind(30064771072L);

		// Token: 0x0400049A RID: 1178
		public static readonly PosTagKind VerbInf = new PosTagKind(4294967296L);

		// Token: 0x0400049B RID: 1179
		public static readonly PosTagKind VerbNumberMask = new PosTagKind(103079215104L);

		// Token: 0x0400049C RID: 1180
		public static readonly PosTagKind VerbPersonMask = new PosTagKind(412316860416L);

		// Token: 0x0400049D RID: 1181
		public static readonly PosTagKind VerbPolitenessMask = new PosTagKind(1649267441664L);

		// Token: 0x0400049E RID: 1182
		public static readonly PosTagKind VerbTenseAspectMask = new PosTagKind(15393162788864L);

		// Token: 0x0400049F RID: 1183
		public static readonly PosTagKind VerbEncliticReflex = new PosTagKind(80L);

		// Token: 0x040004A0 RID: 1184
		public static readonly PosTagKind VerbEncliticPlur = new PosTagKind(4096L);

		// Token: 0x040004A1 RID: 1185
		public static readonly PosTagKind VerbEncliticPers2 = new PosTagKind(16384L);

		// Token: 0x040004A2 RID: 1186
		public static readonly PosTagKind VerbEnclitic2Dat = new PosTagKind(98304L);

		// Token: 0x040004A3 RID: 1187
		public static readonly PosTagKind VerbEnclitic2Plur = new PosTagKind(8388608L);

		// Token: 0x040004A4 RID: 1188
		public static readonly PosTagKind VerbEnclitic2Pers3 = new PosTagKind(50331648L);

		// Token: 0x040004A5 RID: 1189
		public static readonly PosTagKind VerbEncliticDatReflex = new PosTagKind(112L);

		// Token: 0x040004A6 RID: 1190
		public static readonly PosTagKind VerbEnclitic2Acc = new PosTagKind(65536L);

		// Token: 0x040004A7 RID: 1191
		public static readonly PosTagKind VerbEnclitic2Fem = new PosTagKind(1048576L);

		// Token: 0x040004A8 RID: 1192
		public static readonly PosTagKind VerbEnclitic2Masc = new PosTagKind(524288L);

		// Token: 0x040004A9 RID: 1193
		public static readonly PosTagKind VerbEnclitic2Sing = new PosTagKind(4194304L);

		// Token: 0x040004AA RID: 1194
		public static readonly PosTagKind VerbEncliticPers1 = new PosTagKind(8192L);

		// Token: 0x040004AB RID: 1195
		public static readonly PosTagKind VerbEncliticPers3 = new PosTagKind(24576L);

		// Token: 0x040004AC RID: 1196
		public static readonly PosTagKind VerbEncliticSing = new PosTagKind(2048L);

		// Token: 0x040004AD RID: 1197
		public static readonly PosTagKind VerbEnclitic2Pers2 = new PosTagKind(33554432L);

		// Token: 0x040004AE RID: 1198
		public static readonly PosTagKind VerbEnclitic2Pers1 = new PosTagKind(16777216L);

		// Token: 0x040004AF RID: 1199
		public static readonly PosTagKind VerbEncliticDat = new PosTagKind(48L);

		// Token: 0x040004B0 RID: 1200
		public static readonly PosTagKind VerbEncliticAcc = new PosTagKind(32L);

		// Token: 0x040004B1 RID: 1201
		public static readonly PosTagKind VerbEncliticFem = new PosTagKind(512L);

		// Token: 0x040004B2 RID: 1202
		public static readonly PosTagKind VerbEncliticMasc = new PosTagKind(256L);

		// Token: 0x040004B3 RID: 1203
		public static readonly PosTagKind VerbEncliticAccDatReflex = new PosTagKind(128L);

		// Token: 0x040004B4 RID: 1204
		public static readonly PosTagKind VerbSubj = new PosTagKind(1073741824L);

		// Token: 0x040004B5 RID: 1205
		public static readonly PosTagKind VerbSing = new PosTagKind(34359738368L);

		// Token: 0x040004B6 RID: 1206
		public static readonly PosTagKind VerbPers1 = new PosTagKind(137438953472L);

		// Token: 0x040004B7 RID: 1207
		public static readonly PosTagKind VerbFut = new PosTagKind(6597069766656L);

		// Token: 0x040004B8 RID: 1208
		public static readonly PosTagKind VerbPers2 = new PosTagKind(274877906944L);

		// Token: 0x040004B9 RID: 1209
		public static readonly PosTagKind VerbPers3 = new PosTagKind(412316860416L);

		// Token: 0x040004BA RID: 1210
		public static readonly PosTagKind VerbPlur = new PosTagKind(68719476736L);

		// Token: 0x040004BB RID: 1211
		public static readonly PosTagKind VerbIndic = new PosTagKind(536870912L);

		// Token: 0x040004BC RID: 1212
		public static readonly PosTagKind VerbPast = new PosTagKind(4398046511104L);

		// Token: 0x040004BD RID: 1213
		public static readonly PosTagKind VerbImp = new PosTagKind(8796093022208L);

		// Token: 0x040004BE RID: 1214
		public static readonly PosTagKind VerbPres = new PosTagKind(2199023255552L);

		// Token: 0x040004BF RID: 1215
		public static readonly PosTagKind VerbVoseo = new PosTagKind(1099511627776L);

		// Token: 0x040004C0 RID: 1216
		public static readonly PosTagKind VerbCond = new PosTagKind(1610612736L);

		// Token: 0x040004C1 RID: 1217
		public static readonly PosTagKind VerbImper = new PosTagKind((long)((ulong)int.MinValue));

		// Token: 0x040004C2 RID: 1218
		public static readonly PosTagKind VerbFormal = new PosTagKind(549755813888L);

		// Token: 0x040004C3 RID: 1219
		public static readonly PosTagKind VerbGer = new PosTagKind(12884901888L);

		// Token: 0x040004C4 RID: 1220
		public static readonly PosTagKind VerbMasc = new PosTagKind(67108864L);

		// Token: 0x040004C5 RID: 1221
		public static readonly PosTagKind VerbPastpart = new PosTagKind(8589934592L);

		// Token: 0x040004C6 RID: 1222
		public static readonly PosTagKind VerbFem = new PosTagKind(134217728L);

		// Token: 0x040004C7 RID: 1223
		public static readonly PosTagKind VerbImpers = new PosTagKind(17179869184L);

		// Token: 0x040004C8 RID: 1224
		private static readonly PosTagKind NounPluralMask = new PosTagKind(SpanishPosTagSet.Noun.Value | SpanishPosTagSet.NounNumberMask.Value);

		// Token: 0x040004C9 RID: 1225
		private static readonly PosTagKind NounPluralValue = new PosTagKind(SpanishPosTagSet.Noun.Value | SpanishPosTagSet.NounPlur.Value);

		// Token: 0x040004CA RID: 1226
		private static readonly Func<PosTagKind, bool> _isNonNoun = (PosTagKind x) => !SpanishPosTagSet.Instance.IsNoun(x);

		// Token: 0x040004CB RID: 1227
		private static readonly Func<StemmerSuggestion, bool> _stemIsNoun = (StemmerSuggestion x) => SpanishPosTagSet.Instance.IsNoun(x.PosTagKind);

		// Token: 0x040004CC RID: 1228
		private static readonly Func<StemmerSuggestion, bool> _stemIsNounPlural = (StemmerSuggestion x) => SpanishPosTagSet.Instance.IsNounPlural(x.PosTagKind);

		// Token: 0x040004CD RID: 1229
		public static readonly SpanishPosTagSet Instance = new SpanishPosTagSet();
	}
}
