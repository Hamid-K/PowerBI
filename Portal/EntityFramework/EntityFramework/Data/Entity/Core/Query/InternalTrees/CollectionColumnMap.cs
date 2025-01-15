using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000384 RID: 900
	internal abstract class CollectionColumnMap : ColumnMap
	{
		// Token: 0x06002BC1 RID: 11201 RVA: 0x0008DE6F File Offset: 0x0008C06F
		internal CollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys)
			: base(type, name)
		{
			this.m_element = elementMap;
			this.m_keys = keys ?? new SimpleColumnMap[0];
			this.m_foreignKeys = foreignKeys ?? new SimpleColumnMap[0];
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x0008DEA4 File Offset: 0x0008C0A4
		internal SimpleColumnMap[] ForeignKeys
		{
			get
			{
				return this.m_foreignKeys;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x0008DEAC File Offset: 0x0008C0AC
		internal SimpleColumnMap[] Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x0008DEB4 File Offset: 0x0008C0B4
		internal ColumnMap Element
		{
			get
			{
				return this.m_element;
			}
		}

		// Token: 0x04000EDD RID: 3805
		private readonly ColumnMap m_element;

		// Token: 0x04000EDE RID: 3806
		private readonly SimpleColumnMap[] m_foreignKeys;

		// Token: 0x04000EDF RID: 3807
		private readonly SimpleColumnMap[] m_keys;
	}
}
