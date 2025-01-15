using System;
using System.Data.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000965 RID: 2405
	internal class MdxCellProperty : MdxCubeObject
	{
		// Token: 0x0600447E RID: 17534 RVA: 0x000E666A File Offset: 0x000E486A
		public MdxCellProperty(string name, string mdxIdentifier, string caption, OleDbType type)
			: base(mdxIdentifier, caption)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x00075E2C File Offset: 0x0007402C
		public override MdxCubeObjectKind Kind
		{
			get
			{
				return MdxCubeObjectKind.CellProperty;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x000E6683 File Offset: 0x000E4883
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x000E668B File Offset: 0x000E488B
		public OleDbType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0400247E RID: 9342
		private readonly string name;

		// Token: 0x0400247F RID: 9343
		private readonly OleDbType type;
	}
}
