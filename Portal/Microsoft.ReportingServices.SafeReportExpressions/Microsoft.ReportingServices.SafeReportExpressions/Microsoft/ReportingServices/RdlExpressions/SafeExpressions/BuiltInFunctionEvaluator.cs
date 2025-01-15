using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000011 RID: 17
	internal sealed class BuiltInFunctionEvaluator : IFunctionEvaluator
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000027EF File Offset: 0x000009EF
		public BuiltInFunctionEvaluator(ISafeExpressionsReportContext safeExpressionsReportContext)
		{
			this._safeExpressionsReportContext = safeExpressionsReportContext;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002800 File Offset: 0x00000A00
		public object Evaluate(string functionname, List<object> arguments)
		{
			string text = functionname.ToUpperInvariant();
			if (!(text == "LEVEL"))
			{
				if (!(text == "INSCOPE"))
				{
					throw new NotSupportedException();
				}
				string asString = this.GetAsString("InScope", arguments[0]);
				return this._safeExpressionsReportContext.InScope(asString);
			}
			else
			{
				if (arguments.Count == 0)
				{
					return this._safeExpressionsReportContext.GetLevel(null);
				}
				string asString2 = this.GetAsString("Level", arguments[0]);
				return this._safeExpressionsReportContext.GetLevel(asString2);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000289C File Offset: 0x00000A9C
		private string GetAsString(string functionName, object value)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			string text2;
			try
			{
				text2 = (string)VBConvert.ChangeType(value, typeof(string));
			}
			catch
			{
				throw new ArgumentException(functionName + " function: Cannot convert value of type <" + value.GetType().Name + "> to string.");
			}
			return text2;
		}

		// Token: 0x0400001E RID: 30
		private readonly ISafeExpressionsReportContext _safeExpressionsReportContext;
	}
}
