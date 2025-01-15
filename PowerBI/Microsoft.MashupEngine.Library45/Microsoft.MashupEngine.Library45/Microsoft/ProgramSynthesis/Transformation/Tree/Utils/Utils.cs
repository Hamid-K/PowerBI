using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Utils
{
	// Token: 0x02001EAD RID: 7853
	public static class Utils
	{
		// Token: 0x06010923 RID: 67875 RVA: 0x0038FBD0 File Offset: 0x0038DDD0
		public static string GenerateCode(this Node node, string language = "CSharp", string[] sourceLines = null, Node sourceNode = null)
		{
			if (language == "Python")
			{
				return PyPrintCodeVisitor.CreateAndVisit(node, Utils.NoSpacesBeforePython, Utils.NoSpacesAfterPython, sourceLines, sourceNode, -1);
			}
			if (language == "TypeScript")
			{
				return DefaultPrintCodeVisitor.CreateAndVisit(node, Utils.NoSpacesBeforeTS, Utils.NoSpacesAfterTS, sourceLines, sourceNode);
			}
			if (!(language == "CSharp"))
			{
				return DefaultPrintCodeVisitor.CreateAndVisit(node, Utils.NoSpacesBeforeDefault, Utils.NoSpacesAfterDefault, sourceLines, sourceNode);
			}
			return DefaultPrintCodeVisitor.CreateAndVisit(node, Utils.NoSpacesBeforeCSharp, Utils.NoSpacesAfterCSharp, sourceLines, sourceNode);
		}

		// Token: 0x06010924 RID: 67876 RVA: 0x0038FC54 File Offset: 0x0038DE54
		public static bool CodeEquals(this Node node, IEnumerable<Node> otherNodes)
		{
			IEnumerable<string> enumerable = node.GenerateCode("CSharp", null, null).Split(Utils.SplitChars);
			IEnumerable<string> enumerable2 = otherNodes.SelectMany((Node n) => n.GenerateCode("CSharp", null, null).Split(Utils.SplitChars));
			return enumerable.SequenceEqual(enumerable2);
		}

		// Token: 0x06010925 RID: 67877 RVA: 0x0038FCA4 File Offset: 0x0038DEA4
		public static bool CodeEquals(this Node node, Node otherNode)
		{
			return node.GenerateCode("CSharp", null, null) == otherNode.GenerateCode("CSharp", null, null);
		}

		// Token: 0x06010926 RID: 67878 RVA: 0x0038FCC8 File Offset: 0x0038DEC8
		public static Node CloneTreeAndContext(this Node nodeToClone)
		{
			Node node2;
			if (nodeToClone.Parent != null)
			{
				int? num = nodeToClone.Parent.Children.IndexOfByReference(nodeToClone);
				if (num == null)
				{
					throw new Exception("Inconsistent tree. Node should be in parent's children list");
				}
				Node node = nodeToClone.Parent;
				Stack<int?> stack = new Stack<int?>();
				stack.Push(num);
				if (node.HasPosition)
				{
					while (node.Parent != null && node.Parent.StartPosition.Line == node.StartPosition.Line && node.Parent.EndPosition.Line == node.EndPosition.Line)
					{
						num = node.Parent.Children.IndexOfByReference(node);
						if (num == null)
						{
							throw new Exception("Inconsistent tree. Node should be in parent's children list");
						}
						stack.Push(num);
						node = node.Parent;
					}
				}
				node2 = node.DeepClone();
				while (stack.Any<int?>())
				{
					node2 = node2.Children[stack.Pop().Value];
				}
			}
			else
			{
				node2 = nodeToClone.DeepClone();
			}
			return node2;
		}

		// Token: 0x06010927 RID: 67879 RVA: 0x0038FDCC File Offset: 0x0038DFCC
		public static IEnumerable<Attributes.Attribute> GetRelevantAttributes(this Node node)
		{
			if (!node.Children.Any<Node>())
			{
				return node.Attributes.AllAttributes;
			}
			return node.Attributes.AllAttributes.Where((Attributes.Attribute e) => !string.IsNullOrEmpty(e.Value));
		}

		// Token: 0x06010928 RID: 67880 RVA: 0x0038FE23 File Offset: 0x0038E023
		public static Attributes CreateAttributesFromDictionary(Dictionary<string, string> attributes)
		{
			return new Attributes(attributes.Select((KeyValuePair<string, string> kvp) => Attributes.Attribute.Create(kvp.Key, kvp.Value)));
		}

		// Token: 0x06010929 RID: 67881 RVA: 0x0038FE50 File Offset: 0x0038E050
		public static Attributes CreateAttributesFromDictionary(Dictionary<string, string> attributes, ConcurrentLruCache<Record<string, string>, Attributes.Attribute> cache)
		{
			return new Attributes(attributes.Select((KeyValuePair<string, string> kvp) => Attributes.Attribute.Create(kvp.Key, kvp.Value, cache)));
		}

		// Token: 0x0601092A RID: 67882 RVA: 0x0038FE81 File Offset: 0x0038E081
		public static Attributes BuildValueAttribute(string value, ConcurrentLruCache<Record<string, string>, Attributes.Attribute> cache)
		{
			return new Attributes(new Attributes.Attribute[] { Attributes.Attribute.Create("value", value, cache) });
		}

		// Token: 0x0601092B RID: 67883 RVA: 0x0038FEA1 File Offset: 0x0038E0A1
		public static Attributes BuildValueAttribute(string value)
		{
			return new Attributes(new Attributes.Attribute[] { Attributes.Attribute.Create("value", value) });
		}

		// Token: 0x0601092C RID: 67884 RVA: 0x0038FEC0 File Offset: 0x0038E0C0
		public static int? GetLine(this Node node)
		{
			if (!node.HasPosition)
			{
				return null;
			}
			int line = node.StartPosition.Line;
			int line2 = node.EndPosition.Line;
			if (line != line2)
			{
				return null;
			}
			return new int?(line);
		}

		// Token: 0x0601092D RID: 67885 RVA: 0x0038FF0B File Offset: 0x0038E10B
		public static Dictionary<string, int> GetTokenValueCounts(this Node node)
		{
			return node.GetLeaves().SelectValues((Node node) => node.Attributes.MaybeGet("value")).ToMultiset<string>();
		}

		// Token: 0x0601092E RID: 67886 RVA: 0x0038FF3C File Offset: 0x0038E13C
		public static int GetIndentLevelAttr(this Node node)
		{
			string text = null;
			if (node.Attributes.TryGetValue("indentLevel", out text))
			{
				return Convert.ToInt32(text);
			}
			return -1;
		}

		// Token: 0x0601092F RID: 67887 RVA: 0x0038FF67 File Offset: 0x0038E167
		public static IEnumerable<Node> GetLeaves(this Node node)
		{
			if (node.Children.Length == 0)
			{
				yield return node;
			}
			else
			{
				foreach (Node node2 in node.Children)
				{
					foreach (Node node3 in node2.GetLeaves())
					{
						yield return node3;
					}
					IEnumerator<Node> enumerator = null;
				}
				Node[] array = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06010930 RID: 67888 RVA: 0x0038FF77 File Offset: 0x0038E177
		public static IEnumerable<ProgramNode> GetProgramNodesByName(this ProgramNode node, string nodeName)
		{
			return node.AcceptVisitor<IEnumerable<ProgramNode>>(new GetNodesByNameVisitor(nodeName));
		}

		// Token: 0x04006301 RID: 25345
		public const string Value = "value";

		// Token: 0x04006302 RID: 25346
		public const string CallExpression = "call";

		// Token: 0x04006303 RID: 25347
		public const string Caller = "caller";

		// Token: 0x04006304 RID: 25348
		internal static readonly Attributes BuildEmptyAttribute = new Attributes(new Attributes.Attribute[] { Attributes.Attribute.WellKnownAttributes.EmptyValueAttribute });

		// Token: 0x04006305 RID: 25349
		private static readonly HashSet<char> NoSpacesBeforeDefault = new HashSet<char> { '.', ',', '(', ')', '@', ';', '[', ']' };

		// Token: 0x04006306 RID: 25350
		private static readonly HashSet<char> NoSpacesAfterDefault = new HashSet<char> { '.', '(', '@', '[' };

		// Token: 0x04006307 RID: 25351
		private static readonly HashSet<char> NoSpacesBeforePython = Utils.NoSpacesBeforeDefault;

		// Token: 0x04006308 RID: 25352
		private static readonly HashSet<char> NoSpacesAfterPython = Utils.NoSpacesAfterDefault;

		// Token: 0x04006309 RID: 25353
		private static readonly HashSet<char> NoSpacesBeforeTS = Utils.NoSpacesBeforeDefault;

		// Token: 0x0400630A RID: 25354
		private static readonly HashSet<char> NoSpacesAfterTS = Utils.NoSpacesAfterDefault;

		// Token: 0x0400630B RID: 25355
		private static readonly HashSet<char> NoSpacesBeforeCSharp = Utils.NoSpacesBeforeDefault;

		// Token: 0x0400630C RID: 25356
		private static readonly HashSet<char> NoSpacesAfterCSharp = Utils.NoSpacesAfterDefault;

		// Token: 0x0400630D RID: 25357
		private static readonly char[] SplitChars = new char[] { ' ', '\n' };
	}
}
