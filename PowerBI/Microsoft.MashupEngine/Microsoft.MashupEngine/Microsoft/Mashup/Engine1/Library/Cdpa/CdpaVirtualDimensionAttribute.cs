using System;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D9D RID: 3485
	internal abstract class CdpaVirtualDimensionAttribute : CdpaDimensionAttribute, ICubeHierarchy, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x06005EEF RID: 24303 RVA: 0x00147CFE File Offset: 0x00145EFE
		public CdpaVirtualDimensionAttribute(CdpaDimension dimension, string name, string caption)
			: base(dimension)
		{
			this.name = name;
			this.caption = caption;
			this.qualifiedName = base.Dimension.QualifiedName.Qualify(this.name);
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override ICubeHierarchy Hierarchy
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x06005EF1 RID: 24305 RVA: 0x00002105 File Offset: 0x00000305
		public override int Number
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public ICubeLevel GetLevel(int number)
		{
			return this;
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x06005EF3 RID: 24307 RVA: 0x00147D31 File Offset: 0x00145F31
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x00147D39 File Offset: 0x00145F39
		public override string PropertyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x06005EF5 RID: 24309 RVA: 0x00147D41 File Offset: 0x00145F41
		public override string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x04003419 RID: 13337
		private readonly string name;

		// Token: 0x0400341A RID: 13338
		private readonly string caption;

		// Token: 0x0400341B RID: 13339
		private readonly QualifiedName qualifiedName;
	}
}
