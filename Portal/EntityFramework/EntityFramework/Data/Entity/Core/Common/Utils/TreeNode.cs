using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000600 RID: 1536
	internal class TreeNode
	{
		// Token: 0x06004B22 RID: 19234 RVA: 0x00109DD0 File Offset: 0x00107FD0
		internal TreeNode()
		{
			this._text = new StringBuilder();
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x00109DF0 File Offset: 0x00107FF0
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

		// Token: 0x06004B24 RID: 19236 RVA: 0x00109E3E File Offset: 0x0010803E
		internal TreeNode(string text, List<TreeNode> children)
			: this(text, new TreeNode[0])
		{
			if (children != null)
			{
				this._children.AddRange(children);
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x00109E5C File Offset: 0x0010805C
		internal StringBuilder Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x00109E64 File Offset: 0x00108064
		internal IList<TreeNode> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x00109E6C File Offset: 0x0010806C
		// (set) Token: 0x06004B28 RID: 19240 RVA: 0x00109E74 File Offset: 0x00108074
		internal int Position { get; set; }

		// Token: 0x04001A4A RID: 6730
		private readonly StringBuilder _text;

		// Token: 0x04001A4B RID: 6731
		private readonly List<TreeNode> _children = new List<TreeNode>();
	}
}
