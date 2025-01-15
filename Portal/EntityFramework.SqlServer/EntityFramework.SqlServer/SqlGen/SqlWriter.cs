using System;
using System.Data.Entity.Migrations.Utilities;
using System.IO;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003B RID: 59
	internal class SqlWriter : IndentedTextWriter
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x00019E2E File Offset: 0x0001802E
		public SqlWriter(StringBuilder b)
			: base(new StringWriter(b, IndentedTextWriter.Culture))
		{
		}
	}
}
