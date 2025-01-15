using System;
using System.Data.OleDb;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009B9 RID: 2489
	internal class MdxProperty : MdxCubeObject, ICubeProperty, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x06004704 RID: 18180 RVA: 0x000EE080 File Offset: 0x000EC280
		public MdxProperty(MdxPropertyKind propertyKind, string name, string mdxIdentifier, string caption, MdxLevel level, OleDbType type, MdxProperty key)
			: base(mdxIdentifier, caption)
		{
			this.propertyKind = propertyKind;
			this.name = name;
			this.level = level;
			this.type = type;
			this.key = key;
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06004705 RID: 18181 RVA: 0x0000244F File Offset: 0x0000064F
		public override MdxCubeObjectKind Kind
		{
			get
			{
				return MdxCubeObjectKind.Property;
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x000EE0B1 File Offset: 0x000EC2B1
		public MdxPropertyKind PropertyKind
		{
			get
			{
				return this.propertyKind;
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x000EE0B9 File Offset: 0x000EC2B9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06004708 RID: 18184 RVA: 0x000EE0C1 File Offset: 0x000EC2C1
		public MdxLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x000EE0C9 File Offset: 0x000EC2C9
		public OleDbType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x0600470A RID: 18186 RVA: 0x000EE0D1 File Offset: 0x000EC2D1
		public MdxProperty Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x0600470B RID: 18187 RVA: 0x000EE0C1 File Offset: 0x000EC2C1
		ICubeLevel ICubeProperty.Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x0600470C RID: 18188 RVA: 0x000EE0D9 File Offset: 0x000EC2D9
		CubePropertyKind ICubeProperty.PropertyKind
		{
			get
			{
				return this.PropertyKind.ToCubePropertyKind();
			}
		}

		// Token: 0x040025C8 RID: 9672
		private readonly MdxPropertyKind propertyKind;

		// Token: 0x040025C9 RID: 9673
		private readonly string name;

		// Token: 0x040025CA RID: 9674
		private readonly MdxLevel level;

		// Token: 0x040025CB RID: 9675
		private readonly OleDbType type;

		// Token: 0x040025CC RID: 9676
		private readonly MdxProperty key;
	}
}
