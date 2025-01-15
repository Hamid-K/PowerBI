using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x02000060 RID: 96
	internal class TreeNode
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x0001412F File Offset: 0x0001232F
		internal TreeNode()
		{
			this._text = new StringBuilder();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00014150 File Offset: 0x00012350
		internal TreeNode(string text, params TreeNode[] children)
		{
			if (string.IsNullOrEmpty(text))
			{
				this._text = new StringBuilder();
			}
			else
			{
				this._text = new StringBuilder(text);
			}
			if (children != null)
			{
				this._children.AddRange(children);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001419E File Offset: 0x0001239E
		internal TreeNode(string text, List<TreeNode> children)
			: this(text, Array.Empty<TreeNode>())
		{
			if (children != null)
			{
				this._children.AddRange(children);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000141BB File Offset: 0x000123BB
		internal StringBuilder Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x000141C3 File Offset: 0x000123C3
		internal IList<TreeNode> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000141CB File Offset: 0x000123CB
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x000141D3 File Offset: 0x000123D3
		internal int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x040006F5 RID: 1781
		private StringBuilder _text;

		// Token: 0x040006F6 RID: 1782
		private List<TreeNode> _children = new List<TreeNode>();

		// Token: 0x040006F7 RID: 1783
		private int _position;
	}
}
