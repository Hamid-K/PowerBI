using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Internal;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006E4 RID: 1764
	internal sealed class OdbcInterop : IOdbcInterop
	{
		// Token: 0x06003503 RID: 13571 RVA: 0x000AAF38 File Offset: 0x000A9138
		public OdbcInterop(string dll)
		{
			this.libraryHandle = DynamicLinkLibrary.LoadLibrary(dll);
			if (this.libraryHandle.IsInvalid)
			{
				Win32Exception ex = new Win32Exception();
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Could not load DLL '{0}': {1}", dll, ex.Message), ex);
			}
			this.allocHandle = this.GetProcAddress<OdbcInterop.NativeMethods.SQLAllocHandle>("SQLAllocHandle");
			this.allocHandle2 = this.GetProcAddress<OdbcInterop.NativeMethods.SQLAllocHandle2>("SQLAllocHandle");
			this.bindCol = this.GetProcAddress<OdbcInterop.NativeMethods.SQLBindCol>("SQLAllocHandle");
			this.bindCol2 = this.GetProcAddress<OdbcInterop.NativeMethods.SQLBindCol2>("SQLBindCol");
			this.bindParameter = this.GetProcAddress<OdbcInterop.NativeMethods.SQLBindParameter>("SQLBindParameter");
			this.cancel = this.GetProcAddress<OdbcInterop.NativeMethods.SQLCancel>("SQLCancel");
			this.closeCursor = this.GetProcAddress<OdbcInterop.NativeMethods.SQLCloseCursor>("SQLCloseCursor");
			this.colAttribute = this.GetProcAddress<OdbcInterop.NativeMethods.SQLColAttributeW>("SQLColAttributeW");
			this.colAttribute2 = this.GetProcAddress<OdbcInterop.NativeMethods.SQLColAttributeW2>("SQLColAttributeW");
			this.columns = this.GetProcAddress<OdbcInterop.NativeMethods.SQLColumnsW>("SQLColumnsW");
			this.disconnect = this.GetProcAddress<OdbcInterop.NativeMethods.SQLDisconnect>("SQLDisconnect");
			this.driverConnect = this.GetProcAddress<OdbcInterop.NativeMethods.SQLDriverConnectW>("SQLDriverConnectW");
			this.execDirect = this.GetProcAddress<OdbcInterop.NativeMethods.SQLExecDirectW>("SQLExecDirectW");
			this.execute = this.GetProcAddress<OdbcInterop.NativeMethods.SQLExecute>("SQLExecute");
			this.fetch = this.GetProcAddress<OdbcInterop.NativeMethods.SQLFetch>("SQLFetch");
			this.foreignKeys = this.GetProcAddress<OdbcInterop.NativeMethods.SQLForeignKeysW>("SQLForeignKeysW");
			this.freeHandle = this.GetProcAddress<OdbcInterop.NativeMethods.SQLFreeHandle>("SQLFreeHandle");
			this.freeStmt = this.GetProcAddress<OdbcInterop.NativeMethods.SQLFreeStmt>("SQLFreeStmt");
			this.getConnectAttr = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetConnectAttrW>("SQLGetConnectAttrW");
			this.getConnectAttr2 = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetConnectAttrW2>("SQLGetConnectAttrW");
			this.getData = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetData>("SQLGetData");
			this.getDescField = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetDescFieldW>("SQLGetDescFieldW");
			this.getDiagField = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetDiagFieldW>("SQLGetDiagFieldW");
			this.getDiagField2 = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetDiagFieldW2>("SQLGetDiagFieldW");
			this.getDiagRec = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetDiagRecW>("SQLGetDiagRecW");
			this.getFunctions = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetFunctions>("SQLGetFunctions");
			this.getInfo = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetInfoW>("SQLGetInfoW");
			this.getStmtAttr = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetStmtAttrW>("SQLGetStmtAttrW");
			this.getTypeInfo = this.GetProcAddress<OdbcInterop.NativeMethods.SQLGetTypeInfo>("SQLGetTypeInfo");
			this.moreResults = this.GetProcAddress<OdbcInterop.NativeMethods.SQLMoreResults>("SQLMoreResults");
			this.numResultCols = this.GetProcAddress<OdbcInterop.NativeMethods.SQLNumResultCols>("SQLNumResultCols");
			this.prepare = this.GetProcAddress<OdbcInterop.NativeMethods.SQLPrepareW>("SQLPrepareW");
			this.primaryKeys = this.GetProcAddress<OdbcInterop.NativeMethods.SQLPrimaryKeysW>("SQLPrimaryKeysW");
			this.procedureColumns = this.GetProcAddress<OdbcInterop.NativeMethods.SQLProcedureColumnsW>("SQLProcedureColumnsW");
			this.procedures = this.GetProcAddress<OdbcInterop.NativeMethods.SQLProceduresW>("SQLProceduresW");
			this.rowCount = this.GetProcAddress<OdbcInterop.NativeMethods.SQLRowCount>("SQLRowCount");
			this.setConnectAttr = this.GetProcAddress<OdbcInterop.NativeMethods.SQLSetConnectAttrW>("SQLSetConnectAttrW");
			this.setConnectAttr2 = this.GetProcAddress<OdbcInterop.NativeMethods.SQLSetConnectAttrW2>("SQLSetConnectAttrW");
			this.setDescField = this.GetProcAddress<OdbcInterop.NativeMethods.SQLSetDescFieldW>("SQLSetDescFieldW");
			this.setDescField2 = this.GetProcAddress<OdbcInterop.NativeMethods.SQLSetDescFieldW2>("SQLSetDescFieldW");
			this.setEnv = this.GetProcAddress<OdbcInterop.NativeMethods.SQLSetEnvAttr>("SQLSetEnvAttr");
			this.setStmtAttr = this.GetProcAddress<OdbcInterop.NativeMethods.SQLSetStmtAttrW>("SQLSetStmtAttrW");
			this.specialColumns = this.GetProcAddress<OdbcInterop.NativeMethods.SQLSpecialColumnsW>("SQLSpecialColumnsW");
			this.statistics = this.GetProcAddress<OdbcInterop.NativeMethods.SQLStatisticsW>("SQLStatisticsW");
			this.tables = this.GetProcAddress<OdbcInterop.NativeMethods.SQLTablesW>("SQLTablesW");
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000AB284 File Offset: 0x000A9484
		private TDelegate GetProcAddress<TDelegate>(string functionName) where TDelegate : class
		{
			Delegate procAddress = DynamicLinkLibrary.GetProcAddress(this.libraryHandle, functionName, typeof(TDelegate));
			if (procAddress == null)
			{
				throw new InvalidOperationException(functionName);
			}
			return (TDelegate)((object)procAddress);
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000AB2AB File Offset: 0x000A94AB
		public SafeHandle LibraryHandle
		{
			get
			{
				return this.libraryHandle;
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000AB2B3 File Offset: 0x000A94B3
		public Odbc32.RetCode SQLAllocHandle(Odbc32.SQL_HANDLE HandleType, IntPtr InputHandle, out IntPtr OutputHandle)
		{
			return this.allocHandle(HandleType, InputHandle, out OutputHandle);
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000AB2C3 File Offset: 0x000A94C3
		public Odbc32.RetCode SQLAllocHandle(Odbc32.SQL_HANDLE HandleType, OdbcHandle InputHandle, out IntPtr OutputHandle)
		{
			return this.allocHandle2(HandleType, InputHandle, out OutputHandle);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000AB2D3 File Offset: 0x000A94D3
		public Odbc32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, HandleRef TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind)
		{
			return this.bindCol(StatementHandle, ColumnNumber, TargetType, TargetValue, BufferLength, StrLen_or_Ind);
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000AB2E9 File Offset: 0x000A94E9
		public Odbc32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind)
		{
			return this.bindCol2(StatementHandle, ColumnNumber, TargetType, TargetValue, BufferLength, StrLen_or_Ind);
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000AB300 File Offset: 0x000A9500
		public Odbc32.RetCode SQLBindParameter(OdbcStatementHandle StatementHandle, ushort ParameterNumber, short ParamDirection, Odbc32.SQL_C SQLCType, short SQLType, UIntPtr cbColDef, IntPtr ibScale, HandleRef rgbValue, IntPtr BufferLength, HandleRef StrLen_or_Ind)
		{
			return this.bindParameter(StatementHandle, ParameterNumber, ParamDirection, SQLCType, SQLType, cbColDef, ibScale, rgbValue, BufferLength, StrLen_or_Ind);
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000AB329 File Offset: 0x000A9529
		public Odbc32.RetCode SQLCancel(OdbcStatementHandle StatementHandle)
		{
			return this.cancel(StatementHandle);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000AB337 File Offset: 0x000A9537
		public Odbc32.RetCode SQLCloseCursor(OdbcStatementHandle StatementHandle)
		{
			return this.closeCursor(StatementHandle);
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000AB345 File Offset: 0x000A9545
		public Odbc32.RetCode SQLColAttributeW(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ushort FieldIdentifier, OdbcBuffer CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute)
		{
			return this.colAttribute(StatementHandle, ColumnNumber, FieldIdentifier, CharacterAttribute, BufferLength, out StringLength, out NumericAttribute);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000AB35D File Offset: 0x000A955D
		public Odbc32.RetCode SQLColAttributeW(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ushort FieldIdentifier, IntPtr CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute)
		{
			return this.colAttribute2(StatementHandle, ColumnNumber, FieldIdentifier, CharacterAttribute, BufferLength, out StringLength, out NumericAttribute);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000AB378 File Offset: 0x000A9578
		public Odbc32.RetCode SQLColumnsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, string ColumnName, short NameLen4)
		{
			return this.columns(StatementHandle, CatalogName, NameLen1, SchemaName, NameLen2, TableName, NameLen3, ColumnName, NameLen4);
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000AB39F File Offset: 0x000A959F
		public Odbc32.RetCode SQLDisconnect(IntPtr ConnectionHandle)
		{
			return this.disconnect(ConnectionHandle);
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000AB3B0 File Offset: 0x000A95B0
		public Odbc32.RetCode SQLDriverConnectW(OdbcConnectionHandle hdbc, IntPtr hwnd, string connectionstring, short cbConnectionstring, IntPtr connectionstringout, short cbConnectionstringoutMax, out short cbConnectionstringout, ushort fDriverCompletion)
		{
			return this.driverConnect(hdbc, hwnd, connectionstring, cbConnectionstring, connectionstringout, cbConnectionstringoutMax, out cbConnectionstringout, fDriverCompletion);
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000AB3D5 File Offset: 0x000A95D5
		public Odbc32.RetCode SQLExecDirectW(OdbcStatementHandle StatementHandle, string StatementText, int TextLength)
		{
			return this.execDirect(StatementHandle, StatementText, TextLength);
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000AB3E5 File Offset: 0x000A95E5
		public Odbc32.RetCode SQLExecute(OdbcStatementHandle StatementHandle)
		{
			return this.execute(StatementHandle);
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000AB3F3 File Offset: 0x000A95F3
		public Odbc32.RetCode SQLFetch(OdbcStatementHandle StatementHandle)
		{
			return this.fetch(StatementHandle);
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000AB404 File Offset: 0x000A9604
		public Odbc32.RetCode SQLForeignKeysW(OdbcStatementHandle StatementHandle, string pkCatalogName, short NameLen1, string pkSchemaName, short NameLen2, string pkTableName, short NameLen3, string fkCatalogName, short NameLen4, string fkSchemaName, short NameLen5, string fkTableName, short NameLen6)
		{
			return this.foreignKeys(StatementHandle, pkCatalogName, NameLen1, pkSchemaName, NameLen2, pkTableName, NameLen3, fkCatalogName, NameLen4, fkSchemaName, NameLen5, fkTableName, NameLen6);
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000AB433 File Offset: 0x000A9633
		public Odbc32.RetCode SQLFreeHandle(Odbc32.SQL_HANDLE HandleType, IntPtr StatementHandle)
		{
			return this.freeHandle(HandleType, StatementHandle);
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000AB442 File Offset: 0x000A9642
		public Odbc32.RetCode SQLFreeStmt(OdbcStatementHandle StatementHandle, Odbc32.STMT Option)
		{
			return this.freeStmt(StatementHandle, Option);
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000AB451 File Offset: 0x000A9651
		public Odbc32.RetCode SQLGetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, byte[] Value, int BufferLength, out int StringLength)
		{
			return this.getConnectAttr(ConnectionHandle, Attribute, Value, BufferLength, out StringLength);
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000AB465 File Offset: 0x000A9665
		public Odbc32.RetCode SQLGetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, StringBuilder Value, int BufferLength, out int StringLength)
		{
			return this.getConnectAttr2(ConnectionHandle, Attribute, Value, BufferLength, out StringLength);
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000AB479 File Offset: 0x000A9679
		public Odbc32.RetCode SQLGetData(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, out IntPtr StrLen_or_Ind)
		{
			return this.getData(StatementHandle, ColumnNumber, TargetType, TargetValue, BufferLength, out StrLen_or_Ind);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000AB48F File Offset: 0x000A968F
		public Odbc32.RetCode SQLGetDescFieldW(OdbcDescriptorHandle StatementHandle, ushort RecNumber, Odbc32.SQL_DESC FieldIdentifier, OdbcBuffer ValuePointer, int BufferLength, out int StringLength)
		{
			return this.getDescField(StatementHandle, RecNumber, FieldIdentifier, ValuePointer, BufferLength, out StringLength);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000AB4A8 File Offset: 0x000A96A8
		public Odbc32.RetCode SQLGetDiagRecW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, StringBuilder rchState, out int NativeError, StringBuilder MessageText, short BufferLength, out short TextLength)
		{
			return this.getDiagRec(HandleType, Handle, RecNumber, rchState, out NativeError, MessageText, BufferLength, out TextLength);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000AB4CD File Offset: 0x000A96CD
		public Odbc32.RetCode SQLGetDiagFieldW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, StringBuilder rchState, short BufferLength, out short StringLength)
		{
			return this.getDiagField(HandleType, Handle, RecNumber, DiagIdentifier, rchState, BufferLength, out StringLength);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000AB4E5 File Offset: 0x000A96E5
		public Odbc32.RetCode SQLGetDiagFieldW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, IntPtr rchState, short BufferLength, out short StringLength)
		{
			return this.getDiagField2(HandleType, Handle, RecNumber, DiagIdentifier, rchState, BufferLength, out StringLength);
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000AB4FD File Offset: 0x000A96FD
		public Odbc32.RetCode SQLGetFunctions(OdbcConnectionHandle hdbc, Odbc32.SQL_API fFunction, out short pfExists)
		{
			return this.getFunctions(hdbc, fFunction, out pfExists);
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000AB50D File Offset: 0x000A970D
		public Odbc32.RetCode SQLGetInfoW(OdbcConnectionHandle hdbc, Odbc32.SQL_INFO fInfoType, byte[] rgbInfoValue, short cbInfoValueMax, IntPtr pcbInfoValue)
		{
			return this.getInfo(hdbc, fInfoType, rgbInfoValue, cbInfoValueMax, pcbInfoValue);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000AB521 File Offset: 0x000A9721
		public Odbc32.RetCode SQLGetStmtAttrW(OdbcStatementHandle StatementHandle, Odbc32.SQL_ATTR Attribute, out IntPtr Value, int BufferLength, out int StringLength)
		{
			return this.getStmtAttr(StatementHandle, Attribute, out Value, BufferLength, out StringLength);
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000AB535 File Offset: 0x000A9735
		public Odbc32.RetCode SQLGetTypeInfo(OdbcStatementHandle StatementHandle, short fSqlType)
		{
			return this.getTypeInfo(StatementHandle, fSqlType);
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000AB544 File Offset: 0x000A9744
		public Odbc32.RetCode SQLMoreResults(OdbcStatementHandle StatementHandle)
		{
			return this.moreResults(StatementHandle);
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000AB552 File Offset: 0x000A9752
		public Odbc32.RetCode SQLNumResultCols(OdbcStatementHandle StatementHandle, out short ColumnCount)
		{
			return this.numResultCols(StatementHandle, out ColumnCount);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000AB561 File Offset: 0x000A9761
		public Odbc32.RetCode SQLPrepareW(OdbcStatementHandle StatementHandle, string StatementText, int TextLength)
		{
			return this.prepare(StatementHandle, StatementText, TextLength);
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000AB571 File Offset: 0x000A9771
		public Odbc32.RetCode SQLPrimaryKeysW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3)
		{
			return this.primaryKeys(StatementHandle, CatalogName, NameLen1, SchemaName, NameLen2, TableName, NameLen3);
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000AB58C File Offset: 0x000A978C
		public Odbc32.RetCode SQLProcedureColumnsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string ProcName, short NameLen3, string ColumnName, short NameLen4)
		{
			return this.procedureColumns(StatementHandle, CatalogName, NameLen1, SchemaName, NameLen2, ProcName, NameLen3, ColumnName, NameLen4);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000AB5B3 File Offset: 0x000A97B3
		public Odbc32.RetCode SQLProceduresW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string ProcName, short NameLen3)
		{
			return this.procedures(StatementHandle, CatalogName, NameLen1, SchemaName, NameLen2, ProcName, NameLen3);
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000AB5CB File Offset: 0x000A97CB
		public Odbc32.RetCode SQLRowCount(OdbcStatementHandle StatementHandle, out IntPtr RowCount)
		{
			return this.rowCount(StatementHandle, out RowCount);
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000AB5DA File Offset: 0x000A97DA
		public Odbc32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, string Value, int StringLength)
		{
			return this.setConnectAttr(ConnectionHandle, Attribute, Value, StringLength);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000AB5EC File Offset: 0x000A97EC
		public Odbc32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, IntPtr Value, int StringLength)
		{
			return this.setConnectAttr2(ConnectionHandle, Attribute, Value, StringLength);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000AB5FE File Offset: 0x000A97FE
		public Odbc32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, Odbc32.SQL_DESC FieldIdentifier, HandleRef CharacterAttribute, int BufferLength)
		{
			return this.setDescField(StatementHandle, ColumnNumber, FieldIdentifier, CharacterAttribute, BufferLength);
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000AB612 File Offset: 0x000A9812
		public Odbc32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, Odbc32.SQL_DESC FieldIdentifier, IntPtr CharacterAttribute, int BufferLength)
		{
			return this.setDescField2(StatementHandle, ColumnNumber, FieldIdentifier, CharacterAttribute, BufferLength);
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000AB626 File Offset: 0x000A9826
		public Odbc32.RetCode SQLSetEnvAttr(OdbcEnvironmentHandle EnvironmentHandle, Odbc32.SQL_ATTR Attribute, IntPtr Value, int StringLength)
		{
			return this.setEnv(EnvironmentHandle, Attribute, Value, StringLength);
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000AB638 File Offset: 0x000A9838
		public Odbc32.RetCode SQLSetStmtAttrW(OdbcStatementHandle StatementHandle, int Attribute, IntPtr Value, int StringLength)
		{
			return this.setStmtAttr(StatementHandle, Attribute, Value, StringLength);
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000AB64C File Offset: 0x000A984C
		public Odbc32.RetCode SQLSpecialColumnsW(OdbcStatementHandle StatementHandle, Odbc32.SQL_SPECIALCOLS IdentifierType, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, Odbc32.SQL_SCOPE Scope, Odbc32.SQL_NULLABILITY Nullable)
		{
			return this.specialColumns(StatementHandle, IdentifierType, CatalogName, NameLen1, SchemaName, NameLen2, TableName, NameLen3, Scope, Nullable);
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000AB678 File Offset: 0x000A9878
		public Odbc32.RetCode SQLStatisticsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, ushort Unique, ushort Reserved)
		{
			return this.statistics(StatementHandle, CatalogName, NameLen1, SchemaName, NameLen2, TableName, NameLen3, Unique, Reserved);
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000AB6A0 File Offset: 0x000A98A0
		public Odbc32.RetCode SQLTablesW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, string TableType, short NameLen4)
		{
			return this.tables(StatementHandle, CatalogName, NameLen1, SchemaName, NameLen2, TableName, NameLen3, TableType, NameLen4);
		}

		// Token: 0x04001B75 RID: 7029
		private readonly SafeHandle libraryHandle;

		// Token: 0x04001B76 RID: 7030
		private readonly OdbcInterop.NativeMethods.SQLAllocHandle allocHandle;

		// Token: 0x04001B77 RID: 7031
		private readonly OdbcInterop.NativeMethods.SQLAllocHandle2 allocHandle2;

		// Token: 0x04001B78 RID: 7032
		private readonly OdbcInterop.NativeMethods.SQLBindCol bindCol;

		// Token: 0x04001B79 RID: 7033
		private readonly OdbcInterop.NativeMethods.SQLBindCol2 bindCol2;

		// Token: 0x04001B7A RID: 7034
		private readonly OdbcInterop.NativeMethods.SQLBindParameter bindParameter;

		// Token: 0x04001B7B RID: 7035
		private readonly OdbcInterop.NativeMethods.SQLCancel cancel;

		// Token: 0x04001B7C RID: 7036
		private readonly OdbcInterop.NativeMethods.SQLCloseCursor closeCursor;

		// Token: 0x04001B7D RID: 7037
		private readonly OdbcInterop.NativeMethods.SQLColAttributeW colAttribute;

		// Token: 0x04001B7E RID: 7038
		private readonly OdbcInterop.NativeMethods.SQLColAttributeW2 colAttribute2;

		// Token: 0x04001B7F RID: 7039
		private readonly OdbcInterop.NativeMethods.SQLColumnsW columns;

		// Token: 0x04001B80 RID: 7040
		private readonly OdbcInterop.NativeMethods.SQLDisconnect disconnect;

		// Token: 0x04001B81 RID: 7041
		private readonly OdbcInterop.NativeMethods.SQLDriverConnectW driverConnect;

		// Token: 0x04001B82 RID: 7042
		private readonly OdbcInterop.NativeMethods.SQLExecDirectW execDirect;

		// Token: 0x04001B83 RID: 7043
		private readonly OdbcInterop.NativeMethods.SQLExecute execute;

		// Token: 0x04001B84 RID: 7044
		private readonly OdbcInterop.NativeMethods.SQLFetch fetch;

		// Token: 0x04001B85 RID: 7045
		private readonly OdbcInterop.NativeMethods.SQLForeignKeysW foreignKeys;

		// Token: 0x04001B86 RID: 7046
		private readonly OdbcInterop.NativeMethods.SQLFreeHandle freeHandle;

		// Token: 0x04001B87 RID: 7047
		private readonly OdbcInterop.NativeMethods.SQLFreeStmt freeStmt;

		// Token: 0x04001B88 RID: 7048
		private readonly OdbcInterop.NativeMethods.SQLGetConnectAttrW getConnectAttr;

		// Token: 0x04001B89 RID: 7049
		private readonly OdbcInterop.NativeMethods.SQLGetConnectAttrW2 getConnectAttr2;

		// Token: 0x04001B8A RID: 7050
		private readonly OdbcInterop.NativeMethods.SQLGetData getData;

		// Token: 0x04001B8B RID: 7051
		private readonly OdbcInterop.NativeMethods.SQLGetDescFieldW getDescField;

		// Token: 0x04001B8C RID: 7052
		private readonly OdbcInterop.NativeMethods.SQLGetDiagFieldW getDiagField;

		// Token: 0x04001B8D RID: 7053
		private readonly OdbcInterop.NativeMethods.SQLGetDiagFieldW2 getDiagField2;

		// Token: 0x04001B8E RID: 7054
		private readonly OdbcInterop.NativeMethods.SQLGetDiagRecW getDiagRec;

		// Token: 0x04001B8F RID: 7055
		private readonly OdbcInterop.NativeMethods.SQLGetFunctions getFunctions;

		// Token: 0x04001B90 RID: 7056
		private readonly OdbcInterop.NativeMethods.SQLGetInfoW getInfo;

		// Token: 0x04001B91 RID: 7057
		private readonly OdbcInterop.NativeMethods.SQLGetStmtAttrW getStmtAttr;

		// Token: 0x04001B92 RID: 7058
		private readonly OdbcInterop.NativeMethods.SQLGetTypeInfo getTypeInfo;

		// Token: 0x04001B93 RID: 7059
		private readonly OdbcInterop.NativeMethods.SQLMoreResults moreResults;

		// Token: 0x04001B94 RID: 7060
		private readonly OdbcInterop.NativeMethods.SQLNumResultCols numResultCols;

		// Token: 0x04001B95 RID: 7061
		private readonly OdbcInterop.NativeMethods.SQLPrepareW prepare;

		// Token: 0x04001B96 RID: 7062
		private readonly OdbcInterop.NativeMethods.SQLPrimaryKeysW primaryKeys;

		// Token: 0x04001B97 RID: 7063
		private readonly OdbcInterop.NativeMethods.SQLProcedureColumnsW procedureColumns;

		// Token: 0x04001B98 RID: 7064
		private readonly OdbcInterop.NativeMethods.SQLProceduresW procedures;

		// Token: 0x04001B99 RID: 7065
		private readonly OdbcInterop.NativeMethods.SQLRowCount rowCount;

		// Token: 0x04001B9A RID: 7066
		private readonly OdbcInterop.NativeMethods.SQLSetConnectAttrW setConnectAttr;

		// Token: 0x04001B9B RID: 7067
		private readonly OdbcInterop.NativeMethods.SQLSetConnectAttrW2 setConnectAttr2;

		// Token: 0x04001B9C RID: 7068
		private readonly OdbcInterop.NativeMethods.SQLSetDescFieldW setDescField;

		// Token: 0x04001B9D RID: 7069
		private readonly OdbcInterop.NativeMethods.SQLSetDescFieldW2 setDescField2;

		// Token: 0x04001B9E RID: 7070
		private readonly OdbcInterop.NativeMethods.SQLSetEnvAttr setEnv;

		// Token: 0x04001B9F RID: 7071
		private readonly OdbcInterop.NativeMethods.SQLSetStmtAttrW setStmtAttr;

		// Token: 0x04001BA0 RID: 7072
		private readonly OdbcInterop.NativeMethods.SQLSpecialColumnsW specialColumns;

		// Token: 0x04001BA1 RID: 7073
		private readonly OdbcInterop.NativeMethods.SQLStatisticsW statistics;

		// Token: 0x04001BA2 RID: 7074
		private readonly OdbcInterop.NativeMethods.SQLTablesW tables;

		// Token: 0x020006E5 RID: 1765
		private static class NativeMethods
		{
			// Token: 0x020006E6 RID: 1766
			// (Invoke) Token: 0x06003534 RID: 13620
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLAllocHandle(Odbc32.SQL_HANDLE HandleType, IntPtr InputHandle, out IntPtr OutputHandle);

			// Token: 0x020006E7 RID: 1767
			// (Invoke) Token: 0x06003538 RID: 13624
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLAllocHandle2(Odbc32.SQL_HANDLE HandleType, OdbcHandle InputHandle, out IntPtr OutputHandle);

			// Token: 0x020006E8 RID: 1768
			// (Invoke) Token: 0x0600353C RID: 13628
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, HandleRef TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

			// Token: 0x020006E9 RID: 1769
			// (Invoke) Token: 0x06003540 RID: 13632
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLBindCol2(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

			// Token: 0x020006EA RID: 1770
			// (Invoke) Token: 0x06003544 RID: 13636
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLBindParameter(OdbcStatementHandle StatementHandle, ushort ParameterNumber, short ParamDirection, Odbc32.SQL_C SQLCType, short SQLType, UIntPtr cbColDef, IntPtr ibScale, HandleRef rgbValue, IntPtr BufferLength, HandleRef StrLen_or_Ind);

			// Token: 0x020006EB RID: 1771
			// (Invoke) Token: 0x06003548 RID: 13640
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLCancel(OdbcStatementHandle StatementHandle);

			// Token: 0x020006EC RID: 1772
			// (Invoke) Token: 0x0600354C RID: 13644
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLCloseCursor(OdbcStatementHandle StatementHandle);

			// Token: 0x020006ED RID: 1773
			// (Invoke) Token: 0x06003550 RID: 13648
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLColAttributeW(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ushort FieldIdentifier, OdbcBuffer CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute);

			// Token: 0x020006EE RID: 1774
			// (Invoke) Token: 0x06003554 RID: 13652
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLColAttributeW2(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ushort FieldIdentifier, IntPtr CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute);

			// Token: 0x020006EF RID: 1775
			// (Invoke) Token: 0x06003558 RID: 13656
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLColumnsW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, [MarshalAs(UnmanagedType.LPWStr)] [In] string ColumnName, short NameLen4);

			// Token: 0x020006F0 RID: 1776
			// (Invoke) Token: 0x0600355C RID: 13660
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLDisconnect(IntPtr ConnectionHandle);

			// Token: 0x020006F1 RID: 1777
			// (Invoke) Token: 0x06003560 RID: 13664
			public delegate Odbc32.RetCode SQLDriverConnectW(OdbcConnectionHandle hdbc, IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] [In] string connectionstring, short cbConnectionstring, IntPtr connectionstringout, short cbConnectionstringoutMax, out short cbConnectionstringout, ushort fDriverCompletion);

			// Token: 0x020006F2 RID: 1778
			// (Invoke) Token: 0x06003564 RID: 13668
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLExecDirectW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string StatementText, int TextLength);

			// Token: 0x020006F3 RID: 1779
			// (Invoke) Token: 0x06003568 RID: 13672
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLExecute(OdbcStatementHandle StatementHandle);

			// Token: 0x020006F4 RID: 1780
			// (Invoke) Token: 0x0600356C RID: 13676
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLFetch(OdbcStatementHandle StatementHandle);

			// Token: 0x020006F5 RID: 1781
			// (Invoke) Token: 0x06003570 RID: 13680
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLForeignKeysW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string pkCatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string pkSchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string pkTableName, short NameLen3, [MarshalAs(UnmanagedType.LPWStr)] [In] string fkCatalogName, short NameLen4, [MarshalAs(UnmanagedType.LPWStr)] [In] string fkSchemaName, short NameLen5, [MarshalAs(UnmanagedType.LPWStr)] [In] string fkTableName, short NameLen6);

			// Token: 0x020006F6 RID: 1782
			// (Invoke) Token: 0x06003574 RID: 13684
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLFreeHandle(Odbc32.SQL_HANDLE HandleType, IntPtr StatementHandle);

			// Token: 0x020006F7 RID: 1783
			// (Invoke) Token: 0x06003578 RID: 13688
			public delegate Odbc32.RetCode SQLFreeStmt(OdbcStatementHandle StatementHandle, Odbc32.STMT Option);

			// Token: 0x020006F8 RID: 1784
			// (Invoke) Token: 0x0600357C RID: 13692
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, byte[] Value, int BufferLength, out int StringLength);

			// Token: 0x020006F9 RID: 1785
			// (Invoke) Token: 0x06003580 RID: 13696
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetConnectAttrW2(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder Value, int BufferLength, out int StringLength);

			// Token: 0x020006FA RID: 1786
			// (Invoke) Token: 0x06003584 RID: 13700
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLGetData(OdbcStatementHandle StatementHandle, ushort ColumnNumber, Odbc32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, out IntPtr StrLen_or_Ind);

			// Token: 0x020006FB RID: 1787
			// (Invoke) Token: 0x06003588 RID: 13704
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetDescFieldW(OdbcDescriptorHandle StatementHandle, ushort RecNumber, Odbc32.SQL_DESC FieldIdentifier, OdbcBuffer ValuePointer, int BufferLength, out int StringLength);

			// Token: 0x020006FC RID: 1788
			// (Invoke) Token: 0x0600358C RID: 13708
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetDiagRecW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, StringBuilder rchState, out int NativeError, StringBuilder MessageText, short BufferLength, out short TextLength);

			// Token: 0x020006FD RID: 1789
			// (Invoke) Token: 0x06003590 RID: 13712
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetDiagFieldW(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder rchState, short BufferLength, out short StringLength);

			// Token: 0x020006FE RID: 1790
			// (Invoke) Token: 0x06003594 RID: 13716
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetDiagFieldW2(Odbc32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, IntPtr rchState, short BufferLength, out short StringLength);

			// Token: 0x020006FF RID: 1791
			// (Invoke) Token: 0x06003598 RID: 13720
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLGetFunctions(OdbcConnectionHandle hdbc, Odbc32.SQL_API fFunction, out short pfExists);

			// Token: 0x02000700 RID: 1792
			// (Invoke) Token: 0x0600359C RID: 13724
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetInfoW(OdbcConnectionHandle hdbc, Odbc32.SQL_INFO fInfoType, byte[] rgbInfoValue, short cbInfoValueMax, IntPtr pcbInfoValue);

			// Token: 0x02000701 RID: 1793
			// (Invoke) Token: 0x060035A0 RID: 13728
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLGetStmtAttrW(OdbcStatementHandle StatementHandle, Odbc32.SQL_ATTR Attribute, out IntPtr Value, int BufferLength, out int StringLength);

			// Token: 0x02000702 RID: 1794
			// (Invoke) Token: 0x060035A4 RID: 13732
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLGetTypeInfo(OdbcStatementHandle StatementHandle, short fSqlType);

			// Token: 0x02000703 RID: 1795
			// (Invoke) Token: 0x060035A8 RID: 13736
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLMoreResults(OdbcStatementHandle StatementHandle);

			// Token: 0x02000704 RID: 1796
			// (Invoke) Token: 0x060035AC RID: 13740
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLNumResultCols(OdbcStatementHandle StatementHandle, out short ColumnCount);

			// Token: 0x02000705 RID: 1797
			// (Invoke) Token: 0x060035B0 RID: 13744
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLPrepareW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string StatementText, int TextLength);

			// Token: 0x02000706 RID: 1798
			// (Invoke) Token: 0x060035B4 RID: 13748
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLPrimaryKeysW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3);

			// Token: 0x02000707 RID: 1799
			// (Invoke) Token: 0x060035B8 RID: 13752
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLProcedureColumnsW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string ProcName, short NameLen3, [MarshalAs(UnmanagedType.LPWStr)] [In] string ColumnName, short NameLen4);

			// Token: 0x02000708 RID: 1800
			// (Invoke) Token: 0x060035BC RID: 13756
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLProceduresW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string ProcName, short NameLen3);

			// Token: 0x02000709 RID: 1801
			// (Invoke) Token: 0x060035C0 RID: 13760
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			public delegate Odbc32.RetCode SQLRowCount(OdbcStatementHandle StatementHandle, out IntPtr RowCount);

			// Token: 0x0200070A RID: 1802
			// (Invoke) Token: 0x060035C4 RID: 13764
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, string Value, int StringLength);

			// Token: 0x0200070B RID: 1803
			// (Invoke) Token: 0x060035C8 RID: 13768
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLSetConnectAttrW2(OdbcConnectionHandle ConnectionHandle, Odbc32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

			// Token: 0x0200070C RID: 1804
			// (Invoke) Token: 0x060035CC RID: 13772
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, Odbc32.SQL_DESC FieldIdentifier, HandleRef CharacterAttribute, int BufferLength);

			// Token: 0x0200070D RID: 1805
			// (Invoke) Token: 0x060035D0 RID: 13776
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLSetDescFieldW2(OdbcDescriptorHandle StatementHandle, short ColumnNumber, Odbc32.SQL_DESC FieldIdentifier, IntPtr CharacterAttribute, int BufferLength);

			// Token: 0x0200070E RID: 1806
			// (Invoke) Token: 0x060035D4 RID: 13780
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLSetEnvAttr(OdbcEnvironmentHandle EnvironmentHandle, Odbc32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

			// Token: 0x0200070F RID: 1807
			// (Invoke) Token: 0x060035D8 RID: 13784
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLSetStmtAttrW(OdbcStatementHandle StatementHandle, int Attribute, IntPtr Value, int StringLength);

			// Token: 0x02000710 RID: 1808
			// (Invoke) Token: 0x060035DC RID: 13788
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLSpecialColumnsW(OdbcStatementHandle StatementHandle, Odbc32.SQL_SPECIALCOLS IdentifierType, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, Odbc32.SQL_SCOPE Scope, Odbc32.SQL_NULLABILITY Nullable);

			// Token: 0x02000711 RID: 1809
			// (Invoke) Token: 0x060035E0 RID: 13792
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLStatisticsW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, ushort Unique, ushort Reserved);

			// Token: 0x02000712 RID: 1810
			// (Invoke) Token: 0x060035E4 RID: 13796
			[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
			public delegate Odbc32.RetCode SQLTablesW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableType, short NameLen4);
		}
	}
}
