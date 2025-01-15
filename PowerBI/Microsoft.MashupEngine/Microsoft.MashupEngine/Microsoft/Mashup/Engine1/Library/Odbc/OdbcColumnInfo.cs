using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005CB RID: 1483
	internal sealed class OdbcColumnInfo
	{
		// Token: 0x06002E57 RID: 11863 RVA: 0x0008D30C File Offset: 0x0008B50C
		public OdbcColumnInfo(OdbcTypeInfoCollection typeInfos, string name, int ordinal, Odbc32.SQL_TYPE sqlType, bool isNullable, string typeName, OdbcNumberPrecisionRadix? numberPrecisionRadix, int? columnSize, int? decimalDigits, string defaultValue, string remarks, OdbcIdentifier tableIdentifier)
		{
			this.typeInfos = typeInfos;
			this.name = name;
			this.Ordinal = ordinal;
			this.type = sqlType;
			this.isNullable = isNullable;
			this.typeName = typeName;
			this.numberPrecisionRadix = numberPrecisionRadix;
			this.columnSize = columnSize;
			this.decimalDigits = decimalDigits;
			this.defaultValue = defaultValue;
			this.remarks = remarks;
			this.tableIdentifier = tableIdentifier;
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x0008D37C File Offset: 0x0008B57C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x0008D384 File Offset: 0x0008B584
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x0008D38C File Offset: 0x0008B58C
		public int Ordinal { get; set; }

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x0008D395 File Offset: 0x0008B595
		public Odbc32.SQL_TYPE SqlType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x0008D39D File Offset: 0x0008B59D
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x0008D3A5 File Offset: 0x0008B5A5
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x0008D3AD File Offset: 0x0008B5AD
		public int? DecimalDigits
		{
			get
			{
				return this.decimalDigits;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x0008D3B5 File Offset: 0x0008B5B5
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x0008D3BD File Offset: 0x0008B5BD
		public OdbcNumberPrecisionRadix? NumberPrecisionRadix
		{
			get
			{
				return this.numberPrecisionRadix;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x0008D3C5 File Offset: 0x0008B5C5
		public string Remarks
		{
			get
			{
				return this.remarks;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x0008D3CD File Offset: 0x0008B5CD
		public int? ColumnSize
		{
			get
			{
				if (this.columnSize == null || this.columnSize.Value == 0)
				{
					this.columnSize = this.TypeInfo.ColumnSize;
				}
				return this.columnSize;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x0008D400 File Offset: 0x0008B600
		public OdbcTypeInfo TypeInfo
		{
			get
			{
				if (this.typeInfo == null && !this.typeInfos.TryGetType(this.SqlType, this.TypeName, out this.typeInfo))
				{
					if (OdbcTypeMap.FromSqlType(this.SqlType).TypeValue.TypeKind == ValueKind.Number)
					{
						this.typeValue = TypeValue.Number;
					}
					this.typeInfo = new OdbcTypeInfo(this.SqlType);
				}
				return this.typeInfo;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x0008D470 File Offset: 0x0008B670
		public TypeValue TypeValue
		{
			get
			{
				if (this.typeValue == null)
				{
					Odbc32.SQL_TYPE sqlType = this.SqlType;
					string text = this.TypeName;
					string text2 = this.DefaultValue;
					int? num = ((this.NumberPrecisionRadix != null) ? new int?((int)this.NumberPrecisionRadix.Value) : null);
					int? num2 = this.ColumnSize;
					this.typeValue = OdbcTypeValue.New(sqlType, text, text2, num, (num2 != null) ? new long?((long)num2.GetValueOrDefault()) : null, this.DecimalDigits, (this.TypeInfo.Name == null) ? null : new bool?(this.TypeInfo.Unsigned), new bool?(this.IsNullable), this.Remarks);
				}
				return this.typeValue;
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x0008D540 File Offset: 0x0008B740
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1} ({2}) <{3}>", new object[]
			{
				this.tableIdentifier.ToString(),
				this.Name,
				this.Ordinal,
				this.SqlType
			});
		}

		// Token: 0x0400145E RID: 5214
		private readonly OdbcTypeInfoCollection typeInfos;

		// Token: 0x0400145F RID: 5215
		private readonly string name;

		// Token: 0x04001460 RID: 5216
		private readonly Odbc32.SQL_TYPE type;

		// Token: 0x04001461 RID: 5217
		private readonly bool isNullable;

		// Token: 0x04001462 RID: 5218
		private readonly string typeName;

		// Token: 0x04001463 RID: 5219
		private readonly OdbcNumberPrecisionRadix? numberPrecisionRadix;

		// Token: 0x04001464 RID: 5220
		private readonly int? decimalDigits;

		// Token: 0x04001465 RID: 5221
		private readonly string defaultValue;

		// Token: 0x04001466 RID: 5222
		private readonly string remarks;

		// Token: 0x04001467 RID: 5223
		private readonly OdbcIdentifier tableIdentifier;

		// Token: 0x04001468 RID: 5224
		private int? columnSize;

		// Token: 0x04001469 RID: 5225
		private TypeValue typeValue;

		// Token: 0x0400146A RID: 5226
		private OdbcTypeInfo typeInfo;
	}
}
