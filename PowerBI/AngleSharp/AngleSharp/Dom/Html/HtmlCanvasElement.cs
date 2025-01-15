using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Services;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200034A RID: 842
	internal sealed class HtmlCanvasElement : HtmlElement, IHtmlCanvasElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001978 RID: 6520 RVA: 0x000501AD File Offset: 0x0004E3AD
		public HtmlCanvasElement(Document owner, string prefix = null)
			: base(owner, TagNames.Canvas, prefix, NodeFlags.None)
		{
			this._mode = HtmlCanvasElement.ContextMode.None;
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x000501C4 File Offset: 0x0004E3C4
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x000501DB File Offset: 0x0004E3DB
		public int Width
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Width).ToInteger(300);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Width, value.ToString(), false);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x000501F0 File Offset: 0x0004E3F0
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x00050207 File Offset: 0x0004E407
		public int Height
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Height).ToInteger(150);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Height, value.ToString(), false);
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0005021C File Offset: 0x0004E41C
		public IRenderingContext GetContext(string contextId)
		{
			if (this._current == null || contextId.Isi(this._current.ContextId))
			{
				foreach (IRenderingService renderingService in base.Owner.Options.GetServices<IRenderingService>())
				{
					if (renderingService.IsSupportingContext(contextId))
					{
						IRenderingContext renderingContext = renderingService.CreateContext(this, contextId);
						if (renderingContext != null)
						{
							this._mode = HtmlCanvasElement.GetModeFrom(contextId);
							this._current = renderingContext;
						}
						return renderingContext;
					}
				}
				return null;
			}
			return this._current;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000502C0 File Offset: 0x0004E4C0
		public bool IsSupportingContext(string contextId)
		{
			using (IEnumerator<IRenderingService> enumerator = base.Owner.Options.GetServices<IRenderingService>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsSupportingContext(contextId))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00050320 File Offset: 0x0004E520
		public void SetContext(IRenderingContext context)
		{
			if (this._mode != HtmlCanvasElement.ContextMode.None && this._mode != HtmlCanvasElement.ContextMode.Indirect)
			{
				throw new DomException(DomError.InvalidState);
			}
			if (context.IsFixed)
			{
				throw new DomException(DomError.InvalidState);
			}
			if (context.Host != this)
			{
				throw new DomException(DomError.InUse);
			}
			this._current = context;
			this._mode = HtmlCanvasElement.ContextMode.Indirect;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00050375 File Offset: 0x0004E575
		public string ToDataUrl(string type = null)
		{
			return Convert.ToBase64String(this.GetImageData(type));
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00050384 File Offset: 0x0004E584
		public void ToBlob(Action<Stream> callback, string type = null)
		{
			MemoryStream memoryStream = new MemoryStream(this.GetImageData(type));
			callback(memoryStream);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000503A5 File Offset: 0x0004E5A5
		private byte[] GetImageData(string type)
		{
			IRenderingContext current = this._current;
			return ((current != null) ? current.ToImage(type ?? MimeTypeNames.Plain) : null) ?? new byte[0];
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000503CD File Offset: 0x0004E5CD
		private static HtmlCanvasElement.ContextMode GetModeFrom(string contextId)
		{
			if (contextId.Isi("2d"))
			{
				return HtmlCanvasElement.ContextMode.Direct2d;
			}
			if (contextId.Isi("webgl"))
			{
				return HtmlCanvasElement.ContextMode.DirectWebGl;
			}
			return HtmlCanvasElement.ContextMode.None;
		}

		// Token: 0x04000CC3 RID: 3267
		private HtmlCanvasElement.ContextMode _mode;

		// Token: 0x04000CC4 RID: 3268
		private IRenderingContext _current;

		// Token: 0x02000514 RID: 1300
		private enum ContextMode : byte
		{
			// Token: 0x04001254 RID: 4692
			None,
			// Token: 0x04001255 RID: 4693
			Direct2d,
			// Token: 0x04001256 RID: 4694
			DirectWebGl,
			// Token: 0x04001257 RID: 4695
			Indirect,
			// Token: 0x04001258 RID: 4696
			Proxied
		}
	}
}
