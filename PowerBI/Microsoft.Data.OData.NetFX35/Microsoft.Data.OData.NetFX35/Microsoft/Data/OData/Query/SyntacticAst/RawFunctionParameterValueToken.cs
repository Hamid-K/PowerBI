using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000014 RID: 20
	internal sealed class RawFunctionParameterValueToken : QueryToken
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000363E File Offset: 0x0000183E
		public RawFunctionParameterValueToken(string rawText)
		{
			this.RawText = rawText;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000364D File Offset: 0x0000184D
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003655 File Offset: 0x00001855
		public string RawText { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000365E File Offset: 0x0000185E
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.RawFunctionParameterValue;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003662 File Offset: 0x00001862
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
