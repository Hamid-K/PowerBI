using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001B0 RID: 432
	internal class XmlaScript
	{
		// Token: 0x06001A71 RID: 6769 RVA: 0x000AF5A1 File Offset: 0x000AD7A1
		internal XmlaScript()
		{
			this.Commands = new List<XmlaCommand>();
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x000AF5B4 File Offset: 0x000AD7B4
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x000AF5BC File Offset: 0x000AD7BC
		public IList<XmlaCommand> Commands { get; private set; }
	}
}
