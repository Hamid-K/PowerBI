using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000073 RID: 115
	internal sealed class GroupAndSortingContext
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x00014DEC File Offset: 0x00012FEC
		internal GroupAndSortingContext(DataMember scope, ReadOnlyCollection<GroupReference> groupReferences, List<QueryBuilder.SortDetail> sortDetails, bool isProjected)
		{
			this.m_scope = scope;
			this.m_groupReferences = groupReferences;
			if (sortDetails != null)
			{
				this.m_sortDetails = sortDetails.AsReadOnly();
			}
			this.m_isProjected = isProjected;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00014E19 File Offset: 0x00013019
		internal DataMember Scope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00014E21 File Offset: 0x00013021
		internal ReadOnlyCollection<GroupReference> GroupReferences
		{
			get
			{
				return this.m_groupReferences;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00014E29 File Offset: 0x00013029
		internal ReadOnlyCollection<QueryBuilder.SortDetail> SortDetails
		{
			get
			{
				return this.m_sortDetails;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00014E31 File Offset: 0x00013031
		public bool IsProjected
		{
			get
			{
				return this.m_isProjected;
			}
		}

		// Token: 0x040002E7 RID: 743
		private readonly DataMember m_scope;

		// Token: 0x040002E8 RID: 744
		private readonly ReadOnlyCollection<GroupReference> m_groupReferences;

		// Token: 0x040002E9 RID: 745
		private readonly ReadOnlyCollection<QueryBuilder.SortDetail> m_sortDetails;

		// Token: 0x040002EA RID: 746
		private readonly bool m_isProjected;
	}
}
