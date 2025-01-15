using System;
using System.Globalization;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CC RID: 204
	[NLogConfigurationItem]
	public abstract class LayoutRenderer : ISupportsInitialize, IRenderable, IDisposable
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00020099 File Offset: 0x0001E299
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x000200A1 File Offset: 0x0001E2A1
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x06000C80 RID: 3200 RVA: 0x000200AC File Offset: 0x0001E2AC
		public override string ToString()
		{
			LayoutRendererAttribute customAttribute = base.GetType().GetCustomAttribute<LayoutRendererAttribute>();
			if (customAttribute != null)
			{
				return "Layout Renderer: ${" + customAttribute.Name + "}";
			}
			return base.GetType().Name;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000200E9 File Offset: 0x0001E2E9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000200F8 File Offset: 0x0001E2F8
		public string Render(LogEventInfo logEvent)
		{
			int num = this._maxRenderedLength;
			if (num > 16384)
			{
				num = 16384;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			this.RenderAppendBuilder(logEvent, stringBuilder);
			if (stringBuilder.Length > this._maxRenderedLength)
			{
				this._maxRenderedLength = stringBuilder.Length;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00020149 File Offset: 0x0001E349
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00020152 File Offset: 0x0001E352
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002015A File Offset: 0x0001E35A
		internal void Initialize(LoggingConfiguration configuration)
		{
			if (this.LoggingConfiguration == null)
			{
				this.LoggingConfiguration = configuration;
			}
			if (!this._isInitialized)
			{
				this._isInitialized = true;
				this.InitializeLayoutRenderer();
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00020180 File Offset: 0x0001E380
		internal void Close()
		{
			if (this._isInitialized)
			{
				this.LoggingConfiguration = null;
				this._isInitialized = false;
				this.CloseLayoutRenderer();
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000201A0 File Offset: 0x0001E3A0
		internal void RenderAppendBuilder(LogEventInfo logEvent, StringBuilder builder)
		{
			if (!this._isInitialized)
			{
				this._isInitialized = true;
				this.InitializeLayoutRenderer();
			}
			try
			{
				this.Append(builder, logEvent);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Exception in layout renderer.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x06000C88 RID: 3208
		protected abstract void Append(StringBuilder builder, LogEventInfo logEvent);

		// Token: 0x06000C89 RID: 3209 RVA: 0x000201F4 File Offset: 0x0001E3F4
		protected virtual void InitializeLayoutRenderer()
		{
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000201F6 File Offset: 0x0001E3F6
		protected virtual void CloseLayoutRenderer()
		{
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000201F8 File Offset: 0x0001E3F8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00020203 File Offset: 0x0001E403
		protected IFormatProvider GetFormatProvider(LogEventInfo logEvent, IFormatProvider layoutCulture = null)
		{
			IFormatProvider formatProvider;
			if ((formatProvider = logEvent.FormatProvider) == null)
			{
				formatProvider = layoutCulture;
				if (layoutCulture == null)
				{
					LoggingConfiguration loggingConfiguration = this.LoggingConfiguration;
					if (loggingConfiguration == null)
					{
						return null;
					}
					formatProvider = loggingConfiguration.DefaultCultureInfo;
				}
			}
			return formatProvider;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00020228 File Offset: 0x0001E428
		protected CultureInfo GetCulture(LogEventInfo logEvent, CultureInfo layoutCulture = null)
		{
			CultureInfo cultureInfo = (logEvent.FormatProvider as CultureInfo) ?? layoutCulture;
			if (cultureInfo == null && this.LoggingConfiguration != null)
			{
				cultureInfo = this.LoggingConfiguration.DefaultCultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00020260 File Offset: 0x0001E460
		public static void Register<T>(string name) where T : LayoutRenderer
		{
			Type typeFromHandle = typeof(T);
			LayoutRenderer.Register(name, typeFromHandle);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002027F File Offset: 0x0001E47F
		public static void Register(string name, Type layoutRendererType)
		{
			ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition(name, layoutRendererType);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00020294 File Offset: 0x0001E494
		public static void Register(string name, Func<LogEventInfo, object> func)
		{
			LayoutRenderer.Register(name, (LogEventInfo info, LoggingConfiguration configuration) => func(info));
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000202C0 File Offset: 0x0001E4C0
		public static void Register(string name, Func<LogEventInfo, LoggingConfiguration, object> func)
		{
			FuncLayoutRenderer funcLayoutRenderer = new FuncLayoutRenderer(name, func);
			ConfigurationItemFactory.Default.GetLayoutRenderers().RegisterFuncLayout(name, funcLayoutRenderer);
		}

		// Token: 0x0400031F RID: 799
		private const int MaxInitialRenderBufferLength = 16384;

		// Token: 0x04000320 RID: 800
		private int _maxRenderedLength;

		// Token: 0x04000321 RID: 801
		private bool _isInitialized;
	}
}
