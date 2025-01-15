using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200019F RID: 415
	[DomName("StyleSheetList")]
	public interface IStyleSheetList : IEnumerable<IStyleSheet>, IEnumerable
	{
		// Token: 0x17000300 RID: 768
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		IStyleSheet this[int index] { get; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000EBF RID: 3775
		[DomName("length")]
		int Length { get; }
	}
}
