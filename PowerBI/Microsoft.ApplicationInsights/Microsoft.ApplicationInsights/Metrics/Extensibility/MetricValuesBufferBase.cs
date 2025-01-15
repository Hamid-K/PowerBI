using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200004B RID: 75
	internal abstract class MetricValuesBufferBase<TValue>
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000CB70 File Offset: 0x0000AD70
		[SuppressMessage("Microsoft.Usage", "CA2214: Do not call overridable methods in constructors", Justification = "Call chain has been reviewed.")]
		public MetricValuesBufferBase(int capacity)
		{
			if (capacity < 1)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.values = new TValue[capacity];
			this.ResetValues(this.values);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.values.Length;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000CBBA File Offset: 0x0000ADBA
		public int NextFlushIndex
		{
			get
			{
				return this.nextFlushIndex;
			}
			set
			{
				this.nextFlushIndex = value;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000CBC5 File Offset: 0x0000ADC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int IncWriteIndex()
		{
			return Interlocked.Increment(ref this.lastWriteIndex);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000CBD2 File Offset: 0x0000ADD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int PeekLastWriteIndex()
		{
			return Volatile.Read(ref this.lastWriteIndex);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000CBDF File Offset: 0x0000ADDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteValue(int index, TValue value)
		{
			this.WriteValueOnce(this.values, index, value);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000CBEF File Offset: 0x0000ADEF
		public void ResetIndices()
		{
			this.nextFlushIndex = 0;
			Interlocked.Exchange(ref this.lastWriteIndex, -1);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000CC07 File Offset: 0x0000AE07
		public void ResetIndicesAndData()
		{
			Interlocked.Exchange(ref this.lastWriteIndex, this.Capacity);
			this.ResetValues(this.values);
			this.ResetIndices();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000CC30 File Offset: 0x0000AE30
		public TValue GetAndResetValue(int index)
		{
			TValue tvalue = this.GetAndResetValueOnce(this.values, index);
			if (this.IsInvalidValue(tvalue))
			{
				SpinWait spinWait = default(SpinWait);
				tvalue = this.GetAndResetValueOnce(this.values, index);
				while (this.IsInvalidValue(tvalue))
				{
					spinWait.SpinOnce();
					if (spinWait.Count % 100 == 0)
					{
						Task.Delay(10).ConfigureAwait(false).GetAwaiter()
							.GetResult();
					}
					tvalue = this.GetAndResetValueOnce(this.values, index);
				}
			}
			return tvalue;
		}

		// Token: 0x06000276 RID: 630
		protected abstract void ResetValues(TValue[] values);

		// Token: 0x06000277 RID: 631
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract TValue GetAndResetValueOnce(TValue[] values, int index);

		// Token: 0x06000278 RID: 632
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void WriteValueOnce(TValue[] values, int index, TValue value);

		// Token: 0x06000279 RID: 633
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract bool IsInvalidValue(TValue value);

		// Token: 0x0400011B RID: 283
		private readonly TValue[] values;

		// Token: 0x0400011C RID: 284
		private int lastWriteIndex = -1;

		// Token: 0x0400011D RID: 285
		private volatile int nextFlushIndex;
	}
}
