using System;
using System.IO;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200020A RID: 522
	internal sealed class NotCondition : CssNode, IConditionFunction, ICssNode, IStyleFormattable
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0004AA63 File Offset: 0x00048C63
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x0004AA74 File Offset: 0x00048C74
		public IConditionFunction Content
		{
			get
			{
				return this._content ?? new EmptyCondition();
			}
			set
			{
				if (this._content != null)
				{
					base.RemoveChild(this._content);
				}
				this._content = value;
				if (value != null)
				{
					base.AppendChild(this._content);
				}
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004AAA0 File Offset: 0x00048CA0
		public bool Check()
		{
			return !this.Content.Check();
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004AAB0 File Offset: 0x00048CB0
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write("not ");
			this.Content.ToCss(writer, formatter);
		}

		// Token: 0x04000AB0 RID: 2736
		private IConditionFunction _content;
	}
}
