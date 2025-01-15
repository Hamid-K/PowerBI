using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200009E RID: 158
	public struct ExpressionCompilationFlags
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00019CD6 File Offset: 0x00017ED6
		private ExpressionCompilationFlags(ExpressionCompilationFlags.InternalFlags flags)
		{
			this.m_flags = flags;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00019CDF File Offset: 0x00017EDF
		internal bool Named
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.Named) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00019CEC File Offset: 0x00017EEC
		internal bool QueryResult
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.QueryResult) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00019CF9 File Offset: 0x00017EF9
		internal bool MustFloat
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.MustFloat) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00019D06 File Offset: 0x00017F06
		internal bool IsModelAttribute
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.IsModelAttribute) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00019D13 File Offset: 0x00017F13
		internal bool IsCalculatedAttribute
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.IsCalculatedAttribute) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00019D21 File Offset: 0x00017F21
		internal bool IsMeasure
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.IsMeasure) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00019D2F File Offset: 0x00017F2F
		internal bool IsFilter
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.IsFilter) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00019D3D File Offset: 0x00017F3D
		internal bool IsGroupingExpression
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.IsGroupingExpression) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00019D4E File Offset: 0x00017F4E
		internal bool AllowPrecompiled
		{
			get
			{
				return (this.m_flags & ExpressionCompilationFlags.InternalFlags.AllowPrecompiled) > ExpressionCompilationFlags.InternalFlags.None;
			}
		}

		// Token: 0x040003A1 RID: 929
		public static readonly ExpressionCompilationFlags ScalarAttribute = new ExpressionCompilationFlags((ExpressionCompilationFlags.InternalFlags)136);

		// Token: 0x040003A2 RID: 930
		public static readonly ExpressionCompilationFlags AggregateAttribute = new ExpressionCompilationFlags((ExpressionCompilationFlags.InternalFlags)140);

		// Token: 0x040003A3 RID: 931
		public static readonly ExpressionCompilationFlags GroupingExpression = new ExpressionCompilationFlags((ExpressionCompilationFlags.InternalFlags)259);

		// Token: 0x040003A4 RID: 932
		public static readonly ExpressionCompilationFlags GroupingDetail = new ExpressionCompilationFlags((ExpressionCompilationFlags.InternalFlags)3);

		// Token: 0x040003A5 RID: 933
		public static readonly ExpressionCompilationFlags Measure = new ExpressionCompilationFlags((ExpressionCompilationFlags.InternalFlags)39);

		// Token: 0x040003A6 RID: 934
		public static readonly ExpressionCompilationFlags CalculatedAttribute = new ExpressionCompilationFlags((ExpressionCompilationFlags.InternalFlags)145);

		// Token: 0x040003A7 RID: 935
		public static readonly ExpressionCompilationFlags Filter = new ExpressionCompilationFlags(ExpressionCompilationFlags.InternalFlags.IsFilter);

		// Token: 0x040003A8 RID: 936
		public static readonly ExpressionCompilationFlags None = new ExpressionCompilationFlags(ExpressionCompilationFlags.InternalFlags.None);

		// Token: 0x040003A9 RID: 937
		private readonly ExpressionCompilationFlags.InternalFlags m_flags;

		// Token: 0x02000196 RID: 406
		private enum InternalFlags
		{
			// Token: 0x040006D2 RID: 1746
			None,
			// Token: 0x040006D3 RID: 1747
			Named,
			// Token: 0x040006D4 RID: 1748
			QueryResult,
			// Token: 0x040006D5 RID: 1749
			MustFloat = 4,
			// Token: 0x040006D6 RID: 1750
			IsModelAttribute = 8,
			// Token: 0x040006D7 RID: 1751
			IsCalculatedAttribute = 16,
			// Token: 0x040006D8 RID: 1752
			IsMeasure = 32,
			// Token: 0x040006D9 RID: 1753
			IsFilter = 64,
			// Token: 0x040006DA RID: 1754
			AllowPrecompiled = 128,
			// Token: 0x040006DB RID: 1755
			IsGroupingExpression = 256
		}
	}
}
