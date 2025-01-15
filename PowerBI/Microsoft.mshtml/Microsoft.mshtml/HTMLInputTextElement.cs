using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D13 RID: 3347
	[CoClass(typeof(HTMLInputTextElementClass))]
	[Guid("3050F520-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLInputTextElement : DispIHTMLInputTextElement, HTMLInputTextElementEvents_Event
	{
	}
}
