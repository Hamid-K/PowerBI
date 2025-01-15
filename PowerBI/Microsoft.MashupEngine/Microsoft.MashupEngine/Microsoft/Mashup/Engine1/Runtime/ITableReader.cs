using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200135A RID: 4954
	internal interface ITableReader : IDisposable
	{
		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x06008234 RID: 33332
		int Columns { get; }

		// Token: 0x06008235 RID: 33333
		bool MoveNext();

		// Token: 0x17002326 RID: 8998
		Value this[int ordinal] { get; }
	}
}
