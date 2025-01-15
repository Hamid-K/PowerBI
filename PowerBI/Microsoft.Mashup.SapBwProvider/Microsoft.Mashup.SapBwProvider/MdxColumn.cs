using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000031 RID: 49
	[DataContract]
	public class MdxColumn
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000AF5C File Offset: 0x0000915C
		static MdxColumn()
		{
			foreach (KeyValuePair<string, SapBwDataType> keyValuePair in MdxColumn.DataTypesByName)
			{
				MdxColumn.DataTypeToName[keyValuePair.Value] = keyValuePair.Key;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B198 File Offset: 0x00009398
		public MdxColumn(string dataTypeName)
		{
			this.DataTypeName = dataTypeName;
			this.DataType = MdxColumn.ToDataType(dataTypeName, false, 0);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public MdxColumn(MdxColumnType mdxColumnType, string columnName, string dataTypeName, int length = 0, int? precision = null, string fieldName = null, int propertyKey = -1)
		{
			this.ColumnName = columnName;
			this.DataTypeName = dataTypeName;
			this.Length = length;
			this.Precision = precision;
			this.FieldName = fieldName;
			this.MdxColumnType = mdxColumnType;
			this.PropertyKey = propertyKey;
			this.DataType = MdxColumn.ToDataType(this.DataTypeName, mdxColumnType == MdxColumnType.KeyFigureValue, this.Length);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000B21B File Offset: 0x0000941B
		public bool IsKeyFigure
		{
			get
			{
				return this.MdxColumnType == MdxColumnType.KeyFigureValue;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000B226 File Offset: 0x00009426
		public bool IsCellProperty
		{
			get
			{
				return this.MdxColumnType == MdxColumnType.CellProperty || this.MdxColumnType == MdxColumnType.FormatStringCellProperty || this.MdxColumnType == MdxColumnType.UnitOfMeasureCellProperty || this.MdxColumnType == MdxColumnType.FormattedValueCellProperty;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000B24E File Offset: 0x0000944E
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000B256 File Offset: 0x00009456
		[DataMember]
		public int ColumnOrdinal { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B25F File Offset: 0x0000945F
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000B267 File Offset: 0x00009467
		[DataMember]
		public string ColumnName { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B270 File Offset: 0x00009470
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000B278 File Offset: 0x00009478
		[DataMember]
		public string FieldName { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B281 File Offset: 0x00009481
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000B289 File Offset: 0x00009489
		[DataMember]
		public SapBwDataType DataType { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B292 File Offset: 0x00009492
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000B29A File Offset: 0x0000949A
		[DataMember]
		public string DataTypeName { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B2A3 File Offset: 0x000094A3
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000B2AB File Offset: 0x000094AB
		[DataMember]
		public int Length { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B2B4 File Offset: 0x000094B4
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000B2BC File Offset: 0x000094BC
		[DataMember]
		public int? Precision { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B2C5 File Offset: 0x000094C5
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000B2CD File Offset: 0x000094CD
		[DataMember]
		public MdxColumnType MdxColumnType { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B2D6 File Offset: 0x000094D6
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000B2DE File Offset: 0x000094DE
		[DataMember]
		public int PropertyKey { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000B2E7 File Offset: 0x000094E7
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000B2EF File Offset: 0x000094EF
		[DataMember]
		public SapBwDataType? ValueType { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000B2F8 File Offset: 0x000094F8
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000B300 File Offset: 0x00009500
		[DataMember]
		public ValueProviderKind? ValueProviderKind { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B309 File Offset: 0x00009509
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000B311 File Offset: 0x00009511
		[DataMember]
		public int[] ValueProviderIndices { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B31C File Offset: 0x0000951C
		public Type ClrType
		{
			get
			{
				SapBwDataType dataType = this.DataType;
				if (dataType <= SapBwDataType.Fltp)
				{
					if (dataType == SapBwDataType.Dats)
					{
						return typeof(DateTime);
					}
					if (dataType != SapBwDataType.Fltp)
					{
						goto IL_0045;
					}
				}
				else if (dataType != SapBwDataType.Int4)
				{
					if (dataType != SapBwDataType.Tims)
					{
						goto IL_0045;
					}
					return typeof(TimeSpan);
				}
				return typeof(decimal);
				IL_0045:
				return typeof(string);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B378 File Offset: 0x00009578
		public bool TryExtractValue(string text, out object extractedValue)
		{
			if (this.ValueProviderKind != null)
			{
				extractedValue = null;
				return false;
			}
			extractedValue = this.ExtractValue(text);
			return true;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B3A4 File Offset: 0x000095A4
		private object ExtractValue(string text)
		{
			if (this.IsKeyFigure)
			{
				if (string.IsNullOrEmpty(text))
				{
					return DBNull.Value;
				}
				decimal num;
				if (decimal.TryParse(text, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B3F4 File Offset: 0x000095F4
		private static SapBwDataType ToDataType(string dataTypeString, bool isKeyFigure = false, int length = 0)
		{
			if (isKeyFigure && length == 22 && dataTypeString == "CHAR")
			{
				return SapBwDataType.Fltp;
			}
			SapBwDataType sapBwDataType;
			if (MdxColumn.DataTypesByName.TryGetValue(dataTypeString, out sapBwDataType))
			{
				return sapBwDataType;
			}
			if (!isKeyFigure)
			{
				return SapBwDataType.Char;
			}
			return SapBwDataType.Fltp;
		}

		// Token: 0x040001B6 RID: 438
		public static readonly Dictionary<string, SapBwDataType> DataTypesByName = new Dictionary<string, SapBwDataType>
		{
			{
				"ACCP",
				SapBwDataType.Accp
			},
			{
				"CHAR",
				SapBwDataType.Char
			},
			{
				"CLNT",
				SapBwDataType.Clnt
			},
			{
				"CUKY",
				SapBwDataType.Cuky
			},
			{
				"CURR",
				SapBwDataType.Curr
			},
			{
				"D16D",
				SapBwDataType.D16D
			},
			{
				"D16R",
				SapBwDataType.D16R
			},
			{
				"D16S",
				SapBwDataType.D16S
			},
			{
				"D34D",
				SapBwDataType.D34D
			},
			{
				"D34R",
				SapBwDataType.D34R
			},
			{
				"D34S",
				SapBwDataType.D34S
			},
			{
				"DATS",
				SapBwDataType.Dats
			},
			{
				"DEC,",
				SapBwDataType.Dec
			},
			{
				"FLTP",
				SapBwDataType.Fltp
			},
			{
				"INT1",
				SapBwDataType.Int1
			},
			{
				"INT2",
				SapBwDataType.Int2
			},
			{
				"INT4",
				SapBwDataType.Int4
			},
			{
				"LANG",
				SapBwDataType.Lang
			},
			{
				"LCHR",
				SapBwDataType.Lchr
			},
			{
				"LRAW",
				SapBwDataType.Lraw
			},
			{
				"NUMC",
				SapBwDataType.Numc
			},
			{
				"PREC",
				SapBwDataType.Prec
			},
			{
				"QUAN",
				SapBwDataType.Quan
			},
			{
				"RAW,",
				SapBwDataType.Raw
			},
			{
				"RSTR",
				SapBwDataType.Rstr
			},
			{
				"SSTR",
				SapBwDataType.Sstr
			},
			{
				"STRG",
				SapBwDataType.Strg
			},
			{
				"TIMS",
				SapBwDataType.Tims
			},
			{
				"UNIT",
				SapBwDataType.Unit
			},
			{
				"VARC",
				SapBwDataType.Varc
			}
		};

		// Token: 0x040001B7 RID: 439
		public static readonly Dictionary<SapBwDataType, string> DataTypeToName = new Dictionary<SapBwDataType, string>();

		// Token: 0x040001B8 RID: 440
		public static readonly Dictionary<OleDbType, SapBwDataType> OleDbTypeToSapBwType = new Dictionary<OleDbType, SapBwDataType>
		{
			{
				OleDbType.DBDate,
				SapBwDataType.Dats
			},
			{
				OleDbType.DBTime,
				SapBwDataType.Tims
			}
		};

		// Token: 0x040001B9 RID: 441
		public static readonly Dictionary<SapBwDataType, OleDbType> SapBwTypeToOleDbType = new Dictionary<SapBwDataType, OleDbType>
		{
			{
				SapBwDataType.Dats,
				OleDbType.DBDate
			},
			{
				SapBwDataType.Tims,
				OleDbType.DBTime
			}
		};
	}
}
