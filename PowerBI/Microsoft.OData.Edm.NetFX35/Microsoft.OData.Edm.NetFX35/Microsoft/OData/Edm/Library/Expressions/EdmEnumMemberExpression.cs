using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x0200000E RID: 14
	public class EdmEnumMemberExpression : EdmElement, IEdmEnumMemberExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000256D File Offset: 0x0000076D
		public EdmEnumMemberExpression(params IEdmEnumMember[] enumMembers)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember[]>(enumMembers, "referencedEnumMember");
			this.enumMembers = Enumerable.ToList<IEdmEnumMember>(enumMembers);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000258D File Offset: 0x0000078D
		public IEnumerable<IEdmEnumMember> EnumMembers
		{
			get
			{
				return this.enumMembers;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002595 File Offset: 0x00000795
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x04000014 RID: 20
		private readonly List<IEdmEnumMember> enumMembers;
	}
}
