using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EB RID: 235
	public class EdmEnumMemberExpression : EdmElement, IEdmEnumMemberExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00011F0B File Offset: 0x0001010B
		public EdmEnumMemberExpression(params IEdmEnumMember[] enumMembers)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember[]>(enumMembers, "referencedEnumMember");
			this.enumMembers = enumMembers.ToList<IEdmEnumMember>();
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00011F2B File Offset: 0x0001012B
		public IEnumerable<IEdmEnumMember> EnumMembers
		{
			get
			{
				return this.enumMembers;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00011F33 File Offset: 0x00010133
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x04000309 RID: 777
		private readonly List<IEdmEnumMember> enumMembers;
	}
}
