using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F8 RID: 760
	public sealed class ConversationIndexedItemView : PagedView
	{
		// Token: 0x06001AE3 RID: 6883 RVA: 0x0004851E File Offset: 0x0004751E
		internal override ServiceObjectType GetServiceObjectType()
		{
			return ServiceObjectType.Conversation;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00048524 File Offset: 0x00047524
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			if (this.Traversal != null)
			{
				writer.WriteAttributeValue("Traversal", this.Traversal);
			}
			if (this.ViewFilter != null)
			{
				writer.WriteAttributeValue("ViewFilter", this.ViewFilter);
			}
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0004857D File Offset: 0x0004757D
		internal override string GetViewXmlElementName()
		{
			return "IndexedPageItemView";
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00048584 File Offset: 0x00047584
		internal override string GetViewJsonTypeName()
		{
			return "IndexedPageView";
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0004858C File Offset: 0x0004758C
		internal override void InternalValidate(ServiceRequestBase request)
		{
			base.InternalValidate(request);
			if (this.Traversal != null)
			{
				EwsUtilities.ValidateEnumVersionValue((Enum)this.traversal, request.Service.RequestedServerVersion);
			}
			if (this.ViewFilter != null)
			{
				EwsUtilities.ValidateEnumVersionValue((Enum)this.viewFilter, request.Service.RequestedServerVersion);
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00048600 File Offset: 0x00047600
		internal override void InternalWriteSearchSettingsToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
			base.InternalWriteSearchSettingsToXml(writer, groupBy);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0004860A File Offset: 0x0004760A
		internal override void WriteOrderByToXml(EwsServiceXmlWriter writer)
		{
			this.orderBy.WriteToXml(writer, "SortOrder");
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00048620 File Offset: 0x00047620
		internal override void AddJsonProperties(JsonObject jsonRequest, ExchangeService service)
		{
			jsonRequest.Add("SortOrder", ((IJsonSerializable)this.orderBy).ToJson(service));
			if (this.Traversal != null)
			{
				jsonRequest.Add("Traversal", (Enum)this.Traversal);
			}
			if (this.ViewFilter != null)
			{
				jsonRequest.Add("ViewFilter", (Enum)this.ViewFilter);
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0004869A File Offset: 0x0004769A
		internal override void WriteToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
			writer.WriteStartElement(XmlNamespace.Messages, this.GetViewXmlElementName());
			this.InternalWriteViewToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000486B6 File Offset: 0x000476B6
		public ConversationIndexedItemView(int pageSize)
			: base(pageSize)
		{
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000486CA File Offset: 0x000476CA
		public ConversationIndexedItemView(int pageSize, int offset)
			: base(pageSize, offset)
		{
			base.Offset = offset;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000486E6 File Offset: 0x000476E6
		public ConversationIndexedItemView(int pageSize, int offset, OffsetBasePoint offsetBasePoint)
			: base(pageSize, offset, offsetBasePoint)
		{
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000486FC File Offset: 0x000476FC
		public OrderByCollection OrderBy
		{
			get
			{
				return this.orderBy;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00048704 File Offset: 0x00047704
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x0004870C File Offset: 0x0004770C
		public ConversationQueryTraversal? Traversal
		{
			get
			{
				return this.traversal;
			}
			set
			{
				this.traversal = value;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00048715 File Offset: 0x00047715
		// (set) Token: 0x06001AF3 RID: 6899 RVA: 0x0004871D File Offset: 0x0004771D
		public ViewFilter? ViewFilter
		{
			get
			{
				return this.viewFilter;
			}
			set
			{
				this.viewFilter = value;
			}
		}

		// Token: 0x04001432 RID: 5170
		private OrderByCollection orderBy = new OrderByCollection();

		// Token: 0x04001433 RID: 5171
		private ConversationQueryTraversal? traversal;

		// Token: 0x04001434 RID: 5172
		private ViewFilter? viewFilter;
	}
}
