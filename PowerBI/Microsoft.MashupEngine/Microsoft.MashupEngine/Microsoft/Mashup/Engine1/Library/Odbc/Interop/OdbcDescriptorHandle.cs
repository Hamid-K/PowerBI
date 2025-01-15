using System;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006DF RID: 1759
	internal sealed class OdbcDescriptorHandle : OdbcHandle
	{
		// Token: 0x060034E3 RID: 13539 RVA: 0x000AA6F0 File Offset: 0x000A88F0
		public OdbcDescriptorHandle(IOdbcInterop odbcInterop, OdbcStatementHandle statementHandle, Odbc32.SQL_ATTR attribute)
			: base(odbcInterop, statementHandle, attribute)
		{
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000AA6FB File Offset: 0x000A88FB
		public Odbc32.RetCode GetDescriptionField(int i, Odbc32.SQL_DESC attribute, OdbcBuffer buffer, out int numericAttribute)
		{
			return this.odbcInterop.SQLGetDescFieldW(this, checked((ushort)i), attribute, buffer, (int)buffer.ShortLength, out numericAttribute);
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000AA715 File Offset: 0x000A8915
		public Odbc32.RetCode SetDescriptionField(short ordinal, Odbc32.SQL_DESC type, IntPtr value)
		{
			return this.odbcInterop.SQLSetDescFieldW(this, ordinal, type, value, 0);
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000AA727 File Offset: 0x000A8927
		public Odbc32.RetCode SetDescriptionField(short ordinal, Odbc32.SQL_DESC type, HandleRef value)
		{
			return this.odbcInterop.SQLSetDescFieldW(this, ordinal, type, value, 0);
		}
	}
}
