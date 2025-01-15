using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200008B RID: 139
	public abstract class PointerCapExprHost : StyleExprHost
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00003467 File Offset: 0x00001667
		public virtual object OnTopExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000346A File Offset: 0x0000166A
		public virtual object ReflectionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000346D File Offset: 0x0000166D
		public virtual object CapStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00003470 File Offset: 0x00001670
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00003473 File Offset: 0x00001673
		public virtual object WidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000E7 RID: 231
		public CapImageExprHost CapImageHost;
	}
}
