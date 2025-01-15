using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Tree.Utils;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001ECF RID: 7887
	public static class Semantics
	{
		// Token: 0x06010A3F RID: 68159 RVA: 0x000CD646 File Offset: 0x000CB846
		public static bool Conj(bool pred1, bool pred2)
		{
			return pred1 && pred2;
		}

		// Token: 0x06010A40 RID: 68160 RVA: 0x00394A4C File Offset: 0x00392C4C
		private static bool IsAttributePresentHelper(Node node, string name, string value)
		{
			string text;
			return node.Attributes.TryGetValue(name, out text) && value == text;
		}

		// Token: 0x06010A41 RID: 68161 RVA: 0x00394A72 File Offset: 0x00392C72
		public static bool IsNthChild(Node node, int k)
		{
			return node != null && (node.Parent != null && node.Parent.Children != null && node.Parent.Children.Length > k) && node.Parent.Children[k] == node;
		}

		// Token: 0x06010A42 RID: 68162 RVA: 0x00394AB0 File Offset: 0x00392CB0
		public static bool HasNChildren(Node node, TreePath path, int k)
		{
			if (node == null || path == null)
			{
				return false;
			}
			Node node2 = path.Find(node);
			return node2 != null && node2.Children.Length == k;
		}

		// Token: 0x06010A43 RID: 68163 RVA: 0x00394AE0 File Offset: 0x00392CE0
		public static bool IsAttributePresent(Node node, TreePath path, string name, string value)
		{
			Node node2 = path.Find(node);
			return node2 != null && Semantics.IsAttributePresentHelper(node2, name, value);
		}

		// Token: 0x06010A44 RID: 68164 RVA: 0x00394B04 File Offset: 0x00392D04
		public static bool IsKind(Node node, TreePath path, string kind)
		{
			Node node2 = path.Find(node);
			return node2 != null && node2.Label == kind;
		}

		// Token: 0x06010A45 RID: 68165 RVA: 0x000CD7FA File Offset: 0x000CB9FA
		public static bool Not(bool pred)
		{
			return !pred;
		}

		// Token: 0x06010A46 RID: 68166 RVA: 0x00394B2A File Offset: 0x00392D2A
		public static Node[] SingleList(Node node)
		{
			return new Node[] { node.DeepClone() };
		}

		// Token: 0x06010A47 RID: 68167 RVA: 0x00394B3B File Offset: 0x00392D3B
		public static Node[] Prepend(Node[] prefix, Node[] nodes)
		{
			return prefix.Concat(nodes).ToArray<Node>();
		}

		// Token: 0x06010A48 RID: 68168 RVA: 0x00394B3B File Offset: 0x00392D3B
		public static Node[] PrependReplacement(Node[] prefix, IEnumerable<Node> nodes)
		{
			return prefix.Concat(nodes).ToArray<Node>();
		}

		// Token: 0x06010A49 RID: 68169 RVA: 0x00394B49 File Offset: 0x00392D49
		public static Node[] AllNodes(Node node)
		{
			return node.EnumerateDescendantsPostOrder.ToArray<Node>();
		}

		// Token: 0x06010A4A RID: 68170 RVA: 0x00394B56 File Offset: 0x00392D56
		public static Node[] AllNodes(Node node, Func<Node, bool> predicate)
		{
			return node.EnumerateDescendantsPostOrder.Where(predicate).ToArray<Node>();
		}

		// Token: 0x06010A4B RID: 68171 RVA: 0x00394B69 File Offset: 0x00392D69
		public static Node[] InOrderAllNodes(Node node)
		{
			return node.EnumerateDescendantsPreOrder.ToArray<Node>();
		}

		// Token: 0x06010A4C RID: 68172 RVA: 0x00394B78 File Offset: 0x00392D78
		public static IEnumerable<Node> DeleteChild(Node node, Node child)
		{
			if (!node.Children.Any((Node c) => c == child))
			{
				return null;
			}
			return node.Children.Where((Node n) => n != child);
		}

		// Token: 0x06010A4D RID: 68173 RVA: 0x00394BC4 File Offset: 0x00392DC4
		private static Node[] InsertAt(Node parent, int index, Node newChild)
		{
			Node[] children = parent.Children;
			int num = ((index > 0) ? (index - 1) : (children.Length + index + 1));
			if (num < 0 || num > children.Length)
			{
				return null;
			}
			if (num == children.Length)
			{
				return children.AppendItem(newChild).ToArray<Node>();
			}
			if (num == 0)
			{
				return children.PrependItem(newChild).ToArray<Node>();
			}
			IEnumerable<Node> enumerable = children.Take(num);
			IEnumerable<Node> enumerable2 = children.Skip(num);
			return enumerable.AppendItem(newChild).Concat(enumerable2).ToArray<Node>();
		}

		// Token: 0x06010A4E RID: 68174 RVA: 0x00394C38 File Offset: 0x00392E38
		public static Node[] InsertAtAbs(Node parent, int index, Node newChild)
		{
			return Semantics.InsertAt(parent, index, newChild);
		}

		// Token: 0x06010A4F RID: 68175 RVA: 0x00394C44 File Offset: 0x00392E44
		public static IEnumerable<Node> InsertAtRel(Node parent, Node relChild, Node newChild)
		{
			if (parent == null || relChild == null || newChild == null)
			{
				return null;
			}
			int? num = parent.Children.IndexOfByReference(relChild);
			if (num == null)
			{
				return null;
			}
			return Semantics.InsertAt(parent, num.Value + 1, newChild);
		}

		// Token: 0x06010A50 RID: 68176 RVA: 0x00394C84 File Offset: 0x00392E84
		public static IEnumerable<Node> ReplaceChildren(Node node, Node[] replacedChildren, IEnumerable<Node> children)
		{
			List<int?> list = replacedChildren.Select((Node x) => node.Children.IndexOfByReference(x)).ToList<int?>();
			if (!list.IsEmpty<int?>())
			{
				if (!list.Any((int? x) => x == null))
				{
					int? num = list.First<int?>();
					int num2 = 0;
					if (!((num.GetValueOrDefault() < num2) & (num != null)))
					{
						num = list.Last<int?>();
						num2 = node.Children.Count<Node>() - 1;
						if (!((num.GetValueOrDefault() > num2) & (num != null)))
						{
							for (int i = 1; i < list.Count; i++)
							{
								num = list[i];
								int? num3 = list[i - 1] + 1;
								if (!((num.GetValueOrDefault() == num3.GetValueOrDefault()) & (num != null == (num3 != null))))
								{
									return null;
								}
							}
							IEnumerable<Node> enumerable = node.Children.Take(list.First<int?>().Value);
							IEnumerable<Node> enumerable2 = node.Children.Skip(list.Last<int?>().Value + 1);
							return enumerable.Concat(children).Concat(enumerable2).ToArray<Node>();
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06010A51 RID: 68177 RVA: 0x00394B3B File Offset: 0x00392D3B
		public static Node[] ConcatChild(Node[] firstList, Node[] secondList)
		{
			return firstList.Concat(secondList).ToArray<Node>();
		}

		// Token: 0x06010A52 RID: 68178 RVA: 0x00394DFC File Offset: 0x00392FFC
		public static Node[] SinglePosList(Node node)
		{
			return new Node[] { node };
		}

		// Token: 0x06010A53 RID: 68179 RVA: 0x00394E08 File Offset: 0x00393008
		public static int? AbsPos(int k)
		{
			return new int?(k);
		}

		// Token: 0x06010A54 RID: 68180 RVA: 0x00004FAE File Offset: 0x000031AE
		public static Node RelChild(Node node)
		{
			return node;
		}

		// Token: 0x06010A55 RID: 68181 RVA: 0x00394E10 File Offset: 0x00393010
		public static Node LeafConstLabelNode(string label, Dictionary<string, string> attributes)
		{
			return StructNode.Create(label, Utils.CreateAttributesFromDictionary(attributes, Semantics.SemanticsAttributeCache), null, null);
		}

		// Token: 0x06010A56 RID: 68182 RVA: 0x00394E28 File Offset: 0x00393028
		public static Node ConstLabelNode(string label, Dictionary<string, string> attributes, Node[] children)
		{
			if (children.Any((Node c) => c == null))
			{
				return null;
			}
			Node[] array = children.Select((Node c) => c.DeepClone()).ToArray<Node>();
			return StructNode.Create(label, Utils.CreateAttributesFromDictionary(attributes, Semantics.SemanticsAttributeCache), array, null);
		}

		// Token: 0x06010A57 RID: 68183 RVA: 0x00394E9C File Offset: 0x0039309C
		public static Node ConstSequenceLabelNode(string label, Dictionary<string, string> attributes, Node separator, IEnumerable<Node> children)
		{
			if (children.Any((Node c) => c == null))
			{
				return null;
			}
			Node[] array = children.Select((Node c) => c.DeepClone()).ToArray<Node>();
			return new SequenceNode(label, Utils.CreateAttributesFromDictionary(attributes, Semantics.SemanticsAttributeCache), null, array, separator.DeepClone());
		}

		// Token: 0x06010A58 RID: 68184 RVA: 0x00394F16 File Offset: 0x00393116
		public static Node LeafConstSequenceLabelNode(string label, Dictionary<string, string> attributes, Node separator)
		{
			return new SequenceNode(label, Utils.CreateAttributesFromDictionary(attributes, Semantics.SemanticsAttributeCache), null, null, separator.DeepClone());
		}

		// Token: 0x06010A59 RID: 68185 RVA: 0x00394F31 File Offset: 0x00393131
		public static Node[] Children(Node parent)
		{
			return parent.Children;
		}

		// Token: 0x06010A5A RID: 68186 RVA: 0x00394F39 File Offset: 0x00393139
		public static Node GuardedRule(bool guard, Node transformation)
		{
			if (!guard)
			{
				return null;
			}
			return transformation;
		}

		// Token: 0x04006371 RID: 25457
		private static readonly ConcurrentLruCache<Record<string, string>, Attributes.Attribute> SemanticsAttributeCache = new ConcurrentLruCache<Record<string, string>, Attributes.Attribute>(4096, null, null, null);
	}
}
