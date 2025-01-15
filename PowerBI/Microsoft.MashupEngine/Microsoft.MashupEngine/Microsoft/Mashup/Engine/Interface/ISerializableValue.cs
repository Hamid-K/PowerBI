using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000084 RID: 132
	public interface ISerializableValue
	{
		// Token: 0x060001EB RID: 491
		byte[] GetBytes();

		// Token: 0x060001EC RID: 492
		IValue GetValue();
	}
}
