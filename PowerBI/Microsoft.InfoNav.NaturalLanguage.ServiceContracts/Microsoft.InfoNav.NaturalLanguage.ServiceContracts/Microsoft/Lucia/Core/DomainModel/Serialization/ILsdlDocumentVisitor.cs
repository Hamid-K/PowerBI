using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D1 RID: 465
	public interface ILsdlDocumentVisitor : IRelationshipVisitor, IPhrasingVisitor
	{
		// Token: 0x060009FF RID: 2559
		void Visit(LsdlDocument lsdlDocument);

		// Token: 0x06000A00 RID: 2560
		void Visit(Entity entity);

		// Token: 0x06000A01 RID: 2561
		void Visit(Term term);

		// Token: 0x06000A02 RID: 2562
		void Visit(GlobalSubstitution globalSubstitution);

		// Token: 0x06000A03 RID: 2563
		void Visit(Example example);

		// Token: 0x06000A04 RID: 2564
		void Visit(AgentProperties agentProperties);
	}
}
