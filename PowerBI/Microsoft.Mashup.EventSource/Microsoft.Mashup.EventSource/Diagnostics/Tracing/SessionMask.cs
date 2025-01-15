using System;
using Microsoft.Diagnostics.Contracts.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200001F RID: 31
	internal struct SessionMask
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00008B7A File Offset: 0x00006D7A
		public SessionMask(SessionMask m)
		{
			this.m_mask = m.m_mask;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008B88 File Offset: 0x00006D88
		public SessionMask(uint mask = 0U)
		{
			this.m_mask = mask & 15U;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008B94 File Offset: 0x00006D94
		public bool IsEqualOrSupersetOf(SessionMask m)
		{
			return (this.m_mask | m.m_mask) == this.m_mask;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00008BAB File Offset: 0x00006DAB
		public static SessionMask All
		{
			get
			{
				return new SessionMask(15U);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008BB4 File Offset: 0x00006DB4
		public static SessionMask FromId(int perEventSourceSessionId)
		{
			Contract.Assert((long)perEventSourceSessionId < 4L);
			return new SessionMask(1U << perEventSourceSessionId);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008BCC File Offset: 0x00006DCC
		public ulong ToEventKeywords()
		{
			return (ulong)this.m_mask << 44;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public static SessionMask FromEventKeywords(ulong m)
		{
			return new SessionMask((uint)(m >> 44));
		}

		// Token: 0x17000042 RID: 66
		public bool this[int perEventSourceSessionId]
		{
			get
			{
				Contract.Assert((long)perEventSourceSessionId < 4L);
				return ((ulong)this.m_mask & (ulong)(1L << (perEventSourceSessionId & 31))) > 0UL;
			}
			set
			{
				Contract.Assert((long)perEventSourceSessionId < 4L);
				if (value)
				{
					this.m_mask |= 1U << perEventSourceSessionId;
					return;
				}
				this.m_mask &= ~(1U << perEventSourceSessionId);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008C3C File Offset: 0x00006E3C
		public static SessionMask operator |(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask | m2.m_mask);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008C50 File Offset: 0x00006E50
		public static SessionMask operator &(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask & m2.m_mask);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008C64 File Offset: 0x00006E64
		public static SessionMask operator ^(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask ^ m2.m_mask);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008C78 File Offset: 0x00006E78
		public static SessionMask operator ~(SessionMask m)
		{
			return new SessionMask(15U & ~m.m_mask);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008C89 File Offset: 0x00006E89
		public static explicit operator ulong(SessionMask m)
		{
			return (ulong)m.m_mask;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008C92 File Offset: 0x00006E92
		public static explicit operator uint(SessionMask m)
		{
			return m.m_mask;
		}

		// Token: 0x04000087 RID: 135
		private uint m_mask;

		// Token: 0x04000088 RID: 136
		internal const int SHIFT_SESSION_TO_KEYWORD = 44;

		// Token: 0x04000089 RID: 137
		internal const uint MASK = 15U;

		// Token: 0x0400008A RID: 138
		internal const uint MAX = 4U;
	}
}
