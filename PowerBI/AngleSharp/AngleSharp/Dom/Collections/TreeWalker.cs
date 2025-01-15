using System;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000407 RID: 1031
	internal sealed class TreeWalker : ITreeWalker
	{
		// Token: 0x060020EA RID: 8426 RVA: 0x00058408 File Offset: 0x00056608
		public TreeWalker(INode root, FilterSettings settings, NodeFilter filter)
		{
			this._root = root;
			this._settings = settings;
			NodeFilter nodeFilter = filter;
			if (filter == null && (nodeFilter = TreeWalker.<>c.<>9__4_0) == null)
			{
				nodeFilter = (TreeWalker.<>c.<>9__4_0 = (INode m) => FilterResult.Accept);
			}
			this._filter = nodeFilter;
			this._current = this._root;
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x0005845F File Offset: 0x0005665F
		public INode Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x00058467 File Offset: 0x00056667
		public FilterSettings Settings
		{
			get
			{
				return this._settings;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x0005846F File Offset: 0x0005666F
		public NodeFilter Filter
		{
			get
			{
				return this._filter;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x00058477 File Offset: 0x00056677
		// (set) Token: 0x060020EF RID: 8431 RVA: 0x0005847F File Offset: 0x0005667F
		public INode Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = value;
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00058488 File Offset: 0x00056688
		public INode ToNext()
		{
			INode node = this._current;
			FilterResult filterResult = FilterResult.Accept;
			while (node != null)
			{
				while (filterResult != FilterResult.Reject)
				{
					if (!node.HasChildNodes)
					{
						break;
					}
					node = node.FirstChild;
					filterResult = this.Check(node);
					if (filterResult == FilterResult.Accept)
					{
						this._current = node;
						return node;
					}
				}
				while (node != this._root)
				{
					INode nextSibling = node.NextSibling;
					if (nextSibling != null)
					{
						node = nextSibling;
						break;
					}
					node = node.Parent;
				}
				if (node == this._root)
				{
					break;
				}
				filterResult = this.Check(node);
				if (filterResult == FilterResult.Accept)
				{
					this._current = node;
					return node;
				}
			}
			return null;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0005850C File Offset: 0x0005670C
		public INode ToPrevious()
		{
			INode node = this._current;
			while (node != null && node != this._root)
			{
				INode previousSibling = node.PreviousSibling;
				while (previousSibling != null)
				{
					node = previousSibling;
					FilterResult filterResult = this.Check(node);
					while (filterResult != FilterResult.Reject && node.HasChildNodes)
					{
						node = node.LastChild;
						filterResult = this.Check(node);
						if (filterResult == FilterResult.Accept)
						{
							this._current = node;
							return node;
						}
					}
				}
				if (node == this._root || node.Parent == null)
				{
					break;
				}
				if (this.Check(node) == FilterResult.Accept)
				{
					this._current = node;
					return node;
				}
			}
			return null;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00058594 File Offset: 0x00056794
		public INode ToParent()
		{
			INode node = this._current;
			while (node != null && node != this._root)
			{
				node = node.Parent;
				if (node != null && this.Check(node) == FilterResult.Accept)
				{
					this._current = node;
					return node;
				}
			}
			return null;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000585D4 File Offset: 0x000567D4
		public INode ToFirst()
		{
			INode current = this._current;
			INode node = ((current != null) ? current.FirstChild : null);
			while (node != null)
			{
				FilterResult filterResult = this.Check(node);
				if (filterResult == FilterResult.Accept)
				{
					this._current = node;
					return node;
				}
				if (filterResult == FilterResult.Skip)
				{
					INode firstChild = node.FirstChild;
					if (firstChild != null)
					{
						node = firstChild;
						continue;
					}
				}
				while (node != null)
				{
					INode nextSibling = node.NextSibling;
					if (nextSibling != null)
					{
						node = nextSibling;
						break;
					}
					INode parent = node.Parent;
					if (parent == null || parent == this._root || parent == this._current)
					{
						node = null;
						break;
					}
					node = parent;
				}
			}
			return null;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0005865C File Offset: 0x0005685C
		public INode ToLast()
		{
			INode current = this._current;
			INode node = ((current != null) ? current.LastChild : null);
			while (node != null)
			{
				FilterResult filterResult = this.Check(node);
				if (filterResult == FilterResult.Accept)
				{
					this._current = node;
					return node;
				}
				if (filterResult == FilterResult.Skip)
				{
					INode lastChild = node.LastChild;
					if (lastChild != null)
					{
						node = lastChild;
						continue;
					}
				}
				while (node != null)
				{
					INode previousSibling = node.PreviousSibling;
					if (previousSibling != null)
					{
						node = previousSibling;
						break;
					}
					INode parent = node.Parent;
					if (parent == null || parent == this._root || parent == this._current)
					{
						node = null;
						break;
					}
					node = parent;
				}
			}
			return null;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000586E4 File Offset: 0x000568E4
		public INode ToPreviousSibling()
		{
			INode node = this._current;
			if (node != this._root)
			{
				while (node != null)
				{
					INode node2 = node.PreviousSibling;
					while (node2 != null)
					{
						node = node2;
						FilterResult filterResult = this.Check(node);
						if (filterResult == FilterResult.Accept)
						{
							this._current = node;
							return node;
						}
						node2 = node.LastChild;
						if (filterResult == FilterResult.Reject || node2 == null)
						{
							node2 = node.PreviousSibling;
						}
					}
					node = node.Parent;
					if (node == null || node == this._root || this.Check(node) == FilterResult.Accept)
					{
						break;
					}
				}
			}
			return null;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0005875C File Offset: 0x0005695C
		public INode ToNextSibling()
		{
			INode node = this._current;
			if (node != this._root)
			{
				while (node != null)
				{
					INode node2 = node.NextSibling;
					while (node2 != null)
					{
						node = node2;
						FilterResult filterResult = this.Check(node);
						if (filterResult == FilterResult.Accept)
						{
							this._current = node;
							return node;
						}
						node2 = node.FirstChild;
						if (filterResult == FilterResult.Reject || node2 == null)
						{
							node2 = node.NextSibling;
						}
					}
					node = node.Parent;
					if (node == null || node == this._root || this.Check(node) == FilterResult.Accept)
					{
						break;
					}
				}
			}
			return null;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000587D4 File Offset: 0x000569D4
		private FilterResult Check(INode node)
		{
			if (!this._settings.Accepts(node))
			{
				return FilterResult.Skip;
			}
			return this._filter(node);
		}

		// Token: 0x04000D27 RID: 3367
		private readonly INode _root;

		// Token: 0x04000D28 RID: 3368
		private readonly FilterSettings _settings;

		// Token: 0x04000D29 RID: 3369
		private readonly NodeFilter _filter;

		// Token: 0x04000D2A RID: 3370
		private INode _current;
	}
}
