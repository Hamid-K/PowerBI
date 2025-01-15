using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F3 RID: 243
	internal abstract class CreateRequest<TServiceObject, TResponse> : MultiResponseServiceRequest<TResponse>, IJsonSerializable where TServiceObject : ServiceObject where TResponse : ServiceResponse
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x00028B89 File Offset: 0x00027B89
		protected CreateRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00028B93 File Offset: 0x00027B93
		internal override void Validate()
		{
			base.Validate();
			if (this.ParentFolderId != null)
			{
				this.ParentFolderId.Validate(base.Service.RequestedServerVersion);
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00028BB9 File Offset: 0x00027BB9
		internal override int GetExpectedResponseMessageCount()
		{
			return EwsUtilities.GetEnumeratedObjectCount(this.objects);
		}

		// Token: 0x06000C46 RID: 3142
		internal abstract string GetParentFolderXmlElementName();

		// Token: 0x06000C47 RID: 3143
		internal abstract string GetObjectCollectionXmlElementName();

		// Token: 0x06000C48 RID: 3144 RVA: 0x00028BC8 File Offset: 0x00027BC8
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.ParentFolderId != null)
			{
				writer.WriteStartElement(XmlNamespace.Messages, this.GetParentFolderXmlElementName());
				this.ParentFolderId.WriteToXml(writer);
				writer.WriteEndElement();
			}
			writer.WriteStartElement(XmlNamespace.Messages, this.GetObjectCollectionXmlElementName());
			foreach (TServiceObject tserviceObject in this.objects)
			{
				ServiceObject serviceObject = tserviceObject;
				serviceObject.WriteToXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00028C54 File Offset: 0x00027C54
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.ParentFolderId != null)
			{
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("BaseFolderId", this.ParentFolderId.InternalToJson(service));
				jsonObject.Add(this.GetParentFolderXmlElementName(), jsonObject2);
			}
			List<object> list = new List<object>();
			foreach (TServiceObject tserviceObject in this.objects)
			{
				ServiceObject serviceObject = tserviceObject;
				list.Add(serviceObject.ToJson(service, false));
			}
			jsonObject.Add(this.GetObjectCollectionXmlElementName(), list.ToArray());
			this.AddJsonProperties(jsonObject, service);
			return jsonObject;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00028D0C File Offset: 0x00027D0C
		internal virtual void AddJsonProperties(JsonObject jsonRequest, ExchangeService service)
		{
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00028D0E File Offset: 0x00027D0E
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x00028D16 File Offset: 0x00027D16
		internal IEnumerable<TServiceObject> Objects
		{
			get
			{
				return this.objects;
			}
			set
			{
				this.objects = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00028D1F File Offset: 0x00027D1F
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00028D27 File Offset: 0x00027D27
		public FolderId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
			set
			{
				this.parentFolderId = value;
			}
		}

		// Token: 0x040008C6 RID: 2246
		private FolderId parentFolderId;

		// Token: 0x040008C7 RID: 2247
		private IEnumerable<TServiceObject> objects;
	}
}
