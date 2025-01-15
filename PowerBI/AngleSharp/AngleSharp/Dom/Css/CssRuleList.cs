using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000303 RID: 771
	internal sealed class CssRuleList : ICssRuleList, IEnumerable<ICssRule>, IEnumerable
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x0004E950 File Offset: 0x0004CB50
		internal CssRuleList(CssNode parent)
		{
			this._parent = parent;
		}

		// Token: 0x170005C7 RID: 1479
		public CssRule this[int index]
		{
			get
			{
				return this.Nodes.GetItemByIndex(index);
			}
		}

		// Token: 0x170005C8 RID: 1480
		ICssRule ICssRuleList.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0004E976 File Offset: 0x0004CB76
		public bool HasDeclarativeRules
		{
			get
			{
				return this.Nodes.Any((CssRule m) => CssRuleList.IsDeclarativeRule(m));
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0004E9A2 File Offset: 0x0004CBA2
		public IEnumerable<CssRule> Nodes
		{
			get
			{
				return this._parent.Children.OfType<CssRule>();
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0004E9B4 File Offset: 0x0004CBB4
		public int Length
		{
			get
			{
				return this.Nodes.Count<CssRule>();
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0004E9C4 File Offset: 0x0004CBC4
		internal void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			CssRule cssRule = this[index];
			if (cssRule.Type == CssRuleType.Namespace && this.HasDeclarativeRules)
			{
				throw new DomException(DomError.InvalidState);
			}
			this.Remove(cssRule);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0004EA0E File Offset: 0x0004CC0E
		internal void Remove(CssRule rule)
		{
			if (rule != null)
			{
				this._parent.RemoveChild(rule);
			}
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0004EA20 File Offset: 0x0004CC20
		internal void Insert(int index, CssRule rule)
		{
			if (rule == null)
			{
				throw new DomException(DomError.Syntax);
			}
			if (rule.Type == CssRuleType.Charset)
			{
				throw new DomException(DomError.Syntax);
			}
			if (index > this.Length || index < 0)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			if (rule.Type == CssRuleType.Namespace && this.HasDeclarativeRules)
			{
				throw new DomException(DomError.InvalidState);
			}
			if (index == this.Length)
			{
				this._parent.AppendChild(rule);
				return;
			}
			this._parent.InsertBefore(this[index], rule);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0004EAA0 File Offset: 0x0004CCA0
		internal void Add(CssRule rule)
		{
			if (rule != null)
			{
				this._parent.AppendChild(rule);
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0004EAB1 File Offset: 0x0004CCB1
		public IEnumerator<ICssRule> GetEnumerator()
		{
			return this.Nodes.GetEnumerator();
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004EABE File Offset: 0x0004CCBE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0004EAC8 File Offset: 0x0004CCC8
		private static bool IsDeclarativeRule(CssRule rule)
		{
			CssRuleType type = rule.Type;
			return type != CssRuleType.Import && type != CssRuleType.Charset && type != CssRuleType.Namespace;
		}

		// Token: 0x04000C96 RID: 3222
		private readonly CssNode _parent;
	}
}
