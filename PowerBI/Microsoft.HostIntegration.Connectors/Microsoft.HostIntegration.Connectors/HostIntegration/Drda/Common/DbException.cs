using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007C2 RID: 1986
	public abstract class DbException : Exception
	{
		// Token: 0x06003F1B RID: 16155 RVA: 0x0001E12D File Offset: 0x0001C32D
		public DbException()
		{
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0001E135 File Offset: 0x0001C335
		public DbException(string message)
			: base(message)
		{
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0001E13E File Offset: 0x0001C33E
		public DbException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06003F1E RID: 16158
		public abstract IDbExceptionInfo ExceptionInfo { get; }
	}
}
