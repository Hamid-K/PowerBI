using System;
using System.CodeDom.Compiler;

namespace System.Data.Entity.SqlServer.Resources
{
	// Token: 0x02000042 RID: 66
	[GeneratedCode("Resources.SqlServer.tt", "1.0.0.0")]
	internal static class Error
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x0001A55D File Offset: 0x0001875D
		internal static Exception InvalidDatabaseName(object p0)
		{
			return new ArgumentException(Strings.InvalidDatabaseName(p0));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001A56A File Offset: 0x0001876A
		internal static Exception SqlServerMigrationSqlGenerator_UnknownOperation(object p0, object p1)
		{
			return new InvalidOperationException(Strings.SqlServerMigrationSqlGenerator_UnknownOperation(p0, p1));
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001A578 File Offset: 0x00018778
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001A580 File Offset: 0x00018780
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001A587 File Offset: 0x00018787
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
