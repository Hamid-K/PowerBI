using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000544 RID: 1348
	[CannotApplyEqualityOperator]
	public class ActivityTag : IEquatable<ActivityTag>
	{
		// Token: 0x060028F0 RID: 10480 RVA: 0x0009283F File Offset: 0x00090A3F
		public ActivityTag(string tag)
		{
			this.Tag = tag;
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x0009284E File Offset: 0x00090A4E
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x00092856 File Offset: 0x00090A56
		public string Tag { get; private set; }

		// Token: 0x060028F3 RID: 10483 RVA: 0x0009285F File Offset: 0x00090A5F
		public override int GetHashCode()
		{
			return this.Tag.GetHashCode();
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0009286C File Offset: 0x00090A6C
		public override bool Equals(object other)
		{
			return this.Equals(other as ActivityTag);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0009287A File Offset: 0x00090A7A
		public bool Equals(ActivityTag other)
		{
			return other != null && this.Tag.Equals(other.Tag, StringComparison.Ordinal);
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00092893 File Offset: 0x00090A93
		public override string ToString()
		{
			return this.Tag;
		}
	}
}
