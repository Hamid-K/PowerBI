using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009B1 RID: 2481
	internal sealed class MdxHierarchy : MdxCubeObject, ICubeHierarchy, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x060046CE RID: 18126 RVA: 0x000EDAAF File Offset: 0x000EBCAF
		public MdxHierarchy(string mdxIdentifier, string uniqueIdentifier, string caption, string displayFolders, MdxHierarchyType type, MdxDimension dimension, bool isVisible)
			: base(mdxIdentifier, caption)
		{
			this.displayFolders = displayFolders;
			this.levels = new List<MdxLevel>();
			this.type = type;
			this.dimension = dimension;
			this.isVisible = isVisible;
			this.uniqueIdentifier = uniqueIdentifier;
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x000023C4 File Offset: 0x000005C4
		public override MdxCubeObjectKind Kind
		{
			get
			{
				return MdxCubeObjectKind.Hierarchy;
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x060046D0 RID: 18128 RVA: 0x000EDAEB File Offset: 0x000EBCEB
		public string DisplayFolders
		{
			get
			{
				return this.displayFolders;
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x060046D1 RID: 18129 RVA: 0x000EDAF3 File Offset: 0x000EBCF3
		public MdxHierarchyType Type
		{
			get
			{
				if (this.Levels.Count > 1)
				{
					return MdxHierarchyType.UserDefined;
				}
				return this.type;
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x000EDB0B File Offset: 0x000EBD0B
		public IList<MdxLevel> Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x000EDB13 File Offset: 0x000EBD13
		public MdxDimension Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x000EDB1B File Offset: 0x000EBD1B
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x000EDB23 File Offset: 0x000EBD23
		public string UniqueIdentifier
		{
			get
			{
				return this.uniqueIdentifier;
			}
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x000EDB2B File Offset: 0x000EBD2B
		public ICubeLevel GetLevel(int number)
		{
			return this.levels[number];
		}

		// Token: 0x0400259D RID: 9629
		private readonly string displayFolders;

		// Token: 0x0400259E RID: 9630
		private readonly bool isVisible;

		// Token: 0x0400259F RID: 9631
		private readonly MdxHierarchyType type;

		// Token: 0x040025A0 RID: 9632
		private readonly IList<MdxLevel> levels;

		// Token: 0x040025A1 RID: 9633
		private readonly MdxDimension dimension;

		// Token: 0x040025A2 RID: 9634
		private readonly string uniqueIdentifier;
	}
}
