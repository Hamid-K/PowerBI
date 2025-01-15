using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200003F RID: 63
	public abstract class SpatialOperations
	{
		// Token: 0x06000184 RID: 388 RVA: 0x000043F5 File Offset: 0x000025F5
		public virtual double Distance(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000043F5 File Offset: 0x000025F5
		public virtual double Distance(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000043F5 File Offset: 0x000025F5
		public virtual double Length(Geometry operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000043F5 File Offset: 0x000025F5
		public virtual double Length(Geography operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000043F5 File Offset: 0x000025F5
		public virtual bool Intersects(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000043F5 File Offset: 0x000025F5
		public virtual bool Intersects(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}
	}
}
