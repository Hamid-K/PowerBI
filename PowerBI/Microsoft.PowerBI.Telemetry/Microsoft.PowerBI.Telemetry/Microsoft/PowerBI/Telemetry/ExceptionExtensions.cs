using System;
using System.Reflection;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000028 RID: 40
	public static class ExceptionExtensions
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00003D98 File Offset: 0x00001F98
		public static int GetHResult(this Exception e)
		{
			object obj = null;
			try
			{
				obj = ExceptionExtensions.hResult.GetValue(e, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is TargetException || ex is TargetParameterCountException || ex is MethodAccessException || ex is TargetInvocationException)
				{
					return 0;
				}
				throw;
			}
			if (obj != null && obj is int)
			{
				return (int)obj;
			}
			return 0;
		}

		// Token: 0x04000099 RID: 153
		private static readonly PropertyInfo hResult = typeof(Exception).GetProperty("HResult", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	}
}
