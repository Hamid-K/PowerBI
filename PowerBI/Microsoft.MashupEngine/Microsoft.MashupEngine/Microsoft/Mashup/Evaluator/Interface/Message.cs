using System;
using System.IO;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E36 RID: 7734
	public abstract class Message
	{
		// Token: 0x0600BE4F RID: 48719 RVA: 0x000020FD File Offset: 0x000002FD
		public Message()
		{
		}

		// Token: 0x17002ED2 RID: 11986
		// (get) Token: 0x0600BE50 RID: 48720 RVA: 0x00268051 File Offset: 0x00266251
		// (set) Token: 0x0600BE51 RID: 48721 RVA: 0x00268059 File Offset: 0x00266259
		public IMessageSerializer Serializer { get; set; }

		// Token: 0x0600BE52 RID: 48722
		public abstract void Serialize(BinaryWriter writer);

		// Token: 0x0600BE53 RID: 48723
		public abstract void Deserialize(BinaryReader reader);

		// Token: 0x0600BE54 RID: 48724
		public abstract void Prepare();

		// Token: 0x0600BE55 RID: 48725
		public abstract void WriteTo(BinaryWriter writer);
	}
}
