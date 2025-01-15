using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200079C RID: 1948
	[Guid("FECEAAA2-8405-11CF-8BA1-00AA00476DA6")]
	[CoClass(typeof(HTMLHistoryClass))]
	[ComImport]
	public interface HTMLHistory : IOmHistory
	{
	}
}
