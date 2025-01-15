using System;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200049F RID: 1183
	public interface IRenderableLiteral
	{
		// Token: 0x06001A91 RID: 6801
		string RenderHumanReadable();

		// Token: 0x06001A92 RID: 6802
		XElement RenderXML();
	}
}
