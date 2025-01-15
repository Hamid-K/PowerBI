using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils.Text;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models
{
	// Token: 0x02001726 RID: 5926
	public class ConditionalClusterList : ReadOnlyListBase<ConditionalCluster>
	{
		// Token: 0x0600C535 RID: 50485 RVA: 0x002A7089 File Offset: 0x002A5289
		public ConditionalClusterList()
			: this(new ConditionalCluster[0], new IRow[0])
		{
		}

		// Token: 0x0600C536 RID: 50486 RVA: 0x002A709D File Offset: 0x002A529D
		public ConditionalClusterList(IEnumerable<ConditionalCluster> clusters)
			: this(clusters, new IRow[0])
		{
		}

		// Token: 0x0600C537 RID: 50487 RVA: 0x002A70AC File Offset: 0x002A52AC
		public ConditionalClusterList(IEnumerable<ConditionalCluster> clusters, IEnumerable<IRow> unclusteredInputs)
			: base(clusters)
		{
			this.UnclusteredInputs = unclusteredInputs.ToReadOnlyList<IRow>();
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x0600C538 RID: 50488 RVA: 0x002A70C4 File Offset: 0x002A52C4
		public bool ValidPredicates
		{
			get
			{
				bool flag = this._validPredicates.GetValueOrDefault();
				if (this._validPredicates == null)
				{
					flag = base.Items.Count((ConditionalCluster c) => c.ValidPredicates.None<Predicate>()) <= 1;
					this._validPredicates = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x0600C539 RID: 50489 RVA: 0x002A712A File Offset: 0x002A532A
		public IReadOnlyList<IRow> UnclusteredInputs { get; }

		// Token: 0x0600C53A RID: 50490 RVA: 0x002A7134 File Offset: 0x002A5334
		public string Render(bool showLocalPredicates = false)
		{
			if (!this.Any<ConditionalCluster>())
			{
				return "<none>";
			}
			TextTableBuilder textTableBuilder = TextTableBuilder.Create(TextTableBorder.None, null, null);
			string text = "";
			int? num = new int?(0);
			TextTableBuilder textTableBuilder2 = textTableBuilder.AddIdentityColumn(text, null, num).AddStaticColumn(":", false, true, false);
			string text2 = "Program -> Predicate";
			int num2 = 0;
			int? num3 = new int?(70);
			bool flag = false;
			string text3 = null;
			string text4 = null;
			num = null;
			int? num4 = num;
			num = null;
			TextTableBuilder textTableBuilder3 = textTableBuilder2.AddColumn(text2, num2, num3, flag, text3, text4, num4, num).AddBorderColumn();
			string text5 = "Meta";
			int num5 = 0;
			int? num6 = new int?(20);
			bool flag2 = false;
			string text6 = null;
			string text7 = null;
			num = null;
			int? num7 = num;
			num = null;
			TextTableBuilder textTableBuilder4 = textTableBuilder3.AddColumn(text5, num5, num6, flag2, text6, text7, num7, num).AddBorderColumn();
			string text8 = "Examples";
			int num8 = 0;
			int? num9 = new int?(80);
			bool flag3 = false;
			string text9 = null;
			string text10 = null;
			num = null;
			int? num10 = num;
			num = null;
			ITextRowBuilder textRowBuilder = textTableBuilder4.AddColumn(text8, num8, num9, flag3, text9, text10, num10, num).AddHeadingRow().AddBorderRow();
			string newLine = Environment.NewLine;
			string text11 = newLine + "    ";
			foreach (ConditionalCluster conditionalCluster in this)
			{
				string text12;
				if (!showLocalPredicates)
				{
					text12 = string.Empty;
				}
				else
				{
					text12 = "--- local ---------" + text11 + conditionalCluster.Predicates.Select((Predicate i) => i.ToString()).ToJoinString(text11);
				}
				string text13 = text12;
				string text14 = conditionalCluster.ValidPredicates.Select((Predicate i) => i.ToString()).ToJoinString(text11);
				string text15 = string.Format("{0}{1}[{2,11:N4}]{3}", new object[]
				{
					conditionalCluster.Program,
					newLine,
					conditionalCluster.Program.Score,
					text11
				}) + text14 + text11 + text13;
				string text16 = ((conditionalCluster.Score == null) ? "--" : conditionalCluster.Score.Value.ToString("N3"));
				string text17 = string.Format("[ {0,10} ]{1}{2}{3}", new object[] { text16, newLine, newLine, conditionalCluster.ProgramMeta });
				string text18 = (from i in conditionalCluster.SourceExamples
					orderby i.Position
					select i into v
					select string.Format("{0,2}: {1}", v.Position, v.Example)).ToJoinString(newLine);
				string text19 = (from i in conditionalCluster.SourceInputs
					orderby i.Position
					select i into v
					select string.Format("{0,2}: {1}", v.Position, v.Input)).ToJoinString(newLine);
				if (text19.Any<char>())
				{
					text18 = string.Concat(new string[] { text18, newLine, "--", newLine, text19 });
				}
				textRowBuilder.AddDataRow(new object[] { text15, text17, text18 }).AddBorderRow();
			}
			string text20 = null;
			if (this.UnclusteredInputs.Any<IRow>())
			{
				text20 = ("Unclustered Additional Inputs: " + newLine + this.UnclusteredInputs.Select((IRow a) => a.ToString()).ToJoinString(newLine)).IndentSkipFirstLine(4);
			}
			return textRowBuilder.Render() + newLine + text20;
		}

		// Token: 0x04004D39 RID: 19769
		private bool? _validPredicates;
	}
}
