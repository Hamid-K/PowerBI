using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D2 RID: 210
	public abstract class UtilityBlock : Block
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00010777 File Offset: 0x0000E977
		protected UtilityBlock(string name)
			: base(name)
		{
		}

		// Token: 0x060005EE RID: 1518
		public abstract int Run();

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000E568 File Offset: 0x0000C768
		public virtual bool ExitOnControlC
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00014FA9 File Offset: 0x000131A9
		protected virtual IList<IBlock> BlocksToAdd
		{
			get
			{
				return new List<IBlock>();
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00014FB0 File Offset: 0x000131B0
		internal IList<IBlock> GetBlocksToAdd()
		{
			return this.BlocksToAdd;
		}
	}
}
