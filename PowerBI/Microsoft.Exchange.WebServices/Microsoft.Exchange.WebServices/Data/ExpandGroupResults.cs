using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000294 RID: 660
	public sealed class ExpandGroupResults : IEnumerable<EmailAddress>, IEnumerable
	{
		// Token: 0x06001746 RID: 5958 RVA: 0x0003F6D4 File Offset: 0x0003E6D4
		internal ExpandGroupResults()
		{
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0003F6E7 File Offset: 0x0003E6E7
		public int Count
		{
			get
			{
				return this.members.Count;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0003F6F4 File Offset: 0x0003E6F4
		public bool IncludesAllMembers
		{
			get
			{
				return this.includesAllMembers;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0003F6FC File Offset: 0x0003E6FC
		public Collection<EmailAddress> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0003F704 File Offset: 0x0003E704
		public IEnumerator<EmailAddress> GetEnumerator()
		{
			return this.members.GetEnumerator();
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0003F711 File Offset: 0x0003E711
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.members.GetEnumerator();
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0003F720 File Offset: 0x0003E720
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "DLExpansion");
			if (!reader.IsEmptyElement)
			{
				int num = reader.ReadAttributeValue<int>("TotalItemsInView");
				this.includesAllMembers = reader.ReadAttributeValue<bool>("IncludesLastItemInRange");
				for (int i = 0; i < num; i++)
				{
					EmailAddress emailAddress = new EmailAddress();
					reader.ReadStartElement(XmlNamespace.Types, "Mailbox");
					emailAddress.LoadFromXml(reader, "Mailbox");
					this.members.Add(emailAddress);
				}
				reader.ReadEndElement(XmlNamespace.Messages, "DLExpansion");
			}
		}

		// Token: 0x0400134E RID: 4942
		private bool includesAllMembers;

		// Token: 0x0400134F RID: 4943
		private Collection<EmailAddress> members = new Collection<EmailAddress>();
	}
}
