using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000258 RID: 600
	public abstract class SuffixTree<TSequenceable, TSequence, TSubSequence> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable>, IEquatable<TSequence> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000CDD RID: 3293
		protected abstract TSequence OnNewInput(TSequence input);

		// Token: 0x06000CDE RID: 3294
		protected abstract TSequence RecoverInput(List<TSubSequence> parts);

		// Token: 0x06000CDF RID: 3295
		protected abstract TSequenceable At(int index);

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000CE0 RID: 3296
		protected abstract int SumOfInputLengths { get; }

		// Token: 0x06000CE1 RID: 3297
		protected abstract TSubSequence SliceFromCombinedInputs(int start, int end);

		// Token: 0x06000CE2 RID: 3298
		protected abstract List<ValueAndCount<TSequence>> FilterResults(List<ValueAndCount<TSequence>> results);

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000CE3 RID: 3299
		protected abstract TSequenceable Terminator { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00025FDC File Offset: 0x000241DC
		private SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode Root
		{
			get
			{
				SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode internalNode;
				if ((internalNode = this._rootNode) == null)
				{
					internalNode = (this._rootNode = new SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode(null));
				}
				return internalNode;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00026002 File Offset: 0x00024202
		public IReadOnlyList<TSequence> Inputs
		{
			get
			{
				return this._inputList;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002600A File Offset: 0x0002420A
		protected SuffixTree()
		{
			this._inputList = new List<TSequence>();
			this._leafNodes = new List<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode>();
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00026028 File Offset: 0x00024228
		protected SuffixTree(IEnumerable<TSequence> inputs)
			: this()
		{
			inputs.ForEach(new Action<TSequence>(this.Add));
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00026044 File Offset: 0x00024244
		public void Add(TSequence input)
		{
			this._inputsAdded = true;
			int sumOfInputLengths = this.SumOfInputLengths;
			int count = this._inputList.Count;
			TSequence tsequence = this.OnNewInput(input);
			this._inputList.Add(tsequence);
			this._Add(sumOfInputLengths, count);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00026088 File Offset: 0x00024288
		private SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus FindTarget(int index, SuffixTree<TSequenceable, TSequence, TSubSequence>.ActivePoint activePoint)
		{
			SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edgeFor;
			for (;;)
			{
				if (activePoint.ActiveLength == 0)
				{
					activePoint.ActiveIndex = index;
				}
				TSequenceable tsequenceable = this.At(activePoint.ActiveIndex);
				edgeFor = activePoint.ActiveNode.GetEdgeFor(tsequenceable);
				if (edgeFor == null)
				{
					break;
				}
				int length = edgeFor.GetLength(index);
				if (activePoint.ActiveLength < length)
				{
					goto IL_0078;
				}
				if (edgeFor.Child is SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode)
				{
					return SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.Implicit;
				}
				activePoint.ActiveIndex += length;
				activePoint.ActiveLength -= length;
				activePoint.ActiveNode = edgeFor.Child;
			}
			return SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.AddLeaf;
			IL_0078:
			TSequenceable tsequenceable2 = this.At(edgeFor.Start + activePoint.ActiveLength);
			if (tsequenceable2.Equals(this.At(index)))
			{
				int num = activePoint.ActiveLength + 1;
				activePoint.ActiveLength = num;
				return SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.Implicit;
			}
			return SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.Split;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002614C File Offset: 0x0002434C
		private void UpdateSuffixLinks(SuffixTree<TSequenceable, TSequence, TSubSequence>.Node activeNode, ref SuffixTree<TSequenceable, TSequence, TSubSequence>.Node previousNode)
		{
			if (previousNode != null)
			{
				previousNode.SuffixLink = activeNode;
			}
			previousNode = activeNode;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00026160 File Offset: 0x00024360
		private void _Add(int start, int inputId)
		{
			SuffixTree<TSequenceable, TSequence, TSubSequence>.ActivePoint activePoint = new SuffixTree<TSequenceable, TSequence, TSubSequence>.ActivePoint
			{
				ActiveNode = this.Root,
				ActiveIndex = 0,
				ActiveLength = 0
			};
			int i = 0;
			List<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode> list = new List<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode>();
			int end = this.SumOfInputLengths;
			for (int j = start; j < end; j++)
			{
				SuffixTree<TSequenceable, TSequence, TSubSequence>.Node node = null;
				i++;
				while (i > 0)
				{
					SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus targetStatus = this.FindTarget(j, activePoint);
					TSequenceable tsequenceable = this.At(j);
					switch (targetStatus)
					{
					case SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.AddLeaf:
					{
						SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode internalNode = activePoint.ActiveNode as SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode;
						SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode leafNode = internalNode.AddLeafChild(tsequenceable, j, int.MaxValue, inputId);
						list.Add(leafNode);
						this.UpdateSuffixLinks(internalNode, ref node);
						break;
					}
					case SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.Implicit:
						break;
					case SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.Split:
					{
						TSequenceable tsequenceable2 = this.At(activePoint.ActiveIndex);
						SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode internalNode2 = activePoint.ActiveNode as SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode;
						SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edge = internalNode2.EdgesToChildren[tsequenceable2];
						SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edge2 = new SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge(edge.Start, edge.Start + activePoint.ActiveLength)
						{
							Parent = internalNode2
						};
						internalNode2.EdgesToChildren[tsequenceable2] = edge2;
						SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode internalNode3 = new SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode(null)
						{
							EdgeFromParent = edge2
						};
						edge2.Child = internalNode3;
						SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode leafNode = internalNode3.AddLeafChild(tsequenceable, j, int.MaxValue, inputId);
						int num = edge.Start + activePoint.ActiveLength;
						internalNode3.LinkChild(this.At(num), edge.Child, new SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge(num, edge.End));
						list.Add(leafNode);
						this.UpdateSuffixLinks(internalNode3, ref node);
						break;
					}
					default:
						throw new NotImplementedException();
					}
					if (targetStatus == SuffixTree<TSequenceable, TSequence, TSubSequence>.TargetStatus.Implicit)
					{
						if (j < end - 1)
						{
							this.UpdateSuffixLinks(activePoint.ActiveNode, ref node);
							break;
						}
						((SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode)activePoint.ActiveNode.GetEdgeFor(this.At(activePoint.ActiveIndex)).Child).CoveredInputs.ExtendAndUpdate(inputId, true);
						if (node != null)
						{
							node.SuffixLink = activePoint.ActiveNode;
						}
						node = null;
					}
					i--;
					if (activePoint.ActiveNode == this.Root && activePoint.ActiveLength > 0)
					{
						SuffixTree<TSequenceable, TSequence, TSubSequence>.ActivePoint activePoint2 = activePoint;
						int num2 = activePoint2.ActiveLength - 1;
						activePoint2.ActiveLength = num2;
						activePoint.ActiveIndex = j - i + 1;
					}
					else
					{
						activePoint.ActiveNode = activePoint.ActiveNode.SuffixLink ?? this.Root;
					}
				}
			}
			list.ForEach(delegate(SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode leaf)
			{
				leaf.EdgeFromParent.End = end;
			});
			this._leafNodes.AddRange(list);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000263EC File Offset: 0x000245EC
		private void UpdateCoveredInputs()
		{
			if (this.Root.CoveredInputs == null)
			{
				this.Root.CoveredInputs = new BitArray(Math.Max(this._inputList.Count, 1), false);
			}
			foreach (SuffixTree<TSequenceable, TSequence, TSubSequence>.Node node in this._leafNodes)
			{
				while (node.EdgeFromParent != null)
				{
					SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode parent = node.EdgeFromParent.Parent;
					if (parent.CoveredInputs == null)
					{
						parent.CoveredInputs = new BitArray(this._inputList.Count, false);
					}
					parent.CoveredInputs.InPlaceOr(node.CoveredInputs, false);
					node = parent;
				}
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000264B4 File Offset: 0x000246B4
		private IEnumerable<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node> GetAncestors(int minCount, int maxCount)
		{
			if (this._inputsAdded)
			{
				this.UpdateCoveredInputs();
			}
			HashSet<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node> hashSet = new HashSet<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node>();
			foreach (SuffixTree<TSequenceable, TSequence, TSubSequence>.Node node in this._leafNodes)
			{
				while (node.EdgeFromParent != null)
				{
					int num = node.CoveredInputs.BitCount();
					if (num >= minCount && num <= maxCount)
					{
						hashSet.Add(node);
					}
					node = node.EdgeFromParent.Parent;
				}
			}
			return hashSet;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00026548 File Offset: 0x00024748
		public IReadOnlyList<ValueAndCount<TSequence>> FindCommonSubSequences(double minFraction, double maxFraction)
		{
			if (this.Inputs.Count == 1)
			{
				return new List<ValueAndCount<TSequence>>
				{
					new ValueAndCount<TSequence>(this.Inputs.First<TSequence>(), 1)
				};
			}
			List<ValueAndCount<TSequence>> list = new List<ValueAndCount<TSequence>>();
			if (minFraction < 0.0 || minFraction > 1.0 || maxFraction < 0.0 || maxFraction > 1.0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid arguments to {0}.{1}.", new object[]
				{
					base.GetType(),
					"FindCommonSubSequences"
				})));
			}
			int num = (int)Math.Ceiling((double)this._inputList.Count * minFraction);
			int num2 = (int)Math.Floor((double)this._inputList.Count * maxFraction);
			foreach (SuffixTree<TSequenceable, TSequence, TSubSequence>.Node node in this.GetAncestors(num, num2))
			{
				list.Add(this.GetSequenceForNode(node));
			}
			return this.FilterResults(list);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00026660 File Offset: 0x00024860
		private ValueAndCount<TSequence> GetSequenceForNode(SuffixTree<TSequenceable, TSequence, TSubSequence>.Node node)
		{
			List<TSubSequence> list = new List<TSubSequence>();
			for (SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edge = node.EdgeFromParent; edge != null; edge = edge.Parent.EdgeFromParent)
			{
				list.Add(this.SliceFromCombinedInputs(edge.Start, edge.End));
			}
			list.Reverse();
			return new ValueAndCount<TSequence>(this.RecoverInput(list), node.CoveredInputs.BitCount());
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000266C0 File Offset: 0x000248C0
		private IEnumerable<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode> GetAllLeavesSatisfyingCoverage(SuffixTree<TSequenceable, TSequence, TSubSequence>.Node node, int coverageCountLow, int coverageCountHigh)
		{
			int num = node.CoveredInputs.BitCount();
			SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode leafNode = node as SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode;
			if (leafNode == null)
			{
				return ((SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode)node).EdgesToChildren.SelectMany((KeyValuePair<TSequenceable, SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge> kvp) => this.GetAllLeavesSatisfyingCoverage(kvp.Value.Child, coverageCountLow, coverageCountHigh));
			}
			if (num >= coverageCountLow && num <= coverageCountHigh)
			{
				return Seq.Of<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode>(new SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode[] { leafNode });
			}
			return Enumerable.Empty<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode>();
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00026744 File Offset: 0x00024944
		protected IReadOnlyList<ValueAndCount<TSequence>> LookupPrefix(TSequence prefix, double minFraction, double maxFraction, SuffixTree<TSequenceable, TSequence, TSubSequence>.Converter caseInverter = null)
		{
			this.UpdateCoveredInputs();
			SuffixTree<TSequenceable, TSequence, TSubSequence>.Converter converter;
			if ((converter = caseInverter) == null && (converter = SuffixTree<TSequenceable, TSequence, TSubSequence>.<>c.<>9__36_0) == null)
			{
				converter = (SuffixTree<TSequenceable, TSequence, TSubSequence>.<>c.<>9__36_0 = (TSequenceable c) => c);
			}
			caseInverter = converter;
			List<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node> list = new List<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node>();
			TSequenceable[] array = prefix.ToArray<TSequenceable>();
			int lowThreshold = (int)Math.Ceiling(minFraction * (double)this._inputList.Count);
			int highThreshold = (int)Math.Floor(maxFraction * (double)this._inputList.Count);
			if (array.Length == 0)
			{
				return this.GetAllLeavesSatisfyingCoverage(this.Root, lowThreshold, highThreshold).Select(new Func<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode, ValueAndCount<TSequence>>(this.GetSequenceForNode)).ToList<ValueAndCount<TSequence>>();
			}
			Queue<Record<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node, int>> queue = new Queue<Record<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node, int>>();
			queue.Enqueue(Record.Create<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node, int>(this.Root, 0));
			while (!queue.IsEmpty<Record<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node, int>>())
			{
				Record<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node, int> record = queue.Dequeue();
				SuffixTree<TSequenceable, TSequence, TSubSequence>.Node item = record.Item1;
				int item2 = record.Item2;
				SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edgeFor = item.GetEdgeFor(array[item2]);
				SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edgeFor2 = item.GetEdgeFor(caseInverter(array[item2]));
				if (edgeFor != null)
				{
					this.ProcessEdge(edgeFor, array, queue, list, item2, caseInverter);
				}
				if (edgeFor2 != null && edgeFor != edgeFor2)
				{
					this.ProcessEdge(edgeFor2, array, queue, list, item2, caseInverter);
				}
			}
			return list.SelectMany((SuffixTree<TSequenceable, TSequence, TSubSequence>.Node n) => this.GetAllLeavesSatisfyingCoverage(n, lowThreshold, highThreshold)).Select(new Func<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode, ValueAndCount<TSequence>>(this.GetSequenceForNode)).ToList<ValueAndCount<TSequence>>();
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000268B8 File Offset: 0x00024AB8
		private void ProcessEdge(SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge e, TSequenceable[] array, Queue<Record<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node, int>> searchQueue, List<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node> lowestNodes, int currentPosition, SuffixTree<TSequenceable, TSequence, TSubSequence>.Converter caseInverter)
		{
			int num = Math.Min(array.Length - currentPosition, e.GetLength(this.SumOfInputLengths));
			bool flag = Enumerable.Range(0, num).All((int i) => object.Equals(array[currentPosition + i], this.At(e.Start + i)) || object.Equals(caseInverter(array[currentPosition + i]), this.At(e.Start + i)));
			int num2 = currentPosition + num;
			if (flag)
			{
				if (num2 < array.Length)
				{
					if (e.Child is SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode)
					{
						searchQueue.Enqueue(Record.Create<SuffixTree<TSequenceable, TSequence, TSubSequence>.Node, int>(e.Child, num2));
						return;
					}
				}
				else
				{
					lowestNodes.Add(e.Child);
				}
			}
		}

		// Token: 0x04000647 RID: 1607
		private const int Infinity = 2147483647;

		// Token: 0x04000648 RID: 1608
		private SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode _rootNode;

		// Token: 0x04000649 RID: 1609
		private readonly List<TSequence> _inputList;

		// Token: 0x0400064A RID: 1610
		private readonly List<SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode> _leafNodes;

		// Token: 0x0400064B RID: 1611
		private bool _inputsAdded;

		// Token: 0x02000259 RID: 601
		// (Invoke) Token: 0x06000CF4 RID: 3316
		public delegate TSequenceable Converter(TSequenceable c);

		// Token: 0x0200025A RID: 602
		private abstract class Node
		{
			// Token: 0x06000CF7 RID: 3319 RVA: 0x00026980 File Offset: 0x00024B80
			protected Node(SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edgeFromParent)
			{
				this.EdgeFromParent = edgeFromParent;
			}

			// Token: 0x1700030A RID: 778
			// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002698F File Offset: 0x00024B8F
			// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00026997 File Offset: 0x00024B97
			public SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge EdgeFromParent { get; set; }

			// Token: 0x1700030B RID: 779
			// (get) Token: 0x06000CFA RID: 3322 RVA: 0x000269A0 File Offset: 0x00024BA0
			// (set) Token: 0x06000CFB RID: 3323 RVA: 0x000269A8 File Offset: 0x00024BA8
			public BitArray CoveredInputs { get; set; }

			// Token: 0x1700030C RID: 780
			// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000269B1 File Offset: 0x00024BB1
			// (set) Token: 0x06000CFD RID: 3325 RVA: 0x000269B9 File Offset: 0x00024BB9
			public SuffixTree<TSequenceable, TSequence, TSubSequence>.Node SuffixLink { get; set; }

			// Token: 0x06000CFE RID: 3326
			public abstract SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge GetEdgeFor(TSequenceable key);
		}

		// Token: 0x0200025B RID: 603
		private class LeafNode : SuffixTree<TSequenceable, TSequence, TSubSequence>.Node
		{
			// Token: 0x06000CFF RID: 3327 RVA: 0x000269C2 File Offset: 0x00024BC2
			public LeafNode(SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edgeFromParent, int coveredInput)
				: base(edgeFromParent)
			{
				base.CoveredInputs = new BitArray(coveredInput + 1);
				base.CoveredInputs.Set(coveredInput, true);
			}

			// Token: 0x06000D00 RID: 3328 RVA: 0x00002188 File Offset: 0x00000388
			public override SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge GetEdgeFor(TSequenceable key)
			{
				return null;
			}
		}

		// Token: 0x0200025C RID: 604
		private class InternalNode : SuffixTree<TSequenceable, TSequence, TSubSequence>.Node
		{
			// Token: 0x06000D01 RID: 3329 RVA: 0x000269E6 File Offset: 0x00024BE6
			public InternalNode(SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edgeFromParent = null)
				: base(edgeFromParent)
			{
			}

			// Token: 0x1700030D RID: 781
			// (get) Token: 0x06000D02 RID: 3330 RVA: 0x000269F0 File Offset: 0x00024BF0
			public Dictionary<TSequenceable, SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge> EdgesToChildren
			{
				get
				{
					Dictionary<TSequenceable, SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge> dictionary;
					if ((dictionary = this._edgesToChildren) == null)
					{
						dictionary = (this._edgesToChildren = new Dictionary<TSequenceable, SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge>());
					}
					return dictionary;
				}
			}

			// Token: 0x06000D03 RID: 3331 RVA: 0x00026A15 File Offset: 0x00024C15
			public void LinkChild(TSequenceable key, SuffixTree<TSequenceable, TSequence, TSubSequence>.Node child, SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edge)
			{
				edge.Parent = this;
				edge.Child = child;
				this.EdgesToChildren[key] = edge;
				child.EdgeFromParent = edge;
			}

			// Token: 0x06000D04 RID: 3332 RVA: 0x00026A3C File Offset: 0x00024C3C
			public SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode AddLeafChild(TSequenceable key, int start, int end, int coveredInput)
			{
				SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edge = new SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge(start, end);
				SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode leafNode = new SuffixTree<TSequenceable, TSequence, TSubSequence>.LeafNode(edge, coveredInput);
				this.LinkChild(key, leafNode, edge);
				return leafNode;
			}

			// Token: 0x06000D05 RID: 3333 RVA: 0x00026A64 File Offset: 0x00024C64
			public override SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge GetEdgeFor(TSequenceable key)
			{
				SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge edge;
				if (this.EdgesToChildren.TryGetValue(key, out edge))
				{
					return edge;
				}
				return null;
			}

			// Token: 0x0400064F RID: 1615
			private Dictionary<TSequenceable, SuffixTree<TSequenceable, TSequence, TSubSequence>.Edge> _edgesToChildren;
		}

		// Token: 0x0200025D RID: 605
		private class Edge
		{
			// Token: 0x06000D06 RID: 3334 RVA: 0x00026A84 File Offset: 0x00024C84
			public Edge(int start, int end)
			{
				this.Start = start;
				this.End = end;
			}

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00026A9A File Offset: 0x00024C9A
			// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00026AA2 File Offset: 0x00024CA2
			public SuffixTree<TSequenceable, TSequence, TSubSequence>.InternalNode Parent { get; set; }

			// Token: 0x1700030F RID: 783
			// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00026AAB File Offset: 0x00024CAB
			// (set) Token: 0x06000D0A RID: 3338 RVA: 0x00026AB3 File Offset: 0x00024CB3
			public SuffixTree<TSequenceable, TSequence, TSubSequence>.Node Child { get; set; }

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00026ABC File Offset: 0x00024CBC
			public int Start { get; }

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00026AC4 File Offset: 0x00024CC4
			// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00026ACC File Offset: 0x00024CCC
			public int End { get; set; }

			// Token: 0x06000D0E RID: 3342 RVA: 0x00026AD5 File Offset: 0x00024CD5
			public int GetLength(int currentIndex)
			{
				return Math.Min(this.End, currentIndex + 1) - this.Start;
			}

			// Token: 0x06000D0F RID: 3343 RVA: 0x00026AEC File Offset: 0x00024CEC
			public override string ToString()
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("[{0}-{1})", new object[] { this.Start, this.End }));
			}
		}

		// Token: 0x0200025E RID: 606
		private class ActivePoint
		{
			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00026B1F File Offset: 0x00024D1F
			// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00026B27 File Offset: 0x00024D27
			public SuffixTree<TSequenceable, TSequence, TSubSequence>.Node ActiveNode { get; set; }

			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00026B30 File Offset: 0x00024D30
			// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00026B38 File Offset: 0x00024D38
			public int ActiveIndex { get; set; }

			// Token: 0x17000314 RID: 788
			// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00026B41 File Offset: 0x00024D41
			// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00026B49 File Offset: 0x00024D49
			public int ActiveLength { get; set; }
		}

		// Token: 0x0200025F RID: 607
		private enum TargetStatus
		{
			// Token: 0x04000658 RID: 1624
			AddLeaf,
			// Token: 0x04000659 RID: 1625
			Implicit,
			// Token: 0x0400065A RID: 1626
			Split
		}
	}
}
