using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.Layouts
{
	// Token: 0x020000A6 RID: 166
	[NLogConfigurationItem]
	public abstract class Layout : ISupportsInitialize, IRenderable
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0001BA8B File Offset: 0x00019C8B
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0001BA93 File Offset: 0x00019C93
		internal bool ThreadAgnostic { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0001BA9C File Offset: 0x00019C9C
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0001BAA4 File Offset: 0x00019CA4
		internal bool ThreadSafe { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0001BAAD File Offset: 0x00019CAD
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0001BAB5 File Offset: 0x00019CB5
		internal bool MutableUnsafe { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0001BABE File Offset: 0x00019CBE
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0001BAC6 File Offset: 0x00019CC6
		internal StackTraceUsage StackTraceUsage { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0001BACF File Offset: 0x00019CCF
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0001BAD7 File Offset: 0x00019CD7
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001BAE0 File Offset: 0x00019CE0
		public static implicit operator Layout([Localizable(false)] string text)
		{
			return Layout.FromString(text);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		public static Layout FromString(string layoutText)
		{
			return Layout.FromString(layoutText, ConfigurationItemFactory.Default);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001BAF5 File Offset: 0x00019CF5
		public static Layout FromString(string layoutText, ConfigurationItemFactory configurationItemFactory)
		{
			return new SimpleLayout(layoutText, configurationItemFactory);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001BAFE File Offset: 0x00019CFE
		public virtual void Precalculate(LogEventInfo logEvent)
		{
			if (!this.ThreadAgnostic || this.MutableUnsafe)
			{
				this.Render(logEvent);
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001BB18 File Offset: 0x00019D18
		public string Render(LogEventInfo logEvent)
		{
			if (!this.IsInitialized)
			{
				this.Initialize(this.LoggingConfiguration);
			}
			object obj;
			if ((!this.ThreadAgnostic || this.MutableUnsafe) && logEvent.TryGetCachedLayoutValue(this, out obj))
			{
				return ((obj != null) ? obj.ToString() : null) ?? string.Empty;
			}
			string text = this.GetFormattedMessage(logEvent) ?? string.Empty;
			if (!this.ThreadAgnostic || this.MutableUnsafe)
			{
				logEvent.AddCachedLayoutValue(this, text);
			}
			return text;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001BB94 File Offset: 0x00019D94
		internal virtual void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
		{
			this.Precalculate(logEvent);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		internal void RenderAppendBuilder(LogEventInfo logEvent, StringBuilder target, bool cacheLayoutResult = false)
		{
			if (!this.IsInitialized)
			{
				this.Initialize(this.LoggingConfiguration);
			}
			object obj;
			if ((!this.ThreadAgnostic || this.MutableUnsafe) && logEvent.TryGetCachedLayoutValue(this, out obj))
			{
				target.Append(((obj != null) ? obj.ToString() : null) ?? string.Empty);
				return;
			}
			cacheLayoutResult = cacheLayoutResult && !this.ThreadAgnostic;
			using (AppendBuilderCreator appendBuilderCreator = new AppendBuilderCreator(target, cacheLayoutResult))
			{
				this.RenderFormattedMessage(logEvent, appendBuilderCreator.Builder);
				if (cacheLayoutResult)
				{
					logEvent.AddCachedLayoutValue(this, appendBuilderCreator.Builder.ToString());
				}
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001BC58 File Offset: 0x00019E58
		internal string RenderAllocateBuilder(LogEventInfo logEvent, StringBuilder reusableBuilder = null)
		{
			int num = this._maxRenderedLength;
			if (num > 16384)
			{
				num = 16384;
			}
			StringBuilder stringBuilder = reusableBuilder ?? new StringBuilder(num);
			this.RenderFormattedMessage(logEvent, stringBuilder);
			if (stringBuilder.Length > this._maxRenderedLength)
			{
				this._maxRenderedLength = stringBuilder.Length;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001BCAE File Offset: 0x00019EAE
		protected virtual void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			target.Append(this.GetFormattedMessage(logEvent) ?? string.Empty);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001BCC7 File Offset: 0x00019EC7
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		internal void Initialize(LoggingConfiguration configuration)
		{
			if (!this.IsInitialized)
			{
				this.LoggingConfiguration = configuration;
				this.IsInitialized = true;
				this._scannedForObjects = false;
				this.InitializeLayout();
				if (!this._scannedForObjects)
				{
					InternalLogger.Debug("Initialized Layout done but not scanned for objects");
					this.PerformObjectScanning();
				}
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001BD18 File Offset: 0x00019F18
		internal void PerformObjectScanning()
		{
			List<object> list = ObjectGraphScanner.FindReachableObjects<object>(true, new object[] { this });
			this.ThreadAgnostic = list.All((object item) => item.GetType().IsDefined(typeof(ThreadAgnosticAttribute), true));
			this.ThreadSafe = list.All((object item) => item.GetType().IsDefined(typeof(ThreadSafeAttribute), true));
			this.MutableUnsafe = list.Any((object item) => item.GetType().IsDefined(typeof(MutableUnsafeAttribute), true));
			this.StackTraceUsage = StackTraceUsage.None;
			this.StackTraceUsage = list.OfType<IUsesStackTrace>().DefaultIfEmpty<IUsesStackTrace>().Max(delegate(IUsesStackTrace item)
			{
				if (item == null)
				{
					return StackTraceUsage.None;
				}
				return item.StackTraceUsage;
			});
			this._scannedForObjects = true;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001BDFA File Offset: 0x00019FFA
		internal void Close()
		{
			if (this.IsInitialized)
			{
				this.LoggingConfiguration = null;
				this.IsInitialized = false;
				this.CloseLayout();
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001BE18 File Offset: 0x0001A018
		protected virtual void InitializeLayout()
		{
			this.PerformObjectScanning();
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001BE20 File Offset: 0x0001A020
		protected virtual void CloseLayout()
		{
		}

		// Token: 0x06000ACE RID: 2766
		protected abstract string GetFormattedMessage(LogEventInfo logEvent);

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001BE24 File Offset: 0x0001A024
		public static void Register<T>(string name) where T : Layout
		{
			Type typeFromHandle = typeof(T);
			Layout.Register(name, typeFromHandle);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001BE43 File Offset: 0x0001A043
		public static void Register(string name, Type layoutType)
		{
			ConfigurationItemFactory.Default.Layouts.RegisterDefinition(name, layoutType);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001BE56 File Offset: 0x0001A056
		internal void PrecalculateBuilderInternal(LogEventInfo logEvent, StringBuilder target)
		{
			if (!this.ThreadAgnostic || this.MutableUnsafe)
			{
				this.RenderAppendBuilder(logEvent, target, true);
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001BE74 File Offset: 0x0001A074
		internal string ToStringWithNestedItems<T>(IList<T> nestedItems, Func<T, string> nextItemToString)
		{
			if (nestedItems != null && nestedItems.Count > 0)
			{
				string[] array = nestedItems.Select((T c) => nextItemToString(c)).ToArray<string>();
				return base.GetType().Name + "=" + string.Join("|", array);
			}
			return base.ToString();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001BED9 File Offset: 0x0001A0D9
		internal virtual bool TryGetRawValue(LogEventInfo logEvent, out object rawValue)
		{
			rawValue = null;
			return false;
		}

		// Token: 0x04000289 RID: 649
		internal bool IsInitialized;

		// Token: 0x0400028A RID: 650
		private bool _scannedForObjects;

		// Token: 0x0400028F RID: 655
		private const int MaxInitialRenderBufferLength = 16384;

		// Token: 0x04000290 RID: 656
		private int _maxRenderedLength;
	}
}
