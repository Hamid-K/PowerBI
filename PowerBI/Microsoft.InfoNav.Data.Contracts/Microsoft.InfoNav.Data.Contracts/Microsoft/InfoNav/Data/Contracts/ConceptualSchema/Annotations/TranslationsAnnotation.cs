using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x0200013A RID: 314
	public sealed class TranslationsAnnotation
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x00010C92 File Offset: 0x0000EE92
		public TranslationsAnnotation(IReadOnlyList<ConceptualTranslation> translations)
		{
			this.Translations = translations;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00010CA1 File Offset: 0x0000EEA1
		public IReadOnlyList<ConceptualTranslation> Translations { get; }
	}
}
