using System;
using System.Globalization;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DDA RID: 7642
	public static class ExpressionExtensions
	{
		// Token: 0x0600BD61 RID: 48481 RVA: 0x002666DC File Offset: 0x002648DC
		public static string SkipAndTake(this string expression, int? skipCount, int? takeCount)
		{
			if (skipCount != null && skipCount.Value > 0)
			{
				expression = string.Format(CultureInfo.InvariantCulture, "let\r\n  _t = {1}\r\nin\r\n  if _t is table then Table.{0}(_t, {2}) meta Value.Metadata(_t)\r\n  else if _t is list then List.{0}(_t, {2}) meta Value.Metadata(_t)\r\n  else _t", "Skip", expression, skipCount.Value);
			}
			if (takeCount != null && takeCount.Value >= 0)
			{
				expression = string.Format(CultureInfo.InvariantCulture, "let\r\n  _t = {1}\r\nin\r\n  if _t is table then Table.{0}(_t, {2}) meta Value.Metadata(_t)\r\n  else if _t is list then List.{0}(_t, {2}) meta Value.Metadata(_t)\r\n  else _t", "FirstN", expression, takeCount.Value);
			}
			return expression;
		}
	}
}
