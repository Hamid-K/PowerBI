using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000F2 RID: 242
	[LayoutRenderer("cached")]
	[AmbientProperty("Cached")]
	[AmbientProperty("ClearCache")]
	[ThreadAgnostic]
	public sealed class CachedLayoutRendererWrapper : WrapperLayoutRendererBase, IStringValueRenderer
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x000229A3 File Offset: 0x00020BA3
		public CachedLayoutRendererWrapper()
		{
			this.Cached = true;
			this.ClearCache = CachedLayoutRendererWrapper.ClearCacheOption.OnInit | CachedLayoutRendererWrapper.ClearCacheOption.OnClose;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000229B9 File Offset: 0x00020BB9
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x000229C1 File Offset: 0x00020BC1
		[DefaultValue(true)]
		public bool Cached { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x000229CA File Offset: 0x00020BCA
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x000229D2 File Offset: 0x00020BD2
		public CachedLayoutRendererWrapper.ClearCacheOption ClearCache { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x000229DB File Offset: 0x00020BDB
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x000229E3 File Offset: 0x00020BE3
		public Layout CacheKey { get; set; }

		// Token: 0x06000DBB RID: 3515 RVA: 0x000229EC File Offset: 0x00020BEC
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			if ((this.ClearCache & CachedLayoutRendererWrapper.ClearCacheOption.OnInit) == CachedLayoutRendererWrapper.ClearCacheOption.OnInit)
			{
				this._cachedValue = null;
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00022A06 File Offset: 0x00020C06
		protected override void CloseLayoutRenderer()
		{
			base.CloseLayoutRenderer();
			if ((this.ClearCache & CachedLayoutRendererWrapper.ClearCacheOption.OnClose) == CachedLayoutRendererWrapper.ClearCacheOption.OnClose)
			{
				this._cachedValue = null;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00022A20 File Offset: 0x00020C20
		protected override string Transform(string text)
		{
			return text;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00022A24 File Offset: 0x00020C24
		protected override string RenderInner(LogEventInfo logEvent)
		{
			if (this.Cached)
			{
				Layout cacheKey = this.CacheKey;
				string text = ((cacheKey != null) ? cacheKey.Render(logEvent) : null);
				if (this._cachedValue == null || this._renderedCacheKey != text)
				{
					this._cachedValue = base.RenderInner(logEvent);
					this._renderedCacheKey = text;
				}
				return this._cachedValue;
			}
			return base.RenderInner(logEvent);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00022A85 File Offset: 0x00020C85
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			if (!this.Cached)
			{
				return null;
			}
			return this.RenderInner(logEvent);
		}

		// Token: 0x040003B0 RID: 944
		private string _cachedValue;

		// Token: 0x040003B1 RID: 945
		private string _renderedCacheKey;

		// Token: 0x02000260 RID: 608
		[Flags]
		public enum ClearCacheOption
		{
			// Token: 0x04000692 RID: 1682
			None = 0,
			// Token: 0x04000693 RID: 1683
			OnInit = 1,
			// Token: 0x04000694 RID: 1684
			OnClose = 2
		}
	}
}
