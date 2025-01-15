using System;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200043D RID: 1085
	internal abstract class SapHanaDimensionAttribute : ICubeLevel, ICubeObject, IEquatable<ICubeObject>, ICubeHierarchy
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x00069617 File Offset: 0x00067817
		public SapHanaDimensionAttribute(SapHanaDimension dimension, string name, string caption)
		{
			this.dimension = dimension;
			this.name = name;
			this.caption = caption ?? name;
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x00002105 File Offset: 0x00000305
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.DimensionAttribute;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x00069639 File Offset: 0x00067839
		public SapHanaDimension Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x00069641 File Offset: 0x00067841
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x00069649 File Offset: 0x00067849
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		ICubeHierarchy ICubeLevel.Hierarchy
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00002105 File Offset: 0x00000305
		int ICubeLevel.Number
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		ICubeLevel ICubeHierarchy.GetLevel(int number)
		{
			return this;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00069651 File Offset: 0x00067851
		public bool Equals(SapHanaDimensionAttribute other)
		{
			return other != null && this.name == other.name;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00069669 File Offset: 0x00067869
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as SapHanaDimensionAttribute);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00069669 File Offset: 0x00067869
		public override bool Equals(object other)
		{
			return this.Equals(other as SapHanaDimensionAttribute);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00069677 File Offset: 0x00067877
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000EE5 RID: 3813
		private readonly SapHanaDimension dimension;

		// Token: 0x04000EE6 RID: 3814
		private readonly string name;

		// Token: 0x04000EE7 RID: 3815
		private readonly string caption;
	}
}
