using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200019C RID: 412
	[DomName("DOMStringList")]
	public interface IStringList : IEnumerable<string>, IEnumerable
	{
		// Token: 0x170002F7 RID: 759
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		string this[int index] { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000EB2 RID: 3762
		[DomName("length")]
		int Length { get; }

		// Token: 0x06000EB3 RID: 3763
		[DomName("contains")]
		bool Contains(string entry);
	}
}
