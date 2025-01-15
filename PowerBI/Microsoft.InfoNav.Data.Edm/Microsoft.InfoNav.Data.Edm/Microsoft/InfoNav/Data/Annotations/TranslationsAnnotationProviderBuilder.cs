using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Annotations
{
	// Token: 0x0200002F RID: 47
	public sealed class TranslationsAnnotationProviderBuilder
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00009143 File Offset: 0x00007343
		public TranslationsAnnotationProviderBuilder()
		{
			this._translations = new Dictionary<IConceptualDisplayItem, TranslationsAnnotation>();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009156 File Offset: 0x00007356
		public void RegisterTranslations(IConceptualDisplayItem target, IReadOnlyList<ConceptualTranslation> translations)
		{
			this._translations.Add(target, new TranslationsAnnotation(translations));
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000916C File Offset: 0x0000736C
		public void RegisterTranslationsAnnotationProvider(IConceptualSchema schema)
		{
			TranslationsAnnotationProviderBuilder.TranslationsAnnotationProvider translationsAnnotationProvider = new TranslationsAnnotationProviderBuilder.TranslationsAnnotationProvider(this._translations);
			schema.RegisterAnnotationProvider<TranslationsAnnotation, IConceptualDisplayItem>(translationsAnnotationProvider);
		}

		// Token: 0x04000199 RID: 409
		private Dictionary<IConceptualDisplayItem, TranslationsAnnotation> _translations;

		// Token: 0x02000050 RID: 80
		private sealed class TranslationsAnnotationProvider : IAnnotationProvider<TranslationsAnnotation, IConceptualDisplayItem>
		{
			// Token: 0x06000234 RID: 564 RVA: 0x00009AC8 File Offset: 0x00007CC8
			public TranslationsAnnotationProvider(IReadOnlyDictionary<IConceptualDisplayItem, TranslationsAnnotation> translations)
			{
				this._translations = translations;
			}

			// Token: 0x06000235 RID: 565 RVA: 0x00009AD7 File Offset: 0x00007CD7
			public bool TryGetAnnotation(IConceptualDisplayItem target, out TranslationsAnnotation annotation)
			{
				return this._translations.TryGetValue(target, out annotation);
			}

			// Token: 0x040001F0 RID: 496
			private IReadOnlyDictionary<IConceptualDisplayItem, TranslationsAnnotation> _translations;
		}
	}
}
