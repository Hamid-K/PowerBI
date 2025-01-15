using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000E6 RID: 230
	public interface IRowSeekable : ISchematized
	{
		// Token: 0x060004C3 RID: 1219
		IRowSeeker GetSeeker(Func<int, bool> predicate);
	}
}
