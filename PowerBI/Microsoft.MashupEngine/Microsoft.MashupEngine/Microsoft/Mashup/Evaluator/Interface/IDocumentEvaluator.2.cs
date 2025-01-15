using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DEC RID: 7660
	public interface IDocumentEvaluator : IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>
	{
	}
}
