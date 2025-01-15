using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009B6 RID: 2486
	internal sealed class MdxMeasure : MdxCubeObject
	{
		// Token: 0x060046FA RID: 18170 RVA: 0x000EDFEE File Offset: 0x000EC1EE
		public MdxMeasure(MdxCube cube, string mdxIdentifier, string caption, OleDbType type, string measureGroupName, string displayFolder, bool isVisible, IList<MdxCellProperty> properties)
			: base(mdxIdentifier, caption)
		{
			this.cube = cube;
			this.measureGroupName = measureGroupName;
			this.displayFolder = displayFolder;
			this.type = type;
			this.isVisible = isVisible;
			this.properties = properties;
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x00002105 File Offset: 0x00000305
		public override MdxCubeObjectKind Kind
		{
			get
			{
				return MdxCubeObjectKind.Measure;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x000EE027 File Offset: 0x000EC227
		public MdxMeasureGroup MeasureGroup
		{
			get
			{
				return this.cube.GetMeasureGroup(this.measureGroupName);
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x000EE03A File Offset: 0x000EC23A
		public string DisplayFolders
		{
			get
			{
				return this.displayFolder;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x060046FE RID: 18174 RVA: 0x000EE042 File Offset: 0x000EC242
		public OleDbType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x000EE04A File Offset: 0x000EC24A
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x000EE052 File Offset: 0x000EC252
		public IList<MdxCellProperty> CellProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040025BC RID: 9660
		private readonly MdxCube cube;

		// Token: 0x040025BD RID: 9661
		private readonly string measureGroupName;

		// Token: 0x040025BE RID: 9662
		private readonly string displayFolder;

		// Token: 0x040025BF RID: 9663
		private readonly OleDbType type;

		// Token: 0x040025C0 RID: 9664
		private readonly bool isVisible;

		// Token: 0x040025C1 RID: 9665
		private readonly IList<MdxCellProperty> properties;
	}
}
