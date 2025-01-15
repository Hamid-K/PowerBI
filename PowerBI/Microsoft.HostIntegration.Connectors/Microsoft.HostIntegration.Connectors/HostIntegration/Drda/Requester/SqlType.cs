using System;
using System.Data;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200091D RID: 2333
	internal class SqlType
	{
		// Token: 0x06004990 RID: 18832 RVA: 0x00111C54 File Offset: 0x0010FE54
		static SqlType()
		{
			Array.Clear(SqlType.DrdaTypeToSqlTypeMappings, 0, SqlType.DrdaTypeToSqlTypeMappings.Length);
			SqlType.DrdaTypeToSqlTypeMappings[2] = 496;
			SqlType.DrdaTypeToSqlTypeMappings[4] = 500;
			SqlType.DrdaTypeToSqlTypeMappings[6] = 500;
			SqlType.DrdaTypeToSqlTypeMappings[8] = 480;
			SqlType.DrdaTypeToSqlTypeMappings[10] = 480;
			SqlType.DrdaTypeToSqlTypeMappings[12] = 480;
			SqlType.DrdaTypeToSqlTypeMappings[14] = 484;
			SqlType.DrdaTypeToSqlTypeMappings[16] = 488;
			SqlType.DrdaTypeToSqlTypeMappings[18] = 504;
			SqlType.DrdaTypeToSqlTypeMappings[22] = 492;
			SqlType.DrdaTypeToSqlTypeMappings[30] = 904;
			SqlType.DrdaTypeToSqlTypeMappings[32] = 384;
			SqlType.DrdaTypeToSqlTypeMappings[34] = 388;
			SqlType.DrdaTypeToSqlTypeMappings[36] = 392;
			SqlType.DrdaTypeToSqlTypeMappings[38] = 912;
			SqlType.DrdaTypeToSqlTypeMappings[40] = 908;
			SqlType.DrdaTypeToSqlTypeMappings[42] = 908;
			SqlType.DrdaTypeToSqlTypeMappings[48] = 452;
			SqlType.DrdaTypeToSqlTypeMappings[50] = 456;
			SqlType.DrdaTypeToSqlTypeMappings[52] = 456;
			SqlType.DrdaTypeToSqlTypeMappings[54] = 468;
			SqlType.DrdaTypeToSqlTypeMappings[56] = 464;
			SqlType.DrdaTypeToSqlTypeMappings[58] = 472;
			SqlType.DrdaTypeToSqlTypeMappings[62] = 456;
			SqlType.DrdaTypeToSqlTypeMappings[64] = 456;
			SqlType.DrdaTypeToSqlTypeMappings[196] = 988;
			SqlType.DrdaTypeToSqlTypeMappings[198] = 988;
			SqlType.DrdaTypeToSqlTypeMappings[200] = 404;
			SqlType.DrdaTypeToSqlTypeMappings[202] = 408;
			SqlType.DrdaTypeToSqlTypeMappings[204] = 412;
			SqlType.DrdaTypeToSqlTypeMappings[206] = 408;
			SqlType.DrdaTypeToSqlTypeMappings[186] = 996;
			for (int i = 1; i < SqlType.DrdaTypeToSqlTypeMappings.Length; i += 2)
			{
				if (SqlType.DrdaTypeToSqlTypeMappings[i] == 0 && SqlType.DrdaTypeToSqlTypeMappings[i - 1] != 0)
				{
					SqlType.DrdaTypeToSqlTypeMappings[i] = SqlType.DrdaTypeToSqlTypeMappings[i - 1] + 1;
				}
			}
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x00111E6C File Offset: 0x0011006C
		internal static bool IsLob(short sqlType)
		{
			switch (sqlType)
			{
			case 404:
			case 405:
			case 408:
			case 409:
			case 412:
			case 413:
				break;
			case 406:
			case 407:
			case 410:
			case 411:
				return false;
			default:
				if (sqlType - 988 > 1)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00111EBA File Offset: 0x001100BA
		internal static bool IsClob(short sqlType)
		{
			return sqlType - 408 <= 1 || sqlType - 412 <= 1 || sqlType - 988 <= 1;
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00111EDD File Offset: 0x001100DD
		internal static bool IsNullable(short sqlType)
		{
			return sqlType % 2 != 0;
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x00111EE7 File Offset: 0x001100E7
		internal static bool IsBinaryType(byte type)
		{
			return type >= 38 && type <= 45;
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00111EF8 File Offset: 0x001100F8
		internal static void ProcessSqlType(short sqlType, int length, ushort ccsid, byte serverType, Requester requester, Action<DrdaClientType, short, short, int, DbType, byte, int> processSqlTypeAttribute)
		{
			ushort num = (ushort)((requester.SqlManager.BinaryCcsid == null) ? 0 : requester.SqlManager.BinaryCcsid._ccsidsbc);
			int num2 = 2;
			DrdaClientType drdaClientType = DrdaClientType.Int;
			short num3 = -1;
			short num4 = -1;
			int num5 = -1;
			DbType dbType = DbType.Int32;
			int num6 = -1;
			bool flag = false;
			if (sqlType % 2 != 0)
			{
				sqlType -= 1;
				flag = true;
			}
			if (sqlType <= 472)
			{
				if (sqlType <= 412)
				{
					if (sqlType <= 392)
					{
						if (sqlType == 384)
						{
							if (requester.SqlManager.DatetimeAsChar)
							{
								drdaClientType = DrdaClientType.Char;
								dbType = DbType.StringFixedLength;
								num5 = 10;
							}
							else
							{
								drdaClientType = DrdaClientType.Date;
								dbType = DbType.Date;
							}
							num2 = 32;
							num4 = 0;
							num3 = 0;
							goto IL_058C;
						}
						if (sqlType == 388)
						{
							if (requester.SqlManager.DatetimeAsChar)
							{
								drdaClientType = DrdaClientType.Char;
								dbType = DbType.StringFixedLength;
								num5 = 8;
							}
							else
							{
								drdaClientType = DrdaClientType.Time;
								dbType = DbType.Time;
							}
							num2 = 34;
							num4 = 0;
							num3 = 0;
							goto IL_058C;
						}
						if (sqlType == 392)
						{
							if (requester.SqlManager.DatetimeAsChar)
							{
								drdaClientType = DrdaClientType.Char;
								dbType = DbType.StringFixedLength;
								num5 = 26;
							}
							else if (requester.SqlManager.DatetimeAsDate)
							{
								drdaClientType = DrdaClientType.Date;
								dbType = DbType.DateTime;
								num5 = 10;
							}
							else
							{
								drdaClientType = DrdaClientType.Timestamp;
								dbType = DbType.DateTime;
								num5 = 26;
							}
							num2 = 36;
							num4 = 0;
							num3 = 0;
							goto IL_058C;
						}
					}
					else
					{
						if (sqlType == 404)
						{
							drdaClientType = DrdaClientType.BLOB;
							dbType = DbType.Binary;
							num2 = 200;
							goto IL_058C;
						}
						if (sqlType == 408)
						{
							drdaClientType = DrdaClientType.CLOB;
							num3 = 0;
							num4 = 0;
							dbType = DbType.AnsiString;
							num2 = 202;
							goto IL_058C;
						}
						if (sqlType == 412)
						{
							drdaClientType = DrdaClientType.DBCLOB;
							num4 = 0;
							num3 = 0;
							dbType = DbType.String;
							num2 = 204;
							goto IL_058C;
						}
					}
				}
				else
				{
					if (sqlType <= 456)
					{
						if (sqlType != 448)
						{
							if (sqlType == 452)
							{
								if (ccsid == 65535 || ccsid == 0 || SqlType.IsBinaryType(serverType))
								{
									if (num != 0 && num != 65535)
									{
										drdaClientType = DrdaClientType.Char;
										dbType = DbType.String;
										num2 = 48;
									}
									else
									{
										drdaClientType = DrdaClientType.Binary;
										dbType = DbType.Binary;
										num2 = 38;
									}
								}
								else
								{
									drdaClientType = DrdaClientType.Char;
									dbType = DbType.String;
									num2 = 48;
								}
								num3 = 0;
								num4 = 0;
								goto IL_058C;
							}
							if (sqlType != 456)
							{
								goto IL_0587;
							}
						}
						if (ccsid == 65535 || ccsid == 0 || SqlType.IsBinaryType(serverType))
						{
							if (num != 0 && num != 65535)
							{
								drdaClientType = DrdaClientType.VarChar;
								dbType = DbType.String;
								num2 = ((sqlType == 448) ? 50 : 52);
							}
							else
							{
								drdaClientType = DrdaClientType.VarBinary;
								dbType = DbType.Binary;
								num2 = ((sqlType == 448) ? 40 : 42);
							}
						}
						else
						{
							drdaClientType = DrdaClientType.VarChar;
							if (Utility.IsMBCS(ccsid) || Utility.IsUnicode(ccsid))
							{
								num2 = ((sqlType == 448) ? 62 : 64);
							}
							else
							{
								num2 = ((sqlType == 448) ? 50 : 52);
							}
						}
						num3 = 0;
						num4 = 0;
						goto IL_058C;
					}
					if (sqlType != 464)
					{
						if (sqlType == 468)
						{
							num4 = 0;
							num3 = 0;
							if (ccsid == 65535 || ccsid == 0 || SqlType.IsBinaryType(serverType))
							{
								if (num != 0 && num != 65535)
								{
									drdaClientType = DrdaClientType.Char;
									dbType = DbType.String;
								}
								else
								{
									drdaClientType = DrdaClientType.Binary;
									dbType = DbType.Binary;
								}
							}
							else
							{
								drdaClientType = DrdaClientType.Char;
								dbType = DbType.String;
							}
							num2 = 54;
							goto IL_058C;
						}
						if (sqlType != 472)
						{
							goto IL_0587;
						}
					}
					num4 = 0;
					num3 = 0;
					if (ccsid == 65535 || ccsid == 0 || SqlType.IsBinaryType(serverType))
					{
						if (num != 0 && num != 65535)
						{
							drdaClientType = DrdaClientType.VarChar;
							dbType = DbType.String;
						}
						else
						{
							drdaClientType = DrdaClientType.VarBinary;
							dbType = DbType.Binary;
						}
					}
					else
					{
						drdaClientType = DrdaClientType.VarChar;
						dbType = DbType.String;
					}
					num2 = ((sqlType == 464) ? 56 : 58);
					goto IL_058C;
				}
			}
			else if (sqlType <= 500)
			{
				if (sqlType <= 488)
				{
					if (sqlType == 480)
					{
						if (length != 4)
						{
							if (length != 8)
							{
								if (length == 16)
								{
									drdaClientType = DrdaClientType.Double;
									num4 = 31;
									dbType = DbType.Decimal;
									num2 = 8;
								}
							}
							else
							{
								drdaClientType = DrdaClientType.Double;
								num4 = 15;
								dbType = DbType.Double;
								num2 = 10;
							}
						}
						else
						{
							drdaClientType = DrdaClientType.Real;
							num4 = 7;
							dbType = DbType.Single;
							num2 = 12;
						}
						num3 = 0;
						num5 = length;
						goto IL_058C;
					}
					if (sqlType != 484)
					{
						if (sqlType == 488)
						{
							drdaClientType = DrdaClientType.Numeric;
							dbType = DbType.VarNumeric;
							num2 = 16;
							if ((length & 65280) > 0)
							{
								num4 = (short)((length & 65280) >> 8);
								num3 = (short)(length & 255);
								num5 = (int)(num4 / 2 + 1);
								num6 = num5;
								goto IL_058C;
							}
							goto IL_058C;
						}
					}
					else
					{
						drdaClientType = DrdaClientType.Decimal;
						num2 = 14;
						dbType = DbType.Decimal;
						if ((length & 65280) > 0)
						{
							num4 = (short)((length & 65280) >> 8);
							num3 = (short)(length & 255);
							num5 = (int)(num4 / 2 + 1);
							num6 = num5;
							goto IL_058C;
						}
						goto IL_058C;
					}
				}
				else
				{
					if (sqlType == 492)
					{
						drdaClientType = DrdaClientType.BigInt;
						num4 = 20;
						num3 = 0;
						num5 = 8;
						dbType = DbType.Int64;
						num2 = 22;
						goto IL_058C;
					}
					if (sqlType == 496)
					{
						drdaClientType = DrdaClientType.Int;
						num4 = 10;
						num3 = 0;
						num5 = 4;
						dbType = DbType.Int32;
						num2 = 2;
						goto IL_058C;
					}
					if (sqlType == 500)
					{
						drdaClientType = DrdaClientType.SmallInt;
						num4 = 5;
						num3 = 0;
						num5 = 2;
						dbType = DbType.Int16;
						num2 = 4;
						goto IL_058C;
					}
				}
			}
			else if (sqlType <= 908)
			{
				if (sqlType == 504)
				{
					drdaClientType = DrdaClientType.Numeric;
					num5 = length + 3;
					dbType = DbType.VarNumeric;
					num2 = 18;
					goto IL_058C;
				}
				if (sqlType == 904)
				{
					drdaClientType = DrdaClientType.VarBinary;
					dbType = DbType.Binary;
					num3 = 0;
					num4 = 0;
					num2 = 30;
					goto IL_058C;
				}
				if (sqlType == 908)
				{
					drdaClientType = DrdaClientType.VarBinary;
					dbType = DbType.Binary;
					num2 = 40;
					num3 = 0;
					num4 = 0;
					goto IL_058C;
				}
			}
			else if (sqlType <= 988)
			{
				if (sqlType == 912)
				{
					drdaClientType = DrdaClientType.Binary;
					dbType = DbType.Binary;
					num2 = 38;
					num3 = 0;
					num4 = 0;
					goto IL_058C;
				}
				if (sqlType == 988)
				{
					if (ccsid == 0)
					{
						drdaClientType = DrdaClientType.Xml;
						num2 = 198;
					}
					else
					{
						drdaClientType = DrdaClientType.Xml;
						num2 = 196;
					}
					dbType = DbType.Xml;
					num5 = int.MaxValue;
					goto IL_058C;
				}
			}
			else
			{
				if (sqlType == 996)
				{
					drdaClientType = DrdaClientType.Decimal;
					dbType = DbType.VarNumeric;
					num2 = 186;
					goto IL_058C;
				}
				if (sqlType == 2448)
				{
					drdaClientType = DrdaClientType.VarChar;
					dbType = DbType.String;
					num2 = 50;
					num4 = 0;
					num3 = 0;
					goto IL_058C;
				}
			}
			IL_0587:
			drdaClientType = DrdaClientType.Binary;
			dbType = DbType.Binary;
			IL_058C:
			if (flag)
			{
				num2++;
			}
			processSqlTypeAttribute(drdaClientType, num3, num4, num5, dbType, (byte)num2, num6);
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x001124AC File Offset: 0x001106AC
		internal static SqlParameter ProcessParameter(ISqlParameter parameter, Requester requester)
		{
			SqlParameter sqlParameter = new SqlParameter();
			sqlParameter.Precision = parameter.Precision;
			sqlParameter.Scale = parameter.Scale;
			sqlParameter.Size = parameter.Size;
			sqlParameter.Value = parameter.Value;
			DrdaClientType drdaClientType = parameter.DrdaType;
			if (drdaClientType == DrdaClientType.Numeric && parameter.Size == 0)
			{
				drdaClientType = DrdaClientType.Decimal;
			}
			switch (drdaClientType)
			{
			case DrdaClientType.BigInt:
				sqlParameter.SqlType = 492;
				sqlParameter.Precision = 20;
				sqlParameter.Scale = 0;
				sqlParameter.Size = 8;
				sqlParameter.DbType = DbType.Int64;
				sqlParameter.DrdaServerType = 23;
				break;
			case DrdaClientType.Binary:
			case DrdaClientType.CharForBit:
				sqlParameter.SqlType = 453;
				sqlParameter.DbType = DbType.Binary;
				sqlParameter.Ccsid = 0;
				sqlParameter.DrdaServerType = 39;
				break;
			case DrdaClientType.Char:
				sqlParameter.SqlType = 453;
				sqlParameter.DbType = DbType.String;
				sqlParameter.DrdaServerType = 61;
				break;
			case DrdaClientType.Date:
				sqlParameter.SqlType = 385;
				sqlParameter.DbType = DbType.Date;
				sqlParameter.DrdaServerType = 33;
				break;
			case DrdaClientType.Decimal:
			case DrdaClientType.Numeric:
			case DrdaClientType.DecFloat:
				sqlParameter.SqlType = 485;
				sqlParameter.Precision = parameter.Precision;
				sqlParameter.Scale = parameter.Scale;
				sqlParameter.Size = ((int)(parameter.Precision & byte.MaxValue) << 8) | (int)(parameter.Scale & byte.MaxValue);
				sqlParameter.DbType = DbType.Decimal;
				sqlParameter.DrdaServerType = 15;
				break;
			case DrdaClientType.Double:
				sqlParameter.SqlType = 481;
				if (sqlParameter.Precision == 31)
				{
					sqlParameter.Precision = 31;
					sqlParameter.Size = 16;
					sqlParameter.DbType = DbType.Decimal;
					sqlParameter.DrdaServerType = 9;
				}
				else
				{
					sqlParameter.Precision = 15;
					sqlParameter.Size = 8;
					sqlParameter.DbType = DbType.Double;
					sqlParameter.DrdaServerType = 11;
				}
				break;
			case DrdaClientType.Int:
				sqlParameter.SqlType = 497;
				sqlParameter.Precision = 10;
				sqlParameter.Size = 4;
				sqlParameter.DbType = DbType.Int32;
				sqlParameter.DrdaServerType = 3;
				break;
			case DrdaClientType.Real:
				sqlParameter.SqlType = 481;
				sqlParameter.Precision = 7;
				sqlParameter.Size = 4;
				sqlParameter.DbType = DbType.Single;
				sqlParameter.DrdaServerType = 13;
				break;
			case DrdaClientType.SmallInt:
			case DrdaClientType.Boolean:
				sqlParameter.SqlType = 501;
				sqlParameter.Precision = 5;
				sqlParameter.Scale = 0;
				sqlParameter.Size = 2;
				sqlParameter.DbType = DbType.Int16;
				sqlParameter.DrdaServerType = 5;
				break;
			case DrdaClientType.Time:
				sqlParameter.SqlType = 389;
				sqlParameter.DbType = DbType.Time;
				sqlParameter.DrdaServerType = 35;
				break;
			case DrdaClientType.Timestamp:
				sqlParameter.SqlType = 393;
				sqlParameter.DbType = DbType.DateTime;
				sqlParameter.DrdaServerType = 37;
				break;
			case DrdaClientType.VarBinary:
				sqlParameter.SqlType = 449;
				sqlParameter.DbType = DbType.Binary;
				sqlParameter.Ccsid = 0;
				sqlParameter.DrdaServerType = 41;
				sqlParameter.Size = 32767;
				break;
			case DrdaClientType.VarChar:
			case DrdaClientType.LongVarChar:
			case DrdaClientType.RowId:
				sqlParameter.SqlType = 449;
				sqlParameter.DbType = DbType.String;
				sqlParameter.DrdaServerType = 51;
				sqlParameter.Size = 32767;
				break;
			case DrdaClientType.Graphic:
			case DrdaClientType.NChar:
				sqlParameter.SqlType = 469;
				sqlParameter.DbType = DbType.String;
				sqlParameter.DrdaServerType = 55;
				break;
			case DrdaClientType.VarGraphic:
			case DrdaClientType.LongVarGraphic:
			case DrdaClientType.NVarChar:
				sqlParameter.SqlType = 465;
				sqlParameter.DbType = DbType.String;
				sqlParameter.DrdaServerType = 57;
				sqlParameter.Size = 32767;
				break;
			case DrdaClientType.CLOB:
				sqlParameter.SqlType = 409;
				sqlParameter.DbType = DbType.AnsiString;
				sqlParameter.DrdaServerType = 203;
				break;
			case DrdaClientType.DBCLOB:
				sqlParameter.SqlType = 413;
				sqlParameter.DbType = DbType.String;
				sqlParameter.DrdaServerType = 205;
				break;
			case DrdaClientType.BLOB:
				sqlParameter.SqlType = 405;
				sqlParameter.DbType = DbType.Binary;
				sqlParameter.DrdaServerType = 201;
				break;
			case DrdaClientType.Xml:
				sqlParameter.SqlType = 989;
				sqlParameter.DbType = DbType.String;
				sqlParameter.DrdaServerType = 197;
				break;
			}
			return sqlParameter;
		}

		// Token: 0x040036DE RID: 14046
		internal const short BigInt = 492;

		// Token: 0x040036DF RID: 14047
		internal const short Nullable_BigInt = 493;

		// Token: 0x040036E0 RID: 14048
		internal const short Integer = 496;

		// Token: 0x040036E1 RID: 14049
		internal const short Nullable_Integer = 497;

		// Token: 0x040036E2 RID: 14050
		internal const short Smallint = 500;

		// Token: 0x040036E3 RID: 14051
		internal const short Nullable_Smallint = 501;

		// Token: 0x040036E4 RID: 14052
		internal const short Float = 480;

		// Token: 0x040036E5 RID: 14053
		internal const short Nullable_Float = 481;

		// Token: 0x040036E6 RID: 14054
		internal const short Fixed_Dec = 484;

		// Token: 0x040036E7 RID: 14055
		internal const short Nullable_Fixed_Dec = 485;

		// Token: 0x040036E8 RID: 14056
		internal const short Zoned_Dec = 488;

		// Token: 0x040036E9 RID: 14057
		internal const short Nullable_Zoned_Dec = 489;

		// Token: 0x040036EA RID: 14058
		internal const short Num_Char = 504;

		// Token: 0x040036EB RID: 14059
		internal const short Nullable_Num_Char = 505;

		// Token: 0x040036EC RID: 14060
		internal const short Date = 384;

		// Token: 0x040036ED RID: 14061
		internal const short Nullable_Date = 385;

		// Token: 0x040036EE RID: 14062
		internal const short Time = 388;

		// Token: 0x040036EF RID: 14063
		internal const short Nullable_Time = 389;

		// Token: 0x040036F0 RID: 14064
		internal const short Timestamp = 392;

		// Token: 0x040036F1 RID: 14065
		internal const short Nullable_Timestamp = 393;

		// Token: 0x040036F2 RID: 14066
		internal const short Char = 452;

		// Token: 0x040036F3 RID: 14067
		internal const short Nullable_Char = 453;

		// Token: 0x040036F4 RID: 14068
		internal const short Varchar = 448;

		// Token: 0x040036F5 RID: 14069
		internal const short Nullable_Varchar = 449;

		// Token: 0x040036F6 RID: 14070
		internal const short Lvarchar = 456;

		// Token: 0x040036F7 RID: 14071
		internal const short Nullable_Lvarchar = 457;

		// Token: 0x040036F8 RID: 14072
		internal const short Nt_Char = 460;

		// Token: 0x040036F9 RID: 14073
		internal const short Nullable_Nt_Char = 461;

		// Token: 0x040036FA RID: 14074
		internal const short Nt_Sbcs = 460;

		// Token: 0x040036FB RID: 14075
		internal const short Nullable_Nt_Sbcs = 461;

		// Token: 0x040036FC RID: 14076
		internal const short Char_Sbcs = 452;

		// Token: 0x040036FD RID: 14077
		internal const short Nullable_Char_Sbcs = 453;

		// Token: 0x040036FE RID: 14078
		internal const short Varchar_Sbcs = 448;

		// Token: 0x040036FF RID: 14079
		internal const short Nullable_Varchar_Sbcs = 449;

		// Token: 0x04003700 RID: 14080
		internal const short Lvarchar_Sbcs = 456;

		// Token: 0x04003701 RID: 14081
		internal const short Nullable_Lvarchar_Sbcs = 457;

		// Token: 0x04003702 RID: 14082
		internal const short Graphic = 468;

		// Token: 0x04003703 RID: 14083
		internal const short Nullable_Graphic = 469;

		// Token: 0x04003704 RID: 14084
		internal const short Vargraphic = 464;

		// Token: 0x04003705 RID: 14085
		internal const short Nullable_Vargraphic = 465;

		// Token: 0x04003706 RID: 14086
		internal const short Lvargraphic = 472;

		// Token: 0x04003707 RID: 14087
		internal const short Nullable_Lvargraphic = 473;

		// Token: 0x04003708 RID: 14088
		internal const short Lob = 404;

		// Token: 0x04003709 RID: 14089
		internal const short Nullable_Lob = 405;

		// Token: 0x0400370A RID: 14090
		internal const short Clob_Mbcs = 408;

		// Token: 0x0400370B RID: 14091
		internal const short Nullable_Clob_Mbcs = 409;

		// Token: 0x0400370C RID: 14092
		internal const short Clob_Dbcs = 412;

		// Token: 0x0400370D RID: 14093
		internal const short Nullable_Clob_Dbcs = 413;

		// Token: 0x0400370E RID: 14094
		internal const short VarBinaryString = 908;

		// Token: 0x0400370F RID: 14095
		internal const short Nullable_VarBinaryString = 909;

		// Token: 0x04003710 RID: 14096
		internal const short BinaryString = 912;

		// Token: 0x04003711 RID: 14097
		internal const short Nullable_BinaryString = 913;

		// Token: 0x04003712 RID: 14098
		internal const short Obl = 960;

		// Token: 0x04003713 RID: 14099
		internal const short Nobl = 961;

		// Token: 0x04003714 RID: 14100
		internal const short Ocl = 964;

		// Token: 0x04003715 RID: 14101
		internal const short Nocl = 965;

		// Token: 0x04003716 RID: 14102
		internal const short Ocdl = 968;

		// Token: 0x04003717 RID: 14103
		internal const short Nocdl = 969;

		// Token: 0x04003718 RID: 14104
		internal const short Rowid = 904;

		// Token: 0x04003719 RID: 14105
		internal const short Xml = 988;

		// Token: 0x0400371A RID: 14106
		internal const short Nullable_Xml = 989;

		// Token: 0x0400371B RID: 14107
		internal const short DecFloat = 996;

		// Token: 0x0400371C RID: 14108
		internal const short Nullable_DecFloat = 997;

		// Token: 0x0400371D RID: 14109
		internal const short Nullable_Timestamp_TimeZone = 2449;

		// Token: 0x0400371E RID: 14110
		internal const short Timestamp_TimeZone = 2448;

		// Token: 0x0400371F RID: 14111
		internal static short[] DrdaTypeToSqlTypeMappings = new short[256];
	}
}
