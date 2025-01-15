using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000D7 RID: 215
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class AsyncCallCompleteEventArgs : EventArgs
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0002695A File Offset: 0x00024B5A
		public AsyncCallCompleteEventArgs(Exception exception, bool cancelled)
		{
			this.m_exception = exception;
			this.m_cancelled = cancelled;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00026970 File Offset: 0x00024B70
		public Exception Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00026978 File Offset: 0x00024B78
		public bool ExceptionOccurred
		{
			get
			{
				return this.m_exception != null;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00026983 File Offset: 0x00024B83
		public bool Cancelled
		{
			get
			{
				return this.m_cancelled;
			}
		}

		// Token: 0x040004CD RID: 1229
		private readonly Exception m_exception;

		// Token: 0x040004CE RID: 1230
		private readonly bool m_cancelled;
	}
}
