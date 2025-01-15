using System;
using System.Threading;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000121 RID: 289
	internal abstract class CommandTreeTranslator
	{
		// Token: 0x06001036 RID: 4150
		public abstract TranslationResult Translate(QueryCommandTree tree, CancellationToken cancellationToken, bool useConceptualSchema);

		// Token: 0x06001037 RID: 4151
		public abstract BatchTranslationResult TranslateBatch(QueryCommandTree tree, CancellationToken cancellationToken, bool useConceptualSchema);
	}
}
