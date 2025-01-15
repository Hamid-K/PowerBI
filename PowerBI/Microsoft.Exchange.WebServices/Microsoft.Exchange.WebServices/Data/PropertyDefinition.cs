using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C4 RID: 708
	public abstract class PropertyDefinition : ServiceObjectPropertyDefinition
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x000449EA File Offset: 0x000439EA
		internal PropertyDefinition(string xmlElementName, string uri, ExchangeVersion version)
			: base(uri)
		{
			this.xmlElementName = xmlElementName;
			this.flags = PropertyDefinitionFlags.None;
			this.version = version;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00044A08 File Offset: 0x00043A08
		internal PropertyDefinition(string xmlElementName, PropertyDefinitionFlags flags, ExchangeVersion version)
		{
			this.xmlElementName = xmlElementName;
			this.flags = flags;
			this.version = version;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00044A25 File Offset: 0x00043A25
		internal PropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: this(xmlElementName, uri, version)
		{
			this.flags = flags;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00044A38 File Offset: 0x00043A38
		internal bool HasFlag(PropertyDefinitionFlags flag)
		{
			return this.HasFlag(flag, default(ExchangeVersion?));
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00044A55 File Offset: 0x00043A55
		internal virtual bool HasFlag(PropertyDefinitionFlags flag, ExchangeVersion? version)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00044A62 File Offset: 0x00043A62
		internal virtual void RegisterAssociatedInternalProperties(List<PropertyDefinition> properties)
		{
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00044A64 File Offset: 0x00043A64
		internal List<PropertyDefinition> GetAssociatedInternalProperties()
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			this.RegisterAssociatedInternalProperties(list);
			return list;
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x00044A7F File Offset: 0x00043A7F
		public override ExchangeVersion Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00044A87 File Offset: 0x00043A87
		internal virtual bool IsNullable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600193D RID: 6461
		internal abstract void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag);

		// Token: 0x0600193E RID: 6462
		internal abstract void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag);

		// Token: 0x0600193F RID: 6463
		internal abstract void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation);

		// Token: 0x06001940 RID: 6464
		internal abstract void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation);

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00044A8A File Offset: 0x00043A8A
		internal string XmlElementName
		{
			get
			{
				return this.xmlElementName;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00044A92 File Offset: 0x00043A92
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x00044AAC File Offset: 0x00043AAC
		public string Name
		{
			get
			{
				if (string.IsNullOrEmpty(this.name))
				{
					ServiceObjectSchema.InitializeSchemaPropertyNames();
				}
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00044AB5 File Offset: 0x00043AB5
		internal override string GetPrintableName()
		{
			return this.Name;
		}

		// Token: 0x040013EE RID: 5102
		private string xmlElementName;

		// Token: 0x040013EF RID: 5103
		private PropertyDefinitionFlags flags;

		// Token: 0x040013F0 RID: 5104
		private string name;

		// Token: 0x040013F1 RID: 5105
		private ExchangeVersion version;
	}
}
