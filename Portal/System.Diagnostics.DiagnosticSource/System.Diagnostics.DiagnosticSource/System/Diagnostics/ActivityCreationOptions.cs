using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ActivityCreationOptions<[Nullable(2)] T>
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005088 File Offset: 0x00003288
		internal ActivityCreationOptions(ActivitySource source, string name, T parent, ActivityKind kind, IEnumerable<KeyValuePair<string, object>> tags, IEnumerable<ActivityLink> links, ActivityIdFormat idFormat)
		{
			this.Source = source;
			this.Name = name;
			this.Kind = kind;
			this.Parent = parent;
			this.Tags = tags;
			this.Links = links;
			this.IdFormat = idFormat;
			if (this.IdFormat == ActivityIdFormat.Unknown && Activity.ForceDefaultIdFormat)
			{
				this.IdFormat = Activity.DefaultIdFormat;
			}
			this._samplerTags = null;
			if (parent is ActivityContext)
			{
				ActivityContext activityContext = parent as ActivityContext;
				if (activityContext != default(ActivityContext))
				{
					this._context = activityContext;
					if (this.IdFormat == ActivityIdFormat.Unknown)
					{
						this.IdFormat = ActivityIdFormat.W3C;
						return;
					}
					return;
				}
			}
			string text = parent as string;
			if (text != null && text != null)
			{
				if (this.IdFormat == ActivityIdFormat.Hierarchical)
				{
					this._context = default(ActivityContext);
					return;
				}
				if (ActivityContext.TryParse(text, null, out this._context))
				{
					this.IdFormat = ActivityIdFormat.W3C;
				}
				if (this.IdFormat == ActivityIdFormat.Unknown)
				{
					this.IdFormat = ActivityIdFormat.Hierarchical;
					return;
				}
			}
			else
			{
				this._context = default(ActivityContext);
				if (this.IdFormat == ActivityIdFormat.Unknown)
				{
					this.IdFormat = ((Activity.Current != null) ? Activity.Current.IdFormat : Activity.DefaultIdFormat);
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000051B4 File Offset: 0x000033B4
		public ActivitySource Source { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000051BC File Offset: 0x000033BC
		public string Name { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000051C4 File Offset: 0x000033C4
		public ActivityKind Kind { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000051CC File Offset: 0x000033CC
		public T Parent { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000051D4 File Offset: 0x000033D4
		[Nullable(new byte[] { 2, 0, 1, 2 })]
		public IEnumerable<KeyValuePair<string, object>> Tags
		{
			[return: Nullable(new byte[] { 2, 0, 1, 2 })]
			get;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000051DC File Offset: 0x000033DC
		[Nullable(2)]
		public IEnumerable<ActivityLink> Links
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000051E4 File Offset: 0x000033E4
		public unsafe ActivityTagsCollection SamplingTags
		{
			[SecuritySafeCritical]
			get
			{
				if (this._samplerTags == null)
				{
					*Unsafe.AsRef<ActivityTagsCollection>(in this._samplerTags) = new ActivityTagsCollection();
				}
				return this._samplerTags;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005208 File Offset: 0x00003408
		public unsafe ActivityTraceId TraceId
		{
			[SecuritySafeCritical]
			get
			{
				if (this.Parent is ActivityContext && this.IdFormat == ActivityIdFormat.W3C && this._context == default(ActivityContext))
				{
					Func<ActivityTraceId> traceIdGenerator = Activity.TraceIdGenerator;
					ActivityTraceId activityTraceId = ((traceIdGenerator == null) ? ActivityTraceId.CreateRandom() : traceIdGenerator());
					*Unsafe.AsRef<ActivityContext>(in this._context) = new ActivityContext(activityTraceId, default(ActivitySpanId), ActivityTraceFlags.None, null, false);
				}
				return this._context.TraceId;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000528A File Offset: 0x0000348A
		internal ActivityIdFormat IdFormat { get; }

		// Token: 0x06000123 RID: 291 RVA: 0x00005292 File Offset: 0x00003492
		internal ActivityTagsCollection GetSamplingTags()
		{
			return this._samplerTags;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000529A File Offset: 0x0000349A
		internal ActivityContext GetContext()
		{
			return this._context;
		}

		// Token: 0x0400005A RID: 90
		private readonly ActivityTagsCollection _samplerTags;

		// Token: 0x0400005B RID: 91
		private readonly ActivityContext _context;
	}
}
