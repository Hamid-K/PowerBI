using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003FC RID: 1020
	public sealed class Bottom : IEquatable<Bottom>
	{
		// Token: 0x06001714 RID: 5908 RVA: 0x00002130 File Offset: 0x00000330
		private Bottom()
		{
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0004670F File Offset: 0x0004490F
		public static Bottom Value
		{
			get
			{
				return Bottom.Lazy.Value;
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0000D050 File Offset: 0x0000B250
		public bool Equals(Bottom other)
		{
			return this == other;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0004671B File Offset: 0x0004491B
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj is Bottom && this.Equals((Bottom)obj)));
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0004673E File Offset: 0x0004493E
		public override int GetHashCode()
		{
			return 11590926;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Bottom left, Bottom right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Bottom left, Bottom right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00046745 File Offset: 0x00044945
		public override string ToString()
		{
			return "⊥";
		}

		// Token: 0x04000B18 RID: 2840
		private static readonly Lazy<Bottom> Lazy = new Lazy<Bottom>(() => new Bottom());
	}
}
