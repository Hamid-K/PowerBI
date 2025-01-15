using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200013D RID: 317
	internal sealed class DaxOperatorFormatter
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x000308BF File Offset: 0x0002EABF
		internal DaxOperatorFormatter(string name, string operatorString, bool alwaysMultiline)
		{
			this.Name = name;
			this._alwaysMultiline = alwaysMultiline;
			this._operator = operatorString;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000308DC File Offset: 0x0002EADC
		public string Name { get; }

		// Token: 0x0600115E RID: 4446 RVA: 0x000308E4 File Offset: 0x0002EAE4
		private string InvokeCore(string formattedArgs, bool multiline)
		{
			if (multiline)
			{
				return '\t' + formattedArgs + DaxFormat.NewLine;
			}
			return formattedArgs;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00030900 File Offset: 0x0002EB00
		internal string Invoke(params string[] arguments)
		{
			bool alwaysMultiline = this._alwaysMultiline;
			string text = DaxOperatorFormatter.SurroundArgumentsWithParentheses(DaxOperatorFormatter.FormatArguments(arguments, this._operator, ref alwaysMultiline, false));
			return this.InvokeCore(text, alwaysMultiline);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00030931 File Offset: 0x0002EB31
		internal string Invoke(IEnumerable<DaxExpression> arguments)
		{
			arguments = arguments ?? Util.EmptyArray<DaxExpression>();
			return this.Invoke(arguments.Select((DaxExpression a) => a.Text).ToArray<string>());
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00030970 File Offset: 0x0002EB70
		internal static string FormatArguments(string[] arguments, string operatorString, ref bool multiline, bool noIndentationOnNewLine = false)
		{
			arguments = arguments ?? Util.EmptyArray<string>();
			ArgumentValidation.CheckCondition(arguments.All((string a) => a != null), "arguments");
			if (!multiline)
			{
				multiline = arguments.Sum((string a) => a.Length) >= 80;
			}
			if (multiline)
			{
				string text = operatorString + DaxFormat.NewLine;
				if (!noIndentationOnNewLine)
				{
					text += "\t";
				}
				return string.Join(text, arguments.Select((string a) => DaxFormat.IncreaseIndent(a)).ToArray<string>());
			}
			return string.Join(operatorString, arguments);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00030A41 File Offset: 0x0002EC41
		internal static string SurroundArgumentsWithParentheses(string arguments)
		{
			return "(" + arguments + ")";
		}

		// Token: 0x04000AD0 RID: 2768
		private readonly bool _alwaysMultiline;

		// Token: 0x04000AD1 RID: 2769
		private readonly string _operator;
	}
}
