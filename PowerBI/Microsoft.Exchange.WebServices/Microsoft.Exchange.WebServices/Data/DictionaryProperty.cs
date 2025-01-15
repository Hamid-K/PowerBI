using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000052 RID: 82
	[EditorBrowsable(1)]
	public abstract class DictionaryProperty<TKey, TEntry> : ComplexProperty, ICustomUpdateSerializer, IJsonCollectionDeserializer where TEntry : DictionaryEntryProperty<TKey>
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000D3B0 File Offset: 0x0000C3B0
		private void EntryChanged(ComplexProperty complexProperty)
		{
			TEntry tentry = complexProperty as TEntry;
			TKey key = tentry.Key;
			if (!this.addedEntries.Contains(key) && !this.modifiedEntries.Contains(key))
			{
				this.modifiedEntries.Add(key);
				this.Changed();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D405 File Offset: 0x0000C405
		private void WriteUriToXml(EwsServiceXmlWriter writer, TKey key)
		{
			writer.WriteStartElement(XmlNamespace.Types, "IndexedFieldURI");
			writer.WriteAttributeValue("FieldURI", this.GetFieldURI());
			writer.WriteAttributeValue("FieldIndex", this.GetFieldIndex(key));
			writer.WriteEndElement();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000D43C File Offset: 0x0000C43C
		private JsonObject WriteUriToJson(TKey key)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter("DictionaryPropertyUri");
			jsonObject.Add("FieldURI", this.GetFieldURI());
			jsonObject.Add("FieldIndex", this.GetFieldIndex(key));
			return jsonObject;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000D47E File Offset: 0x0000C47E
		internal virtual string GetFieldIndex(TKey key)
		{
			return key.ToString();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000D48D File Offset: 0x0000C48D
		internal virtual string GetFieldURI()
		{
			return null;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000D490 File Offset: 0x0000C490
		internal virtual TEntry CreateEntry(EwsServiceXmlReader reader)
		{
			if (reader.LocalName == "Entry")
			{
				return this.CreateEntryInstance();
			}
			return default(TEntry);
		}

		// Token: 0x060003A4 RID: 932
		internal abstract TEntry CreateEntryInstance();

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D4BF File Offset: 0x0000C4BF
		internal virtual string GetEntryXmlElementName(TEntry entry)
		{
			return "Entry";
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D4C8 File Offset: 0x0000C4C8
		internal override void ClearChangeLog()
		{
			this.addedEntries.Clear();
			this.removedEntries.Clear();
			this.modifiedEntries.Clear();
			foreach (TEntry tentry in this.entries.Values)
			{
				tentry.ClearChangeLog();
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D548 File Offset: 0x0000C548
		internal void InternalAdd(TEntry entry)
		{
			entry.OnChange += this.EntryChanged;
			this.entries.Add(entry.Key, entry);
			this.addedEntries.Add(entry.Key);
			this.removedEntries.Remove(entry.Key);
			this.Changed();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D5BC File Offset: 0x0000C5BC
		internal void InternalAddOrReplace(TEntry entry)
		{
			TEntry tentry;
			if (this.entries.TryGetValue(entry.Key, ref tentry))
			{
				tentry.OnChange -= this.EntryChanged;
				entry.OnChange += this.EntryChanged;
				if (!this.addedEntries.Contains(entry.Key) && !this.modifiedEntries.Contains(entry.Key))
				{
					this.modifiedEntries.Add(entry.Key);
				}
				this.Changed();
				return;
			}
			this.InternalAdd(entry);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D670 File Offset: 0x0000C670
		internal void InternalRemove(TKey key)
		{
			TEntry tentry;
			if (this.entries.TryGetValue(key, ref tentry))
			{
				tentry.OnChange -= this.EntryChanged;
				this.entries.Remove(key);
				this.removedEntries.Add(key, tentry);
				this.Changed();
			}
			this.addedEntries.Remove(key);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D6D4 File Offset: 0x0000C6D4
		internal override void LoadFromXml(EwsServiceXmlReader reader, string localElementName)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, localElementName);
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement())
					{
						TEntry tentry = this.CreateEntry(reader);
						if (tentry != null)
						{
							tentry.LoadFromXml(reader, reader.LocalName);
							this.InternalAdd(tentry);
						}
						else
						{
							reader.SkipCurrentElement();
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, localElementName));
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D73C File Offset: 0x0000C73C
		void IJsonCollectionDeserializer.CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				JsonObject jsonObject = obj as JsonObject;
				if (jsonObject != null)
				{
					TEntry tentry = this.CreateEntryInstance();
					tentry.LoadFromJson(jsonObject, service);
					this.InternalAdd(tentry);
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D789 File Offset: 0x0000C789
		void IJsonCollectionDeserializer.UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D790 File Offset: 0x0000C790
		internal override void WriteToXml(EwsServiceXmlWriter writer, XmlNamespace xmlNamespace, string xmlElementName)
		{
			if (this.entries.Count > 0)
			{
				base.WriteToXml(writer, xmlNamespace, xmlElementName);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000D7AC File Offset: 0x0000C7AC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			foreach (KeyValuePair<TKey, TEntry> keyValuePair in this.entries)
			{
				TEntry value = keyValuePair.Value;
				value.WriteToXml(writer, this.GetEntryXmlElementName(keyValuePair.Value));
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000D81C File Offset: 0x0000C81C
		internal override object InternalToJson(ExchangeService service)
		{
			List<object> list = new List<object>();
			foreach (KeyValuePair<TKey, TEntry> keyValuePair in this.entries)
			{
				List<object> list2 = list;
				TEntry value = keyValuePair.Value;
				list2.Add(value.InternalToJson(service));
			}
			return list.ToArray();
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D890 File Offset: 0x0000C890
		internal Dictionary<TKey, TEntry> Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000D898 File Offset: 0x0000C898
		public bool Contains(TKey key)
		{
			return this.Entries.ContainsKey(key);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D8A8 File Offset: 0x0000C8A8
		bool ICustomUpdateSerializer.WriteSetUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject, PropertyDefinition propertyDefinition)
		{
			List<TEntry> list = new List<TEntry>();
			foreach (TKey tkey in this.addedEntries)
			{
				list.Add(this.entries[tkey]);
			}
			foreach (TKey tkey2 in this.modifiedEntries)
			{
				list.Add(this.entries[tkey2]);
			}
			foreach (TEntry tentry in list)
			{
				if (!tentry.WriteSetUpdateToXml(writer, ewsObject, propertyDefinition.XmlElementName))
				{
					writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetSetFieldXmlElementName());
					this.WriteUriToXml(writer, tentry.Key);
					writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetXmlElementName());
					writer.WriteStartElement(XmlNamespace.Types, propertyDefinition.XmlElementName);
					tentry.WriteToXml(writer, this.GetEntryXmlElementName(tentry));
					writer.WriteEndElement();
					writer.WriteEndElement();
					writer.WriteEndElement();
				}
			}
			foreach (TEntry tentry2 in this.removedEntries.Values)
			{
				if (!tentry2.WriteDeleteUpdateToXml(writer, ewsObject))
				{
					writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetDeleteFieldXmlElementName());
					this.WriteUriToXml(writer, tentry2.Key);
					writer.WriteEndElement();
				}
			}
			return true;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000DA8C File Offset: 0x0000CA8C
		bool ICustomUpdateSerializer.WriteSetUpdateToJson(ExchangeService service, ServiceObject ewsObject, PropertyDefinition propertyDefinition, List<JsonObject> updates)
		{
			List<TEntry> list = new List<TEntry>();
			foreach (TKey tkey in this.addedEntries)
			{
				list.Add(this.entries[tkey]);
			}
			foreach (TKey tkey2 in this.modifiedEntries)
			{
				list.Add(this.entries[tkey2]);
			}
			foreach (TEntry tentry in list)
			{
				if (!tentry.WriteSetUpdateToJson(service, ewsObject, propertyDefinition, updates))
				{
					JsonObject jsonObject = new JsonObject();
					jsonObject.AddTypeParameter(ewsObject.GetSetFieldXmlElementName());
					JsonObject jsonObject2 = new JsonObject();
					jsonObject2.AddTypeParameter("DictionaryPropertyUri");
					jsonObject2.Add("FieldURI", this.GetFieldURI());
					JsonObject jsonObject3 = jsonObject2;
					string text = "FieldIndex";
					TKey key = tentry.Key;
					jsonObject3.Add(text, key.ToString());
					jsonObject.Add("Path", jsonObject2);
					object obj = tentry.InternalToJson(service);
					JsonObject jsonObject4 = new JsonObject();
					jsonObject4.AddTypeParameter(ewsObject.GetXmlElementName());
					jsonObject4.Add(propertyDefinition.XmlElementName, new object[] { obj });
					jsonObject.Add(PropertyBag.GetPropertyUpdateItemName(ewsObject), jsonObject4);
					updates.Add(jsonObject);
				}
			}
			foreach (TEntry tentry2 in this.removedEntries.Values)
			{
				if (!tentry2.WriteDeleteUpdateToJson(service, ewsObject, updates))
				{
					JsonObject jsonObject5 = new JsonObject();
					jsonObject5.AddTypeParameter(ewsObject.GetDeleteFieldXmlElementName());
					JsonObject jsonObject6 = new JsonObject();
					jsonObject6.AddTypeParameter("DictionaryPropertyUri");
					jsonObject6.Add("FieldURI", this.GetFieldURI());
					JsonObject jsonObject7 = jsonObject6;
					string text2 = "FieldIndex";
					TKey key2 = tentry2.Key;
					jsonObject7.Add(text2, key2.ToString());
					jsonObject5.Add("Path", jsonObject6);
					updates.Add(jsonObject5);
				}
			}
			return true;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000DD30 File Offset: 0x0000CD30
		bool ICustomUpdateSerializer.WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject)
		{
			return false;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000DD33 File Offset: 0x0000CD33
		bool ICustomUpdateSerializer.WriteDeleteUpdateToJson(ExchangeService service, ServiceObject ewsObject, List<JsonObject> updates)
		{
			return false;
		}

		// Token: 0x0400017B RID: 379
		private Dictionary<TKey, TEntry> entries = new Dictionary<TKey, TEntry>();

		// Token: 0x0400017C RID: 380
		private Dictionary<TKey, TEntry> removedEntries = new Dictionary<TKey, TEntry>();

		// Token: 0x0400017D RID: 381
		private List<TKey> addedEntries = new List<TKey>();

		// Token: 0x0400017E RID: 382
		private List<TKey> modifiedEntries = new List<TKey>();
	}
}
