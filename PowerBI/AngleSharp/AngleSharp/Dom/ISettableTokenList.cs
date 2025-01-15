using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200019A RID: 410
	[DomName("DOMSettableTokenList")]
	public interface ISettableTokenList : ITokenList, IEnumerable<string>, IEnumerable
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000EAA RID: 3754
		// (set) Token: 0x06000EAB RID: 3755
		[DomName("value")]
		string Value { get; set; }
	}
}
