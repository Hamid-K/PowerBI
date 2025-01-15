using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000080 RID: 128
	public sealed class ModelRoleCollection : NamedMetadataObjectCollection<ModelRole, Model>
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x00041AB2 File Offset: 0x0003FCB2
		internal ModelRoleCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Role, parent, comparer, true)
		{
		}
	}
}
