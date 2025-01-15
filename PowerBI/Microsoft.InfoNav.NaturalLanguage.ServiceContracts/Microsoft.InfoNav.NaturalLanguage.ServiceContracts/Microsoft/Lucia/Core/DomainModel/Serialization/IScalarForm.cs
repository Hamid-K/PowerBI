using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001DB RID: 475
	internal interface IScalarForm<T>
	{
		// Token: 0x06000A61 RID: 2657
		bool TryGetScalarForm(out T value);

		// Token: 0x06000A62 RID: 2658
		void SetFromScalarForm(T value);
	}
}
