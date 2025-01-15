using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000783 RID: 1923
	[Guid("3050F517-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLMetaElementClass))]
	[ComImport]
	public interface HTMLMetaElement : DispHTMLMetaElement, HTMLElementEvents_Event
	{
	}
}
