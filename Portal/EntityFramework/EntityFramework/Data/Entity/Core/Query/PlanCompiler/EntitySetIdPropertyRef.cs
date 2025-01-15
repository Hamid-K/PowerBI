using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200033E RID: 830
	internal class EntitySetIdPropertyRef : PropertyRef
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x00074375 File Offset: 0x00072575
		private EntitySetIdPropertyRef()
		{
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0007437D File Offset: 0x0007257D
		public override string ToString()
		{
			return "ENTITYSETID";
		}

		// Token: 0x04000DCC RID: 3532
		internal static EntitySetIdPropertyRef Instance = new EntitySetIdPropertyRef();
	}
}
