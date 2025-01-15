using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000B4 RID: 180
	public abstract class ParagraphExprHost : StyleExprHost
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00003924 File Offset: 0x00001B24
		internal IList<TextRunExprHost> TextRunHostsRemotable
		{
			get
			{
				return this.m_textRunHostsRemotable;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000392C File Offset: 0x00001B2C
		public virtual object LeftIndentExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000392F File Offset: 0x00001B2F
		public virtual object RightIndentExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00003932 File Offset: 0x00001B32
		public virtual object HangingIndentExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00003935 File Offset: 0x00001B35
		public virtual object SpaceBeforeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00003938 File Offset: 0x00001B38
		public virtual object SpaceAfterExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000393B File Offset: 0x00001B3B
		public virtual object ListStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000393E File Offset: 0x00001B3E
		public virtual object ListLevelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400012D RID: 301
		[CLSCompliant(false)]
		protected IList<TextRunExprHost> m_textRunHostsRemotable;
	}
}
