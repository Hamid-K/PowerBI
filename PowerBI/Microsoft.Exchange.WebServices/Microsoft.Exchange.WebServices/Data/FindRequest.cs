using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000105 RID: 261
	internal abstract class FindRequest<TResponse> : MultiResponseServiceRequest<TResponse>, IJsonSerializable where TResponse : ServiceResponse
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x00029DE8 File Offset: 0x00028DE8
		internal FindRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00029E00 File Offset: 0x00028E00
		internal override void Validate()
		{
			base.Validate();
			this.View.InternalValidate(this);
			if (!string.IsNullOrEmpty(this.queryString) && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2010)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "queryString", ExchangeVersion.Exchange2010));
			}
			if (this.ReturnHighlightTerms && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "returnHighlightTerms", ExchangeVersion.Exchange2013));
			}
			if (this.View is SeekToConditionItemView && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "SeekToConditionItemView", ExchangeVersion.Exchange2013));
			}
			if (!string.IsNullOrEmpty(this.queryString) && this.searchFilter != null)
			{
				throw new ServiceLocalException(Strings.BothSearchFilterAndQueryStringCannotBeSpecified);
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00029EF0 File Offset: 0x00028EF0
		internal override int GetExpectedResponseMessageCount()
		{
			return this.ParentFolderIds.Count;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00029EFD File Offset: 0x00028EFD
		internal virtual Grouping GetGroupBy()
		{
			return null;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00029F00 File Offset: 0x00028F00
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			this.View.WriteAttributesToXml(writer);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00029F18 File Offset: 0x00028F18
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.View.WriteToXml(writer, this.GetGroupBy());
			if (this.SearchFilter != null)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "Restriction");
				this.SearchFilter.WriteToXml(writer);
				writer.WriteEndElement();
			}
			this.View.WriteOrderByToXml(writer);
			this.ParentFolderIds.WriteToXml(writer, XmlNamespace.Messages, "ParentFolderIds");
			if (!string.IsNullOrEmpty(this.queryString))
			{
				writer.WriteStartElement(XmlNamespace.Messages, "QueryString");
				if (this.ReturnHighlightTerms)
				{
					writer.WriteAttributeString("ReturnHighlightTerms", this.ReturnHighlightTerms.ToString().ToLowerInvariant());
				}
				writer.WriteValue(this.queryString, "QueryString");
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00029FD4 File Offset: 0x00028FD4
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.View.WriteShapeToJson(jsonObject, service);
			jsonObject.Add("Paging", this.View.WritePagingToJson(service));
			object obj = this.View.WriteGroupingToJson(service, this.GetGroupBy());
			if (obj != null)
			{
				jsonObject.Add("Grouping", obj);
			}
			this.View.AddJsonProperties(jsonObject, service);
			if (this.SearchFilter != null)
			{
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("Item", this.SearchFilter.InternalToJson(service));
				jsonObject.Add("Restriction", jsonObject2);
			}
			jsonObject.Add("ParentFolderIds", this.ParentFolderIds.InternalToJson(service));
			if (!string.IsNullOrEmpty(this.queryString))
			{
				JsonObject jsonObject3 = new JsonObject();
				jsonObject3.Add("Value", this.QueryString);
				if (this.ReturnHighlightTerms)
				{
					jsonObject3.Add("ReturnHighlightTerms", this.ReturnHighlightTerms.ToString().ToLowerInvariant());
				}
				jsonObject.Add("QueryString", jsonObject3);
			}
			return jsonObject;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0002A0DA File Offset: 0x000290DA
		public FolderIdWrapperList ParentFolderIds
		{
			get
			{
				return this.parentFolderIds;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0002A0E2 File Offset: 0x000290E2
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x0002A0EA File Offset: 0x000290EA
		public SearchFilter SearchFilter
		{
			get
			{
				return this.searchFilter;
			}
			set
			{
				this.searchFilter = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0002A0F3 File Offset: 0x000290F3
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x0002A0FB File Offset: 0x000290FB
		public string QueryString
		{
			get
			{
				return this.queryString;
			}
			set
			{
				this.queryString = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0002A104 File Offset: 0x00029104
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x0002A10C File Offset: 0x0002910C
		internal bool ReturnHighlightTerms
		{
			get
			{
				return this.returnHighlightTerms;
			}
			set
			{
				this.returnHighlightTerms = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0002A115 File Offset: 0x00029115
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0002A11D File Offset: 0x0002911D
		public ViewBase View
		{
			get
			{
				return this.view;
			}
			set
			{
				this.view = value;
			}
		}

		// Token: 0x040008E6 RID: 2278
		private FolderIdWrapperList parentFolderIds = new FolderIdWrapperList();

		// Token: 0x040008E7 RID: 2279
		private SearchFilter searchFilter;

		// Token: 0x040008E8 RID: 2280
		private string queryString;

		// Token: 0x040008E9 RID: 2281
		private bool returnHighlightTerms;

		// Token: 0x040008EA RID: 2282
		private ViewBase view;
	}
}
