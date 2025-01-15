using System;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000B1 RID: 177
	public interface INumberedRow : IRow, IEquatable<IRow>
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000419 RID: 1049
		int RowNumber { get; }
	}
}
