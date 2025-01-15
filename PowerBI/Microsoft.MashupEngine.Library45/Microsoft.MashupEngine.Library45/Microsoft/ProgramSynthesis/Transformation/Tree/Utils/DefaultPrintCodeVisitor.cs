using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Utils
{
	// Token: 0x02001E98 RID: 7832
	public class DefaultPrintCodeVisitor : PrintCodeVisitor
	{
		// Token: 0x060108CE RID: 67790 RVA: 0x0038E242 File Offset: 0x0038C442
		private DefaultPrintCodeVisitor()
		{
		}

		// Token: 0x060108CF RID: 67791 RVA: 0x0038E24A File Offset: 0x0038C44A
		private DefaultPrintCodeVisitor(HashSet<char> noPredSpace, HashSet<char> noSuccSpace, string[] sourceLines = null, Node sourceNode = null)
			: base(noPredSpace, noSuccSpace, sourceLines, sourceNode)
		{
		}

		// Token: 0x060108D0 RID: 67792 RVA: 0x0038E258 File Offset: 0x0038C458
		public static string CreateAndVisit(Node node, HashSet<char> noPredSpace = null, HashSet<char> noSuccSpace = null, string[] sourceLines = null, Node sourceNode = null)
		{
			DefaultPrintCodeVisitor defaultPrintCodeVisitor = new DefaultPrintCodeVisitor(noPredSpace ?? new HashSet<char>(), noSuccSpace ?? new HashSet<char>(), sourceLines, sourceNode);
			return node.AcceptVisitor<StringBuilder>(defaultPrintCodeVisitor).ToString();
		}

		// Token: 0x060108D1 RID: 67793 RVA: 0x0038E28E File Offset: 0x0038C48E
		[DebuggerStepThrough]
		public override StringBuilder VisitStruct(StructNode node)
		{
			return this.VisitNode(node);
		}

		// Token: 0x060108D2 RID: 67794 RVA: 0x0038E28E File Offset: 0x0038C48E
		[DebuggerStepThrough]
		public override StringBuilder VisitSequence(SequenceNode node)
		{
			return this.VisitNode(node);
		}

		// Token: 0x060108D3 RID: 67795 RVA: 0x0038E298 File Offset: 0x0038C498
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
					() => this.RecreateSequenceNodeFormatting(node),
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

		// Token: 0x060108D4 RID: 67796 RVA: 0x0038E38C File Offset: 0x0038C58C
		private bool CopyFromSource(Node node)
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

		// Token: 0x060108D5 RID: 67797 RVA: 0x0038E3D4 File Offset: 0x0038C5D4
		private bool SingleValueNode(Node node)
		{
			string text;
			if (node.Attributes.TryGetValue("value", out text) && !string.IsNullOrEmpty(text))
			{
				base.AddSpaceBeforeIfNecessary(node);
				this._builder.Append(text);
				return true;
			}
			return false;
		}

		// Token: 0x060108D6 RID: 67798 RVA: 0x0038E414 File Offset: 0x0038C614
		private bool RecreateSequenceNodeFormatting(Node node)
		{
			SequenceNode sequenceNode = node as SequenceNode;
			if (sequenceNode == null)
			{
				return false;
			}
			List<Node> list = node.Children.Select(new Func<Node, Node>(base.GetEquivalentNodeInSource)).ToList<Node>();
			Node sourceParent = list.Collect(delegate(Node sc)
			{
				if (sc == null)
				{
					return null;
				}
				return sc.Parent;
			}).Distinct<Node>().OnlyOrDefault<Node>();
			Node sourceParent2 = sourceParent;
			if (((sourceParent2 != null) ? sourceParent2.Label : null) != node.Label)
			{
				return false;
			}
			string text = null;
			Node separator = sequenceNode.Separator;
			if (separator != null)
			{
				separator.Attributes.TryGetValue("value", out text);
			}
			bool flag = base.HasSourceInfo(sourceParent);
			string text2 = ((flag && base.HasSourceInfo(sourceParent.Children[0])) ? PrintCodeVisitor.Slice(this._sourceLines, sourceParent.StartPosition, sourceParent.Children[0].StartPosition) : "");
			string text3 = ((flag && base.HasSourceInfo(sourceParent.Children.Last<Node>())) ? PrintCodeVisitor.Slice(this._sourceLines, sourceParent.Children.Last<Node>().EndPosition, sourceParent.EndPosition) : "");
			IList<string> list2 = PrintCodeVisitor.GenerateSeparators(sourceParent, this._sourceLines);
			string text4 = PrintCodeVisitor.GenerateDefaultSeparator(text, list2);
			List<int> list3 = list.Select(delegate(Node sc)
			{
				if (sc != null)
				{
					return sourceParent.Children.FindAllIndexes((Node c) => sc == c, 0).First<int>();
				}
				return -1;
			}).ToList<int>();
			this._builder.Append(text2);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == null || !base.HasSourceInfo(list[i]))
				{
					node.Children[i].AcceptVisitor<StringBuilder>(this);
				}
				else
				{
					this._builder.Append(PrintCodeVisitor.Slice(this._sourceLines, list[i].StartPosition, list[i].EndPosition));
				}
				if (i != list.Count - 1)
				{
					if (list3[i] != -1 && list2[list3[i]] != null && list3[i] == list3[i + 1] - 1)
					{
						this._builder.Append(list2[list3[i]]);
					}
					else
					{
						this._builder.Append(text4);
					}
				}
			}
			this._builder.Append(text3);
			return true;
		}

		// Token: 0x060108D7 RID: 67799 RVA: 0x0038E6A4 File Offset: 0x0038C8A4
		private bool FallBack(Node node)
		{
			DefaultPrintCodeVisitor.<>c__DisplayClass9_0 CS$<>8__locals1 = new DefaultPrintCodeVisitor.<>c__DisplayClass9_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.separatorValue = string.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
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
					CS$<>8__locals1.separatorValue = sequenceNode.Separator.Attributes["value"];
				}
			}
			DefaultPrintCodeVisitor.<>c__DisplayClass9_0 CS$<>8__locals2 = CS$<>8__locals1;
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
					DefaultPrintCodeVisitor.<>c__DisplayClass9_1 CS$<>8__locals3 = new DefaultPrintCodeVisitor.<>c__DisplayClass9_1();
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
			return true;
		}
	}
}
