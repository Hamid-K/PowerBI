using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200019D RID: 413
	[DomName("DOMStringMap")]
	public interface IStringMap : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x170002F9 RID: 761
		[DomAccessor(Accessors.Getter | Accessors.Setter)]
		string this[string name] { get; set; }

		// Token: 0x06000EB6 RID: 3766
		[DomAccessor(Accessors.Deleter)]
		void Remove(string name);
	}
}
