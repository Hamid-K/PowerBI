using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002FE RID: 766
	public sealed class OrderByCollection : IEnumerable<KeyValuePair<PropertyDefinitionBase, SortDirection>>, IEnumerable, IJsonSerializable
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x00048AB2 File Offset: 0x00047AB2
		internal OrderByCollection()
		{
			this.propDefSortOrderPairList = new List<KeyValuePair<PropertyDefinitionBase, SortDirection>>();
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00048AC5 File Offset: 0x00047AC5
		public void Add(PropertyDefinitionBase propertyDefinition, SortDirection sortDirection)
		{
			if (this.Contains(propertyDefinition))
			{
				throw new ServiceLocalException(string.Format(Strings.PropertyAlreadyExistsInOrderByCollection, propertyDefinition.GetPrintableName()));
			}
			this.propDefSortOrderPairList.Add(new KeyValuePair<PropertyDefinitionBase, SortDirection>(propertyDefinition, sortDirection));
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00048AFD File Offset: 0x00047AFD
		public void Clear()
		{
			this.propDefSortOrderPairList.Clear();
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00048B28 File Offset: 0x00047B28
		internal bool Contains(PropertyDefinitionBase propertyDefinition)
		{
			return this.propDefSortOrderPairList.Exists((KeyValuePair<PropertyDefinitionBase, SortDirection> pair) => pair.Key.Equals(propertyDefinition));
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00048B59 File Offset: 0x00047B59
		public int Count
		{
			get
			{
				return this.propDefSortOrderPairList.Count;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00048B84 File Offset: 0x00047B84
		public bool Remove(PropertyDefinitionBase propertyDefinition)
		{
			int num = this.propDefSortOrderPairList.RemoveAll((KeyValuePair<PropertyDefinitionBase, SortDirection> pair) => pair.Key.Equals(propertyDefinition));
			return num > 0;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00048BBA File Offset: 0x00047BBA
		public void RemoveAt(int index)
		{
			this.propDefSortOrderPairList.RemoveAt(index);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00048BC8 File Offset: 0x00047BC8
		public bool TryGetValue(PropertyDefinitionBase propertyDefinition, out SortDirection sortDirection)
		{
			foreach (KeyValuePair<PropertyDefinitionBase, SortDirection> keyValuePair in this.propDefSortOrderPairList)
			{
				if (keyValuePair.Value.Equals(propertyDefinition))
				{
					sortDirection = keyValuePair.Value;
					return true;
				}
			}
			sortDirection = SortDirection.Ascending;
			return false;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00048C3C File Offset: 0x00047C3C
		internal void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			if (this.Count > 0)
			{
				writer.WriteStartElement(XmlNamespace.Messages, xmlElementName);
				foreach (KeyValuePair<PropertyDefinitionBase, SortDirection> keyValuePair in this)
				{
					writer.WriteStartElement(XmlNamespace.Types, "FieldOrder");
					writer.WriteAttributeValue("Order", keyValuePair.Value);
					keyValuePair.Key.WriteToXml(writer);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00048CCC File Offset: 0x00047CCC
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			if (this.Count > 0)
			{
				List<object> list = new List<object>();
				foreach (KeyValuePair<PropertyDefinitionBase, SortDirection> keyValuePair in this)
				{
					JsonObject jsonObject = new JsonObject();
					jsonObject.Add("Order", keyValuePair.Value);
					jsonObject.Add("Path", ((IJsonSerializable)keyValuePair.Key).ToJson(service));
					list.Add(jsonObject);
				}
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x1700067C RID: 1660
		public KeyValuePair<PropertyDefinitionBase, SortDirection> this[int index]
		{
			get
			{
				return this.propDefSortOrderPairList[index];
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00048D6E File Offset: 0x00047D6E
		public IEnumerator<KeyValuePair<PropertyDefinitionBase, SortDirection>> GetEnumerator()
		{
			return this.propDefSortOrderPairList.GetEnumerator();
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00048D80 File Offset: 0x00047D80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.propDefSortOrderPairList.GetEnumerator();
		}

		// Token: 0x04001442 RID: 5186
		private List<KeyValuePair<PropertyDefinitionBase, SortDirection>> propDefSortOrderPairList;
	}
}
