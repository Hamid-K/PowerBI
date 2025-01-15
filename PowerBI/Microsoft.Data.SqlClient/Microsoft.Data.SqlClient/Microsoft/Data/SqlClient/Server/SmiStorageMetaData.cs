using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000123 RID: 291
	internal class SmiStorageMetaData : SmiExtendedMetaData
	{
		// Token: 0x060016D4 RID: 5844 RVA: 0x00061AE8 File Offset: 0x0005FCE8
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity)
		{
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00061B24 File Offset: 0x0005FD24
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false)
		{
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00061B64 File Offset: 0x0005FD64
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._allowsDBNull = allowsDBNull;
			this._serverName = serverName;
			this._catalogName = catalogName;
			this._schemaName = schemaName;
			this._tableName = tableName;
			this._columnName = columnName;
			this._isKey = isKey;
			this._isIdentity = isIdentity;
			this._isColumnSet = isColumnSet;
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00061BDA File Offset: 0x0005FDDA
		internal bool AllowsDBNull
		{
			get
			{
				return this._allowsDBNull;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00061BE2 File Offset: 0x0005FDE2
		internal string ServerName
		{
			get
			{
				return this._serverName;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00061BEA File Offset: 0x0005FDEA
		internal string CatalogName
		{
			get
			{
				return this._catalogName;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x00061BF2 File Offset: 0x0005FDF2
		internal string SchemaName
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00061BFA File Offset: 0x0005FDFA
		internal string TableName
		{
			get
			{
				return this._tableName;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00061C02 File Offset: 0x0005FE02
		internal string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00061C0A File Offset: 0x0005FE0A
		internal SqlBoolean IsKey
		{
			get
			{
				return this._isKey;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00061C12 File Offset: 0x0005FE12
		internal bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00061C1A File Offset: 0x0005FE1A
		internal bool IsColumnSet
		{
			get
			{
				return this._isColumnSet;
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00061C24 File Offset: 0x0005FE24
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}         AllowsDBNull={2}\n\t{1}           ServerName='{3}'\n\t{1}          CatalogName='{4}'\n\t{1}           SchemaName='{5}'\n\t{1}            TableName='{6}'\n\t{1}           ColumnName='{7}'\n\t{1}                IsKey={8}\n\t{1}           IsIdentity={9}\n\t", new object[]
			{
				base.TraceString(indent),
				new string(' ', indent),
				this.AllowsDBNull,
				this.ServerName ?? "<null>",
				this.CatalogName ?? "<null>",
				this.SchemaName ?? "<null>",
				this.TableName ?? "<null>",
				this.ColumnName ?? "<null>",
				this.IsKey,
				this.IsIdentity
			});
		}

		// Token: 0x0400094B RID: 2379
		private readonly bool _allowsDBNull;

		// Token: 0x0400094C RID: 2380
		private readonly string _serverName;

		// Token: 0x0400094D RID: 2381
		private readonly string _catalogName;

		// Token: 0x0400094E RID: 2382
		private readonly string _schemaName;

		// Token: 0x0400094F RID: 2383
		private readonly string _tableName;

		// Token: 0x04000950 RID: 2384
		private readonly string _columnName;

		// Token: 0x04000951 RID: 2385
		private readonly SqlBoolean _isKey;

		// Token: 0x04000952 RID: 2386
		private readonly bool _isIdentity;

		// Token: 0x04000953 RID: 2387
		private readonly bool _isColumnSet;
	}
}
