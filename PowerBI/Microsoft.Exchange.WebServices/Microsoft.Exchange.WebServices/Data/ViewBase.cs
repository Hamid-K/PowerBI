using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F2 RID: 754
	[EditorBrowsable(1)]
	public abstract class ViewBase
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x00047FF4 File Offset: 0x00046FF4
		internal ViewBase()
		{
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00047FFC File Offset: 0x00046FFC
		internal virtual void InternalValidate(ServiceRequestBase request)
		{
			if (this.PropertySet != null)
			{
				this.PropertySet.InternalValidate();
				this.PropertySet.ValidateForRequest(request, true);
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00048020 File Offset: 0x00047020
		internal virtual void InternalWriteViewToXml(EwsServiceXmlWriter writer)
		{
			int? maxEntriesReturned = this.GetMaxEntriesReturned();
			if (maxEntriesReturned != null)
			{
				writer.WriteAttributeValue("MaxEntriesReturned", maxEntriesReturned.Value);
			}
		}

		// Token: 0x06001A8F RID: 6799
		internal abstract void InternalWriteSearchSettingsToXml(EwsServiceXmlWriter writer, Grouping groupBy);

		// Token: 0x06001A90 RID: 6800
		internal abstract void WriteOrderByToXml(EwsServiceXmlWriter writer);

		// Token: 0x06001A91 RID: 6801
		internal abstract string GetViewXmlElementName();

		// Token: 0x06001A92 RID: 6802 RVA: 0x00048054 File Offset: 0x00047054
		internal virtual string GetViewJsonTypeName()
		{
			return this.GetViewXmlElementName();
		}

		// Token: 0x06001A93 RID: 6803
		internal abstract int? GetMaxEntriesReturned();

		// Token: 0x06001A94 RID: 6804
		internal abstract ServiceObjectType GetServiceObjectType();

		// Token: 0x06001A95 RID: 6805
		internal abstract void WriteAttributesToXml(EwsServiceXmlWriter writer);

		// Token: 0x06001A96 RID: 6806
		internal abstract void AddJsonProperties(JsonObject jsonRequest, ExchangeService service);

		// Token: 0x06001A97 RID: 6807 RVA: 0x0004805C File Offset: 0x0004705C
		internal virtual void WriteToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
			this.GetPropertySetOrDefault().WriteToXml(writer, this.GetServiceObjectType());
			writer.WriteStartElement(XmlNamespace.Messages, this.GetViewXmlElementName());
			this.InternalWriteViewToXml(writer);
			writer.WriteEndElement();
			this.InternalWriteSearchSettingsToXml(writer, groupBy);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00048092 File Offset: 0x00047092
		internal void WriteShapeToJson(JsonObject jsonRequest, ExchangeService service)
		{
			this.GetPropertySetOrDefault().WriteGetShapeToJson(jsonRequest, service, this.GetServiceObjectType());
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000480A8 File Offset: 0x000470A8
		internal object WritePagingToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(this.GetViewJsonTypeName());
			this.InternalWritePagingToJson(jsonObject, service);
			return jsonObject;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000480D0 File Offset: 0x000470D0
		internal virtual void InternalWritePagingToJson(JsonObject jsonView, ExchangeService service)
		{
			int? maxEntriesReturned = this.GetMaxEntriesReturned();
			if (maxEntriesReturned != null)
			{
				jsonView.Add("MaxEntriesReturned", maxEntriesReturned.Value);
			}
		}

		// Token: 0x06001A9B RID: 6811
		internal abstract object WriteGroupingToJson(ExchangeService service, Grouping groupBy);

		// Token: 0x06001A9C RID: 6812 RVA: 0x000480FF File Offset: 0x000470FF
		internal PropertySet GetPropertySetOrDefault()
		{
			return this.PropertySet ?? PropertySet.FirstClassProperties;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00048110 File Offset: 0x00047110
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x00048118 File Offset: 0x00047118
		public PropertySet PropertySet
		{
			get
			{
				return this.propertySet;
			}
			set
			{
				this.propertySet = value;
			}
		}

		// Token: 0x0400141D RID: 5149
		private PropertySet propertySet;
	}
}
