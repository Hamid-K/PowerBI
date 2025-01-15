using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000042 RID: 66
	[SecuritySafeCritical]
	public sealed class Histogram<T> : Instrument<T> where T : struct
	{
		// Token: 0x06000208 RID: 520 RVA: 0x000091F6 File Offset: 0x000073F6
		internal Histogram(Meter meter, string name, string unit, string description)
			: base(meter, name, unit, description)
		{
			base.Publish();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009209 File Offset: 0x00007409
		public void Record(T value)
		{
			base.RecordMeasurement(value);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009212 File Offset: 0x00007412
		public void Record(T value, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag)
		{
			base.RecordMeasurement(value, tag);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000921C File Offset: 0x0000741C
		public void Record(T value, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag1, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag2)
		{
			base.RecordMeasurement(value, tag1, tag2);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009227 File Offset: 0x00007427
		public void Record(T value, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag1, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag2, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag3)
		{
			base.RecordMeasurement(value, tag1, tag2, tag3);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009234 File Offset: 0x00007434
		public void Record(T value, [Nullable(new byte[] { 0, 0, 1, 2 })] ReadOnlySpan<KeyValuePair<string, object>> tags)
		{
			base.RecordMeasurement(value, tags);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000923E File Offset: 0x0000743E
		public void Record(T value, [Nullable(new byte[] { 1, 0, 1, 2 })] params KeyValuePair<string, object>[] tags)
		{
			base.RecordMeasurement(value, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(tags));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009252 File Offset: 0x00007452
		public void Record(T value, in TagList tagList)
		{
			base.RecordMeasurement(value, in tagList);
		}
	}
}
