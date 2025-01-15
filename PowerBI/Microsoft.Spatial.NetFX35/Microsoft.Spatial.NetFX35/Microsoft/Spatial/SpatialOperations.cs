using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000044 RID: 68
	public abstract class SpatialOperations
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00005177 File Offset: 0x00003377
		public virtual double Distance(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000517E File Offset: 0x0000337E
		public virtual double Distance(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005185 File Offset: 0x00003385
		public virtual double Length(Geometry operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000518C File Offset: 0x0000338C
		public virtual double Length(Geography operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005193 File Offset: 0x00003393
		public virtual bool Intersects(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000519A File Offset: 0x0000339A
		public virtual bool Intersects(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}
	}
}
