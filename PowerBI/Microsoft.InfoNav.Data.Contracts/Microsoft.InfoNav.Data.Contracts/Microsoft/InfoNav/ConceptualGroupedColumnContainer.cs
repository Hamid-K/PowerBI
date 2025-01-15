using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000023 RID: 35
	[ImmutableObject(true)]
	internal sealed class ConceptualGroupedColumnContainer
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000278C File Offset: 0x0000098C
		internal ConceptualGroupedColumnContainer(IConceptualColumn column)
		{
			this._column = column;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000279B File Offset: 0x0000099B
		internal ConceptualGroupedColumnContainer(IConceptualHierarchyLevel hierarchyLevel)
		{
			this._hierarchyLevel = hierarchyLevel;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000027AA File Offset: 0x000009AA
		internal IConceptualColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000027B2 File Offset: 0x000009B2
		internal IConceptualHierarchyLevel HierarchyLevel
		{
			get
			{
				return this._hierarchyLevel;
			}
		}

		// Token: 0x040000B4 RID: 180
		private readonly IConceptualColumn _column;

		// Token: 0x040000B5 RID: 181
		private readonly IConceptualHierarchyLevel _hierarchyLevel;
	}
}
