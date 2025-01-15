using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F0 RID: 240
	internal sealed class FilterMergeInfo
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x000250BD File Offset: 0x000232BD
		internal FilterMergeInfo(Filter filterToMerge, DataShape targetDataShape)
		{
			this.m_filterToMerge = filterToMerge;
			this.m_targetDataShape = targetDataShape;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000250D3 File Offset: 0x000232D3
		internal Filter FilterToMerge
		{
			get
			{
				return this.m_filterToMerge;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000250DB File Offset: 0x000232DB
		internal DataShape TargetDataShape
		{
			get
			{
				return this.m_targetDataShape;
			}
		}

		// Token: 0x04000496 RID: 1174
		private readonly Filter m_filterToMerge;

		// Token: 0x04000497 RID: 1175
		private readonly DataShape m_targetDataShape;
	}
}
