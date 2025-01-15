using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000031 RID: 49
	public abstract class ConceptualSchemaVisitor
	{
		// Token: 0x060000A8 RID: 168
		public abstract void Visit(IConceptualSchema schema);

		// Token: 0x060000A9 RID: 169
		public abstract void Visit(IConceptualEntity entity);

		// Token: 0x060000AA RID: 170
		public abstract void Visit(IConceptualProperty property);

		// Token: 0x060000AB RID: 171
		public abstract void Visit(IConceptualHierarchy hierarchy);

		// Token: 0x060000AC RID: 172
		public abstract void Visit(IConceptualHierarchyLevel hierarchyLevel);
	}
}
