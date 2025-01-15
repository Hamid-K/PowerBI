using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000362 RID: 866
	internal class RelPropertyRef : PropertyRef
	{
		// Token: 0x06002A1C RID: 10780 RVA: 0x000893B3 File Offset: 0x000875B3
		internal RelPropertyRef(RelProperty property)
		{
			this.m_property = property;
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000893C2 File Offset: 0x000875C2
		internal RelProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000893CC File Offset: 0x000875CC
		public override bool Equals(object obj)
		{
			RelPropertyRef relPropertyRef = obj as RelPropertyRef;
			return relPropertyRef != null && this.m_property.Equals(relPropertyRef.m_property);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000893F6 File Offset: 0x000875F6
		public override int GetHashCode()
		{
			return this.m_property.GetHashCode();
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00089403 File Offset: 0x00087603
		public override string ToString()
		{
			return this.m_property.ToString();
		}

		// Token: 0x04000E70 RID: 3696
		private readonly RelProperty m_property;
	}
}
