using System;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A4 RID: 676
	[CannotApplyEqualityOperator]
	public struct VolatileRef<T> where T : class
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x0004018D File Offset: 0x0003E38D
		public VolatileRef(T value)
		{
			this.m_value = value;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00040198 File Offset: 0x0003E398
		public T VolatileRead()
		{
			return this.m_value;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004018D File Offset: 0x0003E38D
		public void VolatileWrite(T newValue)
		{
			this.m_value = newValue;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000401A2 File Offset: 0x0003E3A2
		public T InterlockedCompareExchange(T value, T comparand)
		{
			return Interlocked.CompareExchange<T>(ref this.m_value, value, comparand);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000401B1 File Offset: 0x0003E3B1
		public T InterlockedExchange(T value)
		{
			return Interlocked.Exchange<T>(ref this.m_value, value);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000401BF File Offset: 0x0003E3BF
		public static bool operator ==(VolatileRef<T> left, VolatileRef<T> right)
		{
			throw new InvalidOperationException("One must not compare VolatileRef<>");
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000401BF File Offset: 0x0003E3BF
		public static bool operator !=(VolatileRef<T> left, VolatileRef<T> right)
		{
			throw new InvalidOperationException("One must not compare VolatileRef<>");
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000401CB File Offset: 0x0003E3CB
		public override bool Equals(object obj)
		{
			throw new InvalidOperationException("One must never compare VolatileRef<>");
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000401D7 File Offset: 0x0003E3D7
		public override int GetHashCode()
		{
			throw new InvalidOperationException("One must never put VolatileRef<> in a container");
		}

		// Token: 0x040006CF RID: 1743
		private volatile T m_value;
	}
}
