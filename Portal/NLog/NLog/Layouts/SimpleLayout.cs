using System;
using System.Collections.ObjectModel;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.LayoutRenderers;

namespace NLog.Layouts
{
	// Token: 0x020000AB RID: 171
	[Layout("SimpleLayout")]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class SimpleLayout : Layout, IUsesStackTrace
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0001C8E1 File Offset: 0x0001AAE1
		public SimpleLayout()
			: this(string.Empty)
		{
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001C8EE File Offset: 0x0001AAEE
		public SimpleLayout(string txt)
			: this(txt, ConfigurationItemFactory.Default)
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		public SimpleLayout(string txt, ConfigurationItemFactory configurationItemFactory)
		{
			this._configurationItemFactory = configurationItemFactory;
			this.Text = txt;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001C912 File Offset: 0x0001AB12
		internal SimpleLayout(LayoutRenderer[] renderers, string text, ConfigurationItemFactory configurationItemFactory)
		{
			this._configurationItemFactory = configurationItemFactory;
			this.SetRenderers(renderers, text);
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001C929 File Offset: 0x0001AB29
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x0001C931 File Offset: 0x0001AB31
		public string OriginalText { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0001C93A File Offset: 0x0001AB3A
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x0001C944 File Offset: 0x0001AB44
		public string Text
		{
			get
			{
				return this._layoutText;
			}
			set
			{
				this.OriginalText = value;
				LayoutRenderer[] array;
				string empty;
				if (value == null)
				{
					array = ArrayHelper.Empty<LayoutRenderer>();
					empty = string.Empty;
				}
				else
				{
					array = LayoutParser.CompileLayout(this._configurationItemFactory, new SimpleStringReader(value), false, out empty);
				}
				this.SetRenderers(array, empty);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0001C986 File Offset: 0x0001AB86
		public bool IsFixedText
		{
			get
			{
				return this._fixedText != null;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0001C991 File Offset: 0x0001AB91
		public string FixedText
		{
			get
			{
				return this._fixedText;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0001C999 File Offset: 0x0001AB99
		internal bool IsSimpleStringText
		{
			get
			{
				return this._stringValueRenderer != null;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0001C9AC File Offset: 0x0001ABAC
		public ReadOnlyCollection<LayoutRenderer> Renderers { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001C9B5 File Offset: 0x0001ABB5
		public new StackTraceUsage StackTraceUsage
		{
			get
			{
				return base.StackTraceUsage;
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0001C9BD File Offset: 0x0001ABBD
		public new static implicit operator SimpleLayout(string text)
		{
			if (text == null)
			{
				return null;
			}
			return new SimpleLayout(text);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001C9CA File Offset: 0x0001ABCA
		public static string Escape(string text)
		{
			return text.Replace("${", "${literal:text=${}");
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		public static string Evaluate(string text, LogEventInfo logEvent)
		{
			return new SimpleLayout(text).Render(logEvent);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001C9EA File Offset: 0x0001ABEA
		public static string Evaluate(string text)
		{
			return SimpleLayout.Evaluate(text, LogEventInfo.CreateNullEvent());
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Text))
			{
				ReadOnlyCollection<LayoutRenderer> renderers = this.Renderers;
				if (renderers != null && renderers.Count > 0)
				{
					return base.ToStringWithNestedItems<LayoutRenderer>(this.Renderers, (LayoutRenderer r) => r.ToString());
				}
			}
			return "'" + this.Text + "'";
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		internal void SetRenderers(LayoutRenderer[] renderers, string text)
		{
			this.Renderers = new ReadOnlyCollection<LayoutRenderer>(renderers);
			this._fixedText = null;
			this._rawValueRenderer = null;
			this._stringValueRenderer = null;
			if (this.Renderers.Count == 1)
			{
				LiteralLayoutRenderer literalLayoutRenderer;
				if ((literalLayoutRenderer = this.Renderers[0] as LiteralLayoutRenderer) != null)
				{
					this._fixedText = literalLayoutRenderer.Text;
				}
				else
				{
					IRawValue rawValue;
					if ((rawValue = this.Renderers[0] as IRawValue) != null)
					{
						this._rawValueRenderer = rawValue;
					}
					IStringValueRenderer stringValueRenderer;
					if ((stringValueRenderer = this.Renderers[0] as IStringValueRenderer) != null)
					{
						this._stringValueRenderer = stringValueRenderer;
					}
				}
			}
			this._layoutText = text;
			if (base.LoggingConfiguration != null)
			{
				base.PerformObjectScanning();
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001CB18 File Offset: 0x0001AD18
		protected override void InitializeLayout()
		{
			for (int i = 0; i < this.Renderers.Count; i++)
			{
				LayoutRenderer layoutRenderer = this.Renderers[i];
				try
				{
					layoutRenderer.Initialize(base.LoggingConfiguration);
				}
				catch (Exception ex)
				{
					if (InternalLogger.IsWarnEnabled || InternalLogger.IsErrorEnabled)
					{
						InternalLogger.Warn(ex, "Exception in '{0}.InitializeLayout()'", new object[] { layoutRenderer.GetType().FullName });
					}
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			base.InitializeLayout();
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		public override void Precalculate(LogEventInfo logEvent)
		{
			if (this._rawValueRenderer != null)
			{
				if (!this.IsInitialized)
				{
					base.Initialize(base.LoggingConfiguration);
				}
				object obj;
				if (base.ThreadAgnostic && base.MutableUnsafe && ((this._rawValueRenderer.TryGetRawValue(logEvent, out obj) && Convert.GetTypeCode(obj) != TypeCode.Object) || obj.GetType().IsValueType()))
				{
					return;
				}
			}
			base.Precalculate(logEvent);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001CC0F File Offset: 0x0001AE0F
		internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
		{
			base.PrecalculateBuilderInternal(logEvent, target);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		internal override bool TryGetRawValue(LogEventInfo logEvent, out object rawValue)
		{
			if (!this.IsInitialized)
			{
				base.Initialize(base.LoggingConfiguration);
			}
			try
			{
				if (this._rawValueRenderer != null)
				{
					object obj;
					if ((!base.ThreadAgnostic || base.MutableUnsafe) && logEvent.TryGetCachedLayoutValue(this, out obj))
					{
						rawValue = null;
						return false;
					}
					return this._rawValueRenderer.TryGetRawValue(logEvent, out rawValue);
				}
			}
			catch (Exception ex)
			{
				if (InternalLogger.IsWarnEnabled || InternalLogger.IsErrorEnabled)
				{
					InternalLogger.Warn(ex, "Exception in '{0}.InitializeLayout()'", new object[] { (this.Renderers.Count > 0) ? this.Renderers[0].GetType().FullName : null });
				}
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
			rawValue = null;
			return false;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			if (this.IsFixedText)
			{
				return this._fixedText;
			}
			if (this._stringValueRenderer != null)
			{
				try
				{
					string formattedString = this._stringValueRenderer.GetFormattedString(logEvent);
					if (formattedString != null)
					{
						return formattedString;
					}
					this._stringValueRenderer = null;
				}
				catch (Exception ex)
				{
					if (InternalLogger.IsWarnEnabled || InternalLogger.IsErrorEnabled)
					{
						Exception ex2 = ex;
						string text = "Exception in '{0}.Append()'";
						object[] array = new object[1];
						int num = 0;
						IStringValueRenderer stringValueRenderer = this._stringValueRenderer;
						array[num] = ((stringValueRenderer != null) ? stringValueRenderer.GetType().FullName : null);
						InternalLogger.Warn(ex2, text, array);
					}
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			return base.RenderAllocateBuilder(logEvent, null);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		private void RenderAllRenderers(LogEventInfo logEvent, StringBuilder target)
		{
			for (int i = 0; i < this.Renderers.Count; i++)
			{
				LayoutRenderer layoutRenderer = this.Renderers[i];
				try
				{
					layoutRenderer.RenderAppendBuilder(logEvent, target);
				}
				catch (Exception ex)
				{
					if (InternalLogger.IsWarnEnabled || InternalLogger.IsErrorEnabled)
					{
						InternalLogger.Warn(ex, "Exception in '{0}.Append()'", new object[] { layoutRenderer.GetType().FullName });
					}
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001CE10 File Offset: 0x0001B010
		protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			if (this.IsFixedText)
			{
				target.Append(this._fixedText);
				return;
			}
			this.RenderAllRenderers(logEvent, target);
		}

		// Token: 0x04000296 RID: 662
		private string _fixedText;

		// Token: 0x04000297 RID: 663
		private string _layoutText;

		// Token: 0x04000298 RID: 664
		private IRawValue _rawValueRenderer;

		// Token: 0x04000299 RID: 665
		private IStringValueRenderer _stringValueRenderer;

		// Token: 0x0400029A RID: 666
		private readonly ConfigurationItemFactory _configurationItemFactory;
	}
}
