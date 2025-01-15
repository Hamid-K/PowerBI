using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BCE RID: 7118
	public sealed class ModuleSyntaxNode : RangeSyntaxNode, ISection, ISyntaxNode, IDeclarator
	{
		// Token: 0x0600B1A2 RID: 45474 RVA: 0x00243D9F File Offset: 0x00241F9F
		public ModuleSyntaxNode(IRecordExpression attribute, Identifier sectionName, IList<ISectionMember> members, TokenRange range)
			: base(range)
		{
			this.sectionName = sectionName;
			this.attribute = attribute;
			this.members = members;
		}

		// Token: 0x17002CA4 RID: 11428
		// (get) Token: 0x0600B1A3 RID: 45475 RVA: 0x00243DBE File Offset: 0x00241FBE
		public Identifier SectionName
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x17002CA5 RID: 11429
		// (get) Token: 0x0600B1A4 RID: 45476 RVA: 0x00243DC6 File Offset: 0x00241FC6
		public IRecordExpression Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x17002CA6 RID: 11430
		// (get) Token: 0x0600B1A5 RID: 45477 RVA: 0x00243DCE File Offset: 0x00241FCE
		public IList<ISectionMember> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x17002CA7 RID: 11431
		// (get) Token: 0x0600B1A6 RID: 45478 RVA: 0x00243DD6 File Offset: 0x00241FD6
		int IDeclarator.Count
		{
			get
			{
				return this.members.Count;
			}
		}

		// Token: 0x17002CA8 RID: 11432
		Identifier IDeclarator.this[int index]
		{
			get
			{
				return this.members[index].Name;
			}
		}

		// Token: 0x04005B0F RID: 23311
		private Identifier sectionName;

		// Token: 0x04005B10 RID: 23312
		private IRecordExpression attribute;

		// Token: 0x04005B11 RID: 23313
		private IList<ISectionMember> members;
	}
}
