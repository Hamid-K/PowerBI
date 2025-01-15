using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000145 RID: 325
	public sealed class AlternateKeysODataUriResolver : ODataUriResolver
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x0002EE89 File Offset: 0x0002D089
		public AlternateKeysODataUriResolver(IEdmModel model)
		{
			this.model = model;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0002EE98 File Offset: 0x0002D098
		public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			IEnumerable<KeyValuePair<string, object>> enumerable;
			try
			{
				enumerable = base.ResolveKeys(type, namedValues, convertFunc);
			}
			catch (ODataException)
			{
				if (!this.TryResolveAlternateKeys(type, namedValues, convertFunc, out enumerable))
				{
					throw;
				}
			}
			return enumerable;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0002EED4 File Offset: 0x0002D0D4
		private bool TryResolveAlternateKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc, out IEnumerable<KeyValuePair<string, object>> convertedPairs)
		{
			IEnumerable<IDictionary<string, IEdmProperty>> alternateKeysAnnotation = this.model.GetAlternateKeysAnnotation(type);
			foreach (IDictionary<string, IEdmProperty> dictionary in alternateKeysAnnotation)
			{
				if (this.TryResolveKeys(type, namedValues, dictionary, convertFunc, out convertedPairs))
				{
					return true;
				}
			}
			convertedPairs = null;
			return false;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0002EF3C File Offset: 0x0002D13C
		private bool TryResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, IDictionary<string, IEdmProperty> keyProperties, Func<IEdmTypeReference, string, object> convertFunc, out IEnumerable<KeyValuePair<string, object>> convertedPairs)
		{
			if (namedValues.Count != keyProperties.Count)
			{
				convertedPairs = null;
				return false;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			using (IEnumerator<KeyValuePair<string, IEdmProperty>> enumerator = keyProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, IEdmProperty> kvp = enumerator.Current;
					string text;
					if (!namedValues.TryGetValue(kvp.Key, out text) && !this.EnableCaseInsensitive)
					{
						convertedPairs = null;
						return false;
					}
					if (text == null)
					{
						List<string> list = namedValues.Keys.Where((string key) => string.Equals(kvp.Key, key, StringComparison.OrdinalIgnoreCase)).ToList<string>();
						if (list.Count > 1)
						{
							throw new ODataException(Strings.UriParserMetadata_MultipleMatchingKeysFound(kvp.Key));
						}
						if (list.Count == 0)
						{
							convertedPairs = null;
							return false;
						}
						text = namedValues[list.Single<string>()];
					}
					object obj = convertFunc(kvp.Value.Type, text);
					if (obj == null)
					{
						convertedPairs = null;
						return false;
					}
					dictionary[kvp.Key] = obj;
				}
			}
			convertedPairs = dictionary;
			return true;
		}

		// Token: 0x040007D5 RID: 2005
		private readonly IEdmModel model;
	}
}
