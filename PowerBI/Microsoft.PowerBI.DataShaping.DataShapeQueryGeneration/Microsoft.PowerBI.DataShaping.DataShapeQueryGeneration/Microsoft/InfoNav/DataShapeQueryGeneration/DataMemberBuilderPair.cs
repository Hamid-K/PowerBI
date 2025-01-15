using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000058 RID: 88
	internal sealed class DataMemberBuilderPair
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		internal DataMemberBuilderPair(DataMember @static, DataMember dynamic, QueryMember queryMember)
		{
			this.Static = ((@static == null) ? null : new DataMemberBuilder(@static));
			this.Dynamic = new DataMemberBuilder(dynamic);
			this.GroupBuilder = this.Dynamic.WithGroup(queryMember.Group.SuppressSortByMeasureRollup);
			this.QueryMember = queryMember;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000E11C File Offset: 0x0000C31C
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000E124 File Offset: 0x0000C324
		internal DataMemberBuilder Static { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000E12D File Offset: 0x0000C32D
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000E135 File Offset: 0x0000C335
		internal DataMemberBuilder Dynamic { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000E13E File Offset: 0x0000C33E
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000E146 File Offset: 0x0000C346
		internal GroupBuilder<DataMemberBuilder<DataMember>> GroupBuilder { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000E14F File Offset: 0x0000C34F
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000E157 File Offset: 0x0000C357
		internal QueryMember QueryMember { get; private set; }
	}
}
