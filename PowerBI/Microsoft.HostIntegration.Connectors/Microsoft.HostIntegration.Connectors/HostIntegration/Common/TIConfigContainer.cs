using System;
using System.Xml;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x02000506 RID: 1286
	public class TIConfigContainer
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x0009502D File Offset: 0x0009322D
		public TIConfigContainer(XmlNode Section)
		{
			this.section = Section;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x0009503C File Offset: 0x0009323C
		public XmlNode Section
		{
			get
			{
				return this.section;
			}
		}

		// Token: 0x04001B73 RID: 7027
		private XmlNode section;
	}
}
