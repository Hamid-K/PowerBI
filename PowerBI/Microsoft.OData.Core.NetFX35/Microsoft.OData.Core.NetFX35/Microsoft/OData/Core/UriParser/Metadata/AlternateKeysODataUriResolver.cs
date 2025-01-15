using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Metadata
{
	// Token: 0x020001EA RID: 490
	public sealed class AlternateKeysODataUriResolver : ODataUriResolver
	{
		// Token: 0x060011DE RID: 4574 RVA: 0x0004088A File Offset: 0x0003EA8A
		public AlternateKeysODataUriResolver(IEdmModel model)
		{
			this.model = model;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004089C File Offset: 0x0003EA9C
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

		// Token: 0x060011E0 RID: 4576 RVA: 0x000408D8 File Offset: 0x0003EAD8
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

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004096C File Offset: 0x0003EB6C
		private bool TryResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, IDictionary<string, IEdmProperty> keyProperties, Func<IEdmTypeReference, string, object> convertFunc, out IEnumerable<KeyValuePair<string, object>> convertedPairs)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			using (IEnumerator<KeyValuePair<string, IEdmProperty>> enumerator = keyProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, IEdmProperty> kvp = enumerator.Current;
					string text;
					if (this.EnableCaseInsensitive)
					{
						List<string> list = Enumerable.ToList<string>(Enumerable.Where<string>(namedValues.Keys, delegate(string key)
						{
							KeyValuePair<string, IEdmProperty> kvp5 = kvp;
							return string.Equals(kvp5.Key, key, 5);
						}));
						if (list.Count > 1)
						{
							KeyValuePair<string, IEdmProperty> kvp6 = kvp;
							throw new ODataException(Strings.UriParserMetadata_MultipleMatchingKeysFound(kvp6.Key));
						}
						if (list.Count == 0)
						{
							convertedPairs = null;
							return false;
						}
						text = namedValues[Enumerable.Single<string>(list)];
					}
					else
					{
						KeyValuePair<string, IEdmProperty> kvp2 = kvp;
						if (!namedValues.TryGetValue(kvp2.Key, ref text))
						{
							convertedPairs = null;
							return false;
						}
					}
					KeyValuePair<string, IEdmProperty> kvp3 = kvp;
					object obj = convertFunc.Invoke(kvp3.Value.Type, text);
					if (obj == null)
					{
						convertedPairs = null;
						return false;
					}
					Dictionary<string, object> dictionary2 = dictionary;
					KeyValuePair<string, IEdmProperty> kvp4 = kvp;
					dictionary2[kvp4.Key] = obj;
				}
			}
			convertedPairs = dictionary;
			return true;
		}

		// Token: 0x040007B5 RID: 1973
		private readonly IEdmModel model;
	}
}
