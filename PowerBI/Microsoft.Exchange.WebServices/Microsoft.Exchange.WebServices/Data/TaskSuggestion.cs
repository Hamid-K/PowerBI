using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200008B RID: 139
	public sealed class TaskSuggestion : ExtractedEntity
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x000152EB File Offset: 0x000142EB
		internal TaskSuggestion()
		{
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x000152F3 File Offset: 0x000142F3
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x000152FB File Offset: 0x000142FB
		public string TaskString { get; internal set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00015304 File Offset: 0x00014304
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001530C File Offset: 0x0001430C
		public EmailUserEntityCollection Assignees { get; internal set; }

		// Token: 0x0600063D RID: 1597 RVA: 0x00015318 File Offset: 0x00014318
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "TaskString")
				{
					this.TaskString = reader.ReadElementValue();
					return true;
				}
				if (localName == "Assignees")
				{
					this.Assignees = new EmailUserEntityCollection();
					this.Assignees.LoadFromXml(reader, XmlNamespace.Types, "Assignees");
					return true;
				}
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
