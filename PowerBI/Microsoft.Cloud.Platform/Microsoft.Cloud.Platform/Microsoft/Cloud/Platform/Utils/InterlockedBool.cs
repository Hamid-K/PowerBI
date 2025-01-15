using System;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A2 RID: 674
	[CannotApplyEqualityOperator]
	public struct InterlockedBool
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x000400EC File Offset: 0x0003E2EC
		public InterlockedBool(bool value)
		{
			this.m_value = (value ? 1 : 0);
			Interlocked.Exchange(ref this.m_value, this.m_value);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004010D File Offset: 0x0003E30D
		public bool InterlockedRead()
		{
			return Interlocked.CompareExchange(ref this.m_value, 0, 0) != 0;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004011F File Offset: 0x0003E31F
		public bool InterlockedWrite(bool newValue)
		{
			return Interlocked.Exchange(ref this.m_value, newValue ? 1 : 0) == 1;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00040139 File Offset: 0x0003E339
		public static bool operator ==(InterlockedBool left, InterlockedBool right)
		{
			throw new InvalidOperationException("One must never compare InterlockBool");
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00040139 File Offset: 0x0003E339
		public static bool operator !=(InterlockedBool left, InterlockedBool right)
		{
			throw new InvalidOperationException("One must never compare InterlockBool");
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00040139 File Offset: 0x0003E339
		public override bool Equals(object obj)
		{
			throw new InvalidOperationException("One must never compare InterlockBool");
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00040145 File Offset: 0x0003E345
		public override int GetHashCode()
		{
			throw new InvalidOperationException("One must never put InterlockBool in a container");
		}

		// Token: 0x040006CE RID: 1742
		private int m_value;
	}
}
