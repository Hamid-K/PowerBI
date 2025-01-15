using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000B6 RID: 182
	public interface IType<T> : IType, IEquatable<IType>
	{
		// Token: 0x06000421 RID: 1057
		Optional<T> GetTypedValue(ITypedValue obj);
	}
}
