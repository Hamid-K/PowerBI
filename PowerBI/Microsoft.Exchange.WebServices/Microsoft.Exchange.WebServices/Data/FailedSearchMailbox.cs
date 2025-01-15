using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000270 RID: 624
	public sealed class FailedSearchMailbox
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x0003D3CD File Offset: 0x0003C3CD
		public FailedSearchMailbox(string mailbox, int errorCode, string errorMessage)
			: this(mailbox, errorCode, errorMessage, false)
		{
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0003D3D9 File Offset: 0x0003C3D9
		public FailedSearchMailbox(string mailbox, int errorCode, string errorMessage, bool isArchive)
		{
			this.Mailbox = mailbox;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
			this.IsArchive = isArchive;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0003D3FE File Offset: 0x0003C3FE
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x0003D406 File Offset: 0x0003C406
		public string Mailbox { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0003D40F File Offset: 0x0003C40F
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x0003D417 File Offset: 0x0003C417
		public int ErrorCode { get; set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0003D420 File Offset: 0x0003C420
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0003D428 File Offset: 0x0003C428
		public string ErrorMessage { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0003D431 File Offset: 0x0003C431
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0003D439 File Offset: 0x0003C439
		public bool IsArchive { get; set; }

		// Token: 0x060015FA RID: 5626 RVA: 0x0003D444 File Offset: 0x0003C444
		internal static FailedSearchMailbox[] LoadFailedMailboxesXml(XmlNamespace rootXmlNamespace, EwsServiceXmlReader reader)
		{
			List<FailedSearchMailbox> list = new List<FailedSearchMailbox>();
			reader.EnsureCurrentNodeIsStartElement(rootXmlNamespace, "FailedMailboxes");
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "FailedMailbox"))
				{
					string text = reader.ReadElementValue(XmlNamespace.Types, "Mailbox");
					int num = 0;
					int.TryParse(reader.ReadElementValue(XmlNamespace.Types, "ErrorCode"), ref num);
					string text2 = reader.ReadElementValue(XmlNamespace.Types, "ErrorMessage");
					bool flag = false;
					bool.TryParse(reader.ReadElementValue(XmlNamespace.Types, "IsArchive"), ref flag);
					list.Add(new FailedSearchMailbox(text, num, text2, flag));
				}
			}
			while (!reader.IsEndElement(rootXmlNamespace, "FailedMailboxes"));
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}
	}
}
