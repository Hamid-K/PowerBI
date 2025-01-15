using System;
using System.Collections;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000024 RID: 36
	internal sealed class SyntaxNodeEvaluation
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003237 File Offset: 0x00001437
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000323F File Offset: 0x0000143F
		public SyntaxNode SyntaxNode { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003248 File Offset: 0x00001448
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003250 File Offset: 0x00001450
		public ExpressionEvaluationResult EvaluationResult { get; private set; }

		// Token: 0x06000094 RID: 148 RVA: 0x00003259 File Offset: 0x00001459
		public SyntaxNodeEvaluation(SyntaxNode syntaxNode, ExpressionEvaluationResult evaluationResult)
		{
			this.SyntaxNode = syntaxNode;
			this.EvaluationResult = evaluationResult;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003270 File Offset: 0x00001470
		public override string ToString()
		{
			string text = ((this.SyntaxNode == null) ? "<null>" : this.SyntaxNode.ToString());
			string text2;
			if (this.EvaluationResult.Value == null)
			{
				text2 = "null <Object>";
			}
			else
			{
				Type type = this.EvaluationResult.Value.GetType();
				TypeCode typeCode = Type.GetTypeCode(type);
				if (type.IsArray)
				{
					IEnumerable enumerable = this.EvaluationResult.Value as IEnumerable;
					bool flag = false;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in enumerable)
					{
						if (flag)
						{
							stringBuilder.Append(",");
						}
						flag = true;
						if (obj == null)
						{
							stringBuilder.Append("null");
						}
						else
						{
							stringBuilder.Append(this.FormatResult(obj, Type.GetTypeCode(obj.GetType())));
						}
					}
					text2 = string.Format("[{0}] <{1}>", stringBuilder, type.Name);
				}
				else if (type.IsEnum)
				{
					text2 = string.Format("{0} <{1}>", this.EvaluationResult.Value, type.Name);
				}
				else if (typeCode == TypeCode.Object)
				{
					text2 = type.Name + " <Object>";
				}
				else
				{
					text2 = string.Format("{0} <{1}>", this.FormatResult(this.EvaluationResult.Value, typeCode), typeCode);
				}
			}
			return text + " => " + text2;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003408 File Offset: 0x00001608
		private string FormatResult(object value, TypeCode typeCode)
		{
			if (typeCode == TypeCode.Char)
			{
				return string.Format("'{0}'", value);
			}
			if (typeCode == TypeCode.String)
			{
				return string.Format("\"{0}\"", value);
			}
			return value.ToString();
		}
	}
}
