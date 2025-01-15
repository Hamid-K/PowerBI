using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D2 RID: 466
	public interface IRelationshipVisitor : IPhrasingVisitor
	{
		// Token: 0x06000A05 RID: 2565
		void Visit(Relationship relationship);

		// Token: 0x06000A06 RID: 2566
		void Visit(Role role);

		// Token: 0x06000A07 RID: 2567
		void Visit(RoleReference roleReference);

		// Token: 0x06000A08 RID: 2568
		void Visit(EntityReference entityReference);

		// Token: 0x06000A09 RID: 2569
		void Visit(Condition condition);

		// Token: 0x06000A0A RID: 2570
		void Visit(Phrasing phrasing);
	}
}
