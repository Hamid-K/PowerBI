using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000385 RID: 901
	internal class CollectionInfo
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x0008DEBC File Offset: 0x0008C0BC
		internal Var CollectionVar
		{
			get
			{
				return this.m_collectionVar;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x0008DEC4 File Offset: 0x0008C0C4
		internal ColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x0008DECC File Offset: 0x0008C0CC
		internal VarList FlattenedElementVars
		{
			get
			{
				return this.m_flattenedElementVars;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x0008DED4 File Offset: 0x0008C0D4
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x0008DEDC File Offset: 0x0008C0DC
		internal List<SortKey> SortKeys
		{
			get
			{
				return this.m_sortKeys;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x0008DEE4 File Offset: 0x0008C0E4
		internal object DiscriminatorValue
		{
			get
			{
				return this.m_discriminatorValue;
			}
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x0008DEEC File Offset: 0x0008C0EC
		internal CollectionInfo(Var collectionVar, ColumnMap columnMap, VarList flattenedElementVars, VarVec keys, List<SortKey> sortKeys, object discriminatorValue)
		{
			this.m_collectionVar = collectionVar;
			this.m_columnMap = columnMap;
			this.m_flattenedElementVars = flattenedElementVars;
			this.m_keys = keys;
			this.m_sortKeys = sortKeys;
			this.m_discriminatorValue = discriminatorValue;
		}

		// Token: 0x04000EE0 RID: 3808
		private readonly Var m_collectionVar;

		// Token: 0x04000EE1 RID: 3809
		private readonly ColumnMap m_columnMap;

		// Token: 0x04000EE2 RID: 3810
		private readonly VarList m_flattenedElementVars;

		// Token: 0x04000EE3 RID: 3811
		private readonly VarVec m_keys;

		// Token: 0x04000EE4 RID: 3812
		private readonly List<SortKey> m_sortKeys;

		// Token: 0x04000EE5 RID: 3813
		private readonly object m_discriminatorValue;
	}
}
