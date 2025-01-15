using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200038B RID: 907
	internal sealed class HtmlScriptElement : HtmlElement, IHtmlScriptElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x06001C57 RID: 7255 RVA: 0x000542D5 File Offset: 0x000524D5
		public HtmlScriptElement(Document owner, string prefix = null, bool parserInserted = false, bool started = false)
			: base(owner, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
		{
			this._forceAsync = false;
			this._started = started;
			this._parserInserted = parserInserted;
			this._request = ScriptRequestProcessor.Create(this);
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00054307 File Offset: 0x00052507
		public IDownload CurrentDownload
		{
			get
			{
				ScriptRequestProcessor request = this._request;
				if (request == null)
				{
					return null;
				}
				return request.Download;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00051A0B File Offset: 0x0004FC0B
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x00051A18 File Offset: 0x0004FC18
		public string Source
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Src);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Src, value, false);
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Type);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0004FC8F File Offset: 0x0004DE8F
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x0004FC9C File Offset: 0x0004DE9C
		public string CharacterSet
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Charset);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Charset, value, false);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0004FCC7 File Offset: 0x0004DEC7
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x0004FCCF File Offset: 0x0004DECF
		public string Text
		{
			get
			{
				return this.TextContent;
			}
			set
			{
				this.TextContent = value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x000528DE File Offset: 0x00050ADE
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x000528EB File Offset: 0x00050AEB
		public string CrossOrigin
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.CrossOrigin);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.CrossOrigin, value, false);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0005431A File Offset: 0x0005251A
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x00054327 File Offset: 0x00052527
		public bool IsDeferred
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Defer);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Defer, value);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00054335 File Offset: 0x00052535
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00054342 File Offset: 0x00052542
		public bool IsAsync
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Async);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Async, value);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00053289 File Offset: 0x00051489
		// (set) Token: 0x06001C68 RID: 7272 RVA: 0x00053296 File Offset: 0x00051496
		public string Integrity
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Integrity);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Integrity, value, false);
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00054350 File Offset: 0x00052550
		protected override void OnParentChanged()
		{
			base.OnParentChanged();
			if (!this._parserInserted && this.Prepare(base.Owner))
			{
				this.RunAsync(CancellationToken.None);
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0005437A File Offset: 0x0005257A
		internal Task RunAsync(CancellationToken cancel)
		{
			ScriptRequestProcessor request = this._request;
			if (request == null)
			{
				return null;
			}
			return request.RunAsync(cancel);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00054390 File Offset: 0x00052590
		internal bool Prepare(Document document)
		{
			IConfiguration options = document.Options;
			string text = this.GetOwnAttribute(AttributeNames.Event);
			string text2 = this.GetOwnAttribute(AttributeNames.For);
			string source = this.Source;
			bool parserInserted = this._parserInserted;
			if (this._started)
			{
				return false;
			}
			if (parserInserted)
			{
				this._forceAsync = !this.IsAsync;
			}
			if (string.IsNullOrEmpty(source) && string.IsNullOrEmpty(this.Text))
			{
				return false;
			}
			if (this._request.Engine == null)
			{
				return false;
			}
			if (parserInserted)
			{
				this._forceAsync = false;
			}
			this._started = true;
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				text = text.Trim();
				text2 = text2.Trim();
				if (text.EndsWith("()"))
				{
					text = text.Substring(0, text.Length - 2);
				}
				bool flag = text2.Equals(AttributeNames.Window, StringComparison.OrdinalIgnoreCase);
				bool flag2 = text.Equals("onload", StringComparison.OrdinalIgnoreCase);
				if (!flag || !flag2)
				{
					return false;
				}
			}
			if (source == null)
			{
				this._request.Process(this.Text);
				return true;
			}
			if (source.Length != 0)
			{
				return this.InvokeLoadingScript(document, this.HyperReference(source));
			}
			document.QueueTask(new Action(this.FireErrorEvent));
			return false;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000544BC File Offset: 0x000526BC
		public override INode Clone(bool deep = true)
		{
			HtmlScriptElement htmlScriptElement = new HtmlScriptElement(base.Owner, base.Prefix, this._parserInserted, this._started)
			{
				_forceAsync = this._forceAsync
			};
			base.CloneElement(htmlScriptElement, deep);
			return htmlScriptElement;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000544FC File Offset: 0x000526FC
		private bool InvokeLoadingScript(Document document, Url url)
		{
			bool flag = true;
			if (this._parserInserted && (this.IsDeferred || this.IsAsync))
			{
				document.AddScript(this);
				flag = false;
			}
			this.Process(this._request, url);
			return flag;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0005453A File Offset: 0x0005273A
		private void FireErrorEvent()
		{
			this.FireSimpleEvent(EventNames.Error, false, false);
		}

		// Token: 0x04000CF5 RID: 3317
		private readonly bool _parserInserted;

		// Token: 0x04000CF6 RID: 3318
		private readonly ScriptRequestProcessor _request;

		// Token: 0x04000CF7 RID: 3319
		private bool _started;

		// Token: 0x04000CF8 RID: 3320
		private bool _forceAsync;
	}
}
