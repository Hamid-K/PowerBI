using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000447 RID: 1095
	internal sealed class SapHanaHierarchy
	{
		// Token: 0x06002505 RID: 9477 RVA: 0x0006A259 File Offset: 0x00068459
		public SapHanaHierarchy(SapHanaDimension dimension, string name, string caption)
		{
			this.dimension = dimension;
			this.name = name;
			this.caption = caption ?? name;
			this.levels = new List<SapHanaLevel>(4);
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x0006A287 File Offset: 0x00068487
		public SapHanaDimension Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x0006A28F File Offset: 0x0006848F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x0006A297 File Offset: 0x00068497
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x0006A29F File Offset: 0x0006849F
		public List<SapHanaLevel> Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0006A2A7 File Offset: 0x000684A7
		public bool Equals(SapHanaHierarchy other)
		{
			return other != null && other.name == this.name;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0006A2BF File Offset: 0x000684BF
		public override bool Equals(object other)
		{
			return this.Equals(other as SapHanaHierarchy);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0006A2CD File Offset: 0x000684CD
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000F05 RID: 3845
		private readonly SapHanaDimension dimension;

		// Token: 0x04000F06 RID: 3846
		private readonly string name;

		// Token: 0x04000F07 RID: 3847
		private readonly string caption;

		// Token: 0x04000F08 RID: 3848
		private readonly List<SapHanaLevel> levels;
	}
}
