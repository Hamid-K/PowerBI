using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000064 RID: 100
	internal sealed class Expression
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000B7F1 File Offset: 0x000099F1
		internal Expression(string expressionText)
		{
			this._expressionText = expressionText;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000B800 File Offset: 0x00009A00
		public string ExpressionText
		{
			get
			{
				return this._expressionText;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000B808 File Offset: 0x00009A08
		public string FieldName
		{
			get
			{
				int num = this._expressionText.IndexOf(ExpressionBuilder.FieldPrefix, StringComparison.Ordinal);
				if (num != 0)
				{
					return null;
				}
				int num2 = this._expressionText.IndexOf(ExpressionBuilder.ValuePostfix, StringComparison.Ordinal);
				int num3 = num + ExpressionBuilder.FieldPrefix.Length;
				return this._expressionText.Substring(num3, num2 - num3);
			}
		}

		// Token: 0x0400016E RID: 366
		private readonly string _expressionText;
	}
}
