using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DCD RID: 7629
	public static class EvaluationException
	{
		// Token: 0x0600BCFD RID: 48381 RVA: 0x00265B35 File Offset: 0x00263D35
		public static bool IsEvaluationException(this Exception exception)
		{
			return exception is RuntimeException || exception is ErrorException || exception is OperationCanceledException || exception is HostingException || exception is ContainerTerminatedException;
		}

		// Token: 0x0600BCFE RID: 48382 RVA: 0x00265B64 File Offset: 0x00263D64
		public static bool IsExpectedException(this Exception exception)
		{
			ErrorException ex = exception.ToEvaluationException() as ErrorException;
			return ex == null || ex.IsExpected;
		}

		// Token: 0x0600BCFF RID: 48383 RVA: 0x00265B88 File Offset: 0x00263D88
		public static Exception ToEvaluationException(this Exception exception)
		{
			if (!exception.IsEvaluationException())
			{
				exception = exception.ToErrorException();
			}
			return exception;
		}

		// Token: 0x0600BD00 RID: 48384 RVA: 0x00265B9C File Offset: 0x00263D9C
		public static Exception ToCallbackException(this Exception exception)
		{
			exception = exception.ToEvaluationException();
			ErrorException ex = exception as ErrorException;
			if (ex != null)
			{
				string text = new StackTrace(1, true).ToString();
				text = text.TrimEnd(Environment.NewLine.ToCharArray());
				exception = new ErrorException(ex.Message, text, ex.ClassName, ex.Details, ex.IsRecoverable, ex.IsExpected, ex);
				exception = exception.ToThrowableException();
			}
			return exception;
		}

		// Token: 0x0600BD01 RID: 48385 RVA: 0x00265C0C File Offset: 0x00263E0C
		public static Exception ToThrowableException(this Exception exception)
		{
			exception = exception.ToEvaluationException();
			ErrorException ex = exception as ErrorException;
			if (ex != null)
			{
				exception = new ErrorException(ex.Message, ex);
			}
			return exception;
		}

		// Token: 0x0600BD02 RID: 48386 RVA: 0x00265C3C File Offset: 0x00263E3C
		public static ErrorException ToErrorException(this Exception exception)
		{
			ErrorException ex = exception as ErrorException;
			ErrorException ex2 = null;
			if (exception.InnerException != null)
			{
				ex2 = exception.InnerException.ToErrorException();
			}
			if (exception is InvalidOperationException)
			{
				InvalidOperationException ex3 = exception as InvalidOperationException;
				if (ex3.Data.Contains("ModuleName"))
				{
					bool flag = false;
					if (ex3.Data.Contains("IsRecoverable"))
					{
						flag = (ex3.Data["IsRecoverable"] as bool?).GetValueOrDefault();
					}
					ex = new ErrorException(exception.Message, exception.StackTrace, exception.Data["ModuleName"] as string, flag, false, ex2);
				}
			}
			else if (exception is ContainerExitException)
			{
				ContainerExitException ex4 = exception as ContainerExitException;
				ex = new ErrorException(exception.Message, exception.StackTrace, exception.GetType().FullName, ex4.ContainerLogText, false, false, ex2);
			}
			else if (exception is ExternalException)
			{
				ExternalException ex5 = exception as ExternalException;
				ex = new ErrorException(exception.Message, exception.StackTrace, exception.GetType().FullName, "0x" + ex5.ErrorCode.ToString("x", CultureInfo.InvariantCulture), false, false, ex2);
			}
			if (ex == null)
			{
				ex = new ErrorException(exception.Message, exception.StackTrace, exception.GetType().FullName, false, false, ex2);
			}
			return ex;
		}
	}
}
