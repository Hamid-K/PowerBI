using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000086 RID: 134
	internal sealed class EmptyMembersCollection : IMemberCollectionInternal
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x00026EC9 File Offset: 0x000250C9
		internal EmptyMembersCollection()
		{
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00026ED1 File Offset: 0x000250D1
		int IMemberCollectionInternal.Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00026ED4 File Offset: 0x000250D4
		bool IMemberCollectionInternal.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00026ED7 File Offset: 0x000250D7
		object IMemberCollectionInternal.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000261 RID: 609
		Member IMemberCollectionInternal.this[int index]
		{
			get
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00026EE6 File Offset: 0x000250E6
		Member IMemberCollectionInternal.Find(string index)
		{
			return null;
		}
	}
}
