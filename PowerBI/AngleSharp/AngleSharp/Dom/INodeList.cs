using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000193 RID: 403
	[DomName("NodeList")]
	public interface INodeList : IEnumerable<INode>, IEnumerable, IMarkupFormattable
	{
		// Token: 0x170002E3 RID: 739
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		INode this[int index] { get; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000E83 RID: 3715
		[DomName("length")]
		int Length { get; }
	}
}
