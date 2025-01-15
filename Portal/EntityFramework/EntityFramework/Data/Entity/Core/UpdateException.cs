using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002E0 RID: 736
	[Serializable]
	public class UpdateException : DataException
	{
		// Token: 0x06002346 RID: 9030 RVA: 0x00063785 File Offset: 0x00061985
		public UpdateException()
		{
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0006378D File Offset: 0x0006198D
		public UpdateException(string message)
			: base(message)
		{
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x00063796 File Offset: 0x00061996
		public UpdateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000637A0 File Offset: 0x000619A0
		public UpdateException(string message, Exception innerException, IEnumerable<ObjectStateEntry> stateEntries)
			: base(message, innerException)
		{
			List<ObjectStateEntry> list = new List<ObjectStateEntry>(stateEntries);
			this._stateEntries = new ReadOnlyCollection<ObjectStateEntry>(list);
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000637C8 File Offset: 0x000619C8
		public ReadOnlyCollection<ObjectStateEntry> StateEntries
		{
			get
			{
				return this._stateEntries;
			}
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000637D0 File Offset: 0x000619D0
		protected UpdateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000C0E RID: 3086
		[NonSerialized]
		private readonly ReadOnlyCollection<ObjectStateEntry> _stateEntries;
	}
}
