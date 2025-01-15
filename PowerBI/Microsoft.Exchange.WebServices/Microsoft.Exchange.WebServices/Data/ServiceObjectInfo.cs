using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200018E RID: 398
	internal class ServiceObjectInfo
	{
		// Token: 0x06001173 RID: 4467 RVA: 0x00032B90 File Offset: 0x00031B90
		internal ServiceObjectInfo()
		{
			this.xmlElementNameToServiceObjectClassMap = new Dictionary<string, Type>();
			this.serviceObjectConstructorsWithServiceParam = new Dictionary<Type, CreateServiceObjectWithServiceParam>();
			this.serviceObjectConstructorsWithAttachmentParam = new Dictionary<Type, CreateServiceObjectWithAttachmentParam>();
			this.InitializeServiceObjectClassMap();
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00032CA0 File Offset: 0x00031CA0
		private void InitializeServiceObjectClassMap()
		{
			this.AddServiceObjectType("CalendarItem", typeof(Appointment), (ExchangeService srv) => new Appointment(srv), (ItemAttachment itemAttachment, bool isNew) => new Appointment(itemAttachment, isNew));
			this.AddServiceObjectType("CalendarFolder", typeof(CalendarFolder), (ExchangeService srv) => new CalendarFolder(srv), null);
			this.AddServiceObjectType("Contact", typeof(Contact), (ExchangeService srv) => new Contact(srv), (ItemAttachment itemAttachment, bool isNew) => new Contact(itemAttachment));
			this.AddServiceObjectType("ContactsFolder", typeof(ContactsFolder), (ExchangeService srv) => new ContactsFolder(srv), null);
			this.AddServiceObjectType("DistributionList", typeof(ContactGroup), (ExchangeService srv) => new ContactGroup(srv), (ItemAttachment itemAttachment, bool isNew) => new ContactGroup(itemAttachment));
			this.AddServiceObjectType("Conversation", typeof(Conversation), (ExchangeService srv) => new Conversation(srv), null);
			this.AddServiceObjectType("Message", typeof(EmailMessage), (ExchangeService srv) => new EmailMessage(srv), (ItemAttachment itemAttachment, bool isNew) => new EmailMessage(itemAttachment));
			this.AddServiceObjectType("Folder", typeof(Folder), (ExchangeService srv) => new Folder(srv), null);
			this.AddServiceObjectType("Item", typeof(Item), (ExchangeService srv) => new Item(srv), (ItemAttachment itemAttachment, bool isNew) => new Item(itemAttachment));
			this.AddServiceObjectType("MeetingCancellation", typeof(MeetingCancellation), (ExchangeService srv) => new MeetingCancellation(srv), (ItemAttachment itemAttachment, bool isNew) => new MeetingCancellation(itemAttachment));
			this.AddServiceObjectType("MeetingMessage", typeof(MeetingMessage), (ExchangeService srv) => new MeetingMessage(srv), (ItemAttachment itemAttachment, bool isNew) => new MeetingMessage(itemAttachment));
			this.AddServiceObjectType("MeetingRequest", typeof(MeetingRequest), (ExchangeService srv) => new MeetingRequest(srv), (ItemAttachment itemAttachment, bool isNew) => new MeetingRequest(itemAttachment));
			this.AddServiceObjectType("MeetingResponse", typeof(MeetingResponse), (ExchangeService srv) => new MeetingResponse(srv), (ItemAttachment itemAttachment, bool isNew) => new MeetingResponse(itemAttachment));
			this.AddServiceObjectType("PostItem", typeof(PostItem), (ExchangeService srv) => new PostItem(srv), (ItemAttachment itemAttachment, bool isNew) => new PostItem(itemAttachment));
			this.AddServiceObjectType("SearchFolder", typeof(SearchFolder), (ExchangeService srv) => new SearchFolder(srv), null);
			this.AddServiceObjectType("Task", typeof(Task), (ExchangeService srv) => new Task(srv), (ItemAttachment itemAttachment, bool isNew) => new Task(itemAttachment));
			this.AddServiceObjectType("TasksFolder", typeof(TasksFolder), (ExchangeService srv) => new TasksFolder(srv), null);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00033144 File Offset: 0x00032144
		private void AddServiceObjectType(string xmlElementName, Type type, CreateServiceObjectWithServiceParam createServiceObjectWithServiceParam, CreateServiceObjectWithAttachmentParam createServiceObjectWithAttachmentParam)
		{
			this.xmlElementNameToServiceObjectClassMap.Add(xmlElementName, type);
			this.serviceObjectConstructorsWithServiceParam.Add(type, createServiceObjectWithServiceParam);
			if (createServiceObjectWithAttachmentParam != null)
			{
				this.serviceObjectConstructorsWithAttachmentParam.Add(type, createServiceObjectWithAttachmentParam);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00033172 File Offset: 0x00032172
		internal Dictionary<string, Type> XmlElementNameToServiceObjectClassMap
		{
			get
			{
				return this.xmlElementNameToServiceObjectClassMap;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003317A File Offset: 0x0003217A
		internal Dictionary<Type, CreateServiceObjectWithServiceParam> ServiceObjectConstructorsWithServiceParam
		{
			get
			{
				return this.serviceObjectConstructorsWithServiceParam;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00033182 File Offset: 0x00032182
		internal Dictionary<Type, CreateServiceObjectWithAttachmentParam> ServiceObjectConstructorsWithAttachmentParam
		{
			get
			{
				return this.serviceObjectConstructorsWithAttachmentParam;
			}
		}

		// Token: 0x040009E9 RID: 2537
		private Dictionary<string, Type> xmlElementNameToServiceObjectClassMap;

		// Token: 0x040009EA RID: 2538
		private Dictionary<Type, CreateServiceObjectWithServiceParam> serviceObjectConstructorsWithServiceParam;

		// Token: 0x040009EB RID: 2539
		private Dictionary<Type, CreateServiceObjectWithAttachmentParam> serviceObjectConstructorsWithAttachmentParam;
	}
}
