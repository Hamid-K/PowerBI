using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000207 RID: 519
	public class EdmStringConstant : EdmValue, IEdmStringConstantExpression, IEdmExpression, IEdmStringValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x0002288F File Offset: 0x00020A8F
		public EdmStringConstant(string value)
			: this(null, value)
		{
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00022899 File Offset: 0x00020A99
		public EdmStringConstant(IEdmStringTypeReference type, string value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<string>(value, "value");
			this.value = value;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x000228B5 File Offset: 0x00020AB5
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000228BD File Offset: 0x00020ABD
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x000228C0 File Offset: 0x00020AC0
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.String;
			}
		}

		// Token: 0x04000593 RID: 1427
		private readonly string value;
	}
}
