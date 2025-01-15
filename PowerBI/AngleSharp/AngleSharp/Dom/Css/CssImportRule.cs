using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002FC RID: 764
	internal sealed class CssImportRule : CssRule, ICssImportRule, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x0600161C RID: 5660 RVA: 0x0004E302 File Offset: 0x0004C502
		internal CssImportRule(CssParser parser)
			: base(CssRuleType.Import, parser)
		{
			base.AppendChild(new MediaList(parser));
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x0004E318 File Offset: 0x0004C518
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x0004E320 File Offset: 0x0004C520
		public string Href
		{
			get
			{
				return this._href;
			}
			set
			{
				this._href = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0004E329 File Offset: 0x0004C529
		public MediaList Media
		{
			get
			{
				return base.Children.OfType<MediaList>().FirstOrDefault<MediaList>();
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0004E33B File Offset: 0x0004C53B
		IMediaList ICssImportRule.Media
		{
			get
			{
				return this.Media;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0004E343 File Offset: 0x0004C543
		public ICssStyleSheet Sheet
		{
			get
			{
				return this._styleSheet;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0004E34C File Offset: 0x0004C54C
		internal async Task LoadStylesheetFromAsync(Document document)
		{
			if (document != null)
			{
				IResourceLoader loader = document.Loader;
				Url url = new Url(Url.Create(this.Owner.Href ?? document.BaseUri), this._href);
				if (!this.IsRecursion(url) && loader != null)
				{
					ResourceRequest resourceRequest = this.Owner.OwnerNode.CreateRequestFor(url);
					IResponse response2 = await loader.DownloadAsync(resourceRequest).Task.ConfigureAwait(false);
					using (IResponse response = response2)
					{
						CssStyleSheet cssStyleSheet = new CssStyleSheet(this, response.Address.Href);
						TextSource textSource = new TextSource(response.Content, null);
						CssImportRule cssImportRule = this;
						CssStyleSheet styleSheet = cssImportRule._styleSheet;
						cssImportRule._styleSheet = await this.Parser.ParseStylesheetAsync(cssStyleSheet, textSource).ConfigureAwait(false);
						cssImportRule = null;
					}
					IResponse response = null;
				}
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0004E39C File Offset: 0x0004C59C
		protected override void ReplaceWith(ICssRule rule)
		{
			CssImportRule cssImportRule = rule as CssImportRule;
			this._href = cssImportRule._href;
			this._styleSheet = null;
			base.ReplaceWith(rule);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0004E3CC File Offset: 0x0004C5CC
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string mediaText = this.Media.MediaText;
			string text = (string.IsNullOrEmpty(mediaText) ? string.Empty : " ");
			string text2 = this._href.CssUrl() + text + mediaText;
			writer.Write(formatter.Rule("@import", text2));
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0004E420 File Offset: 0x0004C620
		private bool IsRecursion(Url url)
		{
			string href = url.Href;
			ICssStyleSheet cssStyleSheet = base.Owner;
			while (cssStyleSheet != null && !cssStyleSheet.Href.Is(href))
			{
				cssStyleSheet = cssStyleSheet.Parent;
			}
			return cssStyleSheet != null;
		}

		// Token: 0x04000C8C RID: 3212
		private string _href;

		// Token: 0x04000C8D RID: 3213
		private CssStyleSheet _styleSheet;
	}
}
