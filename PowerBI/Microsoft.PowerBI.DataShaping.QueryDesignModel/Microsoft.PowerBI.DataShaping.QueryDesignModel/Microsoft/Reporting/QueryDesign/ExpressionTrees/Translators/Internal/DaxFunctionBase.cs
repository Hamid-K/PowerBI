using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000130 RID: 304
	internal abstract class DaxFunctionBase
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x0002D549 File Offset: 0x0002B749
		protected DaxFunctionBase(string name, bool alwaysMultiline)
		{
			this._name = name;
			this._alwaysMultiline = alwaysMultiline;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0002D55F File Offset: 0x0002B75F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0600108D RID: 4237
		protected abstract string InvokeCore(string formattedArgs, bool multiline);

		// Token: 0x0600108E RID: 4238 RVA: 0x0002D568 File Offset: 0x0002B768
		internal string Invoke(params string[] arguments)
		{
			bool alwaysMultiline = this._alwaysMultiline;
			string text = DaxFunctionBase.FormatArguments(arguments, ref alwaysMultiline);
			return this.InvokeCore(text, alwaysMultiline);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002D58D File Offset: 0x0002B78D
		internal string Invoke(IEnumerable<string> arguments)
		{
			return this.Invoke((arguments != null) ? arguments.ToArray<string>() : Util.EmptyArray<string>());
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002D5A5 File Offset: 0x0002B7A5
		internal string Invoke(params DaxExpression[] arguments)
		{
			arguments = arguments ?? Util.EmptyArray<DaxExpression>();
			return this.Invoke(arguments.Select((DaxExpression a) => a.Text).ToArray<string>());
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0002D5E3 File Offset: 0x0002B7E3
		internal string Invoke(IEnumerable<DaxExpression> arguments)
		{
			arguments = arguments ?? Util.EmptyArray<DaxExpression>();
			return this.Invoke(arguments.Select((DaxExpression a) => a.Text).ToArray<string>());
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0002D621 File Offset: 0x0002B821
		private static string FormatArguments(string[] arguments, ref bool multiline)
		{
			return DaxFunctionBase.FormatArguments(arguments, ref multiline, true, false);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002D62C File Offset: 0x0002B82C
		internal static string FormatArguments(string[] arguments, ref bool multiline, bool cleanArgs, bool noIndentationOnNewLine = false)
		{
			arguments = arguments ?? Util.EmptyArray<string>();
			ArgumentValidation.CheckCondition(arguments.All((string a) => a != null), "arguments");
			if (cleanArgs)
			{
				DaxFunctionBase.CleanArguments(arguments);
			}
			if (!multiline)
			{
				bool flag;
				if (arguments.Sum((string a) => a.Length) < 80)
				{
					flag = arguments.Sum((string a) => a.Count((char c) => c == '(')) >= 3;
				}
				else
				{
					flag = true;
				}
				multiline = flag;
			}
			if (multiline)
			{
				string text = "," + DaxFormat.NewLine;
				if (!noIndentationOnNewLine)
				{
					text += "\t";
				}
				return string.Join(text, arguments.Select((string a) => DaxFormat.IncreaseIndent(a)).ToArray<string>());
			}
			return string.Join(", ", arguments);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002D73C File Offset: 0x0002B93C
		private static void CleanArguments(string[] arguments)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				string text = arguments[i];
				if (text.Length > 2 && text[0] == '(' && text[text.Length - 1] == ')')
				{
					text = text.Substring(1, text.Length - 2);
				}
				arguments[i] = text;
			}
		}

		// Token: 0x04000AA7 RID: 2727
		private readonly string _name;

		// Token: 0x04000AA8 RID: 2728
		private readonly bool _alwaysMultiline;
	}
}
