using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.PlatformHost;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000018 RID: 24
	public class ManagedErrorHandler
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000034AE File Offset: 0x000016AE
		protected virtual EngineErrorInfo HandleExceptions(Action func, [CallerMemberName] string callerMethodName = null, bool requiresHandlingAsPrivate = false, Func<Exception, EngineException> mToEngineExceptionTranslator = null)
		{
			return this.HandleExceptions(func, null, callerMethodName, requiresHandlingAsPrivate, mToEngineExceptionTranslator);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000034BC File Offset: 0x000016BC
		protected virtual EngineErrorInfo HandleExceptions(Action func, string MProgram, [CallerMemberName] string callerMethodName = null, bool requiresHandlingAsPrivate = false, Func<Exception, EngineException> mToEngineExceptionTranslator = null)
		{
			EngineException ex = null;
			try
			{
				func();
			}
			catch (EngineException ex2)
			{
				if (requiresHandlingAsPrivate)
				{
					this.engineTracer.LogPrivateMessage(string.Format("M engine error occurred in {0}: {1}", callerMethodName, ex2.ToString(requiresHandlingAsPrivate)));
				}
				else
				{
					this.engineTracer.LogMessage(string.Format("M engine error occurred in {0}: {1}", callerMethodName, ex2.ToString(requiresHandlingAsPrivate)));
				}
				this.TraceMProgram(MProgram, "MProgram");
				ex = ex2;
			}
			catch (Exception ex3)
			{
				if (requiresHandlingAsPrivate)
				{
					this.engineTracer.LogPrivateMessage(string.Format("Unexpected M engine error occurred {0}: {1}", callerMethodName, ex3.ToString(requiresHandlingAsPrivate)));
				}
				else
				{
					this.engineTracer.LogMessage(string.Format("Unexpected M engine error occurred {0}: {1}", callerMethodName, ex3.ToString(requiresHandlingAsPrivate)));
				}
				this.TraceMProgram(MProgram, "MProgram");
				if (mToEngineExceptionTranslator != null)
				{
					ex = mToEngineExceptionTranslator(ex3);
				}
				if (ex == null)
				{
					ex = EngineException.PFE_M_ENGINE_INTERNAL(string.Format("{0}; {1}", ex3.Source, ex3.GetMessage(requiresHandlingAsPrivate)));
				}
			}
			if (ex != null)
			{
				return new EngineErrorInfo(ex);
			}
			return default(EngineErrorInfo);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000035D8 File Offset: 0x000017D8
		protected void TraceMProgram(string MProgram, string prefix = "MProgram")
		{
			MProgram = MProgram.RedactSensitiveStrings();
			if (!string.IsNullOrEmpty(MProgram))
			{
				int num = 4000 - prefix.Length;
				int length = MProgram.Length;
				int i = 0;
				int num2 = 0;
				while (i < length)
				{
					this.engineTracer.LogPrivateMessage(string.Format("{0}[{1}]: {2}.", prefix, num2, MProgram.Substring(i, Math.Min(num, length - i)).MarkAsCustomerContent()));
					i += num;
					num2++;
				}
			}
		}

		// Token: 0x040000A4 RID: 164
		protected IEngineTracer engineTracer;
	}
}
