using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200098E RID: 2446
	internal sealed class MdxDimension : MdxCubeObject
	{
		// Token: 0x06004633 RID: 17971 RVA: 0x000EBBC6 File Offset: 0x000E9DC6
		public MdxDimension(string mdxIdentifier, string caption, MdxDimensionType dimensionType, OleDbType type, MdxCubeMetadataProviderCube cube)
			: base(mdxIdentifier, caption)
		{
			this.dimensionType = dimensionType;
			this.type = type;
			this.cube = cube;
			this.hierarchies = new Dictionary<string, MdxHierarchy>();
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x000EBBF2 File Offset: 0x000E9DF2
		public MdxDimensionType DimensionType
		{
			get
			{
				return this.dimensionType;
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x000EBBFA File Offset: 0x000E9DFA
		public OleDbType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x00002139 File Offset: 0x00000339
		public override MdxCubeObjectKind Kind
		{
			get
			{
				return MdxCubeObjectKind.Dimension;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x000EBC02 File Offset: 0x000E9E02
		public Dictionary<string, MdxHierarchy> Hierarchies
		{
			get
			{
				return this.hierarchies;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x000EBC0A File Offset: 0x000E9E0A
		public IEnumerable<MdxHierarchy> VisibleHierarchies
		{
			get
			{
				return this.hierarchies.Values.Where((MdxHierarchy h) => h.IsVisible);
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x000EBC3C File Offset: 0x000E9E3C
		public Dictionary<string, MdxPropertyMetadata> PropertiesMetadata
		{
			get
			{
				if (this.propertiesMetadata == null)
				{
					this.propertiesMetadata = new Dictionary<string, MdxPropertyMetadata>();
					if (this.cube.SupportsProperties)
					{
						foreach (MdxPropertyMetadata mdxPropertyMetadata in this.cube.Metadata.GetProperties(this))
						{
							try
							{
								this.propertiesMetadata.Add(mdxPropertyMetadata.UniqueName, mdxPropertyMetadata);
							}
							catch (ArgumentException)
							{
							}
						}
					}
				}
				return this.propertiesMetadata;
			}
		}

		// Token: 0x04002519 RID: 9497
		private readonly Dictionary<string, MdxHierarchy> hierarchies;

		// Token: 0x0400251A RID: 9498
		private readonly MdxCubeMetadataProviderCube cube;

		// Token: 0x0400251B RID: 9499
		private readonly MdxDimensionType dimensionType;

		// Token: 0x0400251C RID: 9500
		private readonly OleDbType type;

		// Token: 0x0400251D RID: 9501
		private Dictionary<string, MdxPropertyMetadata> propertiesMetadata;
	}
}
