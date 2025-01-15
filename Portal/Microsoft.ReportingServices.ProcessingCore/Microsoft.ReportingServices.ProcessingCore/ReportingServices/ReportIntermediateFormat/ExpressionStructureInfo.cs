using System;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004CB RID: 1227
	internal sealed class ExpressionStructureInfo
	{
		// Token: 0x06003E45 RID: 15941 RVA: 0x0010A4E8 File Offset: 0x001086E8
		public ExpressionStructureInfo(ExpressionSyntax expressionSyntax)
		{
			this.m_expressionSyntax = expressionSyntax;
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x0010A4F7 File Offset: 0x001086F7
		public ExpressionSyntax ExpressionSyntax
		{
			get
			{
				return this.m_expressionSyntax;
			}
		}

		// Token: 0x04001D02 RID: 7426
		private readonly ExpressionSyntax m_expressionSyntax;
	}
}
