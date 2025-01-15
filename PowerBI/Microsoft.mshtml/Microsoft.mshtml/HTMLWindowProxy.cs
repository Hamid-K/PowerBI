using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007DE RID: 2014
	[Guid("3050F55E-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLWindowProxyClass))]
	[ComImport]
	public interface HTMLWindowProxy : DispHTMLWindowProxy, HTMLWindowEvents_Event
	{
	}
}
