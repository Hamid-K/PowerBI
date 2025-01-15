using System;
using System.Globalization;

namespace NLog.Conditions
{
	// Token: 0x020001A7 RID: 423
	internal sealed class ConditionLiteralExpression : ConditionExpression
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x0003399A File Offset: 0x00031B9A
		public ConditionLiteralExpression(object literalValue)
		{
			this.LiteralValue = literalValue;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x000339A9 File Offset: 0x00031BA9
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x000339B1 File Offset: 0x00031BB1
		public object LiteralValue { get; private set; }

		// Token: 0x06001307 RID: 4871 RVA: 0x000339BA File Offset: 0x00031BBA
		public override string ToString()
		{
			if (this.LiteralValue == null)
			{
				return "null";
			}
			return Convert.ToString(this.LiteralValue, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000339DA File Offset: 0x00031BDA
		protected override object EvaluateNode(LogEventInfo context)
		{
			return this.LiteralValue;
		}
	}
}
