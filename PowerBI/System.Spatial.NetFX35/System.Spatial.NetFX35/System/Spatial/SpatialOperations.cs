using System;

namespace System.Spatial
{
	// Token: 0x02000043 RID: 67
	public abstract class SpatialOperations
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00005207 File Offset: 0x00003407
		public virtual double Distance(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000520E File Offset: 0x0000340E
		public virtual double Distance(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005215 File Offset: 0x00003415
		public virtual double Length(Geometry operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000521C File Offset: 0x0000341C
		public virtual double Length(Geography operand)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005223 File Offset: 0x00003423
		public virtual bool Intersects(Geometry operand1, Geometry operand2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000522A File Offset: 0x0000342A
		public virtual bool Intersects(Geography operand1, Geography operand2)
		{
			throw new NotImplementedException();
		}
	}
}
