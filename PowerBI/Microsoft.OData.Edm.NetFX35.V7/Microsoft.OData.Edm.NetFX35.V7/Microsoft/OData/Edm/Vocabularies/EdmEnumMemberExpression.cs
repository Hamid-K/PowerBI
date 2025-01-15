using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F2 RID: 242
	public class EdmEnumMemberExpression : EdmElement, IEdmEnumMemberExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x00013A27 File Offset: 0x00011C27
		public EdmEnumMemberExpression(params IEdmEnumMember[] enumMembers)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember[]>(enumMembers, "referencedEnumMember");
			this.enumMembers = Enumerable.ToList<IEdmEnumMember>(enumMembers);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00013A47 File Offset: 0x00011C47
		public IEnumerable<IEdmEnumMember> EnumMembers
		{
			get
			{
				return this.enumMembers;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00013A4F File Offset: 0x00011C4F
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x04000415 RID: 1045
		private readonly List<IEdmEnumMember> enumMembers;
	}
}
