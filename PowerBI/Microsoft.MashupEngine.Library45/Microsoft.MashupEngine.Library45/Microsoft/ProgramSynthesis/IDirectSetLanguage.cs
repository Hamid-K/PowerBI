using System;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x0200008B RID: 139
	public interface IDirectSetLanguage : ILanguage
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600030F RID: 783
		bool IsVariable { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000310 RID: 784
		bool IsInput { get; }
	}
}
