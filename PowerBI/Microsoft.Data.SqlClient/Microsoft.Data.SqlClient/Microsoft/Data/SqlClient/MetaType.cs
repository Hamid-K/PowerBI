using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient.Server;
using Microsoft.SqlServer.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200007A RID: 122
	internal sealed class MetaType
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001DF14 File Offset: 0x0001C114
		public MetaType(byte precision, byte scale, int fixedLength, bool isFixed, bool isLong, bool isPlp, byte tdsType, byte nullableTdsType, string typeName, Type classType, Type sqlType, SqlDbType sqldbType, DbType dbType, byte propBytes)
		{
			this.Precision = precision;
			this.Scale = scale;
			this.FixedLength = fixedLength;
			this.IsFixed = isFixed;
			this.IsLong = isLong;
			this.IsPlp = isPlp;
			this.TDSType = tdsType;
			this.NullableType = nullableTdsType;
			this.TypeName = typeName;
			this.SqlDbType = sqldbType;
			this.DbType = dbType;
			this.ClassType = classType;
			this.SqlType = sqlType;
			this.PropBytes = propBytes;
			this.IsAnsiType = MetaType._IsAnsiType(sqldbType);
			this.IsBinType = MetaType._IsBinType(sqldbType);
			this.IsCharType = MetaType._IsCharType(sqldbType);
			this.IsNCharType = MetaType._IsNCharType(sqldbType);
			this.IsSizeInCharacters = MetaType._IsSizeInCharacters(sqldbType);
			this.Is2008Type = MetaType._Is2008Type(sqldbType);
			this.IsVarTime = MetaType._IsVarTime(sqldbType);
			this.Is70Supported = MetaType._Is70Supported(this.SqlDbType);
			this.Is80Supported = MetaType._Is80Supported(this.SqlDbType);
			this.Is90Supported = MetaType._Is90Supported(this.SqlDbType);
			this.Is100Supported = MetaType._Is100Supported(this.SqlDbType);
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001996E File Offset: 0x00017B6E
		public int TypeId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001E033 File Offset: 0x0001C233
		private static bool _IsAnsiType(SqlDbType type)
		{
			return type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Text;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001E045 File Offset: 0x0001C245
		private static bool _IsSizeInCharacters(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.Xml || type == SqlDbType.NText;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001E05D File Offset: 0x0001C25D
		private static bool _IsCharType(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.NText || type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Text || type == SqlDbType.Xml;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001E083 File Offset: 0x0001C283
		private static bool _IsNCharType(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.NText || type == SqlDbType.Xml;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001E09B File Offset: 0x0001C29B
		private static bool _IsBinType(SqlDbType type)
		{
			return type == SqlDbType.Image || type == SqlDbType.Binary || type == SqlDbType.VarBinary || type == SqlDbType.Timestamp || type == SqlDbType.Udt || type == (SqlDbType)24;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001E0BB File Offset: 0x0001C2BB
		private static bool _Is70Supported(SqlDbType type)
		{
			return type != SqlDbType.BigInt && type > SqlDbType.BigInt && type <= SqlDbType.VarChar;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001E0CE File Offset: 0x0001C2CE
		private static bool _Is80Supported(SqlDbType type)
		{
			return type >= SqlDbType.BigInt && type <= SqlDbType.Variant;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001E0DE File Offset: 0x0001C2DE
		private static bool _Is90Supported(SqlDbType type)
		{
			return MetaType._Is80Supported(type) || SqlDbType.Xml == type || SqlDbType.Udt == type;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
		private static bool _Is100Supported(SqlDbType type)
		{
			return MetaType._Is90Supported(type) || SqlDbType.Date == type || SqlDbType.Time == type || SqlDbType.DateTime2 == type || SqlDbType.DateTimeOffset == type;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001E114 File Offset: 0x0001C314
		private static bool _Is2008Type(SqlDbType type)
		{
			return SqlDbType.Structured == type;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001E11B File Offset: 0x0001C31B
		internal static bool _IsVarTime(SqlDbType type)
		{
			return type == SqlDbType.Time || type == SqlDbType.DateTime2 || type == SqlDbType.DateTimeOffset;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001E130 File Offset: 0x0001C330
		internal static MetaType GetMetaTypeFromSqlDbType(SqlDbType target, bool isMultiValued)
		{
			switch (target)
			{
			case SqlDbType.BigInt:
				return MetaType.s_metaBigInt;
			case SqlDbType.Binary:
				return MetaType.s_metaBinary;
			case SqlDbType.Bit:
				return MetaType.s_metaBit;
			case SqlDbType.Char:
				return MetaType.s_metaChar;
			case SqlDbType.DateTime:
				return MetaType.s_metaDateTime;
			case SqlDbType.Decimal:
				return MetaType.MetaDecimal;
			case SqlDbType.Float:
				return MetaType.s_metaFloat;
			case SqlDbType.Image:
				return MetaType.MetaImage;
			case SqlDbType.Int:
				return MetaType.s_metaInt;
			case SqlDbType.Money:
				return MetaType.s_metaMoney;
			case SqlDbType.NChar:
				return MetaType.s_metaNChar;
			case SqlDbType.NText:
				return MetaType.MetaNText;
			case SqlDbType.NVarChar:
				return MetaType.MetaNVarChar;
			case SqlDbType.Real:
				return MetaType.s_metaReal;
			case SqlDbType.UniqueIdentifier:
				return MetaType.s_metaUniqueId;
			case SqlDbType.SmallDateTime:
				return MetaType.s_metaSmallDateTime;
			case SqlDbType.SmallInt:
				return MetaType.s_metaSmallInt;
			case SqlDbType.SmallMoney:
				return MetaType.s_metaSmallMoney;
			case SqlDbType.Text:
				return MetaType.MetaText;
			case SqlDbType.Timestamp:
				return MetaType.s_metaTimestamp;
			case SqlDbType.TinyInt:
				return MetaType.s_metaTinyInt;
			case SqlDbType.VarBinary:
				return MetaType.MetaVarBinary;
			case SqlDbType.VarChar:
				return MetaType.s_metaVarChar;
			case SqlDbType.Variant:
				return MetaType.s_metaVariant;
			case (SqlDbType)24:
				return MetaType.s_metaSmallVarBinary;
			case SqlDbType.Xml:
				return MetaType.MetaXml;
			case SqlDbType.Udt:
				return MetaType.MetaUdt;
			case SqlDbType.Structured:
				if (isMultiValued)
				{
					return MetaType.s_metaTable;
				}
				return MetaType.s_metaSUDT;
			case SqlDbType.Date:
				return MetaType.s_metaDate;
			case SqlDbType.Time:
				return MetaType.MetaTime;
			case SqlDbType.DateTime2:
				return MetaType.s_metaDateTime2;
			case SqlDbType.DateTimeOffset:
				return MetaType.MetaDateTimeOffset;
			}
			throw SQL.InvalidSqlDbType(target);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		internal static MetaType GetMetaTypeFromDbType(DbType target)
		{
			switch (target)
			{
			case DbType.AnsiString:
				return MetaType.s_metaVarChar;
			case DbType.Binary:
				return MetaType.MetaVarBinary;
			case DbType.Byte:
				return MetaType.s_metaTinyInt;
			case DbType.Boolean:
				return MetaType.s_metaBit;
			case DbType.Currency:
				return MetaType.s_metaMoney;
			case DbType.Date:
				return MetaType.s_metaDate;
			case DbType.DateTime:
				return MetaType.s_metaDateTime;
			case DbType.Decimal:
				return MetaType.MetaDecimal;
			case DbType.Double:
				return MetaType.s_metaFloat;
			case DbType.Guid:
				return MetaType.s_metaUniqueId;
			case DbType.Int16:
				return MetaType.s_metaSmallInt;
			case DbType.Int32:
				return MetaType.s_metaInt;
			case DbType.Int64:
				return MetaType.s_metaBigInt;
			case DbType.Object:
				return MetaType.s_metaVariant;
			case DbType.Single:
				return MetaType.s_metaReal;
			case DbType.String:
				return MetaType.MetaNVarChar;
			case DbType.Time:
				return MetaType.MetaTime;
			case DbType.AnsiStringFixedLength:
				return MetaType.s_metaChar;
			case DbType.StringFixedLength:
				return MetaType.s_metaNChar;
			case DbType.Xml:
				return MetaType.MetaXml;
			case DbType.DateTime2:
				return MetaType.s_metaDateTime2;
			case DbType.DateTimeOffset:
				return MetaType.MetaDateTimeOffset;
			}
			throw ADP.DbTypeNotSupported(target, typeof(SqlDbType));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001E408 File Offset: 0x0001C608
		internal static MetaType GetMaxMetaTypeFromMetaType(MetaType mt)
		{
			SqlDbType sqlDbType = mt.SqlDbType;
			if (sqlDbType <= SqlDbType.NChar)
			{
				if (sqlDbType != SqlDbType.Binary)
				{
					if (sqlDbType == SqlDbType.Char)
					{
						goto IL_0040;
					}
					if (sqlDbType != SqlDbType.NChar)
					{
						goto IL_0058;
					}
					goto IL_0048;
				}
			}
			else if (sqlDbType <= SqlDbType.VarBinary)
			{
				if (sqlDbType == SqlDbType.NVarChar)
				{
					goto IL_0048;
				}
				if (sqlDbType != SqlDbType.VarBinary)
				{
					goto IL_0058;
				}
			}
			else
			{
				if (sqlDbType == SqlDbType.VarChar)
				{
					goto IL_0040;
				}
				if (sqlDbType != SqlDbType.Udt)
				{
					goto IL_0058;
				}
				return MetaType.s_metaMaxUdt;
			}
			return MetaType.MetaMaxVarBinary;
			IL_0040:
			return MetaType.MetaMaxVarChar;
			IL_0048:
			return MetaType.MetaMaxNVarChar;
			IL_0058:
			return mt;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001E470 File Offset: 0x0001C670
		internal static MetaType GetMetaTypeFromType(Type dataType)
		{
			return MetaType.GetMetaTypeFromValue(dataType, null, false, true);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001E47B File Offset: 0x0001C67B
		internal static MetaType GetMetaTypeFromValue(object value, bool streamAllowed = true)
		{
			return MetaType.GetMetaTypeFromValue(value.GetType(), value, true, streamAllowed);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001E48C File Offset: 0x0001C68C
		private static MetaType GetMetaTypeFromValue(Type dataType, object value, bool inferLen, bool streamAllowed)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (dataType == typeof(byte[]))
				{
					if (!inferLen || ((byte[])value).Length <= 8000)
					{
						return MetaType.MetaVarBinary;
					}
					return MetaType.MetaImage;
				}
				else
				{
					if (dataType == typeof(Guid))
					{
						return MetaType.s_metaUniqueId;
					}
					if (dataType == typeof(object))
					{
						return MetaType.s_metaVariant;
					}
					if (dataType == typeof(SqlBinary))
					{
						return MetaType.MetaVarBinary;
					}
					if (dataType == typeof(SqlBoolean))
					{
						return MetaType.s_metaBit;
					}
					if (dataType == typeof(SqlByte))
					{
						return MetaType.s_metaTinyInt;
					}
					if (dataType == typeof(SqlBytes))
					{
						return MetaType.MetaVarBinary;
					}
					if (dataType == typeof(SqlChars))
					{
						return MetaType.MetaNVarChar;
					}
					if (dataType == typeof(SqlDateTime))
					{
						return MetaType.s_metaDateTime;
					}
					if (dataType == typeof(SqlDouble))
					{
						return MetaType.s_metaFloat;
					}
					if (dataType == typeof(SqlGuid))
					{
						return MetaType.s_metaUniqueId;
					}
					if (dataType == typeof(SqlInt16))
					{
						return MetaType.s_metaSmallInt;
					}
					if (dataType == typeof(SqlInt32))
					{
						return MetaType.s_metaInt;
					}
					if (dataType == typeof(SqlInt64))
					{
						return MetaType.s_metaBigInt;
					}
					if (dataType == typeof(SqlMoney))
					{
						return MetaType.s_metaMoney;
					}
					if (dataType == typeof(SqlDecimal))
					{
						return MetaType.MetaDecimal;
					}
					if (dataType == typeof(SqlSingle))
					{
						return MetaType.s_metaReal;
					}
					if (dataType == typeof(SqlXml))
					{
						return MetaType.MetaXml;
					}
					if (dataType == typeof(SqlString))
					{
						if (!inferLen || ((SqlString)value).IsNull)
						{
							return MetaType.MetaNVarChar;
						}
						return MetaType.PromoteStringType(((SqlString)value).Value);
					}
					else
					{
						if (dataType == typeof(IEnumerable<DbDataRecord>) || dataType == typeof(DataTable))
						{
							return MetaType.s_metaTable;
						}
						if (dataType == typeof(TimeSpan))
						{
							return MetaType.MetaTime;
						}
						if (dataType == typeof(DateTimeOffset))
						{
							return MetaType.MetaDateTimeOffset;
						}
						SqlUdtInfo sqlUdtInfo = SqlUdtInfo.TryGetFromType(dataType);
						if (sqlUdtInfo != null)
						{
							return MetaType.MetaUdt;
						}
						if (streamAllowed)
						{
							if (typeof(Stream).IsAssignableFrom(dataType))
							{
								return MetaType.MetaVarBinary;
							}
							if (typeof(TextReader).IsAssignableFrom(dataType))
							{
								return MetaType.MetaNVarChar;
							}
							if (typeof(XmlReader).IsAssignableFrom(dataType))
							{
								return MetaType.MetaXml;
							}
						}
						throw ADP.UnknownDataType(dataType);
					}
				}
				break;
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return MetaType.s_metaBit;
			case TypeCode.Char:
				throw ADP.InvalidDataType(TypeCode.Char);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return MetaType.s_metaTinyInt;
			case TypeCode.Int16:
				return MetaType.s_metaSmallInt;
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return MetaType.s_metaInt;
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return MetaType.s_metaBigInt;
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return MetaType.s_metaReal;
			case TypeCode.Double:
				return MetaType.s_metaFloat;
			case TypeCode.Decimal:
				return MetaType.MetaDecimal;
			case TypeCode.DateTime:
				return MetaType.s_metaDateTime;
			case TypeCode.String:
				if (!inferLen)
				{
					return MetaType.MetaNVarChar;
				}
				return MetaType.PromoteStringType((string)value);
			}
			throw ADP.UnknownDataTypeCode(dataType, Type.GetTypeCode(dataType));
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001E844 File Offset: 0x0001CA44
		internal static object GetNullSqlValue(Type sqlType)
		{
			if (sqlType == typeof(SqlSingle))
			{
				return SqlSingle.Null;
			}
			if (sqlType == typeof(SqlString))
			{
				return SqlString.Null;
			}
			if (sqlType == typeof(SqlDouble))
			{
				return SqlDouble.Null;
			}
			if (sqlType == typeof(SqlBinary))
			{
				return SqlBinary.Null;
			}
			if (sqlType == typeof(SqlGuid))
			{
				return SqlGuid.Null;
			}
			if (sqlType == typeof(SqlBoolean))
			{
				return SqlBoolean.Null;
			}
			if (sqlType == typeof(SqlByte))
			{
				return SqlByte.Null;
			}
			if (sqlType == typeof(SqlInt16))
			{
				return SqlInt16.Null;
			}
			if (sqlType == typeof(SqlInt32))
			{
				return SqlInt32.Null;
			}
			if (sqlType == typeof(SqlInt64))
			{
				return SqlInt64.Null;
			}
			if (sqlType == typeof(SqlDecimal))
			{
				return SqlDecimal.Null;
			}
			if (sqlType == typeof(SqlDateTime))
			{
				return SqlDateTime.Null;
			}
			if (sqlType == typeof(SqlMoney))
			{
				return SqlMoney.Null;
			}
			if (sqlType == typeof(SqlXml))
			{
				return SqlXml.Null;
			}
			if (sqlType == typeof(object))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(IEnumerable<DbDataRecord>))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(DataTable))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(DateTime))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(TimeSpan))
			{
				return DBNull.Value;
			}
			sqlType == typeof(DateTimeOffset);
			return DBNull.Value;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001EA70 File Offset: 0x0001CC70
		internal static MetaType PromoteStringType(string s)
		{
			int length = s.Length;
			if (length << 1 > 8000)
			{
				return MetaType.s_metaVarChar;
			}
			return MetaType.MetaNVarChar;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		internal static object GetComValueFromSqlVariant(object sqlVal)
		{
			object obj = null;
			if (ADP.IsNull(sqlVal))
			{
				return obj;
			}
			if (!(sqlVal is SqlSingle))
			{
				if (!(sqlVal is SqlString))
				{
					if (!(sqlVal is SqlDouble))
					{
						if (!(sqlVal is SqlBinary))
						{
							if (!(sqlVal is SqlGuid))
							{
								if (!(sqlVal is SqlBoolean))
								{
									if (!(sqlVal is SqlByte))
									{
										if (!(sqlVal is SqlInt16))
										{
											if (!(sqlVal is SqlInt32))
											{
												if (!(sqlVal is SqlInt64))
												{
													if (!(sqlVal is SqlDecimal))
													{
														if (!(sqlVal is SqlDateTime))
														{
															if (!(sqlVal is SqlMoney))
															{
																if (sqlVal is SqlXml)
																{
																	obj = ((SqlXml)sqlVal).Value;
																}
															}
															else
															{
																obj = ((SqlMoney)sqlVal).Value;
															}
														}
														else
														{
															obj = ((SqlDateTime)sqlVal).Value;
														}
													}
													else
													{
														obj = ((SqlDecimal)sqlVal).Value;
													}
												}
												else
												{
													obj = ((SqlInt64)sqlVal).Value;
												}
											}
											else
											{
												obj = ((SqlInt32)sqlVal).Value;
											}
										}
										else
										{
											obj = ((SqlInt16)sqlVal).Value;
										}
									}
									else
									{
										obj = ((SqlByte)sqlVal).Value;
									}
								}
								else
								{
									obj = ((SqlBoolean)sqlVal).Value;
								}
							}
							else
							{
								obj = ((SqlGuid)sqlVal).Value;
							}
						}
						else
						{
							obj = ((SqlBinary)sqlVal).Value;
						}
					}
					else
					{
						obj = ((SqlDouble)sqlVal).Value;
					}
				}
				else
				{
					obj = ((SqlString)sqlVal).Value;
				}
			}
			else
			{
				obj = ((SqlSingle)sqlVal).Value;
			}
			return obj;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001EC94 File Offset: 0x0001CE94
		[Conditional("DEBUG")]
		private static void AssertIsUserDefinedTypeInstance(object sqlValue, string failedAssertMessage)
		{
			Type type = sqlValue.GetType();
			SqlUserDefinedTypeAttribute[] array = (SqlUserDefinedTypeAttribute[])type.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), true);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001ECC0 File Offset: 0x0001CEC0
		internal static object GetSqlValueFromComVariant(object comVal)
		{
			object obj = null;
			if (comVal != null && DBNull.Value != comVal)
			{
				if (!(comVal is float))
				{
					if (!(comVal is string))
					{
						if (!(comVal is double))
						{
							if (!(comVal is byte[]))
							{
								if (!(comVal is char))
								{
									if (!(comVal is char[]))
									{
										if (!(comVal is Guid))
										{
											if (!(comVal is bool))
											{
												if (!(comVal is byte))
												{
													if (!(comVal is short))
													{
														if (!(comVal is int))
														{
															if (!(comVal is long))
															{
																if (!(comVal is decimal))
																{
																	if (!(comVal is DateTime))
																	{
																		if (!(comVal is XmlReader))
																		{
																			if (comVal is TimeSpan || comVal is DateTimeOffset)
																			{
																				obj = comVal;
																			}
																		}
																		else
																		{
																			obj = new SqlXml((XmlReader)comVal);
																		}
																	}
																	else
																	{
																		obj = new SqlDateTime((DateTime)comVal);
																	}
																}
																else
																{
																	obj = new SqlDecimal((decimal)comVal);
																}
															}
															else
															{
																obj = new SqlInt64((long)comVal);
															}
														}
														else
														{
															obj = new SqlInt32((int)comVal);
														}
													}
													else
													{
														obj = new SqlInt16((short)comVal);
													}
												}
												else
												{
													obj = new SqlByte((byte)comVal);
												}
											}
											else
											{
												obj = new SqlBoolean((bool)comVal);
											}
										}
										else
										{
											obj = new SqlGuid((Guid)comVal);
										}
									}
									else
									{
										obj = new SqlChars((char[])comVal);
									}
								}
								else
								{
									obj = new SqlString(((char)comVal).ToString());
								}
							}
							else
							{
								obj = new SqlBinary((byte[])comVal);
							}
						}
						else
						{
							obj = new SqlDouble((double)comVal);
						}
					}
					else
					{
						obj = new SqlString((string)comVal);
					}
				}
				else
				{
					obj = new SqlSingle((float)comVal);
				}
			}
			return obj;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001EED8 File Offset: 0x0001D0D8
		internal static SqlDbType GetSqlDbTypeFromOleDbType(short dbType, string typeName)
		{
			SqlDbType sqlDbType = SqlDbType.Variant;
			if (dbType <= 64)
			{
				switch (dbType)
				{
				case 2:
				case 18:
					return SqlDbType.SmallInt;
				case 3:
					return SqlDbType.Int;
				case 4:
					return SqlDbType.Real;
				case 5:
					return SqlDbType.Float;
				case 6:
					return (typeName == "smallmoney") ? SqlDbType.SmallMoney : SqlDbType.Money;
				case 7:
					break;
				case 8:
					goto IL_01A7;
				case 9:
				case 10:
				case 13:
				case 15:
				case 19:
					return sqlDbType;
				case 11:
					return SqlDbType.Bit;
				case 12:
					return SqlDbType.Variant;
				case 14:
					goto IL_015F;
				case 16:
				case 17:
					return SqlDbType.TinyInt;
				case 20:
					return SqlDbType.BigInt;
				default:
					if (dbType != 64)
					{
						return sqlDbType;
					}
					break;
				}
			}
			else
			{
				if (dbType != 72)
				{
					switch (dbType)
					{
					case 128:
						goto IL_018D;
					case 129:
						break;
					case 130:
						goto IL_01A7;
					case 131:
						goto IL_015F;
					case 132:
						return SqlDbType.Udt;
					case 133:
						return SqlDbType.Date;
					case 134:
					case 136:
					case 137:
					case 138:
					case 139:
					case 140:
					case 142:
					case 143:
					case 144:
						return sqlDbType;
					case 135:
						goto IL_0133;
					case 141:
						return SqlDbType.Xml;
					case 145:
						return SqlDbType.Time;
					case 146:
						return SqlDbType.DateTimeOffset;
					default:
						switch (dbType)
						{
						case 200:
							break;
						case 201:
							return SqlDbType.Text;
						case 202:
							goto IL_01A7;
						case 203:
							return SqlDbType.NText;
						case 204:
							goto IL_018D;
						case 205:
							return SqlDbType.Image;
						default:
							return sqlDbType;
						}
						break;
					}
					return (typeName == "char") ? SqlDbType.Char : SqlDbType.VarChar;
					IL_018D:
					return (typeName == "binary") ? SqlDbType.Binary : SqlDbType.VarBinary;
				}
				return SqlDbType.UniqueIdentifier;
			}
			IL_0133:
			SqlDbType sqlDbType2;
			if (!(typeName == "smalldatetime"))
			{
				if (!(typeName == "datetime2"))
				{
					sqlDbType2 = SqlDbType.DateTime;
				}
				else
				{
					sqlDbType2 = SqlDbType.DateTime2;
				}
			}
			else
			{
				sqlDbType2 = SqlDbType.SmallDateTime;
			}
			return sqlDbType2;
			IL_015F:
			return SqlDbType.Decimal;
			IL_01A7:
			sqlDbType = ((typeName == "nchar") ? SqlDbType.NChar : SqlDbType.NVarChar);
			return sqlDbType;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		internal static MetaType GetSqlDataType(int tdsType, uint userType, int length)
		{
			if (tdsType <= 165)
			{
				if (tdsType <= 111)
				{
					switch (tdsType)
					{
					case 31:
					case 32:
					case 33:
					case 44:
					case 46:
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
						goto IL_0279;
					case 34:
						return MetaType.MetaImage;
					case 35:
						return MetaType.MetaText;
					case 36:
						return MetaType.s_metaUniqueId;
					case 37:
						return MetaType.s_metaSmallVarBinary;
					case 38:
						if (4 > length)
						{
							if (2 != length)
							{
								return MetaType.s_metaTinyInt;
							}
							return MetaType.s_metaSmallInt;
						}
						else
						{
							if (4 != length)
							{
								return MetaType.s_metaBigInt;
							}
							return MetaType.s_metaInt;
						}
						break;
					case 39:
						goto IL_01C6;
					case 40:
						return MetaType.s_metaDate;
					case 41:
						return MetaType.MetaTime;
					case 42:
						return MetaType.s_metaDateTime2;
					case 43:
						return MetaType.MetaDateTimeOffset;
					case 45:
						goto IL_01CC;
					case 47:
						goto IL_01E3;
					case 48:
						return MetaType.s_metaTinyInt;
					case 50:
						break;
					case 52:
						return MetaType.s_metaSmallInt;
					case 56:
						return MetaType.s_metaInt;
					case 58:
						return MetaType.s_metaSmallDateTime;
					case 59:
						return MetaType.s_metaReal;
					case 60:
						return MetaType.s_metaMoney;
					case 61:
						return MetaType.s_metaDateTime;
					case 62:
						return MetaType.s_metaFloat;
					default:
						switch (tdsType)
						{
						case 98:
							return MetaType.s_metaVariant;
						case 99:
							return MetaType.MetaNText;
						case 100:
						case 101:
						case 102:
						case 103:
						case 105:
						case 107:
							goto IL_0279;
						case 104:
							break;
						case 106:
						case 108:
							return MetaType.MetaDecimal;
						case 109:
							if (4 != length)
							{
								return MetaType.s_metaFloat;
							}
							return MetaType.s_metaReal;
						case 110:
							if (4 != length)
							{
								return MetaType.s_metaMoney;
							}
							return MetaType.s_metaSmallMoney;
						case 111:
							if (4 != length)
							{
								return MetaType.s_metaDateTime;
							}
							return MetaType.s_metaSmallDateTime;
						default:
							goto IL_0279;
						}
						break;
					}
					return MetaType.s_metaBit;
				}
				if (tdsType == 122)
				{
					return MetaType.s_metaSmallMoney;
				}
				if (tdsType == 127)
				{
					return MetaType.s_metaBigInt;
				}
				if (tdsType != 165)
				{
					goto IL_0279;
				}
				return MetaType.MetaVarBinary;
			}
			else if (tdsType <= 173)
			{
				if (tdsType != 167)
				{
					if (tdsType != 173)
					{
						goto IL_0279;
					}
					goto IL_01CC;
				}
			}
			else
			{
				if (tdsType == 175)
				{
					goto IL_01E3;
				}
				if (tdsType == 231)
				{
					return MetaType.MetaNVarChar;
				}
				switch (tdsType)
				{
				case 239:
					return MetaType.s_metaNChar;
				case 240:
					return MetaType.MetaUdt;
				case 241:
					return MetaType.MetaXml;
				case 242:
					goto IL_0279;
				case 243:
					return MetaType.s_metaTable;
				default:
					goto IL_0279;
				}
			}
			IL_01C6:
			return MetaType.s_metaVarChar;
			IL_01CC:
			if (80U != userType)
			{
				return MetaType.s_metaBinary;
			}
			return MetaType.s_metaTimestamp;
			IL_01E3:
			return MetaType.s_metaChar;
			IL_0279:
			throw SQL.InvalidSqlDbType((SqlDbType)tdsType);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001F348 File Offset: 0x0001D548
		internal static MetaType GetDefaultMetaType()
		{
			return MetaType.MetaNVarChar;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001F350 File Offset: 0x0001D550
		internal static string GetStringFromXml(XmlReader xmlreader)
		{
			SqlXml sqlXml = new SqlXml(xmlreader);
			return sqlXml.Value;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001F36C File Offset: 0x0001D56C
		public static TdsDateTime FromDateTime(DateTime dateTime, byte cb)
		{
			TdsDateTime tdsDateTime = default(TdsDateTime);
			SqlDateTime sqlDateTime;
			if (cb == 8)
			{
				sqlDateTime = new SqlDateTime(dateTime);
				tdsDateTime.time = sqlDateTime.TimeTicks;
			}
			else
			{
				sqlDateTime = new SqlDateTime(dateTime.AddSeconds(30.0));
				tdsDateTime.time = sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute;
			}
			tdsDateTime.days = sqlDateTime.DayTicks;
			return tdsDateTime;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		public static DateTime ToDateTime(int sqlDays, int sqlTime, int length)
		{
			if (length == 4)
			{
				return new SqlDateTime(sqlDays, sqlTime * SqlDateTime.SQLTicksPerMinute).Value;
			}
			return new SqlDateTime(sqlDays, sqlTime).Value;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001F40E File Offset: 0x0001D60E
		internal static int GetTimeSizeFromScale(byte scale)
		{
			if (scale <= 2)
			{
				return 3;
			}
			if (scale <= 4)
			{
				return 4;
			}
			return 5;
		}

		// Token: 0x04000247 RID: 583
		internal readonly Type ClassType;

		// Token: 0x04000248 RID: 584
		internal readonly Type SqlType;

		// Token: 0x04000249 RID: 585
		internal readonly int FixedLength;

		// Token: 0x0400024A RID: 586
		internal readonly bool IsFixed;

		// Token: 0x0400024B RID: 587
		internal readonly bool IsLong;

		// Token: 0x0400024C RID: 588
		internal readonly bool IsPlp;

		// Token: 0x0400024D RID: 589
		internal readonly byte Precision;

		// Token: 0x0400024E RID: 590
		internal readonly byte Scale;

		// Token: 0x0400024F RID: 591
		internal readonly byte TDSType;

		// Token: 0x04000250 RID: 592
		internal readonly byte NullableType;

		// Token: 0x04000251 RID: 593
		internal readonly string TypeName;

		// Token: 0x04000252 RID: 594
		internal readonly SqlDbType SqlDbType;

		// Token: 0x04000253 RID: 595
		internal readonly DbType DbType;

		// Token: 0x04000254 RID: 596
		internal readonly byte PropBytes;

		// Token: 0x04000255 RID: 597
		internal readonly bool IsAnsiType;

		// Token: 0x04000256 RID: 598
		internal readonly bool IsBinType;

		// Token: 0x04000257 RID: 599
		internal readonly bool IsCharType;

		// Token: 0x04000258 RID: 600
		internal readonly bool IsNCharType;

		// Token: 0x04000259 RID: 601
		internal readonly bool IsSizeInCharacters;

		// Token: 0x0400025A RID: 602
		internal readonly bool Is2008Type;

		// Token: 0x0400025B RID: 603
		internal readonly bool IsVarTime;

		// Token: 0x0400025C RID: 604
		internal readonly bool Is70Supported;

		// Token: 0x0400025D RID: 605
		internal readonly bool Is80Supported;

		// Token: 0x0400025E RID: 606
		internal readonly bool Is90Supported;

		// Token: 0x0400025F RID: 607
		internal readonly bool Is100Supported;

		// Token: 0x04000260 RID: 608
		private static readonly MetaType s_metaBigInt = new MetaType(19, byte.MaxValue, 8, true, false, false, 127, 38, "bigint", typeof(long), typeof(SqlInt64), SqlDbType.BigInt, DbType.Int64, 0);

		// Token: 0x04000261 RID: 609
		private static readonly MetaType s_metaFloat = new MetaType(15, byte.MaxValue, 8, true, false, false, 62, 109, "float", typeof(double), typeof(SqlDouble), SqlDbType.Float, DbType.Double, 0);

		// Token: 0x04000262 RID: 610
		private static readonly MetaType s_metaReal = new MetaType(7, byte.MaxValue, 4, true, false, false, 59, 109, "real", typeof(float), typeof(SqlSingle), SqlDbType.Real, DbType.Single, 0);

		// Token: 0x04000263 RID: 611
		private static readonly MetaType s_metaBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 173, 173, "binary", typeof(byte[]), typeof(SqlBinary), SqlDbType.Binary, DbType.Binary, 2);

		// Token: 0x04000264 RID: 612
		private static readonly MetaType s_metaTimestamp = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 173, 173, "timestamp", typeof(byte[]), typeof(SqlBinary), SqlDbType.Timestamp, DbType.Binary, 2);

		// Token: 0x04000265 RID: 613
		internal static readonly MetaType MetaVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 165, 165, "varbinary", typeof(byte[]), typeof(SqlBinary), SqlDbType.VarBinary, DbType.Binary, 2);

		// Token: 0x04000266 RID: 614
		internal static readonly MetaType MetaMaxVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 165, 165, "varbinary", typeof(byte[]), typeof(SqlBinary), SqlDbType.VarBinary, DbType.Binary, 2);

		// Token: 0x04000267 RID: 615
		private static readonly MetaType s_metaSmallVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 37, 173, "", typeof(byte[]), typeof(SqlBinary), (SqlDbType)24, DbType.Binary, 2);

		// Token: 0x04000268 RID: 616
		internal static readonly MetaType MetaImage = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 34, 34, "image", typeof(byte[]), typeof(SqlBinary), SqlDbType.Image, DbType.Binary, 0);

		// Token: 0x04000269 RID: 617
		private static readonly MetaType s_metaBit = new MetaType(byte.MaxValue, byte.MaxValue, 1, true, false, false, 50, 104, "bit", typeof(bool), typeof(SqlBoolean), SqlDbType.Bit, DbType.Boolean, 0);

		// Token: 0x0400026A RID: 618
		private static readonly MetaType s_metaTinyInt = new MetaType(3, byte.MaxValue, 1, true, false, false, 48, 38, "tinyint", typeof(byte), typeof(SqlByte), SqlDbType.TinyInt, DbType.Byte, 0);

		// Token: 0x0400026B RID: 619
		private static readonly MetaType s_metaSmallInt = new MetaType(5, byte.MaxValue, 2, true, false, false, 52, 38, "smallint", typeof(short), typeof(SqlInt16), SqlDbType.SmallInt, DbType.Int16, 0);

		// Token: 0x0400026C RID: 620
		private static readonly MetaType s_metaInt = new MetaType(10, byte.MaxValue, 4, true, false, false, 56, 38, "int", typeof(int), typeof(SqlInt32), SqlDbType.Int, DbType.Int32, 0);

		// Token: 0x0400026D RID: 621
		private static readonly MetaType s_metaChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 175, 175, "char", typeof(string), typeof(SqlString), SqlDbType.Char, DbType.AnsiStringFixedLength, 7);

		// Token: 0x0400026E RID: 622
		private static readonly MetaType s_metaVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 167, 167, "varchar", typeof(string), typeof(SqlString), SqlDbType.VarChar, DbType.AnsiString, 7);

		// Token: 0x0400026F RID: 623
		internal static readonly MetaType MetaMaxVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 167, 167, "varchar", typeof(string), typeof(SqlString), SqlDbType.VarChar, DbType.AnsiString, 7);

		// Token: 0x04000270 RID: 624
		internal static readonly MetaType MetaText = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 35, 35, "text", typeof(string), typeof(SqlString), SqlDbType.Text, DbType.AnsiString, 0);

		// Token: 0x04000271 RID: 625
		private static readonly MetaType s_metaNChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 239, 239, "nchar", typeof(string), typeof(SqlString), SqlDbType.NChar, DbType.StringFixedLength, 7);

		// Token: 0x04000272 RID: 626
		internal static readonly MetaType MetaNVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 231, 231, "nvarchar", typeof(string), typeof(SqlString), SqlDbType.NVarChar, DbType.String, 7);

		// Token: 0x04000273 RID: 627
		internal static readonly MetaType MetaMaxNVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 231, 231, "nvarchar", typeof(string), typeof(SqlString), SqlDbType.NVarChar, DbType.String, 7);

		// Token: 0x04000274 RID: 628
		internal static readonly MetaType MetaNText = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 99, 99, "ntext", typeof(string), typeof(SqlString), SqlDbType.NText, DbType.String, 7);

		// Token: 0x04000275 RID: 629
		internal static readonly MetaType MetaDecimal = new MetaType(38, 4, 17, true, false, false, 108, 108, "decimal", typeof(decimal), typeof(SqlDecimal), SqlDbType.Decimal, DbType.Decimal, 2);

		// Token: 0x04000276 RID: 630
		internal static readonly MetaType MetaXml = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 241, 241, "xml", typeof(string), typeof(SqlXml), SqlDbType.Xml, DbType.Xml, 0);

		// Token: 0x04000277 RID: 631
		private static readonly MetaType s_metaDateTime = new MetaType(23, 3, 8, true, false, false, 61, 111, "datetime", typeof(DateTime), typeof(SqlDateTime), SqlDbType.DateTime, DbType.DateTime, 0);

		// Token: 0x04000278 RID: 632
		private static readonly MetaType s_metaSmallDateTime = new MetaType(16, 0, 4, true, false, false, 58, 111, "smalldatetime", typeof(DateTime), typeof(SqlDateTime), SqlDbType.SmallDateTime, DbType.DateTime, 0);

		// Token: 0x04000279 RID: 633
		private static readonly MetaType s_metaMoney = new MetaType(19, byte.MaxValue, 8, true, false, false, 60, 110, "money", typeof(decimal), typeof(SqlMoney), SqlDbType.Money, DbType.Currency, 0);

		// Token: 0x0400027A RID: 634
		private static readonly MetaType s_metaSmallMoney = new MetaType(10, byte.MaxValue, 4, true, false, false, 122, 110, "smallmoney", typeof(decimal), typeof(SqlMoney), SqlDbType.SmallMoney, DbType.Currency, 0);

		// Token: 0x0400027B RID: 635
		private static readonly MetaType s_metaUniqueId = new MetaType(byte.MaxValue, byte.MaxValue, 16, true, false, false, 36, 36, "uniqueidentifier", typeof(Guid), typeof(SqlGuid), SqlDbType.UniqueIdentifier, DbType.Guid, 0);

		// Token: 0x0400027C RID: 636
		private static readonly MetaType s_metaVariant = new MetaType(byte.MaxValue, byte.MaxValue, -1, true, false, false, 98, 98, "sql_variant", typeof(object), typeof(object), SqlDbType.Variant, DbType.Object, 0);

		// Token: 0x0400027D RID: 637
		internal static readonly MetaType MetaUdt = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, true, 240, 240, "udt", typeof(object), typeof(object), SqlDbType.Udt, DbType.Object, 0);

		// Token: 0x0400027E RID: 638
		private static readonly MetaType s_metaMaxUdt = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 240, 240, "udt", typeof(object), typeof(object), SqlDbType.Udt, DbType.Object, 0);

		// Token: 0x0400027F RID: 639
		private static readonly MetaType s_metaTable = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 243, 243, "table", typeof(IEnumerable<DbDataRecord>), typeof(IEnumerable<DbDataRecord>), SqlDbType.Structured, DbType.Object, 0);

		// Token: 0x04000280 RID: 640
		private static readonly MetaType s_metaSUDT = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 31, 31, "", typeof(Microsoft.Data.SqlClient.Server.SqlDataRecord), typeof(Microsoft.Data.SqlClient.Server.SqlDataRecord), SqlDbType.Structured, DbType.Object, 0);

		// Token: 0x04000281 RID: 641
		private static readonly MetaType s_metaDate = new MetaType(byte.MaxValue, byte.MaxValue, 3, true, false, false, 40, 40, "date", typeof(DateTime), typeof(DateTime), SqlDbType.Date, DbType.Date, 0);

		// Token: 0x04000282 RID: 642
		internal static readonly MetaType MetaTime = new MetaType(byte.MaxValue, 7, -1, false, false, false, 41, 41, "time", typeof(TimeSpan), typeof(TimeSpan), SqlDbType.Time, DbType.Time, 1);

		// Token: 0x04000283 RID: 643
		private static readonly MetaType s_metaDateTime2 = new MetaType(byte.MaxValue, 7, -1, false, false, false, 42, 42, "datetime2", typeof(DateTime), typeof(DateTime), SqlDbType.DateTime2, DbType.DateTime2, 1);

		// Token: 0x04000284 RID: 644
		internal static readonly MetaType MetaDateTimeOffset = new MetaType(byte.MaxValue, 7, -1, false, false, false, 43, 43, "datetimeoffset", typeof(DateTimeOffset), typeof(DateTimeOffset), SqlDbType.DateTimeOffset, DbType.DateTimeOffset, 1);

		// Token: 0x020001D0 RID: 464
		private static class MetaTypeName
		{
			// Token: 0x040013FD RID: 5117
			public const string BIGINT = "bigint";

			// Token: 0x040013FE RID: 5118
			public const string BINARY = "binary";

			// Token: 0x040013FF RID: 5119
			public const string BIT = "bit";

			// Token: 0x04001400 RID: 5120
			public const string CHAR = "char";

			// Token: 0x04001401 RID: 5121
			public const string DATETIME = "datetime";

			// Token: 0x04001402 RID: 5122
			public const string DECIMAL = "decimal";

			// Token: 0x04001403 RID: 5123
			public const string FLOAT = "float";

			// Token: 0x04001404 RID: 5124
			public const string IMAGE = "image";

			// Token: 0x04001405 RID: 5125
			public const string INT = "int";

			// Token: 0x04001406 RID: 5126
			public const string MONEY = "money";

			// Token: 0x04001407 RID: 5127
			public const string NCHAR = "nchar";

			// Token: 0x04001408 RID: 5128
			public const string NTEXT = "ntext";

			// Token: 0x04001409 RID: 5129
			public const string NVARCHAR = "nvarchar";

			// Token: 0x0400140A RID: 5130
			public const string REAL = "real";

			// Token: 0x0400140B RID: 5131
			public const string ROWGUID = "uniqueidentifier";

			// Token: 0x0400140C RID: 5132
			public const string SMALLDATETIME = "smalldatetime";

			// Token: 0x0400140D RID: 5133
			public const string SMALLINT = "smallint";

			// Token: 0x0400140E RID: 5134
			public const string SMALLMONEY = "smallmoney";

			// Token: 0x0400140F RID: 5135
			public const string TEXT = "text";

			// Token: 0x04001410 RID: 5136
			public const string TIMESTAMP = "timestamp";

			// Token: 0x04001411 RID: 5137
			public const string TINYINT = "tinyint";

			// Token: 0x04001412 RID: 5138
			public const string UDT = "udt";

			// Token: 0x04001413 RID: 5139
			public const string VARBINARY = "varbinary";

			// Token: 0x04001414 RID: 5140
			public const string VARCHAR = "varchar";

			// Token: 0x04001415 RID: 5141
			public const string VARIANT = "sql_variant";

			// Token: 0x04001416 RID: 5142
			public const string XML = "xml";

			// Token: 0x04001417 RID: 5143
			public const string TABLE = "table";

			// Token: 0x04001418 RID: 5144
			public const string DATE = "date";

			// Token: 0x04001419 RID: 5145
			public const string TIME = "time";

			// Token: 0x0400141A RID: 5146
			public const string DATETIME2 = "datetime2";

			// Token: 0x0400141B RID: 5147
			public const string DATETIMEOFFSET = "datetimeoffset";
		}
	}
}
