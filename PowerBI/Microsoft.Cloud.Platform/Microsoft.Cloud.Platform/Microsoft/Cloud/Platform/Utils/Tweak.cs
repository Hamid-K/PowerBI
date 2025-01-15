using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D6 RID: 726
	public abstract class Tweak
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x000437B0 File Offset: 0x000419B0
		internal Tweak(string name, string description, Action<Tweak> notifyChangesTo)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ArgumentNullException("description");
			}
			this.m_name = name;
			this.m_description = description;
			this.m_notifyChangesTo = notifyChangesTo;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x000437FE File Offset: 0x000419FE
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00043806 File Offset: 0x00041A06
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0004380E File Offset: 0x00041A0E
		public Action<Tweak> NotifyChangesTo
		{
			get
			{
				return this.m_notifyChangesTo;
			}
		}

		// Token: 0x0400074D RID: 1869
		private string m_name;

		// Token: 0x0400074E RID: 1870
		private string m_description;

		// Token: 0x0400074F RID: 1871
		private Action<Tweak> m_notifyChangesTo;
	}
}
