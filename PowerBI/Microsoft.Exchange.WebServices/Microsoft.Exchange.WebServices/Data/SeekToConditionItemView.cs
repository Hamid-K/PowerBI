using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002FF RID: 767
	public sealed class SeekToConditionItemView : ViewBase
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x00048D92 File Offset: 0x00047D92
		internal override ServiceObjectType GetServiceObjectType()
		{
			return this.serviceObjType;
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00048D9A File Offset: 0x00047D9A
		internal void SetServiceObjectType(ServiceObjectType objType)
		{
			this.serviceObjType = objType;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00048DA3 File Offset: 0x00047DA3
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			if (this.serviceObjType == ServiceObjectType.Item)
			{
				writer.WriteAttributeValue("Traversal", this.Traversal);
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00048DC4 File Offset: 0x00047DC4
		internal override string GetViewXmlElementName()
		{
			return "SeekToConditionPageItemView";
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00048DCB File Offset: 0x00047DCB
		internal override string GetViewJsonTypeName()
		{
			return "SeekToConditionPageView";
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00048DD2 File Offset: 0x00047DD2
		internal override void InternalValidate(ServiceRequestBase request)
		{
			base.InternalValidate(request);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00048DDC File Offset: 0x00047DDC
		internal override void InternalWriteViewToXml(EwsServiceXmlWriter writer)
		{
			base.InternalWriteViewToXml(writer);
			writer.WriteAttributeValue("BasePoint", this.OffsetBasePoint);
			if (this.Condition != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, "Condition");
				this.Condition.WriteToXml(writer);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00048E2C File Offset: 0x00047E2C
		internal override void InternalWritePagingToJson(JsonObject jsonView, ExchangeService service)
		{
			base.InternalWritePagingToJson(jsonView, service);
			jsonView.Add("BasePoint", this.OffsetBasePoint);
			if (this.Condition != null)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.Add("Item", this.Condition.InternalToJson(service));
				jsonView.Add("Condition", jsonObject);
			}
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00048E88 File Offset: 0x00047E88
		internal override void InternalWriteSearchSettingsToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
			if (groupBy != null)
			{
				groupBy.WriteToXml(writer);
			}
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00048E94 File Offset: 0x00047E94
		internal override object WriteGroupingToJson(ExchangeService service, Grouping groupBy)
		{
			if (groupBy != null)
			{
				return ((IJsonSerializable)groupBy).ToJson(service);
			}
			return null;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00048EA2 File Offset: 0x00047EA2
		internal override int? GetMaxEntriesReturned()
		{
			return new int?(this.PageSize);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00048EAF File Offset: 0x00047EAF
		internal override void WriteOrderByToXml(EwsServiceXmlWriter writer)
		{
			this.orderBy.WriteToXml(writer, "SortOrder");
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00048EC2 File Offset: 0x00047EC2
		internal override void AddJsonProperties(JsonObject jsonRequest, ExchangeService service)
		{
			if (this.serviceObjType == ServiceObjectType.Item)
			{
				jsonRequest.Add("Traversal", this.Traversal);
			}
			jsonRequest.Add("SortOrder", ((IJsonSerializable)this.orderBy).ToJson(service));
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00048EFA File Offset: 0x00047EFA
		internal override void WriteToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
			if (this.serviceObjType == ServiceObjectType.Item)
			{
				base.GetPropertySetOrDefault().WriteToXml(writer, this.GetServiceObjectType());
			}
			writer.WriteStartElement(XmlNamespace.Messages, this.GetViewXmlElementName());
			this.InternalWriteViewToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00048F31 File Offset: 0x00047F31
		public SeekToConditionItemView(SearchFilter condition, int pageSize)
		{
			this.Condition = condition;
			this.PageSize = pageSize;
			this.serviceObjType = ServiceObjectType.Item;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00048F59 File Offset: 0x00047F59
		public SeekToConditionItemView(SearchFilter condition, int pageSize, OffsetBasePoint offsetBasePoint)
			: this(condition, pageSize)
		{
			this.OffsetBasePoint = offsetBasePoint;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x00048F6A File Offset: 0x00047F6A
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x00048F72 File Offset: 0x00047F72
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException(Strings.ValueMustBeGreaterThanZero);
				}
				this.pageSize = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00048F8F File Offset: 0x00047F8F
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x00048F97 File Offset: 0x00047F97
		public OffsetBasePoint OffsetBasePoint
		{
			get
			{
				return this.offsetBasePoint;
			}
			set
			{
				this.offsetBasePoint = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x00048FA0 File Offset: 0x00047FA0
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x00048FA8 File Offset: 0x00047FA8
		public SearchFilter Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Condition");
				}
				this.condition = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x00048FBF File Offset: 0x00047FBF
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x00048FC7 File Offset: 0x00047FC7
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

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00048FD0 File Offset: 0x00047FD0
		public OrderByCollection OrderBy
		{
			get
			{
				return this.orderBy;
			}
		}

		// Token: 0x04001443 RID: 5187
		private int pageSize;

		// Token: 0x04001444 RID: 5188
		private ItemTraversal traversal;

		// Token: 0x04001445 RID: 5189
		private SearchFilter condition;

		// Token: 0x04001446 RID: 5190
		private OffsetBasePoint offsetBasePoint;

		// Token: 0x04001447 RID: 5191
		private OrderByCollection orderBy = new OrderByCollection();

		// Token: 0x04001448 RID: 5192
		private ServiceObjectType serviceObjType;
	}
}
