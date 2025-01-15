using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000E7 RID: 231
	public interface IRowSeeker : IRow, ISchematized, ICounted, IDisposable
	{
		// Token: 0x060004C4 RID: 1220
		bool MoveTo(long rowIndex);
	}
}
