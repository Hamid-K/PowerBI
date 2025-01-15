using System;
using System.Threading;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200014D RID: 333
	internal sealed class DaxTranslator : CommandTreeTranslator
	{
		// Token: 0x06001280 RID: 4736 RVA: 0x000357B8 File Offset: 0x000339B8
		public override TranslationResult Translate(QueryCommandTree tree, CancellationToken cancellationToken, bool useConceptualSchema)
		{
			string text = (useConceptualSchema ? tree.ConceptualSchema.ConceptualCollation.Culture : tree.EntityDataModel.Culture);
			return new DaxTransform(tree.LanguageCapabilities, text, cancellationToken).Translate(tree);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000357FC File Offset: 0x000339FC
		public override BatchTranslationResult TranslateBatch(QueryCommandTree tree, CancellationToken cancellationToken, bool useConceptualSchema)
		{
			string text = (useConceptualSchema ? tree.ConceptualSchema.ConceptualCollation.Culture : tree.EntityDataModel.Culture);
			return new DaxTransform(tree.LanguageCapabilities, text, cancellationToken).TranslateBatch(tree);
		}
	}
}
