using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006E0 RID: 1760
	internal sealed class OdbcEnvironmentHandle : OdbcHandle
	{
		// Token: 0x060034E7 RID: 13543 RVA: 0x000AA739 File Offset: 0x000A8939
		public OdbcEnvironmentHandle(IOdbcInterop odbcInterop)
			: this(odbcInterop, true)
		{
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000AA744 File Offset: 0x000A8944
		public OdbcEnvironmentHandle(IOdbcInterop odbcInterop, bool enableDriverManagerPooling)
			: base(odbcInterop, Odbc32.SQL_HANDLE.ENV, null)
		{
			Odbc32.RetCode retCode = this.SetAttribute(Odbc32.SQL_ATTR.ODBC_VERSION, Odbc32.SQL_OV_ODBC3);
			if (enableDriverManagerPooling)
			{
				retCode = this.SetAttribute(Odbc32.SQL_ATTR.CONNECTION_POOLING, 2);
				if (retCode > Odbc32.RetCode.SUCCESS_WITH_INFO)
				{
					base.Dispose();
					throw new InvalidOperationException(Strings.OdbcConnectionPoolingInitError(retCode));
				}
			}
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000AA79B File Offset: 0x000A899B
		public Odbc32.RetCode SetAttribute(Odbc32.SQL_ATTR attribute, int value)
		{
			return this.odbcInterop.SQLSetEnvAttr(this, attribute, (IntPtr)value, -6);
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000AA7B4 File Offset: 0x000A89B4
		public unsafe Odbc32.RetCode SetAttribute(Odbc32.SQL_ATTR attribute, string value)
		{
			char* ptr = value;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.odbcInterop.SQLSetEnvAttr(this, attribute, (IntPtr)((void*)ptr), value.Length * 2);
		}
	}
}
