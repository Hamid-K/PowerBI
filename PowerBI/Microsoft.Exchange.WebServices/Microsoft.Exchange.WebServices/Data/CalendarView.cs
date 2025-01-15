using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F3 RID: 755
	public sealed class CalendarView : ViewBase
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x00048121 File Offset: 0x00047121
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Traversal", this.Traversal);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00048139 File Offset: 0x00047139
		internal override void InternalWriteSearchSettingsToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0004813B File Offset: 0x0004713B
		internal override object WriteGroupingToJson(ExchangeService service, Grouping groupBy)
		{
			return null;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0004813E File Offset: 0x0004713E
		internal override void WriteOrderByToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00048140 File Offset: 0x00047140
		internal override void AddJsonProperties(JsonObject jsonRequest, ExchangeService service)
		{
			jsonRequest.Add("Traversal", this.Traversal);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00048158 File Offset: 0x00047158
		internal override ServiceObjectType GetServiceObjectType()
		{
			return ServiceObjectType.Item;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0004815B File Offset: 0x0004715B
		public CalendarView(DateTime startDate, DateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00048171 File Offset: 0x00047171
		public CalendarView(DateTime startDate, DateTime endDate, int maxItemsReturned)
			: this(startDate, endDate)
		{
			this.MaxItemsReturned = new int?(maxItemsReturned);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00048187 File Offset: 0x00047187
		internal override void InternalValidate(ServiceRequestBase request)
		{
			base.InternalValidate(request);
			if (this.endDate < this.StartDate)
			{
				throw new ServiceValidationException(Strings.EndDateMustBeGreaterThanStartDate);
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000481B3 File Offset: 0x000471B3
		internal override void InternalWriteViewToXml(EwsServiceXmlWriter writer)
		{
			base.InternalWriteViewToXml(writer);
			writer.WriteAttributeValue("StartDate", this.StartDate);
			writer.WriteAttributeValue("EndDate", this.EndDate);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000481E8 File Offset: 0x000471E8
		internal override void InternalWritePagingToJson(JsonObject jsonView, ExchangeService service)
		{
			base.InternalWritePagingToJson(jsonView, service);
			jsonView.Add("StartDate", this.StartDate);
			jsonView.Add("EndDate", this.EndDate);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0004821E File Offset: 0x0004721E
		internal override string GetViewXmlElementName()
		{
			return "CalendarView";
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00048225 File Offset: 0x00047225
		internal override string GetViewJsonTypeName()
		{
			return "CalendarPageView";
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0004822C File Offset: 0x0004722C
		internal override int? GetMaxEntriesReturned()
		{
			return this.MaxItemsReturned;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00048234 File Offset: 0x00047234
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0004823C File Offset: 0x0004723C
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00048245 File Offset: 0x00047245
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0004824D File Offset: 0x0004724D
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00048256 File Offset: 0x00047256
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0004825E File Offset: 0x0004725E
		public int? MaxItemsReturned
		{
			get
			{
				return this.maxItemsReturned;
			}
			set
			{
				if (value != null && value.Value <= 0)
				{
					throw new ArgumentException(Strings.ValueMustBeGreaterThanZero);
				}
				this.maxItemsReturned = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0004828A File Offset: 0x0004728A
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00048292 File Offset: 0x00047292
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

		// Token: 0x0400141E RID: 5150
		private ItemTraversal traversal;

		// Token: 0x0400141F RID: 5151
		private int? maxItemsReturned;

		// Token: 0x04001420 RID: 5152
		private DateTime startDate;

		// Token: 0x04001421 RID: 5153
		private DateTime endDate;
	}
}
