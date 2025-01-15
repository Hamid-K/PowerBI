using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DF RID: 479
	internal class CsdlConstantExpression : CsdlExpressionBase
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x00025C82 File Offset: 0x00023E82
		public CsdlConstantExpression(EdmValueKind kind, string value, CsdlLocation location)
			: base(location)
		{
			this.kind = kind;
			this.value = value;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00025C9C File Offset: 0x00023E9C
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				switch (this.kind)
				{
				case EdmValueKind.Binary:
					return EdmExpressionKind.BinaryConstant;
				case EdmValueKind.Boolean:
					return EdmExpressionKind.BooleanConstant;
				case EdmValueKind.DateTimeOffset:
					return EdmExpressionKind.DateTimeOffsetConstant;
				case EdmValueKind.Decimal:
					return EdmExpressionKind.DecimalConstant;
				case EdmValueKind.Floating:
					return EdmExpressionKind.FloatingConstant;
				case EdmValueKind.Guid:
					return EdmExpressionKind.GuidConstant;
				case EdmValueKind.Integer:
					return EdmExpressionKind.IntegerConstant;
				case EdmValueKind.Null:
					return EdmExpressionKind.Null;
				case EdmValueKind.String:
					return EdmExpressionKind.StringConstant;
				case EdmValueKind.Duration:
					return EdmExpressionKind.DurationConstant;
				case EdmValueKind.Date:
					return EdmExpressionKind.DateConstant;
				case EdmValueKind.TimeOfDay:
					return EdmExpressionKind.TimeOfDayConstant;
				}
				return EdmExpressionKind.None;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00025D13 File Offset: 0x00023F13
		public EdmValueKind ValueKind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00025D1B File Offset: 0x00023F1B
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400075E RID: 1886
		private readonly EdmValueKind kind;

		// Token: 0x0400075F RID: 1887
		private readonly string value;
	}
}
