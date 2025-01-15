using System;
using System.Data.SqlTypes;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000063 RID: 99
	internal sealed class SqlCollation
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x00016D5A File Offset: 0x00014F5A
		public SqlCollation(uint info, byte sortId)
		{
			this._info = info;
			this._sortId = sortId;
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00016D70 File Offset: 0x00014F70
		internal int LCID
		{
			get
			{
				return (int)(this._info & 1048575U);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00016D80 File Offset: 0x00014F80
		internal SqlCompareOptions SqlCompareOptions
		{
			get
			{
				SqlCompareOptions sqlCompareOptions = SqlCompareOptions.None;
				if ((this._info & 1048576U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreCase;
				}
				if ((this._info & 2097152U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreNonSpace;
				}
				if ((this._info & 4194304U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreWidth;
				}
				if ((this._info & 8388608U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreKanaType;
				}
				if ((this._info & 16777216U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.BinarySort;
				}
				return sqlCompareOptions;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00016DEF File Offset: 0x00014FEF
		internal bool IsUTF8
		{
			get
			{
				return (this._info & 67108864U) == 67108864U;
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00016E04 File Offset: 0x00015004
		internal string TraceString()
		{
			return string.Format(null, "(LCID={0}, Opts={1})", this.LCID, (int)this.SqlCompareOptions);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00016E28 File Offset: 0x00015028
		private static int FirstSupportedCollationVersion(int lcid)
		{
			if (lcid <= 1157)
			{
				if (lcid <= 1093)
				{
					if (lcid <= 1047)
					{
						if (lcid == 1044)
						{
							return 2;
						}
						if (lcid == 1047)
						{
							return 2;
						}
					}
					else
					{
						if (lcid == 1056)
						{
							return 2;
						}
						switch (lcid)
						{
						case 1065:
							return 2;
						case 1066:
						case 1067:
						case 1069:
							break;
						case 1068:
							return 2;
						case 1070:
							return 2;
						case 1071:
							return 1;
						default:
							switch (lcid)
							{
							case 1081:
								return 1;
							case 1082:
								return 2;
							case 1083:
								return 2;
							case 1087:
								return 1;
							case 1090:
								return 2;
							case 1091:
								return 1;
							case 1092:
								return 1;
							case 1093:
								return 2;
							}
							break;
						}
					}
				}
				else if (lcid <= 1114)
				{
					switch (lcid)
					{
					case 1101:
						return 2;
					case 1102:
					case 1103:
					case 1104:
						break;
					case 1105:
						return 2;
					case 1106:
						return 2;
					case 1107:
						return 2;
					case 1108:
						return 2;
					default:
						if (lcid == 1114)
						{
							return 1;
						}
						break;
					}
				}
				else
				{
					switch (lcid)
					{
					case 1121:
						return 2;
					case 1122:
						return 2;
					case 1123:
						return 2;
					case 1124:
						break;
					case 1125:
						return 1;
					default:
						if (lcid == 1133)
						{
							return 2;
						}
						switch (lcid)
						{
						case 1146:
							return 2;
						case 1148:
							return 2;
						case 1150:
							return 2;
						case 1152:
							return 2;
						case 1153:
							return 2;
						case 1155:
							return 2;
						case 1157:
							return 2;
						}
						break;
					}
				}
			}
			else if (lcid <= 2143)
			{
				if (lcid <= 2074)
				{
					if (lcid == 1164)
					{
						return 2;
					}
					if (lcid == 2074)
					{
						return 2;
					}
				}
				else
				{
					if (lcid == 2092)
					{
						return 2;
					}
					if (lcid == 2107)
					{
						return 2;
					}
					if (lcid == 2143)
					{
						return 2;
					}
				}
			}
			else if (lcid <= 3098)
			{
				if (lcid == 3076)
				{
					return 1;
				}
				if (lcid == 3098)
				{
					return 2;
				}
			}
			else
			{
				if (lcid == 5124)
				{
					return 2;
				}
				if (lcid == 5146)
				{
					return 2;
				}
				if (lcid == 8218)
				{
					return 2;
				}
			}
			return 0;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00017072 File Offset: 0x00015272
		internal static bool Equals(SqlCollation a, SqlCollation b)
		{
			if (a == null || b == null)
			{
				return a == b;
			}
			return a._info == b._info && a._sortId == b._sortId;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001709D File Offset: 0x0001529D
		internal static bool Equals(SqlCollation collation, uint info, byte sortId)
		{
			return collation != null && collation._info == info && collation._sortId == sortId;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000170B8 File Offset: 0x000152B8
		public static SqlCollation FromLCIDAndSort(int lcid, SqlCompareOptions sqlCompareOptions)
		{
			uint num = 0U;
			byte b = 0;
			uint num2 = 0U;
			if ((sqlCompareOptions & SqlCompareOptions.IgnoreCase) == SqlCompareOptions.IgnoreCase)
			{
				num2 |= 1048576U;
			}
			if ((sqlCompareOptions & SqlCompareOptions.IgnoreNonSpace) == SqlCompareOptions.IgnoreNonSpace)
			{
				num2 |= 2097152U;
			}
			if ((sqlCompareOptions & SqlCompareOptions.IgnoreWidth) == SqlCompareOptions.IgnoreWidth)
			{
				num2 |= 4194304U;
			}
			if ((sqlCompareOptions & SqlCompareOptions.IgnoreKanaType) == SqlCompareOptions.IgnoreKanaType)
			{
				num2 |= 8388608U;
			}
			if ((sqlCompareOptions & SqlCompareOptions.BinarySort) == SqlCompareOptions.BinarySort)
			{
				num2 |= 16777216U;
			}
			num = (num & 1048575U) | num2;
			int num3 = lcid & 1048575;
			int num4 = SqlCollation.FirstSupportedCollationVersion(num3) << 28;
			num = (num & 32505856U) | (uint)num3 | (uint)num4;
			return new SqlCollation(num, b);
		}

		// Token: 0x04000170 RID: 368
		private const uint IgnoreCase = 1048576U;

		// Token: 0x04000171 RID: 369
		private const uint IgnoreNonSpace = 2097152U;

		// Token: 0x04000172 RID: 370
		private const uint IgnoreWidth = 4194304U;

		// Token: 0x04000173 RID: 371
		private const uint IgnoreKanaType = 8388608U;

		// Token: 0x04000174 RID: 372
		private const uint BinarySort = 16777216U;

		// Token: 0x04000175 RID: 373
		internal const uint MaskLcid = 1048575U;

		// Token: 0x04000176 RID: 374
		private const int LcidVersionBitOffset = 28;

		// Token: 0x04000177 RID: 375
		private const uint MaskLcidVersion = 4026531840U;

		// Token: 0x04000178 RID: 376
		private const uint MaskCompareOpt = 32505856U;

		// Token: 0x04000179 RID: 377
		internal readonly uint _info;

		// Token: 0x0400017A RID: 378
		internal readonly byte _sortId;
	}
}
