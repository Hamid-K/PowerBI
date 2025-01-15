using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F7 RID: 759
	[EditorBrowsable(1)]
	public abstract class PagedView : ViewBase
	{
		// Token: 0x06001AD3 RID: 6867 RVA: 0x000483FB File Offset: 0x000473FB
		internal override void InternalWriteViewToXml(EwsServiceXmlWriter writer)
		{
			base.InternalWriteViewToXml(writer);
			writer.WriteAttributeValue("Offset", this.Offset);
			writer.WriteAttributeValue("BasePoint", this.OffsetBasePoint);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00048430 File Offset: 0x00047430
		internal override void InternalWritePagingToJson(JsonObject jsonView, ExchangeService service)
		{
			base.InternalWritePagingToJson(jsonView, service);
			jsonView.Add("Offset", this.Offset);
			jsonView.Add("BasePoint", this.OffsetBasePoint);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00048461 File Offset: 0x00047461
		internal override int? GetMaxEntriesReturned()
		{
			return new int?(this.PageSize);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0004846E File Offset: 0x0004746E
		internal override void InternalWriteSearchSettingsToXml(EwsServiceXmlWriter writer, Grouping groupBy)
		{
			if (groupBy != null)
			{
				groupBy.WriteToXml(writer);
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0004847A File Offset: 0x0004747A
		internal override object WriteGroupingToJson(ExchangeService service, Grouping groupBy)
		{
			if (groupBy != null)
			{
				return ((IJsonSerializable)groupBy).ToJson(service);
			}
			return null;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00048488 File Offset: 0x00047488
		internal override void WriteOrderByToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0004848A File Offset: 0x0004748A
		internal override void InternalValidate(ServiceRequestBase request)
		{
			base.InternalValidate(request);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00048493 File Offset: 0x00047493
		internal PagedView(int pageSize)
		{
			this.PageSize = pageSize;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000484A2 File Offset: 0x000474A2
		internal PagedView(int pageSize, int offset)
			: this(pageSize)
		{
			this.Offset = offset;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000484B2 File Offset: 0x000474B2
		internal PagedView(int pageSize, int offset, OffsetBasePoint offsetBasePoint)
			: this(pageSize, offset)
		{
			this.OffsetBasePoint = offsetBasePoint;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x000484C3 File Offset: 0x000474C3
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x000484CB File Offset: 0x000474CB
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

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000484E8 File Offset: 0x000474E8
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x000484F0 File Offset: 0x000474F0
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

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x000484F9 File Offset: 0x000474F9
		// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x00048501 File Offset: 0x00047501
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				if (value >= 0)
				{
					this.offset = value;
					return;
				}
				throw new ArgumentException(Strings.OffsetMustBeGreaterThanZero);
			}
		}

		// Token: 0x0400142F RID: 5167
		private int pageSize;

		// Token: 0x04001430 RID: 5168
		private OffsetBasePoint offsetBasePoint;

		// Token: 0x04001431 RID: 5169
		private int offset;
	}
}
