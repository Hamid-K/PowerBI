using System;
using System.ComponentModel.DataAnnotations;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000131 RID: 305
	internal class EntityValidationContext
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x0003613C File Offset: 0x0003433C
		public EntityValidationContext(InternalEntityEntry entityEntry, ValidationContext externalValidationContext)
		{
			this._entityEntry = entityEntry;
			this.ExternalValidationContext = externalValidationContext;
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00036152 File Offset: 0x00034352
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x0003615A File Offset: 0x0003435A
		public ValidationContext ExternalValidationContext { get; private set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00036163 File Offset: 0x00034363
		public InternalEntityEntry InternalEntity
		{
			get
			{
				return this._entityEntry;
			}
		}

		// Token: 0x040009B9 RID: 2489
		private readonly InternalEntityEntry _entityEntry;
	}
}
