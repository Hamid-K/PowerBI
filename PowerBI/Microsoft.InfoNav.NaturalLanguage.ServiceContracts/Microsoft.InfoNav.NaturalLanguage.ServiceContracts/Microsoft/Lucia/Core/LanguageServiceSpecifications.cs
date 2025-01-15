using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000099 RID: 153
	public sealed class LanguageServiceSpecifications
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x000061F0 File Offset: 0x000043F0
		public LanguageServiceSpecifications(WordBreakerSpecification wordBreaker, SpellCorrectorSpecification spellCorrector, StemmerSpecification stemmer, TextAnalysisLexiconSpecification textAnalysisLexicon, SynonymServiceStoreConfiguration synonymStoreConfiguration, SynonymProviderSpecification synonymProviderSpecification, OfficeThesaurusSpecification officeThesaurusSpecification)
		{
			this.WordBreaker = wordBreaker;
			this.SpellCorrector = spellCorrector;
			this.Stemmer = stemmer;
			this.TextAnalysisLexicon = textAnalysisLexicon;
			this.SynonymStoreConfiguration = synonymStoreConfiguration;
			this.SynonymProviderSpecification = synonymProviderSpecification;
			this.OfficeThesaurusSpecification = officeThesaurusSpecification;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000622D File Offset: 0x0000442D
		public WordBreakerSpecification WordBreaker { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00006235 File Offset: 0x00004435
		public SpellCorrectorSpecification SpellCorrector { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000623D File Offset: 0x0000443D
		public StemmerSpecification Stemmer { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00006245 File Offset: 0x00004445
		public TextAnalysisLexiconSpecification TextAnalysisLexicon { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000624D File Offset: 0x0000444D
		public SynonymServiceStoreConfiguration SynonymStoreConfiguration { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00006255 File Offset: 0x00004455
		public SynonymProviderSpecification SynonymProviderSpecification { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000625D File Offset: 0x0000445D
		public OfficeThesaurusSpecification OfficeThesaurusSpecification { get; }
	}
}
