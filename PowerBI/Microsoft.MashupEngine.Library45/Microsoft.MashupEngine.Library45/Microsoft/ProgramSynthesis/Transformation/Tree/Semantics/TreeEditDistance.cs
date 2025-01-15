using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001ED3 RID: 7891
	internal class TreeEditDistance
	{
		// Token: 0x06010A68 RID: 68200 RVA: 0x00394F8D File Offset: 0x0039318D
		internal TreeEditDistance(Node previousTree, Node currentTree)
		{
			this._previousTree = previousTree;
			this._currentTree = currentTree;
		}

		// Token: 0x06010A69 RID: 68201 RVA: 0x00394FA4 File Offset: 0x003931A4
		private void GenerateNodes(Node t1, Node t2)
		{
			this.A = new List<Node>();
			TreeEditDistance.PostWalk(t1, this.A);
			this.T1 = Enumerable.Range(1, this.A.Count).ToArray<int>();
			this.B = new List<Node>();
			TreeEditDistance.PostWalk(t2, this.B);
			this.T2 = Enumerable.Range(1, this.B.Count).ToArray<int>();
		}

		// Token: 0x06010A6A RID: 68202 RVA: 0x00395018 File Offset: 0x00393218
		private static void PostWalk(Node node, List<Node> nodes)
		{
			Node[] children = node.Children;
			for (int i = 0; i < children.Length; i++)
			{
				TreeEditDistance.PostWalk(children[i], nodes);
			}
			nodes.Add(node);
		}

		// Token: 0x06010A6B RID: 68203 RVA: 0x0039504C File Offset: 0x0039324C
		private IReadOnlyList<int> ComputeK(int[] tree, int[] l)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < tree.Length; i++)
			{
				bool flag = true;
				if (i < tree.Length - 1)
				{
					for (int j = i + 1; j < tree.Length; j++)
					{
						if (l[tree[i]] == l[tree[j]])
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					list.Add(tree[i]);
				}
			}
			return list;
		}

		// Token: 0x06010A6C RID: 68204 RVA: 0x003950A4 File Offset: 0x003932A4
		private int[] ComputeL(int[] t1, List<Node> tree)
		{
			int[] array = new int[t1.Length + 1];
			array[0] = 0;
			for (int i = 0; i < t1.Length; i++)
			{
				int num = t1[i];
				Node currentNode = tree[num - 1];
				array[num] = tree.FindIndex((Node n) => n == TreeEditDistance.GetLeftMostDescendant(currentNode)) + 1;
			}
			return array;
		}

		// Token: 0x06010A6D RID: 68205 RVA: 0x00395104 File Offset: 0x00393304
		private static Node GetLeftMostDescendant(Node node)
		{
			List<Node> list = new List<Node>();
			TreeEditDistance.PostWalk(node, list);
			if (list.Count == 0)
			{
				throw new Exception("list should not be empty");
			}
			return list.First<Node>();
		}

		// Token: 0x06010A6E RID: 68206 RVA: 0x00395138 File Offset: 0x00393338
		internal EditDistanceResult Compute()
		{
			this.GenerateNodes(this._previousTree, this._currentTree);
			this._l1 = this.ComputeL(this.T1, this.A);
			this._l2 = this.ComputeL(this.T2, this.B);
			this._k1 = this.ComputeK(this.T1, this._l1);
			this._k2 = this.ComputeK(this.T2, this._l2);
			this._treedists = new EditDistanceResult[this.T1.Length + 1, this.T2.Length + 1];
			this._treedists[0, 0] = new EditDistanceResult
			{
				Distance = 0
			};
			foreach (int num in this._k1)
			{
				foreach (int num2 in this._k2)
				{
					this.Treedists(num, num2);
				}
			}
			return this._treedists[this.T1.Length, this.T2.Length];
		}

		// Token: 0x06010A6F RID: 68207 RVA: 0x00395280 File Offset: 0x00393480
		private void Treedists(int i, int j)
		{
			int num = i - this._l1[i] + 2;
			int num2 = j - this._l2[j] + 2;
			EditDistanceResult[,] array = new EditDistanceResult[num, num2];
			array[0, 0] = new EditDistanceResult
			{
				Distance = 0
			};
			int num3 = this._l1[i] - 1;
			int num4 = this._l2[j] - 1;
			for (int k = 1; k < num; k++)
			{
				for (int l = 1; l < num2; l++)
				{
					if (this._l1[i] == this._l1[k + num3] && this._l2[j] == this._l2[l + num4])
					{
						int num5 = TreeEditDistance.CostUpdateTree(this.A[k + num3 - 1], this.B[l + num4 - 1]);
						if (array[k - 1, l - 1] != null)
						{
							num5 = Math.Min(num5, array[k - 1, l - 1].Distance + TreeEditDistance.CostUpdate(this.A[k + num3 - 1], this.B[l + num4 - 1]));
						}
						List<Edit> list;
						Dictionary<Node, Node> dictionary;
						if (num5 == TreeEditDistance.CostUpdateTree(this.A[k + num3 - 1], this.B[l + num4 - 1]) || this.UpdateAllChildren(array[k - 1, l - 1].Edits))
						{
							num5 = TreeEditDistance.CostUpdateTree(this.A[k + num3 - 1], this.B[l + num4 - 1]);
							list = new List<Edit>();
							Node node = this.A[k + num3 - 1];
							Node node2 = this.B[l + num4 - 1];
							if (TreeEditDistance.CostUpdateTree(node, node2) > 0)
							{
								list.Add(new Update(node2, node));
							}
							dictionary = new Dictionary<Node, Node> { { node2, node } };
						}
						else
						{
							list = new List<Edit>(array[k - 1, l - 1].Edits);
							Node node3 = this.A[k + num3 - 1];
							Node node4 = this.B[l + num4 - 1];
							if (TreeEditDistance.CostUpdate(node3, node4) > 0)
							{
								list.Add(new Update(node4, node3));
							}
							dictionary = new Dictionary<Node, Node>(array[k - 1, l - 1].Mapping);
							if (dictionary.ContainsKey(node4))
							{
								dictionary.Remove(node4);
							}
							dictionary.Add(node4, node3);
						}
						array[k, l] = new EditDistanceResult
						{
							Distance = num5,
							Edits = list,
							Mapping = dictionary
						};
						this._treedists[k + num3, l + num4] = array[k, l];
					}
					else
					{
						int num6 = this._l1[k + num3] - 1 - num3;
						int num7 = this._l2[l + num4] - 1 - num4;
						if (array[num6, num7] != null && this._treedists[k + num3, l + num4] != null)
						{
							int num8 = array[num6, num7].Distance + this._treedists[k + num3, l + num4].Distance;
							List<Edit> list2 = new List<Edit>(array[num6, num7].Edits);
							list2.AddRange(this._treedists[k + num3, l + num4].Edits);
							Dictionary<Node, Node> dictionary2 = new Dictionary<Node, Node>(array[num6, num7].Mapping);
							foreach (KeyValuePair<Node, Node> keyValuePair in this._treedists[k + num3, l + num4].Mapping)
							{
								if (dictionary2.ContainsKey(keyValuePair.Key))
								{
									dictionary2.Remove(keyValuePair.Key);
								}
								Node node5 = null;
								foreach (KeyValuePair<Node, Node> keyValuePair2 in dictionary2)
								{
									if (keyValuePair2.Value.Equals(keyValuePair.Value))
									{
										node5 = keyValuePair2.Key;
									}
								}
								if (node5 != null)
								{
									dictionary2.Remove(node5);
								}
								dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
							}
							array[k, l] = new EditDistanceResult
							{
								Distance = num8,
								Edits = list2,
								Mapping = dictionary2
							};
						}
					}
				}
			}
		}

		// Token: 0x06010A70 RID: 68208 RVA: 0x0039574C File Offset: 0x0039394C
		private bool UpdateAllChildren(List<Edit> edits)
		{
			if (edits.IsEmpty<Edit>())
			{
				return false;
			}
			Edit edit = edits.First<Edit>();
			HashSet<Node> hashSet = edits.Select((Edit e) => e.Target.Parent).ConvertToHashSet<Node>();
			return (hashSet.Count == 1 && hashSet.First<Node>().Children.Length == edits.Count) || ((double)hashSet.Count / (double)edits.Count >= 0.5 && (double)edits.Count / (double)edit.Target.Parent.Children.Length > 0.5 && edit.Target.Parent.Count < 500);
		}

		// Token: 0x06010A71 RID: 68209 RVA: 0x00395810 File Offset: 0x00393A10
		private static int CostUpdate(Node inputNode, Node outputNode)
		{
			if (!(inputNode.Label == outputNode.Label) || !inputNode.Attributes.Equals(outputNode.Attributes))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06010A72 RID: 68210 RVA: 0x0039583B File Offset: 0x00393A3B
		private static int CostUpdateTree(Node inputTree, Node outputTree)
		{
			if (!inputTree.Equals(outputTree))
			{
				return outputTree.Count;
			}
			return 0;
		}

		// Token: 0x0400637A RID: 25466
		private const double ThresholdForEditsWithSameParent = 0.5;

		// Token: 0x0400637B RID: 25467
		private const double ThresholdForEditsInParentsChildren = 0.5;

		// Token: 0x0400637C RID: 25468
		private const int NodesForSingleLine = 500;

		// Token: 0x0400637D RID: 25469
		private readonly Node _previousTree;

		// Token: 0x0400637E RID: 25470
		private readonly Node _currentTree;

		// Token: 0x0400637F RID: 25471
		protected List<Node> A;

		// Token: 0x04006380 RID: 25472
		protected List<Node> B;

		// Token: 0x04006381 RID: 25473
		protected int[] T1;

		// Token: 0x04006382 RID: 25474
		protected int[] T2;

		// Token: 0x04006383 RID: 25475
		private int[] _l1;

		// Token: 0x04006384 RID: 25476
		private int[] _l2;

		// Token: 0x04006385 RID: 25477
		private IReadOnlyList<int> _k1;

		// Token: 0x04006386 RID: 25478
		private IReadOnlyList<int> _k2;

		// Token: 0x04006387 RID: 25479
		private EditDistanceResult[,] _treedists;
	}
}
