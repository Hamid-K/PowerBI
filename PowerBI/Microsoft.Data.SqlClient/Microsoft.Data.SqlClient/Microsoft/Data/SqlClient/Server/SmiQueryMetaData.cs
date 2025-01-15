using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000124 RID: 292
	internal class SmiQueryMetaData : SmiStorageMetaData
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x00061CE4 File Offset: 0x0005FEE4
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00061D28 File Offset: 0x0005FF28
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00061D70 File Offset: 0x0005FF70
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isColumnSet)
		{
			this._isReadOnly = isReadOnly;
			this._isExpression = isExpression;
			this._isAliased = isAliased;
			this._isHidden = isHidden;
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00061DD0 File Offset: 0x0005FFD0
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00061DD8 File Offset: 0x0005FFD8
		internal SqlBoolean IsExpression
		{
			get
			{
				return this._isExpression;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00061DE0 File Offset: 0x0005FFE0
		internal SqlBoolean IsAliased
		{
			get
			{
				return this._isAliased;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00061DE8 File Offset: 0x0005FFE8
		internal SqlBoolean IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00061DF0 File Offset: 0x0005FFF0
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}           IsReadOnly={2}\n\t{1}         IsExpression={3}\n\t{1}            IsAliased={4}\n\t{1}             IsHidden={5}", new object[]
			{
				base.TraceString(indent),
				new string(' ', indent),
				base.AllowsDBNull,
				this.IsExpression,
				this.IsAliased,
				this.IsHidden
			});
		}

		// Token: 0x04000954 RID: 2388
		private readonly bool _isReadOnly;

		// Token: 0x04000955 RID: 2389
		private readonly SqlBoolean _isExpression;

		// Token: 0x04000956 RID: 2390
		private readonly SqlBoolean _isAliased;

		// Token: 0x04000957 RID: 2391
		private readonly SqlBoolean _isHidden;
	}
}
