using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A3 RID: 675
	internal sealed class PrivilegedUserId
	{
		// Token: 0x060017D0 RID: 6096 RVA: 0x0004107D File Offset: 0x0004007D
		public PrivilegedUserId()
		{
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00041085 File Offset: 0x00040085
		public PrivilegedUserId(PrivilegedLogonType openType, ConnectingIdType idType, string id)
			: this()
		{
			this.logonType = openType;
			this.idType = idType;
			this.id = id;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000410A4 File Offset: 0x000400A4
		internal void WriteToXml(EwsServiceXmlWriter writer, ExchangeVersion requestedServerVersion)
		{
			if (string.IsNullOrEmpty(this.id))
			{
				throw new ArgumentException(Strings.IdPropertyMustBeSet);
			}
			writer.WriteStartElement(XmlNamespace.Types, "OpenAsAdminOrSystemService");
			writer.WriteAttributeString("LogonType", this.logonType.ToString());
			if (requestedServerVersion >= ExchangeVersion.Exchange2013 && this.budgetType != null)
			{
				writer.WriteAttributeString("BudgetType", ((int)this.budgetType.Value).ToString());
			}
			writer.WriteStartElement(XmlNamespace.Types, "ConnectingSID");
			writer.WriteElementValue(XmlNamespace.Types, this.idType.ToString(), this.id);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00041159 File Offset: 0x00040159
		// (set) Token: 0x060017D4 RID: 6100 RVA: 0x00041161 File Offset: 0x00040161
		public ConnectingIdType IdType
		{
			get
			{
				return this.idType;
			}
			set
			{
				this.idType = value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0004116A File Offset: 0x0004016A
		// (set) Token: 0x060017D6 RID: 6102 RVA: 0x00041172 File Offset: 0x00040172
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0004117B File Offset: 0x0004017B
		// (set) Token: 0x060017D8 RID: 6104 RVA: 0x00041183 File Offset: 0x00040183
		public PrivilegedLogonType LogonType
		{
			get
			{
				return this.logonType;
			}
			set
			{
				this.logonType = value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0004118C File Offset: 0x0004018C
		// (set) Token: 0x060017DA RID: 6106 RVA: 0x00041194 File Offset: 0x00040194
		public PrivilegedUserIdBudgetType? BudgetType
		{
			get
			{
				return this.budgetType;
			}
			set
			{
				this.budgetType = value;
			}
		}

		// Token: 0x04001382 RID: 4994
		private PrivilegedLogonType logonType;

		// Token: 0x04001383 RID: 4995
		private ConnectingIdType idType;

		// Token: 0x04001384 RID: 4996
		private string id;

		// Token: 0x04001385 RID: 4997
		private PrivilegedUserIdBudgetType? budgetType;
	}
}
