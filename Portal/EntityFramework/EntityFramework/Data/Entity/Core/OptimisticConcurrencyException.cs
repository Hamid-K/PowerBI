using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002DD RID: 733
	[Serializable]
	public sealed class OptimisticConcurrencyException : UpdateException
	{
		// Token: 0x06002335 RID: 9013 RVA: 0x0006369E File Offset: 0x0006189E
		public OptimisticConcurrencyException()
		{
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000636A6 File Offset: 0x000618A6
		public OptimisticConcurrencyException(string message)
			: base(message)
		{
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000636AF File Offset: 0x000618AF
		public OptimisticConcurrencyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000636B9 File Offset: 0x000618B9
		public OptimisticConcurrencyException(string message, Exception innerException, IEnumerable<ObjectStateEntry> stateEntries)
			: base(message, innerException, stateEntries)
		{
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000636C4 File Offset: 0x000618C4
		private OptimisticConcurrencyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
