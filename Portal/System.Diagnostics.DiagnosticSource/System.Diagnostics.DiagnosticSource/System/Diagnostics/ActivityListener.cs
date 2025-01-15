using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x02000025 RID: 37
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class ActivityListener : IDisposable
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000546C File Offset: 0x0000366C
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005474 File Offset: 0x00003674
		[Nullable(new byte[] { 2, 1 })]
		public Action<Activity> ActivityStarted
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000547D File Offset: 0x0000367D
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00005485 File Offset: 0x00003685
		[Nullable(new byte[] { 2, 1 })]
		public Action<Activity> ActivityStopped
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000548E File Offset: 0x0000368E
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005496 File Offset: 0x00003696
		[Nullable(new byte[] { 2, 1 })]
		public Func<ActivitySource, bool> ShouldListenTo
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000549F File Offset: 0x0000369F
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000054A7 File Offset: 0x000036A7
		[Nullable(new byte[] { 2, 1 })]
		public SampleActivity<string> SampleUsingParentId
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000054B0 File Offset: 0x000036B0
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000054B8 File Offset: 0x000036B8
		public SampleActivity<ActivityContext> Sample { get; set; }

		// Token: 0x06000142 RID: 322 RVA: 0x000054C1 File Offset: 0x000036C1
		public void Dispose()
		{
			ActivitySource.DetachListener(this);
		}
	}
}
