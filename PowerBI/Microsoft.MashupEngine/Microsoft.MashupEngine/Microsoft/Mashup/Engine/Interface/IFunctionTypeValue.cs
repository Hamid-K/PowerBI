using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000F3 RID: 243
	public interface IFunctionTypeValue : ITypeValue, IValue
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003AE RID: 942
		int ParameterCount { get; }

		// Token: 0x060003AF RID: 943
		string ParameterName(int index);

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003B0 RID: 944
		int Min { get; }

		// Token: 0x060003B1 RID: 945
		ITypeValue ParameterType(int index);

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003B2 RID: 946
		ITypeValue ReturnType { get; }
	}
}
