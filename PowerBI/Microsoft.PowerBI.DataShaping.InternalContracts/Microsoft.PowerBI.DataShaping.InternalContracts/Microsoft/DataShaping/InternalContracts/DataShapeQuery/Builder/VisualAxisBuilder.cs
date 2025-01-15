using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E3 RID: 227
	internal class VisualAxisBuilder<TParent> : BuilderBase<VisualAxis, TParent>
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0000DA57 File Offset: 0x0000BC57
		internal VisualAxisBuilder(TParent parent, VisualAxis activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000DA61 File Offset: 0x0000BC61
		public VisualAxisBuilder<TParent> WithName(string name)
		{
			base.ActiveObject.Name = name;
			return this;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public VisualAxisBuilder<TParent> WithGroup(string targetDataMemberId)
		{
			Expression expression = targetDataMemberId.StructureReference();
			if (base.ActiveObject.Groups == null)
			{
				base.ActiveObject.Groups = new List<VisualAxisGroup>();
			}
			base.ActiveObject.Groups.Add(new VisualAxisGroup
			{
				Member = expression
			});
			return this;
		}
	}
}
