using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x02000757 RID: 1879
	internal class EventAttribute
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x00071FBF File Offset: 0x000701BF
		public EventAttribute(string name, object value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x00071FD5 File Offset: 0x000701D5
		public string Name { get; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x00071FDD File Offset: 0x000701DD
		public object Value { get; }

		// Token: 0x0600282C RID: 10284 RVA: 0x00071FE5 File Offset: 0x000701E5
		public virtual XObject ToXML(Dictionary<object, int> identityCache)
		{
			if (this.Value != null)
			{
				return new XAttribute(this.Name, this.Value);
			}
			return null;
		}
	}
}
