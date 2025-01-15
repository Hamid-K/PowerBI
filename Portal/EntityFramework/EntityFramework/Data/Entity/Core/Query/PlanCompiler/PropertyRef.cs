using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200035F RID: 863
	internal abstract class PropertyRef
	{
		// Token: 0x06002A0A RID: 10762 RVA: 0x00089082 File Offset: 0x00087282
		internal virtual PropertyRef CreateNestedPropertyRef(PropertyRef p)
		{
			return new NestedPropertyRef(p, this);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0008908B File Offset: 0x0008728B
		internal PropertyRef CreateNestedPropertyRef(EdmMember p)
		{
			return this.CreateNestedPropertyRef(new SimplePropertyRef(p));
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00089099 File Offset: 0x00087299
		internal PropertyRef CreateNestedPropertyRef(RelProperty p)
		{
			return this.CreateNestedPropertyRef(new RelPropertyRef(p));
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000890A7 File Offset: 0x000872A7
		public override string ToString()
		{
			return "";
		}
	}
}
