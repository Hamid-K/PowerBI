using System;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000131 RID: 305
	public sealed class SqlMetaData
	{
		// Token: 0x060017E8 RID: 6120 RVA: 0x00062FFA File Offset: 0x000611FA
		public SqlMetaData(string name, SqlDbType dbType)
		{
			this.Construct(name, dbType, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0006300E File Offset: 0x0006120E
		public SqlMetaData(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00063025 File Offset: 0x00061225
		public SqlMetaData(string name, SqlDbType dbType, long maxLength)
		{
			this.Construct(name, dbType, maxLength, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0006303A File Offset: 0x0006123A
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00063054 File Offset: 0x00061254
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType)
		{
			this.Construct(name, dbType, userDefinedType, null, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00063078 File Offset: 0x00061278
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0006309C File Offset: 0x0006129C
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000630C4 File Offset: 0x000612C4
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale)
		{
			this.Construct(name, dbType, precision, scale, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000630E8 File Offset: 0x000612E8
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00063110 File Offset: 0x00061310
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00063134 File Offset: 0x00061334
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0006315C File Offset: 0x0006135C
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00063184 File Offset: 0x00061384
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long locale, SqlCompareOptions compareOptions, Type userDefinedType)
			: this(name, dbType, maxLength, precision, scale, locale, compareOptions, userDefinedType, false, false, SortOrder.Unspecified, -1)
		{
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000631A8 File Offset: 0x000613A8
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			switch (dbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Float:
			case SqlDbType.Image:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.Timestamp:
			case SqlDbType.TinyInt:
			case SqlDbType.Xml:
			case SqlDbType.Date:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this.Construct(name, dbType, maxLength, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Decimal:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this.Construct(name, dbType, SqlMetaData.Max, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Variant:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Udt:
				this.Construct(name, dbType, userDefinedType, string.Empty, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			}
			throw SQL.InvalidSqlDbTypeForConstructor(dbType);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000632EC File Offset: 0x000614EC
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00063310 File Offset: 0x00061510
		internal SqlMetaData(string name, SqlDbType sqlDBType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName, bool partialLength, Type udtType)
		{
			this.AssertNameIsValid(name);
			this._name = name;
			this._sqlDbType = sqlDBType;
			this._maxLength = maxLength;
			this._precision = precision;
			this._scale = scale;
			this._locale = localeId;
			this._compareOptions = compareOptions;
			this._xmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this._xmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this._xmlSchemaCollectionName = xmlSchemaCollectionName;
			this._partialLength = partialLength;
			this._udtType = udtType;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00063388 File Offset: 0x00061588
		private SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool partialLength)
		{
			this.AssertNameIsValid(name);
			this._name = name;
			this._sqlDbType = sqlDbType;
			this._maxLength = maxLength;
			this._precision = precision;
			this._scale = scale;
			this._locale = localeId;
			this._compareOptions = compareOptions;
			this._partialLength = partialLength;
			this._udtType = null;
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x000633E6 File Offset: 0x000615E6
		public SqlCompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x000633EE File Offset: 0x000615EE
		public DbType DbType
		{
			get
			{
				return SqlMetaData.s_sqlDbTypeToDbType[(int)this._sqlDbType];
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x000633FC File Offset: 0x000615FC
		public bool IsUniqueKey
		{
			get
			{
				return this._isUniqueKey;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00063404 File Offset: 0x00061604
		public long LocaleId
		{
			get
			{
				return this._locale;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0006340C File Offset: 0x0006160C
		public static long Max
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00063410 File Offset: 0x00061610
		public long MaxLength
		{
			get
			{
				return this._maxLength;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00063418 File Offset: 0x00061618
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00063420 File Offset: 0x00061620
		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00063428 File Offset: 0x00061628
		public byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00063430 File Offset: 0x00061630
		public SortOrder SortOrder
		{
			get
			{
				return this._columnSortOrder;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00063438 File Offset: 0x00061638
		public int SortOrdinal
		{
			get
			{
				return this._sortOrdinal;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00063440 File Offset: 0x00061640
		public SqlDbType SqlDbType
		{
			get
			{
				return this._sqlDbType;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00063448 File Offset: 0x00061648
		public Type Type
		{
			get
			{
				return this._udtType;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00063450 File Offset: 0x00061650
		public string TypeName
		{
			get
			{
				if (this._serverTypeName != null)
				{
					return this._serverTypeName;
				}
				if (this.SqlDbType == SqlDbType.Udt)
				{
					return this.UdtTypeName;
				}
				return SqlMetaData.s_defaults[(int)this.SqlDbType].Name;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00063483 File Offset: 0x00061683
		internal string ServerTypeName
		{
			get
			{
				return this._serverTypeName;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0006348B File Offset: 0x0006168B
		public bool UseServerDefault
		{
			get
			{
				return this._useServerDefault;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00063493 File Offset: 0x00061693
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this._xmlSchemaCollectionDatabase;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x0006349B File Offset: 0x0006169B
		public string XmlSchemaCollectionName
		{
			get
			{
				return this._xmlSchemaCollectionName;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000634A3 File Offset: 0x000616A3
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this._xmlSchemaCollectionOwningSchema;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000634AB File Offset: 0x000616AB
		internal bool IsPartialLength
		{
			get
			{
				return this._partialLength;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000634B3 File Offset: 0x000616B3
		internal string UdtTypeName
		{
			get
			{
				if (this.SqlDbType != SqlDbType.Udt)
				{
					return null;
				}
				if (this._udtType == null)
				{
					return null;
				}
				return this._udtType.FullName;
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000634DC File Offset: 0x000616DC
		private void Construct(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (dbType != SqlDbType.BigInt && SqlDbType.Bit != dbType && SqlDbType.DateTime != dbType && SqlDbType.Date != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType && SqlDbType.Decimal != dbType && SqlDbType.Float != dbType && SqlDbType.Image != dbType && SqlDbType.Int != dbType && SqlDbType.Money != dbType && SqlDbType.NText != dbType && SqlDbType.Real != dbType && SqlDbType.SmallDateTime != dbType && SqlDbType.SmallInt != dbType && SqlDbType.SmallMoney != dbType && SqlDbType.Text != dbType && SqlDbType.Time != dbType && SqlDbType.Timestamp != dbType && SqlDbType.TinyInt != dbType && SqlDbType.UniqueIdentifier != dbType && SqlDbType.Variant != dbType && SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			this.SetDefaultsForType(dbType);
			if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				this._locale = (long)CultureInfo.CurrentCulture.LCID;
			}
			this._name = name;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x000635B4 File Offset: 0x000617B4
		private void Construct(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			long num = 0L;
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.Binary == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.VarBinary == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.Image != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			this.SetDefaultsForType(dbType);
			this._name = name;
			this._maxLength = maxLength;
			this._locale = num;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00063868 File Offset: 0x00061A68
		private void Construct(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.NText != dbType && SqlDbType.Text != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataLength2, new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			if (SqlCompareOptions.BinarySort != compareOptions && (~(SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth) & compareOptions) != SqlCompareOptions.None)
			{
				throw ADP.InvalidEnumerationValue(typeof(SqlCompareOptions), (int)compareOptions);
			}
			this.SetDefaultsForType(dbType);
			this._name = name;
			this._maxLength = maxLength;
			this._locale = locale;
			this._compareOptions = compareOptions;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00063A40 File Offset: 0x00061C40
		private void Construct(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Decimal == dbType)
			{
				if (precision > SqlDecimal.MaxPrecision || scale > precision)
				{
					throw SQL.PrecisionValueOutOfRange(precision);
				}
				if (scale > SqlDecimal.MaxScale)
				{
					throw SQL.ScaleValueOutOfRange(scale);
				}
			}
			else
			{
				if (SqlDbType.Time != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (scale > 7)
				{
					throw SQL.TimeScaleValueOutOfRange(scale);
				}
			}
			this.SetDefaultsForType(dbType);
			this._name = name;
			this._precision = precision;
			this._scale = scale;
			if (SqlDbType.Decimal == dbType)
			{
				this._maxLength = (long)((ulong)SqlMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
			}
			else
			{
				this._maxLength -= (long)((ulong)SqlMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
			}
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00063B14 File Offset: 0x00061D14
		private void Construct(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Udt != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if (null == userDefinedType)
			{
				throw ADP.ArgumentNull("userDefinedType");
			}
			this.SetDefaultsForType(SqlDbType.Udt);
			this._name = name;
			this._maxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
			this._udtType = userDefinedType;
			this._serverTypeName = serverTypeName;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00063BA0 File Offset: 0x00061DA0
		private void Construct(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if ((database != null || owningSchema != null) && objectName == null)
			{
				throw ADP.ArgumentNull("objectName");
			}
			this.SetDefaultsForType(SqlDbType.Xml);
			this._name = name;
			this._xmlSchemaCollectionDatabase = database;
			this._xmlSchemaCollectionOwningSchema = owningSchema;
			this._xmlSchemaCollectionName = objectName;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00063C26 File Offset: 0x00061E26
		private void AssertNameIsValid(string name)
		{
			if (name == null)
			{
				throw ADP.ArgumentNull("name");
			}
			if (128L < (long)name.Length)
			{
				throw SQL.NameTooLong("name");
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00063C50 File Offset: 0x00061E50
		private void ValidateSortOrder(SortOrder columnSortOrder, int sortOrdinal)
		{
			if (SortOrder.Unspecified != columnSortOrder && columnSortOrder != SortOrder.Ascending && SortOrder.Descending != columnSortOrder)
			{
				throw SQL.InvalidSortOrder(columnSortOrder);
			}
			if (SortOrder.Unspecified == columnSortOrder != (-1 == sortOrdinal))
			{
				throw SQL.MustSpecifyBothSortOrderAndOrdinal(columnSortOrder, sortOrdinal);
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00063C76 File Offset: 0x00061E76
		public short Adjust(short value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00063C88 File Offset: 0x00061E88
		public int Adjust(int value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00063C99 File Offset: 0x00061E99
		public long Adjust(long value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00063CA9 File Offset: 0x00061EA9
		public float Adjust(float value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00063CBB File Offset: 0x00061EBB
		public double Adjust(double value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00063CCC File Offset: 0x00061ECC
		public string Adjust(string value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					value = value.PadRight((int)this.MaxLength);
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = value.Remove((int)this.MaxLength, (int)((long)value.Length - this.MaxLength));
			}
			return value;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00063D7C File Offset: 0x00061F7C
		public decimal Adjust(decimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType && SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				this.VerifyMoneyRange(new SqlMoney(value));
				return value;
			}
			return this.InternalAdjustSqlDecimal(new SqlDecimal(value)).Value;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00063DD8 File Offset: 0x00061FD8
		public DateTime Adjust(DateTime value)
		{
			if (SqlDbType.DateTime == this.SqlDbType || SqlDbType.SmallDateTime == this.SqlDbType)
			{
				this.VerifyDateTimeRange(value);
			}
			else
			{
				if (SqlDbType.DateTime2 == this.SqlDbType)
				{
					return new DateTime(this.InternalAdjustTimeTicks(value.Ticks));
				}
				if (SqlDbType.Date == this.SqlDbType)
				{
					return value.Date;
				}
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00063E36 File Offset: 0x00062036
		public Guid Adjust(Guid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00063E48 File Offset: 0x00062048
		public SqlBoolean Adjust(SqlBoolean value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00063E59 File Offset: 0x00062059
		public SqlByte Adjust(SqlByte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00063C76 File Offset: 0x00061E76
		public SqlInt16 Adjust(SqlInt16 value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00063C88 File Offset: 0x00061E88
		public SqlInt32 Adjust(SqlInt32 value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00063C99 File Offset: 0x00061E99
		public SqlInt64 Adjust(SqlInt64 value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00063CA9 File Offset: 0x00061EA9
		public SqlSingle Adjust(SqlSingle value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00063CBB File Offset: 0x00061EBB
		public SqlDouble Adjust(SqlDouble value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00063E6B File Offset: 0x0006206B
		public SqlMoney Adjust(SqlMoney value)
		{
			if (SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyMoneyRange(value);
			}
			return value;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00063E97 File Offset: 0x00062097
		public SqlDateTime Adjust(SqlDateTime value)
		{
			if (SqlDbType.DateTime != this.SqlDbType && SqlDbType.SmallDateTime != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyDateTimeRange(value.Value);
			}
			return value;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00063EC8 File Offset: 0x000620C8
		public SqlDecimal Adjust(SqlDecimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return this.InternalAdjustSqlDecimal(value);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00063EE0 File Offset: 0x000620E0
		public SqlString Adjust(SqlString value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Value.Length < this.MaxLength)
				{
					return new SqlString(value.Value.PadRight((int)this.MaxLength));
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = new SqlString(value.Value.Remove((int)this.MaxLength, (int)((long)value.Value.Length - this.MaxLength)));
			}
			return value;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00063FC4 File Offset: 0x000621C4
		public SqlBinary Adjust(SqlBinary value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Length < this.MaxLength)
				{
					byte[] value2 = value.Value;
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value2, 0, array, 0, value2.Length);
					Array.Clear(array, value2.Length, array.Length - value2.Length);
					return new SqlBinary(array);
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				byte[] value3 = value.Value;
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value3, 0, array2, 0, (int)this.MaxLength);
				value = new SqlBinary(array2);
			}
			return value;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00063E36 File Offset: 0x00062036
		public SqlGuid Adjust(SqlGuid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000640A4 File Offset: 0x000622A4
		public SqlChars Adjust(SqlChars value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					long length = value.Length;
					if (length < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							char[] array = new char[(int)this.MaxLength];
							Array.Copy(value.Buffer, array, (int)length);
							value = new SqlChars(array);
						}
						char[] buffer = value.Buffer;
						for (long num = length; num < this.MaxLength; num += 1L)
						{
							buffer[(int)(checked((IntPtr)num))] = ' ';
						}
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000641AC File Offset: 0x000623AC
		public SqlBytes Adjust(SqlBytes value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					int num = (int)value.Length;
					if ((long)num < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							byte[] array = new byte[this.MaxLength];
							Buffer.BlockCopy(value.Buffer, 0, array, 0, num);
							value = new SqlBytes(array);
						}
						byte[] buffer = value.Buffer;
						Array.Clear(buffer, num, buffer.Length - num);
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0006428C File Offset: 0x0006248C
		public SqlXml Adjust(SqlXml value)
		{
			if (SqlDbType.Xml != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0006429E File Offset: 0x0006249E
		public TimeSpan Adjust(TimeSpan value)
		{
			if (SqlDbType.Time != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			this.VerifyTimeRange(value);
			return new TimeSpan(this.InternalAdjustTimeTicks(value.Ticks));
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000642C8 File Offset: 0x000624C8
		public DateTimeOffset Adjust(DateTimeOffset value)
		{
			if (SqlDbType.DateTimeOffset != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return new DateTimeOffset(this.InternalAdjustTimeTicks(value.Ticks), value.Offset);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000642F4 File Offset: 0x000624F4
		public object Adjust(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					return this.Adjust((byte[])value);
				}
				if (type == typeof(char[]))
				{
					return this.Adjust((char[])value);
				}
				if (type == typeof(Guid))
				{
					return this.Adjust((Guid)value);
				}
				if (type == typeof(object))
				{
					throw ADP.InvalidDataType(TypeCode.UInt64);
				}
				if (type == typeof(SqlBinary))
				{
					return this.Adjust((SqlBinary)value);
				}
				if (type == typeof(SqlBoolean))
				{
					return this.Adjust((SqlBoolean)value);
				}
				if (type == typeof(SqlByte))
				{
					return this.Adjust((SqlByte)value);
				}
				if (type == typeof(SqlDateTime))
				{
					return this.Adjust((SqlDateTime)value);
				}
				if (type == typeof(SqlDouble))
				{
					return this.Adjust((SqlDouble)value);
				}
				if (type == typeof(SqlGuid))
				{
					return this.Adjust((SqlGuid)value);
				}
				if (type == typeof(SqlInt16))
				{
					return this.Adjust((SqlInt16)value);
				}
				if (type == typeof(SqlInt32))
				{
					return this.Adjust((SqlInt32)value);
				}
				if (type == typeof(SqlInt64))
				{
					return this.Adjust((SqlInt64)value);
				}
				if (type == typeof(SqlMoney))
				{
					return this.Adjust((SqlMoney)value);
				}
				if (type == typeof(SqlDecimal))
				{
					return this.Adjust((SqlDecimal)value);
				}
				if (type == typeof(SqlSingle))
				{
					return this.Adjust((SqlSingle)value);
				}
				if (type == typeof(SqlString))
				{
					return this.Adjust((SqlString)value);
				}
				if (type == typeof(SqlChars))
				{
					return this.Adjust((SqlChars)value);
				}
				if (type == typeof(SqlBytes))
				{
					return this.Adjust((SqlBytes)value);
				}
				if (type == typeof(SqlXml))
				{
					return this.Adjust((SqlXml)value);
				}
				if (type == typeof(TimeSpan))
				{
					return this.Adjust((TimeSpan)value);
				}
				if (type == typeof(DateTimeOffset))
				{
					return this.Adjust((DateTimeOffset)value);
				}
				throw ADP.UnknownDataType(type);
			case TypeCode.DBNull:
				return value;
			case TypeCode.Boolean:
				return this.Adjust((bool)value);
			case TypeCode.Char:
				return this.Adjust((char)value);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return this.Adjust((byte)value);
			case TypeCode.Int16:
				return this.Adjust((short)value);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return this.Adjust((int)value);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return this.Adjust((long)value);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return this.Adjust((float)value);
			case TypeCode.Double:
				return this.Adjust((double)value);
			case TypeCode.Decimal:
				return this.Adjust((decimal)value);
			case TypeCode.DateTime:
				return this.Adjust((DateTime)value);
			case TypeCode.String:
				return this.Adjust((string)value);
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00064814 File Offset: 0x00062A14
		public static SqlMetaData InferFromValue(object value, string name)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull("value");
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					long num = (long)((byte[])value).Length;
					if (num < 1L)
					{
						num = 1L;
					}
					if (8000L < num)
					{
						num = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num);
				}
				if (type == typeof(char[]))
				{
					long num2 = (long)((char[])value).Length;
					if (num2 < 1L)
					{
						num2 = 1L;
					}
					if (4000L < num2)
					{
						num2 = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, num2);
				}
				if (type == typeof(Guid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(object))
				{
					return new SqlMetaData(name, SqlDbType.Variant);
				}
				if (type == typeof(SqlBinary))
				{
					SqlBinary sqlBinary = (SqlBinary)value;
					long num3;
					if (!sqlBinary.IsNull)
					{
						num3 = (long)sqlBinary.Length;
						if (num3 < 1L)
						{
							num3 = 1L;
						}
						if (8000L < num3)
						{
							num3 = SqlMetaData.Max;
						}
					}
					else
					{
						num3 = SqlMetaData.s_defaults[21].MaxLength;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num3);
				}
				if (type == typeof(SqlBoolean))
				{
					return new SqlMetaData(name, SqlDbType.Bit);
				}
				if (type == typeof(SqlByte))
				{
					return new SqlMetaData(name, SqlDbType.TinyInt);
				}
				if (type == typeof(SqlDateTime))
				{
					return new SqlMetaData(name, SqlDbType.DateTime);
				}
				if (type == typeof(SqlDouble))
				{
					return new SqlMetaData(name, SqlDbType.Float);
				}
				if (type == typeof(SqlGuid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(SqlInt16))
				{
					return new SqlMetaData(name, SqlDbType.SmallInt);
				}
				if (type == typeof(SqlInt32))
				{
					return new SqlMetaData(name, SqlDbType.Int);
				}
				if (type == typeof(SqlInt64))
				{
					return new SqlMetaData(name, SqlDbType.BigInt);
				}
				if (type == typeof(SqlMoney))
				{
					return new SqlMetaData(name, SqlDbType.Money);
				}
				if (type == typeof(SqlDecimal))
				{
					SqlDecimal sqlDecimal = (SqlDecimal)value;
					byte b;
					byte b2;
					if (!sqlDecimal.IsNull)
					{
						b = sqlDecimal.Precision;
						b2 = sqlDecimal.Scale;
					}
					else
					{
						b = SqlMetaData.s_defaults[5].Precision;
						b2 = SqlMetaData.s_defaults[5].Scale;
					}
					return new SqlMetaData(name, SqlDbType.Decimal, b, b2);
				}
				if (type == typeof(SqlSingle))
				{
					return new SqlMetaData(name, SqlDbType.Real);
				}
				if (type == typeof(SqlString))
				{
					SqlString sqlString = (SqlString)value;
					if (!sqlString.IsNull)
					{
						long num4 = (long)sqlString.Value.Length;
						if (num4 < 1L)
						{
							num4 = 1L;
						}
						if (num4 > 4000L)
						{
							num4 = SqlMetaData.Max;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num4, (long)sqlString.LCID, sqlString.SqlCompareOptions);
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, SqlMetaData.s_defaults[12].MaxLength);
				}
				else
				{
					if (type == typeof(SqlChars))
					{
						SqlChars sqlChars = (SqlChars)value;
						long num5;
						if (!sqlChars.IsNull)
						{
							num5 = sqlChars.Length;
							if (num5 < 1L)
							{
								num5 = 1L;
							}
							if (num5 > 4000L)
							{
								num5 = SqlMetaData.Max;
							}
						}
						else
						{
							num5 = SqlMetaData.s_defaults[12].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num5);
					}
					if (type == typeof(SqlBytes))
					{
						SqlBytes sqlBytes = (SqlBytes)value;
						long num6;
						if (!sqlBytes.IsNull)
						{
							num6 = sqlBytes.Length;
							if (num6 < 1L)
							{
								num6 = 1L;
							}
							else if (8000L < num6)
							{
								num6 = SqlMetaData.Max;
							}
						}
						else
						{
							num6 = SqlMetaData.s_defaults[21].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.VarBinary, num6);
					}
					if (type == typeof(SqlXml))
					{
						return new SqlMetaData(name, SqlDbType.Xml);
					}
					if (type == typeof(TimeSpan))
					{
						return new SqlMetaData(name, SqlDbType.Time, 0, SqlMetaData.InferScaleFromTimeTicks(((TimeSpan)value).Ticks));
					}
					if (type == typeof(DateTimeOffset))
					{
						return new SqlMetaData(name, SqlDbType.DateTimeOffset, 0, SqlMetaData.InferScaleFromTimeTicks(((DateTimeOffset)value).Ticks));
					}
					throw ADP.UnknownDataType(type);
				}
				break;
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return new SqlMetaData(name, SqlDbType.Bit);
			case TypeCode.Char:
				return new SqlMetaData(name, SqlDbType.NVarChar, 1L);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return new SqlMetaData(name, SqlDbType.TinyInt);
			case TypeCode.Int16:
				return new SqlMetaData(name, SqlDbType.SmallInt);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return new SqlMetaData(name, SqlDbType.Int);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return new SqlMetaData(name, SqlDbType.BigInt);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return new SqlMetaData(name, SqlDbType.Real);
			case TypeCode.Double:
				return new SqlMetaData(name, SqlDbType.Float);
			case TypeCode.Decimal:
			{
				SqlDecimal sqlDecimal2 = new SqlDecimal((decimal)value);
				return new SqlMetaData(name, SqlDbType.Decimal, sqlDecimal2.Precision, sqlDecimal2.Scale);
			}
			case TypeCode.DateTime:
				return new SqlMetaData(name, SqlDbType.DateTime);
			case TypeCode.String:
			{
				long num7 = (long)((string)value).Length;
				if (num7 < 1L)
				{
					num7 = 1L;
				}
				if (4000L < num7)
				{
					num7 = SqlMetaData.Max;
				}
				return new SqlMetaData(name, SqlDbType.NVarChar, num7);
			}
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00063E48 File Offset: 0x00062048
		public bool Adjust(bool value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00063E59 File Offset: 0x00062059
		public byte Adjust(byte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00064E64 File Offset: 0x00063064
		public byte[] Adjust(byte[] value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value, 0, array, 0, value.Length);
					Array.Clear(array, value.Length, array.Length - value.Length);
					return array;
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value, 0, array2, 0, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00064F18 File Offset: 0x00063118
		public char Adjust(char value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (1L != this.MaxLength)
				{
					SqlMetaData.ThrowInvalidType();
				}
			}
			else if (1L > this.MaxLength || (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType))
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00064F84 File Offset: 0x00063184
		public char[] Adjust(char[] value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null)
				{
					long num = (long)value.Length;
					if (num < this.MaxLength)
					{
						char[] array = new char[(int)this.MaxLength];
						Array.Copy(value, array, (int)num);
						for (long num2 = num; num2 < (long)array.Length; num2 += 1L)
						{
							array[(int)(checked((IntPtr)num2))] = ' ';
						}
						return array;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				char[] array2 = new char[this.MaxLength];
				Array.Copy(value, array2, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00065050 File Offset: 0x00063250
		internal static SqlMetaData GetPartialLengthMetaData(SqlMetaData md)
		{
			if (md.IsPartialLength)
			{
				return md;
			}
			if (md.SqlDbType == SqlDbType.Xml)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (md.SqlDbType == SqlDbType.NVarChar || md.SqlDbType == SqlDbType.VarChar || md.SqlDbType == SqlDbType.VarBinary)
			{
				return new SqlMetaData(md.Name, md.SqlDbType, SqlMetaData.Max, 0, 0, md.LocaleId, md.CompareOptions, null, null, null, true, md.Type);
			}
			return md;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000650C4 File Offset: 0x000632C4
		private static void ThrowInvalidType()
		{
			throw ADP.InvalidMetaDataValue();
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000650CB File Offset: 0x000632CB
		private void VerifyDateTimeRange(DateTime value)
		{
			if (SqlDbType.SmallDateTime == this.SqlDbType && (SqlMetaData.s_smallDateTimeMax < value || SqlMetaData.s_smallDateTimeMin > value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000650F8 File Offset: 0x000632F8
		private void VerifyMoneyRange(SqlMoney value)
		{
			if (SqlDbType.SmallMoney == this.SqlDbType && ((SqlMetaData.s_smallMoneyMax < value).Value || (SqlMetaData.s_smallMoneyMin > value).Value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00065140 File Offset: 0x00063340
		private SqlDecimal InternalAdjustSqlDecimal(SqlDecimal value)
		{
			if (!value.IsNull && (value.Precision != this.Precision || value.Scale != this.Scale))
			{
				if (value.Scale != this.Scale)
				{
					value = SqlDecimal.AdjustScale(value, (int)(this.Scale - value.Scale), false);
				}
				return SqlDecimal.ConvertToPrecScale(value, (int)this.Precision, (int)this.Scale);
			}
			return value;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000651AE File Offset: 0x000633AE
		private void VerifyTimeRange(TimeSpan value)
		{
			if (SqlDbType.Time == this.SqlDbType && (SqlMetaData.s_timeMin > value || value > SqlMetaData.s_timeMax))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000651D9 File Offset: 0x000633D9
		private long InternalAdjustTimeTicks(long ticks)
		{
			return ticks / SqlMetaData.s_unitTicksFromScale[(int)this.Scale] * SqlMetaData.s_unitTicksFromScale[(int)this.Scale];
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000651F8 File Offset: 0x000633F8
		private static byte InferScaleFromTimeTicks(long ticks)
		{
			for (byte b = 0; b < 7; b += 1)
			{
				if (ticks / SqlMetaData.s_unitTicksFromScale[(int)b] * SqlMetaData.s_unitTicksFromScale[(int)b] == ticks)
				{
					return b;
				}
			}
			return 7;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0006522C File Offset: 0x0006342C
		private void SetDefaultsForType(SqlDbType dbType)
		{
			if (SqlDbType.BigInt <= dbType && SqlDbType.DateTimeOffset >= dbType)
			{
				SqlMetaData sqlMetaData = SqlMetaData.s_defaults[(int)dbType];
				this._sqlDbType = dbType;
				this._maxLength = sqlMetaData.MaxLength;
				this._precision = sqlMetaData.Precision;
				this._scale = sqlMetaData.Scale;
				this._locale = sqlMetaData.LocaleId;
				this._compareOptions = sqlMetaData.CompareOptions;
			}
		}

		// Token: 0x04000989 RID: 2441
		private const long MaxUnicodeLength = 4000L;

		// Token: 0x0400098A RID: 2442
		private const long MaxANSILength = 8000L;

		// Token: 0x0400098B RID: 2443
		private const long MaxBinaryLength = 8000L;

		// Token: 0x0400098C RID: 2444
		private const long UnlimitedMaxLength = -1L;

		// Token: 0x0400098D RID: 2445
		private const bool DefaultUseServerDefault = false;

		// Token: 0x0400098E RID: 2446
		private const bool DefaultIsUniqueKey = false;

		// Token: 0x0400098F RID: 2447
		private const SortOrder DefaultColumnSortOrder = SortOrder.Unspecified;

		// Token: 0x04000990 RID: 2448
		private const int DefaultSortOrdinal = -1;

		// Token: 0x04000991 RID: 2449
		private const byte MaxTimeScale = 7;

		// Token: 0x04000992 RID: 2450
		private const SqlCompareOptions DefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04000993 RID: 2451
		private static readonly SqlMoney s_smallMoneyMax = new SqlMoney(214748.3647m);

		// Token: 0x04000994 RID: 2452
		private static readonly SqlMoney s_smallMoneyMin = new SqlMoney(-214748.3648m);

		// Token: 0x04000995 RID: 2453
		private static readonly DateTime s_smallDateTimeMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04000996 RID: 2454
		private static readonly DateTime s_smallDateTimeMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04000997 RID: 2455
		private static readonly TimeSpan s_timeMin = TimeSpan.Zero;

		// Token: 0x04000998 RID: 2456
		private static readonly TimeSpan s_timeMax = new TimeSpan(863999999999L);

		// Token: 0x04000999 RID: 2457
		private static readonly byte[] s_maxLenFromPrecision = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 17, 17,
			17, 17, 17, 17, 17, 17, 17, 17
		};

		// Token: 0x0400099A RID: 2458
		private static readonly byte[] s_maxVarTimeLenOffsetFromScale = new byte[] { 2, 2, 2, 1, 1, 0, 0, 0 };

		// Token: 0x0400099B RID: 2459
		private static readonly long[] s_unitTicksFromScale = new long[] { 10000000L, 1000000L, 100000L, 10000L, 1000L, 100L, 10L, 1L };

		// Token: 0x0400099C RID: 2460
		private static readonly DbType[] s_sqlDbTypeToDbType = new DbType[]
		{
			DbType.Int64,
			DbType.Binary,
			DbType.Boolean,
			DbType.AnsiString,
			DbType.DateTime,
			DbType.Decimal,
			DbType.Double,
			DbType.Binary,
			DbType.Int32,
			DbType.Currency,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Single,
			DbType.Guid,
			DbType.DateTime,
			DbType.Int16,
			DbType.Currency,
			DbType.AnsiString,
			DbType.Binary,
			DbType.Byte,
			DbType.Binary,
			DbType.AnsiString,
			DbType.Object,
			DbType.Object,
			DbType.Xml,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Object,
			DbType.Object,
			DbType.Date,
			DbType.Time,
			DbType.DateTime2,
			DbType.DateTimeOffset
		};

		// Token: 0x0400099D RID: 2461
		internal static SqlMetaData[] s_defaults = new SqlMetaData[]
		{
			new SqlMetaData("bigint", SqlDbType.BigInt, 8L, 19, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("binary", SqlDbType.Binary, 1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("bit", SqlDbType.Bit, 1L, 1, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("char", SqlDbType.Char, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("datetime", SqlDbType.DateTime, 8L, 23, 3, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("decimal", SqlDbType.Decimal, 9L, 18, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("float", SqlDbType.Float, 8L, 53, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("image", SqlDbType.Image, -1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("int", SqlDbType.Int, 4L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("money", SqlDbType.Money, 8L, 19, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nchar", SqlDbType.NChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("ntext", SqlDbType.NText, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("real", SqlDbType.Real, 4L, 24, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("uniqueidentifier", SqlDbType.UniqueIdentifier, 16L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smalldatetime", SqlDbType.SmallDateTime, 4L, 16, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallint", SqlDbType.SmallInt, 2L, 5, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallmoney", SqlDbType.SmallMoney, 4L, 10, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("text", SqlDbType.Text, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("timestamp", SqlDbType.Timestamp, 8L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("tinyint", SqlDbType.TinyInt, 1L, 3, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varbinary", SqlDbType.VarBinary, 8000L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varchar", SqlDbType.VarChar, 8000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("sql_variant", SqlDbType.Variant, 8016L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("xml", SqlDbType.Xml, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, true),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("udt", SqlDbType.Udt, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("table", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("date", SqlDbType.Date, 3L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("time", SqlDbType.Time, 5L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetime2", SqlDbType.DateTime2, 8L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetimeoffset", SqlDbType.DateTimeOffset, 10L, 0, 7, 0L, SqlCompareOptions.None, false)
		};

		// Token: 0x0400099E RID: 2462
		private string _name;

		// Token: 0x0400099F RID: 2463
		private long _maxLength;

		// Token: 0x040009A0 RID: 2464
		private SqlDbType _sqlDbType;

		// Token: 0x040009A1 RID: 2465
		private byte _precision;

		// Token: 0x040009A2 RID: 2466
		private byte _scale;

		// Token: 0x040009A3 RID: 2467
		private long _locale;

		// Token: 0x040009A4 RID: 2468
		private SqlCompareOptions _compareOptions;

		// Token: 0x040009A5 RID: 2469
		private string _xmlSchemaCollectionDatabase;

		// Token: 0x040009A6 RID: 2470
		private string _xmlSchemaCollectionOwningSchema;

		// Token: 0x040009A7 RID: 2471
		private string _xmlSchemaCollectionName;

		// Token: 0x040009A8 RID: 2472
		private string _serverTypeName;

		// Token: 0x040009A9 RID: 2473
		private bool _partialLength;

		// Token: 0x040009AA RID: 2474
		private Type _udtType;

		// Token: 0x040009AB RID: 2475
		private bool _useServerDefault;

		// Token: 0x040009AC RID: 2476
		private bool _isUniqueKey;

		// Token: 0x040009AD RID: 2477
		private SortOrder _columnSortOrder;

		// Token: 0x040009AE RID: 2478
		private int _sortOrdinal;
	}
}
