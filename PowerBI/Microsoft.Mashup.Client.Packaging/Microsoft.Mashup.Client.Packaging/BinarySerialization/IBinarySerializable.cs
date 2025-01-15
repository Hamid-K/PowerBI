using System;

namespace Microsoft.Mashup.Client.Packaging.BinarySerialization
{
	// Token: 0x0200001B RID: 27
	public interface IBinarySerializable
	{
		// Token: 0x060000BE RID: 190
		void Deserialize(BinarySerializationReader reader);

		// Token: 0x060000BF RID: 191
		void Serialize(BinarySerializationWriter writer);
	}
}
