using System;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x02000713 RID: 1811
	internal sealed class OdbcStatementHandle : OdbcHandle
	{
		// Token: 0x060035E7 RID: 13799 RVA: 0x000AB6C7 File Offset: 0x000A98C7
		public OdbcStatementHandle(IOdbcInterop odbcInterop, OdbcConnectionHandle connectionHandle)
			: base(odbcInterop, Odbc32.SQL_HANDLE.STMT, connectionHandle)
		{
			this.isV3Driver = connectionHandle.IsV3Driver;
			this.isBigIntSupportedByDriver = connectionHandle.IsBigIntSupportedByDriver;
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x060035E8 RID: 13800 RVA: 0x000AB6EA File Offset: 0x000A98EA
		public bool IsV3Driver
		{
			get
			{
				return this.isV3Driver;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000AB6F2 File Offset: 0x000A98F2
		public bool IsBigIntSupportedByDriver
		{
			get
			{
				return this.isBigIntSupportedByDriver;
			}
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000AB6FA File Offset: 0x000A98FA
		public Odbc32.RetCode BindColumn(int columnNumber, Odbc32.SQL_C targetType, IntPtr buffer, long length, IntPtr lengthOrIndicator)
		{
			return this.odbcInterop.SQLBindCol(this, checked((ushort)columnNumber), targetType, buffer, new IntPtr(length), lengthOrIndicator);
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000AB718 File Offset: 0x000A9918
		public Odbc32.RetCode BindParameter(short ordinal, short parameterDirection, Odbc32.SQL_C sqlctype, Odbc32.SQL_TYPE sqltype, ulong cchSize, long scale, HandleRef buffer, long bufferLength, HandleRef intbuffer)
		{
			return this.odbcInterop.SQLBindParameter(this, checked((ushort)ordinal), parameterDirection, sqlctype, (short)sqltype, new UIntPtr(cchSize), new IntPtr(scale), buffer, new IntPtr(bufferLength), intbuffer);
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000AB750 File Offset: 0x000A9950
		public OdbcDescriptorHandle GetDescriptorHandle(Odbc32.SQL_ATTR attribute)
		{
			if (this.descriptorHandle == null)
			{
				this.descriptorHandle = new OdbcDescriptorHandle(this.odbcInterop, this, attribute);
			}
			return this.descriptorHandle;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000AB773 File Offset: 0x000A9973
		public Odbc32.RetCode Cancel()
		{
			return this.odbcInterop.SQLCancel(this);
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000AB784 File Offset: 0x000A9984
		public Odbc32.RetCode GetColumnAttribute(int columnNumber, ushort fieldIdentifier, OdbcBuffer characterAttribute, out short stringLength, out long numericAttribute)
		{
			checked
			{
				IntPtr intPtr;
				Odbc32.RetCode retCode;
				if (characterAttribute == null)
				{
					retCode = this.odbcInterop.SQLColAttributeW(this, (ushort)columnNumber, fieldIdentifier, IntPtr.Zero, 0, out stringLength, out intPtr);
				}
				else
				{
					retCode = this.odbcInterop.SQLColAttributeW(this, (ushort)columnNumber, fieldIdentifier, characterAttribute, characterAttribute.ShortLength, out stringLength, out intPtr);
				}
				numericAttribute = intPtr.ToInt64();
				return retCode;
			}
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000AB7D8 File Offset: 0x000A99D8
		public short GetColumnAttribute(int columnNumber, ushort fieldIdentifier, OdbcBuffer buffer)
		{
			short num;
			long num2;
			OdbcUtils.HandleError(this, this.GetColumnAttribute(columnNumber, fieldIdentifier, buffer, out num, out num2));
			return num;
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000AB7FC File Offset: 0x000A99FC
		public long GetColumnAttribute(int columnNumber, Odbc32.SQL_DESC fieldIdentifier)
		{
			short num = 0;
			IntPtr intPtr;
			Odbc32.RetCode retCode = this.odbcInterop.SQLColAttributeW(this, checked((ushort)columnNumber), (ushort)fieldIdentifier, IntPtr.Zero, 0, out num, out intPtr);
			OdbcUtils.HandleError(this, retCode);
			return intPtr.ToInt64();
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000AB834 File Offset: 0x000A9A34
		public long GetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, Odbc32.SQL_COLUMN v2FieldId)
		{
			long num;
			Odbc32.RetCode columnAttribute = this.GetColumnAttribute(columnNumber, v3FieldId, v2FieldId, out num);
			OdbcUtils.HandleError(this, columnAttribute);
			return num;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000AB858 File Offset: 0x000A9A58
		public short GetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, Odbc32.SQL_COLUMN v2FieldId, OdbcBuffer buffer)
		{
			short num;
			Odbc32.RetCode columnAttribute = this.GetColumnAttribute(columnNumber, v3FieldId, v2FieldId, buffer, out num);
			OdbcUtils.HandleError(this, columnAttribute);
			return num;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000AB87C File Offset: 0x000A9A7C
		public bool TryGetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, Odbc32.SQL_COLUMN v2FieldId, OdbcBuffer buffer, out short resultLength)
		{
			if (this.GetColumnAttribute(columnNumber, v3FieldId, v2FieldId, buffer, out resultLength) == Odbc32.RetCode.SUCCESS)
			{
				return true;
			}
			resultLength = 0;
			return false;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000AB894 File Offset: 0x000A9A94
		public bool TryGetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, Odbc32.SQL_COLUMN v2FieldId, out long numericAttribute)
		{
			if (this.GetColumnAttribute(columnNumber, v3FieldId, v2FieldId, out numericAttribute) == Odbc32.RetCode.SUCCESS)
			{
				return true;
			}
			numericAttribute = 0L;
			return false;
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000AB8AC File Offset: 0x000A9AAC
		public bool TryGetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, out long numericAttribute)
		{
			short num;
			if (this.isV3Driver && this.GetColumnAttribute(columnNumber, (ushort)v3FieldId, null, out num, out numericAttribute) == Odbc32.RetCode.SUCCESS)
			{
				return true;
			}
			numericAttribute = 0L;
			return false;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000AB8D8 File Offset: 0x000A9AD8
		private Odbc32.RetCode GetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, Odbc32.SQL_COLUMN v2FieldId, OdbcBuffer buffer, out short resultLength)
		{
			long num;
			return this.GetColumnAttribute(columnNumber, v3FieldId, v2FieldId, buffer, out resultLength, out num);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000AB8F4 File Offset: 0x000A9AF4
		private Odbc32.RetCode GetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, Odbc32.SQL_COLUMN v2FieldId, out long numericAttribute)
		{
			short num;
			return this.GetColumnAttribute(columnNumber, v3FieldId, v2FieldId, null, out num, out numericAttribute);
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000AB90F File Offset: 0x000A9B0F
		private Odbc32.RetCode GetColumnAttribute(int columnNumber, Odbc32.SQL_DESC v3FieldId, Odbc32.SQL_COLUMN v2FieldId, OdbcBuffer buffer, out short resultLength, out long numericAttribute)
		{
			if (this.isV3Driver)
			{
				return this.GetColumnAttribute(columnNumber, (ushort)v3FieldId, buffer, out resultLength, out numericAttribute);
			}
			return this.GetColumnAttribute(columnNumber, (ushort)v2FieldId, buffer, out resultLength, out numericAttribute);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000AB938 File Offset: 0x000A9B38
		public Odbc32.RetCode Columns(string tableCatalog, string tableSchema, string tableName, string columnName)
		{
			return this.odbcInterop.SQLColumnsW(this, tableCatalog, OdbcUtils.ShortStringLength(tableCatalog), tableSchema, OdbcUtils.ShortStringLength(tableSchema), tableName, OdbcUtils.ShortStringLength(tableName), columnName, OdbcUtils.ShortStringLength(columnName));
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000AB96F File Offset: 0x000A9B6F
		public Odbc32.RetCode ExecuteDirect(string commandText)
		{
			return this.odbcInterop.SQLExecDirectW(this, commandText, -3);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000AB980 File Offset: 0x000A9B80
		public Odbc32.RetCode Execute()
		{
			return this.odbcInterop.SQLExecute(this);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000AB98E File Offset: 0x000A9B8E
		public Odbc32.RetCode Fetch()
		{
			return this.odbcInterop.SQLFetch(this);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000AB99C File Offset: 0x000A9B9C
		public Odbc32.RetCode FreeStatement(Odbc32.STMT stmt)
		{
			return this.odbcInterop.SQLFreeStmt(this, stmt);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000AB9AC File Offset: 0x000A9BAC
		public long GetRowCount()
		{
			IntPtr intPtr;
			Odbc32.RetCode retCode = this.odbcInterop.SQLRowCount(this, out intPtr);
			OdbcUtils.HandleError(this, retCode);
			return intPtr.ToInt64();
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
		public Odbc32.RetCode SpecialColumns(string tableCatalog, string tableSchema, string tableName, Odbc32.SQL_SPECIALCOLS identifierType, Odbc32.SQL_SCOPE scope, Odbc32.SQL_NULLABILITY nullability)
		{
			return this.odbcInterop.SQLSpecialColumnsW(this, identifierType, tableCatalog, OdbcUtils.ShortStringLength(tableCatalog), tableSchema, OdbcUtils.ShortStringLength(tableSchema), tableName, OdbcUtils.ShortStringLength(tableName), scope, nullability);
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000ABA0C File Offset: 0x000A9C0C
		public Odbc32.RetCode GetData(int index, Odbc32.SQL_C sqlctype, IntPtr buffer, long bufferLength, out long cbActual)
		{
			IntPtr intPtr;
			Odbc32.RetCode retCode = this.odbcInterop.SQLGetData(this, checked((ushort)index), sqlctype, buffer, new IntPtr(bufferLength), out intPtr);
			cbActual = intPtr.ToInt64();
			return retCode;
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000ABA3C File Offset: 0x000A9C3C
		public Odbc32.RetCode GetStatementAttribute(Odbc32.SQL_ATTR attribute, out IntPtr value, out int stringLength)
		{
			return this.odbcInterop.SQLGetStmtAttrW(this, attribute, out value, IntPtr.Size, out stringLength);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000ABA52 File Offset: 0x000A9C52
		public Odbc32.RetCode GetTypeInfo(short fSqlType)
		{
			return this.odbcInterop.SQLGetTypeInfo(this, fSqlType);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000ABA61 File Offset: 0x000A9C61
		public Odbc32.RetCode NumberOfResultColumns(out short columnCount)
		{
			return this.odbcInterop.SQLNumResultCols(this, out columnCount);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000ABA70 File Offset: 0x000A9C70
		public Odbc32.RetCode MoreResults()
		{
			return this.odbcInterop.SQLMoreResults(this);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000ABA7E File Offset: 0x000A9C7E
		public Odbc32.RetCode Prepare(string commandText)
		{
			return this.odbcInterop.SQLPrepareW(this, commandText, -3);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000ABA8F File Offset: 0x000A9C8F
		public Odbc32.RetCode PrimaryKeys(string catalogName, string schemaName, string tableName)
		{
			return this.odbcInterop.SQLPrimaryKeysW(this, catalogName, OdbcUtils.ShortStringLength(catalogName), schemaName, OdbcUtils.ShortStringLength(schemaName), tableName, OdbcUtils.ShortStringLength(tableName));
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000ABAB4 File Offset: 0x000A9CB4
		public Odbc32.RetCode ForeignKeys(string pkCatalogName, string pkSchemaName, string pkTableName, string fkCatalogName, string fkSchemaName, string fkTableName)
		{
			return this.odbcInterop.SQLForeignKeysW(this, pkCatalogName, OdbcUtils.ShortStringLength(pkCatalogName), pkSchemaName, OdbcUtils.ShortStringLength(pkSchemaName), pkTableName, OdbcUtils.ShortStringLength(pkTableName), fkCatalogName, OdbcUtils.ShortStringLength(fkCatalogName), fkSchemaName, OdbcUtils.ShortStringLength(fkSchemaName), fkTableName, OdbcUtils.ShortStringLength(fkTableName));
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000ABAFD File Offset: 0x000A9CFD
		public Odbc32.RetCode SetStatementAttribute(Odbc32.SQL_ATTR attribute, IntPtr value, Odbc32.SQL_IS stringLength)
		{
			return this.odbcInterop.SQLSetStmtAttrW(this, (int)attribute, value, (int)stringLength);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000ABB10 File Offset: 0x000A9D10
		public Odbc32.RetCode Tables(string tableCatalog, string tableSchema, string tableName, string tableType)
		{
			return this.odbcInterop.SQLTablesW(this, tableCatalog, OdbcUtils.ShortStringLength(tableCatalog), tableSchema, OdbcUtils.ShortStringLength(tableSchema), tableName, OdbcUtils.ShortStringLength(tableName), tableType, OdbcUtils.ShortStringLength(tableType));
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000ABB47 File Offset: 0x000A9D47
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.descriptorHandle != null)
			{
				this.descriptorHandle.Dispose();
				this.descriptorHandle = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001BA3 RID: 7075
		private readonly bool isV3Driver;

		// Token: 0x04001BA4 RID: 7076
		private readonly bool isBigIntSupportedByDriver;

		// Token: 0x04001BA5 RID: 7077
		private OdbcDescriptorHandle descriptorHandle;
	}
}
