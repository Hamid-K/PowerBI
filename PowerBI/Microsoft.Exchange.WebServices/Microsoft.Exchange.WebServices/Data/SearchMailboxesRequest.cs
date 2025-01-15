using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000136 RID: 310
	internal sealed class SearchMailboxesRequest : MultiResponseServiceRequest<SearchMailboxesResponse>, IJsonSerializable, IDiscoveryVersionable
	{
		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002CE2E File Offset: 0x0002BE2E
		internal SearchMailboxesRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002CE4A File Offset: 0x0002BE4A
		internal override SearchMailboxesResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new SearchMailboxesResponse();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002CE51 File Offset: 0x0002BE51
		internal override string GetResponseXmlElementName()
		{
			return "SearchMailboxesResponse";
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002CE58 File Offset: 0x0002BE58
		internal override string GetResponseMessageXmlElementName()
		{
			return "SearchMailboxesResponseMessage";
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002CE5F File Offset: 0x0002BE5F
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002CE62 File Offset: 0x0002BE62
		internal override string GetXmlElementName()
		{
			return "SearchMailboxes";
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002CE6C File Offset: 0x0002BE6C
		internal override void Validate()
		{
			base.Validate();
			if (this.SearchQueries == null || this.SearchQueries.Count == 0)
			{
				throw new ServiceValidationException(Strings.MailboxQueriesParameterIsNotSpecified);
			}
			foreach (MailboxQuery mailboxQuery in this.SearchQueries)
			{
				if (mailboxQuery.MailboxSearchScopes == null || mailboxQuery.MailboxSearchScopes.Length == 0)
				{
					throw new ServiceValidationException(Strings.MailboxQueriesParameterIsNotSpecified);
				}
				foreach (MailboxSearchScope mailboxSearchScope in mailboxQuery.MailboxSearchScopes)
				{
					if (mailboxSearchScope.ExtendedAttributes != null && mailboxSearchScope.ExtendedAttributes.Count > 0 && !DiscoverySchemaChanges.SearchMailboxesExtendedData.IsCompatible(this))
					{
						throw new ServiceVersionException(string.Format(Strings.ClassIncompatibleWithRequestVersion, typeof(ExtendedAttribute).Name, DiscoverySchemaChanges.SearchMailboxesExtendedData.MinimumServerVersion));
					}
					if (mailboxSearchScope.SearchScopeType != MailboxSearchScopeType.LegacyExchangeDN && (!DiscoverySchemaChanges.SearchMailboxesExtendedData.IsCompatible(this) || !DiscoverySchemaChanges.SearchMailboxesAdditionalSearchScopes.IsCompatible(this)))
					{
						throw new ServiceVersionException(string.Format(Strings.EnumValueIncompatibleWithRequestVersion, mailboxSearchScope.SearchScopeType.ToString(), typeof(MailboxSearchScopeType).Name, DiscoverySchemaChanges.SearchMailboxesAdditionalSearchScopes.MinimumServerVersion));
					}
				}
			}
			if (!string.IsNullOrEmpty(this.SortByProperty))
			{
				PropertyDefinitionBase propertyDefinitionBase = null;
				try
				{
					propertyDefinitionBase = ServiceObjectSchema.FindPropertyDefinition(this.SortByProperty);
				}
				catch (KeyNotFoundException)
				{
				}
				if (propertyDefinitionBase == null)
				{
					throw new ServiceValidationException(string.Format(Strings.InvalidSortByPropertyForMailboxSearch, this.SortByProperty));
				}
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0002D050 File Offset: 0x0002C050
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			ServiceResponseCollection<SearchMailboxesResponse> serviceResponseCollection = new ServiceResponseCollection<SearchMailboxesResponse>();
			reader.ReadStartElement(XmlNamespace.Messages, "ResponseMessages");
			for (;;)
			{
				reader.Read();
				if (reader.IsEndElement(XmlNamespace.Messages, "ResponseMessages"))
				{
					break;
				}
				SearchMailboxesResponse searchMailboxesResponse = new SearchMailboxesResponse();
				searchMailboxesResponse.LoadFromXml(reader, this.GetResponseMessageXmlElementName());
				serviceResponseCollection.Add(searchMailboxesResponse);
			}
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "ResponseMessages");
			return serviceResponseCollection;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002D0AC File Offset: 0x0002C0AC
		internal override object ParseResponse(JsonObject jsonBody)
		{
			ServiceResponseCollection<SearchMailboxesResponse> serviceResponseCollection = new ServiceResponseCollection<SearchMailboxesResponse>();
			object[] array = jsonBody.ReadAsJsonObject("ResponseMessages").ReadAsArray("Items");
			foreach (object obj in array)
			{
				SearchMailboxesResponse searchMailboxesResponse = new SearchMailboxesResponse();
				searchMailboxesResponse.LoadFromJson(obj as JsonObject, base.Service);
				serviceResponseCollection.Add(searchMailboxesResponse);
			}
			return serviceResponseCollection;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002D114 File Offset: 0x0002C114
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "SearchQueries");
			foreach (MailboxQuery mailboxQuery in this.SearchQueries)
			{
				writer.WriteStartElement(XmlNamespace.Types, "MailboxQuery");
				writer.WriteElementValue(XmlNamespace.Types, "Query", mailboxQuery.Query);
				writer.WriteStartElement(XmlNamespace.Types, "MailboxSearchScopes");
				foreach (MailboxSearchScope mailboxSearchScope in mailboxQuery.MailboxSearchScopes)
				{
					if (mailboxSearchScope.SearchScopeType == MailboxSearchScopeType.LegacyExchangeDN || DiscoverySchemaChanges.SearchMailboxesAdditionalSearchScopes.IsCompatible(this))
					{
						writer.WriteStartElement(XmlNamespace.Types, "MailboxSearchScope");
						writer.WriteElementValue(XmlNamespace.Types, "Mailbox", mailboxSearchScope.Mailbox);
						writer.WriteElementValue(XmlNamespace.Types, "SearchScope", mailboxSearchScope.SearchScope);
						if (DiscoverySchemaChanges.SearchMailboxesExtendedData.IsCompatible(this))
						{
							writer.WriteStartElement(XmlNamespace.Types, "ExtendedAttributes");
							if (mailboxSearchScope.SearchScopeType != MailboxSearchScopeType.LegacyExchangeDN)
							{
								writer.WriteStartElement(XmlNamespace.Types, "ExtendedAttribute");
								writer.WriteElementValue(XmlNamespace.Types, "Name", "SearchScopeType");
								writer.WriteElementValue(XmlNamespace.Types, "Value", mailboxSearchScope.SearchScopeType);
								writer.WriteEndElement();
							}
							if (mailboxSearchScope.ExtendedAttributes != null && mailboxSearchScope.ExtendedAttributes.Count > 0)
							{
								foreach (ExtendedAttribute extendedAttribute in mailboxSearchScope.ExtendedAttributes)
								{
									writer.WriteStartElement(XmlNamespace.Types, "ExtendedAttribute");
									writer.WriteElementValue(XmlNamespace.Types, "Name", extendedAttribute.Name);
									writer.WriteElementValue(XmlNamespace.Types, "Value", extendedAttribute.Value);
									writer.WriteEndElement();
								}
							}
							writer.WriteEndElement();
						}
						writer.WriteEndElement();
					}
				}
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteElementValue(XmlNamespace.Messages, "ResultType", this.ResultType);
			if (this.PreviewItemResponseShape != null)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "PreviewItemResponseShape");
				writer.WriteElementValue(XmlNamespace.Types, "BaseShape", this.PreviewItemResponseShape.BaseShape);
				if (this.PreviewItemResponseShape.AdditionalProperties != null && this.PreviewItemResponseShape.AdditionalProperties.Length > 0)
				{
					writer.WriteStartElement(XmlNamespace.Types, "AdditionalProperties");
					foreach (ExtendedPropertyDefinition extendedPropertyDefinition in this.PreviewItemResponseShape.AdditionalProperties)
					{
						extendedPropertyDefinition.WriteToXml(writer);
					}
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			if (!string.IsNullOrEmpty(this.SortByProperty))
			{
				writer.WriteStartElement(XmlNamespace.Messages, "SortBy");
				writer.WriteAttributeValue("Order", this.SortOrder.ToString());
				writer.WriteStartElement(XmlNamespace.Types, "FieldURI");
				writer.WriteAttributeValue("FieldURI", this.sortByProperty);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			if (!string.IsNullOrEmpty(this.Language))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "Language", this.Language);
			}
			writer.WriteElementValue(XmlNamespace.Messages, "Deduplication", this.performDeduplication);
			if (this.PageSize > 0)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "PageSize", this.PageSize.ToString());
			}
			if (!string.IsNullOrEmpty(this.PageItemReference))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "PageItemReference", this.PageItemReference);
			}
			writer.WriteElementValue(XmlNamespace.Messages, "PageDirection", this.PageDirection.ToString());
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002D4CC File Offset: 0x0002C4CC
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002D4D0 File Offset: 0x0002C4D0
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0002D4E4 File Offset: 0x0002C4E4
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0002D4EC File Offset: 0x0002C4EC
		public List<MailboxQuery> SearchQueries
		{
			get
			{
				return this.searchQueries;
			}
			set
			{
				this.searchQueries = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0002D4F5 File Offset: 0x0002C4F5
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x0002D4FD File Offset: 0x0002C4FD
		public SearchResultType ResultType
		{
			get
			{
				return this.searchResultType;
			}
			set
			{
				this.searchResultType = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002D506 File Offset: 0x0002C506
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0002D50E File Offset: 0x0002C50E
		public PreviewItemResponseShape PreviewItemResponseShape
		{
			get
			{
				return this.previewItemResponseShape;
			}
			set
			{
				this.previewItemResponseShape = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002D517 File Offset: 0x0002C517
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0002D51F File Offset: 0x0002C51F
		public SortDirection SortOrder
		{
			get
			{
				return this.sortOrder;
			}
			set
			{
				this.sortOrder = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0002D528 File Offset: 0x0002C528
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x0002D530 File Offset: 0x0002C530
		public string SortByProperty
		{
			get
			{
				return this.sortByProperty;
			}
			set
			{
				this.sortByProperty = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0002D539 File Offset: 0x0002C539
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0002D541 File Offset: 0x0002C541
		public string Language { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0002D54A File Offset: 0x0002C54A
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0002D552 File Offset: 0x0002C552
		public bool PerformDeduplication
		{
			get
			{
				return this.performDeduplication;
			}
			set
			{
				this.performDeduplication = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0002D55B File Offset: 0x0002C55B
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0002D563 File Offset: 0x0002C563
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0002D56C File Offset: 0x0002C56C
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0002D574 File Offset: 0x0002C574
		public string PageItemReference
		{
			get
			{
				return this.pageItemReference;
			}
			set
			{
				this.pageItemReference = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0002D57D File Offset: 0x0002C57D
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x0002D585 File Offset: 0x0002C585
		public SearchPageDirection PageDirection
		{
			get
			{
				return this.pageDirection;
			}
			set
			{
				this.pageDirection = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0002D58E File Offset: 0x0002C58E
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x0002D596 File Offset: 0x0002C596
		long IDiscoveryVersionable.ServerVersion { get; set; }

		// Token: 0x0400094A RID: 2378
		private List<MailboxQuery> searchQueries = new List<MailboxQuery>();

		// Token: 0x0400094B RID: 2379
		private SearchResultType searchResultType = SearchResultType.PreviewOnly;

		// Token: 0x0400094C RID: 2380
		private SortDirection sortOrder;

		// Token: 0x0400094D RID: 2381
		private string sortByProperty;

		// Token: 0x0400094E RID: 2382
		private bool performDeduplication;

		// Token: 0x0400094F RID: 2383
		private int pageSize;

		// Token: 0x04000950 RID: 2384
		private string pageItemReference;

		// Token: 0x04000951 RID: 2385
		private SearchPageDirection pageDirection;

		// Token: 0x04000952 RID: 2386
		private PreviewItemResponseShape previewItemResponseShape;
	}
}
