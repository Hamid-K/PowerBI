using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003DD RID: 989
	internal class ScalarColumnMap : SimpleColumnMap
	{
		// Token: 0x06002EE1 RID: 12001 RVA: 0x0009522A File Offset: 0x0009342A
		internal ScalarColumnMap(TypeUsage type, string name, int commandId, int columnPos)
			: base(type, name)
		{
			this.m_commandId = commandId;
			this.m_columnPos = columnPos;
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x00095243 File Offset: 0x00093443
		internal int CommandId
		{
			get
			{
				return this.m_commandId;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x0009524B File Offset: 0x0009344B
		internal int ColumnPos
		{
			get
			{
				return this.m_columnPos;
			}
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x00095253 File Offset: 0x00093453
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x0009525D File Offset: 0x0009345D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x00095267 File Offset: 0x00093467
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "S({0},{1})", new object[] { this.CommandId, this.ColumnPos });
		}

		// Token: 0x04000FD0 RID: 4048
		private readonly int m_commandId;

		// Token: 0x04000FD1 RID: 4049
		private readonly int m_columnPos;
	}
}
