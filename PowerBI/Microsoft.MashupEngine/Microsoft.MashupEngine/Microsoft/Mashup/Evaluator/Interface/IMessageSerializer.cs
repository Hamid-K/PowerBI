using System;
using System.IO;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E0E RID: 7694
	public interface IMessageSerializer
	{
		// Token: 0x0600BDDA RID: 48602
		void Serialize(BinaryWriter writer, Message message);

		// Token: 0x0600BDDB RID: 48603
		Message Deserialize(BinaryReader reader);
	}
}
