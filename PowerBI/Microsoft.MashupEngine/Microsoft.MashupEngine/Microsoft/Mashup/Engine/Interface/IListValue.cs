using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E9 RID: 233
	public interface IListValue : IValue
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000386 RID: 902
		int Count { get; }

		// Token: 0x17000163 RID: 355
		IValue this[int index] { get; }

		// Token: 0x06000388 RID: 904
		IEnumerable<IValueReference2> GetEnumerable();

		// Token: 0x06000389 RID: 905
		IEnumerator<IValueReference2> GetEnumerator();
	}
}
