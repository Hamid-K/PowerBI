using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E1 RID: 225
	[ImmutableObject(true)]
	public sealed class SynonymProviderSpecification
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x000084EC File Offset: 0x000066EC
		public SynonymProviderSpecification(IDictionary<SynonymProvider, SynonymProviderConfiguration> synonymProviders)
		{
			this._synonymProviderConfigurations = synonymProviders.AsReadOnlyDictionary<SynonymProvider, SynonymProviderConfiguration>();
			Dictionary<int, List<SynonymProvider>> dictionary = new Dictionary<int, List<SynonymProvider>>();
			Dictionary<int, List<SynonymProvider>> dictionary2 = new Dictionary<int, List<SynonymProvider>>();
			foreach (KeyValuePair<SynonymProvider, SynonymProviderConfiguration> keyValuePair in this._synonymProviderConfigurations)
			{
				if (SynonymProviderSpecification.IsInstanceSynonymProvider(keyValuePair.Key))
				{
					using (IEnumerator<int> enumerator2 = keyValuePair.Value.SupportedNgramSize.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int num = enumerator2.Current;
							dictionary2.Add(num, keyValuePair.Key, this._synonymProviderConfigurations.Count);
						}
						continue;
					}
				}
				foreach (int num2 in keyValuePair.Value.SupportedNgramSize)
				{
					dictionary.Add(num2, keyValuePair.Key, this._synonymProviderConfigurations.Count);
				}
			}
			this._instanceSynonymProviderMap = dictionary2.AsReadOnlyDictionary<int, List<SynonymProvider>>();
			this._entitySynonymProviderMap = dictionary.AsReadOnlyDictionary<int, List<SynonymProvider>>();
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00008634 File Offset: 0x00006834
		internal ReadOnlyDictionary<SynonymProvider, SynonymProviderConfiguration> SynonymProviderConfigurations
		{
			get
			{
				return this._synonymProviderConfigurations;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000863C File Offset: 0x0000683C
		internal ReadOnlyDictionary<int, List<SynonymProvider>> InstanceSynonymProviderMap
		{
			get
			{
				return this._instanceSynonymProviderMap;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00008644 File Offset: 0x00006844
		internal ReadOnlyDictionary<int, List<SynonymProvider>> EntitySynonymProviderMap
		{
			get
			{
				return this._entitySynonymProviderMap;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000864C File Offset: 0x0000684C
		private static bool IsInstanceSynonymProvider(SynonymProvider provider)
		{
			return provider == SynonymProvider.CountryRegion || provider == SynonymProvider.Nationality || provider == SynonymProvider.InstanceValueSynonyms || provider == SynonymProvider.CityStateZipCode;
		}

		// Token: 0x040004F8 RID: 1272
		public static readonly SynonymProviderSpecification EmptySynonymProviderSpecification = new SynonymProviderSpecification(Util.EmptyReadOnlyDictionary<SynonymProvider, SynonymProviderConfiguration>());

		// Token: 0x040004F9 RID: 1273
		private readonly ReadOnlyDictionary<SynonymProvider, SynonymProviderConfiguration> _synonymProviderConfigurations;

		// Token: 0x040004FA RID: 1274
		private readonly ReadOnlyDictionary<int, List<SynonymProvider>> _instanceSynonymProviderMap;

		// Token: 0x040004FB RID: 1275
		private readonly ReadOnlyDictionary<int, List<SynonymProvider>> _entitySynonymProviderMap;
	}
}
