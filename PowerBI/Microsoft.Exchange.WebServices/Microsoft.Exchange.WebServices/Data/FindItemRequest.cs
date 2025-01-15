using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000107 RID: 263
	internal sealed class FindItemRequest<TItem> : FindRequest<FindItemResponse<TItem>> where TItem : Item
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x0002A15A File Offset: 0x0002915A
		internal FindItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002A164 File Offset: 0x00029164
		internal override Grouping GetGroupBy()
		{
			return this.GroupBy;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002A16C File Offset: 0x0002916C
		internal override FindItemResponse<TItem> CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new FindItemResponse<TItem>(this.GroupBy != null, base.View.GetPropertySetOrDefault());
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0002A18A File Offset: 0x0002918A
		internal override string GetXmlElementName()
		{
			return "FindItem";
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0002A191 File Offset: 0x00029191
		internal override string GetResponseXmlElementName()
		{
			return "FindItemResponse";
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002A198 File Offset: 0x00029198
		internal override string GetResponseMessageXmlElementName()
		{
			return "FindItemResponseMessage";
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002A19F File Offset: 0x0002919F
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002A1A2 File Offset: 0x000291A2
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x0002A1AA File Offset: 0x000291AA
		public Grouping GroupBy
		{
			get
			{
				return this.groupBy;
			}
			set
			{
				this.groupBy = value;
			}
		}

		// Token: 0x040008EB RID: 2283
		private Grouping groupBy;
	}
}
