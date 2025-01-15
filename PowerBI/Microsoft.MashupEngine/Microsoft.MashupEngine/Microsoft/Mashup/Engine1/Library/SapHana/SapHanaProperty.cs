using System;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000475 RID: 1141
	internal sealed class SapHanaProperty : ICubeProperty, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x060025F3 RID: 9715 RVA: 0x0006DF08 File Offset: 0x0006C108
		public SapHanaProperty(ColumnSapHanaDimensionAttribute attribute, CubePropertyKind kind, string name, string caption)
		{
			this.attribute = attribute;
			this.kind = kind;
			this.name = name;
			this.caption = caption ?? name;
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x00002139 File Offset: 0x00000339
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.Property;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x0006DF32 File Offset: 0x0006C132
		public ColumnSapHanaDimensionAttribute Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x0006DF3A File Offset: 0x0006C13A
		public CubePropertyKind PropertyKind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x0006DF42 File Offset: 0x0006C142
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x0006DF4A File Offset: 0x0006C14A
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0006DF52 File Offset: 0x0006C152
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x0006DF5A File Offset: 0x0006C15A
		public SapHanaColumn Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x0006DF32 File Offset: 0x0006C132
		ICubeLevel ICubeProperty.Level
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0006DF63 File Offset: 0x0006C163
		public bool Equals(SapHanaProperty other)
		{
			return other != null && this.name == other.name;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0006DF7B File Offset: 0x0006C17B
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as SapHanaProperty);
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0006DF7B File Offset: 0x0006C17B
		public override bool Equals(object other)
		{
			return this.Equals(other as SapHanaProperty);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0006DF89 File Offset: 0x0006C189
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000FE1 RID: 4065
		private readonly ColumnSapHanaDimensionAttribute attribute;

		// Token: 0x04000FE2 RID: 4066
		private readonly CubePropertyKind kind;

		// Token: 0x04000FE3 RID: 4067
		private readonly string name;

		// Token: 0x04000FE4 RID: 4068
		private readonly string caption;

		// Token: 0x04000FE5 RID: 4069
		private SapHanaColumn column;
	}
}
