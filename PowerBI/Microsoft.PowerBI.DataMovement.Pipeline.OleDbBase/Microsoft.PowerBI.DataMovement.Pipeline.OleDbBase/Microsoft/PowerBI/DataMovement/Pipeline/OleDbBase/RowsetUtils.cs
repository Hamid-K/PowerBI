using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C5 RID: 197
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class RowsetUtils
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00009FB0 File Offset: 0x000081B0
		internal static int[] CreateOrdinalIndices(this ColumnInfo[] columnInfos)
		{
			int[] array = new int[RowsetUtils.GetMaxOrdinal(columnInfos) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			for (int j = 0; j < columnInfos.Length; j++)
			{
				array[(int)(checked((IntPtr)columnInfos[j].Ordinal.Value))] = j;
			}
			return array;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A000 File Offset: 0x00008200
		internal static ColumnInfo[] ToColumnInfos(this DataTable schemaTable, IDictionary<Type, DBTYPE> columnTypeProjection = null)
		{
			ColumnInfo[] array = new ColumnInfo[schemaTable.Rows.Count];
			for (int i = 0; i < array.Length; i++)
			{
				string text = (string)schemaTable.Rows[i]["ColumnName"];
				Guid guid = (Guid)schemaTable.Rows[i]["ColumnGuid"];
				DBPROPID dbpropid = (DBPROPID)schemaTable.Rows[i]["ColumnPropId"];
				Type type = (Type)schemaTable.Rows[i]["DataType"];
				bool flag = (bool)schemaTable.Rows[i]["AllowDBNull"];
				int num = (int)schemaTable.Rows[i]["ColumnOrdinal"];
				DBTYPE dbtype = type.ToDbType(columnTypeProjection);
				RowsetUtils.TypeInfo typeInfo = RowsetUtils.TypeInfo.GetTypeInfo(dbtype);
				ColumnID columnID;
				if (guid == Guid.Empty)
				{
					columnID = ((dbpropid > (DBPROPID)0U) ? new ColumnID(dbpropid) : new ColumnID(text));
				}
				else
				{
					columnID = ((dbpropid > (DBPROPID)0U) ? new ColumnID(guid, dbpropid) : ((!string.IsNullOrEmpty(text)) ? new ColumnID(guid, text) : new ColumnID(guid)));
				}
				ColumnInfo columnInfo = new ColumnInfo(columnID, new DBORDINAL
				{
					Value = (ulong)num
				}, typeInfo.ColumnFlags | (flag ? ((DBCOLUMNFLAGS)96U) : DBCOLUMNFLAGS.NONE), typeInfo.Length, dbtype, 0, 0);
				array[i] = columnInfo;
			}
			return array;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A17C File Offset: 0x0000837C
		internal static DBTYPE ToDbType(this Type type, IDictionary<Type, DBTYPE> columnTypeProjection = null)
		{
			DBTYPE dbtype;
			if (columnTypeProjection != null && columnTypeProjection.TryGetValue(type, out dbtype))
			{
				return dbtype;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return DBTYPE.BOOL;
			case TypeCode.SByte:
				return DBTYPE.I1;
			case TypeCode.Byte:
				return DBTYPE.UI1;
			case TypeCode.Int16:
				return DBTYPE.I2;
			case TypeCode.UInt16:
				return DBTYPE.UI2;
			case TypeCode.Int32:
				return DBTYPE.I4;
			case TypeCode.UInt32:
				return DBTYPE.UI4;
			case TypeCode.Int64:
				return DBTYPE.I8;
			case TypeCode.UInt64:
				return DBTYPE.UI8;
			case TypeCode.Single:
				return DBTYPE.R4;
			case TypeCode.Double:
				return DBTYPE.R8;
			case TypeCode.Decimal:
				return DBTYPE.DECIMAL;
			case TypeCode.DateTime:
				return DBTYPE.DATE;
			case TypeCode.String:
				return DBTYPE.WSTR;
			}
			if (type == typeof(Guid))
			{
				return DBTYPE.GUID;
			}
			if (type == typeof(DateTimeOffset))
			{
				return DBTYPE.DBTIMESTAMPOFFSET;
			}
			if (type == typeof(TimeSpan))
			{
				return DBTYPE.DBDURATION;
			}
			if (type == typeof(object))
			{
				return DBTYPE.VARIANT;
			}
			if (type == typeof(ErrorWrapper))
			{
				return DBTYPE.ERROR;
			}
			if (type == typeof(byte[]))
			{
				return DBTYPE.BYTES;
			}
			if (type == typeof(Currency))
			{
				return DBTYPE.CY;
			}
			if (type == typeof(Time))
			{
				return DBTYPE.DBTIME2;
			}
			if (type == typeof(Date))
			{
				return DBTYPE.DATE;
			}
			if (type == typeof(Number))
			{
				return DBTYPE.NUMERIC;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A2F8 File Offset: 0x000084F8
		internal unsafe static void GetData(IPage page, int[] columnOrdinalIndecies, Binding[] bindings, IDataConvert dataConvert, int row, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destBuffer)
		{
			for (int i = 0; i < bindings.Length; i++)
			{
				RowsetUtils.ClearBinding(bindings[i], destBuffer);
			}
			try
			{
				foreach (Binding binding in bindings)
				{
					DBPART part = binding.Part;
					int num = columnOrdinalIndecies[(int)binding.Ordinal.Value];
					IColumn column = page.GetColumn(num);
					DBLENGTH dblength;
					DBSTATUS dbstatus;
					if ((part & DBPART.VALUE) == DBPART.VALUE)
					{
						dbstatus = column.GetValue(row, dataConvert, binding, destBuffer + binding.ValueOffset.Value, out dblength);
					}
					else
					{
						dbstatus = (column.IsNull(row) ? DBSTATUS.S_ISNULL : DBSTATUS.S_OK);
						dblength = new DBLENGTH
						{
							Value = 0UL
						};
					}
					if ((part & DBPART.LENGTH) == DBPART.LENGTH)
					{
						DBLENGTH* ptr = (DBLENGTH*)(destBuffer + binding.LengthOffset.Value);
						*ptr = ((dbstatus == DBSTATUS.S_ISNULL) ? DbLength.Zero : dblength);
					}
					if ((part & DBPART.STATUS) == DBPART.STATUS)
					{
						DBSTATUS* ptr2 = (DBSTATUS*)(destBuffer + binding.StatusOffset.Value);
						*ptr2 = dbstatus;
					}
				}
			}
			catch
			{
				for (int k = 0; k < bindings.Length; k++)
				{
					RowsetUtils.FreeBinding(bindings[k], destBuffer);
				}
				throw;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A41C File Offset: 0x0000861C
		private static int GetMaxOrdinal(ColumnInfo[] columnInfos)
		{
			int num = -1;
			for (int i = 0; i < columnInfos.Length; i++)
			{
				num = Math.Max(num, (int)columnInfos[i].Ordinal.Value);
			}
			return num;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A450 File Offset: 0x00008650
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private unsafe static void ClearBinding([global::System.Runtime.CompilerServices.Nullable(1)] Binding binding, byte* destBuffer)
		{
			if ((binding.Part & DBPART.VALUE) == DBPART.VALUE)
			{
				void* ptr = (void*)(destBuffer + binding.ValueOffset.Value);
				if (binding.DestType == DBTYPE.BSTR)
				{
					*(IntPtr*)ptr = (IntPtr)((UIntPtr)0);
					return;
				}
				if (binding.DestType == DBTYPE.VARIANT)
				{
					Variant.Init((VARIANT*)ptr);
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A498 File Offset: 0x00008698
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private unsafe static void FreeBinding([global::System.Runtime.CompilerServices.Nullable(1)] Binding binding, byte* destBuffer)
		{
			if ((binding.Part & DBPART.VALUE) == DBPART.VALUE)
			{
				void* ptr = (void*)(destBuffer + binding.ValueOffset.Value);
				if (binding.DestType == DBTYPE.BSTR)
				{
					Marshal.FreeBSTR(new IntPtr(*(IntPtr*)ptr));
					return;
				}
				if (binding.DestType == DBTYPE.VARIANT)
				{
					Variant.Clear((VARIANT*)ptr);
				}
			}
		}

		// Token: 0x020000F4 RID: 244
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal struct TypeInfo
		{
			// Token: 0x060004DA RID: 1242 RVA: 0x0000EE88 File Offset: 0x0000D088
			public TypeInfo(DBLENGTH length, DBCOLUMNFLAGS columnFlags)
			{
				this.length = length;
				this.columnFlags = columnFlags;
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000EE98 File Offset: 0x0000D098
			public static RowsetUtils.TypeInfo Boolean
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.VariantBool, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
			public static RowsetUtils.TypeInfo UI1
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.One, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
			public static RowsetUtils.TypeInfo I1
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.One, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
			public static RowsetUtils.TypeInfo I2
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Two, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000EED0 File Offset: 0x0000D0D0
			public static RowsetUtils.TypeInfo I4
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Four, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000EEDE File Offset: 0x0000D0DE
			public static RowsetUtils.TypeInfo I8
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Eight, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000EEEC File Offset: 0x0000D0EC
			public static RowsetUtils.TypeInfo R4
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Four, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000EEFA File Offset: 0x0000D0FA
			public static RowsetUtils.TypeInfo R8
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Eight, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000EF08 File Offset: 0x0000D108
			public static RowsetUtils.TypeInfo UI2
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Two, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000EF16 File Offset: 0x0000D116
			public static RowsetUtils.TypeInfo UI4
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Four, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000EF24 File Offset: 0x0000D124
			public static RowsetUtils.TypeInfo UI8
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Eight, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000EF32 File Offset: 0x0000D132
			public static RowsetUtils.TypeInfo Currency
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Currency, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000EF40 File Offset: 0x0000D140
			public static RowsetUtils.TypeInfo Decimal
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Decimal, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000EF4E File Offset: 0x0000D14E
			public static RowsetUtils.TypeInfo Guid
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Guid, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000EF5C File Offset: 0x0000D15C
			public static RowsetUtils.TypeInfo TimeStamp
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.TimeStamp, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000EF6A File Offset: 0x0000D16A
			public static RowsetUtils.TypeInfo TimeStampOffset
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.TimeStampOffset, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000EF78 File Offset: 0x0000D178
			public static RowsetUtils.TypeInfo Duration
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Duration, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000EF86 File Offset: 0x0000D186
			public static RowsetUtils.TypeInfo String
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.MaxValue, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000EF93 File Offset: 0x0000D193
			public static RowsetUtils.TypeInfo Variant
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Variant, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
			public static RowsetUtils.TypeInfo Error
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Error, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000EFAD File Offset: 0x0000D1AD
			public static RowsetUtils.TypeInfo Numeric
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Numeric, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000EFBB File Offset: 0x0000D1BB
			public static RowsetUtils.TypeInfo DbTime2
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.DbTime2, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
			public static RowsetUtils.TypeInfo DbDate
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.DbDate, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000EFD7 File Offset: 0x0000D1D7
			public static RowsetUtils.TypeInfo Date
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.Date, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000EFE5 File Offset: 0x0000D1E5
			public static RowsetUtils.TypeInfo Binary
			{
				get
				{
					return new RowsetUtils.TypeInfo(DbLength.MaxValue, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000EFF2 File Offset: 0x0000D1F2
			public DBLENGTH Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000EFFA File Offset: 0x0000D1FA
			public DBCOLUMNFLAGS ColumnFlags
			{
				get
				{
					return this.columnFlags;
				}
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x0000F004 File Offset: 0x0000D204
			public static RowsetUtils.TypeInfo GetTypeInfo(DBTYPE type)
			{
				if (type <= DBTYPE.DBTIMESTAMP)
				{
					switch (type)
					{
					case DBTYPE.I2:
						return RowsetUtils.TypeInfo.I2;
					case DBTYPE.I4:
						return RowsetUtils.TypeInfo.I4;
					case DBTYPE.R4:
						return RowsetUtils.TypeInfo.R4;
					case DBTYPE.R8:
						return RowsetUtils.TypeInfo.R8;
					case DBTYPE.CY:
						return RowsetUtils.TypeInfo.Currency;
					case DBTYPE.DATE:
						return RowsetUtils.TypeInfo.Date;
					case DBTYPE.BSTR:
					case DBTYPE.IDISPATCH:
					case DBTYPE.IUNKNOWN:
					case (DBTYPE)15:
						break;
					case DBTYPE.ERROR:
						return RowsetUtils.TypeInfo.Error;
					case DBTYPE.BOOL:
						return RowsetUtils.TypeInfo.Boolean;
					case DBTYPE.VARIANT:
						return RowsetUtils.TypeInfo.Variant;
					case DBTYPE.DECIMAL:
						return RowsetUtils.TypeInfo.Decimal;
					case DBTYPE.I1:
						return RowsetUtils.TypeInfo.I1;
					case DBTYPE.UI1:
						return RowsetUtils.TypeInfo.UI1;
					case DBTYPE.UI2:
						return RowsetUtils.TypeInfo.UI2;
					case DBTYPE.UI4:
						return RowsetUtils.TypeInfo.UI4;
					case DBTYPE.I8:
						return RowsetUtils.TypeInfo.I8;
					case DBTYPE.UI8:
						return RowsetUtils.TypeInfo.UI8;
					default:
						if (type == DBTYPE.GUID)
						{
							return RowsetUtils.TypeInfo.Guid;
						}
						switch (type)
						{
						case DBTYPE.BYTES:
							return RowsetUtils.TypeInfo.Binary;
						case DBTYPE.WSTR:
							return RowsetUtils.TypeInfo.String;
						case DBTYPE.NUMERIC:
							return RowsetUtils.TypeInfo.Numeric;
						case DBTYPE.DBDATE:
							return RowsetUtils.TypeInfo.DbDate;
						case DBTYPE.DBTIMESTAMP:
							return RowsetUtils.TypeInfo.TimeStamp;
						}
						break;
					}
				}
				else
				{
					if (type == DBTYPE.DBTIME2)
					{
						return RowsetUtils.TypeInfo.DbTime2;
					}
					if (type == DBTYPE.DBTIMESTAMPOFFSET)
					{
						return RowsetUtils.TypeInfo.TimeStampOffset;
					}
					if (type == DBTYPE.DBDURATION)
					{
						return RowsetUtils.TypeInfo.Duration;
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x0400041A RID: 1050
			private readonly DBLENGTH length;

			// Token: 0x0400041B RID: 1051
			private readonly DBCOLUMNFLAGS columnFlags;
		}
	}
}
