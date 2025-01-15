using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000044 RID: 68
	public abstract class SpatialOperations
	{
		// Token: 0x060001FA RID: 506 RVA: 0x000050C9 File Offset: 0x000032C9
		public virtual double Distance(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000050C9 File Offset: 0x000032C9
		public virtual double Distance(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000050C9 File Offset: 0x000032C9
		public virtual double Length(Geometry operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000050C9 File Offset: 0x000032C9
		public virtual double Length(Geography operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000050C9 File Offset: 0x000032C9
		public virtual bool Intersects(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000050C9 File Offset: 0x000032C9
		public virtual bool Intersects(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}
	}
}
