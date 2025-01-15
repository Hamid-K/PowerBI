using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000EA RID: 234
	internal sealed class KeyBinder
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		internal KeyBinder(MetadataBinder.QueryTokenVisitor keyValueBindMethod)
		{
			this.keyValueBindMethod = keyValueBindMethod;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		internal QueryNode BindKeyValues(CollectionResourceNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model)
		{
			IEdmEntityTypeReference edmEntityTypeReference = collectionNode.ItemStructuredType as IEdmEntityTypeReference;
			IEdmEntityType edmEntityType = edmEntityTypeReference.EntityDefinition();
			QueryNode queryNode;
			if (this.TryBindToDeclaredKey(collectionNode, namedValues, model, edmEntityType, out queryNode))
			{
				return queryNode;
			}
			if (this.TryBindToDeclaredAlternateKey(collectionNode, namedValues, model, edmEntityType, out queryNode))
			{
				return queryNode;
			}
			throw new ODataException(Strings.MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(collectionNode.ItemStructuredType.FullName()));
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001D92C File Offset: 0x0001BB2C
		private bool TryBindToDeclaredAlternateKey(CollectionResourceNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model, IEdmEntityType collectionItemEntityType, out QueryNode keyLookupNode)
		{
			IEnumerable<IDictionary<string, IEdmProperty>> alternateKeysAnnotation = model.GetAlternateKeysAnnotation(collectionItemEntityType);
			foreach (IDictionary<string, IEdmProperty> dictionary in alternateKeysAnnotation)
			{
				if (this.TryBindToKeys(collectionNode, namedValues, model, collectionItemEntityType, dictionary, out keyLookupNode))
				{
					return true;
				}
			}
			keyLookupNode = null;
			return false;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001D994 File Offset: 0x0001BB94
		private bool TryBindToDeclaredKey(CollectionResourceNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model, IEdmEntityType collectionItemEntityType, out QueryNode keyLookupNode)
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>(StringComparer.Ordinal);
			foreach (IEdmStructuralProperty edmStructuralProperty in collectionItemEntityType.Key())
			{
				dictionary[edmStructuralProperty.Name] = edmStructuralProperty;
			}
			return this.TryBindToKeys(collectionNode, namedValues, model, collectionItemEntityType, dictionary, out keyLookupNode);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001DA04 File Offset: 0x0001BC04
		private bool TryBindToKeys(CollectionResourceNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model, IEdmEntityType collectionItemEntityType, IDictionary<string, IEdmProperty> keys, out QueryNode keyLookupNode)
		{
			List<KeyPropertyValue> list = new List<KeyPropertyValue>();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (NamedValue namedValue in namedValues)
			{
				KeyPropertyValue keyPropertyValue;
				if (!this.TryBindKeyPropertyValue(namedValue, collectionItemEntityType, keys, out keyPropertyValue))
				{
					keyLookupNode = null;
					return false;
				}
				if (!hashSet.Add(keyPropertyValue.KeyProperty.Name))
				{
					throw new ODataException(Strings.MetadataBinder_DuplicitKeyPropertyInKeyValues(keyPropertyValue.KeyProperty.Name));
				}
				list.Add(keyPropertyValue);
			}
			if (list.Count == 0)
			{
				keyLookupNode = collectionNode;
				return true;
			}
			if (list.Count != Enumerable.Count<IEdmStructuralProperty>(collectionItemEntityType.Key()))
			{
				keyLookupNode = null;
				return false;
			}
			keyLookupNode = new KeyLookupNode(collectionNode, new ReadOnlyCollection<KeyPropertyValue>(list));
			return true;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		private bool TryBindKeyPropertyValue(NamedValue namedValue, IEdmEntityType collectionItemEntityType, IDictionary<string, IEdmProperty> keys, out KeyPropertyValue keyPropertyValue)
		{
			ExceptionUtils.CheckArgumentNotNull<NamedValue>(namedValue, "namedValue");
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(namedValue.Value, "namedValue.Value");
			IEdmProperty edmProperty = null;
			if (namedValue.Name == null)
			{
				using (IEnumerator<IEdmProperty> enumerator = keys.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmProperty edmProperty2 = enumerator.Current;
						if (edmProperty != null)
						{
							throw new ODataException(Strings.MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties(collectionItemEntityType.FullTypeName()));
						}
						edmProperty = edmProperty2;
					}
					goto IL_00B0;
				}
			}
			edmProperty = Enumerable.SingleOrDefault<KeyValuePair<string, IEdmProperty>>(keys, (KeyValuePair<string, IEdmProperty> k) => string.CompareOrdinal(k.Key, namedValue.Name) == 0).Value;
			if (edmProperty == null)
			{
				keyPropertyValue = null;
				return false;
			}
			IL_00B0:
			IEdmTypeReference type = edmProperty.Type;
			SingleValueNode singleValueNode = (SingleValueNode)this.keyValueBindMethod(namedValue.Value);
			singleValueNode = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, type);
			keyPropertyValue = new KeyPropertyValue
			{
				KeyProperty = edmProperty,
				KeyValue = singleValueNode
			};
			return true;
		}

		// Token: 0x04000693 RID: 1683
		private readonly MetadataBinder.QueryTokenVisitor keyValueBindMethod;
	}
}
