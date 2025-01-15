using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000049 RID: 73
	internal class ExtensionBoundary
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00008BDF File Offset: 0x00006DDF
		private ExtensionBoundary(ExtensionBoundary.GetExceptionMethod getExceptionMethod)
		{
			this.m_getExceptionMethod = getExceptionMethod;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008BF0 File Offset: 0x00006DF0
		internal void Invoke(ExtensionBoundary.Method m)
		{
			try
			{
				m();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsStoppingException(ex))
				{
					throw;
				}
				if (ex is RSException)
				{
					throw;
				}
				throw this.m_getExceptionMethod(ex);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008C38 File Offset: 0x00006E38
		internal static ExtensionBoundary RdceBoundary
		{
			get
			{
				return ExtensionBoundary.m_rdceBoundary;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008C3F File Offset: 0x00006E3F
		internal static ExtensionBoundary AuthorizationExtensionBoundary
		{
			get
			{
				return ExtensionBoundary.m_authorizationExtensionBoundary;
			}
		}

		// Token: 0x04000107 RID: 263
		private static readonly ExtensionBoundary m_rdceBoundary = new ExtensionBoundary((Exception e) => new RdceWrappedException(e));

		// Token: 0x04000108 RID: 264
		private static readonly ExtensionBoundary m_authorizationExtensionBoundary = new ExtensionBoundary((Exception e) => new AuthorizationExtensionException(e));

		// Token: 0x04000109 RID: 265
		private ExtensionBoundary.GetExceptionMethod m_getExceptionMethod;

		// Token: 0x020000E8 RID: 232
		// (Invoke) Token: 0x060007B1 RID: 1969
		internal delegate void Method();

		// Token: 0x020000E9 RID: 233
		// (Invoke) Token: 0x060007B5 RID: 1973
		private delegate RSException GetExceptionMethod(Exception e);
	}
}
