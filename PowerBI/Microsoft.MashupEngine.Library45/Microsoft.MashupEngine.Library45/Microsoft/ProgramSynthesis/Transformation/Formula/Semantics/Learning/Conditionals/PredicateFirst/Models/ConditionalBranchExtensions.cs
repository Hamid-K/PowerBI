using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;
using Microsoft.ProgramSynthesis.Utils.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.PredicateFirst.Models
{
	// Token: 0x0200174D RID: 5965
	public static class ConditionalBranchExtensions
	{
		// Token: 0x0600C611 RID: 50705 RVA: 0x002A97EC File Offset: 0x002A79EC
		public static string Render(this IEnumerable<Cluster> subject)
		{
			subject = subject.ToReadOnlyList<Cluster>();
			if (!subject.Any<Cluster>())
			{
				return "<none>";
			}
			TextTableBuilder textTableBuilder = TextTableBuilder.Create(TextTableBorder.None, null, null);
			string text = "";
			string text2 = "N0";
			int num = 0;
			string text3 = "--";
			int? num2 = new int?(0);
			TextTableBuilder textTableBuilder2 = textTableBuilder.AddNumberColumn(text, text2, num, text3, null, num2).AddStaticColumn(":", false, true, false);
			string text4 = "Predicate";
			int num3 = 0;
			int? num4 = new int?(70);
			bool flag = false;
			string text5 = null;
			string text6 = null;
			num2 = null;
			int? num5 = num2;
			num2 = null;
			TextTableBuilder textTableBuilder3 = textTableBuilder2.AddColumn(text4, num3, num4, flag, text5, text6, num5, num2).AddBorderColumn();
			string text7 = "Score";
			int num6 = 0;
			num2 = null;
			int? num7 = num2;
			bool flag2 = true;
			string text8 = null;
			string text9 = null;
			num2 = null;
			int? num8 = num2;
			num2 = null;
			TextTableBuilder textTableBuilder4 = textTableBuilder3.AddColumn(text7, num6, num7, flag2, text8, text9, num8, num2).AddBorderColumn();
			string text10 = "Examples";
			int num9 = 0;
			int? num10 = new int?(80);
			bool flag3 = false;
			string text11 = null;
			string text12 = null;
			num2 = null;
			int? num11 = num2;
			num2 = null;
			ITextRowBuilder textRowBuilder = textTableBuilder4.AddColumn(text10, num9, num10, flag3, text11, text12, num11, num2).AddHeadingRow().AddBorderRow();
			string newLine = Environment.NewLine;
			foreach (Cluster cluster in subject)
			{
				string text13;
				if (cluster.Program == null)
				{
					bool? learnFailed = cluster.LearnFailed;
					bool flag4 = true;
					text13 = (((learnFailed.GetValueOrDefault() == flag4) & (learnFailed != null)) ? "Learn Failed" : string.Empty);
				}
				else
				{
					text13 = string.Format("{0}", cluster.Program);
				}
				string text14 = text13;
				string text15 = string.Format("{0}", cluster.Predicate);
				string text16 = cluster.Examples.Render();
				string text17 = "{0,11:N4}{1}{2,11:N4}";
				Predicate predicate = cluster.Predicate;
				object obj = ((predicate != null) ? predicate.Score : null);
				object obj2 = newLine;
				IProgram program = cluster.Program;
				string text18 = string.Format(text17, obj, obj2, (program != null) ? new double?(program.Score) : null);
				if (cluster.LearnTime != null)
				{
					text18 += string.Format("{0}{1:N2}ms", newLine, cluster.LearnTime);
				}
				textRowBuilder.AddDataRow(new object[]
				{
					cluster.Position,
					text15 + newLine + text14,
					text18,
					text16
				}).AddBorderRow();
			}
			return textRowBuilder.Render();
		}
	}
}
