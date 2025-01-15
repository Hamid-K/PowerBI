using System;
using System.Globalization;
using System.Threading;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CCC RID: 7372
	public static class GlobalizedEvaluatorThreadPool
	{
		// Token: 0x0600B7B7 RID: 47031 RVA: 0x002545A3 File Offset: 0x002527A3
		public static void Start(ThreadStart threadStart)
		{
			GlobalizedEvaluatorThreadPool.Start(delegate(object state)
			{
				threadStart();
			}, null);
		}

		// Token: 0x0600B7B8 RID: 47032 RVA: 0x002545C2 File Offset: 0x002527C2
		public static void Start(ParameterizedThreadStart threadStart, object state)
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			CultureInfo currentUiCulture = CultureInfo.CurrentUICulture;
			EvaluatorThreadPool.Start(delegate(object s)
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
				Thread.CurrentThread.CurrentUICulture = currentUiCulture;
				threadStart(s);
			}, state);
		}
	}
}
