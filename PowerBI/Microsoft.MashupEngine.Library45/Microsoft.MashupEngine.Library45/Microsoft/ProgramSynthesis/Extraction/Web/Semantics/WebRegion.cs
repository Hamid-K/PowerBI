using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x0200118D RID: 4493
	[DataContract]
	[DebuggerDisplay("WebRegion(Start={Start}, End={End})")]
	public class WebRegion : IRegion<WebRegion>, IComparable<WebRegion>, IEquatable<WebRegion>
	{
		// Token: 0x060085A2 RID: 34210 RVA: 0x001C1331 File Offset: 0x001BF531
		public WebRegion()
		{
			this.Initialize();
		}

		// Token: 0x060085A3 RID: 34211 RVA: 0x001C133F File Offset: 0x001BF53F
		public WebRegion(string selector)
			: this()
		{
			this.Selector = selector;
			this.Value = "";
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x001C1359 File Offset: 0x001BF559
		public WebRegion(IDomNode node)
			: this()
		{
			this._doc = node.Document;
			this.BeginNode = node;
			this.EndNode = this.BeginNode;
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x001C1380 File Offset: 0x001BF580
		public WebRegion(IDomNode beginNode, IDomNode endNode)
			: this()
		{
			this._doc = beginNode.Document;
			this.BeginNode = beginNode;
			this.EndNode = endNode;
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x001C13A2 File Offset: 0x001BF5A2
		public WebRegion(HtmlDoc document)
			: this()
		{
			this._doc = document;
			this.BeginNode = this._doc.RootNode;
			this.EndNode = this.BeginNode;
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x060085A7 RID: 34215 RVA: 0x001C13CE File Offset: 0x001BF5CE
		private IEnumerable<IDomNode> Siblings
		{
			get
			{
				yield return this.BeginNode;
				if (this.IsPair)
				{
					foreach (IDomNode domNode in this.BeginNode.GetYoungerSiblings())
					{
						if (domNode.Start <= this.EndNode.Start)
						{
							yield return domNode;
						}
					}
					IEnumerator<IDomNode> enumerator = null;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x060085A8 RID: 34216 RVA: 0x001C13E0 File Offset: 0x001BF5E0
		public string OuterHtml
		{
			get
			{
				if (this.BeginNode == null)
				{
					return "";
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (IDomNode domNode in this.Siblings)
				{
					stringBuilder.Append(domNode.GetOuterHtml());
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x060085A9 RID: 34217 RVA: 0x001C1450 File Offset: 0x001BF650
		// (set) Token: 0x060085AA RID: 34218 RVA: 0x001C146C File Offset: 0x001BF66C
		[DataMember]
		public string Selector
		{
			get
			{
				return this._selector ?? ((DomNode)this.BeginNode).SpecificSelector;
			}
			set
			{
				this._selector = value;
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x060085AB RID: 34219 RVA: 0x001C1475 File Offset: 0x001BF675
		// (set) Token: 0x060085AC RID: 34220 RVA: 0x001C1497 File Offset: 0x001BF697
		[DataMember]
		public string EndSelector
		{
			get
			{
				string text;
				if ((text = this._endSelector) == null)
				{
					DomNode domNode = (DomNode)this.EndNode;
					if (domNode == null)
					{
						return null;
					}
					text = domNode.SpecificSelector;
				}
				return text;
			}
			set
			{
				this._endSelector = value;
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x060085AD RID: 34221 RVA: 0x001C14A0 File Offset: 0x001BF6A0
		public virtual int Start
		{
			get
			{
				return this.BeginNode.Start;
			}
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x060085AE RID: 34222 RVA: 0x001C14AD File Offset: 0x001BF6AD
		public virtual int End
		{
			get
			{
				return this.EndNode.End;
			}
		}

		// Token: 0x060085AF RID: 34223 RVA: 0x001C14BC File Offset: 0x001BF6BC
		public int CompareTo(WebRegion other)
		{
			if (other == null)
			{
				return 1;
			}
			if (this.Start < other.Start)
			{
				return -1;
			}
			if (this.Start != other.Start)
			{
				return 1;
			}
			if (this.End < other.End)
			{
				return -1;
			}
			if (this.End == other.End)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x060085B0 RID: 34224 RVA: 0x001C150F File Offset: 0x001BF70F
		// (set) Token: 0x060085B1 RID: 34225 RVA: 0x001C1526 File Offset: 0x001BF726
		[DataMember]
		public string Value
		{
			get
			{
				return this._serializedValue ?? this._value.Value;
			}
			set
			{
				this._serializedValue = value;
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x060085B2 RID: 34226 RVA: 0x001C152F File Offset: 0x001BF72F
		public WebRegion Parent
		{
			get
			{
				return this._parent.Value;
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x060085B3 RID: 34227 RVA: 0x001C153C File Offset: 0x001BF73C
		// (set) Token: 0x060085B4 RID: 34228 RVA: 0x001C1544 File Offset: 0x001BF744
		[IgnoreDataMember]
		public IDomNode BeginNode { get; private set; }

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x060085B5 RID: 34229 RVA: 0x001C1550 File Offset: 0x001BF750
		// (set) Token: 0x060085B6 RID: 34230 RVA: 0x001C1576 File Offset: 0x001BF776
		[IgnoreDataMember]
		public IDomNode EndNode
		{
			get
			{
				IDomNode domNode;
				if ((domNode = this._endNode) == null)
				{
					domNode = (this._endNode = this.BeginNode);
				}
				return domNode;
			}
			set
			{
				this._endNode = value;
			}
		}

		// Token: 0x060085B7 RID: 34231 RVA: 0x001C1580 File Offset: 0x001BF780
		public string Text()
		{
			if (this.BeginNode == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (IDomNode domNode in this.Siblings)
			{
				if (!flag)
				{
					stringBuilder.Append(127);
				}
				flag = false;
				stringBuilder.Append(domNode.InnerText);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060085B8 RID: 34232 RVA: 0x001C1600 File Offset: 0x001BF800
		public string NonWhitespaceText()
		{
			if (this.BeginNode == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IDomNode domNode in this.Siblings)
			{
				stringBuilder.Append(domNode.NormalizedInnerText);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x001C1670 File Offset: 0x001BF870
		public override string ToString()
		{
			string text = "WebRegion({0} ... {1} = \"{2}\")";
			object[] array = new object[3];
			array[0] = this.Selector;
			array[1] = this.EndSelector;
			int num = 2;
			object obj;
			if (this.Value.Length > 30)
			{
				string value = this.Value;
				int? num2 = new int?(30);
				obj = value.Slice(null, num2, 1) + "...";
			}
			else
			{
				obj = this.Value;
			}
			array[num] = obj;
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x001C16E8 File Offset: 0x001BF8E8
		public override bool Equals(object obj)
		{
			WebRegion webRegion = obj as WebRegion;
			return webRegion != null && this.Equals(webRegion);
		}

		// Token: 0x060085BB RID: 34235 RVA: 0x001C1708 File Offset: 0x001BF908
		public override int GetHashCode()
		{
			return this.BeginNode.GetHashCode() * 31 + this.EndNode.GetHashCode();
		}

		// Token: 0x060085BC RID: 34236 RVA: 0x001C1724 File Offset: 0x001BF924
		public bool Equals(WebRegion other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.BeginNode == null)
			{
				return this.Selector == other.Selector;
			}
			return object.Equals(this.BeginNode, other.BeginNode) && object.Equals(this.EndNode, other.EndNode);
		}

		// Token: 0x060085BD RID: 34237 RVA: 0x001C1776 File Offset: 0x001BF976
		public bool Contains(WebRegion other)
		{
			return other != null && this._doc == other._doc && this.Start <= other.Start && this.End >= other.End;
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x001C17AE File Offset: 0x001BF9AE
		public WebRegion GetRegion(string selector)
		{
			WebRegion region = this._doc.GetRegion(selector);
			region.Selector = selector;
			return region;
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x001C17C4 File Offset: 0x001BF9C4
		public WebRegion GetRegion(string beginSelector, string endSelector)
		{
			IDomNode domNode = this._doc.GetDomNode(beginSelector);
			DomNode domNode2 = this._doc.GetDomNode(endSelector);
			return new WebRegion(domNode, domNode2)
			{
				Selector = beginSelector,
				EndSelector = endSelector
			};
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x001C17FE File Offset: 0x001BF9FE
		public bool IntersectNonEmpty(WebRegion other)
		{
			return this.Contains(other) || other.Contains(this) || Math.Max(this.Start, other.Start) <= Math.Min(this.End, other.End);
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x001C183B File Offset: 0x001BFA3B
		public bool IsBefore(WebRegion other)
		{
			return ((DomNode)this.BeginNode).IsBefore(other.BeginNode as DomNode);
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x00004FAE File Offset: 0x000031AE
		public WebRegion ClipBefore(WebRegion other)
		{
			return this;
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x001C1858 File Offset: 0x001BFA58
		public IEnumerable<IDomNode> GetAllChildrenAndSelf()
		{
			return this.Siblings.SelectMany((IDomNode sibling) => sibling.GetDescendants(true)).ToArray<IDomNode>();
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x001C1889 File Offset: 0x001BFA89
		public string GetSpecificSelector()
		{
			return ((DomNode)this.BeginNode).SpecificSelector;
		}

		// Token: 0x060085C5 RID: 34245 RVA: 0x001C189C File Offset: 0x001BFA9C
		public bool IsSameRegion(WebRegion example, Func<string, string> fix = null)
		{
			if (fix != null && !fix(this.Value).Equals(fix(example.Value)))
			{
				return false;
			}
			string selector = this.Selector;
			string selector2 = example.Selector;
			return selector.Equals(selector2) || selector.StartsWith(selector2, StringComparison.Ordinal) || selector2.StartsWith(selector, StringComparison.Ordinal);
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x001C18FC File Offset: 0x001BFAFC
		public WebRegion GetSubregionByText(string text)
		{
			if (!this.Value.Contains(text))
			{
				return null;
			}
			IDomNode domNode = this.GetAllChildrenAndSelf().FirstOrDefault((IDomNode x) => x.InnerText.Equals(text));
			if (domNode == null)
			{
				return null;
			}
			return domNode.ToWebRegion();
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x001C1950 File Offset: 0x001BFB50
		private void Initialize()
		{
			this._parent = new Lazy<WebRegion>(delegate
			{
				IDomNode beginNode = this.BeginNode;
				if (beginNode == null)
				{
					return null;
				}
				IDomNode parent = beginNode.Parent;
				if (parent == null)
				{
					return null;
				}
				return parent.ToWebRegion();
			});
			this._value = new Lazy<string>(new Func<string>(this.Text));
			this._trimmedContent = new Lazy<string>(() => this._value.Value.Trim());
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x060085C8 RID: 34248 RVA: 0x001C19A2 File Offset: 0x001BFBA2
		public bool IsPair
		{
			get
			{
				return this.BeginNode != this.EndNode;
			}
		}

		// Token: 0x0400371E RID: 14110
		private HtmlDoc _doc;

		// Token: 0x0400371F RID: 14111
		private string _selector;

		// Token: 0x04003720 RID: 14112
		private string _endSelector;

		// Token: 0x04003721 RID: 14113
		private string _serializedValue;

		// Token: 0x04003722 RID: 14114
		private Lazy<WebRegion> _parent;

		// Token: 0x04003723 RID: 14115
		private Lazy<string> _trimmedContent;

		// Token: 0x04003724 RID: 14116
		private Lazy<string> _value;

		// Token: 0x04003726 RID: 14118
		[IgnoreDataMember]
		private IDomNode _endNode;
	}
}
