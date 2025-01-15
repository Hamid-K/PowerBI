using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200000A RID: 10
	public interface IEdmEnumMemberExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000024 RID: 36
		IEnumerable<IEdmEnumMember> EnumMembers { get; }
	}
}
