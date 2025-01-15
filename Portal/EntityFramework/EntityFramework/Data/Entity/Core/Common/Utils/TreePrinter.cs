using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000601 RID: 1537
	internal abstract class TreePrinter
	{
		// Token: 0x06004B29 RID: 19241 RVA: 0x00109E80 File Offset: 0x00108080
		internal virtual string Print(TreeNode node)
		{
			this.PreProcess(node);
			StringBuilder stringBuilder = new StringBuilder();
			this.PrintNode(stringBuilder, node);
			return stringBuilder.ToString();
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x00109EA8 File Offset: 0x001080A8
		internal virtual void PreProcess(TreeNode node)
		{
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00109EAA File Offset: 0x001080AA
		internal virtual void AfterAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00109EAC File Offset: 0x001080AC
		internal virtual void BeforeAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00109EAE File Offset: 0x001080AE
		internal virtual void PrintNode(StringBuilder text, TreeNode node)
		{
			this.IndentLine(text);
			this.BeforeAppend(node, text);
			text.Append(node.Text);
			this.AfterAppend(node, text);
			this.PrintChildren(text, node);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00109EDC File Offset: 0x001080DC
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

		// Token: 0x06004B2F RID: 19247 RVA: 0x00109F6C File Offset: 0x0010816C
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

		// Token: 0x04001A4D RID: 6733
		private readonly List<TreeNode> _scopes = new List<TreeNode>();

		// Token: 0x04001A4E RID: 6734
		private bool _showLines = true;

		// Token: 0x04001A4F RID: 6735
		private char _horizontals = '_';

		// Token: 0x04001A50 RID: 6736
		private char _verticals = '|';
	}
}
