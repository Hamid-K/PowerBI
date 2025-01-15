using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000095 RID: 149
	public sealed class RuleActions : ComplexProperty
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x00016EFC File Offset: 0x00015EFC
		internal RuleActions()
		{
			this.assignCategories = new StringList();
			this.forwardAsAttachmentToRecipients = new EmailAddressCollection("Address");
			this.forwardToRecipients = new EmailAddressCollection("Address");
			this.redirectToRecipients = new EmailAddressCollection("Address");
			this.sendSMSAlertToRecipients = new Collection<MobilePhone>();
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00016F55 File Offset: 0x00015F55
		public StringList AssignCategories
		{
			get
			{
				return this.assignCategories;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00016F5D File Offset: 0x00015F5D
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00016F65 File Offset: 0x00015F65
		public FolderId CopyToFolder
		{
			get
			{
				return this.copyToFolder;
			}
			set
			{
				this.SetFieldValue<FolderId>(ref this.copyToFolder, value);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00016F74 File Offset: 0x00015F74
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00016F7C File Offset: 0x00015F7C
		public bool Delete
		{
			get
			{
				return this.delete;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.delete, value);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00016F8B File Offset: 0x00015F8B
		public EmailAddressCollection ForwardAsAttachmentToRecipients
		{
			get
			{
				return this.forwardAsAttachmentToRecipients;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00016F93 File Offset: 0x00015F93
		public EmailAddressCollection ForwardToRecipients
		{
			get
			{
				return this.forwardToRecipients;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00016F9B File Offset: 0x00015F9B
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00016FA3 File Offset: 0x00015FA3
		public Importance? MarkImportance
		{
			get
			{
				return this.markImportance;
			}
			set
			{
				this.SetFieldValue<Importance?>(ref this.markImportance, value);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00016FB2 File Offset: 0x00015FB2
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00016FBA File Offset: 0x00015FBA
		public bool MarkAsRead
		{
			get
			{
				return this.markAsRead;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.markAsRead, value);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00016FC9 File Offset: 0x00015FC9
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x00016FD1 File Offset: 0x00015FD1
		public FolderId MoveToFolder
		{
			get
			{
				return this.moveToFolder;
			}
			set
			{
				this.SetFieldValue<FolderId>(ref this.moveToFolder, value);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00016FE0 File Offset: 0x00015FE0
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x00016FE8 File Offset: 0x00015FE8
		public bool PermanentDelete
		{
			get
			{
				return this.permanentDelete;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.permanentDelete, value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00016FF7 File Offset: 0x00015FF7
		public EmailAddressCollection RedirectToRecipients
		{
			get
			{
				return this.redirectToRecipients;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00016FFF File Offset: 0x00015FFF
		public Collection<MobilePhone> SendSMSAlertToRecipients
		{
			get
			{
				return this.sendSMSAlertToRecipients;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00017007 File Offset: 0x00016007
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0001700F File Offset: 0x0001600F
		public ItemId ServerReplyWithMessage
		{
			get
			{
				return this.serverReplyWithMessage;
			}
			set
			{
				this.SetFieldValue<ItemId>(ref this.serverReplyWithMessage, value);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001701E File Offset: 0x0001601E
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00017026 File Offset: 0x00016026
		public bool StopProcessingRules
		{
			get
			{
				return this.stopProcessingRules;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.stopProcessingRules, value);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00017038 File Offset: 0x00016038
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000674-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(13);
					dictionary.Add("AssignCategories", 0);
					dictionary.Add("CopyToFolder", 1);
					dictionary.Add("Delete", 2);
					dictionary.Add("ForwardAsAttachmentToRecipients", 3);
					dictionary.Add("ForwardToRecipients", 4);
					dictionary.Add("MarkImportance", 5);
					dictionary.Add("MarkAsRead", 6);
					dictionary.Add("MoveToFolder", 7);
					dictionary.Add("PermanentDelete", 8);
					dictionary.Add("RedirectToRecipients", 9);
					dictionary.Add("SendSMSAlertToRecipients", 10);
					dictionary.Add("ServerReplyWithMessage", 11);
					dictionary.Add("StopProcessingRules", 12);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000674-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000674-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.assignCategories.LoadFromXml(reader, reader.LocalName);
						return true;
					case 1:
						reader.ReadStartElement(XmlNamespace.NotSpecified, "FolderId");
						this.copyToFolder = new FolderId();
						this.copyToFolder.LoadFromXml(reader, "FolderId");
						reader.ReadEndElement(XmlNamespace.NotSpecified, "CopyToFolder");
						return true;
					case 2:
						this.delete = reader.ReadElementValue<bool>();
						return true;
					case 3:
						this.forwardAsAttachmentToRecipients.LoadFromXml(reader, reader.LocalName);
						return true;
					case 4:
						this.forwardToRecipients.LoadFromXml(reader, reader.LocalName);
						return true;
					case 5:
						this.markImportance = new Importance?(reader.ReadElementValue<Importance>());
						return true;
					case 6:
						this.markAsRead = reader.ReadElementValue<bool>();
						return true;
					case 7:
						reader.ReadStartElement(XmlNamespace.NotSpecified, "FolderId");
						this.moveToFolder = new FolderId();
						this.moveToFolder.LoadFromXml(reader, "FolderId");
						reader.ReadEndElement(XmlNamespace.NotSpecified, "MoveToFolder");
						return true;
					case 8:
						this.permanentDelete = reader.ReadElementValue<bool>();
						return true;
					case 9:
						this.redirectToRecipients.LoadFromXml(reader, reader.LocalName);
						return true;
					case 10:
					{
						EmailAddressCollection emailAddressCollection = new EmailAddressCollection("Address");
						emailAddressCollection.LoadFromXml(reader, reader.LocalName);
						this.sendSMSAlertToRecipients = RuleActions.ConvertSMSRecipientsFromEmailAddressCollectionToMobilePhoneCollection(emailAddressCollection);
						return true;
					}
					case 11:
						this.serverReplyWithMessage = new ItemId();
						this.serverReplyWithMessage.LoadFromXml(reader, reader.LocalName);
						return true;
					case 12:
						this.stopProcessingRules = reader.ReadElementValue<bool>();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000172AC File Offset: 0x000162AC
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000675-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(13);
						dictionary.Add("AssignCategories", 0);
						dictionary.Add("CopyToFolder", 1);
						dictionary.Add("Delete", 2);
						dictionary.Add("ForwardAsAttachmentToRecipients", 3);
						dictionary.Add("ForwardToRecipients", 4);
						dictionary.Add("MarkImportance", 5);
						dictionary.Add("MarkAsRead", 6);
						dictionary.Add("MoveToFolder", 7);
						dictionary.Add("PermanentDelete", 8);
						dictionary.Add("RedirectToRecipients", 9);
						dictionary.Add("SendSMSAlertToRecipients", 10);
						dictionary.Add("ServerReplyWithMessage", 11);
						dictionary.Add("StopProcessingRules", 12);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000675-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000675-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.assignCategories.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 1:
							this.copyToFolder = new FolderId();
							this.copyToFolder.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 2:
							this.delete = jsonProperty.ReadAsBool(text);
							break;
						case 3:
							this.forwardAsAttachmentToRecipients.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 4:
							this.forwardToRecipients.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 5:
							this.markImportance = new Importance?(jsonProperty.ReadEnumValue<Importance>(text));
							break;
						case 6:
							this.markAsRead = jsonProperty.ReadAsBool(text);
							break;
						case 7:
							this.moveToFolder = new FolderId();
							this.moveToFolder.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 8:
							this.permanentDelete = jsonProperty.ReadAsBool(text);
							break;
						case 9:
							this.redirectToRecipients.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 10:
						{
							EmailAddressCollection emailAddressCollection = new EmailAddressCollection("Address");
							emailAddressCollection.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							this.sendSMSAlertToRecipients = RuleActions.ConvertSMSRecipientsFromEmailAddressCollectionToMobilePhoneCollection(emailAddressCollection);
							break;
						}
						case 11:
							this.serverReplyWithMessage = new ItemId();
							this.serverReplyWithMessage.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 12:
							this.stopProcessingRules = jsonProperty.ReadAsBool(text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00017560 File Offset: 0x00016560
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.AssignCategories.Count > 0)
			{
				this.AssignCategories.WriteToXml(writer, "AssignCategories");
			}
			if (this.CopyToFolder != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, "CopyToFolder");
				this.CopyToFolder.WriteToXml(writer);
				writer.WriteEndElement();
			}
			if (this.Delete)
			{
				writer.WriteElementValue(XmlNamespace.Types, "Delete", this.Delete);
			}
			if (this.ForwardAsAttachmentToRecipients.Count > 0)
			{
				this.ForwardAsAttachmentToRecipients.WriteToXml(writer, "ForwardAsAttachmentToRecipients");
			}
			if (this.ForwardToRecipients.Count > 0)
			{
				this.ForwardToRecipients.WriteToXml(writer, "ForwardToRecipients");
			}
			if (this.MarkImportance != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "MarkImportance", this.MarkImportance.Value);
			}
			if (this.MarkAsRead)
			{
				writer.WriteElementValue(XmlNamespace.Types, "MarkAsRead", this.MarkAsRead);
			}
			if (this.MoveToFolder != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, "MoveToFolder");
				this.MoveToFolder.WriteToXml(writer);
				writer.WriteEndElement();
			}
			if (this.PermanentDelete)
			{
				writer.WriteElementValue(XmlNamespace.Types, "PermanentDelete", this.PermanentDelete);
			}
			if (this.RedirectToRecipients.Count > 0)
			{
				this.RedirectToRecipients.WriteToXml(writer, "RedirectToRecipients");
			}
			if (this.SendSMSAlertToRecipients.Count > 0)
			{
				EmailAddressCollection emailAddressCollection = RuleActions.ConvertSMSRecipientsFromMobilePhoneCollectionToEmailAddressCollection(this.SendSMSAlertToRecipients);
				emailAddressCollection.WriteToXml(writer, "SendSMSAlertToRecipients");
			}
			if (this.ServerReplyWithMessage != null)
			{
				this.ServerReplyWithMessage.WriteToXml(writer, "ServerReplyWithMessage");
			}
			if (this.StopProcessingRules)
			{
				writer.WriteElementValue(XmlNamespace.Types, "StopProcessingRules", this.StopProcessingRules);
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00017720 File Offset: 0x00016720
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.AssignCategories.Count > 0)
			{
				jsonObject.Add("AssignCategories", this.AssignCategories.InternalToJson(service));
			}
			if (this.CopyToFolder != null)
			{
				jsonObject.Add("CopyToFolder", this.CopyToFolder.InternalToJson(service));
			}
			if (this.Delete)
			{
				jsonObject.Add("Delete", this.Delete);
			}
			if (this.ForwardAsAttachmentToRecipients.Count > 0)
			{
				jsonObject.Add("ForwardAsAttachmentToRecipients", this.ForwardAsAttachmentToRecipients.InternalToJson(service));
			}
			if (this.ForwardToRecipients.Count > 0)
			{
				jsonObject.Add("ForwardToRecipients", this.ForwardToRecipients.InternalToJson(service));
			}
			if (this.MarkImportance != null)
			{
				jsonObject.Add("MarkImportance", this.MarkImportance.Value);
			}
			if (this.MarkAsRead)
			{
				jsonObject.Add("MarkAsRead", this.MarkAsRead);
			}
			if (this.MoveToFolder != null)
			{
				jsonObject.Add("MoveToFolder", this.MoveToFolder.InternalToJson(service));
			}
			if (this.PermanentDelete)
			{
				jsonObject.Add("PermanentDelete", this.PermanentDelete);
			}
			if (this.RedirectToRecipients.Count > 0)
			{
				jsonObject.Add("RedirectToRecipients", this.RedirectToRecipients.InternalToJson(service));
			}
			if (this.SendSMSAlertToRecipients.Count > 0)
			{
				EmailAddressCollection emailAddressCollection = RuleActions.ConvertSMSRecipientsFromMobilePhoneCollectionToEmailAddressCollection(this.SendSMSAlertToRecipients);
				jsonObject.Add("SendSMSAlertToRecipients", emailAddressCollection.InternalToJson(service));
			}
			if (this.ServerReplyWithMessage != null)
			{
				jsonObject.Add("ServerReplyWithMessage", this.ServerReplyWithMessage.InternalToJson(service));
			}
			if (this.StopProcessingRules)
			{
				jsonObject.Add("StopProcessingRules", this.StopProcessingRules);
			}
			return jsonObject;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000178E4 File Offset: 0x000168E4
		internal override void InternalValidate()
		{
			base.InternalValidate();
			EwsUtilities.ValidateParam(this.forwardAsAttachmentToRecipients, "ForwardAsAttachmentToRecipients");
			EwsUtilities.ValidateParam(this.forwardToRecipients, "ForwardToRecipients");
			EwsUtilities.ValidateParam(this.redirectToRecipients, "RedirectToRecipients");
			foreach (MobilePhone mobilePhone in this.sendSMSAlertToRecipients)
			{
				EwsUtilities.ValidateParam(mobilePhone, "SendSMSAlertToRecipient");
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001796C File Offset: 0x0001696C
		private static Collection<MobilePhone> ConvertSMSRecipientsFromEmailAddressCollectionToMobilePhoneCollection(EmailAddressCollection emailCollection)
		{
			Collection<MobilePhone> collection = new Collection<MobilePhone>();
			foreach (EmailAddress emailAddress in emailCollection)
			{
				collection.Add(new MobilePhone(emailAddress.Name, emailAddress.Address));
			}
			return collection;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000179CC File Offset: 0x000169CC
		private static EmailAddressCollection ConvertSMSRecipientsFromMobilePhoneCollectionToEmailAddressCollection(Collection<MobilePhone> recipientCollection)
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection("Address");
			foreach (MobilePhone mobilePhone in recipientCollection)
			{
				EmailAddress emailAddress = new EmailAddress(mobilePhone.Name, mobilePhone.PhoneNumber, "MOBILE");
				emailAddressCollection.Add(emailAddress);
			}
			return emailAddressCollection;
		}

		// Token: 0x04000228 RID: 552
		private const string MobileType = "MOBILE";

		// Token: 0x04000229 RID: 553
		private StringList assignCategories;

		// Token: 0x0400022A RID: 554
		private FolderId copyToFolder;

		// Token: 0x0400022B RID: 555
		private bool delete;

		// Token: 0x0400022C RID: 556
		private EmailAddressCollection forwardAsAttachmentToRecipients;

		// Token: 0x0400022D RID: 557
		private EmailAddressCollection forwardToRecipients;

		// Token: 0x0400022E RID: 558
		private Importance? markImportance;

		// Token: 0x0400022F RID: 559
		private bool markAsRead;

		// Token: 0x04000230 RID: 560
		private FolderId moveToFolder;

		// Token: 0x04000231 RID: 561
		private bool permanentDelete;

		// Token: 0x04000232 RID: 562
		private EmailAddressCollection redirectToRecipients;

		// Token: 0x04000233 RID: 563
		private Collection<MobilePhone> sendSMSAlertToRecipients;

		// Token: 0x04000234 RID: 564
		private ItemId serverReplyWithMessage;

		// Token: 0x04000235 RID: 565
		private bool stopProcessingRules;
	}
}
