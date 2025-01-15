using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x02000061 RID: 97
	internal abstract class TreePrinter
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x000141DC File Offset: 0x000123DC
		internal virtual string Print(TreeNode node)
		{
			this.PreProcess(node);
			StringBuilder stringBuilder = new StringBuilder();
			this.PrintNode(stringBuilder, node);
			return stringBuilder.ToString();
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00014204 File Offset: 0x00012404
		internal TreePrinter()
		{
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001422E File Offset: 0x0001242E
		internal virtual void PreProcess(TreeNode node)
		{
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00014230 File Offset: 0x00012430
		internal virtual void AfterAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00014232 File Offset: 0x00012432
		internal virtual void BeforeAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00014234 File Offset: 0x00012434
		internal virtual void PrintNode(StringBuilder text, TreeNode node)
		{
			this.IndentLine(text);
			this.BeforeAppend(node, text);
			text.Append(node.Text.ToString());
			this.AfterAppend(node, text);
			this.PrintChildren(text, node);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00014268 File Offset: 0x00012468
		internal virtual void PrintChildren(StringBuilder text, TreeNode node)
		{
			this._scopes.Add(node);
			node.Position = 0;
			foreach (TreeNode treeNode in node.Children)
			{
				text.AppendLine();
				int position = node.Position;
				node.Position = position + 1;
				this.PrintNode(text, treeNode);
			}
			this._scopes.RemoveAt(this._scopes.Count - 1);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000142F8 File Offset: 0x000124F8
		private void IndentLine(StringBuilder text)
		{
			int num = 0;
			for (int i = 0; i < this._scopes.Count; i++)
			{
				TreeNode treeNode = this._scopes[i];
				if (!this._showLines || (treeNode.Position == treeNode.Children.Count && i != this._scopes.Count - 1))
				{
					text.Append(' ');
				}
				else
				{
					text.Append(this._verticals);
				}
				num++;
				if (this._scopes.Count == num && this._showLines)
				{
					text.Append(this._horizontals);
				}
				else
				{
					text.Append(' ');
				}
			}
		}

		// Token: 0x040006F8 RID: 1784
		private List<TreeNode> _scopes = new List<TreeNode>();

		// Token: 0x040006F9 RID: 1785
		private bool _showLines = true;

		// Token: 0x040006FA RID: 1786
		private char _horizontals = '_';

		// Token: 0x040006FB RID: 1787
		private char _verticals = '|';
	}
}
