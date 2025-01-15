using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002DA RID: 730
	[Serializable]
	public sealed class MappingException : EntityException
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x00063604 File Offset: 0x00061804
		public MappingException()
			: base(Strings.Mapping_General_Error)
		{
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00063611 File Offset: 0x00061811
		public MappingException(string message)
			: base(message)
		{
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0006361A File Offset: 0x0006181A
		public MappingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00063624 File Offset: 0x00061824
		private MappingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
