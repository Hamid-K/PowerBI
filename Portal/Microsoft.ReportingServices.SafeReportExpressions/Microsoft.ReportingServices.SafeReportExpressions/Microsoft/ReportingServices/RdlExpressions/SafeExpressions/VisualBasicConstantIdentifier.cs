using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000026 RID: 38
	internal sealed class VisualBasicConstantIdentifier
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00003434 File Offset: 0x00001634
		public bool IsIdentifierVisualBasicConstant(string identifierName)
		{
			string text = identifierName.ToUpperInvariant();
			return VisualBasicConstantIdentifier.VisualBasicConstants.Contains(text);
		}

		// Token: 0x04000036 RID: 54
		private static readonly HashSet<string> VisualBasicConstants = new HashSet<string> { "VBCR", "VBLF", "VBCRLF", "VBBINARYCOMPARE", "VBTEXTCOMPARE" };
	}
}
