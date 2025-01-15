using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x0200116D RID: 4461
	public class DomNode : IEquatable<DomNode>, IDomNode
	{
		// Token: 0x06008476 RID: 33910 RVA: 0x001BEAD1 File Offset: 0x001BCCD1
		public DomNode(IElement el, DomNode parent, HtmlDoc doc, int index, int docIndex)
		{
			this._element = el;
			this._parent = parent;
			this.Index = ((parent == null) ? 0 : (index + 1));
			this.Doc = doc;
			this.Start = docIndex;
			this.Initialize();
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06008477 RID: 33911 RVA: 0x001BEB0C File Offset: 0x001BCD0C
		internal HtmlDoc Doc { get; }

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06008478 RID: 33912 RVA: 0x001BEB14 File Offset: 0x001BCD14
		// (set) Token: 0x06008479 RID: 33913 RVA: 0x001BEB1C File Offset: 0x001BCD1C
		internal List<IDomNode> Children { get; set; }

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x0600847A RID: 33914 RVA: 0x001BEB25 File Offset: 0x001BCD25
		public string SpecificSelector
		{
			get
			{
				return this._specificSelector.Value;
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x0600847B RID: 33915 RVA: 0x001BEB32 File Offset: 0x001BCD32
		public string NodeNameClassSelector
		{
			get
			{
				return this._nodeNameClassSelector.Value;
			}
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x0600847C RID: 33916 RVA: 0x001BEB3F File Offset: 0x001BCD3F
		// (set) Token: 0x0600847D RID: 33917 RVA: 0x001BEB47 File Offset: 0x001BCD47
		public DomNode FirstChild { get; set; }

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x0600847E RID: 33918 RVA: 0x001BEB50 File Offset: 0x001BCD50
		// (set) Token: 0x0600847F RID: 33919 RVA: 0x001BEB58 File Offset: 0x001BCD58
		public DomNode NextSibling { get; set; }

		// Token: 0x06008480 RID: 33920 RVA: 0x001BEB61 File Offset: 0x001BCD61
		public string GetAttribute(string attrib)
		{
			return this._element.GetAttribute(attrib);
		}

		// Token: 0x06008481 RID: 33921 RVA: 0x001BEB70 File Offset: 0x001BCD70
		public string GetStyle(string style)
		{
			string text;
			try
			{
				if (this._element.Style == null || this._element.Style[style].Equals(string.Empty))
				{
					text = null;
				}
				else
				{
					text = this._element.Style[style];
				}
			}
			catch (OverflowException)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06008482 RID: 33922 RVA: 0x001BEBD8 File Offset: 0x001BCDD8
		public IEnumerable<string> GetStyles()
		{
			IEnumerable<string> enumerable2;
			try
			{
				IEnumerable<string> enumerable;
				if (this._element.Style == null)
				{
					enumerable = Enumerable.Empty<string>();
				}
				else
				{
					enumerable = this._element.Style.Select((ICssProperty x) => x.Name);
				}
				enumerable2 = enumerable;
			}
			catch (OverflowException)
			{
				enumerable2 = Enumerable.Empty<string>();
			}
			return enumerable2;
		}

		// Token: 0x06008483 RID: 33923 RVA: 0x001BEC48 File Offset: 0x001BCE48
		public List<DomNode> GetDescendantsByCss(string selector)
		{
			return this._element.QuerySelectorAll(selector).ToDomNodes(this.Document);
		}

		// Token: 0x06008484 RID: 33924 RVA: 0x001BEC61 File Offset: 0x001BCE61
		public IEnumerable<string> GetAttributes()
		{
			return this._element.Attributes.Select((IAttr a) => a.Name);
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06008485 RID: 33925 RVA: 0x001BEC92 File Offset: 0x001BCE92
		public int Start { get; }

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06008486 RID: 33926 RVA: 0x001BEC9A File Offset: 0x001BCE9A
		// (set) Token: 0x06008487 RID: 33927 RVA: 0x001BECA2 File Offset: 0x001BCEA2
		public int End { get; set; }

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06008488 RID: 33928 RVA: 0x001BECAB File Offset: 0x001BCEAB
		public int Index { get; }

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06008489 RID: 33929 RVA: 0x001BECB4 File Offset: 0x001BCEB4
		public int IndexFromLast
		{
			get
			{
				DomNode parent = this._parent;
				return (((parent != null) ? new int?(parent.Children.Count + 1) : null) - this.Index).GetValueOrDefault();
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x0600848A RID: 33930 RVA: 0x001BED19 File Offset: 0x001BCF19
		// (set) Token: 0x0600848B RID: 33931 RVA: 0x001BED21 File Offset: 0x001BCF21
		public string NodeName { get; private set; }

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x0600848C RID: 33932 RVA: 0x001BED2A File Offset: 0x001BCF2A
		public string[] Classes
		{
			get
			{
				return this._classes.Value;
			}
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x0600848D RID: 33933 RVA: 0x001BED37 File Offset: 0x001BCF37
		public string Id
		{
			get
			{
				return this._element.Id ?? string.Empty;
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x0600848E RID: 33934 RVA: 0x001BED4D File Offset: 0x001BCF4D
		public string InnerText
		{
			get
			{
				return this._innerText.Value;
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x0600848F RID: 33935 RVA: 0x001BED5A File Offset: 0x001BCF5A
		public string TrimmedInnerText
		{
			get
			{
				if (this._innerText.IsValueCreated)
				{
					return this._trimmedInnerText;
				}
				if (this._innerText.Value != null)
				{
					return this._trimmedInnerText;
				}
				return null;
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06008490 RID: 33936 RVA: 0x001BED85 File Offset: 0x001BCF85
		public string NormalizedInnerText
		{
			get
			{
				if (this._innerText.IsValueCreated)
				{
					return this._normalizedInnerText;
				}
				if (this._innerText.Value != null)
				{
					return this._normalizedInnerText;
				}
				return null;
			}
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06008491 RID: 33937 RVA: 0x001BEDB0 File Offset: 0x001BCFB0
		public IDomNode Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06008492 RID: 33938 RVA: 0x001BEDB8 File Offset: 0x001BCFB8
		public string Title
		{
			get
			{
				return this._element.GetAttribute("title");
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06008493 RID: 33939 RVA: 0x001BEDCA File Offset: 0x001BCFCA
		public HtmlDoc Document
		{
			get
			{
				return this.Doc;
			}
		}

		// Token: 0x06008494 RID: 33940 RVA: 0x001BEDD2 File Offset: 0x001BCFD2
		public IEnumerable<IDomNode> GetChildren()
		{
			return this.Children;
		}

		// Token: 0x06008495 RID: 33941 RVA: 0x001BEDDA File Offset: 0x001BCFDA
		public IEnumerable<IDomNode> GetDescendants(bool includeSelf = true)
		{
			return this.GetDescendantsIncludingSelf().Skip((!includeSelf) ? 1 : 0);
		}

		// Token: 0x06008496 RID: 33942 RVA: 0x001BEDEB File Offset: 0x001BCFEB
		private IEnumerable<IDomNode> GetDescendantsIncludingSelf()
		{
			Stack<DomNode> stack = new Stack<DomNode>();
			stack.Push(this);
			while (stack.Count > 0)
			{
				DomNode current;
				for (current = stack.Pop(); current != null; current = current.FirstChild)
				{
					yield return current;
					if (this != current && current.NextSibling != null)
					{
						stack.Push(current.NextSibling);
					}
				}
				current = null;
			}
			yield break;
		}

		// Token: 0x06008497 RID: 33943 RVA: 0x001BEDFB File Offset: 0x001BCFFB
		public IEnumerable<IDomNode> GetYoungerSiblings()
		{
			if (this.Parent == null)
			{
				return Enumerable.Empty<IDomNode>();
			}
			return from x in this.Parent.GetChildren()
				where x.Start > this.Start
				select x;
		}

		// Token: 0x06008498 RID: 33944 RVA: 0x001BEE27 File Offset: 0x001BD027
		public IEnumerable<IDomNode> GetOlderSiblings()
		{
			if (this.Parent == null)
			{
				return Enumerable.Empty<IDomNode>();
			}
			return from x in this.Parent.GetChildren()
				where x.Start < this.Start
				select x;
		}

		// Token: 0x06008499 RID: 33945 RVA: 0x001BEE53 File Offset: 0x001BD053
		public WebRegion ToWebRegion()
		{
			return this.Doc.GetWebRegion(this);
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x0600849A RID: 33946 RVA: 0x001BEE61 File Offset: 0x001BD061
		public int ChildrenCount
		{
			get
			{
				return this.Children.Count;
			}
		}

		// Token: 0x0600849B RID: 33947 RVA: 0x001BEE6E File Offset: 0x001BD06E
		public string GetOuterHtml()
		{
			return this._element.Html();
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x0600849C RID: 33948 RVA: 0x001BEE7B File Offset: 0x001BD07B
		public HashSet<string> LeafNodes
		{
			get
			{
				return this._leafNodes.Value;
			}
		}

		// Token: 0x0600849D RID: 33949 RVA: 0x001BEE88 File Offset: 0x001BD088
		public bool HasMinimalText()
		{
			if (this._hasMinimalText == null)
			{
				this._hasMinimalText = new bool?(this.GetElement().ChildNodes.Any(delegate(INode c)
				{
					if (c.NodeType == NodeType.Text)
					{
						string text = c.Text();
						return !string.IsNullOrEmpty((text != null) ? text.Trim() : null);
					}
					return false;
				}));
			}
			return this._hasMinimalText.Value;
		}

		// Token: 0x0600849E RID: 33950 RVA: 0x001BEEE7 File Offset: 0x001BD0E7
		public string GetVisibleTextContent()
		{
			return DomNode.HtmlTextValueCreator.CreateValue(this.GetElement());
		}

		// Token: 0x0600849F RID: 33951 RVA: 0x001BEEF4 File Offset: 0x001BD0F4
		public int GetNodeHeight()
		{
			if (this._nodeHeight == null)
			{
				if (this.ChildrenCount == 0)
				{
					return 0;
				}
				this._nodeHeight = new int?(1 + this.Children.Max((IDomNode c) => c.GetNodeHeight()));
			}
			return this._nodeHeight.Value;
		}

		// Token: 0x060084A0 RID: 33952 RVA: 0x001BEF5C File Offset: 0x001BD15C
		public bool Contains(IDomNode other)
		{
			DomNode domNode = other as DomNode;
			if (domNode == null)
			{
				throw new Exception("parameter is not a DomNode");
			}
			return this.Doc == domNode.Doc && !this.Equals(other) && this.Start <= other.Start && this.End >= other.End;
		}

		// Token: 0x060084A1 RID: 33953 RVA: 0x001BEFBC File Offset: 0x001BD1BC
		public bool IsBefore(DomNode other)
		{
			return this.Doc == other.Doc && !this.Equals(other) && (this.Start < other.Start || (this.Start == other.Start && this.End > other.End));
		}

		// Token: 0x060084A2 RID: 33954 RVA: 0x001BF012 File Offset: 0x001BD212
		public bool IsAncestor(IDomNode n)
		{
			return n != null && n.Contains(this);
		}

		// Token: 0x060084A3 RID: 33955 RVA: 0x001BF020 File Offset: 0x001BD220
		private IElement GetElement()
		{
			return this._element;
		}

		// Token: 0x060084A4 RID: 33956 RVA: 0x001BF028 File Offset: 0x001BD228
		public override string ToString()
		{
			string text = "{0}: \"{1}\"";
			object[] array = new object[2];
			array[0] = this._specificSelector.Value;
			int num = 1;
			string value = this._innerText.Value;
			int? num2 = new int?(30);
			array[num] = value.Slice(null, num2, 1);
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x060084A5 RID: 33957 RVA: 0x001BF080 File Offset: 0x001BD280
		private void Initialize()
		{
			this.NodeName = this._element.NodeName;
			this._innerText = new Lazy<string>(delegate
			{
				string innerText = DomNode.GetInnerText(this._element);
				this._trimmedInnerText = innerText.Trim();
				this._normalizedInnerText = HtmlDoc.NormalizeText(innerText);
				return innerText;
			});
			this._specificSelector = new Lazy<string>(new Func<string>(this.GetSpecificSelector));
			this._nodeNameClassSelector = new Lazy<string>(new Func<string>(this.GetNodeNameClassSelector));
			this._classes = new Lazy<string[]>(() => this._element.ClassList.ToArray<string>());
			this._leafNodes = new Lazy<HashSet<string>>(new Func<HashSet<string>>(this.GetLeafNodes));
		}

		// Token: 0x060084A6 RID: 33958 RVA: 0x001BF114 File Offset: 0x001BD314
		public static string GetInnerText(INode n)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DomNode.AppendInnertText(n, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060084A7 RID: 33959 RVA: 0x001BF134 File Offset: 0x001BD334
		private static void AppendInnertText(INode n, StringBuilder sb)
		{
			if (DomNode.ExcludedNodeNames.Contains(n.NodeName))
			{
				return;
			}
			IText text = n as IText;
			if (text != null)
			{
				sb.Append(text.Data);
			}
			foreach (INode node in n.ChildNodes)
			{
				DomNode.AppendInnertText(node, sb);
			}
		}

		// Token: 0x060084A8 RID: 33960 RVA: 0x001BF1AC File Offset: 0x001BD3AC
		private string GetSpecificSelector()
		{
			if (!string.IsNullOrEmpty(this.Id) && !this.Doc.AllNodes.Any((IDomNode n) => n.Id == this.Id && n != this))
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}[id='{1}']", new object[]
				{
					this.NodeName,
					DomNode.EscapeSpecialCharactersCss(this.Id)
				}));
			}
			string text = this.NodeName;
			if (this.Classes.Any<string>())
			{
				string text2 = text;
				string text3 = ".";
				string text4 = ".";
				IEnumerable<string> classes = this.Classes;
				Func<string, string> func;
				if ((func = DomNode.<>O.<0>__EscapeSpecialCharactersCss) == null)
				{
					func = (DomNode.<>O.<0>__EscapeSpecialCharactersCss = new Func<string, string>(DomNode.EscapeSpecialCharactersCss));
				}
				text = text2 + text3 + string.Join(text4, classes.Select(func));
			}
			if (this.Parent == null)
			{
				return text;
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} > {1}:nth-child({2})", new object[]
			{
				this._parent.SpecificSelector,
				text,
				this.Index
			}));
		}

		// Token: 0x060084A9 RID: 33961 RVA: 0x001BF2A0 File Offset: 0x001BD4A0
		public static string EscapeSpecialCharactersCss(string className)
		{
			return string.Concat(className.Select((char c) => (DomNode.CssSpecials.Contains(c) ? "\\" : string.Empty) + c.ToString())).Replace("--", "\\-\\-");
		}

		// Token: 0x060084AA RID: 33962 RVA: 0x001BF2DC File Offset: 0x001BD4DC
		private string GetNodeNameClassSelector()
		{
			string text = this.NodeName;
			if (this.Classes.Any<string>())
			{
				text = text + "." + string.Join(".", this.Classes);
			}
			if (this.Parent == null)
			{
				return text;
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} > {1}", new object[]
			{
				this._parent.NodeNameClassSelector,
				text
			}));
		}

		// Token: 0x060084AB RID: 33963 RVA: 0x001BF34C File Offset: 0x001BD54C
		private HashSet<string> GetLeafNodes()
		{
			List<IDomNode> children = this.Children;
			if (children.Count == 0)
			{
				return new HashSet<string>();
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IDomNode domNode in children)
			{
				hashSet.UnionWith(domNode.LeafNodes);
				if (domNode.LeafNodes.Count == 0)
				{
					hashSet.Add(domNode.NodeName);
				}
			}
			return hashSet;
		}

		// Token: 0x060084AC RID: 33964 RVA: 0x001BF3D8 File Offset: 0x001BD5D8
		public bool Equals(DomNode other)
		{
			return this.Doc == other.Doc && other.Start == this.Start && other.End == this.End;
		}

		// Token: 0x060084AD RID: 33965 RVA: 0x001BF406 File Offset: 0x001BD606
		public override int GetHashCode()
		{
			return (17 * 31 + this.Start) * 31 + this.End;
		}

		// Token: 0x060084AE RID: 33966 RVA: 0x001BF420 File Offset: 0x001BD620
		public override bool Equals(object obj)
		{
			DomNode domNode = obj as DomNode;
			return domNode != null && domNode.Equals(this);
		}

		// Token: 0x040036A6 RID: 13990
		private static readonly HashSet<string> ExcludedNodeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "SCRIPT", "STYLE" };

		// Token: 0x040036A7 RID: 13991
		private static readonly HashSet<char> CssSpecials = new HashSet<char>("!#$%&'()*+,./:;<=>?@[]^`{|}\"\\");

		// Token: 0x040036A8 RID: 13992
		private Lazy<string[]> _classes;

		// Token: 0x040036A9 RID: 13993
		private readonly IElement _element;

		// Token: 0x040036AA RID: 13994
		private Lazy<string> _innerText;

		// Token: 0x040036AB RID: 13995
		private Lazy<HashSet<string>> _leafNodes;

		// Token: 0x040036AC RID: 13996
		private readonly DomNode _parent;

		// Token: 0x040036AD RID: 13997
		private Lazy<string> _specificSelector;

		// Token: 0x040036AE RID: 13998
		private Lazy<string> _nodeNameClassSelector;

		// Token: 0x040036AF RID: 13999
		private string _trimmedInnerText;

		// Token: 0x040036B0 RID: 14000
		private string _normalizedInnerText;

		// Token: 0x040036B1 RID: 14001
		private bool? _hasMinimalText;

		// Token: 0x040036B2 RID: 14002
		private int? _nodeHeight;

		// Token: 0x0200116E RID: 4462
		private sealed class HtmlTextValueCreator
		{
			// Token: 0x060084B5 RID: 33973 RVA: 0x001BF4FC File Offset: 0x001BD6FC
			private void AddNewlineIfBlock(string nodeName)
			{
				if (nodeName != null)
				{
					switch (nodeName.Length)
					{
					case 1:
						if (!(nodeName == "P"))
						{
							return;
						}
						break;
					case 2:
					{
						char c = nodeName[1];
						switch (c)
						{
						case '1':
							if (!(nodeName == "H1"))
							{
								return;
							}
							break;
						case '2':
							if (!(nodeName == "H2"))
							{
								return;
							}
							break;
						case '3':
							if (!(nodeName == "H3"))
							{
								return;
							}
							break;
						case '4':
							if (!(nodeName == "H4"))
							{
								return;
							}
							break;
						case '5':
							if (!(nodeName == "H5"))
							{
								return;
							}
							break;
						case '6':
							if (!(nodeName == "H6"))
							{
								return;
							}
							break;
						default:
							if (c != 'R')
							{
								return;
							}
							if (!(nodeName == "BR"))
							{
								return;
							}
							break;
						}
						break;
					}
					case 3:
						if (!(nodeName == "DIV"))
						{
							return;
						}
						break;
					default:
						return;
					}
					if (this._canAppendNewLine)
					{
						this.builder.AppendLine();
						this._canAppendNewLine = false;
					}
				}
			}

			// Token: 0x060084B6 RID: 33974 RVA: 0x001BF5F4 File Offset: 0x001BD7F4
			private void CollectTextFromNode(INode node)
			{
				if (!this.visitedNodes.Add(node))
				{
					return;
				}
				NodeType nodeType = node.NodeType;
				if (nodeType != NodeType.Element)
				{
					if (nodeType != NodeType.Text)
					{
						return;
					}
					DomNode.AppendInnertText(node, this.builder);
					this._canAppendNewLine = true;
					return;
				}
				else
				{
					IElement element = node as IElement;
					if (element == null)
					{
						return;
					}
					if (string.Equals(element.GetAttribute("hidden"), "false", StringComparison.OrdinalIgnoreCase))
					{
						return;
					}
					if (string.Equals(element.NodeName, "SELECT", StringComparison.OrdinalIgnoreCase))
					{
						return;
					}
					this.AddNewlineIfBlock(node.NodeName);
					if (node.ChildNodes.Any<INode>())
					{
						this.CollectTextFromNodes(node.ChildNodes);
					}
					this.AddNewlineIfBlock(node.NodeName);
					return;
				}
			}

			// Token: 0x060084B7 RID: 33975 RVA: 0x001BF6A0 File Offset: 0x001BD8A0
			private void CollectTextFromNodes(IEnumerable<INode> nodes)
			{
				foreach (INode node in nodes)
				{
					this.CollectTextFromNode(node);
				}
			}

			// Token: 0x060084B8 RID: 33976 RVA: 0x001BF6E8 File Offset: 0x001BD8E8
			public static string CreateValue(INode node)
			{
				DomNode.HtmlTextValueCreator htmlTextValueCreator = new DomNode.HtmlTextValueCreator();
				htmlTextValueCreator.CollectTextFromNode(node);
				return htmlTextValueCreator.builder.ToString().Trim();
			}

			// Token: 0x060084B9 RID: 33977 RVA: 0x001BF705 File Offset: 0x001BD905
			public static string CreateValue(List<INode> nodes)
			{
				DomNode.HtmlTextValueCreator htmlTextValueCreator = new DomNode.HtmlTextValueCreator();
				htmlTextValueCreator.CollectTextFromNodes(nodes);
				return htmlTextValueCreator.builder.ToString().Trim();
			}

			// Token: 0x040036BB RID: 14011
			private bool _canAppendNewLine;

			// Token: 0x040036BC RID: 14012
			private StringBuilder builder = new StringBuilder();

			// Token: 0x040036BD RID: 14013
			private HashSet<INode> visitedNodes = new HashSet<INode>();
		}

		// Token: 0x0200116F RID: 4463
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040036BE RID: 14014
			public static Func<string, string> <0>__EscapeSpecialCharactersCss;
		}
	}
}
