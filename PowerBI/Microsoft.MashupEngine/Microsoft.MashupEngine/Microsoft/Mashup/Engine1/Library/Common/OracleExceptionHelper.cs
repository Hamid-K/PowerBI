using System;
using System.Data.Common;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001163 RID: 4451
	internal static class OracleExceptionHelper
	{
		// Token: 0x060074A1 RID: 29857 RVA: 0x00190584 File Offset: 0x0018E784
		public static bool TryGetErrorCode(DbException dbException, out int errorCode, IHostTrace trace = null)
		{
			if (dbException == null)
			{
				errorCode = -1;
				return false;
			}
			if (dbException.GetType().FullName == "Oracle.DataAccess.Client.OracleException")
			{
				errorCode = OracleExceptionHelper.GetPropertyValue(dbException, "Number", trace);
				return true;
			}
			errorCode = 0;
			return false;
		}

		// Token: 0x060074A2 RID: 29858 RVA: 0x001905BC File Offset: 0x0018E7BC
		public static int GetPropertyValue(DbException exception, string propertyName, IHostTrace trace)
		{
			PropertyInfo property = exception.GetType().GetProperty(propertyName);
			if (property != null)
			{
				try
				{
					return (int)property.GetGetMethod().Invoke(exception, new object[0]);
				}
				catch (Exception ex)
				{
					if ((trace != null) ? (!SafeExceptions.TraceIsSafeException(trace, ex)) : (!SafeExceptions.IsSafeException(ex)))
					{
						throw;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x0400401F RID: 16415
		public const int OracleInvalidCredentialsErrorCode = 1017;

		// Token: 0x04004020 RID: 16416
		public const int OracleOverflowExceptionErrorCode = 22053;
	}
}
