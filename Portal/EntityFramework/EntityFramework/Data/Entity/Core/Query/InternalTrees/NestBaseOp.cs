using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003BA RID: 954
	internal abstract class NestBaseOp : PhysicalOp
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x00092393 File Offset: 0x00090593
		internal List<SortKey> PrefixSortKeys
		{
			get
			{
				return this.m_prefixSortKeys;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x0009239B File Offset: 0x0009059B
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputs;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x000923A3 File Offset: 0x000905A3
		internal List<CollectionInfo> CollectionInfo
		{
			get
			{
				return this.m_collectionInfoList;
			}
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000923AB File Offset: 0x000905AB
		internal NestBaseOp(OpType opType, List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList)
			: base(opType)
		{
			this.m_outputs = outputVars;
			this.m_collectionInfoList = collectionInfoList;
			this.m_prefixSortKeys = prefixSortKeys;
		}

		// Token: 0x04000F4D RID: 3917
		private readonly List<SortKey> m_prefixSortKeys;

		// Token: 0x04000F4E RID: 3918
		private readonly VarVec m_outputs;

		// Token: 0x04000F4F RID: 3919
		private readonly List<CollectionInfo> m_collectionInfoList;
	}
}
