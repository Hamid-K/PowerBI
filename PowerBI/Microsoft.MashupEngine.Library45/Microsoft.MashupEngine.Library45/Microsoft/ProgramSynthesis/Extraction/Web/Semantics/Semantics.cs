using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x0200117D RID: 4477
	public static class Semantics
	{
		// Token: 0x06008529 RID: 34089 RVA: 0x001C026C File Offset: 0x001BE46C
		static Semantics()
		{
			string text = "(Jan(uary)?|Feb(ruary)?|Mar(ch)?|Apr(il)?|May|Jun(e)?|Jul(y)?|Aug(ust)?|Sep(tember)?|Oct(tober)?|Nov(ember)?|Dec(ember)?)";
			string text2 = "((\\d?\\d)|" + text + ")";
			Semantics.DateRegex = new Regex(string.Concat(new string[]
			{
				"(?i)(?<!\\d)(\\d?\\d)(-", text2, "-|\\/", text2, "\\/|\\.", text2, "\\.)(19|20)?\\d\\d|(19|20)?\\d\\d(-", text2, "-|\\/", text2,
				"\\/|\\.", text2, "\\.)(\\d?\\d)|", text2, "\\s(\\d?)\\d, (19|20)?\\d\\d(?!\\d)"
			}), RegexOptions.Compiled);
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x0600852A RID: 34090 RVA: 0x001C0309 File Offset: 0x001BE509
		public static IReadOnlyDictionary<string, Token> Tokens
		{
			get
			{
				return Token.Tokens;
			}
		}

		// Token: 0x0600852B RID: 34091 RVA: 0x001C0310 File Offset: 0x001BE510
		public static Token GetStaticTokenByName(string name)
		{
			return Semantics.Tokens.MaybeGet(name).OrElseDefault<Token>();
		}

		// Token: 0x0600852C RID: 34092 RVA: 0x001C0322 File Offset: 0x001BE522
		public static bool NodeName(string name, IDomNode region)
		{
			return string.Equals(name, region.NodeName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600852D RID: 34093 RVA: 0x001C0331 File Offset: 0x001BE531
		public static bool NodeNames(string[] names, IDomNode region)
		{
			return names.Contains(region.NodeName, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600852E RID: 34094 RVA: 0x001C0344 File Offset: 0x001BE544
		public static bool ContainsLeafNodes(string[] names, IDomNode region)
		{
			foreach (string text in names)
			{
				if (!region.LeafNodes.Contains(text, StringComparer.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600852F RID: 34095 RVA: 0x001C037B File Offset: 0x001BE57B
		public static bool NthChild(int idx1, IDomNode region)
		{
			return region.Index == idx1;
		}

		// Token: 0x06008530 RID: 34096 RVA: 0x001C0386 File Offset: 0x001BE586
		public static bool NthLastChild(int idx2, IDomNode region)
		{
			return region.IndexFromLast == idx2;
		}

		// Token: 0x06008531 RID: 34097 RVA: 0x001C0391 File Offset: 0x001BE591
		public static bool Class(string name, IDomNode region)
		{
			return region.Classes.Contains(name, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06008532 RID: 34098 RVA: 0x001C03A4 File Offset: 0x001BE5A4
		public static bool ID_substring(string id, IDomNode region)
		{
			return region.Id.Contains(id);
		}

		// Token: 0x06008533 RID: 34099 RVA: 0x001C03B4 File Offset: 0x001BE5B4
		public static bool HasAttribute(string name, string value, IDomNode node)
		{
			string attribute = node.GetAttribute(name);
			return attribute != null && attribute.Equals(value, StringComparison.Ordinal);
		}

		// Token: 0x06008534 RID: 34100 RVA: 0x001C03D8 File Offset: 0x001BE5D8
		public static bool HasStyle(string name, string value, IDomNode node)
		{
			string style = node.GetStyle(name);
			return style != null && style.Equals(value, StringComparison.Ordinal);
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x001C03FA File Offset: 0x001BE5FA
		public static bool HasEntityAnchor(EntityDetector[] entityObjs, KeyDirections direction, IDomNode region)
		{
			return entityObjs != null && entityObjs.Any<EntityDetector>() && region != null && Semantics.CheckForEntities(entityObjs, region, direction);
		}

		// Token: 0x06008536 RID: 34102 RVA: 0x001C0414 File Offset: 0x001BE614
		public static bool CheckForEntities(EntityDetector[] entityObjs, IDomNode region, KeyDirections dir)
		{
			return (from p in DomNodeKVPUtils.GetKeys(region, dir, 3)
				where !string.IsNullOrEmpty(p.TrimmedInnerText)
				select p).Any((IDomNode node) => entityObjs.All((EntityDetector entity) => entity.HasEntity(node.TrimmedInnerText)));
		}

		// Token: 0x06008537 RID: 34103 RVA: 0x000CD7FA File Offset: 0x000CB9FA
		public static bool Not(bool val)
		{
			return !val;
		}

		// Token: 0x06008538 RID: 34104 RVA: 0x001C046C File Offset: 0x001BE66C
		public static IEnumerable<IDomNode> ChildrenOf(object rawNodes)
		{
			return from n in (from IDomNode x in rawNodes.ToEnumerable<object>()
					where x != null
					select x).SelectMany((IDomNode x) => x.GetChildren())
				orderby n.Start
				select n;
		}

		// Token: 0x06008539 RID: 34105 RVA: 0x001C04F0 File Offset: 0x001BE6F0
		public static IEnumerable<IDomNode> LeafChildrenOf(object rawNodes)
		{
			return Semantics.ChildrenOf(rawNodes);
		}

		// Token: 0x0600853A RID: 34106 RVA: 0x001C04F8 File Offset: 0x001BE6F8
		public static IEnumerable<IDomNode> ToRegions(IEnumerable<object> regions)
		{
			return regions.Cast<IDomNode>();
		}

		// Token: 0x0600853B RID: 34107 RVA: 0x001C0500 File Offset: 0x001BE700
		public static bool ChildrenCount(int count, IDomNode r)
		{
			return r.ChildrenCount == count;
		}

		// Token: 0x0600853C RID: 34108 RVA: 0x001C050B File Offset: 0x001BE70B
		public static bool TitleIs(string str, IDomNode r)
		{
			return string.Equals(r.Title, str);
		}

		// Token: 0x0600853D RID: 34109 RVA: 0x001C051C File Offset: 0x001BE71C
		public static bool ContainsDate(IDomNode n)
		{
			string trimmedInnerText = n.TrimmedInnerText;
			return trimmedInnerText != null && (double)trimmedInnerText.Length <= 200.0 && Semantics.DateRegex.Match(trimmedInnerText).Success;
		}

		// Token: 0x0600853E RID: 34110 RVA: 0x001C0558 File Offset: 0x001BE758
		public static bool ContainsNum(IDomNode n)
		{
			string trimmedInnerText = n.TrimmedInnerText;
			if (trimmedInnerText == null || (double)trimmedInnerText.Length > 200.0)
			{
				return false;
			}
			IEnumerable<char> enumerable = trimmedInnerText;
			Func<char, bool> func;
			if ((func = Semantics.<>O.<0>__IsNumber) == null)
			{
				func = (Semantics.<>O.<0>__IsNumber = new Func<char, bool>(char.IsNumber));
			}
			return enumerable.All(func);
		}

		// Token: 0x0600853F RID: 34111 RVA: 0x00004FAE File Offset: 0x000031AE
		public static IEnumerable<IDomNode> SingleSelection(IEnumerable<IDomNode> selection)
		{
			return selection;
		}

		// Token: 0x06008540 RID: 34112 RVA: 0x001C05A4 File Offset: 0x001BE7A4
		public static IEnumerable<IDomNode> DisjSelection(IEnumerable<IDomNode> disjSelection, IEnumerable<IDomNode> selection)
		{
			if (disjSelection.Any<IDomNode>())
			{
				return disjSelection;
			}
			return selection;
		}

		// Token: 0x06008541 RID: 34113 RVA: 0x001C05B4 File Offset: 0x001BE7B4
		public static IEnumerable<IDomNode> CSSSelection(string cssSelector, IEnumerable<IDomNode> docNodes)
		{
			IDomNode domNode = docNodes.FirstOrDefault<IDomNode>();
			IEnumerable<IDomNode> enumerable = ((domNode != null) ? domNode.Document.Select(cssSelector) : null);
			return enumerable ?? Enumerable.Empty<IDomNode>();
		}

		// Token: 0x06008542 RID: 34114 RVA: 0x001C05E4 File Offset: 0x001BE7E4
		[LazySemantics]
		public static ValueSubstring[] SingleSubstring(ValueSubstring v)
		{
			return new ValueSubstring[] { v };
		}

		// Token: 0x06008543 RID: 34115 RVA: 0x001C05F0 File Offset: 0x001BE7F0
		[LazySemantics]
		public static ValueSubstring[] DisjSubstring(ValueSubstring[] substringDisj, ValueSubstring v)
		{
			return substringDisj.AppendItem(v).ToArray<ValueSubstring>();
		}

		// Token: 0x06008544 RID: 34116 RVA: 0x001C05FE File Offset: 0x001BE7FE
		public static IEnumerable<IDomNode> YoungerSiblingsOf(IDomNode node)
		{
			return node.GetYoungerSiblings();
		}

		// Token: 0x06008545 RID: 34117 RVA: 0x001C0606 File Offset: 0x001BE806
		public static WebRegion NodeToWebRegion(IDomNode node)
		{
			return node.ToWebRegion();
		}

		// Token: 0x06008546 RID: 34118 RVA: 0x001C0606 File Offset: 0x001BE806
		public static WebRegion NodeToWebRegionInSequence(IDomNode node)
		{
			return node.ToWebRegion();
		}

		// Token: 0x06008547 RID: 34119 RVA: 0x001C060E File Offset: 0x001BE80E
		public static WebRegion NodeRegionToWebRegion(IDomNode beginNode, IDomNode endNode)
		{
			return new WebRegion(beginNode, endNode);
		}

		// Token: 0x06008548 RID: 34120 RVA: 0x001C060E File Offset: 0x001BE80E
		public static WebRegion NodeRegionToWebRegionInSequence(IDomNode beginNode, IDomNode endNode)
		{
			return new WebRegion(beginNode, endNode);
		}

		// Token: 0x06008549 RID: 34121 RVA: 0x001C0617 File Offset: 0x001BE817
		public static IEnumerable<WebRegion> Union(IEnumerable<WebRegion> selection1, IEnumerable<WebRegion> selection2)
		{
			return (from r in selection1.Union(selection2)
				orderby r.BeginNode.Start
				select r).ToArray<WebRegion>();
		}

		// Token: 0x0600854A RID: 34122 RVA: 0x001C064C File Offset: 0x001BE84C
		public static ValueSubstring GetValueSubstring(WebRegion r)
		{
			return ValueSubstring.Create(r.Text(), null, null, null, null);
		}

		// Token: 0x0600854B RID: 34123 RVA: 0x001C0678 File Offset: 0x001BE878
		public static string[] AppendField(string[] list, string[] f)
		{
			return list.Concat(f).ToArray<string>();
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x001C0686 File Offset: 0x001BE886
		public static string[] TrimmedTextField(WebRegion r)
		{
			string[] array = new string[1];
			int num = 0;
			string text = r.Text();
			array[num] = ((text != null) ? text.Trim() : null);
			return array;
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x001C06A3 File Offset: 0x001BE8A3
		public static string[] GetTrimmedTextValues(IEnumerable<WebRegion> regions)
		{
			return regions.Select(delegate(WebRegion r)
			{
				string text = r.Text();
				if (text == null)
				{
					return null;
				}
				return text.Trim();
			}).ToArray<string>();
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x001C06CF File Offset: 0x001BE8CF
		public static IEnumerable<WebRegion> EmptySequence()
		{
			return Enumerable.Empty<WebRegion>();
		}

		// Token: 0x0600854F RID: 34127 RVA: 0x001C06D6 File Offset: 0x001BE8D6
		public static IEnumerable<IEnumerable<WebRegion>> ColumnSequence(IEnumerable<IEnumerable<WebRegion>> columnSelectors, IEnumerable<WebRegion> resultSequence)
		{
			return columnSelectors.AppendItem(resultSequence).ToList<IEnumerable<WebRegion>>();
		}

		// Token: 0x06008550 RID: 34128 RVA: 0x001C06E4 File Offset: 0x001BE8E4
		public static IEnumerable<IEnumerable<WebRegion>> SingleColumn(IEnumerable<WebRegion> resultSequence)
		{
			return new IEnumerable<WebRegion>[] { resultSequence };
		}

		// Token: 0x06008551 RID: 34129 RVA: 0x001C06F0 File Offset: 0x001BE8F0
		[LazySemantics]
		public static IEnumerable<IEnumerable<WebRegion>> ExtractTable(IEnumerable<IEnumerable<WebRegion>> columnSelectors)
		{
			List<List<WebRegion>> list = columnSelectors.Select((IEnumerable<WebRegion> c) => c.ToList<WebRegion>()).ToList<List<WebRegion>>();
			int num = list.Max((List<WebRegion> c) => c.Count);
			foreach (List<WebRegion> list2 in list)
			{
				int num2 = num - list2.Count;
				for (int i = 0; i < num2; i++)
				{
					list2.Add(null);
				}
			}
			return list;
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x001C07AC File Offset: 0x001BE9AC
		[LazySemantics]
		public static IEnumerable<IEnumerable<WebRegion>> ExtractRowBasedTable(IEnumerable<IEnumerable<WebRegion>> columnSelectors, IEnumerable<WebRegion> rowSelector)
		{
			IDomNode[] array = rowSelector.Select((WebRegion r) => r.BeginNode).ToArray<IDomNode>();
			return (from c in Semantics.GetBoundaryBasedRowAlignment(columnSelectors.Select((IEnumerable<WebRegion> c) => c.Select((WebRegion r) => r.BeginNode).ToArray<IDomNode>()).ToArray<IDomNode[]>(), array)
				select c.Select(delegate(IDomNode n)
				{
					if (n != null)
					{
						return new WebRegion(n);
					}
					return null;
				}).ToList<WebRegion>()).ToList<List<WebRegion>>();
		}

		// Token: 0x06008553 RID: 34131 RVA: 0x001C0840 File Offset: 0x001BEA40
		public static IDomNode[][] GetBoundaryBasedRowAlignment(IEnumerable<IEnumerable<IDomNode>> columnNodes, IEnumerable<IDomNode> rowNodes)
		{
			int[] rowNodeIndexes = rowNodes.Select((IDomNode n) => n.Start).ToArray<int>();
			int numRows = rowNodeIndexes.Length;
			return columnNodes.Select(delegate(IEnumerable<IDomNode> c)
			{
				if (c != null)
				{
					return base.<GetBoundaryBasedRowAlignment>g__GetRowAlignedColNodes|1(c);
				}
				return null;
			}).ToArray<IDomNode[]>();
		}

		// Token: 0x06008554 RID: 34132 RVA: 0x001C08A8 File Offset: 0x001BEAA8
		public static string[] SubstringField(ValueSubstring v)
		{
			return new string[] { v.Value };
		}

		// Token: 0x06008555 RID: 34133 RVA: 0x001C08BC File Offset: 0x001BEABC
		public static ValueSubstring Trim(ValueSubstring cs)
		{
			if (cs == null || cs.Value == null)
			{
				return null;
			}
			return ValueSubstring.Create(cs.Value.Trim(), null, null, null, null);
		}

		// Token: 0x06008556 RID: 34134 RVA: 0x001C08FC File Offset: 0x001BEAFC
		public static ValueSubstring SelectSubstring(ValueSubstring[] substringDisj, string[] featureNames, int[] targetValues)
		{
			return substringDisj.ArgMin((ValueSubstring v) => Semantics.Distance(targetValues, Semantics.ComputeSubstringFeatures(v, featureNames)));
		}

		// Token: 0x06008557 RID: 34135 RVA: 0x001C0930 File Offset: 0x001BEB30
		private static int Distance(int[] v1, int[] v2)
		{
			int num = 0;
			for (int i = 0; i < v1.Length; i++)
			{
				int num2 = ((v1[i] != v2[i]) ? 1 : 0);
				num += num2;
			}
			return num;
		}

		// Token: 0x06008558 RID: 34136 RVA: 0x001C0960 File Offset: 0x001BEB60
		private static int[] ComputeSubstringFeatures(ValueSubstring v, string[] featureNames)
		{
			int[] array = new int[featureNames.Length];
			for (int i = 0; i < featureNames.Length; i++)
			{
				string s = featureNames[i];
				if (s == "IsNull")
				{
					array[i] = ((v == null || v.Value == null) ? 1 : 0);
				}
				else if (s == "NumChars")
				{
					int[] array2 = array;
					int num = i;
					int? num2;
					if (v == null)
					{
						num2 = null;
					}
					else
					{
						string value = v.Value;
						num2 = ((value != null) ? new int?(value.Length) : null);
					}
					int? num3 = num2;
					array2[num] = num3.GetValueOrDefault();
				}
				else if (s == "NumNonWSChars")
				{
					int[] array3 = array;
					int num4 = i;
					int? num5;
					if (v == null)
					{
						num5 = null;
					}
					else
					{
						string value2 = v.Value;
						if (value2 == null)
						{
							num5 = null;
						}
						else
						{
							num5 = new int?(value2.Count((char c) => !char.IsWhiteSpace(c)));
						}
					}
					int? num3 = num5;
					array3[num4] = num3.GetValueOrDefault();
				}
				else
				{
					int[] array4 = array;
					int num6 = i;
					int? num7;
					if (v == null)
					{
						num7 = null;
					}
					else
					{
						string value3 = v.Value;
						num7 = ((value3 != null) ? new int?(value3.Count((char x) => x == s[0])) : null);
					}
					int? num3 = num7;
					array4[num6] = num3.GetValueOrDefault();
				}
			}
			return array;
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x001C0AC7 File Offset: 0x001BECC7
		public static NodeCollection AsCollection(IEnumerable<IDomNode> nodes)
		{
			return new NodeCollection(nodes);
		}

		// Token: 0x0600855A RID: 34138 RVA: 0x001C0AD0 File Offset: 0x001BECD0
		public static IEnumerable<WebRegion> ConvertToWebRegions(NodeCollection nodes)
		{
			return (from n in nodes.Set
				orderby n.Start
				select new WebRegion(n)).ToArray<WebRegion>();
		}

		// Token: 0x0600855B RID: 34139 RVA: 0x001C0B30 File Offset: 0x001BED30
		public static NodeCollection ChildrenOfCollection(NodeCollection nodes)
		{
			return new NodeCollection(Semantics.ChildrenOf(nodes.Set));
		}

		// Token: 0x0600855C RID: 34140 RVA: 0x001C0B44 File Offset: 0x001BED44
		public static NodeCollection DescendantsOf(NodeCollection nodes)
		{
			NodeCollection nodeCollection = new NodeCollection(Enumerable.Empty<IDomNode>());
			foreach (IDomNode domNode in nodes.Set)
			{
				if (!nodeCollection.Set.Contains(domNode))
				{
					nodeCollection.Set.AddRange(domNode.GetDescendants(false));
				}
			}
			return nodeCollection;
		}

		// Token: 0x0600855D RID: 34141 RVA: 0x001C0BBC File Offset: 0x001BEDBC
		public static NodeCollection RightSiblingOf(NodeCollection nodes)
		{
			new NodeCollection(Enumerable.Empty<IDomNode>());
			return new NodeCollection(from n in nodes.Set
				select n.NextSibling into n
				where n != null
				select n);
		}

		// Token: 0x0600855E RID: 34142 RVA: 0x001C0C28 File Offset: 0x001BEE28
		public static NodeCollection NodeNameFilter(string name, NodeCollection regions)
		{
			return new NodeCollection(regions.Set.Where((IDomNode r) => Semantics.NodeName(name, r)));
		}

		// Token: 0x0600855F RID: 34143 RVA: 0x001C0C60 File Offset: 0x001BEE60
		public static NodeCollection NthChildFilter(int idx1, NodeCollection regions)
		{
			return new NodeCollection(regions.Set.Where((IDomNode r) => Semantics.NthChild(idx1, r)));
		}

		// Token: 0x06008560 RID: 34144 RVA: 0x001C0C98 File Offset: 0x001BEE98
		public static NodeCollection NthLastChildFilter(int idx2, NodeCollection regions)
		{
			return new NodeCollection(regions.Set.Where((IDomNode r) => Semantics.NthLastChild(idx2, r)));
		}

		// Token: 0x06008561 RID: 34145 RVA: 0x001C0CD0 File Offset: 0x001BEED0
		public static NodeCollection ClassFilter(string name, NodeCollection regions)
		{
			return new NodeCollection(regions.Set.Where((IDomNode r) => Semantics.Class(name, r)));
		}

		// Token: 0x06008562 RID: 34146 RVA: 0x001C0D08 File Offset: 0x001BEF08
		public static NodeCollection ItemPropFilter(string propName, NodeCollection regions)
		{
			return new NodeCollection(regions.Set.Where(delegate(IDomNode r)
			{
				string attribute = r.GetAttribute("itemprop");
				return attribute != null && attribute.Equals(propName, StringComparison.Ordinal);
			}));
		}

		// Token: 0x06008563 RID: 34147 RVA: 0x001C0D40 File Offset: 0x001BEF40
		public static NodeCollection IDFilter(string name, NodeCollection regions)
		{
			return new NodeCollection(regions.Set.Where((IDomNode r) => Semantics.ID(name, r)));
		}

		// Token: 0x06008564 RID: 34148 RVA: 0x001C0D76 File Offset: 0x001BEF76
		public static bool ID(string id, IDomNode region)
		{
			return region.Id == id;
		}

		// Token: 0x06008565 RID: 34149 RVA: 0x001C0D84 File Offset: 0x001BEF84
		public static object GEN_NthChildFilter(object o1, object o2)
		{
			return Semantics.GenerateFilterValues<int>(o1, o2, new Func<IDomNode, int>(Semantics.<GEN_NthChildFilter>g__GetNodeAttributeValue|64_0), null);
		}

		// Token: 0x06008566 RID: 34150 RVA: 0x001C0D9A File Offset: 0x001BEF9A
		public static object GEN_NthLastChildFilter(object o1, object o2)
		{
			return Semantics.GenerateFilterValues<int>(o1, o2, new Func<IDomNode, int>(Semantics.<GEN_NthLastChildFilter>g__GetNodeAttributeValue|65_0), null);
		}

		// Token: 0x06008567 RID: 34151 RVA: 0x001C0DB0 File Offset: 0x001BEFB0
		public static object GEN_ClassFilter(object o1, object o2)
		{
			return Semantics.GenerateFilterValues<string>(o1, o2, null, new Func<IDomNode, IEnumerable<string>>(Semantics.<GEN_ClassFilter>g__GetNodeAttributeValues|66_0));
		}

		// Token: 0x06008568 RID: 34152 RVA: 0x001C0DC6 File Offset: 0x001BEFC6
		public static object GEN_IDFilter(object o1, object o2)
		{
			return Semantics.GenerateFilterValues<string>(o1, o2, new Func<IDomNode, string>(Semantics.<GEN_IDFilter>g__GetNodeAttributeValue|67_0), null);
		}

		// Token: 0x06008569 RID: 34153 RVA: 0x001C0DDC File Offset: 0x001BEFDC
		public static object GEN_NodeNameFilter(object o1, object o2)
		{
			return Semantics.GenerateFilterValues<string>(o1, o2, new Func<IDomNode, string>(Semantics.<GEN_NodeNameFilter>g__GetNodeAttributeValue|68_0), null);
		}

		// Token: 0x0600856A RID: 34154 RVA: 0x001C0DF2 File Offset: 0x001BEFF2
		public static object GEN_ItemPropFilter(object o1, object o2)
		{
			return Semantics.GenerateFilterValues<string>(o1, o2, new Func<IDomNode, string>(Semantics.<GEN_ItemPropFilter>g__GetNodeAttributeValue|69_0), null);
		}

		// Token: 0x0600856B RID: 34155 RVA: 0x001C0E08 File Offset: 0x001BF008
		public static object GenerateFilterValues<T>(object o1, object o2, Func<IDomNode, T> getNodeAttributeValue, Func<IDomNode, IEnumerable<T>> getNodeAttributeValues = null)
		{
			HashSet<T> hashSet = ((object[][])o1).SelectMany((object[] x) => x).Cast<T>().ConvertToHashSet<T>();
			object[][] array = (object[][])o2;
			Dictionary<T, object[]>[] array2 = new Dictionary<T, object[]>[array.Length];
			int num = 0;
			List<Record<object[], object[][]>> list = new List<Record<object[], object[][]>>();
			if (hashSet.Count == 0)
			{
				return list;
			}
			for (int i = 0; i < array.Length; i++)
			{
				object[] array3 = array[i];
				num = array3.Length;
				array2[i] = new Dictionary<T, object[]>();
				Dictionary<T, object[]> dictionary = array2[i];
				for (int j = 0; j < num; j++)
				{
					foreach (IDomNode domNode in ((NodeCollection)array3[j]).Set)
					{
						if (getNodeAttributeValue != null)
						{
							T t = getNodeAttributeValue(domNode);
							if (hashSet.Contains(t))
							{
								Semantics.UpdateAttributeNodeMap<T>(dictionary, num, t, domNode, j);
							}
						}
						else
						{
							foreach (T t2 in getNodeAttributeValues(domNode))
							{
								if (hashSet.Contains(t2))
								{
									Semantics.UpdateAttributeNodeMap<T>(dictionary, num, t2, domNode, j);
								}
							}
						}
					}
				}
			}
			for (int k = 0; k < array.Length; k++)
			{
				Dictionary<T, object[]> dictionary2 = array2[k];
				object[] array4 = array[k];
				foreach (KeyValuePair<T, object[]> keyValuePair in dictionary2)
				{
					T key = keyValuePair.Key;
					object[] value = keyValuePair.Value;
					object[][] array5 = new object[][]
					{
						Enumerable.Repeat<T>(key, num).Cast<object>().ToArray<object>(),
						array4
					};
					Record<object[], object[][]> record = new Record<object[], object[][]>(value, array5);
					list.Add(record);
				}
			}
			return list;
		}

		// Token: 0x0600856C RID: 34156 RVA: 0x001C1024 File Offset: 0x001BF224
		private static void UpdateAttributeNodeMap<T>(Dictionary<T, object[]> map, int exampleCount, T value, IDomNode n, int exampleIndex)
		{
			object[] array;
			if (!map.TryGetValue(value, out array))
			{
				object[] array2 = new NodeCollection[exampleCount];
				array = array2;
				map[value] = array;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new NodeCollection();
				}
			}
			(array[exampleIndex] as NodeCollection).Set.Add(n);
		}

		// Token: 0x0600856D RID: 34157 RVA: 0x001C1078 File Offset: 0x001BF278
		[CompilerGenerated]
		internal static int <GEN_NthChildFilter>g__GetNodeAttributeValue|64_0(IDomNode n)
		{
			return n.Index;
		}

		// Token: 0x0600856E RID: 34158 RVA: 0x001C1080 File Offset: 0x001BF280
		[CompilerGenerated]
		internal static int <GEN_NthLastChildFilter>g__GetNodeAttributeValue|65_0(IDomNode n)
		{
			return n.IndexFromLast;
		}

		// Token: 0x0600856F RID: 34159 RVA: 0x001C1088 File Offset: 0x001BF288
		[CompilerGenerated]
		internal static IEnumerable<string> <GEN_ClassFilter>g__GetNodeAttributeValues|66_0(IDomNode n)
		{
			return n.Classes;
		}

		// Token: 0x06008570 RID: 34160 RVA: 0x001C1090 File Offset: 0x001BF290
		[CompilerGenerated]
		internal static string <GEN_IDFilter>g__GetNodeAttributeValue|67_0(IDomNode n)
		{
			return n.Id;
		}

		// Token: 0x06008571 RID: 34161 RVA: 0x001C1098 File Offset: 0x001BF298
		[CompilerGenerated]
		internal static string <GEN_NodeNameFilter>g__GetNodeAttributeValue|68_0(IDomNode n)
		{
			return n.NodeName;
		}

		// Token: 0x06008572 RID: 34162 RVA: 0x001C10A0 File Offset: 0x001BF2A0
		[CompilerGenerated]
		internal static string <GEN_ItemPropFilter>g__GetNodeAttributeValue|69_0(IDomNode n)
		{
			return n.GetAttribute("itemprop");
		}

		// Token: 0x040036F2 RID: 14066
		private static readonly Regex DateRegex;

		// Token: 0x040036F3 RID: 14067
		public const double ApplyTextLength = 200.0;

		// Token: 0x040036F4 RID: 14068
		public const int MaxKeyDistance = 3;

		// Token: 0x0200117E RID: 4478
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040036F5 RID: 14069
			public static Func<char, bool> <0>__IsNumber;
		}
	}
}
