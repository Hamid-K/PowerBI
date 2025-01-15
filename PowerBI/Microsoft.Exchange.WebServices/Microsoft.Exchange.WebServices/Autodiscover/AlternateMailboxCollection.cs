using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000008 RID: 8
	public sealed class AlternateMailboxCollection
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000022D4 File Offset: 0x000012D4
		internal AlternateMailboxCollection()
		{
			this.Entries = new List<AlternateMailbox>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022E8 File Offset: 0x000012E8
		internal static AlternateMailboxCollection LoadFromXml(EwsXmlReader reader)
		{
			AlternateMailboxCollection alternateMailboxCollection = new AlternateMailboxCollection();
			do
			{
				reader.Read();
				if (reader.NodeType == 1 && reader.LocalName == "AlternateMailbox")
				{
					alternateMailboxCollection.Entries.Add(AlternateMailbox.LoadFromXml(reader));
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "AlternateMailboxes"));
			return alternateMailboxCollection;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000233D File Offset: 0x0000133D
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002345 File Offset: 0x00001345
		public List<AlternateMailbox> Entries { get; private set; }
	}
}
