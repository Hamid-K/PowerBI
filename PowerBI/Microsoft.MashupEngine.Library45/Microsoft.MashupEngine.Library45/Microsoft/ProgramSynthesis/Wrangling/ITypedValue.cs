using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000B4 RID: 180
	public interface ITypedValue
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600041C RID: 1052
		string Value { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600041D RID: 1053
		IType Type { get; }

		// Token: 0x0600041E RID: 1054
		Optional<T> GetTypedValue<T>();
	}
}
