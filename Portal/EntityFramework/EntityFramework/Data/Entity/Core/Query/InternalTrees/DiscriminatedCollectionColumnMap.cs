using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200039A RID: 922
	internal class DiscriminatedCollectionColumnMap : CollectionColumnMap
	{
		// Token: 0x06002CE5 RID: 11493 RVA: 0x0009022D File Offset: 0x0008E42D
		internal DiscriminatedCollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys, SimpleColumnMap discriminator, object discriminatorValue)
			: base(type, name, elementMap, keys, foreignKeys)
		{
			this.m_discriminator = discriminator;
			this.m_discriminatorValue = discriminatorValue;
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x0009024C File Offset: 0x0008E44C
		internal SimpleColumnMap Discriminator
		{
			get
			{
				return this.m_discriminator;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x00090254 File Offset: 0x0008E454
		internal object DiscriminatorValue
		{
			get
			{
				return this.m_discriminatorValue;
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x0009025C File Offset: 0x0008E45C
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x00090266 File Offset: 0x0008E466
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x00090270 File Offset: 0x0008E470
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "M{{{0}}}", new object[] { base.Element });
		}

		// Token: 0x04000F14 RID: 3860
		private readonly SimpleColumnMap m_discriminator;

		// Token: 0x04000F15 RID: 3861
		private readonly object m_discriminatorValue;
	}
}
