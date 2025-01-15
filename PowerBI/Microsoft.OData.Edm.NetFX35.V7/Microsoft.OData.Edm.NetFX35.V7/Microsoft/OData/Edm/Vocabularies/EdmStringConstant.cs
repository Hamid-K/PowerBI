using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000121 RID: 289
	public class EdmStringConstant : EdmValue, IEdmStringConstantExpression, IEdmExpression, IEdmElement, IEdmStringValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x00013E6A File Offset: 0x0001206A
		public EdmStringConstant(string value)
			: this(null, value)
		{
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00013E74 File Offset: 0x00012074
		public EdmStringConstant(IEdmStringTypeReference type, string value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<string>(value, "value");
			this.value = value;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00013E90 File Offset: 0x00012090
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0000C876 File Offset: 0x0000AA76
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00013C4B File Offset: 0x00011E4B
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.String;
			}
		}

		// Token: 0x04000430 RID: 1072
		private readonly string value;
	}
}
