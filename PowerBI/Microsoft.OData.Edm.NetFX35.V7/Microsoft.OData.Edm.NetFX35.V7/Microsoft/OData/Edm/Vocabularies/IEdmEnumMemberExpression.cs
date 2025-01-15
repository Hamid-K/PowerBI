using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000105 RID: 261
	public interface IEdmEnumMemberExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000735 RID: 1845
		IEnumerable<IEdmEnumMember> EnumMembers { get; }
	}
}
