using System;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.Conditions
{
	// Token: 0x020001A4 RID: 420
	[NLogConfigurationItem]
	[ThreadAgnostic]
	public abstract class ConditionExpression
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x000338EC File Offset: 0x00031AEC
		public static implicit operator ConditionExpression(string conditionExpressionText)
		{
			return ConditionParser.ParseExpression(conditionExpressionText);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000338F4 File Offset: 0x00031AF4
		public object Evaluate(LogEventInfo context)
		{
			object obj;
			try
			{
				obj = this.EvaluateNode(context);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Exception occurred when evaluating condition");
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				throw new ConditionEvaluationException("Exception occurred when evaluating condition", ex);
			}
			return obj;
		}

		// Token: 0x060012F9 RID: 4857
		public abstract override string ToString();

		// Token: 0x060012FA RID: 4858
		protected abstract object EvaluateNode(LogEventInfo context);
	}
}
