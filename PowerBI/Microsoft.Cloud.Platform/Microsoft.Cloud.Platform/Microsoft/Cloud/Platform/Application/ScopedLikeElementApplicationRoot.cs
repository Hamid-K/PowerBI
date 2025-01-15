using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003EF RID: 1007
	public class ScopedLikeElementApplicationRoot : ScopedApplicationRoot
	{
		// Token: 0x06001EFC RID: 7932 RVA: 0x000740D7 File Offset: 0x000722D7
		public ScopedLikeElementApplicationRoot(IEnumerable<IBlock> blocks, IEnumerable<Type> blocksToRemove, string name)
			: base(blocks.Concat(ScopedLikeElementApplicationRoot.RemoveBlocks(new ElementApplicationRoot().GetElementApplicationRootBlocks(), blocksToRemove)), name)
		{
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000740F6 File Offset: 0x000722F6
		public static ScopedLikeElementApplicationRoot Create(IEnumerable<IBlock> blocksToAdd, IEnumerable<Type> blocksToRemove, string[] args, string name)
		{
			ScopedLikeElementApplicationRoot scopedLikeElementApplicationRoot = new ScopedLikeElementApplicationRoot(blocksToAdd, blocksToRemove, name);
			ScopedApplicationRoot.InitializeAndStartScopedApplicationRoot(scopedLikeElementApplicationRoot, args);
			return scopedLikeElementApplicationRoot;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00074107 File Offset: 0x00072307
		public new static ScopedLikeElementApplicationRoot Create(IEnumerable<IBlock> blocksToAdd, string[] args, string name)
		{
			return ScopedLikeElementApplicationRoot.Create(blocksToAdd, new List<Type>(), args, name);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00074118 File Offset: 0x00072318
		private static IEnumerable<IBlock> RemoveBlocks(IEnumerable<IBlock> blocks, IEnumerable<Type> blocksToRemove)
		{
			List<IBlock> list = new List<IBlock>();
			foreach (IBlock block in blocks)
			{
				if (!blocksToRemove.Contains(block.GetType()))
				{
					list.Add(block);
				}
			}
			return list;
		}
	}
}
