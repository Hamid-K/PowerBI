using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002FD RID: 765
	public sealed class ItemView : PagedView
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x000489A9 File Offset: 0x000479A9
		internal override string GetViewXmlElementName()
		{
			return "IndexedPageItemView";
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000489B0 File Offset: 0x000479B0
		internal override string GetViewJsonTypeName()
		{
			return "IndexedPageView";
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000489B7 File Offset: 0x000479B7
		internal override ServiceObjectType GetServiceObjectType()
		{
			return ServiceObjectType.Item;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000489BA File Offset: 0x000479BA
		internal override void InternalValidate(ServiceRequestBase request)
		{
			base.InternalValidate(request);
			EwsUtilities.ValidateEnumVersionValue(this.traversal, request.Service.RequestedServerVersion);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000489DE File Offset: 0x000479DE
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Traversal", this.Traversal);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000489F6 File Offset: 0x000479F6
		internal override void InternalWriteSearchSettingsToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
			base.InternalWriteSearchSettingsToXml(writer, groupBy);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00048A00 File Offset: 0x00047A00
		internal override void WriteOrderByToXml(EwsServiceXmlWriter writer)
		{
			this.orderBy.WriteToXml(writer, "SortOrder");
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00048A14 File Offset: 0x00047A14
		internal override void AddJsonProperties(JsonObject jsonRequest, ExchangeService service)
		{
			jsonRequest.Add("Traversal", this.Traversal);
			object obj = ((IJsonSerializable)this.orderBy).ToJson(service);
			if (obj != null)
			{
				jsonRequest.Add("SortOrder", obj);
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00048A53 File Offset: 0x00047A53
		public ItemView(int pageSize)
			: base(pageSize)
		{
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00048A67 File Offset: 0x00047A67
		public ItemView(int pageSize, int offset)
			: base(pageSize, offset)
		{
			base.Offset = offset;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00048A83 File Offset: 0x00047A83
		public ItemView(int pageSize, int offset, OffsetBasePoint offsetBasePoint)
			: base(pageSize, offset, offsetBasePoint)
		{
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00048A99 File Offset: 0x00047A99
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x00048AA1 File Offset: 0x00047AA1
		public ItemTraversal Traversal
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

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x00048AAA File Offset: 0x00047AAA
		public OrderByCollection OrderBy
		{
			get
			{
				return this.orderBy;
			}
		}

		// Token: 0x04001440 RID: 5184
		private ItemTraversal traversal;

		// Token: 0x04001441 RID: 5185
		private OrderByCollection orderBy = new OrderByCollection();
	}
}
