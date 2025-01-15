using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200024A RID: 586
	public interface IDbConnectionFactory
	{
		// Token: 0x06001EB0 RID: 7856
		DbConnection CreateConnection(string nameOrConnectionString);
	}
}
