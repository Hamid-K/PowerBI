using System;
using System.Threading.Tasks;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200004E RID: 78
	public abstract class SqlAuthenticationProvider
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00011050 File Offset: 0x0000F250
		public static SqlAuthenticationProvider GetProvider(SqlAuthenticationMethod authenticationMethod)
		{
			return SqlAuthenticationProviderManager.Instance.GetProvider(authenticationMethod);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001105D File Offset: 0x0000F25D
		public static bool SetProvider(SqlAuthenticationMethod authenticationMethod, SqlAuthenticationProvider provider)
		{
			return SqlAuthenticationProviderManager.Instance.SetProvider(authenticationMethod, provider);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000BB08 File Offset: 0x00009D08
		public virtual void BeforeLoad(SqlAuthenticationMethod authenticationMethod)
		{
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0000BB08 File Offset: 0x00009D08
		public virtual void BeforeUnload(SqlAuthenticationMethod authenticationMethod)
		{
		}

		// Token: 0x060007BC RID: 1980
		public abstract bool IsSupported(SqlAuthenticationMethod authenticationMethod);

		// Token: 0x060007BD RID: 1981
		public abstract Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters);
	}
}
