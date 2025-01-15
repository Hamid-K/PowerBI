using System;
using System.Threading;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000096 RID: 150
	public interface IInterpretationService
	{
		// Token: 0x060002A1 RID: 673
		InterpretResult Interpret(InterpretRequest request, IDatabaseContext databaseContext, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060002A2 RID: 674
		GenerateUtteranceResult GenerateUtterance(GenerateUtteranceRequest request, IDatabaseContext databaseContext, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060002A3 RID: 675
		RelatedEntityInferenceResponse InferRelatedEntities(RelatedEntityInferenceRequest request, IDatabaseContext databaseContext, CancellationToken cancellationToken = default(CancellationToken));
	}
}
