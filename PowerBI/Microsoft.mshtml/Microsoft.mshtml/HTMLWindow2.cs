using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007DB RID: 2011
	[CoClass(typeof(HTMLWindow2Class))]
	[Guid("3050F55D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLWindow2 : DispHTMLWindow2, HTMLWindowEvents_Event
	{
	}
}
