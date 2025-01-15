using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000838 RID: 2104
	public enum SQLType
	{
		// Token: 0x04002EF5 RID: 12021
		None,
		// Token: 0x04002EF6 RID: 12022
		Date = 384,
		// Token: 0x04002EF7 RID: 12023
		NullableDate,
		// Token: 0x04002EF8 RID: 12024
		Time = 388,
		// Token: 0x04002EF9 RID: 12025
		NullableTime,
		// Token: 0x04002EFA RID: 12026
		Timestamp = 392,
		// Token: 0x04002EFB RID: 12027
		NullableTimestamp,
		// Token: 0x04002EFC RID: 12028
		BLOB = 404,
		// Token: 0x04002EFD RID: 12029
		NullableBLOB,
		// Token: 0x04002EFE RID: 12030
		CLOB = 408,
		// Token: 0x04002EFF RID: 12031
		NullableCLOB,
		// Token: 0x04002F00 RID: 12032
		CLOB_DBCS = 412,
		// Token: 0x04002F01 RID: 12033
		NullableCLOB_DBCS,
		// Token: 0x04002F02 RID: 12034
		VarChar = 448,
		// Token: 0x04002F03 RID: 12035
		NullableVarChar,
		// Token: 0x04002F04 RID: 12036
		VarByte = 448,
		// Token: 0x04002F05 RID: 12037
		NullableVarByte,
		// Token: 0x04002F06 RID: 12038
		VarCharacter = 448,
		// Token: 0x04002F07 RID: 12039
		NullableVarCharacter,
		// Token: 0x04002F08 RID: 12040
		VarCharForSBCS = 448,
		// Token: 0x04002F09 RID: 12041
		NullableVarCharForSBCS,
		// Token: 0x04002F0A RID: 12042
		CharacterVaryingForSBCS = 448,
		// Token: 0x04002F0B RID: 12043
		NullableCharacterVaryingForSBCS,
		// Token: 0x04002F0C RID: 12044
		Char = 452,
		// Token: 0x04002F0D RID: 12045
		NullableChar,
		// Token: 0x04002F0E RID: 12046
		Character = 452,
		// Token: 0x04002F0F RID: 12047
		NullableCharacter,
		// Token: 0x04002F10 RID: 12048
		FixedByte = 452,
		// Token: 0x04002F11 RID: 12049
		NullableFixedByte,
		// Token: 0x04002F12 RID: 12050
		FixedCharSBCS = 452,
		// Token: 0x04002F13 RID: 12051
		NullableFixedCharSBCS,
		// Token: 0x04002F14 RID: 12052
		CharForSBCS = 452,
		// Token: 0x04002F15 RID: 12053
		NullableCharForSBCS,
		// Token: 0x04002F16 RID: 12054
		CharacterForSBCS = 452,
		// Token: 0x04002F17 RID: 12055
		NullableCharacterForSBCS,
		// Token: 0x04002F18 RID: 12056
		LongVarChar = 456,
		// Token: 0x04002F19 RID: 12057
		NullableLongVarChar,
		// Token: 0x04002F1A RID: 12058
		LongVarCharSBCS = 456,
		// Token: 0x04002F1B RID: 12059
		NullableLongVarCharSBCS,
		// Token: 0x04002F1C RID: 12060
		NullTerminatedChar = 460,
		// Token: 0x04002F1D RID: 12061
		NullableNullTerminatedChar,
		// Token: 0x04002F1E RID: 12062
		VarGraphic = 464,
		// Token: 0x04002F1F RID: 12063
		NullableVarGraphic,
		// Token: 0x04002F20 RID: 12064
		GraphicVarying = 464,
		// Token: 0x04002F21 RID: 12065
		NullableGraphicVarying,
		// Token: 0x04002F22 RID: 12066
		Graphic = 468,
		// Token: 0x04002F23 RID: 12067
		NullableGraphic,
		// Token: 0x04002F24 RID: 12068
		LongVarGraphic = 472,
		// Token: 0x04002F25 RID: 12069
		NullableLongVarGraphic,
		// Token: 0x04002F26 RID: 12070
		Float = 480,
		// Token: 0x04002F27 RID: 12071
		NullableFloat,
		// Token: 0x04002F28 RID: 12072
		Real = 480,
		// Token: 0x04002F29 RID: 12073
		NullableReal,
		// Token: 0x04002F2A RID: 12074
		Double = 480,
		// Token: 0x04002F2B RID: 12075
		NullableDouble,
		// Token: 0x04002F2C RID: 12076
		Decimal = 484,
		// Token: 0x04002F2D RID: 12077
		NullableDecimal,
		// Token: 0x04002F2E RID: 12078
		ZonedDecimal = 488,
		// Token: 0x04002F2F RID: 12079
		NullableZonedDecimal,
		// Token: 0x04002F30 RID: 12080
		BigInt = 492,
		// Token: 0x04002F31 RID: 12081
		NullableBigInt,
		// Token: 0x04002F32 RID: 12082
		Int = 496,
		// Token: 0x04002F33 RID: 12083
		NullableInt,
		// Token: 0x04002F34 RID: 12084
		Integer = 496,
		// Token: 0x04002F35 RID: 12085
		NullableInteger,
		// Token: 0x04002F36 RID: 12086
		SmallInt = 500,
		// Token: 0x04002F37 RID: 12087
		NullableSmallInt,
		// Token: 0x04002F38 RID: 12088
		Numeric = 504,
		// Token: 0x04002F39 RID: 12089
		NullableNumeric,
		// Token: 0x04002F3A RID: 12090
		TinyInt = -6,
		// Token: 0x04002F3B RID: 12091
		Bool = 2436,
		// Token: 0x04002F3C RID: 12092
		NBool,
		// Token: 0x04002F3D RID: 12093
		BinaryString = 912,
		// Token: 0x04002F3E RID: 12094
		NullableBinaryString,
		// Token: 0x04002F3F RID: 12095
		CharForBitData = 912,
		// Token: 0x04002F40 RID: 12096
		NullableCharForBitData,
		// Token: 0x04002F41 RID: 12097
		VarBinaryString = 908,
		// Token: 0x04002F42 RID: 12098
		NullableVarBinaryString,
		// Token: 0x04002F43 RID: 12099
		VarCharForBitData = 908,
		// Token: 0x04002F44 RID: 12100
		NullableVarCharForBitData,
		// Token: 0x04002F45 RID: 12101
		LongVarCharForBitData = 908,
		// Token: 0x04002F46 RID: 12102
		NullableLongCharForBitData,
		// Token: 0x04002F47 RID: 12103
		ROWID = 904,
		// Token: 0x04002F48 RID: 12104
		NullableROWID,
		// Token: 0x04002F49 RID: 12105
		DecFloat = 996,
		// Token: 0x04002F4A RID: 12106
		NullableDecFloat
	}
}
