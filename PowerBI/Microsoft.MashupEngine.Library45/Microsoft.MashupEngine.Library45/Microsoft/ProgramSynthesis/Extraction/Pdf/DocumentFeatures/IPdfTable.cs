using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D26 RID: 3366
	[NullableContext(2)]
	public interface IPdfTable
	{
		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005642 RID: 22082
		int StartingPageIndex { get; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005643 RID: 22083
		int EndingPageIndex { get; }

		// Token: 0x06005644 RID: 22084
		[return: Nullable(new byte[] { 1, 2 })]
		string[,] GetTextTable();

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06005645 RID: 22085
		TableIdentity TableIdentity { get; }

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005646 RID: 22086
		string DisplayName { get; }

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005647 RID: 22087
		TableKind Kind { get; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005648 RID: 22088
		int Width { get; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06005649 RID: 22089
		int Height { get; }

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x0600564A RID: 22090
		int? RecognizedHeaderRowCount { get; }
	}
}
