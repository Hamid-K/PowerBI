using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000007 RID: 7
	public sealed class AlternateMailbox
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000213D File Offset: 0x0000113D
		private AlternateMailbox()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00001148
		internal static AlternateMailbox LoadFromXml(EwsXmlReader reader)
		{
			AlternateMailbox alternateMailbox = new AlternateMailbox();
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600000c-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
						dictionary.Add("Type", 0);
						dictionary.Add("DisplayName", 1);
						dictionary.Add("LegacyDN", 2);
						dictionary.Add("Server", 3);
						dictionary.Add("SmtpAddress", 4);
						dictionary.Add("OwnerSmtpAddress", 5);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600000c-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600000c-1.TryGetValue(localName, ref num))
					{
						switch (num)
						{
						case 0:
							alternateMailbox.Type = reader.ReadElementValue<string>();
							break;
						case 1:
							alternateMailbox.DisplayName = reader.ReadElementValue<string>();
							break;
						case 2:
							alternateMailbox.LegacyDN = reader.ReadElementValue<string>();
							break;
						case 3:
							alternateMailbox.Server = reader.ReadElementValue<string>();
							break;
						case 4:
							alternateMailbox.SmtpAddress = reader.ReadElementValue<string>();
							break;
						case 5:
							alternateMailbox.OwnerSmtpAddress = reader.ReadElementValue<string>();
							break;
						}
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "AlternateMailbox"));
			return alternateMailbox;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000226E File Offset: 0x0000126E
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002276 File Offset: 0x00001276
		public string Type
		{
			get
			{
				return this.type;
			}
			internal set
			{
				this.type = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000227F File Offset: 0x0000127F
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002287 File Offset: 0x00001287
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			internal set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00001290
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002298 File Offset: 0x00001298
		public string LegacyDN
		{
			get
			{
				return this.legacyDN;
			}
			internal set
			{
				this.legacyDN = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022A1 File Offset: 0x000012A1
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022A9 File Offset: 0x000012A9
		public string Server
		{
			get
			{
				return this.server;
			}
			internal set
			{
				this.server = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022B2 File Offset: 0x000012B2
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000022BA File Offset: 0x000012BA
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			internal set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022C3 File Offset: 0x000012C3
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000022CB File Offset: 0x000012CB
		public string OwnerSmtpAddress
		{
			get
			{
				return this.ownerSmtpAddress;
			}
			internal set
			{
				this.ownerSmtpAddress = value;
			}
		}

		// Token: 0x04000005 RID: 5
		private string type;

		// Token: 0x04000006 RID: 6
		private string displayName;

		// Token: 0x04000007 RID: 7
		private string legacyDN;

		// Token: 0x04000008 RID: 8
		private string server;

		// Token: 0x04000009 RID: 9
		private string smtpAddress;

		// Token: 0x0400000A RID: 10
		private string ownerSmtpAddress;
	}
}
