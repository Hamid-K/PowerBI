using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000086 RID: 134
	internal sealed class EmptyMembersCollection : IMemberCollectionInternal
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x000271F9 File Offset: 0x000253F9
		internal EmptyMembersCollection()
		{
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00027201 File Offset: 0x00025401
		int IMemberCollectionInternal.Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00027204 File Offset: 0x00025404
		bool IMemberCollectionInternal.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00027207 File Offset: 0x00025407
		object IMemberCollectionInternal.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000267 RID: 615
		Member IMemberCollectionInternal.this[int index]
		{
			get
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00027216 File Offset: 0x00025416
		Member IMemberCollectionInternal.Find(string index)
		{
			return null;
		}
	}
}
