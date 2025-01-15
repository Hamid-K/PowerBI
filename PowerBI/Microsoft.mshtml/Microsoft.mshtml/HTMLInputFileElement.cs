using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D5B RID: 3419
	[Guid("3050F542-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLInputFileElementClass))]
	[ComImport]
	public interface HTMLInputFileElement : DispIHTMLInputFileElement, HTMLInputFileElementEvents_Event
	{
	}
}
