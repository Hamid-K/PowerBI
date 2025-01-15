using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Transformation.Tree.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Utils
{
	// Token: 0x02001E9F RID: 7839
	public abstract class PrintCodeVisitor : NodeVisitor<StringBuilder>
	{
		// Token: 0x060108EB RID: 67819 RVA: 0x0038E9AF File Offset: 0x0038CBAF
		protected PrintCodeVisitor()
		{
		}

		// Token: 0x060108EC RID: 67820 RVA: 0x0038E9C4 File Offset: 0x0038CBC4
		protected PrintCodeVisitor(HashSet<char> noPredSpace, HashSet<char> noSuccSpace, string[] sourceLines = null, Node sourceNode = null)
		{
			this._noPredSpace = noPredSpace ?? new HashSet<char>();
			this._noSuccSpace = noSuccSpace ?? new HashSet<char>();
			this._sourceLines = sourceLines;
			this._sourceNode = sourceNode;
			this._nodeStack = new List<Node>();
		}

		// Token: 0x060108ED RID: 67821 RVA: 0x0038EA1C File Offset: 0x0038CC1C
		protected bool HasSourceInfo(Node node)
		{
			return this._sourceLines != null && node.HasPosition;
		}

		// Token: 0x060108EE RID: 67822 RVA: 0x0038EA30 File Offset: 0x0038CC30
		public static string Slice(string[] sourceLines, Node.Position startPosition, Node.Position endPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (startPosition.Line == endPosition.Line)
			{
				int num = endPosition.Column - startPosition.Column;
				stringBuilder.Append(sourceLines[startPosition.Line].Substring(startPosition.Column, num));
				return stringBuilder.ToString();
			}
			stringBuilder.AppendLine(sourceLines[startPosition.Line].Substring(startPosition.Column));
			for (int i = startPosition.Line + 1; i < endPosition.Line; i++)
			{
				stringBuilder.AppendLine(sourceLines[i]);
			}
			stringBuilder.Append(sourceLines[endPosition.Line].Substring(0, endPosition.Column));
			return stringBuilder.ToString();
		}

		// Token: 0x060108EF RID: 67823 RVA: 0x0038EADE File Offset: 0x0038CCDE
		public static string[] Prefix(string[] sourceLines, Node.Position position)
		{
			return sourceLines.Take(position.Line).AppendItem(sourceLines[position.Line].Substring(0, position.Column)).ToArray<string>();
		}

		// Token: 0x060108F0 RID: 67824 RVA: 0x0038EB0C File Offset: 0x0038CD0C
		public static string[] ReplaceSlice(string[] sourceLines, Node.Position startPosition, Node.Position endPosition, string replacement)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < sourceLines.Length; i++)
			{
				if (i < startPosition.Line || i > endPosition.Line)
				{
					list.Add(sourceLines[i]);
				}
				else if (i <= startPosition.Line || i >= endPosition.Line)
				{
					if (startPosition.Line == endPosition.Line)
					{
						string text = sourceLines[startPosition.Line].Substring(0, startPosition.Column) + replacement + sourceLines[startPosition.Line].Substring(endPosition.Column);
						list.Add(text);
					}
					else
					{
						if (i == startPosition.Line && (startPosition.Column > 0 || replacement.Length > 0))
						{
							list.Add(sourceLines[startPosition.Line].Substring(0, startPosition.Column) + replacement);
						}
						if (i == endPosition.Line && endPosition.Column > 0)
						{
							list.Add(sourceLines[endPosition.Line].Substring(endPosition.Column));
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060108F1 RID: 67825 RVA: 0x0038EC1C File Offset: 0x0038CE1C
		protected Node.Position ExtendForSurroundingSpace(Node node, bool preceding = true)
		{
			PrintCodeVisitor.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.preceding = preceding;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.node = node;
			while (this.<ExtendForSurroundingSpace>g__Valid|12_0(CS$<>8__locals1.node, ref CS$<>8__locals1))
			{
				CS$<>8__locals1.node = CS$<>8__locals1.node.Parent;
			}
			if (!CS$<>8__locals1.preceding)
			{
				return CS$<>8__locals1.node.EndPosition;
			}
			return CS$<>8__locals1.node.StartPosition;
		}

		// Token: 0x060108F2 RID: 67826 RVA: 0x0038EC84 File Offset: 0x0038CE84
		protected Node GetEquivalentNodeInSource(Node node)
		{
			return Semantics.AllNodes(this._sourceNode, (Node n) => n.StartPosition == node.StartPosition && n.EndPosition == node.EndPosition && n.Equals(node)).FirstOrDefault<Node>();
		}

		// Token: 0x060108F3 RID: 67827 RVA: 0x0038ECBC File Offset: 0x0038CEBC
		protected void AddSpaceBeforeIfNecessary(Node node)
		{
			PrintCodeVisitor.<>c__DisplayClass14_0 CS$<>8__locals1 = new PrintCodeVisitor.<>c__DisplayClass14_0();
			CS$<>8__locals1.<>4__this = this;
			if ((this._builder.Length > 0 && char.IsWhiteSpace(this._builder[this._builder.Length - 1])) || this._builder.Length == 0)
			{
				return;
			}
			CS$<>8__locals1.value = null;
			Node node2 = Semantics.AllNodes(node).FirstOrDefault((Node x) => x.Attributes.TryGetValue("value", out CS$<>8__locals1.value) && !string.IsNullOrEmpty(CS$<>8__locals1.value));
			if (node2 == null)
			{
				return;
			}
			bool flag;
			if ((CS$<>8__locals1.value.Length != 1 || !this._noPredSpace.Contains(CS$<>8__locals1.value[0])) && !(node2.Label == "InterpolatedStringTextToken") && !(node2.Label == "InterpolatedStringEndToken"))
			{
				Node parent = node2.Parent;
				if (!(((parent != null) ? parent.Label : null) == "Interpolation") && (this._nodeStack.Count < 2 || !(this._nodeStack[this._nodeStack.Count - 2].Label == "Interpolation")) && !CS$<>8__locals1.<AddSpaceBeforeIfNecessary>g__IsLeftMostInside|1("Interpolation", 1) && !CS$<>8__locals1.<AddSpaceBeforeIfNecessary>g__IsLeftMostInside|1("InterpolationAlignmentClause", 1) && !CS$<>8__locals1.<AddSpaceBeforeIfNecessary>g__IsLeftMostInside|1("UnaryMinusExpression", 1))
				{
					flag = this._nodeStack.Count >= 2 && ((this._nodeStack.Last<Node>().Label == "ColonToken" && this._nodeStack[this._nodeStack.Count - 2].Label == "InterpolationFormatClause") || (this._nodeStack.Last<Node>().Label == "CommaToken" && this._nodeStack[this._nodeStack.Count - 2].Label == "InterpolationAlignmentClause"));
					goto IL_01F5;
				}
			}
			flag = true;
			IL_01F5:
			bool flag2 = this._noSuccSpace.Contains(this._builder[this._builder.Length - 1]);
			if (!flag && !flag2)
			{
				this._builder.Append(' ');
			}
		}

		// Token: 0x060108F4 RID: 67828 RVA: 0x0038EEF8 File Offset: 0x0038D0F8
		protected List<IReadOnlyList<Node>> ComputeRuns(Node node, string separatorValue)
		{
			PrintCodeVisitor.<>c__DisplayClass15_0 CS$<>8__locals1;
			CS$<>8__locals1.runs = new List<IReadOnlyList<Node>>();
			CS$<>8__locals1.currentRun = new List<Record<Node, int>>();
			List<Node> list = node.Children.Select(new Func<Node, Node>(this.GetEquivalentNodeInSource)).ToList<Node>();
			Node node2 = null;
			int num = -1;
			using (IEnumerator<Record<Node, Node>> enumerator = node.Children.ZipWith(list).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Node node3;
					Node node4;
					enumerator.Current.Deconstruct(out node3, out node4);
					Node node5 = node3;
					Node sourceChild = node4;
					if (sourceChild == null || !sourceChild.HasPosition)
					{
						PrintCodeVisitor.<ComputeRuns>g__FlushRun|15_0(ref CS$<>8__locals1);
						CS$<>8__locals1.runs.Add(new List<Node> { node5 });
					}
					else
					{
						bool flag = sourceChild.Parent != null;
						bool flag2 = flag && ((node2 != null) ? node2.Parent : null) == sourceChild.Parent;
						bool flag3 = flag && flag2 && node2.Parent.Children.ElementAtOrDefault(num + 1) == sourceChild;
						bool flag4;
						if (flag)
						{
							SequenceNode sequenceNode = sourceChild.Parent as SequenceNode;
							string text;
							if (sequenceNode == null)
							{
								text = null;
							}
							else
							{
								Node separator = sequenceNode.Separator;
								text = ((separator != null) ? separator.Attributes["value"] : null);
							}
							flag4 = separatorValue == text;
						}
						else
						{
							flag4 = false;
						}
						bool flag5 = flag4;
						int num2;
						if (!flag3)
						{
							Node parent = sourceChild.Parent;
							num2 = ((parent != null) ? parent.Children.FindAllIndexes((Node c) => c == sourceChild, 0).First<int>() : (-1));
						}
						else
						{
							num2 = num + 1;
						}
						int num3 = num2;
						if (!flag2 || !flag3 || !flag5)
						{
							PrintCodeVisitor.<ComputeRuns>g__FlushRun|15_0(ref CS$<>8__locals1);
						}
						Record<Node, int> record = Record.Create<Node, int>(sourceChild, num3);
						CS$<>8__locals1.currentRun.Add(record);
						int num4;
						record.Deconstruct(out node4, out num4);
						node2 = node4;
						num = num4;
						record.ToString();
					}
				}
			}
			PrintCodeVisitor.<ComputeRuns>g__FlushRun|15_0(ref CS$<>8__locals1);
			return CS$<>8__locals1.runs;
		}

		// Token: 0x060108F5 RID: 67829 RVA: 0x0038F118 File Offset: 0x0038D318
		protected static string SaneInitialSpace(SequenceNode node, string[] sourceLines)
		{
			string label = node.Label;
			string text;
			if (!(label == "ArgumentListSeq"))
			{
				if (!(label == "BlockSeq"))
				{
					if (!(label == "ObjectInitializerExpressionSeq"))
					{
						if (!(label == "ParameterListSeq"))
						{
							if (!(label == "ArrayInitializerExpressionSeq"))
							{
								text = " ";
							}
							else
							{
								text = " ";
							}
						}
						else
						{
							text = " ";
						}
					}
					else
					{
						text = " ";
					}
				}
				else
				{
					text = ((sourceLines == null) ? " " : "\n");
				}
			}
			else
			{
				text = "";
			}
			return text;
		}

		// Token: 0x060108F6 RID: 67830 RVA: 0x0038F1A8 File Offset: 0x0038D3A8
		protected static string SaneFinalSpace(SequenceNode node, string[] sourceLines)
		{
			string label = node.Label;
			string text;
			if (!(label == "ArgumentListSeq"))
			{
				if (!(label == "BlockSeq"))
				{
					if (!(label == "ObjectInitializerExpressionSeq"))
					{
						if (!(label == "ParameterListSeq"))
						{
							if (!(label == "ArrayInitializerExpressionSeq"))
							{
								text = " ";
							}
							else
							{
								text = " ";
							}
						}
						else
						{
							text = " ";
						}
					}
					else
					{
						text = " ";
					}
				}
				else
				{
					text = ((sourceLines == null) ? " " : "\n");
				}
			}
			else
			{
				text = "";
			}
			return text;
		}

		// Token: 0x060108F7 RID: 67831 RVA: 0x0038F238 File Offset: 0x0038D438
		protected static string SaneSeparator(SequenceNode node, string[] sourceLines)
		{
			string text = node.Separator.Attributes["value"];
			string label = node.Label;
			string text2;
			if (!(label == "ArgumentListSeq"))
			{
				if (!(label == "BlockSeq"))
				{
					if (!(label == "ObjectInitializerExpressionSeq"))
					{
						if (!(label == "ParameterListSeq"))
						{
							if (!(label == "ArrayInitializerExpressionSeq"))
							{
								text2 = text;
							}
							else
							{
								text2 = text + " ";
							}
						}
						else
						{
							text2 = text + " ";
						}
					}
					else
					{
						text2 = text + " ";
					}
				}
				else
				{
					text2 = text + ((sourceLines == null) ? " " : "\n");
				}
			}
			else
			{
				text2 = text + " ";
			}
			return text2;
		}

		// Token: 0x060108F8 RID: 67832 RVA: 0x0038F2F8 File Offset: 0x0038D4F8
		public static string GenerateDefaultSeparator(Node node, string[] sourceLines)
		{
			return PrintCodeVisitor.GenerateDefaultSeparator(PrintCodeVisitor.SaneSeparator(node as SequenceNode, sourceLines), PrintCodeVisitor.GenerateSeparators(node, sourceLines));
		}

		// Token: 0x060108F9 RID: 67833 RVA: 0x0038F314 File Offset: 0x0038D514
		protected static string GenerateDefaultSeparator(string separator, IList<string> interSeparators)
		{
			if (interSeparators == null)
			{
				return separator;
			}
			return interSeparators.Collect<string>().ToMultiset<string>().ArgMax((KeyValuePair<string, int> kvp) => kvp.Value)
				.Key ?? separator;
		}

		// Token: 0x060108FA RID: 67834 RVA: 0x0038F364 File Offset: 0x0038D564
		protected static IList<string> GenerateSeparators(Node sourceParent, string[] sourceLines)
		{
			if (sourceLines == null)
			{
				return null;
			}
			return sourceParent.Children.ZipWith(sourceParent.Children.Skip(1)).Select(delegate(Record<Node, Node> cp)
			{
				Node node;
				Node node2;
				cp.Deconstruct(out node, out node2);
				Node node3 = node;
				Node node4 = node2;
				if (node3.HasPosition && node4.HasPosition)
				{
					return PrintCodeVisitor.Slice(sourceLines, node3.EndPosition, node4.StartPosition);
				}
				return null;
			}).AppendItem(null)
				.ToList<string>();
		}

		// Token: 0x060108FB RID: 67835 RVA: 0x0038F3BC File Offset: 0x0038D5BC
		[CompilerGenerated]
		private bool <ExtendForSurroundingSpace>g__Valid|12_0(Node currentNode, ref PrintCodeVisitor.<>c__DisplayClass12_0 A_2)
		{
			Node node;
			if (!A_2.preceding)
			{
				Node parent = currentNode.Parent;
				node = ((parent != null) ? parent.Children.Last<Node>() : null);
			}
			else
			{
				Node parent2 = currentNode.Parent;
				node = ((parent2 != null) ? parent2.Children[0] : null);
			}
			Node node2 = node;
			return currentNode == node2 && this.HasSourceInfo(A_2.node.Parent);
		}

		// Token: 0x060108FC RID: 67836 RVA: 0x0038F416 File Offset: 0x0038D616
		[CompilerGenerated]
		internal static void <ComputeRuns>g__FlushRun|15_0(ref PrintCodeVisitor.<>c__DisplayClass15_0 A_0)
		{
			if (A_0.currentRun.Count > 0)
			{
				A_0.runs.Add(A_0.currentRun.UnzipToLists<Node, int>().Item1);
				A_0.currentRun = new List<Record<Node, int>>();
			}
		}

		// Token: 0x040062E3 RID: 25315
		protected readonly StringBuilder _builder = new StringBuilder();

		// Token: 0x040062E4 RID: 25316
		protected readonly HashSet<char> _noPredSpace;

		// Token: 0x040062E5 RID: 25317
		protected readonly HashSet<char> _noSuccSpace;

		// Token: 0x040062E6 RID: 25318
		protected readonly string[] _sourceLines;

		// Token: 0x040062E7 RID: 25319
		protected readonly Node _sourceNode;

		// Token: 0x040062E8 RID: 25320
		protected readonly List<Node> _nodeStack;
	}
}
