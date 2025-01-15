using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001333 RID: 4915
	public interface IAccumulableChainingFunction
	{
		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x060081CD RID: 33229
		string EnumerableParameter { get; }

		// Token: 0x060081CE RID: 33230
		IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable);
	}
}
