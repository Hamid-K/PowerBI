using System;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x0200069A RID: 1690
	public interface IYielder<in T>
	{
		// Token: 0x0600244F RID: 9295
		IAwaitable Break();

		// Token: 0x06002450 RID: 9296
		IAwaitable Return(T value);
	}
}
