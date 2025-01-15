using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001C9B RID: 7323
	public interface IIndexableRow
	{
		// Token: 0x0600F79F RID: 63391
		bool TryGetString(int index, out string value);
	}
}
