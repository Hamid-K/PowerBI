using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200002E RID: 46
	[EditorBrowsable(1)]
	public abstract class ComplexPropertyCollection<TComplexProperty> : ComplexProperty, IEnumerable<TComplexProperty>, IEnumerable, ICustomUpdateSerializer, IJsonCollectionDeserializer where TComplexProperty : ComplexProperty
	{
		// Token: 0x0600020B RID: 523
		internal abstract TComplexProperty CreateComplexProperty(string xmlElementName);

		// Token: 0x0600020C RID: 524
		internal abstract TComplexProperty CreateDefaultComplexProperty();

		// Token: 0x0600020D RID: 525
		internal abstract string GetCollectionItemXmlElementName(TComplexProperty complexProperty);

		// Token: 0x0600020E RID: 526 RVA: 0x00009455 File Offset: 0x00008455
		internal ComplexPropertyCollection()
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000948C File Offset: 0x0000848C
		internal void ItemChanged(ComplexProperty complexProperty)
		{
			TComplexProperty tcomplexProperty = complexProperty as TComplexProperty;
			EwsUtilities.Assert(tcomplexProperty != null, "ComplexPropertyCollection.ItemChanged", string.Format("ComplexPropertyCollection.ItemChanged: the type of the complexProperty argument ({0}) is not supported.", complexProperty.GetType().Name));
			if (!this.addedItems.Contains(tcomplexProperty) && !this.modifiedItems.Contains(tcomplexProperty))
			{
				this.modifiedItems.Add(tcomplexProperty);
				this.Changed();
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000094FE File Offset: 0x000084FE
		internal override void LoadFromXml(EwsServiceXmlReader reader, string localElementName)
		{
			this.LoadFromXml(reader, XmlNamespace.Types, localElementName);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000950C File Offset: 0x0000850C
		internal override void LoadFromXml(EwsServiceXmlReader reader, XmlNamespace xmlNamespace, string localElementName)
		{
			reader.EnsureCurrentNodeIsStartElement(xmlNamespace, localElementName);
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement())
					{
						TComplexProperty tcomplexProperty = this.CreateComplexProperty(reader.LocalName);
						if (tcomplexProperty != null)
						{
							tcomplexProperty.LoadFromXml(reader, reader.LocalName);
							this.InternalAdd(tcomplexProperty, true);
						}
						else
						{
							reader.SkipCurrentElement();
						}
					}
				}
				while (!reader.IsEndElement(xmlNamespace, localElementName));
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000957C File Offset: 0x0000857C
		internal override void UpdateFromXml(EwsServiceXmlReader reader, XmlNamespace xmlNamespace, string xmlElementName)
		{
			reader.EnsureCurrentNodeIsStartElement(xmlNamespace, xmlElementName);
			if (!reader.IsEmptyElement)
			{
				int num = 0;
				for (;;)
				{
					reader.Read();
					if (reader.IsStartElement())
					{
						TComplexProperty tcomplexProperty = this.CreateComplexProperty(reader.LocalName);
						TComplexProperty tcomplexProperty2 = this[num++];
						if (tcomplexProperty == null || !tcomplexProperty.GetType().IsInstanceOfType(tcomplexProperty2))
						{
							break;
						}
						tcomplexProperty2.UpdateFromXml(reader, xmlNamespace, reader.LocalName);
					}
					if (reader.IsEndElement(xmlNamespace, xmlElementName))
					{
						return;
					}
				}
				throw new ServiceLocalException(Strings.PropertyTypeIncompatibleWhenUpdatingCollection);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009614 File Offset: 0x00008614
		void IJsonCollectionDeserializer.CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				JsonObject jsonObject = obj as JsonObject;
				if (jsonObject != null)
				{
					TComplexProperty tcomplexProperty = default(TComplexProperty);
					if (jsonObject.HasTypeProperty())
					{
						tcomplexProperty = this.CreateComplexProperty(jsonObject.ReadTypeString());
					}
					else
					{
						tcomplexProperty = this.CreateDefaultComplexProperty();
					}
					if (tcomplexProperty != null)
					{
						tcomplexProperty.LoadFromJson(jsonObject, service);
						this.InternalAdd(tcomplexProperty, true);
					}
				}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000968C File Offset: 0x0000868C
		void IJsonCollectionDeserializer.UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			if (this.Count != jsonCollection.Length)
			{
				throw new ServiceLocalException(Strings.PropertyCollectionSizeMismatch);
			}
			int num = 0;
			foreach (object obj in jsonCollection)
			{
				JsonObject jsonObject = obj as JsonObject;
				if (jsonObject == null)
				{
					throw new ServiceLocalException();
				}
				TComplexProperty tcomplexProperty = default(TComplexProperty);
				if (jsonObject.HasTypeProperty())
				{
					tcomplexProperty = this.CreateComplexProperty(jsonObject.ReadTypeString());
				}
				else
				{
					tcomplexProperty = this.CreateDefaultComplexProperty();
				}
				TComplexProperty tcomplexProperty2 = this[num++];
				if (tcomplexProperty == null || !tcomplexProperty.GetType().IsInstanceOfType(tcomplexProperty2))
				{
					throw new ServiceLocalException(Strings.PropertyTypeIncompatibleWhenUpdatingCollection);
				}
				tcomplexProperty2.LoadFromJson(jsonObject, service);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000975F File Offset: 0x0000875F
		internal override void WriteToXml(EwsServiceXmlWriter writer, XmlNamespace xmlNamespace, string xmlElementName)
		{
			if (this.ShouldWriteToRequest())
			{
				base.WriteToXml(writer, xmlNamespace, xmlElementName);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009774 File Offset: 0x00008774
		internal override object InternalToJson(ExchangeService service)
		{
			List<object> list = new List<object>();
			foreach (TComplexProperty tcomplexProperty in this)
			{
				list.Add(tcomplexProperty.InternalToJson(service));
			}
			return list.ToArray();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000097D8 File Offset: 0x000087D8
		internal virtual bool ShouldWriteToRequest()
		{
			return this.Count > 0;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000097E4 File Offset: 0x000087E4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			foreach (TComplexProperty tcomplexProperty in this)
			{
				tcomplexProperty.WriteToXml(writer, this.GetCollectionItemXmlElementName(tcomplexProperty));
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000983C File Offset: 0x0000883C
		internal override void ClearChangeLog()
		{
			this.removedItems.Clear();
			this.addedItems.Clear();
			this.modifiedItems.Clear();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000985F File Offset: 0x0000885F
		internal void RemoveFromChangeLog(TComplexProperty complexProperty)
		{
			this.removedItems.Remove(complexProperty);
			this.modifiedItems.Remove(complexProperty);
			this.addedItems.Remove(complexProperty);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009888 File Offset: 0x00008888
		internal List<TComplexProperty> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00009890 File Offset: 0x00008890
		internal List<TComplexProperty> AddedItems
		{
			get
			{
				return this.addedItems;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009898 File Offset: 0x00008898
		internal List<TComplexProperty> ModifiedItems
		{
			get
			{
				return this.modifiedItems;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000098A0 File Offset: 0x000088A0
		internal List<TComplexProperty> RemovedItems
		{
			get
			{
				return this.removedItems;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000098A8 File Offset: 0x000088A8
		internal void InternalAdd(TComplexProperty complexProperty)
		{
			this.InternalAdd(complexProperty, false);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000098B4 File Offset: 0x000088B4
		private void InternalAdd(TComplexProperty complexProperty, bool loading)
		{
			EwsUtilities.Assert(complexProperty != null, "ComplexPropertyCollection.InternalAdd", "complexProperty is null");
			if (!this.items.Contains(complexProperty))
			{
				this.items.Add(complexProperty);
				if (!loading)
				{
					this.removedItems.Remove(complexProperty);
					this.addedItems.Add(complexProperty);
				}
				complexProperty.OnChange += this.ItemChanged;
				this.Changed();
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000992F File Offset: 0x0000892F
		internal void InternalClear()
		{
			while (this.Count > 0)
			{
				this.InternalRemoveAt(0);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009943 File Offset: 0x00008943
		internal void InternalRemoveAt(int index)
		{
			EwsUtilities.Assert(index >= 0 && index < this.Count, "ComplexPropertyCollection.InternalRemoveAt", "index is out of range.");
			this.InternalRemove(this.items[index]);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009978 File Offset: 0x00008978
		internal bool InternalRemove(TComplexProperty complexProperty)
		{
			EwsUtilities.Assert(complexProperty != null, "ComplexPropertyCollection.InternalRemove", "complexProperty is null");
			if (this.items.Remove(complexProperty))
			{
				complexProperty.OnChange -= this.ItemChanged;
				if (!this.addedItems.Contains(complexProperty))
				{
					this.removedItems.Add(complexProperty);
				}
				else
				{
					this.addedItems.Remove(complexProperty);
				}
				this.modifiedItems.Remove(complexProperty);
				this.Changed();
				return true;
			}
			return false;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009A04 File Offset: 0x00008A04
		public bool Contains(TComplexProperty complexProperty)
		{
			return this.items.Contains(complexProperty);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009A12 File Offset: 0x00008A12
		public int IndexOf(TComplexProperty complexProperty)
		{
			return this.items.IndexOf(complexProperty);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009A20 File Offset: 0x00008A20
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000073 RID: 115
		public TComplexProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				return this.items[index];
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009A5D File Offset: 0x00008A5D
		public IEnumerator<TComplexProperty> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009A6F File Offset: 0x00008A6F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009A81 File Offset: 0x00008A81
		bool ICustomUpdateSerializer.WriteSetUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject, PropertyDefinition propertyDefinition)
		{
			if (this.Count == 0)
			{
				writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetDeleteFieldXmlElementName());
				propertyDefinition.WriteToXml(writer);
				writer.WriteEndElement();
				return true;
			}
			return false;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009AA8 File Offset: 0x00008AA8
		bool ICustomUpdateSerializer.WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject)
		{
			return false;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009AAC File Offset: 0x00008AAC
		bool ICustomUpdateSerializer.WriteSetUpdateToJson(ExchangeService service, ServiceObject ewsObject, PropertyDefinition propertyDefinition, List<JsonObject> updates)
		{
			if (this.Count == 0)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.AddTypeParameter(ewsObject.GetDeleteFieldXmlElementName());
				jsonObject.Add("Path", ((IJsonSerializable)propertyDefinition).ToJson(service));
				return true;
			}
			return false;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009AE8 File Offset: 0x00008AE8
		bool ICustomUpdateSerializer.WriteDeleteUpdateToJson(ExchangeService service, ServiceObject ewsObject, List<JsonObject> updates)
		{
			return false;
		}

		// Token: 0x04000114 RID: 276
		private List<TComplexProperty> items = new List<TComplexProperty>();

		// Token: 0x04000115 RID: 277
		private List<TComplexProperty> addedItems = new List<TComplexProperty>();

		// Token: 0x04000116 RID: 278
		private List<TComplexProperty> modifiedItems = new List<TComplexProperty>();

		// Token: 0x04000117 RID: 279
		private List<TComplexProperty> removedItems = new List<TComplexProperty>();
	}
}
