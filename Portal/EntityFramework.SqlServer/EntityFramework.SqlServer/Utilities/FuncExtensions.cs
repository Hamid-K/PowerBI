using System;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200001D RID: 29
	internal static class FuncExtensions
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		internal static TResult NullIfNotImplemented<TResult>(this Func<TResult> func)
		{
			TResult tresult;
			try
			{
				tresult = func();
			}
			catch (NotImplementedException)
			{
				tresult = default(TResult);
			}
			return tresult;
		}
	}
}
