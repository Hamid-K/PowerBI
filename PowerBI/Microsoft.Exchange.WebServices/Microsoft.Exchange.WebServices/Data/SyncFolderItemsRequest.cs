using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000142 RID: 322
	internal class SyncFolderItemsRequest : MultiResponseServiceRequest<SyncFolderItemsResponse>, IJsonSerializable
	{
		// Token: 0x06000FA4 RID: 4004 RVA: 0x0002E3CE File Offset: 0x0002D3CE
		internal SyncFolderItemsRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002E3EB File Offset: 0x0002D3EB
		internal override SyncFolderItemsResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new SyncFolderItemsResponse(this.PropertySet);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002E3F8 File Offset: 0x0002D3F8
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002E3FB File Offset: 0x0002D3FB
		internal override string GetXmlElementName()
		{
			return "SyncFolderItems";
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002E402 File Offset: 0x0002D402
		internal override string GetResponseXmlElementName()
		{
			return "SyncFolderItemsResponse";
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0002E409 File Offset: 0x0002D409
		internal override string GetResponseMessageXmlElementName()
		{
			return "SyncFolderItemsResponseMessage";
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002E410 File Offset: 0x0002D410
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.PropertySet, "PropertySet");
			EwsUtilities.ValidateParam(this.SyncFolderId, "SyncFolderId");
			this.SyncFolderId.Validate(base.Service.RequestedServerVersion);
			if (base.Service.RequestedServerVersion < ExchangeVersion.Exchange2010 && this.syncScope != SyncFolderItemsScope.NormalItems)
			{
				throw new ServiceVersionException(string.Format(Strings.EnumValueIncompatibleWithRequestVersion, this.syncScope.ToString(), this.syncScope.GetType().Name, ExchangeVersion.Exchange2010));
			}
			this.PropertySet.ValidateForRequest(this, true);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0002E4BC File Offset: 0x0002D4BC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.PropertySet.WriteToXml(writer, ServiceObjectType.Item);
			writer.WriteStartElement(XmlNamespace.Messages, "SyncFolderId");
			this.SyncFolderId.WriteToXml(writer);
			writer.WriteEndElement();
			writer.WriteElementValue(XmlNamespace.Messages, "SyncState", this.SyncState);
			this.IgnoredItemIds.WriteToXml(writer, XmlNamespace.Messages, "Ignore");
			writer.WriteElementValue(XmlNamespace.Messages, "MaxChangesReturned", this.MaxChangesReturned);
			if (base.Service.RequestedServerVersion >= ExchangeVersion.Exchange2010)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "SyncScope", this.syncScope);
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0002E554 File Offset: 0x0002D554
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.propertySet.WriteGetShapeToJson(jsonObject, service, ServiceObjectType.Item);
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.Add("BaseFolderId", this.SyncFolderId.InternalToJson(service));
			jsonObject.Add("SyncFolderId", jsonObject2);
			jsonObject.Add("SyncState", this.SyncState);
			if (this.IgnoredItemIds.Count > 0)
			{
				jsonObject.Add("Ignore", this.IgnoredItemIds.InternalToJson(service));
			}
			jsonObject.Add("MaxChangesReturned", this.MaxChangesReturned);
			if (base.Service.RequestedServerVersion >= ExchangeVersion.Exchange2010)
			{
				jsonObject.Add("SyncScope", this.SyncScope);
			}
			return jsonObject;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0002E60A File Offset: 0x0002D60A
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0002E60D File Offset: 0x0002D60D
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x0002E615 File Offset: 0x0002D615
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002E61E File Offset: 0x0002D61E
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x0002E626 File Offset: 0x0002D626
		public FolderId SyncFolderId
		{
			get
			{
				return this.syncFolderId;
			}
			set
			{
				this.syncFolderId = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0002E62F File Offset: 0x0002D62F
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x0002E637 File Offset: 0x0002D637
		public SyncFolderItemsScope SyncScope
		{
			get
			{
				return this.syncScope;
			}
			set
			{
				this.syncScope = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0002E640 File Offset: 0x0002D640
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x0002E648 File Offset: 0x0002D648
		public string SyncState
		{
			get
			{
				return this.syncState;
			}
			set
			{
				this.syncState = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002E651 File Offset: 0x0002D651
		public ItemIdWrapperList IgnoredItemIds
		{
			get
			{
				return this.ignoredItemIds;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0002E659 File Offset: 0x0002D659
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0002E661 File Offset: 0x0002D661
		public int MaxChangesReturned
		{
			get
			{
				return this.maxChangesReturned;
			}
			set
			{
				if (value >= 1 && value <= 512)
				{
					this.maxChangesReturned = value;
					return;
				}
				throw new ArgumentException(Strings.MaxChangesMustBeBetween1And512);
			}
		}

		// Token: 0x04000973 RID: 2419
		private PropertySet propertySet;

		// Token: 0x04000974 RID: 2420
		private FolderId syncFolderId;

		// Token: 0x04000975 RID: 2421
		private SyncFolderItemsScope syncScope;

		// Token: 0x04000976 RID: 2422
		private string syncState;

		// Token: 0x04000977 RID: 2423
		private ItemIdWrapperList ignoredItemIds = new ItemIdWrapperList();

		// Token: 0x04000978 RID: 2424
		private int maxChangesReturned = 100;
	}
}
