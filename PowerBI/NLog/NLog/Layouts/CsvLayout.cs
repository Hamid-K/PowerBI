using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;
using NLog.LayoutRenderers;

namespace NLog.Layouts
{
	// Token: 0x020000A2 RID: 162
	[Layout("CsvLayout")]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class CsvLayout : LayoutWithHeaderAndFooter
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x0001AF64 File Offset: 0x00019164
		public CsvLayout()
		{
			this.Columns = new List<CsvColumn>();
			this.WithHeader = true;
			this.Delimiter = CsvColumnDelimiterMode.Auto;
			this.Quoting = CsvQuotingMode.Auto;
			this.QuoteChar = "\"";
			base.Layout = this;
			base.Header = new CsvLayout.CsvHeaderLayout(this);
			base.Footer = null;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0001AFBC File Offset: 0x000191BC
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x0001AFC4 File Offset: 0x000191C4
		[ArrayParameter(typeof(CsvColumn), "column")]
		public IList<CsvColumn> Columns { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001AFCD File Offset: 0x000191CD
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0001AFD5 File Offset: 0x000191D5
		public bool WithHeader { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001AFDE File Offset: 0x000191DE
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0001AFE6 File Offset: 0x000191E6
		[DefaultValue("Auto")]
		public CsvColumnDelimiterMode Delimiter { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001AFEF File Offset: 0x000191EF
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0001AFF7 File Offset: 0x000191F7
		[DefaultValue("Auto")]
		public CsvQuotingMode Quoting { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0001B000 File Offset: 0x00019200
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0001B008 File Offset: 0x00019208
		[DefaultValue("\"")]
		public string QuoteChar { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001B011 File Offset: 0x00019211
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0001B019 File Offset: 0x00019219
		public string CustomColumnDelimiter { get; set; }

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001B024 File Offset: 0x00019224
		protected override void InitializeLayout()
		{
			if (!this.WithHeader)
			{
				base.Header = null;
			}
			base.InitializeLayout();
			switch (this.Delimiter)
			{
			case CsvColumnDelimiterMode.Auto:
				this._actualColumnDelimiter = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
				break;
			case CsvColumnDelimiterMode.Comma:
				this._actualColumnDelimiter = ",";
				break;
			case CsvColumnDelimiterMode.Semicolon:
				this._actualColumnDelimiter = ";";
				break;
			case CsvColumnDelimiterMode.Tab:
				this._actualColumnDelimiter = "\t";
				break;
			case CsvColumnDelimiterMode.Pipe:
				this._actualColumnDelimiter = "|";
				break;
			case CsvColumnDelimiterMode.Space:
				this._actualColumnDelimiter = " ";
				break;
			case CsvColumnDelimiterMode.Custom:
				this._actualColumnDelimiter = this.CustomColumnDelimiter;
				break;
			}
			this._quotableCharacters = (this.QuoteChar + "\r\n" + this._actualColumnDelimiter).ToCharArray();
			this._doubleQuoteChar = this.QuoteChar + this.QuoteChar;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001B10D File Offset: 0x0001930D
		internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
		{
			base.PrecalculateBuilderInternal(logEvent, target);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001B117 File Offset: 0x00019317
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return base.RenderAllocateBuilder(logEvent, null);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001B124 File Offset: 0x00019324
		protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			for (int i = 0; i < this.Columns.Count; i++)
			{
				Layout layout = this.Columns[i].Layout;
				this.RenderColumnLayout(logEvent, layout, this.Columns[i]._quoting ?? this.Quoting, target, i);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001B190 File Offset: 0x00019390
		private void RenderColumnLayout(LogEventInfo logEvent, Layout columnLayout, CsvQuotingMode quoting, StringBuilder target, int i)
		{
			if (i != 0)
			{
				target.Append(this._actualColumnDelimiter);
			}
			if (quoting == CsvQuotingMode.All)
			{
				target.Append(this.QuoteChar);
			}
			int length = target.Length;
			columnLayout.RenderAppendBuilder(logEvent, target, false);
			if (length != target.Length && this.ColumnValueRequiresQuotes(quoting, target, length))
			{
				string text = target.ToString(length, target.Length - length);
				target.Length = length;
				if (quoting != CsvQuotingMode.All)
				{
					target.Append(this.QuoteChar);
				}
				target.Append(text.Replace(this.QuoteChar, this._doubleQuoteChar));
				target.Append(this.QuoteChar);
				return;
			}
			if (quoting == CsvQuotingMode.All)
			{
				target.Append(this.QuoteChar);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001B250 File Offset: 0x00019450
		private void RenderHeader(StringBuilder sb)
		{
			LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
			for (int i = 0; i < this.Columns.Count; i++)
			{
				CsvColumn csvColumn = this.Columns[i];
				SimpleLayout simpleLayout = new SimpleLayout(new LayoutRenderer[]
				{
					new LiteralLayoutRenderer(csvColumn.Name)
				}, csvColumn.Name, ConfigurationItemFactory.Default);
				simpleLayout.Initialize(base.LoggingConfiguration);
				this.RenderColumnLayout(logEventInfo, simpleLayout, csvColumn._quoting ?? this.Quoting, sb, i);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001B2E4 File Offset: 0x000194E4
		private bool ColumnValueRequiresQuotes(CsvQuotingMode quoting, StringBuilder sb, int startPosition)
		{
			switch (quoting)
			{
			case CsvQuotingMode.All:
				if (this.QuoteChar.Length == 1)
				{
					return sb.IndexOf(this.QuoteChar[0], startPosition) >= 0;
				}
				return sb.IndexOfAny(this._quotableCharacters, startPosition) >= 0;
			case CsvQuotingMode.Nothing:
				return false;
			}
			return sb.IndexOfAny(this._quotableCharacters, startPosition) >= 0;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001B356 File Offset: 0x00019556
		public override string ToString()
		{
			return base.ToStringWithNestedItems<CsvColumn>(this.Columns, (CsvColumn c) => c.Name);
		}

		// Token: 0x0400026E RID: 622
		private string _actualColumnDelimiter;

		// Token: 0x0400026F RID: 623
		private string _doubleQuoteChar;

		// Token: 0x04000270 RID: 624
		private char[] _quotableCharacters;

		// Token: 0x02000251 RID: 593
		[ThreadAgnostic]
		[ThreadSafe]
		[AppDomainFixedOutput]
		private class CsvHeaderLayout : Layout
		{
			// Token: 0x060015C7 RID: 5575 RVA: 0x00039A9B File Offset: 0x00037C9B
			public CsvHeaderLayout(CsvLayout parent)
			{
				this._parent = parent;
			}

			// Token: 0x060015C8 RID: 5576 RVA: 0x00039AAA File Offset: 0x00037CAA
			protected override void InitializeLayout()
			{
				this._headerOutput = null;
				base.InitializeLayout();
			}

			// Token: 0x060015C9 RID: 5577 RVA: 0x00039ABC File Offset: 0x00037CBC
			private string GetHeaderOutput()
			{
				string text;
				if ((text = this._headerOutput) == null)
				{
					text = (this._headerOutput = this.BuilderHeaderOutput());
				}
				return text;
			}

			// Token: 0x060015CA RID: 5578 RVA: 0x00039AE4 File Offset: 0x00037CE4
			private string BuilderHeaderOutput()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this._parent.RenderHeader(stringBuilder);
				return stringBuilder.ToString();
			}

			// Token: 0x060015CB RID: 5579 RVA: 0x00039B09 File Offset: 0x00037D09
			internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
			{
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x00039B0B File Offset: 0x00037D0B
			protected override string GetFormattedMessage(LogEventInfo logEvent)
			{
				return this.GetHeaderOutput();
			}

			// Token: 0x060015CD RID: 5581 RVA: 0x00039B13 File Offset: 0x00037D13
			protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
			{
				target.Append(this.GetHeaderOutput());
			}

			// Token: 0x04000669 RID: 1641
			private readonly CsvLayout _parent;

			// Token: 0x0400066A RID: 1642
			private string _headerOutput;
		}
	}
}
