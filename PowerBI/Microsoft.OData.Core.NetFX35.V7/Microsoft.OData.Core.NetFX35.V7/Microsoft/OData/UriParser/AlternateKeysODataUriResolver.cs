using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A7 RID: 423
	public sealed class AlternateKeysODataUriResolver : ODataUriResolver
	{
		// Token: 0x06001103 RID: 4355 RVA: 0x0002F958 File Offset: 0x0002DB58
		public AlternateKeysODataUriResolver(IEdmModel model)
		{
			this.model = model;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0002F968 File Offset: 0x0002DB68
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

		// Token: 0x06001105 RID: 4357 RVA: 0x0002F9A4 File Offset: 0x0002DBA4
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

		// Token: 0x06001106 RID: 4358 RVA: 0x0002FA0C File Offset: 0x0002DC0C
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
						List<string> list = Enumerable.ToList<string>(Enumerable.Where<string>(namedValues.Keys, (string key) => string.Equals(kvp.Key, key, 5)));
						if (list.Count > 1)
						{
							throw new ODataException(Strings.UriParserMetadata_MultipleMatchingKeysFound(kvp.Key));
						}
						if (list.Count == 0)
						{
							convertedPairs = null;
							return false;
						}
						text = namedValues[Enumerable.Single<string>(list)];
					}
					else if (!namedValues.TryGetValue(kvp.Key, ref text))
					{
						convertedPairs = null;
						return false;
					}
					object obj = convertFunc.Invoke(kvp.Value.Type, text);
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

		// Token: 0x040008BE RID: 2238
		private readonly IEdmModel model;
	}
}
