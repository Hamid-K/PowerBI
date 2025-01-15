using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils
{
	// Token: 0x020001B0 RID: 432
	public struct ValueAndCount<TValue> : IEquatable<ValueAndCount<TValue>> where TValue : IEquatable<TValue>
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001C137 File Offset: 0x0001A337
		public readonly TValue Value { get; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001C13F File Offset: 0x0001A33F
		public readonly int Count { get; }

		// Token: 0x06000991 RID: 2449 RVA: 0x0001C147 File Offset: 0x0001A347
		public ValueAndCount(TValue value, int count)
		{
			this.Value = value;
			this.Count = count;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001C158 File Offset: 0x0001A358
		public bool Equals(ValueAndCount<TValue> other)
		{
			TValue value = other.Value;
			return value.Equals(this.Value) && other.Count == this.Count;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001C193 File Offset: 0x0001A393
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((ValueAndCount<TValue>)obj);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		public override int GetHashCode()
		{
			int num = typeof(TValue).GetHashCode() * 13687;
			TValue value = this.Value;
			return ((num ^ value.GetHashCode()) * 15601) ^ this.Count.GetHashCode();
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001C214 File Offset: 0x0001A414
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} [{1}]", new object[] { this.Value, this.Count }));
		}
	}
}
