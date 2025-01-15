using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000119 RID: 281
	[ImmutableObject(true)]
	public class TextAnalysisServiceSpecification
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x0000A904 File Offset: 0x00008B04
		public TextAnalysisServiceSpecification(IDictionary<LanguageIdentifier, StemmerSpecification> stemmerSpecifications, IDictionary<LanguageIdentifier, WordBreakerSpecification> wordBreakerSpecifications, IDictionary<LanguageIdentifier, SpellCorrectorSpecification> spellCorrectorSpecifications, IDictionary<LanguageIdentifier, TextAnalysisLexiconSpecification> textAnalysisToolsLexiconSpecifications)
		{
			Contract.CheckValue<IDictionary<LanguageIdentifier, StemmerSpecification>>(stemmerSpecifications, "stemmerSpecifications");
			Contract.CheckValue<IDictionary<LanguageIdentifier, WordBreakerSpecification>>(wordBreakerSpecifications, "wordBreakerSpecifications");
			Contract.CheckValue<IDictionary<LanguageIdentifier, SpellCorrectorSpecification>>(spellCorrectorSpecifications, "spellCorrectorSpecification");
			Contract.CheckValue<IDictionary<LanguageIdentifier, TextAnalysisLexiconSpecification>>(textAnalysisToolsLexiconSpecifications, "textAnalysisToolsLexiconSpecifications");
			this._stemmerSpecifications = stemmerSpecifications.AsReadOnlyDictionary<LanguageIdentifier, StemmerSpecification>();
			this._wordBreakerSpecifications = wordBreakerSpecifications.AsReadOnlyDictionary<LanguageIdentifier, WordBreakerSpecification>();
			this._spellCorrectorSpecifications = spellCorrectorSpecifications.AsReadOnlyDictionary<LanguageIdentifier, SpellCorrectorSpecification>();
			this._textAnalysisLexiconSpecifications = textAnalysisToolsLexiconSpecifications.AsReadOnlyDictionary<LanguageIdentifier, TextAnalysisLexiconSpecification>();
			Contract.Check(this._textAnalysisLexiconSpecifications.ContainsKey(LanguageIdentifier.en_US), "the specified text analysis lexicon specifications doesn't have specification for en-US");
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000A98F File Offset: 0x00008B8F
		public IDictionary<LanguageIdentifier, StemmerSpecification> StemmerSpecifications
		{
			get
			{
				return this._stemmerSpecifications;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000A997 File Offset: 0x00008B97
		public IDictionary<LanguageIdentifier, WordBreakerSpecification> WordBreakerSpecifications
		{
			get
			{
				return this._wordBreakerSpecifications;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000A99F File Offset: 0x00008B9F
		public IDictionary<LanguageIdentifier, SpellCorrectorSpecification> SpellCorrectorSpecifications
		{
			get
			{
				return this._spellCorrectorSpecifications;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000A9A7 File Offset: 0x00008BA7
		public IDictionary<LanguageIdentifier, TextAnalysisLexiconSpecification> TextAnalysisLexiconSpecifications
		{
			get
			{
				return this._textAnalysisLexiconSpecifications;
			}
		}

		// Token: 0x040005D0 RID: 1488
		private readonly ReadOnlyDictionary<LanguageIdentifier, StemmerSpecification> _stemmerSpecifications;

		// Token: 0x040005D1 RID: 1489
		private readonly ReadOnlyDictionary<LanguageIdentifier, WordBreakerSpecification> _wordBreakerSpecifications;

		// Token: 0x040005D2 RID: 1490
		private readonly ReadOnlyDictionary<LanguageIdentifier, SpellCorrectorSpecification> _spellCorrectorSpecifications;

		// Token: 0x040005D3 RID: 1491
		private readonly ReadOnlyDictionary<LanguageIdentifier, TextAnalysisLexiconSpecification> _textAnalysisLexiconSpecifications;
	}
}
