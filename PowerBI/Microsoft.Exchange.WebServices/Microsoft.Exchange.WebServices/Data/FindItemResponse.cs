using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200015D RID: 349
	internal sealed class FindItemResponse<TItem> : ServiceResponse where TItem : Item
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x000303BA File Offset: 0x0002F3BA
		internal FindItemResponse(bool isGrouped, PropertySet propertySet)
		{
			this.isGrouped = isGrouped;
			this.propertySet = propertySet;
			EwsUtilities.Assert(this.propertySet != null, "FindItemResponse.ctor", "PropertySet should not be null");
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000303EC File Offset: 0x0002F3EC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "RootFolder");
			int num = reader.ReadAttributeValue<int>("TotalItemsInView");
			bool flag = !reader.ReadAttributeValue<bool>("IncludesLastItemInRange");
			int? num2 = (flag ? reader.ReadNullableAttributeValue<int>("IndexedPagingOffset") : default(int?));
			if (!this.isGrouped)
			{
				this.results = new FindItemsResults<TItem>();
				this.results.TotalCount = num;
				this.results.NextPageOffset = num2;
				this.results.MoreAvailable = flag;
				FindItemResponse<TItem>.InternalReadItemsFromXml(reader, this.propertySet, this.results.Items);
			}
			else
			{
				this.groupedFindResults = new GroupedFindItemsResults<TItem>();
				this.groupedFindResults.TotalCount = num;
				this.groupedFindResults.NextPageOffset = num2;
				this.groupedFindResults.MoreAvailable = flag;
				reader.ReadStartElement(XmlNamespace.Types, "Groups");
				if (!reader.IsEmptyElement)
				{
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "GroupedItems"))
						{
							string text = reader.ReadElementValue(XmlNamespace.Types, "GroupIndex");
							List<TItem> list = new List<TItem>();
							FindItemResponse<TItem>.InternalReadItemsFromXml(reader, this.propertySet, list);
							reader.ReadEndElement(XmlNamespace.Types, "GroupedItems");
							this.groupedFindResults.ItemGroups.Add(new ItemGroup<TItem>(text, list));
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "Groups"));
				}
			}
			reader.ReadEndElement(XmlNamespace.Messages, "RootFolder");
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Messages, "HighlightTerms") && !reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1)
					{
						HighlightTerm highlightTerm = new HighlightTerm();
						highlightTerm.LoadFromXml(reader, XmlNamespace.Types, "Term");
						this.results.HighlightTerms.Add(highlightTerm);
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "HighlightTerms"));
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000305A8 File Offset: 0x0002F5A8
		private static void InternalReadItemsFromXml(EwsServiceXmlReader reader, PropertySet propertySet, IList<TItem> destinationList)
		{
			EwsUtilities.Assert(destinationList != null, "FindItemResponse.InternalReadItemsFromXml", "destinationList is null.");
			reader.ReadStartElement(XmlNamespace.Types, "Items");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1)
					{
						TItem titem = EwsUtilities.CreateEwsObjectFromXmlElementName<TItem>(reader.Service, reader.LocalName);
						if (titem == null)
						{
							reader.SkipCurrentElement();
						}
						else
						{
							titem.LoadFromXml(reader, true, propertySet, true);
							destinationList.Add(titem);
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, "Items"));
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00030638 File Offset: 0x0002F638
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			JsonObject jsonObject = responseObject.ReadAsJsonObject("RootFolder");
			int num = jsonObject.ReadAsInt("TotalItemsInView");
			bool flag = !jsonObject.ReadAsBool("IncludesLastItemInRange");
			int? num2 = default(int?);
			if (flag && jsonObject.ContainsKey("IndexedPagingOffset"))
			{
				num2 = new int?(jsonObject.ReadAsInt("IndexedPagingOffset"));
			}
			if (!this.isGrouped)
			{
				this.results = new FindItemsResults<TItem>();
				this.results.TotalCount = num;
				this.results.NextPageOffset = num2;
				this.results.MoreAvailable = flag;
				this.InternalReadItemsFromJson(jsonObject, this.propertySet, service, this.results.Items);
			}
			else
			{
				this.groupedFindResults = new GroupedFindItemsResults<TItem>();
				this.groupedFindResults.TotalCount = num;
				this.groupedFindResults.NextPageOffset = num2;
				this.groupedFindResults.MoreAvailable = flag;
				if (jsonObject.ContainsKey("Groups"))
				{
					object[] array = jsonObject.ReadAsArray("Groups");
					foreach (JsonObject jsonObject2 in Enumerable.OfType<JsonObject>(array))
					{
						if (jsonObject2.ContainsKey("GroupedItems"))
						{
							JsonObject jsonObject3 = jsonObject2.ReadAsJsonObject("GroupedItems");
							string text = jsonObject3.ReadAsString("GroupIndex");
							List<TItem> list = new List<TItem>();
							this.InternalReadItemsFromJson(jsonObject3, this.propertySet, service, list);
							this.groupedFindResults.ItemGroups.Add(new ItemGroup<TItem>(text, list));
						}
					}
				}
			}
			object[] array2 = responseObject.ReadAsArray("HighlightTerms");
			if (array2 != null)
			{
				foreach (object obj in array2)
				{
					JsonObject jsonObject4 = obj as JsonObject;
					HighlightTerm highlightTerm = new HighlightTerm();
					highlightTerm.LoadFromJson(jsonObject4, service);
					this.results.HighlightTerms.Add(highlightTerm);
				}
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00030850 File Offset: 0x0002F850
		private void InternalReadItemsFromJson(JsonObject jsonObject, PropertySet propertySet, ExchangeService service, IList<TItem> destinationList)
		{
			EwsUtilities.Assert(destinationList != null, "FindItemResponse.InternalReadItemsFromJson", "destinationList is null.");
			if (jsonObject.ContainsKey("Items"))
			{
				List<TItem> list = new EwsServiceJsonReader(service).ReadServiceObjectsCollectionFromJson<TItem>(jsonObject, "Items", new GetObjectInstanceDelegate<TItem>(this.CreateItemInstance), true, this.propertySet, true);
				list.ForEach(delegate(TItem item)
				{
					destinationList.Add(item);
				});
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000308D2 File Offset: 0x0002F8D2
		private TItem CreateItemInstance(ExchangeService service, string xmlElementName)
		{
			return EwsUtilities.CreateEwsObjectFromXmlElementName<TItem>(service, xmlElementName);
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x000308DB File Offset: 0x0002F8DB
		public GroupedFindItemsResults<TItem> GroupedFindResults
		{
			get
			{
				return this.groupedFindResults;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x000308E3 File Offset: 0x0002F8E3
		public FindItemsResults<TItem> Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x040009A4 RID: 2468
		private FindItemsResults<TItem> results;

		// Token: 0x040009A5 RID: 2469
		private bool isGrouped;

		// Token: 0x040009A6 RID: 2470
		private GroupedFindItemsResults<TItem> groupedFindResults;

		// Token: 0x040009A7 RID: 2471
		private PropertySet propertySet;
	}
}
