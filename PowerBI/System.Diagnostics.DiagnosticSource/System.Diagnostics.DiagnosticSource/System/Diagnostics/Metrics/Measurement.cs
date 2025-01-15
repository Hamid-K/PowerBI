using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000049 RID: 73
	[SecuritySafeCritical]
	public readonly struct Measurement<T> where T : struct
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00009953 File Offset: 0x00007B53
		public Measurement(T value)
		{
			this._tags = Instrument.EmptyTags;
			this.Value = value;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009967 File Offset: 0x00007B67
		public Measurement(T value, [Nullable(new byte[] { 2, 0, 1, 2 })] IEnumerable<KeyValuePair<string, object>> tags)
		{
			this._tags = Measurement<T>.ToArray(tags);
			this.Value = value;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000997C File Offset: 0x00007B7C
		public Measurement(T value, [Nullable(new byte[] { 2, 0, 1, 2 })] params KeyValuePair<string, object>[] tags)
		{
			if (tags != null)
			{
				this._tags = new KeyValuePair<string, object>[tags.Length];
				tags.CopyTo(this._tags, 0);
			}
			else
			{
				this._tags = Instrument.EmptyTags;
			}
			this.Value = value;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000099B0 File Offset: 0x00007BB0
		public Measurement(T value, [Nullable(new byte[] { 0, 0, 1, 2 })] ReadOnlySpan<KeyValuePair<string, object>> tags)
		{
			this._tags = tags.ToArray();
			this.Value = value;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000099C6 File Offset: 0x00007BC6
		[Nullable(new byte[] { 0, 0, 1, 2 })]
		public ReadOnlySpan<KeyValuePair<string, object>> Tags
		{
			[return: Nullable(new byte[] { 0, 0, 1, 2 })]
			get
			{
				return MemoryExtensions.AsSpan<KeyValuePair<string, object>>(this._tags);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000099D8 File Offset: 0x00007BD8
		public T Value { get; }

		// Token: 0x06000239 RID: 569 RVA: 0x000099E0 File Offset: 0x00007BE0
		private static KeyValuePair<string, object>[] ToArray(IEnumerable<KeyValuePair<string, object>> tags)
		{
			if (tags != null)
			{
				return new List<KeyValuePair<string, object>>(tags).ToArray();
			}
			return Instrument.EmptyTags;
		}

		// Token: 0x040000FF RID: 255
		private readonly KeyValuePair<string, object>[] _tags;
	}
}
