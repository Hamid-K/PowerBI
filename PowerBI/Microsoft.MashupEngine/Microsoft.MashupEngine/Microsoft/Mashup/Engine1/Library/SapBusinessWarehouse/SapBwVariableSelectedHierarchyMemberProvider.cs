using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000503 RID: 1283
	internal class SapBwVariableSelectedHierarchyMemberProvider : SapBwVariableHierarchyNodeMemberProvider
	{
		// Token: 0x060029CD RID: 10701 RVA: 0x0007D26B File Offset: 0x0007B46B
		public SapBwVariableSelectedHierarchyMemberProvider(ISapBwService service, SapBwMdxCube mdxCube, SapBwVariable variable, string selectedHierarchy, bool allowNonAssigned)
			: base(service, mdxCube, variable, allowNonAssigned)
		{
			this.selectedHierarchy = selectedHierarchy;
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x0007D280 File Offset: 0x0007B480
		protected override Dictionary<string, MdxHierarchy> GetHierarchies(string variableHierarchy, out string defaultHierarchyUniqueName)
		{
			return base.GetHierarchies(this.selectedHierarchy, out defaultHierarchyUniqueName);
		}

		// Token: 0x0400122F RID: 4655
		private readonly string selectedHierarchy;
	}
}
