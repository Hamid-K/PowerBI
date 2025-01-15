using System;
using System.IO;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E38 RID: 7736
	public abstract class UnbufferedMessage : Message
	{
		// Token: 0x0600BE59 RID: 48729 RVA: 0x00268062 File Offset: 0x00266262
		public UnbufferedMessage()
		{
		}

		// Token: 0x0600BE5A RID: 48730 RVA: 0x0000336E File Offset: 0x0000156E
		public sealed override void Prepare()
		{
		}

		// Token: 0x0600BE5B RID: 48731 RVA: 0x00268092 File Offset: 0x00266292
		public sealed override void WriteTo(BinaryWriter writer)
		{
			this.Serialize(writer);
		}
	}
}
