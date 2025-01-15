using System;
using System.Collections.Immutable;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200017F RID: 383
	public interface IMeasureLogicalIdentityAnnotation
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000781 RID: 1921
		IImmutableSet<IConceptualEntity> LogicalEntities { get; }
	}
}
