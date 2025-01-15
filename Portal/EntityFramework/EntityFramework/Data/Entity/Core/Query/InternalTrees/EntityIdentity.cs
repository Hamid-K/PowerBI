using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A1 RID: 929
	internal abstract class EntityIdentity
	{
		// Token: 0x06002D34 RID: 11572 RVA: 0x00091A03 File Offset: 0x0008FC03
		internal EntityIdentity(SimpleColumnMap[] keyColumns)
		{
			this.m_keys = keyColumns;
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x00091A12 File Offset: 0x0008FC12
		internal SimpleColumnMap[] Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x04000F20 RID: 3872
		private readonly SimpleColumnMap[] m_keys;
	}
}
