using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000FE RID: 254
	public interface IEdmEnumMemberExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000762 RID: 1890
		IEnumerable<IEdmEnumMember> EnumMembers { get; }
	}
}
