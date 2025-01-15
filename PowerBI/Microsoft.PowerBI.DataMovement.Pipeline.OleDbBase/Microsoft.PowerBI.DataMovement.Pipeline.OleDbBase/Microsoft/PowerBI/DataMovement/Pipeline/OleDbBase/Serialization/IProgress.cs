using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D9 RID: 217
	public interface IProgress
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003FA RID: 1018
		long Rows { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003FB RID: 1019
		long ExceptionRows { get; }
	}
}
