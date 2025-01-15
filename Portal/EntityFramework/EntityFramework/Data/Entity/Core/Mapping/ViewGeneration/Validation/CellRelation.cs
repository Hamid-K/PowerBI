using System;
using System.Data.Entity.Core.Common.Utils;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000578 RID: 1400
	internal abstract class CellRelation : InternalBase
	{
		// Token: 0x060043DA RID: 17370 RVA: 0x000ED0DC File Offset: 0x000EB2DC
		protected CellRelation(int cellNumber)
		{
			this.m_cellNumber = cellNumber;
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x000ED0EB File Offset: 0x000EB2EB
		internal int CellNumber
		{
			get
			{
				return this.m_cellNumber;
			}
		}

		// Token: 0x060043DC RID: 17372
		protected abstract int GetHash();

		// Token: 0x04001881 RID: 6273
		internal int m_cellNumber;
	}
}
