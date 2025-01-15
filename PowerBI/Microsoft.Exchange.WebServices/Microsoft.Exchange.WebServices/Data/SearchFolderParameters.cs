using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000089 RID: 137
	public sealed class SearchFolderParameters : ComplexProperty
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00014CC2 File Offset: 0x00013CC2
		internal SearchFolderParameters()
		{
			this.rootFolderIds.OnChange += this.PropertyChanged;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00014CEC File Offset: 0x00013CEC
		private void PropertyChanged(ComplexProperty complexProperty)
		{
			this.Changed();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00014CF4 File Offset: 0x00013CF4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "BaseFolderIds")
				{
					this.RootFolderIds.InternalClear();
					this.RootFolderIds.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "Restriction")
				{
					reader.Read();
					this.searchFilter = SearchFilter.LoadFromXml(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00014D5B File Offset: 0x00013D5B
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.Traversal = reader.ReadAttributeValue<SearchFolderTraversal>("Traversal");
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00014D70 File Offset: 0x00013D70
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "BaseFolderIds"))
					{
						if (!(text2 == "Restriction"))
						{
							if (text2 == "Traversal")
							{
								this.Traversal = jsonProperty.ReadEnumValue<SearchFolderTraversal>(text);
							}
						}
						else
						{
							JsonObject jsonObject = jsonProperty.ReadAsJsonObject(text);
							this.searchFilter = SearchFilter.LoadSearchFilterFromJson(jsonObject.ReadAsJsonObject("Item"), service);
						}
					}
					else
					{
						this.RootFolderIds.InternalClear();
						((IJsonCollectionDeserializer)this.RootFolderIds).CreateFromJsonCollection(jsonProperty.ReadAsArray(text), service);
					}
				}
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00014E40 File Offset: 0x00013E40
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Traversal", this.Traversal);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00014E58 File Offset: 0x00013E58
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.SearchFilter != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, "Restriction");
				this.SearchFilter.WriteToXml(writer);
				writer.WriteEndElement();
			}
			this.RootFolderIds.WriteToXml(writer, "BaseFolderIds");
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00014E94 File Offset: 0x00013E94
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Traversal", this.Traversal);
			jsonObject.Add("BaseFolderIds", this.RootFolderIds.InternalToJson(service));
			if (this.SearchFilter != null)
			{
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("Item", this.SearchFilter.InternalToJson(service));
				jsonObject.Add("Restriction", jsonObject2);
			}
			return jsonObject;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00014F06 File Offset: 0x00013F06
		internal void Validate()
		{
			if (this.RootFolderIds.Count == 0)
			{
				throw new ServiceValidationException(Strings.SearchParametersRootFolderIdsEmpty);
			}
			if (this.SearchFilter != null)
			{
				this.SearchFilter.InternalValidate();
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00014F38 File Offset: 0x00013F38
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00014F40 File Offset: 0x00013F40
		public SearchFolderTraversal Traversal
		{
			get
			{
				return this.traversal;
			}
			set
			{
				this.SetFieldValue<SearchFolderTraversal>(ref this.traversal, value);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00014F4F File Offset: 0x00013F4F
		public FolderIdCollection RootFolderIds
		{
			get
			{
				return this.rootFolderIds;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00014F57 File Offset: 0x00013F57
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00014F60 File Offset: 0x00013F60
		public SearchFilter SearchFilter
		{
			get
			{
				return this.searchFilter;
			}
			set
			{
				if (this.searchFilter != null)
				{
					this.searchFilter.OnChange -= this.PropertyChanged;
				}
				this.SetFieldValue<SearchFilter>(ref this.searchFilter, value);
				if (this.searchFilter != null)
				{
					this.searchFilter.OnChange += this.PropertyChanged;
				}
			}
		}

		// Token: 0x04000204 RID: 516
		private SearchFolderTraversal traversal;

		// Token: 0x04000205 RID: 517
		private FolderIdCollection rootFolderIds = new FolderIdCollection();

		// Token: 0x04000206 RID: 518
		private SearchFilter searchFilter;
	}
}
