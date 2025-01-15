using System;
using System.Threading;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DED RID: 7661
	public static class IDocumentEvaluatorExtensions
	{
		// Token: 0x0600BD9E RID: 48542 RVA: 0x00266DF0 File Offset: 0x00264FF0
		public static EvaluationResult2<T> GetResult<T>(this IDocumentEvaluator<T> evaluator, DocumentEvaluationParameters parameters)
		{
			EvaluationResult2<T> result = default(EvaluationResult2<T>);
			using (ManualResetEvent complete = new ManualResetEvent(false))
			{
				evaluator.BeginGetResult(parameters, delegate(EvaluationResult2<T> r)
				{
					result = r;
					complete.Set();
				});
				complete.WaitOne();
			}
			return result;
		}
	}
}
