using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000141 RID: 321
	internal class SyncFolderHierarchyRequest : MultiResponseServiceRequest<SyncFolderHierarchyResponse>, IJsonSerializable
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x0002E261 File Offset: 0x0002D261
		internal SyncFolderHierarchyRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002E26B File Offset: 0x0002D26B
		internal override SyncFolderHierarchyResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new SyncFolderHierarchyResponse(this.PropertySet);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0002E278 File Offset: 0x0002D278
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0002E27B File Offset: 0x0002D27B
		internal override string GetXmlElementName()
		{
			return "SyncFolderHierarchy";
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002E282 File Offset: 0x0002D282
		internal override string GetResponseXmlElementName()
		{
			return "SyncFolderHierarchyResponse";
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0002E289 File Offset: 0x0002D289
		internal override string GetResponseMessageXmlElementName()
		{
			return "SyncFolderHierarchyResponseMessage";
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002E290 File Offset: 0x0002D290
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.PropertySet, "PropertySet");
			if (this.SyncFolderId != null)
			{
				this.SyncFolderId.Validate(base.Service.RequestedServerVersion);
			}
			this.PropertySet.ValidateForRequest(this, false);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002E2E0 File Offset: 0x0002D2E0
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.PropertySet.WriteToXml(writer, ServiceObjectType.Folder);
			if (this.SyncFolderId != null)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "SyncFolderId");
				this.SyncFolderId.WriteToXml(writer);
				writer.WriteEndElement();
			}
			writer.WriteElementValue(XmlNamespace.Messages, "SyncState", this.SyncState);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002E334 File Offset: 0x0002D334
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.propertySet.WriteGetShapeToJson(jsonObject, service, ServiceObjectType.Folder);
			if (this.SyncFolderId != null)
			{
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("BaseFolderId", this.SyncFolderId.InternalToJson(service));
				jsonObject.Add("SyncFolderId", jsonObject2);
			}
			jsonObject.Add("SyncState", this.SyncState);
			return jsonObject;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0002E398 File Offset: 0x0002D398
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0002E39B File Offset: 0x0002D39B
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x0002E3A3 File Offset: 0x0002D3A3
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0002E3AC File Offset: 0x0002D3AC
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x0002E3B4 File Offset: 0x0002D3B4
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0002E3BD File Offset: 0x0002D3BD
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x0002E3C5 File Offset: 0x0002D3C5
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

		// Token: 0x04000970 RID: 2416
		private PropertySet propertySet;

		// Token: 0x04000971 RID: 2417
		private FolderId syncFolderId;

		// Token: 0x04000972 RID: 2418
		private string syncState;
	}
}
