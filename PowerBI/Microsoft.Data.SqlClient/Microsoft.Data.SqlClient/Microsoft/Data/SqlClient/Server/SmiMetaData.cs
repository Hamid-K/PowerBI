using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000120 RID: 288
	internal class SmiMetaData
	{
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00060B15 File Offset: 0x0005ED15
		internal static SmiMetaData DefaultChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultChar_NoCollation.SqlDbType, SmiMetaData.DefaultChar_NoCollation.MaxLength, SmiMetaData.DefaultChar_NoCollation.Precision, SmiMetaData.DefaultChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00060B52 File Offset: 0x0005ED52
		internal static SmiMetaData DefaultNChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNChar_NoCollation.SqlDbType, SmiMetaData.DefaultNChar_NoCollation.MaxLength, SmiMetaData.DefaultNChar_NoCollation.Precision, SmiMetaData.DefaultNChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00060B8F File Offset: 0x0005ED8F
		internal static SmiMetaData DefaultNText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNText_NoCollation.SqlDbType, SmiMetaData.DefaultNText_NoCollation.MaxLength, SmiMetaData.DefaultNText_NoCollation.Precision, SmiMetaData.DefaultNText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00060BCC File Offset: 0x0005EDCC
		internal static SmiMetaData DefaultNVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultNVarChar_NoCollation.MaxLength, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00060C09 File Offset: 0x0005EE09
		internal static SmiMetaData DefaultText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultText_NoCollation.SqlDbType, SmiMetaData.DefaultText_NoCollation.MaxLength, SmiMetaData.DefaultText_NoCollation.Precision, SmiMetaData.DefaultText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00060C46 File Offset: 0x0005EE46
		internal static SmiMetaData DefaultVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultVarChar_NoCollation.MaxLength, SmiMetaData.DefaultVarChar_NoCollation.Precision, SmiMetaData.DefaultVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00060C84 File Offset: 0x0005EE84
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null)
		{
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldTypes, extendedProperties)
		{
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00060CD0 File Offset: 0x0005EED0
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties)
		{
			this.SetDefaultsForType(dbType);
			switch (dbType)
			{
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this._maxLength = maxLength;
				break;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this._maxLength = maxLength;
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Decimal:
				this._precision = precision;
				this._scale = scale;
				this._maxLength = (long)((ulong)SmiMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
				break;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Udt:
				this._clrType = userDefinedType;
				if (userDefinedType != null)
				{
					this._maxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
				}
				else
				{
					this._maxLength = maxLength;
				}
				this._udtAssemblyQualifiedName = udtAssemblyQualifiedName;
				break;
			case SqlDbType.Structured:
				if (fieldTypes != null)
				{
					this._fieldMetaData = new List<SmiExtendedMetaData>(fieldTypes).AsReadOnly();
				}
				this._isMultiValued = isMultiValued;
				this._maxLength = (long)this._fieldMetaData.Count;
				break;
			case SqlDbType.Time:
				this._scale = scale;
				this._maxLength = (long)(5 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTime2:
				this._scale = scale;
				this._maxLength = (long)(8 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTimeOffset:
				this._scale = scale;
				this._maxLength = (long)(10 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			}
			if (extendedProperties != null)
			{
				extendedProperties.SetReadOnly();
				this._extendedProperties = extendedProperties;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00060EB0 File Offset: 0x0005F0B0
		internal bool IsValidMaxLengthForCtorGivenType(SqlDbType dbType, long maxLength)
		{
			bool flag = true;
			switch (dbType)
			{
			case SqlDbType.Binary:
				flag = 0L < maxLength && 8000L >= maxLength;
				break;
			case SqlDbType.Char:
				flag = 0L < maxLength && 8000L >= maxLength;
				break;
			case SqlDbType.NChar:
				flag = 0L < maxLength && 4000L >= maxLength;
				break;
			case SqlDbType.NVarChar:
				flag = -1L == maxLength || (0L < maxLength && 4000L >= maxLength);
				break;
			case SqlDbType.VarBinary:
				flag = -1L == maxLength || (0L < maxLength && 8000L >= maxLength);
				break;
			case SqlDbType.VarChar:
				flag = -1L == maxLength || (0L < maxLength && 8000L >= maxLength);
				break;
			}
			return flag;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00060FFA File Offset: 0x0005F1FA
		internal SqlCompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x00061002 File Offset: 0x0005F202
		internal long LocaleId
		{
			get
			{
				return this._localeId;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0006100A File Offset: 0x0005F20A
		internal long MaxLength
		{
			get
			{
				return this._maxLength;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00061012 File Offset: 0x0005F212
		internal byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0006101A File Offset: 0x0005F21A
		internal byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x00061022 File Offset: 0x0005F222
		internal SqlDbType SqlDbType
		{
			get
			{
				return this._databaseType;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0006102A File Offset: 0x0005F22A
		internal Type Type
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, true);
				}
				return this._clrType;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00061064 File Offset: 0x0005F264
		internal Type TypeWithoutThrowing
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, false);
				}
				return this._clrType;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x000610A0 File Offset: 0x0005F2A0
		internal string TypeName
		{
			get
			{
				string text;
				if (SqlDbType.Udt == this._databaseType)
				{
					text = this.Type.FullName;
				}
				else
				{
					text = SmiMetaData.s_typeNameByDatabaseType[(int)this._databaseType];
				}
				return text;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000610D4 File Offset: 0x0005F2D4
		internal string AssemblyQualifiedName
		{
			get
			{
				string text = null;
				if (SqlDbType.Udt == this._databaseType)
				{
					if (this._udtAssemblyQualifiedName == null && this._clrType != null)
					{
						this._udtAssemblyQualifiedName = this._clrType.AssemblyQualifiedName;
					}
					text = this._udtAssemblyQualifiedName;
				}
				return text;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0006111C File Offset: 0x0005F31C
		internal bool IsMultiValued
		{
			get
			{
				return this._isMultiValued;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00061124 File Offset: 0x0005F324
		internal IList<SmiExtendedMetaData> FieldMetaData
		{
			get
			{
				return this._fieldMetaData;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0006112C File Offset: 0x0005F32C
		internal SmiMetaDataPropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00061134 File Offset: 0x0005F334
		internal static bool IsSupportedDbType(SqlDbType dbType)
		{
			return (SqlDbType.BigInt <= dbType && SqlDbType.Xml >= dbType) || (SqlDbType.Udt <= dbType && SqlDbType.DateTimeOffset >= dbType);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00061150 File Offset: 0x0005F350
		internal static SmiMetaData GetDefaultForType(SqlDbType dbType)
		{
			return SmiMetaData.s_defaultValues[(int)dbType];
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0006115C File Offset: 0x0005F35C
		private SmiMetaData(SqlDbType sqlDbType, long maxLength, byte precision, byte scale, SqlCompareOptions compareOptions)
		{
			this._databaseType = sqlDbType;
			this._maxLength = maxLength;
			this._precision = precision;
			this._scale = scale;
			this._compareOptions = compareOptions;
			this._localeId = 0L;
			this._clrType = null;
			this._isMultiValued = false;
			this._fieldMetaData = SmiMetaData.s_emptyFieldList;
			this._extendedProperties = SmiMetaDataPropertyCollection.s_emptyInstance;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000611C0 File Offset: 0x0005F3C0
		private void SetDefaultsForType(SqlDbType dbType)
		{
			SmiMetaData defaultForType = SmiMetaData.GetDefaultForType(dbType);
			this._databaseType = dbType;
			this._maxLength = defaultForType.MaxLength;
			this._precision = defaultForType.Precision;
			this._scale = defaultForType.Scale;
			this._localeId = defaultForType.LocaleId;
			this._compareOptions = defaultForType.CompareOptions;
			this._clrType = null;
			this._isMultiValued = defaultForType._isMultiValued;
			this._fieldMetaData = defaultForType._fieldMetaData;
			this._extendedProperties = defaultForType._extendedProperties;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00061242 File Offset: 0x0005F442
		internal string TraceString()
		{
			return this.TraceString(0);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0006124C File Offset: 0x0005F44C
		internal virtual string TraceString(int indent)
		{
			string text = new string(' ', indent);
			string text2 = string.Empty;
			if (this._fieldMetaData != null)
			{
				foreach (SmiMetaData smiMetaData in this._fieldMetaData)
				{
					text2 = string.Format(CultureInfo.InvariantCulture, "{0}{1}\n\t", text2, smiMetaData.TraceString(indent + 5));
				}
			}
			string text3 = string.Empty;
			if (this._extendedProperties != null)
			{
				foreach (SmiMetaDataProperty smiMetaDataProperty in this._extendedProperties.Values)
				{
					text3 = string.Format(CultureInfo.InvariantCulture, "{0}{1}                   {2}\n\t", text3, text, smiMetaDataProperty.TraceString());
				}
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string text4 = "\n\t{0}            SqlDbType={1:g}\n\t{0}            MaxLength={2:d}\n\t{0}            Precision={3:d}\n\t{0}                Scale={4:d}\n\t{0}             LocaleId={5:x}\n\t{0}       CompareOptions={6:g}\n\t{0}                 Type={7}\n\t{0}          MultiValued={8}\n\t{0}               fields=\n\t{9}{0}           properties=\n\t{10}";
			object[] array = new object[11];
			array[0] = text;
			array[1] = this.SqlDbType;
			array[2] = this.MaxLength;
			array[3] = this.Precision;
			array[4] = this.Scale;
			array[5] = this.LocaleId;
			array[6] = this.CompareOptions;
			int num = 7;
			Type type = this.Type;
			array[num] = ((type != null) ? type.ToString() : null) ?? "<null>";
			array[8] = this.IsMultiValued;
			array[9] = text2;
			array[10] = text3;
			return string.Format(invariantCulture, text4, array);
		}

		// Token: 0x0400090A RID: 2314
		private SqlDbType _databaseType;

		// Token: 0x0400090B RID: 2315
		private long _maxLength;

		// Token: 0x0400090C RID: 2316
		private byte _precision;

		// Token: 0x0400090D RID: 2317
		private byte _scale;

		// Token: 0x0400090E RID: 2318
		private long _localeId;

		// Token: 0x0400090F RID: 2319
		private SqlCompareOptions _compareOptions;

		// Token: 0x04000910 RID: 2320
		private Type _clrType;

		// Token: 0x04000911 RID: 2321
		private string _udtAssemblyQualifiedName;

		// Token: 0x04000912 RID: 2322
		private bool _isMultiValued;

		// Token: 0x04000913 RID: 2323
		private IList<SmiExtendedMetaData> _fieldMetaData;

		// Token: 0x04000914 RID: 2324
		private SmiMetaDataPropertyCollection _extendedProperties;

		// Token: 0x04000915 RID: 2325
		internal const long UnlimitedMaxLengthIndicator = -1L;

		// Token: 0x04000916 RID: 2326
		internal const long MaxUnicodeCharacters = 4000L;

		// Token: 0x04000917 RID: 2327
		internal const long MaxANSICharacters = 8000L;

		// Token: 0x04000918 RID: 2328
		internal const long MaxBinaryLength = 8000L;

		// Token: 0x04000919 RID: 2329
		internal const int MinPrecision = 1;

		// Token: 0x0400091A RID: 2330
		internal const int MinScale = 0;

		// Token: 0x0400091B RID: 2331
		internal const int MaxTimeScale = 7;

		// Token: 0x0400091C RID: 2332
		internal static readonly DateTime MaxSmallDateTime = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x0400091D RID: 2333
		internal static readonly DateTime MinSmallDateTime = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x0400091E RID: 2334
		internal static readonly SqlMoney MaxSmallMoney = new SqlMoney(214748.3647m);

		// Token: 0x0400091F RID: 2335
		internal static readonly SqlMoney MinSmallMoney = new SqlMoney(-214748.3648m);

		// Token: 0x04000920 RID: 2336
		internal const SqlCompareOptions DefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04000921 RID: 2337
		internal const long MaxNameLength = 128L;

		// Token: 0x04000922 RID: 2338
		private static readonly IList<SmiExtendedMetaData> s_emptyFieldList = new List<SmiExtendedMetaData>().AsReadOnly();

		// Token: 0x04000923 RID: 2339
		private static readonly byte[] s_maxLenFromPrecision = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 17, 17,
			17, 17, 17, 17, 17, 17, 17, 17
		};

		// Token: 0x04000924 RID: 2340
		private static readonly byte[] s_maxVarTimeLenOffsetFromScale = new byte[] { 2, 2, 2, 1, 1, 0, 0, 0 };

		// Token: 0x04000925 RID: 2341
		internal static readonly SmiMetaData DefaultBigInt = new SmiMetaData(SqlDbType.BigInt, 8L, 19, 0, SqlCompareOptions.None);

		// Token: 0x04000926 RID: 2342
		internal static readonly SmiMetaData DefaultBinary = new SmiMetaData(SqlDbType.Binary, 1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000927 RID: 2343
		internal static readonly SmiMetaData DefaultBit = new SmiMetaData(SqlDbType.Bit, 1L, 1, 0, SqlCompareOptions.None);

		// Token: 0x04000928 RID: 2344
		internal static readonly SmiMetaData DefaultChar_NoCollation = new SmiMetaData(SqlDbType.Char, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000929 RID: 2345
		internal static readonly SmiMetaData DefaultDateTime = new SmiMetaData(SqlDbType.DateTime, 8L, 23, 3, SqlCompareOptions.None);

		// Token: 0x0400092A RID: 2346
		internal static readonly SmiMetaData DefaultDecimal = new SmiMetaData(SqlDbType.Decimal, 9L, 18, 0, SqlCompareOptions.None);

		// Token: 0x0400092B RID: 2347
		internal static readonly SmiMetaData DefaultFloat = new SmiMetaData(SqlDbType.Float, 8L, 53, 0, SqlCompareOptions.None);

		// Token: 0x0400092C RID: 2348
		internal static readonly SmiMetaData DefaultImage = new SmiMetaData(SqlDbType.Image, -1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400092D RID: 2349
		internal static readonly SmiMetaData DefaultInt = new SmiMetaData(SqlDbType.Int, 4L, 10, 0, SqlCompareOptions.None);

		// Token: 0x0400092E RID: 2350
		internal static readonly SmiMetaData DefaultMoney = new SmiMetaData(SqlDbType.Money, 8L, 19, 4, SqlCompareOptions.None);

		// Token: 0x0400092F RID: 2351
		internal static readonly SmiMetaData DefaultNChar_NoCollation = new SmiMetaData(SqlDbType.NChar, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000930 RID: 2352
		internal static readonly SmiMetaData DefaultNText_NoCollation = new SmiMetaData(SqlDbType.NText, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000931 RID: 2353
		internal static readonly SmiMetaData DefaultNVarChar_NoCollation = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000932 RID: 2354
		internal static readonly SmiMetaData DefaultReal = new SmiMetaData(SqlDbType.Real, 4L, 24, 0, SqlCompareOptions.None);

		// Token: 0x04000933 RID: 2355
		internal static readonly SmiMetaData DefaultUniqueIdentifier = new SmiMetaData(SqlDbType.UniqueIdentifier, 16L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000934 RID: 2356
		internal static readonly SmiMetaData DefaultSmallDateTime = new SmiMetaData(SqlDbType.SmallDateTime, 4L, 16, 0, SqlCompareOptions.None);

		// Token: 0x04000935 RID: 2357
		internal static readonly SmiMetaData DefaultSmallInt = new SmiMetaData(SqlDbType.SmallInt, 2L, 5, 0, SqlCompareOptions.None);

		// Token: 0x04000936 RID: 2358
		internal static readonly SmiMetaData DefaultSmallMoney = new SmiMetaData(SqlDbType.SmallMoney, 4L, 10, 4, SqlCompareOptions.None);

		// Token: 0x04000937 RID: 2359
		internal static readonly SmiMetaData DefaultText_NoCollation = new SmiMetaData(SqlDbType.Text, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000938 RID: 2360
		internal static readonly SmiMetaData DefaultTimestamp = new SmiMetaData(SqlDbType.Timestamp, 8L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000939 RID: 2361
		internal static readonly SmiMetaData DefaultTinyInt = new SmiMetaData(SqlDbType.TinyInt, 1L, 3, 0, SqlCompareOptions.None);

		// Token: 0x0400093A RID: 2362
		internal static readonly SmiMetaData DefaultVarBinary = new SmiMetaData(SqlDbType.VarBinary, 8000L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400093B RID: 2363
		internal static readonly SmiMetaData DefaultVarChar_NoCollation = new SmiMetaData(SqlDbType.VarChar, 8000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x0400093C RID: 2364
		internal static readonly SmiMetaData DefaultVariant = new SmiMetaData(SqlDbType.Variant, 8016L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400093D RID: 2365
		internal static readonly SmiMetaData DefaultXml = new SmiMetaData(SqlDbType.Xml, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x0400093E RID: 2366
		internal static readonly SmiMetaData DefaultUdt_NoType = new SmiMetaData(SqlDbType.Udt, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400093F RID: 2367
		internal static readonly SmiMetaData DefaultStructured = new SmiMetaData(SqlDbType.Structured, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000940 RID: 2368
		internal static readonly SmiMetaData DefaultDate = new SmiMetaData(SqlDbType.Date, 3L, 10, 0, SqlCompareOptions.None);

		// Token: 0x04000941 RID: 2369
		internal static readonly SmiMetaData DefaultTime = new SmiMetaData(SqlDbType.Time, 5L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04000942 RID: 2370
		internal static readonly SmiMetaData DefaultDateTime2 = new SmiMetaData(SqlDbType.DateTime2, 8L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04000943 RID: 2371
		internal static readonly SmiMetaData DefaultDateTimeOffset = new SmiMetaData(SqlDbType.DateTimeOffset, 10L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04000944 RID: 2372
		private static readonly SmiMetaData[] s_defaultValues = new SmiMetaData[]
		{
			SmiMetaData.DefaultBigInt,
			SmiMetaData.DefaultBinary,
			SmiMetaData.DefaultBit,
			SmiMetaData.DefaultChar_NoCollation,
			SmiMetaData.DefaultDateTime,
			SmiMetaData.DefaultDecimal,
			SmiMetaData.DefaultFloat,
			SmiMetaData.DefaultImage,
			SmiMetaData.DefaultInt,
			SmiMetaData.DefaultMoney,
			SmiMetaData.DefaultNChar_NoCollation,
			SmiMetaData.DefaultNText_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultReal,
			SmiMetaData.DefaultUniqueIdentifier,
			SmiMetaData.DefaultSmallDateTime,
			SmiMetaData.DefaultSmallInt,
			SmiMetaData.DefaultSmallMoney,
			SmiMetaData.DefaultText_NoCollation,
			SmiMetaData.DefaultTimestamp,
			SmiMetaData.DefaultTinyInt,
			SmiMetaData.DefaultVarBinary,
			SmiMetaData.DefaultVarChar_NoCollation,
			SmiMetaData.DefaultVariant,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultXml,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultUdt_NoType,
			SmiMetaData.DefaultStructured,
			SmiMetaData.DefaultDate,
			SmiMetaData.DefaultTime,
			SmiMetaData.DefaultDateTime2,
			SmiMetaData.DefaultDateTimeOffset
		};

		// Token: 0x04000945 RID: 2373
		private static readonly string[] s_typeNameByDatabaseType = new string[]
		{
			"bigint",
			"binary",
			"bit",
			"char",
			"datetime",
			"decimal",
			"float",
			"image",
			"int",
			"money",
			"nchar",
			"ntext",
			"nvarchar",
			"real",
			"uniqueidentifier",
			"smalldatetime",
			"smallint",
			"smallmoney",
			"text",
			"timestamp",
			"tinyint",
			"varbinary",
			"varchar",
			"sql_variant",
			null,
			"xml",
			null,
			null,
			null,
			string.Empty,
			string.Empty,
			"date",
			"time",
			"datetime2",
			"datetimeoffset"
		};
	}
}
