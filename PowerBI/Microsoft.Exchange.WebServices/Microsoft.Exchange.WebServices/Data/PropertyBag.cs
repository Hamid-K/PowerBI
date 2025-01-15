using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000DE RID: 222
	internal class PropertyBag
	{
		// Token: 0x06000B22 RID: 2850 RVA: 0x00024B5C File Offset: 0x00023B5C
		internal PropertyBag(ServiceObject owner)
		{
			EwsUtilities.Assert(owner != null, "PropertyBag.ctor", "owner is null");
			this.owner = owner;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00024BC3 File Offset: 0x00023BC3
		internal Dictionary<PropertyDefinition, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00024BCB File Offset: 0x00023BCB
		internal ServiceObject Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00024BD4 File Offset: 0x00023BD4
		internal bool IsDirty
		{
			get
			{
				int num = this.modifiedProperties.Count + this.deletedProperties.Count + this.addedProperties.Count;
				return num > 0 || this.isDirty;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00024C11 File Offset: 0x00023C11
		internal static void AddToChangeList(PropertyDefinition propertyDefinition, List<PropertyDefinition> changeList)
		{
			if (!changeList.Contains(propertyDefinition))
			{
				changeList.Add(propertyDefinition);
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00024C24 File Offset: 0x00023C24
		internal static JsonObject CreateJsonSetUpdate(PropertyDefinition propertyDefinition, ExchangeService service, ServiceObject serviceObject, PropertyBag propertyBag)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(serviceObject.GetSetFieldXmlElementName());
			jsonObject.Add("Path", ((IJsonSerializable)propertyDefinition).ToJson(service));
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.AddTypeParameter(serviceObject.GetXmlElementName());
			propertyDefinition.WriteJsonValue(jsonObject2, propertyBag, service, true);
			jsonObject.Add(PropertyBag.GetPropertyUpdateItemName(serviceObject), jsonObject2);
			return jsonObject;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00024C80 File Offset: 0x00023C80
		internal static JsonObject CreateJsonSetUpdate(ExtendedProperty value, ExchangeService service, ServiceObject serviceObject)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(serviceObject.GetSetFieldXmlElementName());
			jsonObject.Add("Path", ((IJsonSerializable)value.PropertyDefinition).ToJson(service));
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.AddTypeParameter(serviceObject.GetXmlElementName());
			jsonObject2.Add("ExtendedProperty", new object[] { value.InternalToJson(service) });
			jsonObject.Add(PropertyBag.GetPropertyUpdateItemName(serviceObject), jsonObject2);
			return jsonObject;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00024CF4 File Offset: 0x00023CF4
		internal static JsonObject CreateJsonDeleteUpdate(PropertyDefinitionBase propertyDefinition, ExchangeService service, ServiceObject serviceObject)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(serviceObject.GetDeleteFieldXmlElementName());
			jsonObject.Add("PropertyPath", ((IJsonSerializable)propertyDefinition).ToJson(service));
			return jsonObject;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00024D26 File Offset: 0x00023D26
		internal static string GetPropertyUpdateItemName(ServiceObject serviceObject)
		{
			if (!(serviceObject is Folder))
			{
				return "Item";
			}
			return "Folder";
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00024D3B File Offset: 0x00023D3B
		internal bool IsPropertyLoaded(PropertyDefinition propertyDefinition)
		{
			return this.loadedProperties.Contains(propertyDefinition) || this.IsRequestedProperty(propertyDefinition);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00024D54 File Offset: 0x00023D54
		private bool IsRequestedProperty(PropertyDefinition propertyDefinition)
		{
			if (this.requestedPropertySet == null)
			{
				return false;
			}
			if (this.requestedPropertySet.BasePropertySet == BasePropertySet.FirstClassProperties)
			{
				List<PropertyDefinition> list = (this.onlySummaryPropertiesRequested ? this.Owner.Schema.FirstClassSummaryProperties : this.Owner.Schema.FirstClassProperties);
				return list.Contains(propertyDefinition) || this.requestedPropertySet.Contains(propertyDefinition);
			}
			return this.requestedPropertySet.Contains(propertyDefinition);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00024DC8 File Offset: 0x00023DC8
		internal bool IsPropertyUpdated(PropertyDefinition propertyDefinition)
		{
			return this.modifiedProperties.Contains(propertyDefinition) || this.addedProperties.Contains(propertyDefinition);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00024DE8 File Offset: 0x00023DE8
		internal bool TryGetProperty(PropertyDefinition propertyDefinition, out object propertyValue)
		{
			ServiceLocalException ex;
			propertyValue = this.GetPropertyValueOrException(propertyDefinition, out ex);
			return ex == null;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00024E04 File Offset: 0x00023E04
		internal bool TryGetProperty<T>(PropertyDefinition propertyDefinition, out T propertyValue)
		{
			if (!typeof(T).IsAssignableFrom(propertyDefinition.Type))
			{
				string text = string.Format(Strings.PropertyDefinitionTypeMismatch, EwsUtilities.GetPrintableTypeName(propertyDefinition.Type), EwsUtilities.GetPrintableTypeName(typeof(T)));
				throw new ArgumentException(text, "propertyDefinition");
			}
			object obj;
			bool flag = this.TryGetProperty(propertyDefinition, out obj);
			propertyValue = (flag ? ((T)((object)obj)) : default(T));
			return flag;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00024E84 File Offset: 0x00023E84
		private object GetPropertyValueOrException(PropertyDefinition propertyDefinition, out ServiceLocalException exception)
		{
			object obj = null;
			exception = null;
			if (propertyDefinition.Version > this.Owner.Service.RequestedServerVersion)
			{
				exception = new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, propertyDefinition.Name, propertyDefinition.Version));
				return null;
			}
			if (this.TryGetValue(propertyDefinition, out obj))
			{
				return obj;
			}
			if (propertyDefinition.HasFlag(PropertyDefinitionFlags.AutoInstantiateOnRead))
			{
				ComplexPropertyDefinitionBase complexPropertyDefinitionBase = propertyDefinition as ComplexPropertyDefinitionBase;
				EwsUtilities.Assert(complexPropertyDefinitionBase != null, "PropertyBag.get_this[]", "propertyDefinition is marked with AutoInstantiateOnRead but is not a descendant of ComplexPropertyDefinitionBase");
				obj = complexPropertyDefinitionBase.CreatePropertyInstance(this.Owner);
				if (obj != null)
				{
					this.InitComplexProperty(obj as ComplexProperty);
					this.properties[propertyDefinition] = obj;
				}
			}
			else if (propertyDefinition != this.Owner.GetIdPropertyDefinition())
			{
				if (!this.IsPropertyLoaded(propertyDefinition))
				{
					exception = new ServiceObjectPropertyException(Strings.MustLoadOrAssignPropertyBeforeAccess, propertyDefinition);
					return null;
				}
				if (!propertyDefinition.IsNullable)
				{
					string text = (this.IsRequestedProperty(propertyDefinition) ? Strings.ValuePropertyNotLoaded : Strings.ValuePropertyNotAssigned);
					exception = new ServiceObjectPropertyException(text, propertyDefinition);
				}
			}
			return obj;
		}

		// Token: 0x17000277 RID: 631
		internal object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				ServiceLocalException ex;
				object propertyValueOrException = this.GetPropertyValueOrException(propertyDefinition, out ex);
				if (ex == null)
				{
					return propertyValueOrException;
				}
				throw ex;
			}
			set
			{
				if (propertyDefinition.Version > this.Owner.Service.RequestedServerVersion)
				{
					throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, propertyDefinition.Name, propertyDefinition.Version));
				}
				if (!this.loading)
				{
					if (this.Owner.IsNew && !propertyDefinition.HasFlag(PropertyDefinitionFlags.CanSet, new ExchangeVersion?(this.Owner.Service.RequestedServerVersion)))
					{
						throw new ServiceObjectPropertyException(Strings.PropertyIsReadOnly, propertyDefinition);
					}
					if (!this.Owner.IsNew)
					{
						Item item = this.Owner as Item;
						if (item != null && item.IsAttachment)
						{
							throw new ServiceObjectPropertyException(Strings.ItemAttachmentCannotBeUpdated, propertyDefinition);
						}
						if (value == null && !propertyDefinition.HasFlag(PropertyDefinitionFlags.CanDelete))
						{
							throw new ServiceObjectPropertyException(Strings.PropertyCannotBeDeleted, propertyDefinition);
						}
						if (!propertyDefinition.HasFlag(PropertyDefinitionFlags.CanUpdate))
						{
							throw new ServiceObjectPropertyException(Strings.PropertyCannotBeUpdated, propertyDefinition);
						}
					}
				}
				if (value == null)
				{
					this.DeleteProperty(propertyDefinition);
					return;
				}
				object obj;
				if (this.properties.TryGetValue(propertyDefinition, ref obj))
				{
					ComplexProperty complexProperty = obj as ComplexProperty;
					if (complexProperty != null)
					{
						complexProperty.OnChange -= this.PropertyChanged;
					}
				}
				if (this.deletedProperties.Remove(propertyDefinition))
				{
					PropertyBag.AddToChangeList(propertyDefinition, this.modifiedProperties);
				}
				else if (!this.properties.ContainsKey(propertyDefinition))
				{
					PropertyBag.AddToChangeList(propertyDefinition, this.addedProperties);
				}
				else if (!this.modifiedProperties.Contains(propertyDefinition))
				{
					PropertyBag.AddToChangeList(propertyDefinition, this.modifiedProperties);
				}
				this.InitComplexProperty(value as ComplexProperty);
				this.properties[propertyDefinition] = value;
				this.Changed();
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00025157 File Offset: 0x00024157
		internal void Changed()
		{
			this.isDirty = true;
			this.Owner.Changed();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002516B File Offset: 0x0002416B
		internal bool Contains(PropertyDefinition propertyDefinition)
		{
			return this.properties.ContainsKey(propertyDefinition);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00025179 File Offset: 0x00024179
		internal bool TryGetValue(PropertyDefinition propertyDefinition, out object propertyValue)
		{
			return this.properties.TryGetValue(propertyDefinition, ref propertyValue);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00025188 File Offset: 0x00024188
		internal void PropertyChanged(ComplexProperty complexProperty)
		{
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.properties)
			{
				if (keyValuePair.Value == complexProperty && !this.deletedProperties.ContainsKey(keyValuePair.Key))
				{
					PropertyBag.AddToChangeList(keyValuePair.Key, this.modifiedProperties);
					this.Changed();
				}
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002520C File Offset: 0x0002420C
		internal void DeleteProperty(PropertyDefinition propertyDefinition)
		{
			if (!this.deletedProperties.ContainsKey(propertyDefinition))
			{
				object obj;
				this.properties.TryGetValue(propertyDefinition, ref obj);
				this.properties.Remove(propertyDefinition);
				this.modifiedProperties.Remove(propertyDefinition);
				this.deletedProperties.Add(propertyDefinition, obj);
				ComplexProperty complexProperty = obj as ComplexProperty;
				if (complexProperty != null)
				{
					complexProperty.OnChange -= this.PropertyChanged;
				}
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00025279 File Offset: 0x00024279
		internal void Clear()
		{
			this.ClearChangeLog();
			this.properties.Clear();
			this.loadedProperties.Clear();
			this.requestedPropertySet = null;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000252A0 File Offset: 0x000242A0
		internal void ClearChangeLog()
		{
			this.deletedProperties.Clear();
			this.modifiedProperties.Clear();
			this.addedProperties.Clear();
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.properties)
			{
				ComplexProperty complexProperty = keyValuePair.Value as ComplexProperty;
				if (complexProperty != null)
				{
					complexProperty.ClearChangeLog();
				}
			}
			this.isDirty = false;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002532C File Offset: 0x0002432C
		internal void LoadFromXml(EwsServiceXmlReader reader, bool clear, PropertySet requestedPropertySet, bool onlySummaryPropertiesRequested)
		{
			if (clear)
			{
				this.Clear();
			}
			this.loading = true;
			this.requestedPropertySet = requestedPropertySet;
			this.onlySummaryPropertiesRequested = onlySummaryPropertiesRequested;
			try
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1)
					{
						PropertyDefinition propertyDefinition;
						if (this.Owner.Schema.TryGetPropertyDefinition(reader.LocalName, out propertyDefinition))
						{
							propertyDefinition.LoadPropertyValueFromXml(reader, this);
							this.loadedProperties.Add(propertyDefinition);
						}
						else
						{
							reader.SkipCurrentElement();
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, this.Owner.GetXmlElementName()));
				this.ClearChangeLog();
			}
			finally
			{
				this.loading = false;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000253D4 File Offset: 0x000243D4
		internal void LoadFromJson(JsonObject jsonServiceObject, ExchangeService service, bool clear, PropertySet requestedPropertySet, bool onlySummaryPropertiesRequested)
		{
			if (clear)
			{
				this.Clear();
			}
			this.loading = true;
			this.requestedPropertySet = requestedPropertySet;
			this.onlySummaryPropertiesRequested = onlySummaryPropertiesRequested;
			try
			{
				foreach (string text in jsonServiceObject.Keys)
				{
					PropertyDefinition propertyDefinition;
					if (this.Owner.Schema.TryGetPropertyDefinition(text, out propertyDefinition))
					{
						propertyDefinition.LoadPropertyValueFromJson(jsonServiceObject[text], service, this);
						this.loadedProperties.Add(propertyDefinition);
					}
				}
				this.ClearChangeLog();
			}
			finally
			{
				this.loading = false;
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002548C File Offset: 0x0002448C
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, this.Owner.GetXmlElementName());
			foreach (PropertyDefinition propertyDefinition in this.Owner.Schema)
			{
				if (propertyDefinition.HasFlag(PropertyDefinitionFlags.CanSet, new ExchangeVersion?(writer.Service.RequestedServerVersion)) && this.Contains(propertyDefinition))
				{
					propertyDefinition.WritePropertyValueToXml(writer, this, false);
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002551C File Offset: 0x0002451C
		internal object ToJson(ExchangeService service, bool isUpdateOperation)
		{
			JsonObject jsonObject = new JsonObject();
			if (!isUpdateOperation)
			{
				this.ToJsonForCreate(service, jsonObject);
			}
			else
			{
				this.ToJsonForUpdate(service, jsonObject);
			}
			return jsonObject;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00025548 File Offset: 0x00024548
		private void ToJsonForUpdate(ExchangeService service, JsonObject jsonObject)
		{
			jsonObject.AddTypeParameter(this.Owner.GetChangeXmlElementName());
			jsonObject.Add(this.Owner.GetId().GetJsonTypeName(), this.Owner.GetId().InternalToJson(service));
			List<JsonObject> list = new List<JsonObject>();
			foreach (PropertyDefinition propertyDefinition in this.addedProperties)
			{
				this.WriteSetUpdateToJson(list, propertyDefinition, service);
			}
			foreach (PropertyDefinition propertyDefinition2 in this.modifiedProperties)
			{
				this.WriteSetUpdateToJson(list, propertyDefinition2, service);
			}
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.deletedProperties)
			{
				this.WriteDeleteUpdateToJson(list, keyValuePair.Key, keyValuePair.Value, service);
			}
			jsonObject.Add("Updates", list.ToArray());
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00025684 File Offset: 0x00024684
		private void ToJsonForCreate(ExchangeService service, JsonObject jsonObject)
		{
			jsonObject.AddTypeParameter(this.Owner.GetXmlElementName());
			foreach (PropertyDefinition propertyDefinition in this.Owner.Schema)
			{
				if (propertyDefinition.HasFlag(PropertyDefinitionFlags.CanSet, new ExchangeVersion?(service.RequestedServerVersion)) && this.Contains(propertyDefinition))
				{
					propertyDefinition.WriteJsonValue(jsonObject, this, service, false);
				}
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00025708 File Offset: 0x00024708
		internal void WriteToXmlForUpdate(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, this.Owner.GetChangeXmlElementName());
			this.Owner.GetId().WriteToXml(writer);
			writer.WriteStartElement(XmlNamespace.Types, "Updates");
			foreach (PropertyDefinition propertyDefinition in this.addedProperties)
			{
				this.WriteSetUpdateToXml(writer, propertyDefinition);
			}
			foreach (PropertyDefinition propertyDefinition2 in this.modifiedProperties)
			{
				this.WriteSetUpdateToXml(writer, propertyDefinition2);
			}
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.deletedProperties)
			{
				this.WriteDeleteUpdateToXml(writer, keyValuePair.Key, keyValuePair.Value);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002582C File Offset: 0x0002482C
		internal bool GetIsUpdateCallNecessary()
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			list.AddRange(this.addedProperties);
			list.AddRange(this.modifiedProperties);
			list.AddRange(this.deletedProperties.Keys);
			foreach (PropertyDefinition propertyDefinition in list)
			{
				if (propertyDefinition.HasFlag(PropertyDefinitionFlags.CanUpdate))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000258B4 File Offset: 0x000248B4
		private void InitComplexProperty(ComplexProperty complexProperty)
		{
			if (complexProperty != null)
			{
				complexProperty.OnChange += this.PropertyChanged;
				IOwnedProperty ownedProperty = complexProperty as IOwnedProperty;
				if (ownedProperty != null)
				{
					ownedProperty.Owner = this.Owner;
				}
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000258EC File Offset: 0x000248EC
		private void WriteSetUpdateToXml(EwsServiceXmlWriter writer, PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition.HasFlag(PropertyDefinitionFlags.CanUpdate))
			{
				object obj = this[propertyDefinition];
				bool flag = false;
				ICustomUpdateSerializer customUpdateSerializer = obj as ICustomUpdateSerializer;
				if (customUpdateSerializer != null)
				{
					flag = customUpdateSerializer.WriteSetUpdateToXml(writer, this.Owner, propertyDefinition);
				}
				if (!flag)
				{
					writer.WriteStartElement(XmlNamespace.Types, this.Owner.GetSetFieldXmlElementName());
					propertyDefinition.WriteToXml(writer);
					writer.WriteStartElement(XmlNamespace.Types, this.Owner.GetXmlElementName());
					propertyDefinition.WritePropertyValueToXml(writer, this, true);
					writer.WriteEndElement();
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00025968 File Offset: 0x00024968
		private void WriteSetUpdateToJson(List<JsonObject> jsonUpdates, PropertyDefinition propertyDefinition, ExchangeService service)
		{
			if (propertyDefinition.HasFlag(PropertyDefinitionFlags.CanUpdate))
			{
				object obj = this[propertyDefinition];
				bool flag = false;
				ICustomUpdateSerializer customUpdateSerializer = obj as ICustomUpdateSerializer;
				if (customUpdateSerializer != null)
				{
					flag = customUpdateSerializer.WriteSetUpdateToJson(service, this.Owner, propertyDefinition, jsonUpdates);
				}
				if (!flag)
				{
					JsonObject jsonObject = PropertyBag.CreateJsonSetUpdate(propertyDefinition, service, this.Owner, this);
					jsonUpdates.Add(jsonObject);
				}
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000259BC File Offset: 0x000249BC
		private void WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, PropertyDefinition propertyDefinition, object propertyValue)
		{
			if (propertyDefinition.HasFlag(PropertyDefinitionFlags.CanDelete))
			{
				bool flag = false;
				ICustomUpdateSerializer customUpdateSerializer = propertyValue as ICustomUpdateSerializer;
				if (customUpdateSerializer != null)
				{
					flag = customUpdateSerializer.WriteDeleteUpdateToXml(writer, this.Owner);
				}
				if (!flag)
				{
					writer.WriteStartElement(XmlNamespace.Types, this.Owner.GetDeleteFieldXmlElementName());
					propertyDefinition.WriteToXml(writer);
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00025A10 File Offset: 0x00024A10
		private void WriteDeleteUpdateToJson(List<JsonObject> jsonUpdates, PropertyDefinition propertyDefinition, object propertyValue, ExchangeService service)
		{
			if (propertyDefinition.HasFlag(PropertyDefinitionFlags.CanDelete))
			{
				bool flag = false;
				ICustomUpdateSerializer customUpdateSerializer = propertyValue as ICustomUpdateSerializer;
				if (customUpdateSerializer != null)
				{
					flag = customUpdateSerializer.WriteDeleteUpdateToJson(service, this.Owner, jsonUpdates);
				}
				if (!flag)
				{
					JsonObject jsonObject = PropertyBag.CreateJsonDeleteUpdate(propertyDefinition, service, this.Owner);
					jsonUpdates.Add(jsonObject);
				}
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00025A5C File Offset: 0x00024A5C
		internal void Validate()
		{
			foreach (PropertyDefinition propertyDefinition in this.addedProperties)
			{
				this.ValidatePropertyValue(propertyDefinition);
			}
			foreach (PropertyDefinition propertyDefinition2 in this.modifiedProperties)
			{
				this.ValidatePropertyValue(propertyDefinition2);
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00025AF4 File Offset: 0x00024AF4
		private void ValidatePropertyValue(PropertyDefinition propertyDefinition)
		{
			object obj;
			if (this.TryGetProperty(propertyDefinition, out obj))
			{
				ISelfValidate selfValidate = obj as ISelfValidate;
				if (selfValidate != null)
				{
					selfValidate.Validate();
				}
			}
		}

		// Token: 0x04000355 RID: 853
		private ServiceObject owner;

		// Token: 0x04000356 RID: 854
		private bool isDirty;

		// Token: 0x04000357 RID: 855
		private bool loading;

		// Token: 0x04000358 RID: 856
		private bool onlySummaryPropertiesRequested;

		// Token: 0x04000359 RID: 857
		private List<PropertyDefinition> loadedProperties = new List<PropertyDefinition>();

		// Token: 0x0400035A RID: 858
		private Dictionary<PropertyDefinition, object> properties = new Dictionary<PropertyDefinition, object>();

		// Token: 0x0400035B RID: 859
		private Dictionary<PropertyDefinition, object> deletedProperties = new Dictionary<PropertyDefinition, object>();

		// Token: 0x0400035C RID: 860
		private List<PropertyDefinition> modifiedProperties = new List<PropertyDefinition>();

		// Token: 0x0400035D RID: 861
		private List<PropertyDefinition> addedProperties = new List<PropertyDefinition>();

		// Token: 0x0400035E RID: 862
		private PropertySet requestedPropertySet;
	}
}
