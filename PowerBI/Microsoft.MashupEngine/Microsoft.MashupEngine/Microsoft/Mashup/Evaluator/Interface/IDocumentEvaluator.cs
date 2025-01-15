using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DEB RID: 7659
	public interface IDocumentEvaluator<T>
	{
		// Token: 0x0600BD9D RID: 48541
		IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback);
	}
}
