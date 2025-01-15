using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x02000067 RID: 103
	public sealed class SecretManagerTracer : TraceSourceBase<SecretManagerTracer>
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A90B File Offset: 0x00008B0B
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("SecretManager");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
