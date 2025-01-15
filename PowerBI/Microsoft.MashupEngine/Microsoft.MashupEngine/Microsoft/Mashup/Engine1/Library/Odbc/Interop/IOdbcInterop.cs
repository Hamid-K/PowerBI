using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x0200068A RID: 1674
	internal interface IOdbcInterop
	{
		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003464 RID: 13412
		SafeHandle LibraryHandle { get; }

		// Token: 0x06003465 RID: 13413
		Odbc32.RetCode SQLAllocHandle(Odbc32.SQL_HANDLE HandleType, IntPtr InputHandle, out IntPtr OutputHandle);

		// Token: 0x06003466 RID: 13414
		Odbc32.RetCode SQLAllocHandle(Odbc32.SQL_HANDLE HandleType, OdbcHandle InputHandle, out IntPtr OutputHandle);

		// Token: 0x06003467 RID: 13415
		Odbc32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, HandleRef TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

		// Token: 0x06003468 RID: 13416
		Odbc32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

		// Token: 0x06003469 RID: 13417
		Odbc32.RetCode SQLBindParameter(OdbcStatementHandle StatementHandle, ushort ParameterNumber, short ParamDirection, Odbc32.SQL_C SQLCType, short SQLType, UIntPtr cbColDef, IntPtr ibScale, HandleRef rgbValue, IntPtr BufferLength, HandleRef StrLen_or_Ind);

		// Token: 0x0600346A RID: 13418
		Odbc32.RetCode SQLCancel(OdbcStatementHandle StatementHandle);

		// Token: 0x0600346B RID: 13419
		Odbc32.RetCode SQLCloseCursor(OdbcStatementHandle StatementHandle);

		// Token: 0x0600346C RID: 13420
		Odbc32.RetCode SQLColAttributeW(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ushort FieldIdentifier, OdbcBuffer CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute);

		// Token: 0x0600346D RID: 13421
		Odbc32.RetCode SQLColAttributeW(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ushort FieldIdentifier, IntPtr CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute);

		// Token: 0x0600346E RID: 13422
		Odbc32.RetCode SQLColumnsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, string ColumnName, short NameLen4);

		// Token: 0x0600346F RID: 13423
		Odbc32.RetCode SQLDisconnect(IntPtr ConnectionHandle);

		// Token: 0x06003470 RID: 13424
		Odbc32.RetCode SQLDriverConnectW(OdbcConnectionHandle hdbc, IntPtr hwnd, string connectionstring, short cbConnectionstring, IntPtr connectionstringout, short cbConnectionstringoutMax, out short cbConnectionstringout, ushort fDriverCompletion);

		// Token: 0x06003471 RID: 13425
		Odbc32.RetCode SQLExecDirectW(OdbcStatementHandle StatementHandle, string StatementText, int TextLength);

		// Token: 0x06003472 RID: 13426
		Odbc32.RetCode SQLExecute(OdbcStatementHandle StatementHandle);

		// Token: 0x06003473 RID: 13427
		Odbc32.RetCode SQLFetch(OdbcStatementHandle StatementHandle);

		// Token: 0x06003474 RID: 13428
		Odbc32.RetCode SQLForeignKeysW(OdbcStatementHandle StatementHandle, string pkCatalogName, short NameLen1, string pkSchemaName, short NameLen2, string pkTableName, short NameLen3, string fkCatalogName, short NameLen4, string fkSchemaName, short NameLen5, string fkTableName, short NameLen6);

		// Token: 0x06003475 RID: 13429
		Odbc32.RetCode SQLFreeHandle(Odbc32.SQL_HANDLE HandleType, IntPtr StatementHandle);

		// Token: 0x06003476 RID: 13430
		Odbc32.RetCode SQLFreeStmt(OdbcStatementHandle StatementHandle, Odbc32.STMT Option);

		// Token: 0x06003477 RID: 13431
		Odbc32.RetCode SQLGetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, byte[] Value, int BufferLength, out int StringLength);

		// Token: 0x06003478 RID: 13432
		Odbc32.RetCode SQLGetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, StringBuilder Value, int BufferLength, out int StringLength);

		// Token: 0x06003479 RID: 13433
		Odbc32.RetCode SQLGetData(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, out IntPtr StrLen_or_Ind);

		// Token: 0x0600347A RID: 13434
		Odbc32.RetCode SQLGetDescFieldW(OdbcDescriptorHandle StatementHandle, ushort RecNumber, Odbc32.SQL_DESC FieldIdentifier, OdbcBuffer ValuePointer, int BufferLength, out int StringLength);

		// Token: 0x0600347B RID: 13435
		Odbc32.RetCode SQLGetDiagRecW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, StringBuilder rchState, out int NativeError, StringBuilder MessageText, short BufferLength, out short TextLength);

		// Token: 0x0600347C RID: 13436
		Odbc32.RetCode SQLGetDiagFieldW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, StringBuilder rchState, short BufferLength, out short StringLength);

		// Token: 0x0600347D RID: 13437
		Odbc32.RetCode SQLGetDiagFieldW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, IntPtr rchState, short BufferLength, out short StringLength);

		// Token: 0x0600347E RID: 13438
		Odbc32.RetCode SQLGetFunctions(OdbcConnectionHandle hdbc, Odbc32.SQL_API fFunction, out short pfExists);

		// Token: 0x0600347F RID: 13439
		Odbc32.RetCode SQLGetInfoW(OdbcConnectionHandle hdbc, Odbc32.SQL_INFO fInfoType, byte[] rgbInfoValue, short cbInfoValueMax, IntPtr pcbInfoValue);

		// Token: 0x06003480 RID: 13440
		Odbc32.RetCode SQLGetStmtAttrW(OdbcStatementHandle StatementHandle, Odbc32.SQL_ATTR Attribute, out IntPtr Value, int BufferLength, out int StringLength);

		// Token: 0x06003481 RID: 13441
		Odbc32.RetCode SQLGetTypeInfo(OdbcStatementHandle StatementHandle, short fSqlType);

		// Token: 0x06003482 RID: 13442
		Odbc32.RetCode SQLMoreResults(OdbcStatementHandle StatementHandle);

		// Token: 0x06003483 RID: 13443
		Odbc32.RetCode SQLNumResultCols(OdbcStatementHandle StatementHandle, out short ColumnCount);

		// Token: 0x06003484 RID: 13444
		Odbc32.RetCode SQLPrepareW(OdbcStatementHandle StatementHandle, string StatementText, int TextLength);

		// Token: 0x06003485 RID: 13445
		Odbc32.RetCode SQLPrimaryKeysW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3);

		// Token: 0x06003486 RID: 13446
		Odbc32.RetCode SQLProcedureColumnsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string ProcName, short NameLen3, string ColumnName, short NameLen4);

		// Token: 0x06003487 RID: 13447
		Odbc32.RetCode SQLProceduresW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string ProcName, short NameLen3);

		// Token: 0x06003488 RID: 13448
		Odbc32.RetCode SQLRowCount(OdbcStatementHandle StatementHandle, out IntPtr RowCount);

		// Token: 0x06003489 RID: 13449
		Odbc32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, string Value, int StringLength);

		// Token: 0x0600348A RID: 13450
		Odbc32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

		// Token: 0x0600348B RID: 13451
		Odbc32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, Odbc32.SQL_DESC FieldIdentifier, HandleRef CharacterAttribute, int BufferLength);

		// Token: 0x0600348C RID: 13452
		Odbc32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, Odbc32.SQL_DESC FieldIdentifier, IntPtr CharacterAttribute, int BufferLength);

		// Token: 0x0600348D RID: 13453
		Odbc32.RetCode SQLSetEnvAttr(OdbcEnvironmentHandle EnvironmentHandle, Odbc32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

		// Token: 0x0600348E RID: 13454
		Odbc32.RetCode SQLSetStmtAttrW(OdbcStatementHandle StatementHandle, int Attribute, IntPtr Value, int StringLength);

		// Token: 0x0600348F RID: 13455
		Odbc32.RetCode SQLSpecialColumnsW(OdbcStatementHandle StatementHandle, Odbc32.SQL_SPECIALCOLS IdentifierType, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, Odbc32.SQL_SCOPE Scope, Odbc32.SQL_NULLABILITY Nullable);

		// Token: 0x06003490 RID: 13456
		Odbc32.RetCode SQLStatisticsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, ushort Unique, ushort Reserved);

		// Token: 0x06003491 RID: 13457
		Odbc32.RetCode SQLTablesW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, string TableType, short NameLen4);
	}
}
