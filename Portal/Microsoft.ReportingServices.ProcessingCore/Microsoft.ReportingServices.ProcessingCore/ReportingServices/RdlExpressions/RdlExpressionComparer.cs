using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200056B RID: 1387
	internal sealed class RdlExpressionComparer : IComparer<ExpressionInfo>
	{
		// Token: 0x0600509E RID: 20638 RVA: 0x00152C0E File Offset: 0x00150E0E
		private RdlExpressionComparer()
		{
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x00152C18 File Offset: 0x00150E18
		public int Compare(ExpressionInfo x, ExpressionInfo y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			if (x.Type == ExpressionInfo.Types.Field && y.Type == ExpressionInfo.Types.Field)
			{
				return x.FieldIndex - y.FieldIndex;
			}
			return StringComparer.Ordinal.Compare(x.OriginalText, y.OriginalText);
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x00152C6D File Offset: 0x00150E6D
		public bool Equals(ExpressionInfo x, ExpressionInfo y)
		{
			return this.Compare(x, y) == 0;
		}

		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x060050A1 RID: 20641 RVA: 0x00152C7A File Offset: 0x00150E7A
		public static RdlExpressionComparer Instance
		{
			get
			{
				return RdlExpressionComparer.m_instance;
			}
		}

		// Token: 0x04002899 RID: 10393
		private static readonly RdlExpressionComparer m_instance = new RdlExpressionComparer();
	}
}
