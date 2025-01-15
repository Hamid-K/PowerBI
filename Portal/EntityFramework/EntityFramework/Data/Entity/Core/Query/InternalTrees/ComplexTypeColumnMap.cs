using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000390 RID: 912
	internal class ComplexTypeColumnMap : TypedColumnMap
	{
		// Token: 0x06002CB1 RID: 11441 RVA: 0x0008FF6C File Offset: 0x0008E16C
		internal ComplexTypeColumnMap(TypeUsage type, string name, ColumnMap[] properties, SimpleColumnMap nullSentinel)
			: base(type, name, properties)
		{
			this.m_nullSentinel = nullSentinel;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x0008FF7F File Offset: 0x0008E17F
		internal override SimpleColumnMap NullSentinel
		{
			get
			{
				return this.m_nullSentinel;
			}
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x0008FF87 File Offset: 0x0008E187
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x0008FF91 File Offset: 0x0008E191
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x0008FF9B File Offset: 0x0008E19B
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "C{0}", new object[] { base.ToString() });
		}

		// Token: 0x04000F04 RID: 3844
		private readonly SimpleColumnMap m_nullSentinel;
	}
}
