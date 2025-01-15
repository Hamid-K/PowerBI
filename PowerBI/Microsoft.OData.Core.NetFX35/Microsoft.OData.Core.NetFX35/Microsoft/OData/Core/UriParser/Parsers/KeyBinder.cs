using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001CC RID: 460
	internal sealed class KeyBinder
	{
		// Token: 0x06001129 RID: 4393 RVA: 0x0003C80E File Offset: 0x0003AA0E
		internal KeyBinder(MetadataBinder.QueryTokenVisitor keyValueBindMethod)
		{
			this.keyValueBindMethod = keyValueBindMethod;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0003C820 File Offset: 0x0003AA20
		internal QueryNode BindKeyValues(EntityCollectionNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model)
		{
			IEdmEntityTypeReference entityItemType = collectionNode.EntityItemType;
			IEdmEntityType edmEntityType = entityItemType.EntityDefinition();
			QueryNode queryNode;
			if (this.TryBindToDeclaredKey(collectionNode, namedValues, model, edmEntityType, out queryNode))
			{
				return queryNode;
			}
			if (this.TryBindToDeclaredAlternateKey(collectionNode, namedValues, model, edmEntityType, out queryNode))
			{
				return queryNode;
			}
			throw new ODataException(Strings.MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(collectionNode.ItemType.FullName()));
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0003C870 File Offset: 0x0003AA70
		private bool TryBindToDeclaredAlternateKey(EntityCollectionNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model, IEdmEntityType collectionItemEntityType, out QueryNode keyLookupNode)
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

		// Token: 0x0600112C RID: 4396 RVA: 0x0003C8D8 File Offset: 0x0003AAD8
		private bool TryBindToDeclaredKey(EntityCollectionNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model, IEdmEntityType collectionItemEntityType, out QueryNode keyLookupNode)
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>(StringComparer.Ordinal);
			foreach (IEdmStructuralProperty edmStructuralProperty in collectionItemEntityType.Key())
			{
				dictionary[edmStructuralProperty.Name] = edmStructuralProperty;
			}
			return this.TryBindToKeys(collectionNode, namedValues, model, collectionItemEntityType, dictionary, out keyLookupNode);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003C948 File Offset: 0x0003AB48
		private bool TryBindToKeys(EntityCollectionNode collectionNode, IEnumerable<NamedValue> namedValues, IEdmModel model, IEdmEntityType collectionItemEntityType, IDictionary<string, IEdmProperty> keys, out QueryNode keyLookupNode)
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

		// Token: 0x0600112E RID: 4398 RVA: 0x0003CA4C File Offset: 0x0003AC4C
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
					goto IL_00BD;
				}
			}
			edmProperty = Enumerable.SingleOrDefault<KeyValuePair<string, IEdmProperty>>(keys, (KeyValuePair<string, IEdmProperty> k) => string.CompareOrdinal(k.Key, namedValue.Name) == 0).Value;
			if (edmProperty == null)
			{
				keyPropertyValue = null;
				return false;
			}
			IL_00BD:
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

		// Token: 0x04000786 RID: 1926
		private readonly MetadataBinder.QueryTokenVisitor keyValueBindMethod;
	}
}
