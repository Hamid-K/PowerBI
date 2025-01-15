using System;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000449 RID: 1097
	internal sealed class SapHanaLevel
	{
		// Token: 0x0600250D RID: 9485 RVA: 0x0006A2DA File Offset: 0x000684DA
		public SapHanaLevel(SapHanaHierarchy hierarchy, string name, int number, string caption)
		{
			this.hierarchy = hierarchy;
			this.name = name;
			this.number = number;
			this.caption = caption ?? name;
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x0006A304 File Offset: 0x00068504
		public SapHanaHierarchy Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x0006A30C File Offset: 0x0006850C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x0006A31D File Offset: 0x0006851D
		// (set) Token: 0x06002510 RID: 9488 RVA: 0x0006A314 File Offset: 0x00068514
		public int Number
		{
			get
			{
				return this.number;
			}
			set
			{
				this.number = value;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x0006A325 File Offset: 0x00068525
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x0006A32D File Offset: 0x0006852D
		// (set) Token: 0x06002514 RID: 9492 RVA: 0x0006A335 File Offset: 0x00068535
		public SapHanaDimensionAttribute Attribute
		{
			get
			{
				return this.attribute;
			}
			set
			{
				this.attribute = value;
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0006A33E File Offset: 0x0006853E
		public bool Equals(SapHanaLevel other)
		{
			return other != null && this.name == other.name;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0006A356 File Offset: 0x00068556
		public override bool Equals(object other)
		{
			return this.Equals(other as SapHanaLevel);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0006A364 File Offset: 0x00068564
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000F11 RID: 3857
		private readonly SapHanaHierarchy hierarchy;

		// Token: 0x04000F12 RID: 3858
		private readonly string name;

		// Token: 0x04000F13 RID: 3859
		private readonly string caption;

		// Token: 0x04000F14 RID: 3860
		private int number;

		// Token: 0x04000F15 RID: 3861
		private SapHanaDimensionAttribute attribute;
	}
}
