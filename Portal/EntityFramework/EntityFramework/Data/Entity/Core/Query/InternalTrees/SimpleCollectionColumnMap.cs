using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E4 RID: 996
	internal class SimpleCollectionColumnMap : CollectionColumnMap
	{
		// Token: 0x06002F03 RID: 12035 RVA: 0x000953D4 File Offset: 0x000935D4
		internal SimpleCollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys)
			: base(type, name, elementMap, keys, foreignKeys)
		{
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000953E3 File Offset: 0x000935E3
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000953ED File Offset: 0x000935ED
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}
	}
}
