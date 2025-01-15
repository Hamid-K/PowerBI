using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000251 RID: 593
	public static class ExceptionExtensionMethods
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x00034622 File Offset: 0x00032822
		public static bool IsFatal(this Exception ex)
		{
			return ExceptionUtility.IsFatal(ex);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003462A File Offset: 0x0003282A
		public static bool IsBenign(this Exception ex)
		{
			return ExceptionUtility.IsBenign(ex);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00034632 File Offset: 0x00032832
		public static bool IsPermanent(this Exception ex)
		{
			return ExceptionUtility.IsPermanent(ex);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003463A File Offset: 0x0003283A
		public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
		{
			return ExceptionUtility.GetInnerExceptions(ex);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00034642 File Offset: 0x00032842
		public static Exception PeelTargetInvocationException(this Exception ex)
		{
			return ExceptionUtility.PeelTargetInvocationException(ex);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003464C File Offset: 0x0003284C
		public static bool ContainsInnerExceptionOfType<TException>(this Exception exception, out TException innerException) where TException : Exception
		{
			while (exception != null)
			{
				if (exception is TException)
				{
					innerException = (TException)((object)exception);
					return true;
				}
				AggregateException ex = exception as AggregateException;
				if (ex != null)
				{
					using (IEnumerator<Exception> enumerator = ex.InnerExceptions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.ContainsInnerExceptionOfType(out innerException))
							{
								return true;
							}
						}
					}
					innerException = default(TException);
					return false;
				}
				exception = exception.InnerException;
			}
			innerException = default(TException);
			return false;
		}
	}
}
