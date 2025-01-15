using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000F0 RID: 240
	public interface IListTypeValue : ITypeValue, IValue
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003AA RID: 938
		ITypeValue ItemType { get; }
	}
}
