using System;
using System.Runtime.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DDB RID: 7643
	[Serializable]
	public class FirewallException2 : RuntimeException
	{
		// Token: 0x0600BD62 RID: 48482 RVA: 0x00004DA2 File Offset: 0x00002FA2
		public FirewallException2(string message, Exception innerException = null)
			: base(message, innerException)
		{
		}

		// Token: 0x0600BD63 RID: 48483 RVA: 0x00002BC2 File Offset: 0x00000DC2
		protected FirewallException2(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
