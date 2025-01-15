using System;
using System.IO;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000209 RID: 521
	internal sealed class GroupCondition : CssNode, IConditionFunction, ICssNode, IStyleFormattable
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0004A9F4 File Offset: 0x00048BF4
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0004AA05 File Offset: 0x00048C05
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

		// Token: 0x06001393 RID: 5011 RVA: 0x0004AA31 File Offset: 0x00048C31
		public bool Check()
		{
			return this.Content.Check();
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004AA3E File Offset: 0x00048C3E
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write("(");
			this.Content.ToCss(writer, formatter);
			writer.Write(")");
		}

		// Token: 0x04000AAF RID: 2735
		private IConditionFunction _content;
	}
}
