using System;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000036 RID: 54
	internal abstract class ThreadSafeSqlCommand : IDisposable
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00009078 File Offset: 0x00007278
		protected ThreadSafeSqlCommand(SqlCommand command, IDisposable connectionLockContext)
		{
			RSTrace.CatalogTrace.Assert(command != null, "command");
			this.m_command = command;
			this.m_lockContext = connectionLockContext;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000090A4 File Offset: 0x000072A4
		public virtual void Dispose()
		{
			try
			{
				this.m_command.Dispose();
			}
			finally
			{
				this.ReleaseLockContext();
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000090D8 File Offset: 0x000072D8
		protected void ReleaseLockContext()
		{
			if (this.m_lockContext != null)
			{
				this.m_lockContext.Dispose();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000090ED File Offset: 0x000072ED
		protected SqlCommand Command
		{
			get
			{
				return this.m_command;
			}
		}

		// Token: 0x04000162 RID: 354
		private readonly SqlCommand m_command;

		// Token: 0x04000163 RID: 355
		private readonly IDisposable m_lockContext;
	}
}
