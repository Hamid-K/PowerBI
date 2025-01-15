using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000024 RID: 36
	[DebuggerDisplay("{Kind}: [{Text}]")]
	internal struct ExpressionToken
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00006A2F File Offset: 0x00004C2F
		public ExpressionToken(ExpressionTokenKind kind, string text)
		{
			this.m_kind = kind;
			this.m_text = text;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006A3F File Offset: 0x00004C3F
		public ExpressionTokenKind Kind
		{
			get
			{
				return this.m_kind;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006A47 File Offset: 0x00004C47
		public string Text
		{
			get
			{
				return this.m_text;
			}
		}

		// Token: 0x0400005C RID: 92
		private readonly ExpressionTokenKind m_kind;

		// Token: 0x0400005D RID: 93
		private readonly string m_text;
	}
}
