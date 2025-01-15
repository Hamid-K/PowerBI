using System;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001175 RID: 4469
	public struct Restriction : IEquatable<Restriction>
	{
		// Token: 0x06007530 RID: 30000 RVA: 0x00191A34 File Offset: 0x0018FC34
		private Restriction(bool hasFilter, string item)
		{
			this.hasFilter = hasFilter;
			this.item = item;
		}

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x06007531 RID: 30001 RVA: 0x00191A44 File Offset: 0x0018FC44
		public bool HasFilter
		{
			get
			{
				return this.hasFilter;
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x06007532 RID: 30002 RVA: 0x00191A4C File Offset: 0x0018FC4C
		public string Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x00191A54 File Offset: 0x0018FC54
		public static Restriction To(string item)
		{
			return new Restriction(true, item);
		}

		// Token: 0x06007534 RID: 30004 RVA: 0x00191A5D File Offset: 0x0018FC5D
		public bool Matches(string item)
		{
			return !this.HasFilter || string.Equals(this.Item, item, StringComparison.Ordinal);
		}

		// Token: 0x06007535 RID: 30005 RVA: 0x00191A76 File Offset: 0x0018FC76
		public bool Matches(HierarchyItem item)
		{
			return this.Matches(item.Name);
		}

		// Token: 0x06007536 RID: 30006 RVA: 0x00191A84 File Offset: 0x0018FC84
		public override bool Equals(object obj)
		{
			return obj is Restriction && this.Equals((Restriction)obj);
		}

		// Token: 0x06007537 RID: 30007 RVA: 0x00191A9C File Offset: 0x0018FC9C
		public bool Equals(Restriction other)
		{
			return (!this.HasFilter && !other.HasFilter) || (this.HasFilter && other.HasFilter && this.Matches(other.Item));
		}

		// Token: 0x06007538 RID: 30008 RVA: 0x00191AD1 File Offset: 0x0018FCD1
		public override int GetHashCode()
		{
			if (!this.HasFilter)
			{
				return 313;
			}
			if (this.Item == null)
			{
				return 271;
			}
			return this.Item.GetHashCode();
		}

		// Token: 0x0400405E RID: 16478
		public static Restriction Any = new Restriction(false, null);

		// Token: 0x0400405F RID: 16479
		private readonly bool hasFilter;

		// Token: 0x04004060 RID: 16480
		private readonly string item;
	}
}
