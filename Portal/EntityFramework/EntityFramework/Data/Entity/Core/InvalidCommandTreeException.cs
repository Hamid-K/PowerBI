using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D9 RID: 729
	[Serializable]
	public sealed class InvalidCommandTreeException : DataException
	{
		// Token: 0x06002325 RID: 8997 RVA: 0x000635DA File Offset: 0x000617DA
		public InvalidCommandTreeException()
			: base(Strings.Cqt_Exceptions_InvalidCommandTree)
		{
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000635E7 File Offset: 0x000617E7
		public InvalidCommandTreeException(string message)
			: base(message)
		{
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000635F0 File Offset: 0x000617F0
		public InvalidCommandTreeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000635FA File Offset: 0x000617FA
		private InvalidCommandTreeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
