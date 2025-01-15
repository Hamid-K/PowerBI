using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200048A RID: 1162
	public static class ExceptionUtils
	{
		// Token: 0x06001A38 RID: 6712 RVA: 0x0004F38C File Offset: 0x0004D58C
		public static TReturn DefaultIfException<TReturn, TException>(this Func<TReturn> computation) where TException : Exception
		{
			TReturn treturn;
			try
			{
				treturn = computation();
			}
			catch (TException)
			{
				treturn = default(TReturn);
			}
			return treturn;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0004F3C0 File Offset: 0x0004D5C0
		public static void OnException<TException>(this Action computation, Action exceptionHandler) where TException : Exception
		{
			try
			{
				computation();
			}
			catch (TException)
			{
				exceptionHandler();
			}
		}
	}
}
