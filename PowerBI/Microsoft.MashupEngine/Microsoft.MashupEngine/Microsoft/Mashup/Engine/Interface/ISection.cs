using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000DD RID: 221
	public interface ISection : ISyntaxNode, IDeclarator
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000349 RID: 841
		Identifier SectionName { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600034A RID: 842
		IRecordExpression Attribute { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600034B RID: 843
		IList<ISectionMember> Members { get; }
	}
}
