using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200033F RID: 831
	internal class ExtentPair
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x00074390 File Offset: 0x00072590
		internal EntitySetBase Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00074398 File Offset: 0x00072598
		internal EntitySetBase Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000743A0 File Offset: 0x000725A0
		public override bool Equals(object obj)
		{
			ExtentPair extentPair = obj as ExtentPair;
			return extentPair != null && extentPair.Left.Equals(this.Left) && extentPair.Right.Equals(this.Right);
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000743DD File Offset: 0x000725DD
		public override int GetHashCode()
		{
			return (this.Left.GetHashCode() << 4) ^ this.Right.GetHashCode();
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000743F8 File Offset: 0x000725F8
		internal ExtentPair(EntitySetBase left, EntitySetBase right)
		{
			this.m_left = left;
			this.m_right = right;
		}

		// Token: 0x04000DCD RID: 3533
		private readonly EntitySetBase m_left;

		// Token: 0x04000DCE RID: 3534
		private readonly EntitySetBase m_right;
	}
}
