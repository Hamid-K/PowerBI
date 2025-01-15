using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ActivityEvent
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000052A2 File Offset: 0x000034A2
		public ActivityEvent(string name)
		{
			this = new ActivityEvent(name, DateTimeOffset.UtcNow, ActivityEvent.s_emptyTags);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000052B8 File Offset: 0x000034B8
		public ActivityEvent(string name, DateTimeOffset timestamp = default(DateTimeOffset), [Nullable(2)] ActivityTagsCollection tags = null)
		{
			this.Name = name ?? string.Empty;
			this.Tags = tags ?? ActivityEvent.s_emptyTags;
			this.Timestamp = ((timestamp != default(DateTimeOffset)) ? timestamp : DateTimeOffset.UtcNow);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005304 File Offset: 0x00003504
		public string Name { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000530C File Offset: 0x0000350C
		public DateTimeOffset Timestamp { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005314 File Offset: 0x00003514
		[Nullable(new byte[] { 1, 0, 1, 2 })]
		public IEnumerable<KeyValuePair<string, object>> Tags
		{
			[return: Nullable(new byte[] { 1, 0, 1, 2 })]
			get;
		}

		// Token: 0x04000068 RID: 104
		private static readonly ActivityTagsCollection s_emptyTags = new ActivityTagsCollection();
	}
}
