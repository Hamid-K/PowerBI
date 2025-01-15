using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D0 RID: 464
	internal class CsdlConstantExpression : CsdlExpressionBase
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x00023ABD File Offset: 0x00021CBD
		public CsdlConstantExpression(EdmValueKind kind, string value, CsdlLocation location)
			: base(location)
		{
			this.kind = kind;
			this.value = value;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00023AD4 File Offset: 0x00021CD4
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00023B4B File Offset: 0x00021D4B
		public EdmValueKind ValueKind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00023B53 File Offset: 0x00021D53
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040006E5 RID: 1765
		private readonly EdmValueKind kind;

		// Token: 0x040006E6 RID: 1766
		private readonly string value;
	}
}
