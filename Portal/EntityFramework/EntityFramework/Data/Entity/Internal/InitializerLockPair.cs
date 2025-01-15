using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200011A RID: 282
	internal class InitializerLockPair : Tuple<Action<DbContext>, bool>
	{
		// Token: 0x06001377 RID: 4983 RVA: 0x00032B64 File Offset: 0x00030D64
		public InitializerLockPair(Action<DbContext> initializerDelegate, bool isLocked)
			: base(initializerDelegate, isLocked)
		{
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00032B6E File Offset: 0x00030D6E
		public Action<DbContext> InitializerDelegate
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x00032B76 File Offset: 0x00030D76
		public bool IsLocked
		{
			get
			{
				return base.Item2;
			}
		}
	}
}
