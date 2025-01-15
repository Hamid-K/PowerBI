using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200018B RID: 395
	public abstract class ServiceObject
	{
		// Token: 0x06001140 RID: 4416 RVA: 0x000326F1 File Offset: 0x000316F1
		internal void Changed()
		{
			if (this.OnChange != null)
			{
				this.OnChange(this);
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00032707 File Offset: 0x00031707
		internal void ThrowIfThisIsNew()
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException(Strings.ServiceObjectDoesNotHaveId);
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00032721 File Offset: 0x00031721
		internal void ThrowIfThisIsNotNew()
		{
			if (!this.IsNew)
			{
				throw new InvalidOperationException(Strings.ServiceObjectAlreadyHasId);
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003273B File Offset: 0x0003173B
		internal virtual string GetXmlElementNameOverride()
		{
			return null;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00032740 File Offset: 0x00031740
		internal string GetXmlElementName()
		{
			if (string.IsNullOrEmpty(this.xmlElementName))
			{
				this.xmlElementName = this.GetXmlElementNameOverride();
				if (string.IsNullOrEmpty(this.xmlElementName))
				{
					lock (this.lockObject)
					{
						foreach (Attribute attribute in base.GetType().GetCustomAttributes(false))
						{
							ServiceObjectDefinitionAttribute serviceObjectDefinitionAttribute = attribute as ServiceObjectDefinitionAttribute;
							if (serviceObjectDefinitionAttribute != null)
							{
								this.xmlElementName = serviceObjectDefinitionAttribute.XmlElementName;
							}
						}
					}
				}
			}
			EwsUtilities.Assert(!string.IsNullOrEmpty(this.xmlElementName), "EwsObject.GetXmlElementName", string.Format("The class {0} does not have an associated XML element name.", base.GetType().Name));
			return this.xmlElementName;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003280C File Offset: 0x0003180C
		internal virtual string GetChangeXmlElementName()
		{
			return "ItemChange";
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00032813 File Offset: 0x00031813
		internal virtual string GetSetFieldXmlElementName()
		{
			return "SetItemField";
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003281A File Offset: 0x0003181A
		internal virtual string GetDeleteFieldXmlElementName()
		{
			return "DeleteItemField";
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00032821 File Offset: 0x00031821
		internal virtual bool GetIsTimeZoneHeaderRequired(bool isUpdateOperation)
		{
			return false;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00032824 File Offset: 0x00031824
		internal virtual bool GetIsCustomDateTimeScopingRequired()
		{
			return false;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00032827 File Offset: 0x00031827
		internal PropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0003282F File Offset: 0x0003182F
		internal ServiceObject(ExchangeService service)
		{
			EwsUtilities.ValidateParam(service, "service");
			EwsUtilities.ValidateServiceObjectVersion(this, service.RequestedServerVersion);
			this.service = service;
			this.propertyBag = new PropertyBag(this);
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0003286C File Offset: 0x0003186C
		public ServiceObjectSchema Schema
		{
			get
			{
				return this.GetSchema();
			}
		}

		// Token: 0x0600114D RID: 4429
		internal abstract ServiceObjectSchema GetSchema();

		// Token: 0x0600114E RID: 4430
		internal abstract ExchangeVersion GetMinimumRequiredServerVersion();

		// Token: 0x0600114F RID: 4431 RVA: 0x00032874 File Offset: 0x00031874
		internal void LoadFromXml(EwsServiceXmlReader reader, bool clearPropertyBag)
		{
			this.PropertyBag.LoadFromXml(reader, clearPropertyBag, null, false);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00032885 File Offset: 0x00031885
		internal virtual void Validate()
		{
			this.PropertyBag.Validate();
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00032892 File Offset: 0x00031892
		internal void LoadFromXml(EwsServiceXmlReader reader, bool clearPropertyBag, PropertySet requestedPropertySet, bool summaryPropertiesOnly)
		{
			this.PropertyBag.LoadFromXml(reader, clearPropertyBag, requestedPropertySet, summaryPropertiesOnly);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000328A4 File Offset: 0x000318A4
		internal void LoadFromJson(JsonObject jsonServiceObject, ExchangeService service, bool clearPropertyBag, PropertySet requestedPropertySet, bool summaryPropertiesOnly)
		{
			this.PropertyBag.LoadFromJson(jsonServiceObject, service, clearPropertyBag, requestedPropertySet, summaryPropertiesOnly);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000328B8 File Offset: 0x000318B8
		internal void LoadFromJson(JsonObject jsonObject, ExchangeService service, bool clearPropertyBag)
		{
			this.PropertyBag.LoadFromJson(jsonObject, service, clearPropertyBag, null, false);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000328CA File Offset: 0x000318CA
		internal void ClearChangeLog()
		{
			this.PropertyBag.ClearChangeLog();
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000328D7 File Offset: 0x000318D7
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.PropertyBag.WriteToXml(writer);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000328E5 File Offset: 0x000318E5
		internal object ToJson(ExchangeService service, bool isUpdateOperation)
		{
			return this.PropertyBag.ToJson(service, isUpdateOperation);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000328F4 File Offset: 0x000318F4
		internal void WriteToXmlForUpdate(EwsServiceXmlWriter writer)
		{
			this.PropertyBag.WriteToXmlForUpdate(writer);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00032902 File Offset: 0x00031902
		internal object WriteToJsonForUpdate(ExchangeService service)
		{
			return this.PropertyBag.ToJson(service, true);
		}

		// Token: 0x06001159 RID: 4441
		internal abstract void InternalLoad(PropertySet propertySet);

		// Token: 0x0600115A RID: 4442
		internal abstract void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences);

		// Token: 0x0600115B RID: 4443 RVA: 0x00032911 File Offset: 0x00031911
		public void Load(PropertySet propertySet)
		{
			this.InternalLoad(propertySet);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003291A File Offset: 0x0003191A
		public void Load()
		{
			this.InternalLoad(PropertySet.FirstClassProperties);
		}

		// Token: 0x1700039E RID: 926
		public object this[PropertyDefinitionBase propertyDefinition]
		{
			get
			{
				PropertyDefinition propertyDefinition2 = propertyDefinition as PropertyDefinition;
				if (propertyDefinition2 != null)
				{
					return this.PropertyBag[propertyDefinition2];
				}
				ExtendedPropertyDefinition extendedPropertyDefinition = propertyDefinition as ExtendedPropertyDefinition;
				if (!(extendedPropertyDefinition != null))
				{
					throw new NotSupportedException(string.Format(Strings.OperationNotSupportedForPropertyDefinitionType, propertyDefinition.GetType().Name));
				}
				object obj;
				if (this.TryGetExtendedProperty<object>(extendedPropertyDefinition, out obj))
				{
					return obj;
				}
				throw new ServiceObjectPropertyException(Strings.MustLoadOrAssignPropertyBeforeAccess, propertyDefinition);
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003299C File Offset: 0x0003199C
		internal bool TryGetExtendedProperty<T>(ExtendedPropertyDefinition propertyDefinition, out T propertyValue)
		{
			ExtendedPropertyCollection extendedProperties = this.GetExtendedProperties();
			if (extendedProperties != null && extendedProperties.TryGetValue<T>(propertyDefinition, out propertyValue))
			{
				return true;
			}
			propertyValue = default(T);
			return false;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000329C7 File Offset: 0x000319C7
		public bool TryGetProperty(PropertyDefinitionBase propertyDefinition, out object propertyValue)
		{
			return this.TryGetProperty<object>(propertyDefinition, out propertyValue);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000329D4 File Offset: 0x000319D4
		public bool TryGetProperty<T>(PropertyDefinitionBase propertyDefinition, out T propertyValue)
		{
			PropertyDefinition propertyDefinition2 = propertyDefinition as PropertyDefinition;
			if (propertyDefinition2 != null)
			{
				return this.PropertyBag.TryGetProperty<T>(propertyDefinition2, out propertyValue);
			}
			ExtendedPropertyDefinition extendedPropertyDefinition = propertyDefinition as ExtendedPropertyDefinition;
			if (extendedPropertyDefinition != null)
			{
				return this.TryGetExtendedProperty<T>(extendedPropertyDefinition, out propertyValue);
			}
			throw new NotSupportedException(string.Format(Strings.OperationNotSupportedForPropertyDefinitionType, propertyDefinition.GetType().Name));
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00032A34 File Offset: 0x00031A34
		public Collection<PropertyDefinitionBase> GetLoadedPropertyDefinitions()
		{
			Collection<PropertyDefinitionBase> collection = new Collection<PropertyDefinitionBase>();
			foreach (PropertyDefinition propertyDefinition in this.PropertyBag.Properties.Keys)
			{
				collection.Add(propertyDefinition);
			}
			if (this.GetExtendedProperties() != null)
			{
				foreach (ExtendedProperty extendedProperty in this.GetExtendedProperties())
				{
					collection.Add(extendedProperty.PropertyDefinition);
				}
			}
			return collection;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00032AE8 File Offset: 0x00031AE8
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00032AF0 File Offset: 0x00031AF0
		public ExchangeService Service
		{
			get
			{
				return this.service;
			}
			internal set
			{
				this.service = value;
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00032AF9 File Offset: 0x00031AF9
		internal virtual PropertyDefinition GetIdPropertyDefinition()
		{
			return null;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00032AFC File Offset: 0x00031AFC
		internal ServiceId GetId()
		{
			PropertyDefinition idPropertyDefinition = this.GetIdPropertyDefinition();
			object obj = null;
			if (idPropertyDefinition != null)
			{
				this.PropertyBag.TryGetValue(idPropertyDefinition, out obj);
			}
			return (ServiceId)obj;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00032B2C File Offset: 0x00031B2C
		public virtual bool IsNew
		{
			get
			{
				ServiceId id = this.GetId();
				return id == null || !id.IsValid;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00032B4E File Offset: 0x00031B4E
		public bool IsDirty
		{
			get
			{
				return this.PropertyBag.IsDirty;
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00032B5B File Offset: 0x00031B5B
		internal virtual ExtendedPropertyCollection GetExtendedProperties()
		{
			return null;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001169 RID: 4457 RVA: 0x00032B5E File Offset: 0x00031B5E
		// (remove) Token: 0x0600116A RID: 4458 RVA: 0x00032B77 File Offset: 0x00031B77
		internal event ServiceObjectChangedDelegate OnChange;

		// Token: 0x040009E4 RID: 2532
		private object lockObject = new object();

		// Token: 0x040009E5 RID: 2533
		private ExchangeService service;

		// Token: 0x040009E6 RID: 2534
		private PropertyBag propertyBag;

		// Token: 0x040009E7 RID: 2535
		private string xmlElementName;
	}
}
