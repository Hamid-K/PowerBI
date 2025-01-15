using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009EA RID: 2538
	[Guid("3050F52D-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLPhraseElementClass))]
	[ComImport]
	public interface HTMLPhraseElement : DispHTMLPhraseElement, HTMLElementEvents_Event
	{
	}
}
