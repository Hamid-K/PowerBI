using System;
using System.Collections.Generic;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Svg;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000179 RID: 377
	public class DefaultAttributeObserver : IAttributeObserver
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x000465F1 File Offset: 0x000447F1
		public DefaultAttributeObserver()
		{
			this._actions = new List<Action<IElement, string, string>>();
			this.RegisterStandardObservers();
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0004660C File Offset: 0x0004480C
		protected virtual void RegisterStandardObservers()
		{
			this.RegisterObserver<Element>(AttributeNames.Class, delegate(Element element, string value)
			{
				element.UpdateClassList(value);
			});
			this.RegisterObserver<HtmlElement>(AttributeNames.DropZone, delegate(HtmlElement element, string value)
			{
				element.UpdateDropZone(value);
			});
			this.RegisterObserver<HtmlElement>(AttributeNames.Style, delegate(HtmlElement element, string value)
			{
				element.UpdateStyle(value);
			});
			this.RegisterObserver<HtmlBaseElement>(AttributeNames.Href, delegate(HtmlBaseElement element, string value)
			{
				element.UpdateUrl(value);
			});
			this.RegisterObserver<HtmlEmbedElement>(AttributeNames.Src, delegate(HtmlEmbedElement element, string value)
			{
				element.UpdateSource(value);
			});
			this.RegisterObserver<HtmlLinkElement>(AttributeNames.Sizes, delegate(HtmlLinkElement element, string value)
			{
				element.UpdateSizes(value);
			});
			this.RegisterObserver<HtmlLinkElement>(AttributeNames.Media, delegate(HtmlLinkElement element, string value)
			{
				element.UpdateMedia(value);
			});
			this.RegisterObserver<HtmlLinkElement>(AttributeNames.Disabled, delegate(HtmlLinkElement element, string value)
			{
				element.UpdateDisabled(value);
			});
			this.RegisterObserver<HtmlLinkElement>(AttributeNames.Href, delegate(HtmlLinkElement element, string value)
			{
				element.UpdateSource(value);
			});
			this.RegisterObserver<HtmlLinkElement>(AttributeNames.Rel, delegate(HtmlLinkElement element, string value)
			{
				element.UpdateRelation(value);
			});
			this.RegisterObserver<HtmlUrlBaseElement>(AttributeNames.Rel, delegate(HtmlUrlBaseElement element, string value)
			{
				element.UpdateRel(value);
			});
			this.RegisterObserver<HtmlUrlBaseElement>(AttributeNames.Ping, delegate(HtmlUrlBaseElement element, string value)
			{
				element.UpdatePing(value);
			});
			this.RegisterObserver<HtmlTableCellElement>(AttributeNames.Headers, delegate(HtmlTableCellElement element, string value)
			{
				element.UpdateHeaders(value);
			});
			this.RegisterObserver<HtmlStyleElement>(AttributeNames.Media, delegate(HtmlStyleElement element, string value)
			{
				element.UpdateMedia(value);
			});
			this.RegisterObserver<HtmlSelectElement>(AttributeNames.Value, delegate(HtmlSelectElement element, string value)
			{
				element.UpdateValue(value);
			});
			this.RegisterObserver<HtmlOutputElement>(AttributeNames.For, delegate(HtmlOutputElement element, string value)
			{
				element.UpdateFor(value);
			});
			this.RegisterObserver<HtmlObjectElement>(AttributeNames.Data, delegate(HtmlObjectElement element, string value)
			{
				element.UpdateSource(value);
			});
			this.RegisterObserver<HtmlAudioElement>(AttributeNames.Src, delegate(HtmlAudioElement element, string value)
			{
				element.UpdateSource(value);
			});
			this.RegisterObserver<HtmlVideoElement>(AttributeNames.Src, delegate(HtmlVideoElement element, string value)
			{
				element.UpdateSource(value);
			});
			this.RegisterObserver<HtmlImageElement>(AttributeNames.Src, delegate(HtmlImageElement element, string value)
			{
				element.UpdateSource();
			});
			this.RegisterObserver<HtmlImageElement>(AttributeNames.SrcSet, delegate(HtmlImageElement element, string value)
			{
				element.UpdateSource();
			});
			this.RegisterObserver<HtmlImageElement>(AttributeNames.Sizes, delegate(HtmlImageElement element, string value)
			{
				element.UpdateSource();
			});
			this.RegisterObserver<HtmlImageElement>(AttributeNames.CrossOrigin, delegate(HtmlImageElement element, string value)
			{
				element.UpdateSource();
			});
			this.RegisterObserver<HtmlIFrameElement>(AttributeNames.Sandbox, delegate(HtmlIFrameElement element, string value)
			{
				element.UpdateSandbox(value);
			});
			this.RegisterObserver<HtmlIFrameElement>(AttributeNames.SrcDoc, delegate(HtmlIFrameElement element, string value)
			{
				element.UpdateSource();
			});
			this.RegisterObserver<HtmlFrameElementBase>(AttributeNames.Src, delegate(HtmlFrameElementBase element, string value)
			{
				element.UpdateSource();
			});
			this.RegisterObserver<SvgElement>(AttributeNames.Style, delegate(SvgElement element, string value)
			{
				element.UpdateStyle(value);
			});
			this.RegisterObserver<HtmlInputElement>(AttributeNames.Type, delegate(HtmlInputElement element, string value)
			{
				element.UpdateType(value);
			});
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00046AB4 File Offset: 0x00044CB4
		public void RegisterObserver<TElement>(string expectedName, Action<TElement, string> callback) where TElement : IElement
		{
			this._actions.Add(delegate(IElement element, string actualName, string value)
			{
				if (element is TElement && actualName.Is(expectedName))
				{
					callback((TElement)((object)element), value);
				}
			});
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00046AEC File Offset: 0x00044CEC
		void IAttributeObserver.NotifyChange(IElement host, string name, string value)
		{
			foreach (Action<IElement, string, string> action in this._actions)
			{
				action(host, name, value);
			}
		}

		// Token: 0x04000A04 RID: 2564
		private readonly List<Action<IElement, string, string>> _actions;
	}
}
