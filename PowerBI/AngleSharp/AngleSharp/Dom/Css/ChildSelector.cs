using System;
using System.IO;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000308 RID: 776
	internal abstract class ChildSelector : CssNode, ISelector, ICssNode, IStyleFormattable
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x0004EC6A File Offset: 0x0004CE6A
		public ChildSelector(string name)
		{
			this._name = name;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0004EC79 File Offset: 0x0004CE79
		public Priority Specifity
		{
			get
			{
				return Priority.OneClass;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0004810B File Offset: 0x0004630B
		public string Text
		{
			get
			{
				return this.ToCss();
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0004EC80 File Offset: 0x0004CE80
		internal ChildSelector With(int step, int offset, ISelector kind)
		{
			this._step = step;
			this._offset = offset;
			this._kind = kind;
			return this;
		}

		// Token: 0x06001681 RID: 5761
		public abstract bool Match(IElement element);

		// Token: 0x06001682 RID: 5762 RVA: 0x0004EC98 File Offset: 0x0004CE98
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = this._step.ToString();
			string text2 = string.Empty;
			if (this._offset > 0)
			{
				text2 = "+" + this._offset.ToString();
			}
			else if (this._offset < 0)
			{
				text2 = this._offset.ToString();
			}
			writer.Write(":{0}({1}n{2})", this._name, text, text2);
		}

		// Token: 0x04000C98 RID: 3224
		private readonly string _name;

		// Token: 0x04000C99 RID: 3225
		protected int _step;

		// Token: 0x04000C9A RID: 3226
		protected int _offset;

		// Token: 0x04000C9B RID: 3227
		protected ISelector _kind;
	}
}
