using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C9 RID: 201
	[ImmutableObject(true)]
	public sealed class ServiceSpecification
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x000071B4 File Offset: 0x000053B4
		public ServiceSpecification(TextAnalysisServiceSpecification textAnalysisServiceSpecification, IDictionary<LanguageIdentifier, InterpreterParametersSpecification> interpreterParametersSpecifications, IDictionary<LanguageIdentifier, SynonymProviderSpecification> synonymProviderSpecifications, SynonymServiceSpecification synonymServiceSpecification)
		{
			Contract.CheckValue<TextAnalysisServiceSpecification>(textAnalysisServiceSpecification, "textAnalysisServiceSpecification");
			Contract.CheckValue<IDictionary<LanguageIdentifier, InterpreterParametersSpecification>>(interpreterParametersSpecifications, "interpreterParametersSpecifications");
			Contract.CheckValue<IDictionary<LanguageIdentifier, SynonymProviderSpecification>>(synonymProviderSpecifications, "synonymProviderSpecifications");
			Contract.CheckValue<SynonymServiceSpecification>(synonymServiceSpecification, "synonymServiceSpecification");
			this._textAnalysisServiceSpecification = textAnalysisServiceSpecification;
			this._interpreterParametersSpecifications = interpreterParametersSpecifications.AsReadOnlyDictionary<LanguageIdentifier, InterpreterParametersSpecification>();
			this._synonymProviderSpecifications = synonymProviderSpecifications.AsReadOnlyDictionary<LanguageIdentifier, SynonymProviderSpecification>();
			this._synonymServiceSpecification = synonymServiceSpecification;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000721B File Offset: 0x0000541B
		public TextAnalysisServiceSpecification TextAnalysisServiceSpecification
		{
			get
			{
				return this._textAnalysisServiceSpecification;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00007223 File Offset: 0x00005423
		public IReadOnlyDictionary<LanguageIdentifier, InterpreterParametersSpecification> InterpreterParametersSpecifications
		{
			get
			{
				return this._interpreterParametersSpecifications;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000722B File Offset: 0x0000542B
		public IReadOnlyDictionary<LanguageIdentifier, SynonymProviderSpecification> SynonymProviderSpecifications
		{
			get
			{
				return this._synonymProviderSpecifications;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00007233 File Offset: 0x00005433
		public SynonymServiceSpecification SynonymServiceSpecification
		{
			get
			{
				return this._synonymServiceSpecification;
			}
		}

		// Token: 0x04000429 RID: 1065
		private readonly ReadOnlyDictionary<LanguageIdentifier, InterpreterParametersSpecification> _interpreterParametersSpecifications;

		// Token: 0x0400042A RID: 1066
		private readonly ReadOnlyDictionary<LanguageIdentifier, SynonymProviderSpecification> _synonymProviderSpecifications;

		// Token: 0x0400042B RID: 1067
		private readonly TextAnalysisServiceSpecification _textAnalysisServiceSpecification;

		// Token: 0x0400042C RID: 1068
		private readonly SynonymServiceSpecification _synonymServiceSpecification;
	}
}
