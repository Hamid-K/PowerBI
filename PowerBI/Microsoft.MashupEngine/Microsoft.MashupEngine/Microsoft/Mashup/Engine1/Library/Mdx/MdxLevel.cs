using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009B5 RID: 2485
	internal sealed class MdxLevel : MdxCubeObject, ICubeLevel, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x060046F4 RID: 18164 RVA: 0x000EDF9B File Offset: 0x000EC19B
		public MdxLevel(string mdxIdentifier, string caption, int number, MdxHierarchy hierarchy, Func<MdxLevel, List<MdxProperty>> buildProperties)
			: base(mdxIdentifier, caption)
		{
			this.number = number;
			this.hierarchy = hierarchy;
			this.buildProperties = buildProperties;
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x0000240C File Offset: 0x0000060C
		public override MdxCubeObjectKind Kind
		{
			get
			{
				return MdxCubeObjectKind.Level;
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x000EDFBC File Offset: 0x000EC1BC
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x000EDFC4 File Offset: 0x000EC1C4
		public MdxHierarchy Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x000EDFCC File Offset: 0x000EC1CC
		public IList<MdxProperty> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = this.buildProperties(this);
				}
				return this.properties;
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x000EDFC4 File Offset: 0x000EC1C4
		ICubeHierarchy ICubeLevel.Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x040025B8 RID: 9656
		private readonly int number;

		// Token: 0x040025B9 RID: 9657
		private readonly MdxHierarchy hierarchy;

		// Token: 0x040025BA RID: 9658
		private readonly Func<MdxLevel, List<MdxProperty>> buildProperties;

		// Token: 0x040025BB RID: 9659
		private IList<MdxProperty> properties;
	}
}
