using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200024C RID: 588
	public static class ExceptionUtility
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x00009B3B File Offset: 0x00007D3B
		internal static void Initialize()
		{
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000342C8 File Offset: 0x000324C8
		public static bool IsFatal(Exception ex)
		{
			while (ex != null)
			{
				IMonitoredError monitoredError = ex as IMonitoredError;
				if (monitoredError != null && monitoredError.IsFatal())
				{
					return true;
				}
				if ((ex is OutOfMemoryException && !(ex is InsufficientMemoryException)) || ex is ThreadAbortException || ex is AccessViolationException || ex is SEHException || ex is StackOverflowException || ex is TypeInitializationException || (ex is NullReferenceException && ExtendedEnvironment.TreatNullReferenceExceptionAsFatal))
				{
					return true;
				}
				if (!string.IsNullOrEmpty(ExceptionUtility.s_additionalExceptionsToConsiderFatalTweak.Value) && ExceptionUtility.s_additionalExceptionsToConsiderFatalTweak.Value.Contains(ex.GetType().FullName))
				{
					return true;
				}
				if (ex is TypeInitializationException || ex is TargetInvocationException)
				{
					ex = ex.InnerException;
				}
				else
				{
					if (!(ex is AggregateException))
					{
						break;
					}
					ReadOnlyCollection<Exception> innerExceptions = (ex as AggregateException).Flatten().InnerExceptions;
					if (innerExceptions != null)
					{
						using (IEnumerator<Exception> enumerator = innerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.IsFatal())
								{
									return true;
								}
							}
						}
					}
					ex = ex.InnerException;
				}
			}
			return false;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000343EC File Offset: 0x000325EC
		public static bool IsBenign(Exception ex)
		{
			IMonitoredError monitoredError = ex as IMonitoredError;
			return monitoredError != null && monitoredError.IsBenign();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003440C File Offset: 0x0003260C
		public static bool IsPermanent(Exception ex)
		{
			IMonitoredError monitoredError = ex as IMonitoredError;
			if (monitoredError != null)
			{
				return monitoredError.IsPermanent();
			}
			return ex is ArgumentException;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00034438 File Offset: 0x00032638
		public static IEnumerable<Exception> GetInnerExceptions(Exception ex)
		{
			List<Exception> list = new List<Exception>(10);
			for (Exception ex2 = ex.InnerException; ex2 != null; ex2 = ex2.InnerException)
			{
				list.Add(ex2);
			}
			return list;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00034468 File Offset: 0x00032668
		public static Exception PeelTargetInvocationException(Exception ex)
		{
			if (ex is TargetInvocationException)
			{
				while (ex is TargetInvocationException && ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
			}
			return ex;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003448D File Offset: 0x0003268D
		private static Exception Clone(Exception exception)
		{
			return (Exception)ExceptionUtility.s_memberwiseClone.Invoke(exception, null);
		}

		// Token: 0x040005C7 RID: 1479
		private static Tweak<string> s_additionalExceptionsToConsiderFatalTweak = Anchor.Tweaks.RegisterTweak<string>("Microsoft.Cloud.Platform.Utils.AdditionalExceptionsToConsiderFatal", "An optional semicolon-delimited list of exception types that will be considered as fatal", string.Empty);

		// Token: 0x040005C8 RID: 1480
		public const string AdditionalExceptionsToConsiderFatalTweakName = "Microsoft.Cloud.Platform.Utils.AdditionalExceptionsToConsiderFatal";

		// Token: 0x040005C9 RID: 1481
		private static readonly FieldInfo sm_innerExceptionField = typeof(Exception).GetField("_innerException", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040005CA RID: 1482
		private static readonly FieldInfo sm_innerCallStackField = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040005CB RID: 1483
		private static readonly MethodBase s_memberwiseClone = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
