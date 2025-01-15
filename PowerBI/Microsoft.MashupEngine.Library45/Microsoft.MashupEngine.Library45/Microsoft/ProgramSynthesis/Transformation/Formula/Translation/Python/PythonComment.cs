using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200184B RID: 6219
	internal class PythonComment : PythonBlock
	{
		// Token: 0x0600CB93 RID: 52115 RVA: 0x002B825F File Offset: 0x002B645F
		public PythonComment(string comment, PythonCommentType type)
			: base(PythonComment.GetRawSegments(comment, type))
		{
			this.Comment = comment;
			this.Type = type;
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x0600CB94 RID: 52116 RVA: 0x002B827C File Offset: 0x002B647C
		public string Comment { get; }

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x0600CB95 RID: 52117 RVA: 0x002B8284 File Offset: 0x002B6484
		public PythonCommentType Type { get; }

		// Token: 0x0600CB96 RID: 52118 RVA: 0x002B828C File Offset: 0x002B648C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonComment(this.Comment, this.Type);
		}

		// Token: 0x0600CB97 RID: 52119 RVA: 0x002B82A0 File Offset: 0x002B64A0
		public PythonComment With(string comment = null, PythonCommentType? type = null)
		{
			return new PythonComment(comment ?? this.Comment, type ?? this.Type);
		}

		// Token: 0x0600CB98 RID: 52120 RVA: 0x002B82D8 File Offset: 0x002B64D8
		private static IEnumerable<PythonRawSegment> GetRawSegments(string comment, PythonCommentType type)
		{
			if (string.IsNullOrEmpty(comment))
			{
				return Enumerable.Empty<PythonRawSegment>();
			}
			if (type == PythonCommentType.DocString)
			{
				string text = string.Join(Environment.NewLine, new string[]
				{
					(comment.Contains('\\') ? "r" : "") + "\"\"\"",
					comment,
					"\"\"\""
				});
				return new PythonRawSegment[]
				{
					new PythonRawSegment(text)
				};
			}
			return from line in comment.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)
				select new PythonRawSegment("# " + line);
		}

		// Token: 0x04004FEE RID: 20462
		public static PythonComment Empty = new PythonComment(string.Empty, PythonCommentType.Comment);
	}
}
