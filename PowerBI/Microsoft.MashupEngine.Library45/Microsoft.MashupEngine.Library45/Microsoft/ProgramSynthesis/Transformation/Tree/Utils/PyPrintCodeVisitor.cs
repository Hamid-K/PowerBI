using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Utils
{
	// Token: 0x02001EA8 RID: 7848
	public class PyPrintCodeVisitor : PrintCodeVisitor
	{
		// Token: 0x0601090B RID: 67851 RVA: 0x0038E242 File Offset: 0x0038C442
		private PyPrintCodeVisitor()
		{
		}

		// Token: 0x0601090C RID: 67852 RVA: 0x0038F62C File Offset: 0x0038D82C
		private PyPrintCodeVisitor(HashSet<char> noPredSpace, HashSet<char> noSuccSpace, string[] sourceLines = null, Node sourceNode = null, int indentLevel = -1, string tab = "    ")
			: base(noPredSpace, noSuccSpace, sourceLines, sourceNode)
		{
			this._indentLevel = indentLevel;
			if (indentLevel == -1 && sourceNode != null)
			{
				this._indentLevel = this.GetIndentLevel(sourceNode);
			}
			this._tab = tab;
		}

		// Token: 0x0601090D RID: 67853 RVA: 0x0038F660 File Offset: 0x0038D860
		public static string CreateAndVisit(Node node, HashSet<char> noPredSpace = null, HashSet<char> noSuccSpace = null, string[] sourceLines = null, Node sourceNode = null, int indentLevel = -1)
		{
			int indentLevelAttr = node.GetIndentLevelAttr();
			if (indentLevelAttr != -1)
			{
				indentLevel += indentLevelAttr;
			}
			string text;
			if (sourceNode != null && !sourceNode.Attributes.TryGetValue("tab", out text))
			{
				text = "    ";
			}
			else if (!node.Attributes.TryGetValue("tab", out text))
			{
				text = "    ";
			}
			PyPrintCodeVisitor pyPrintCodeVisitor = new PyPrintCodeVisitor(noPredSpace ?? new HashSet<char>(), noSuccSpace ?? new HashSet<char>(), sourceLines, sourceNode, indentLevel, text);
			return node.AcceptVisitor<StringBuilder>(pyPrintCodeVisitor).ToString();
		}

		// Token: 0x0601090E RID: 67854 RVA: 0x0038F6E5 File Offset: 0x0038D8E5
		[DebuggerStepThrough]
		public override StringBuilder VisitStruct(StructNode node)
		{
			return this.VisitNode(node);
		}

		// Token: 0x0601090F RID: 67855 RVA: 0x0038F6E5 File Offset: 0x0038D8E5
		[DebuggerStepThrough]
		public override StringBuilder VisitSequence(SequenceNode node)
		{
			return this.VisitNode(node);
		}

		// Token: 0x06010910 RID: 67856 RVA: 0x0038F6F0 File Offset: 0x0038D8F0
		private StringBuilder VisitNode(Node node)
		{
			Func<bool>[] array;
			if (this._sourceLines == null)
			{
				array = new Func<bool>[]
				{
					() => this.SingleValueNode(node),
					() => this.FallBack(node)
				};
			}
			else
			{
				array = new Func<bool>[]
				{
					() => this.CopyFromSource(node),
					() => this.SingleValueNode(node),
					() => this.FallBack(node)
				};
			}
			this._nodeStack.Add(node);
			bool flag = false;
			Func<bool>[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				flag = array2[i]();
				if (flag)
				{
					break;
				}
			}
			this._nodeStack.RemoveAt(this._nodeStack.Count - 1);
			if (!flag)
			{
				throw new Exception("All formatting strategies failed.");
			}
			return this._builder;
		}

		// Token: 0x06010911 RID: 67857 RVA: 0x0038F7D4 File Offset: 0x0038D9D4
		private int GetIndentLevel(Node sourcenode)
		{
			int indentLevelAttr = sourcenode.GetIndentLevelAttr();
			if (indentLevelAttr != -1)
			{
				return indentLevelAttr;
			}
			if (sourcenode.Parent == null)
			{
				return -1;
			}
			return this.GetIndentLevel(sourcenode.Parent);
		}

		// Token: 0x06010912 RID: 67858 RVA: 0x0038F804 File Offset: 0x0038DA04
		private string FixEscapedCharacters(string sourceString)
		{
			return Regex.Replace(sourceString, "(\\[rnabvt'\"\\])", "\\$1");
		}

		// Token: 0x06010913 RID: 67859 RVA: 0x0038F818 File Offset: 0x0038DA18
		public bool CopyFromSource(Node node)
		{
			if (!base.HasSourceInfo(node))
			{
				return false;
			}
			base.AddSpaceBeforeIfNecessary(node);
			Node.Position position = base.ExtendForSurroundingSpace(node, true);
			this._builder.Append(PrintCodeVisitor.Slice(this._sourceLines, position, node.EndPosition));
			return true;
		}

		// Token: 0x06010914 RID: 67860 RVA: 0x0038F860 File Offset: 0x0038DA60
		public bool SingleValueNode(Node node)
		{
			string text;
			if (node.Attributes.TryGetValue("value", out text) && !string.IsNullOrEmpty(text))
			{
				base.AddSpaceBeforeIfNecessary(node);
				this._builder.Append(Regex.Replace(text, "(\r|\n|\a|[\b]|\v|\t|\\\\)", "\\$1"));
				return true;
			}
			return false;
		}

		// Token: 0x06010915 RID: 67861 RVA: 0x0038F8B0 File Offset: 0x0038DAB0
		public bool FallBack(Node node)
		{
			PyPrintCodeVisitor.<>c__DisplayClass12_0 CS$<>8__locals1 = new PyPrintCodeVisitor.<>c__DisplayClass12_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.separatorValue = string.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
			bool flag = false;
			SequenceNode sequenceNode = node as SequenceNode;
			if (sequenceNode != null)
			{
				if (this._sourceLines != null)
				{
					CS$<>8__locals1.separatorValue = PrintCodeVisitor.SaneSeparator(sequenceNode, this._sourceLines);
					text = PrintCodeVisitor.SaneInitialSpace(sequenceNode, this._sourceLines);
					text2 = PrintCodeVisitor.SaneFinalSpace(sequenceNode, this._sourceLines);
				}
				else
				{
					string label = sequenceNode.Separator.Label;
					CS$<>8__locals1.separatorValue = sequenceNode.Separator.Attributes["value"];
					if (label == "NewLine")
					{
						this._indentLevel++;
						CS$<>8__locals1.separatorValue = "\r\n" + string.Concat(Enumerable.Repeat<string>(this._tab, this._indentLevel));
						if (this._indentLevel != 0)
						{
							text = "\r\n";
						}
						text += string.Concat(Enumerable.Repeat<string>(this._tab, this._indentLevel));
						flag = true;
					}
				}
			}
			PyPrintCodeVisitor.<>c__DisplayClass12_0 CS$<>8__locals2 = CS$<>8__locals1;
			List<IReadOnlyList<Node>> list;
			if (this._sourceLines != null)
			{
				list = base.ComputeRuns(node, CS$<>8__locals1.separatorValue);
			}
			else
			{
				list = node.Children.Select((Node c) => new List<Node> { c }).ToList<IReadOnlyList<Node>>();
			}
			CS$<>8__locals2.runs = list;
			this._builder.Append(text);
			using (IEnumerator<Record<int, IReadOnlyList<Node>>> enumerator = CS$<>8__locals1.runs.Enumerate<IReadOnlyList<Node>>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PyPrintCodeVisitor.<>c__DisplayClass12_1 CS$<>8__locals3 = new PyPrintCodeVisitor.<>c__DisplayClass12_1();
					CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals1;
					int num;
					IReadOnlyList<Node> readOnlyList;
					enumerator.Current.Deconstruct(out num, out readOnlyList);
					CS$<>8__locals3.i = num;
					IReadOnlyList<Node> readOnlyList2 = readOnlyList;
					if (!base.HasSourceInfo(readOnlyList2[0]))
					{
						readOnlyList2.ForEach(delegate(Node c)
						{
							c.AcceptVisitor<StringBuilder>(CS$<>8__locals3.CS$<>8__locals1.<>4__this);
							base.<FallBack>g__AppendSeparator|1();
						});
					}
					else
					{
						Node.Position position = base.ExtendForSurroundingSpace(readOnlyList2[0], true);
						base.AddSpaceBeforeIfNecessary(readOnlyList2[0]);
						this._builder.Append(PrintCodeVisitor.Slice(this._sourceLines, position, readOnlyList2.Last<Node>().EndPosition));
						CS$<>8__locals3.<FallBack>g__AppendSeparator|1();
					}
				}
			}
			this._builder.Append(text2);
			if (flag)
			{
				this._indentLevel--;
			}
			return true;
		}

		// Token: 0x040062F6 RID: 25334
		private int _indentLevel;

		// Token: 0x040062F7 RID: 25335
		private string _tab;
	}
}
