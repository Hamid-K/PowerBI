using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D1 RID: 977
	internal class RecordColumnMap : StructuredColumnMap
	{
		// Token: 0x06002EAB RID: 11947 RVA: 0x00094CC7 File Offset: 0x00092EC7
		internal RecordColumnMap(TypeUsage type, string name, ColumnMap[] properties, SimpleColumnMap nullSentinel)
			: base(type, name, properties)
		{
			this.m_nullSentinel = nullSentinel;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x00094CDA File Offset: 0x00092EDA
		internal override SimpleColumnMap NullSentinel
		{
			get
			{
				return this.m_nullSentinel;
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x00094CE2 File Offset: 0x00092EE2
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x00094CEC File Offset: 0x00092EEC
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x04000FBD RID: 4029
		private readonly SimpleColumnMap m_nullSentinel;
	}
}
