using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BCD RID: 7117
	public sealed class ModuleMemberSyntaxNode : RangeSyntaxNode, ISectionMember, ISyntaxNode
	{
		// Token: 0x0600B19D RID: 45469 RVA: 0x00243D58 File Offset: 0x00241F58
		public ModuleMemberSyntaxNode(IRecordExpression attribute, bool export, Identifier name, IExpression value, TokenRange range)
			: base(range)
		{
			this.attribute = attribute;
			this.export = export;
			this.name = name;
			this.value = value;
		}

		// Token: 0x17002CA0 RID: 11424
		// (get) Token: 0x0600B19E RID: 45470 RVA: 0x00243D7F File Offset: 0x00241F7F
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002CA1 RID: 11425
		// (get) Token: 0x0600B19F RID: 45471 RVA: 0x00243D87 File Offset: 0x00241F87
		public IRecordExpression Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x17002CA2 RID: 11426
		// (get) Token: 0x0600B1A0 RID: 45472 RVA: 0x00243D8F File Offset: 0x00241F8F
		public bool Export
		{
			get
			{
				return this.export;
			}
		}

		// Token: 0x17002CA3 RID: 11427
		// (get) Token: 0x0600B1A1 RID: 45473 RVA: 0x00243D97 File Offset: 0x00241F97
		public IExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04005B0B RID: 23307
		private Identifier name;

		// Token: 0x04005B0C RID: 23308
		private IRecordExpression attribute;

		// Token: 0x04005B0D RID: 23309
		private bool export;

		// Token: 0x04005B0E RID: 23310
		private IExpression value;
	}
}
