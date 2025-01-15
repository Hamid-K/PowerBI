using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000F0 RID: 240
	[ImmutableObject(true)]
	public sealed class SynonymServiceSpecification
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x000088A4 File Offset: 0x00006AA4
		public SynonymServiceSpecification(IEnumerable<SynonymServiceStoreConfiguration> configurations, IEnumerable<OfficeThesaurusSpecification> specifications)
		{
			Dictionary<LanguageIdentifier, SynonymServiceStoreConfiguration> dictionary = new Dictionary<LanguageIdentifier, SynonymServiceStoreConfiguration>();
			foreach (SynonymServiceStoreConfiguration synonymServiceStoreConfiguration in configurations)
			{
				Contract.Check(!dictionary.ContainsKey(synonymServiceStoreConfiguration.Language), "At least one of the specified service configurations have non-unique language identifiers");
				dictionary.Add(synonymServiceStoreConfiguration.Language, synonymServiceStoreConfiguration);
			}
			this._storeConfigurations = dictionary.AsReadOnlyDictionary<LanguageIdentifier, SynonymServiceStoreConfiguration>();
			Dictionary<LanguageIdentifier, OfficeThesaurusSpecification> dictionary2 = new Dictionary<LanguageIdentifier, OfficeThesaurusSpecification>();
			foreach (OfficeThesaurusSpecification officeThesaurusSpecification in specifications)
			{
				Contract.Check(!dictionary2.ContainsKey(officeThesaurusSpecification.Language), "At least one of the specified service configurations have non-unique language identifiers");
				dictionary2.Add(officeThesaurusSpecification.Language, officeThesaurusSpecification);
			}
			this._thesaurusSpecifications = dictionary2.AsReadOnlyDictionary<LanguageIdentifier, OfficeThesaurusSpecification>();
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00008994 File Offset: 0x00006B94
		public IDictionary<LanguageIdentifier, SynonymServiceStoreConfiguration> StoreConfigurations
		{
			get
			{
				return this._storeConfigurations;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000899C File Offset: 0x00006B9C
		public IDictionary<LanguageIdentifier, OfficeThesaurusSpecification> ThesaurusSpecifications
		{
			get
			{
				return this._thesaurusSpecifications;
			}
		}

		// Token: 0x0400052B RID: 1323
		private readonly ReadOnlyDictionary<LanguageIdentifier, SynonymServiceStoreConfiguration> _storeConfigurations;

		// Token: 0x0400052C RID: 1324
		private readonly ReadOnlyDictionary<LanguageIdentifier, OfficeThesaurusSpecification> _thesaurusSpecifications;
	}
}
