using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020001FE RID: 510
	internal abstract class CssNode : ICssNode, IStyleFormattable
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x00047E71 File Offset: 0x00046071
		public CssNode()
		{
			this._children = new List<ICssNode>();
			this._source = null;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00047E8B File Offset: 0x0004608B
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x00047E93 File Offset: 0x00046093
		public TextView SourceCode
		{
			get
			{
				return this._source;
			}
			internal set
			{
				this._source = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00047E9C File Offset: 0x0004609C
		public IEnumerable<ICssNode> Children
		{
			get
			{
				return this._children.AsEnumerable<ICssNode>();
			}
		}

		// Token: 0x06001152 RID: 4434
		public abstract void ToCss(TextWriter writer, IStyleFormatter formatter);

		// Token: 0x06001153 RID: 4435 RVA: 0x00047EA9 File Offset: 0x000460A9
		public void AppendChild(ICssNode child)
		{
			this.Setup(child);
			this._children.Add(child);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00047EC0 File Offset: 0x000460C0
		public void ReplaceChild(ICssNode oldChild, ICssNode newChild)
		{
			for (int i = 0; i < this._children.Count; i++)
			{
				if (oldChild == this._children[i])
				{
					this.Teardown(oldChild);
					this.Setup(newChild);
					this._children[i] = newChild;
					return;
				}
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00047F10 File Offset: 0x00046110
		public void InsertBefore(ICssNode referenceChild, ICssNode child)
		{
			if (referenceChild != null)
			{
				int num = this._children.IndexOf(referenceChild);
				this.InsertChild(num, child);
				return;
			}
			this.AppendChild(child);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00047F3D File Offset: 0x0004613D
		public void InsertChild(int index, ICssNode child)
		{
			this.Setup(child);
			this._children.Insert(index, child);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00047F53 File Offset: 0x00046153
		public void RemoveChild(ICssNode child)
		{
			this.Teardown(child);
			this._children.Remove(child);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00047F6C File Offset: 0x0004616C
		public void Clear()
		{
			for (int i = this._children.Count - 1; i >= 0; i--)
			{
				ICssNode cssNode = this._children[i];
				this.RemoveChild(cssNode);
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00047FA8 File Offset: 0x000461A8
		protected void ReplaceAll(ICssNode node)
		{
			this.Clear();
			this._source = node.SourceCode;
			foreach (ICssNode cssNode in node.Children)
			{
				this.AppendChild(cssNode);
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00048008 File Offset: 0x00046208
		private void Setup(ICssNode child)
		{
			CssRule cssRule = child as CssRule;
			if (cssRule != null)
			{
				cssRule.Owner = this as ICssStyleSheet;
				cssRule.Parent = this as ICssRule;
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00048038 File Offset: 0x00046238
		private void Teardown(ICssNode child)
		{
			CssRule cssRule = child as CssRule;
			if (cssRule != null)
			{
				cssRule.Parent = null;
				cssRule.Owner = null;
			}
		}

		// Token: 0x04000A90 RID: 2704
		private readonly List<ICssNode> _children;

		// Token: 0x04000A91 RID: 2705
		private TextView _source;
	}
}
