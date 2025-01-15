using System;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000B5 RID: 181
	public interface IType : IEquatable<IType>
	{
		// Token: 0x0600041F RID: 1055
		bool IsValidObject(ITypedValue obj);

		// Token: 0x06000420 RID: 1056
		bool IsAssignableFrom(IType other);
	}
}
