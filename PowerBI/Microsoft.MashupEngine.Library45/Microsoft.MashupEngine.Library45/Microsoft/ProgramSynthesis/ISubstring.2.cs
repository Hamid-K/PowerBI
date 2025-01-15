using System;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x0200008E RID: 142
	public interface ISubstring<out TImplementor> : ISubstring where TImplementor : ISubstring<TImplementor>
	{
		// Token: 0x0600031E RID: 798
		TImplementor AbsoluteSlice(uint start, uint end);

		// Token: 0x0600031F RID: 799
		TImplementor RelativeSlice(uint start, uint end);
	}
}
