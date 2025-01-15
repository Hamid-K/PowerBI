using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x020003FE RID: 1022
	internal sealed class NodeIterator : INodeIterator
	{
		// Token: 0x0600207B RID: 8315 RVA: 0x0005683C File Offset: 0x00054A3C
		public NodeIterator(INode root, FilterSettings settings, NodeFilter filter)
		{
			this._root = root;
			this._settings = settings;
			NodeFilter nodeFilter = filter;
			if (filter == null && (nodeFilter = NodeIterator.<>c.<>9__6_0) == null)
			{
				nodeFilter = (NodeIterator.<>c.<>9__6_0 = (INode m) => FilterResult.Accept);
			}
			this._filter = nodeFilter;
			this._beforeNode = true;
			this._iterator = this._root.GetElements(settings);
			this._reference = this._iterator.First<INode>();
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x000568B1 File Offset: 0x00054AB1
		public INode Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x000568B9 File Offset: 0x00054AB9
		public FilterSettings Settings
		{
			get
			{
				return this._settings;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x000568C1 File Offset: 0x00054AC1
		public NodeFilter Filter
		{
			get
			{
				return this._filter;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x000568C9 File Offset: 0x00054AC9
		public INode Reference
		{
			get
			{
				return this._reference;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x000568D1 File Offset: 0x00054AD1
		public bool IsBeforeReference
		{
			get
			{
				return this._beforeNode;
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000568DC File Offset: 0x00054ADC
		public INode Next()
		{
			INode node = this._reference;
			bool flag = this._beforeNode;
			for (;;)
			{
				if (!flag)
				{
					node = this._iterator.SkipWhile((INode m) => m != node).Skip(1).FirstOrDefault<INode>();
				}
				if (node == null)
				{
					break;
				}
				flag = false;
				if (this._filter(node) == FilterResult.Accept)
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this._beforeNode = false;
			this._reference = node;
			return node;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00056968 File Offset: 0x00054B68
		public INode Previous()
		{
			INode node = this._reference;
			bool flag = this._beforeNode;
			for (;;)
			{
				if (flag)
				{
					node = this._iterator.TakeWhile((INode m) => m != node).LastOrDefault<INode>();
				}
				if (node == null)
				{
					break;
				}
				flag = true;
				if (this._filter(node) == FilterResult.Accept)
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this._beforeNode = true;
			this._reference = node;
			return node;
		}

		// Token: 0x04000D14 RID: 3348
		private readonly INode _root;

		// Token: 0x04000D15 RID: 3349
		private readonly FilterSettings _settings;

		// Token: 0x04000D16 RID: 3350
		private readonly NodeFilter _filter;

		// Token: 0x04000D17 RID: 3351
		private readonly IEnumerable<INode> _iterator;

		// Token: 0x04000D18 RID: 3352
		private INode _reference;

		// Token: 0x04000D19 RID: 3353
		private bool _beforeNode;
	}
}
