using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200003F RID: 63
	[SecuritySafeCritical]
	public sealed class Counter<T> : Instrument<T> where T : struct
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00008DF5 File Offset: 0x00006FF5
		internal Counter(Meter meter, string name, string unit, string description)
			: base(meter, name, unit, description)
		{
			base.Publish();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008E08 File Offset: 0x00007008
		public void Add(T delta)
		{
			base.RecordMeasurement(delta);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008E11 File Offset: 0x00007011
		public void Add(T delta, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag)
		{
			base.RecordMeasurement(delta, tag);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008E1B File Offset: 0x0000701B
		public void Add(T delta, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag1, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag2)
		{
			base.RecordMeasurement(delta, tag1, tag2);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008E26 File Offset: 0x00007026
		public void Add(T delta, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag1, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag2, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag3)
		{
			base.RecordMeasurement(delta, tag1, tag2, tag3);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008E33 File Offset: 0x00007033
		public void Add(T delta, [Nullable(new byte[] { 0, 0, 1, 2 })] ReadOnlySpan<KeyValuePair<string, object>> tags)
		{
			base.RecordMeasurement(delta, tags);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008E3D File Offset: 0x0000703D
		public void Add(T delta, [Nullable(new byte[] { 1, 0, 1, 2 })] params KeyValuePair<string, object>[] tags)
		{
			base.RecordMeasurement(delta, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(tags));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008E51 File Offset: 0x00007051
		public void Add(T delta, in TagList tagList)
		{
			base.RecordMeasurement(delta, in tagList);
		}
	}
}
