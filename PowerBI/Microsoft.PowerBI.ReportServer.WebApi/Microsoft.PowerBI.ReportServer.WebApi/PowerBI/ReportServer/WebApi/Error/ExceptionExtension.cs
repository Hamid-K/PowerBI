using System;

namespace Microsoft.PowerBI.ReportServer.WebApi.Error
{
	// Token: 0x02000038 RID: 56
	public static class ExceptionExtension
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00006E88 File Offset: 0x00005088
		public static bool ExceptionStackContains(this Exception ex, Predicate<Exception> pred)
		{
			for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
			{
				if (pred(ex2))
				{
					return true;
				}
			}
			return false;
		}
	}
}
