using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x02000023 RID: 35
	public readonly struct ActivityLink : IEquatable<ActivityLink>
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00005328 File Offset: 0x00003528
		[NullableContext(2)]
		public ActivityLink(ActivityContext context, ActivityTagsCollection tags = null)
		{
			this.Context = context;
			this.Tags = tags;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005338 File Offset: 0x00003538
		public ActivityContext Context { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005340 File Offset: 0x00003540
		[Nullable(new byte[] { 2, 0, 1, 2 })]
		public IEnumerable<KeyValuePair<string, object>> Tags
		{
			[return: Nullable(new byte[] { 2, 0, 1, 2 })]
			get;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005348 File Offset: 0x00003548
		[NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is ActivityLink)
			{
				ActivityLink activityLink = (ActivityLink)obj;
				return this.Equals(activityLink);
			}
			return false;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000536D File Offset: 0x0000356D
		public bool Equals(ActivityLink value)
		{
			return this.Context == value.Context && value.Tags == this.Tags;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005394 File Offset: 0x00003594
		public static bool operator ==(ActivityLink left, ActivityLink right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000539E File Offset: 0x0000359E
		public static bool operator !=(ActivityLink left, ActivityLink right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000053AC File Offset: 0x000035AC
		public override int GetHashCode()
		{
			if (this == default(ActivityLink))
			{
				return 0;
			}
			int num = 5381;
			num = (num << 5) + num + this.Context.GetHashCode();
			if (this.Tags != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.Tags)
				{
					num = (num << 5) + num + keyValuePair.Key.GetHashCode();
					if (keyValuePair.Value != null)
					{
						num = (num << 5) + num + keyValuePair.Value.GetHashCode();
					}
				}
			}
			return num;
		}
	}
}
