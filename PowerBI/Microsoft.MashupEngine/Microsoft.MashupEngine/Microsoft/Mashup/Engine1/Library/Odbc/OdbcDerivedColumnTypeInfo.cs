using System;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005EE RID: 1518
	internal class OdbcDerivedColumnTypeInfo
	{
		// Token: 0x06002FDC RID: 12252 RVA: 0x0009095B File Offset: 0x0008EB5B
		public OdbcDerivedColumnTypeInfo(OdbcTypeInfo typeInfo, bool isNullable, int? columnSize, int? decimalDigits)
		{
			this.DataSourceType = typeInfo;
			this.IsNullable = isNullable;
			this.ColumnSize = columnSize;
			this.DecimalDigits = decimalDigits;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x00090980 File Offset: 0x0008EB80
		private OdbcDerivedColumnTypeInfo(OdbcTypeInfo typeInfo, bool isNullable, int? columnSize, int? decimalDigits, TypeValue typeValue, ColumnConversion columnConversion)
			: this(typeInfo, isNullable, columnSize, decimalDigits)
		{
			this.typeValue = typeValue;
			this.ColumnConversion = columnConversion;
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x0009099D File Offset: 0x0008EB9D
		public OdbcDerivedColumnTypeInfo AsNullable
		{
			get
			{
				if (this.IsNullable)
				{
					return this;
				}
				return new OdbcDerivedColumnTypeInfo(this.DataSourceType, true, this.ColumnSize, this.DecimalDigits, this.TypeValue.Nullable, this.ColumnConversion);
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000909D2 File Offset: 0x0008EBD2
		public OdbcDerivedColumnTypeInfo AddColumnConversion(ColumnConversion conversion, TypeValue typeValue)
		{
			return new OdbcDerivedColumnTypeInfo(this.DataSourceType, this.IsNullable, this.ColumnSize, this.DecimalDigits, typeValue, conversion);
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x000909F3 File Offset: 0x0008EBF3
		public TypeValue TypeValue
		{
			get
			{
				if (this.typeValue == null)
				{
					this.typeValue = this.GetTypeValue();
				}
				return this.typeValue;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x00090A0F File Offset: 0x0008EC0F
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x00090A17 File Offset: 0x0008EC17
		public OdbcTypeInfo DataSourceType { get; private set; }

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x00090A20 File Offset: 0x0008EC20
		// (set) Token: 0x06002FE4 RID: 12260 RVA: 0x00090A28 File Offset: 0x0008EC28
		public int? ColumnSize { get; private set; }

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x00090A31 File Offset: 0x0008EC31
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x00090A39 File Offset: 0x0008EC39
		public int? DecimalDigits { get; private set; }

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x00090A42 File Offset: 0x0008EC42
		// (set) Token: 0x06002FE8 RID: 12264 RVA: 0x00090A4A File Offset: 0x0008EC4A
		public bool IsNullable { get; private set; }

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x00090A53 File Offset: 0x0008EC53
		// (set) Token: 0x06002FEA RID: 12266 RVA: 0x00090A5B File Offset: 0x0008EC5B
		public ColumnConversion ColumnConversion { get; private set; }

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x00090A64 File Offset: 0x0008EC64
		public bool CanBeUsedInDistinct
		{
			get
			{
				return this.DataSourceType.Searchable == Odbc32.SQL_SEARCHABLE.ALL_EXCEPT_LIKE || this.DataSourceType.Searchable == Odbc32.SQL_SEARCHABLE.SEARCHABLE;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x00090A84 File Offset: 0x0008EC84
		public bool CanBeUsedInSort
		{
			get
			{
				return this.CanBeUsedInDistinct;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x00090A84 File Offset: 0x0008EC84
		public bool CanBeUsedInGroupBy
		{
			get
			{
				return this.CanBeUsedInDistinct;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x00090A8C File Offset: 0x0008EC8C
		public bool IsWholeNumber
		{
			get
			{
				return OdbcTypeMap.FromSqlType(this.DataSourceType.SqlType).IsWholeNumber;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x00090AA4 File Offset: 0x0008ECA4
		public OdbcTypeMap OdbcTypeMap
		{
			get
			{
				Odbc32.SQL_TYPE sqlType = this.DataSourceType.SqlType;
				if (sqlType == Odbc32.SQL_TYPE.UNKNOWN)
				{
					return null;
				}
				return OdbcTypeMap.FromSqlType(sqlType);
			}
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x00090AC8 File Offset: 0x0008ECC8
		public bool IsComparable(OdbcDerivedColumnTypeInfo otherType)
		{
			if (this.DataSourceType.Equals(otherType.DataSourceType))
			{
				int? num = this.ColumnSize;
				int? num2 = otherType.ColumnSize;
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					num2 = this.DecimalDigits;
					num = otherType.DecimalDigits;
					return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
				}
			}
			return false;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x00090B4C File Offset: 0x0008ED4C
		private TypeValue GetTypeValue()
		{
			OdbcTypeMap odbcTypeMap = this.OdbcTypeMap;
			TypeValue typeValue = ((odbcTypeMap != null) ? odbcTypeMap.TypeValue : null);
			if (typeValue == null)
			{
				return TypeValue.Null;
			}
			if (!this.IsNullable)
			{
				return typeValue;
			}
			return typeValue.Nullable;
		}

		// Token: 0x04001514 RID: 5396
		private TypeValue typeValue;
	}
}
