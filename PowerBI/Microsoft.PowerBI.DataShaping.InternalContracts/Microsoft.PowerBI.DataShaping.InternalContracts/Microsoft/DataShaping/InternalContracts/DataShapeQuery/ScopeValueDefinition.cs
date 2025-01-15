using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200006C RID: 108
	internal sealed class ScopeValueDefinition : IIdentifiable
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000060DB File Offset: 0x000042DB
		// (set) Token: 0x0600026B RID: 619 RVA: 0x000060E3 File Offset: 0x000042E3
		public Identifier Id { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000060EC File Offset: 0x000042EC
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000060F4 File Offset: 0x000042F4
		public Expression Value { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000060FD File Offset: 0x000042FD
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.ScopeValueDefinition;
			}
		}
	}
}
