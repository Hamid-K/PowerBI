using System;
using System.Data;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009FA RID: 2554
	internal sealed class DrdaMetaType
	{
		// Token: 0x0600501A RID: 20506 RVA: 0x0014070C File Offset: 0x0013E90C
		static DrdaMetaType()
		{
			DrdaMetaType.dbTypeMetaType[0] = new DrdaMetaType(typeof(string), 0, false, false, byte.MaxValue, byte.MaxValue, "VarChar", DrdaClientType.VarChar, DbType.AnsiString, -8, 32739L, 4045L, 4000L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.dbTypeMetaType[1] = new DrdaMetaType(typeof(byte[]), 0, false, false, byte.MaxValue, byte.MaxValue, "VarBinary", DrdaClientType.VarBinary, DbType.Binary, -2, 32739L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.dbTypeMetaType[2] = new DrdaMetaType(typeof(byte), 2, true, false, 3, byte.MaxValue, "SmallInt", DrdaClientType.SmallInt, DbType.Byte, 5, 5L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[3] = new DrdaMetaType(typeof(bool), 2, true, false, 1, byte.MaxValue, "SmallInt", DrdaClientType.SmallInt, DbType.Boolean, 5, 5L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[4] = new DrdaMetaType(typeof(decimal), 68, true, false, 29, 4, "Decimal", DrdaClientType.Decimal, DbType.Currency, -8, 31L, "precision,scale", false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, 31, 0, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[5] = new DrdaMetaType(typeof(DateTime), 16, true, false, byte.MaxValue, byte.MaxValue, "Date", DrdaClientType.Date, DbType.Date, 93, 10L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[6] = new DrdaMetaType(typeof(DateTime), 16, true, false, 26, 7, "Timestamp", DrdaClientType.Timestamp, DbType.DateTime, 93, 26L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[7] = new DrdaMetaType(typeof(decimal), 68, false, false, 29, 4, "Decimal", DrdaClientType.Decimal, DbType.Decimal, -8, 31L, "precision,scale", false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, 31, 0, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[8] = new DrdaMetaType(typeof(double), 8, true, false, 15, byte.MaxValue, "Double", DrdaClientType.Double, DbType.Double, 8, 15L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[9] = new DrdaMetaType(typeof(Guid), 16, true, false, byte.MaxValue, byte.MaxValue, "VarBinary", DrdaClientType.VarBinary, DbType.Guid, -2, 32739L, "max length", true, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, "'", "'");
			DrdaMetaType.dbTypeMetaType[10] = new DrdaMetaType(typeof(short), 2, true, false, 5, byte.MaxValue, "SmallInt", DrdaClientType.SmallInt, DbType.Int16, 5, 5L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[11] = new DrdaMetaType(typeof(int), 4, true, false, 10, byte.MaxValue, "Int", DrdaClientType.Int, DbType.Int32, 4, 10L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[12] = new DrdaMetaType(typeof(long), 8, true, false, 19, byte.MaxValue, "BigInt", DrdaClientType.BigInt, DbType.Int64, -25, 19L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[13] = new DrdaMetaType(typeof(object), 0, false, false, byte.MaxValue, byte.MaxValue, "VarBinary", DrdaClientType.VarBinary, DbType.Object, -2, 32739L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.dbTypeMetaType[14] = new DrdaMetaType(typeof(sbyte), 2, true, false, 3, byte.MaxValue, "SmallInt", DrdaClientType.SmallInt, DbType.SByte, 5, 5L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[15] = new DrdaMetaType(typeof(float), 4, true, false, 7, byte.MaxValue, "Real", DrdaClientType.Real, DbType.Single, 7, 7L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[16] = new DrdaMetaType(typeof(string), 0, false, false, byte.MaxValue, byte.MaxValue, "VarChar", DrdaClientType.VarChar, DbType.String, -8, 32739L, 4045L, 4000L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.dbTypeMetaType[17] = new DrdaMetaType(typeof(TimeSpan), 6, true, false, byte.MaxValue, byte.MaxValue, "Time", DrdaClientType.Time, DbType.Time, 92, 8L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[18] = new DrdaMetaType(typeof(ushort), 2, true, false, 5, byte.MaxValue, "SmallInt", DrdaClientType.SmallInt, DbType.UInt16, 5, 5L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[19] = new DrdaMetaType(typeof(uint), 4, true, false, 10, byte.MaxValue, "Int", DrdaClientType.Int, DbType.UInt32, 4, 10L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[20] = new DrdaMetaType(typeof(ulong), 8, true, false, 19, byte.MaxValue, "BigInt", DrdaClientType.BigInt, DbType.UInt64, -25, 19L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[21] = new DrdaMetaType(typeof(decimal), 68, false, false, 29, 4, "Decimal", DrdaClientType.Decimal, DbType.VarNumeric, -8, 31L, "precision,scale", false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, 31, 0, string.Empty, string.Empty);
			DrdaMetaType.dbTypeMetaType[22] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "Char", DrdaClientType.Char, DbType.AnsiStringFixedLength, -8, 32765L, 255L, 254L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.dbTypeMetaType[23] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "Char", DrdaClientType.Char, DbType.StringFixedLength, -8, 32765L, 255L, 254L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.dbTypeMetaType[25] = new DrdaMetaType(typeof(byte[]), 0, false, true, byte.MaxValue, byte.MaxValue, "Xml", DrdaClientType.Xml, DbType.Xml, -2, 2147483647L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType = new DrdaMetaType[34];
			DrdaMetaType.drdaTypeMetaType[12] = new DrdaMetaType(typeof(TimeSpan), 6, true, false, byte.MaxValue, byte.MaxValue, "Time", DrdaClientType.Time, DbType.Time, 92, 8L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[13] = new DrdaMetaType(typeof(DateTime), 16, true, false, 26, 7, "Timestamp", DrdaClientType.Timestamp, DbType.DateTime, 93, 26L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[5] = new DrdaMetaType(typeof(DateTime), 16, true, false, byte.MaxValue, byte.MaxValue, "Date", DrdaClientType.Date, DbType.Date, 93, 10L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[3] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "Char", DrdaClientType.Char, DbType.StringFixedLength, -8, 32765L, 255L, 254L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[16] = new DrdaMetaType(typeof(string), 0, false, false, byte.MaxValue, byte.MaxValue, "VarChar", DrdaClientType.VarChar, DbType.String, -8, 32739L, 4045L, 32672L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[33] = new DrdaMetaType(typeof(string), 0, false, false, byte.MaxValue, byte.MaxValue, "VarChar", DrdaClientType.RowId, DbType.String, -8, 32739L, 4045L, 32672L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[1] = new DrdaMetaType(typeof(byte[]), 0, true, false, byte.MaxValue, byte.MaxValue, "Binary", DrdaClientType.Binary, DbType.Binary, -2, 32765L, 255L, 254L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[25] = new DrdaMetaType(typeof(byte[]), 0, true, false, byte.MaxValue, byte.MaxValue, "Binary", DrdaClientType.CharForBit, DbType.Binary, -2, 32765L, 255L, 254L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[15] = new DrdaMetaType(typeof(byte[]), 0, false, false, byte.MaxValue, byte.MaxValue, "VarBinary", DrdaClientType.VarBinary, DbType.Binary, -2, 32739L, 4045L, 32672L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[18] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "Graphic", DrdaClientType.Graphic, DbType.StringFixedLength, -8, 16382L, 127L, 127L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "G'", "'");
			DrdaMetaType.drdaTypeMetaType[31] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "Graphic", DrdaClientType.NChar, DbType.StringFixedLength, -8, 16382L, 127L, 127L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "G'", "'");
			DrdaMetaType.drdaTypeMetaType[32] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "VarGraphic", DrdaClientType.NVarChar, DbType.String, -8, 16382L, 127L, 127L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "G'", "'");
			DrdaMetaType.drdaTypeMetaType[19] = new DrdaMetaType(typeof(string), 0, false, false, byte.MaxValue, byte.MaxValue, "VarGraphic", DrdaClientType.VarGraphic, DbType.String, -8, 16369L, 2023L, 16369L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "G'", "'");
			DrdaMetaType.drdaTypeMetaType[30] = new DrdaMetaType(typeof(string), 0, false, false, byte.MaxValue, byte.MaxValue, "VarGraphic", DrdaClientType.LongVarGraphic, DbType.String, -8, 16369L, 2023L, 16369L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "G'", "'");
			DrdaMetaType.drdaTypeMetaType[19] = new DrdaMetaType(typeof(string), 0, false, false, byte.MaxValue, byte.MaxValue, "VarGraphic", DrdaClientType.VarGraphic, DbType.String, -8, 16369L, 2023L, 16369L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "G'", "'");
			DrdaMetaType.drdaTypeMetaType[6] = new DrdaMetaType(typeof(decimal), 68, false, false, 29, 4, "Decimal", DrdaClientType.Decimal, DbType.Decimal, -8, 38L, 31L, 31L, "precision,scale", false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, 0, 28, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[9] = new DrdaMetaType(typeof(decimal), 68, false, false, 29, 4, "Numeric", DrdaClientType.Numeric, DbType.Decimal, -8, 38L, 31L, 31L, "precision,scale", false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, 0, 31, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[10] = new DrdaMetaType(typeof(float), 4, true, false, 7, byte.MaxValue, "Real", DrdaClientType.Real, DbType.Single, 7, 7L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[7] = new DrdaMetaType(typeof(double), 8, true, false, 15, byte.MaxValue, "Double", DrdaClientType.Double, DbType.Double, 8, 15L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[11] = new DrdaMetaType(typeof(short), 2, true, false, 5, byte.MaxValue, "SmallInt", DrdaClientType.SmallInt, DbType.Int16, 5, 5L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[8] = new DrdaMetaType(typeof(int), 4, true, false, 10, byte.MaxValue, "Int", DrdaClientType.Int, DbType.Int32, 4, 10L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[0] = new DrdaMetaType(typeof(long), 8, true, false, 19, byte.MaxValue, "BigInt", DrdaClientType.BigInt, DbType.Int64, -25, 19L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[21] = new DrdaMetaType(typeof(string), 0, false, true, byte.MaxValue, byte.MaxValue, "CLOB", DrdaClientType.CLOB, DbType.String, -8, 2147483647L, 2147483647L, 2147483647L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[22] = new DrdaMetaType(typeof(string), 0, false, true, byte.MaxValue, byte.MaxValue, "DBCLOB", DrdaClientType.DBCLOB, DbType.String, -8, 1073741823L, 1073741823L, 1073741823L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[23] = new DrdaMetaType(typeof(byte[]), 0, false, true, byte.MaxValue, byte.MaxValue, "BLOB", DrdaClientType.BLOB, DbType.Binary, -2, 2147483647L, 2147483647L, 2147483647L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[24] = new DrdaMetaType(typeof(string), 0, false, true, byte.MaxValue, byte.MaxValue, "Xml", DrdaClientType.Xml, DbType.Xml, -8, 2147483647L, 2147483647L, 2147483647L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[4] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "Char", DrdaClientType.WideChar, DbType.StringFixedLength, -8, 32765L, 255L, 254L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[28] = new DrdaMetaType(typeof(string), 0, true, false, byte.MaxValue, byte.MaxValue, "Char", DrdaClientType.LongVarChar, DbType.StringFixedLength, -8, 32765L, 255L, 254L, "length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
			DrdaMetaType.drdaTypeMetaType[27] = new DrdaMetaType(typeof(decimal), 68, false, false, 29, 4, "Decimal", DrdaClientType.DecFloat, DbType.Decimal, -8, 38L, 31L, 31L, "precision,scale", false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, 0, 28, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[26] = new DrdaMetaType(typeof(short), 2, true, false, 5, byte.MaxValue, "SmallInt", DrdaClientType.Boolean, DbType.Int16, 5, 5L, string.Empty, false, DrdaMetaType.SEARCHABLE.DB_ALL_EXCEPT_LIKE, -1, -1, string.Empty, string.Empty);
			DrdaMetaType.drdaTypeMetaType[29] = new DrdaMetaType(typeof(byte[]), 0, false, false, byte.MaxValue, byte.MaxValue, "VarBinary", DrdaClientType.LongVarCharForBit, DbType.Binary, -2, 32739L, 4045L, 32672L, "max length", true, DrdaMetaType.SEARCHABLE.DB_SEARCHABLE, -1, -1, "'", "'");
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x001416FC File Offset: 0x0013F8FC
		public DrdaMetaType(Type classType, int fixedLength, bool isFixed, bool isLong, byte precision, byte scale, string typeName, DrdaClientType msDb2Type, DbType dbType, short clientType, long maxSizeAS400, long maxSizeMVS, long maxSizeUDB, string createParameter, bool isCaseSensative, DrdaMetaType.SEARCHABLE isSearchable, short minimumScale, short maximumScale, string literalPrefix, string literalSuffix)
		{
			this._classType = classType;
			this._fixedLength = fixedLength;
			this._isFixed = isFixed;
			this._isLong = isLong;
			this._precision = precision;
			this._scale = scale;
			this._typeName = typeName;
			this._msDb2Type = msDb2Type;
			this._dbType = dbType;
			this._clientType = clientType;
			this._maxSizeAS400 = maxSizeAS400;
			this._maxSizeMVS = maxSizeMVS;
			this._maxSizeUDB = maxSizeUDB;
			this._createParameter = createParameter;
			this._isCaseSensative = isCaseSensative;
			this._isSearchable = isSearchable;
			this._minimumScale = minimumScale;
			this._maximumScale = maximumScale;
			this._literalPrefix = literalPrefix;
			this._literalSuffix = literalSuffix;
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x001417AC File Offset: 0x0013F9AC
		public DrdaMetaType(Type classType, int fixedLength, bool isFixed, bool isLong, byte precision, byte scale, string typeName, DrdaClientType msDb2Type, DbType dbType, short clientType, long maxSize, string createParameter, bool isCaseSensative, DrdaMetaType.SEARCHABLE isSearchable, short minimumScale, short maximumScale, string literalPrefix, string literalSuffix)
			: this(classType, fixedLength, isFixed, isLong, precision, scale, typeName, msDb2Type, dbType, clientType, maxSize, maxSize, maxSize, createParameter, isCaseSensative, isSearchable, minimumScale, maximumScale, literalPrefix, literalSuffix)
		{
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x001417E4 File Offset: 0x0013F9E4
		public override bool Equals(object obj)
		{
			if (obj is DrdaMetaType)
			{
				DrdaMetaType drdaMetaType = (DrdaMetaType)obj;
				return drdaMetaType._classType == this._classType && drdaMetaType._fixedLength == this._fixedLength && drdaMetaType._isFixed == this._isFixed && drdaMetaType._isLong == this._isLong && drdaMetaType._precision == this._precision && drdaMetaType._scale == this._scale && drdaMetaType._typeName.Equals(this._typeName) && drdaMetaType._msDb2Type == this._msDb2Type && drdaMetaType._dbType == this._dbType && drdaMetaType._clientType == this._clientType;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0003FF3A File Offset: 0x0003E13A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x0600501F RID: 20511 RVA: 0x001418A6 File Offset: 0x0013FAA6
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06005020 RID: 20512 RVA: 0x001418AE File Offset: 0x0013FAAE
		public DrdaClientType DrdaType
		{
			get
			{
				return this._msDb2Type;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06005021 RID: 20513 RVA: 0x001418B6 File Offset: 0x0013FAB6
		public DbType DbType
		{
			get
			{
				return this._dbType;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06005022 RID: 20514 RVA: 0x001418BE File Offset: 0x0013FABE
		public short ClientType
		{
			get
			{
				return this._clientType;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06005023 RID: 20515 RVA: 0x001418C6 File Offset: 0x0013FAC6
		public int FixedLength
		{
			get
			{
				return this._fixedLength;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x001418CE File Offset: 0x0013FACE
		public bool IsFixed
		{
			get
			{
				return this._isFixed;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x001418D6 File Offset: 0x0013FAD6
		public bool IsLong
		{
			get
			{
				return this._isLong;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06005026 RID: 20518 RVA: 0x001418DE File Offset: 0x0013FADE
		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06005027 RID: 20519 RVA: 0x001418E6 File Offset: 0x0013FAE6
		public byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x001418EE File Offset: 0x0013FAEE
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x001418F6 File Offset: 0x0013FAF6
		public long MaxSizeAS400
		{
			get
			{
				return this._maxSizeAS400;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x001418FE File Offset: 0x0013FAFE
		public long MaxSizeMVS
		{
			get
			{
				return this._maxSizeMVS;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x00141906 File Offset: 0x0013FB06
		public long MaxSizeUDB
		{
			get
			{
				return this._maxSizeUDB;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x0014190E File Offset: 0x0013FB0E
		public string CreateParameter
		{
			get
			{
				return this._createParameter;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x00141916 File Offset: 0x0013FB16
		public bool IsCaseSensative
		{
			get
			{
				return this._isCaseSensative;
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x0014191E File Offset: 0x0013FB1E
		public DrdaMetaType.SEARCHABLE IsSearchable
		{
			get
			{
				return this._isSearchable;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x00141926 File Offset: 0x0013FB26
		public short MinimumScale
		{
			get
			{
				return this._minimumScale;
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x0014192E File Offset: 0x0013FB2E
		public short MaximumScale
		{
			get
			{
				return this._maximumScale;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x00141936 File Offset: 0x0013FB36
		public string LiteralPrefix
		{
			get
			{
				return this._literalPrefix;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x0014193E File Offset: 0x0013FB3E
		public string LiteralSuffix
		{
			get
			{
				return this._literalSuffix;
			}
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x00141946 File Offset: 0x0013FB46
		internal static DrdaMetaType GetDefaultMetaType()
		{
			return DrdaMetaType.dbTypeMetaType[0];
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x00141950 File Offset: 0x0013FB50
		internal static DrdaMetaType GetMetaTypeForObject(object value)
		{
			Type type;
			if (value is Type)
			{
				type = (Type)value;
			}
			else
			{
				type = value.GetType();
			}
			return DrdaMetaType.GetMetaTypeForType(type);
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x0014197C File Offset: 0x0013FB7C
		internal static DrdaMetaType GetMetaTypeForType(Type dataType)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Object:
				if (dataType == typeof(byte[]))
				{
					return DrdaMetaType.drdaTypeMetaType[15];
				}
				if (dataType == typeof(TimeSpan))
				{
					return DrdaMetaType.drdaTypeMetaType[12];
				}
				if (dataType == typeof(Guid))
				{
					return DrdaMetaType.dbTypeMetaType[9];
				}
				throw DrdaException.UnknownDataType(dataType);
			case TypeCode.Boolean:
				return DrdaMetaType.dbTypeMetaType[3];
			case TypeCode.Char:
				return DrdaMetaType.dbTypeMetaType[0];
			case TypeCode.SByte:
				return DrdaMetaType.dbTypeMetaType[14];
			case TypeCode.Byte:
				return DrdaMetaType.dbTypeMetaType[2];
			case TypeCode.Int16:
				return DrdaMetaType.dbTypeMetaType[10];
			case TypeCode.UInt16:
				return DrdaMetaType.dbTypeMetaType[18];
			case TypeCode.Int32:
				return DrdaMetaType.dbTypeMetaType[11];
			case TypeCode.UInt32:
				return DrdaMetaType.dbTypeMetaType[19];
			case TypeCode.Int64:
				return DrdaMetaType.dbTypeMetaType[12];
			case TypeCode.UInt64:
				return DrdaMetaType.dbTypeMetaType[20];
			case TypeCode.Single:
				return DrdaMetaType.dbTypeMetaType[15];
			case TypeCode.Double:
				return DrdaMetaType.dbTypeMetaType[8];
			case TypeCode.Decimal:
				return DrdaMetaType.dbTypeMetaType[7];
			case TypeCode.DateTime:
				return DrdaMetaType.dbTypeMetaType[6];
			case TypeCode.String:
				return DrdaMetaType.dbTypeMetaType[0];
			}
			throw DrdaException.UnknownDataType(dataType);
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00141AC3 File Offset: 0x0013FCC3
		internal static DrdaMetaType GetMetaTypeForType(DbType dbType)
		{
			if (dbType >= DbType.AnsiString)
			{
			}
			return DrdaMetaType.dbTypeMetaType[(int)dbType];
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x00141AD5 File Offset: 0x0013FCD5
		internal static DrdaMetaType GetMetaTypeForType(DrdaClientType msDb2Type)
		{
			if (msDb2Type < DrdaClientType.BigInt || msDb2Type - DrdaClientType.Binary > 33)
			{
				throw DrdaException.InvalidDrdaType(msDb2Type);
			}
			return DrdaMetaType.drdaTypeMetaType[(int)msDb2Type];
		}

		// Token: 0x04003F27 RID: 16167
		private const string N_TIME = "Time";

		// Token: 0x04003F28 RID: 16168
		private const string N_TIMESTAMP = "Timestamp";

		// Token: 0x04003F29 RID: 16169
		private const string N_DATE = "Date";

		// Token: 0x04003F2A RID: 16170
		private const string N_CHAR = "Char";

		// Token: 0x04003F2B RID: 16171
		private const string N_VARCHAR = "VarChar";

		// Token: 0x04003F2C RID: 16172
		private const string N_BINARY = "Binary";

		// Token: 0x04003F2D RID: 16173
		private const string N_VARBINARY = "VarBinary";

		// Token: 0x04003F2E RID: 16174
		private const string N_GRAPHIC = "Graphic";

		// Token: 0x04003F2F RID: 16175
		private const string N_VARGRAPHIC = "VarGraphic";

		// Token: 0x04003F30 RID: 16176
		private const string N_DECIMAL = "Decimal";

		// Token: 0x04003F31 RID: 16177
		private const string N_NUMERIC = "Numeric";

		// Token: 0x04003F32 RID: 16178
		private const string N_REAL = "Real";

		// Token: 0x04003F33 RID: 16179
		private const string N_DOUBLE = "Double";

		// Token: 0x04003F34 RID: 16180
		private const string N_SMALLINT = "SmallInt";

		// Token: 0x04003F35 RID: 16181
		private const string N_INT = "Int";

		// Token: 0x04003F36 RID: 16182
		private const string N_BIGINT = "BigInt";

		// Token: 0x04003F37 RID: 16183
		private const string N_CLOB = "CLOB";

		// Token: 0x04003F38 RID: 16184
		private const string N_DBCLOB = "DBCLOB";

		// Token: 0x04003F39 RID: 16185
		private const string N_BLOB = "BLOB";

		// Token: 0x04003F3A RID: 16186
		private const string N_XML = "Xml";

		// Token: 0x04003F3B RID: 16187
		private static readonly DrdaMetaType[] dbTypeMetaType = new DrdaMetaType[26];

		// Token: 0x04003F3C RID: 16188
		private static readonly DrdaMetaType[] drdaTypeMetaType;

		// Token: 0x04003F3D RID: 16189
		private readonly Type _classType;

		// Token: 0x04003F3E RID: 16190
		private readonly int _fixedLength;

		// Token: 0x04003F3F RID: 16191
		private readonly bool _isFixed;

		// Token: 0x04003F40 RID: 16192
		private readonly bool _isLong;

		// Token: 0x04003F41 RID: 16193
		private readonly byte _precision;

		// Token: 0x04003F42 RID: 16194
		private readonly byte _scale;

		// Token: 0x04003F43 RID: 16195
		private readonly string _typeName;

		// Token: 0x04003F44 RID: 16196
		private readonly DrdaClientType _msDb2Type;

		// Token: 0x04003F45 RID: 16197
		private readonly DbType _dbType;

		// Token: 0x04003F46 RID: 16198
		private readonly short _clientType;

		// Token: 0x04003F47 RID: 16199
		private readonly long _maxSizeAS400;

		// Token: 0x04003F48 RID: 16200
		private readonly long _maxSizeMVS;

		// Token: 0x04003F49 RID: 16201
		private readonly long _maxSizeUDB;

		// Token: 0x04003F4A RID: 16202
		private readonly string _createParameter;

		// Token: 0x04003F4B RID: 16203
		private readonly bool _isCaseSensative;

		// Token: 0x04003F4C RID: 16204
		private readonly DrdaMetaType.SEARCHABLE _isSearchable;

		// Token: 0x04003F4D RID: 16205
		private readonly short _minimumScale;

		// Token: 0x04003F4E RID: 16206
		private readonly short _maximumScale;

		// Token: 0x04003F4F RID: 16207
		private readonly string _literalPrefix;

		// Token: 0x04003F50 RID: 16208
		private readonly string _literalSuffix;

		// Token: 0x020009FB RID: 2555
		public enum SEARCHABLE
		{
			// Token: 0x04003F52 RID: 16210
			DB_UNSEARCHABLE,
			// Token: 0x04003F53 RID: 16211
			DB_LIKE_ONLY,
			// Token: 0x04003F54 RID: 16212
			DB_ALL_EXCEPT_LIKE,
			// Token: 0x04003F55 RID: 16213
			DB_SEARCHABLE
		}
	}
}
