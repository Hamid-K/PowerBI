using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003BF RID: 959
	internal sealed class NewRecordOp : ScalarOp
	{
		// Token: 0x06002DDF RID: 11743 RVA: 0x000924AF File Offset: 0x000906AF
		internal NewRecordOp(TypeUsage type)
			: base(OpType.NewRecord, type)
		{
			this.m_fields = new List<EdmProperty>(TypeHelpers.GetEdmType<RowType>(type).Properties);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000924D0 File Offset: 0x000906D0
		internal NewRecordOp(TypeUsage type, List<EdmProperty> fields)
			: base(OpType.NewRecord, type)
		{
			this.m_fields = fields;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000924E2 File Offset: 0x000906E2
		private NewRecordOp()
			: base(OpType.NewRecord)
		{
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000924EC File Offset: 0x000906EC
		internal bool GetFieldPosition(EdmProperty field, out int fieldPosition)
		{
			fieldPosition = 0;
			for (int i = 0; i < this.m_fields.Count; i++)
			{
				if (this.m_fields[i] == field)
				{
					fieldPosition = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x00092527 File Offset: 0x00090727
		internal List<EdmProperty> Properties
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x0009252F File Offset: 0x0009072F
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x00092539 File Offset: 0x00090739
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F56 RID: 3926
		private readonly List<EdmProperty> m_fields;

		// Token: 0x04000F57 RID: 3927
		internal static readonly NewRecordOp Pattern = new NewRecordOp();
	}
}
