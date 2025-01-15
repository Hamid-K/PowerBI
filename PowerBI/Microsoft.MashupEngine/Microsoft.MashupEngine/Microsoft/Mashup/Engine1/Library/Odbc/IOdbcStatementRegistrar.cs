using System;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005B2 RID: 1458
	internal interface IOdbcStatementRegistrar
	{
		// Token: 0x06002DFA RID: 11770
		OdbcStatementRegistration Register(OdbcStatementHandle statement);
	}
}
