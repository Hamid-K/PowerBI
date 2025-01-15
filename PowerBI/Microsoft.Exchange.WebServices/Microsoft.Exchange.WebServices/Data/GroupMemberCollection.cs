using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000067 RID: 103
	public sealed class GroupMemberCollection : ComplexPropertyCollection<GroupMember>, ICustomUpdateSerializer
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x0001178C File Offset: 0x0001078C
		public GroupMember Find(string key)
		{
			EwsUtilities.ValidateParam(key, "key");
			foreach (GroupMember groupMember in base.Items)
			{
				if (groupMember.Key == key)
				{
					return groupMember;
				}
			}
			return null;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000117F8 File Offset: 0x000107F8
		public void Clear()
		{
			base.InternalClear();
			this.collectionIsCleared = true;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00011808 File Offset: 0x00010808
		public void Add(GroupMember member)
		{
			EwsUtilities.ValidateParam(member, "member");
			EwsUtilities.Assert(member.Key == null, "GroupMemberCollection.Add", "member.Key is not null.");
			EwsUtilities.Assert(!base.Contains(member), "GroupMemberCollection.Add", "The member is already in the collection");
			base.InternalAdd(member);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011858 File Offset: 0x00010858
		public void AddRange(IEnumerable<GroupMember> members)
		{
			EwsUtilities.ValidateParam(members, "members");
			foreach (GroupMember groupMember in members)
			{
				this.Add(groupMember);
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000118AC File Offset: 0x000108AC
		public void AddContactGroup(ItemId contactGroupId)
		{
			this.Add(new GroupMember(contactGroupId));
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000118BA File Offset: 0x000108BA
		public void AddPersonalContact(ItemId contactId, string addressToLink)
		{
			this.Add(new GroupMember(contactId, addressToLink));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000118C9 File Offset: 0x000108C9
		public void AddPersonalContact(ItemId contactId)
		{
			this.AddPersonalContact(contactId, null);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000118D3 File Offset: 0x000108D3
		public void AddDirectoryUser(string smtpAddress)
		{
			this.AddDirectoryUser(smtpAddress, "SMTP");
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000118E1 File Offset: 0x000108E1
		public void AddDirectoryUser(string address, string routingType)
		{
			this.Add(new GroupMember(address, routingType, MailboxType.Mailbox));
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000118F1 File Offset: 0x000108F1
		public void AddDirectoryContact(string smtpAddress)
		{
			this.AddDirectoryContact(smtpAddress, "SMTP");
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000118FF File Offset: 0x000108FF
		public void AddDirectoryContact(string address, string routingType)
		{
			this.Add(new GroupMember(address, routingType, MailboxType.Contact));
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001190F File Offset: 0x0001090F
		public void AddPublicGroup(string smtpAddress)
		{
			this.Add(new GroupMember(smtpAddress, "SMTP", MailboxType.PublicGroup));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011923 File Offset: 0x00010923
		public void AddDirectoryPublicFolder(string smtpAddress)
		{
			this.Add(new GroupMember(smtpAddress, "SMTP", MailboxType.PublicFolder));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00011937 File Offset: 0x00010937
		public void AddOneOff(string displayName, string address, string routingType)
		{
			this.Add(new GroupMember(displayName, address, routingType));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00011947 File Offset: 0x00010947
		public void AddOneOff(string displayName, string smtpAddress)
		{
			this.AddOneOff(displayName, smtpAddress, "SMTP");
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00011956 File Offset: 0x00010956
		public void AddContactEmailAddress(Contact contact, EmailAddressKey emailAddressKey)
		{
			this.Add(new GroupMember(contact, emailAddressKey));
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00011965 File Offset: 0x00010965
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= base.Count)
			{
				throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
			}
			base.InternalRemoveAt(index);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00011990 File Offset: 0x00010990
		public bool Remove(GroupMember member)
		{
			return base.InternalRemove(member);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001199C File Offset: 0x0001099C
		bool ICustomUpdateSerializer.WriteSetUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ownerObject, PropertyDefinition propertyDefinition)
		{
			if (this.collectionIsCleared)
			{
				if (base.AddedItems.Count == 0)
				{
					this.WriteDeleteMembersCollectionToXml(writer);
				}
				else
				{
					this.WriteSetOrAppendMembersToXml(writer, base.AddedItems, true);
				}
			}
			else
			{
				this.WriteSetOrAppendMembersToXml(writer, base.AddedItems, false);
				this.WriteDeleteMembersToXml(writer, base.ModifiedItems);
				this.WriteSetOrAppendMembersToXml(writer, base.ModifiedItems, false);
				this.WriteDeleteMembersToXml(writer, base.RemovedItems);
			}
			return true;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00011A0E File Offset: 0x00010A0E
		bool ICustomUpdateSerializer.WriteSetUpdateToJson(ExchangeService service, ServiceObject ewsObject, PropertyDefinition propertyDefinition, List<JsonObject> updates)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00011A15 File Offset: 0x00010A15
		bool ICustomUpdateSerializer.WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject)
		{
			return false;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00011A18 File Offset: 0x00010A18
		bool ICustomUpdateSerializer.WriteDeleteUpdateToJson(ExchangeService service, ServiceObject ewsObject, List<JsonObject> updates)
		{
			return false;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00011A1B File Offset: 0x00010A1B
		internal override GroupMember CreateComplexProperty(string xmlElementName)
		{
			return new GroupMember();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00011A22 File Offset: 0x00010A22
		internal override GroupMember CreateDefaultComplexProperty()
		{
			return new GroupMember();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00011A29 File Offset: 0x00010A29
		internal override void ClearChangeLog()
		{
			base.ClearChangeLog();
			this.collectionIsCleared = false;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00011A38 File Offset: 0x00010A38
		internal override string GetCollectionItemXmlElementName(GroupMember member)
		{
			return "Member";
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00011A3F File Offset: 0x00010A3F
		private void WriteDeleteMembersCollectionToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "DeleteItemField");
			ContactGroupSchema.Members.WriteToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00011A60 File Offset: 0x00010A60
		private void WriteDeleteMembersToXml(EwsServiceXmlWriter writer, List<GroupMember> members)
		{
			if (members.Count != 0)
			{
				GroupMemberPropertyDefinition groupMemberPropertyDefinition = new GroupMemberPropertyDefinition();
				foreach (GroupMember groupMember in members)
				{
					writer.WriteStartElement(XmlNamespace.Types, "DeleteItemField");
					groupMemberPropertyDefinition.Key = groupMember.Key;
					groupMemberPropertyDefinition.WriteToXml(writer);
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00011ADC File Offset: 0x00010ADC
		private void WriteSetOrAppendMembersToXml(EwsServiceXmlWriter writer, List<GroupMember> members, bool setMode)
		{
			if (members.Count != 0)
			{
				writer.WriteStartElement(XmlNamespace.Types, setMode ? "SetItemField" : "AppendToItemField");
				ContactGroupSchema.Members.WriteToXml(writer);
				writer.WriteStartElement(XmlNamespace.Types, "DistributionList");
				writer.WriteStartElement(XmlNamespace.Types, "Members");
				foreach (GroupMember groupMember in members)
				{
					groupMember.WriteToXml(writer, "Member");
				}
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00011B88 File Offset: 0x00010B88
		internal override void InternalValidate()
		{
			base.InternalValidate();
			foreach (GroupMember groupMember in base.ModifiedItems)
			{
				if (string.IsNullOrEmpty(groupMember.Key))
				{
					throw new ServiceValidationException(Strings.ContactGroupMemberCannotBeUpdatedWithoutBeingLoadedFirst);
				}
			}
		}

		// Token: 0x040001AF RID: 431
		private bool collectionIsCleared;
	}
}
