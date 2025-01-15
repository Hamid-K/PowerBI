using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000794 RID: 1940
	[Guid("3050F539-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLUnknownElementClass))]
	[ComImport]
	public interface HTMLUnknownElement : DispHTMLUnknownElement, HTMLElementEvents_Event
	{
	}
}
