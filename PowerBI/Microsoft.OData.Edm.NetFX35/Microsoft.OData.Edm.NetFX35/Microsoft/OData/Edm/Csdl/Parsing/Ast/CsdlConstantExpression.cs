using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000037 RID: 55
	internal class CsdlConstantExpression : CsdlExpressionBase
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003841 File Offset: 0x00001A41
		public CsdlConstantExpression(EdmValueKind kind, string value, CsdlLocation location)
			: base(location)
		{
			this.kind = kind;
			this.value = value;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003858 File Offset: 0x00001A58
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000038CF File Offset: 0x00001ACF
		public EdmValueKind ValueKind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000038D7 File Offset: 0x00001AD7
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000053 RID: 83
		private readonly EdmValueKind kind;

		// Token: 0x04000054 RID: 84
		private readonly string value;
	}
}
