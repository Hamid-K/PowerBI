using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x0200002B RID: 43
	[Target("ColoredConsole")]
	public sealed class ColoredConsoleTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0000A4E0 File Offset: 0x000086E0
		public ColoredConsoleTarget()
		{
			this.WordHighlightingRules = new List<ConsoleWordHighlightingRule>();
			this.RowHighlightingRules = new List<ConsoleRowHighlightingRule>();
			this.UseDefaultRowHighlightingRules = true;
			this._pauseLogging = false;
			this.DetectConsoleAvailable = false;
			base.OptimizeBufferReuse = true;
			this._consolePrinter = ColoredConsoleTarget.CreateConsolePrinter(this.EnableAnsiOutput);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000A536 File Offset: 0x00008736
		public ColoredConsoleTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000A545 File Offset: 0x00008745
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0000A54D File Offset: 0x0000874D
		[DefaultValue(false)]
		public bool ErrorStream { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000A556 File Offset: 0x00008756
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0000A55E File Offset: 0x0000875E
		[DefaultValue(true)]
		public bool UseDefaultRowHighlightingRules { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000A567 File Offset: 0x00008767
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000A580 File Offset: 0x00008780
		public Encoding Encoding
		{
			get
			{
				return ConsoleTargetHelper.GetConsoleOutputEncoding(this._encoding, base.IsInitialized, this._pauseLogging);
			}
			set
			{
				if (ConsoleTargetHelper.SetConsoleOutputEncoding(value, base.IsInitialized, this._pauseLogging))
				{
					this._encoding = value;
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000A59D File Offset: 0x0000879D
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000A5A5 File Offset: 0x000087A5
		[DefaultValue(false)]
		public bool DetectConsoleAvailable { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000A5AE File Offset: 0x000087AE
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000A5B6 File Offset: 0x000087B6
		[DefaultValue(false)]
		public bool AutoFlush { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000A5BF File Offset: 0x000087BF
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000A5C7 File Offset: 0x000087C7
		[DefaultValue(false)]
		public bool EnableAnsiOutput { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000A5D0 File Offset: 0x000087D0
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000A5D8 File Offset: 0x000087D8
		[ArrayParameter(typeof(ConsoleRowHighlightingRule), "highlight-row")]
		public IList<ConsoleRowHighlightingRule> RowHighlightingRules { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000A5E1 File Offset: 0x000087E1
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0000A5E9 File Offset: 0x000087E9
		[ArrayParameter(typeof(ConsoleWordHighlightingRule), "highlight-word")]
		public IList<ConsoleWordHighlightingRule> WordHighlightingRules { get; private set; }

		// Token: 0x06000507 RID: 1287 RVA: 0x0000A5F4 File Offset: 0x000087F4
		protected override void InitializeTarget()
		{
			this._pauseLogging = false;
			if (this.DetectConsoleAvailable)
			{
				string text;
				this._pauseLogging = !ConsoleTargetHelper.IsConsoleAvailable(out text);
				if (this._pauseLogging)
				{
					InternalLogger.Info<string>("Console has been detected as turned off. Disable DetectConsoleAvailable to skip detection. Reason: {0}", text);
				}
			}
			if (this._encoding != null)
			{
				ConsoleTargetHelper.SetConsoleOutputEncoding(this._encoding, true, this._pauseLogging);
			}
			base.InitializeTarget();
			if (base.Header != null)
			{
				LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
				this.WriteToOutput(logEventInfo, base.RenderLogEvent(base.Header, logEventInfo));
			}
			this._consolePrinter = ColoredConsoleTarget.CreateConsolePrinter(this.EnableAnsiOutput);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000A687 File Offset: 0x00008887
		private static IColoredConsolePrinter CreateConsolePrinter(bool enableAnsiOutput)
		{
			if (!enableAnsiOutput)
			{
				return new ColoredConsoleSystemPrinter();
			}
			return new ColoredConsoleAnsiPrinter();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000A698 File Offset: 0x00008898
		protected override void CloseTarget()
		{
			if (base.Footer != null)
			{
				LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
				this.WriteToOutput(logEventInfo, base.RenderLogEvent(base.Footer, logEventInfo));
			}
			this.ExplicitConsoleFlush();
			base.CloseTarget();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000A6D4 File Offset: 0x000088D4
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			try
			{
				this.ExplicitConsoleFlush();
				base.FlushAsync(asyncContinuation);
			}
			catch (Exception ex)
			{
				asyncContinuation(ex);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000A70C File Offset: 0x0000890C
		private void ExplicitConsoleFlush()
		{
			if (!this._pauseLogging && !this.AutoFlush)
			{
				this.GetOutput().Flush();
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000A729 File Offset: 0x00008929
		protected override void Write(LogEventInfo logEvent)
		{
			if (this._pauseLogging)
			{
				return;
			}
			this.WriteToOutput(logEvent, base.RenderLogEvent(this.Layout, logEvent));
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000A748 File Offset: 0x00008948
		private void WriteToOutput(LogEventInfo logEvent, string message)
		{
			try
			{
				this.WriteToOutputWithColor(logEvent, message);
			}
			catch (IndexOutOfRangeException ex)
			{
				this._pauseLogging = true;
				InternalLogger.Warn(ex, "An IndexOutOfRangeException has been thrown and this is probably due to a race condition.Logging to the console will be paused. Enable by reloading the config or re-initialize the targets");
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				this._pauseLogging = true;
				InternalLogger.Warn(ex2, "An ArgumentOutOfRangeException has been thrown and this is probably due to a race condition.Logging to the console will be paused. Enable by reloading the config or re-initialize the targets");
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000A7A4 File Offset: 0x000089A4
		private void WriteToOutputWithColor(LogEventInfo logEvent, string message)
		{
			ConsoleRowHighlightingRule matchingRowHighlightingRule = this.GetMatchingRowHighlightingRule(logEvent);
			string text = message ?? string.Empty;
			if (this.WordHighlightingRules.Count > 0)
			{
				text = this.GenerateColorEscapeSequences(message);
			}
			ConsoleColor? consoleColor = ((matchingRowHighlightingRule.ForegroundColor != ConsoleOutputColor.NoChange) ? new ConsoleColor?((ConsoleColor)matchingRowHighlightingRule.ForegroundColor) : null);
			ConsoleColor? consoleColor2 = ((matchingRowHighlightingRule.BackgroundColor != ConsoleOutputColor.NoChange) ? new ConsoleColor?((ConsoleColor)matchingRowHighlightingRule.BackgroundColor) : null);
			TextWriter output = this.GetOutput();
			if (text == message && consoleColor == null && consoleColor2 == null)
			{
				output.WriteLine(message);
			}
			else
			{
				bool flag = text != message || (message != null && message.IndexOf('\n') >= 0);
				this.WriteToOutputWithPrinter(output, text, consoleColor, consoleColor2, flag);
			}
			if (this.AutoFlush)
			{
				output.Flush();
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000A880 File Offset: 0x00008A80
		private void WriteToOutputWithPrinter(TextWriter consoleStream, string colorMessage, ConsoleColor? newForegroundColor, ConsoleColor? newBackgroundColor, bool wordHighlighting)
		{
			using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = (base.OptimizeBufferReuse ? this.ReusableLayoutBuilder.Allocate() : this.ReusableLayoutBuilder.None))
			{
				TextWriter textWriter = this._consolePrinter.AcquireTextWriter(consoleStream, lockOject.Result);
				ConsoleColor? consoleColor = null;
				ConsoleColor? consoleColor2 = null;
				try
				{
					if (wordHighlighting)
					{
						consoleColor = this._consolePrinter.ChangeForegroundColor(textWriter, newForegroundColor);
						consoleColor2 = this._consolePrinter.ChangeBackgroundColor(textWriter, newBackgroundColor);
						ConsoleColor? consoleColor3 = newForegroundColor;
						ConsoleColor? consoleColor4 = ((consoleColor3 != null) ? consoleColor3 : consoleColor);
						consoleColor3 = newBackgroundColor;
						ConsoleColor? consoleColor5 = ((consoleColor3 != null) ? consoleColor3 : consoleColor2);
						ColoredConsoleTarget.ColorizeEscapeSequences(this._consolePrinter, textWriter, colorMessage, consoleColor, consoleColor2, consoleColor4, consoleColor5);
						this._consolePrinter.WriteLine(textWriter, string.Empty);
					}
					else
					{
						if (newForegroundColor != null)
						{
							consoleColor = this._consolePrinter.ChangeForegroundColor(textWriter, new ConsoleColor?(newForegroundColor.Value));
							ConsoleColor? consoleColor3 = consoleColor;
							ConsoleColor? consoleColor6 = newForegroundColor;
							if ((consoleColor3.GetValueOrDefault() == consoleColor6.GetValueOrDefault()) & (consoleColor3 != null == (consoleColor6 != null)))
							{
								consoleColor = null;
							}
						}
						if (newBackgroundColor != null)
						{
							consoleColor2 = this._consolePrinter.ChangeBackgroundColor(textWriter, new ConsoleColor?(newBackgroundColor.Value));
							ConsoleColor? consoleColor6 = consoleColor2;
							ConsoleColor? consoleColor3 = newBackgroundColor;
							if ((consoleColor6.GetValueOrDefault() == consoleColor3.GetValueOrDefault()) & (consoleColor6 != null == (consoleColor3 != null)))
							{
								consoleColor2 = null;
							}
						}
						this._consolePrinter.WriteLine(textWriter, colorMessage);
					}
				}
				finally
				{
					this._consolePrinter.ReleaseTextWriter(textWriter, consoleStream, consoleColor, consoleColor2);
				}
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000AA4C File Offset: 0x00008C4C
		private ConsoleRowHighlightingRule GetMatchingRowHighlightingRule(LogEventInfo logEvent)
		{
			ConsoleRowHighlightingRule consoleRowHighlightingRule = this.GetMatchingRowHighlightingRule(this.RowHighlightingRules, logEvent);
			if (consoleRowHighlightingRule == null && this.UseDefaultRowHighlightingRules)
			{
				consoleRowHighlightingRule = this.GetMatchingRowHighlightingRule(this._consolePrinter.DefaultConsoleRowHighlightingRules, logEvent);
			}
			return consoleRowHighlightingRule ?? ConsoleRowHighlightingRule.Default;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000AA90 File Offset: 0x00008C90
		private ConsoleRowHighlightingRule GetMatchingRowHighlightingRule(IList<ConsoleRowHighlightingRule> rules, LogEventInfo logEvent)
		{
			for (int i = 0; i < rules.Count; i++)
			{
				ConsoleRowHighlightingRule consoleRowHighlightingRule = rules[i];
				if (consoleRowHighlightingRule.CheckCondition(logEvent))
				{
					return consoleRowHighlightingRule;
				}
			}
			return null;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		private string GenerateColorEscapeSequences(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return message;
			}
			if (message.IndexOf("\a", StringComparison.Ordinal) >= 0)
			{
				message = message.Replace("\a", "\a\a");
			}
			using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = (base.OptimizeBufferReuse ? this.ReusableLayoutBuilder.Allocate() : this.ReusableLayoutBuilder.None))
			{
				StringBuilder stringBuilder = lockOject.Result;
				for (int i = 0; i < this.WordHighlightingRules.Count; i++)
				{
					ConsoleWordHighlightingRule consoleWordHighlightingRule = this.WordHighlightingRules[i];
					MatchCollection matchCollection = consoleWordHighlightingRule.Matches(message);
					if (matchCollection != null)
					{
						if (stringBuilder != null)
						{
							stringBuilder.Length = 0;
						}
						int num = 0;
						foreach (object obj in matchCollection)
						{
							Match match = (Match)obj;
							stringBuilder = stringBuilder ?? new StringBuilder(message.Length + 5);
							stringBuilder.Append(message, num, match.Index - num);
							stringBuilder.Append('\a');
							stringBuilder.Append((char)(consoleWordHighlightingRule.ForegroundColor + 65));
							stringBuilder.Append((char)(consoleWordHighlightingRule.BackgroundColor + 65));
							stringBuilder.Append(match.Value);
							stringBuilder.Append('\a');
							stringBuilder.Append('X');
							num = match.Index + match.Length;
						}
						if (stringBuilder != null && stringBuilder.Length > 0)
						{
							stringBuilder.Append(message, num, message.Length - num);
							message = stringBuilder.ToString();
						}
					}
				}
			}
			return message;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000AC98 File Offset: 0x00008E98
		private static void ColorizeEscapeSequences(IColoredConsolePrinter consolePrinter, TextWriter consoleWriter, string message, ConsoleColor? defaultForegroundColor, ConsoleColor? defaultBackgroundColor, ConsoleColor? rowForegroundColor, ConsoleColor? rowBackgroundColor)
		{
			Stack<KeyValuePair<ConsoleColor?, ConsoleColor?>> stack = new Stack<KeyValuePair<ConsoleColor?, ConsoleColor?>>();
			stack.Push(new KeyValuePair<ConsoleColor?, ConsoleColor?>(rowForegroundColor, rowBackgroundColor));
			int i = 0;
			while (i < message.Length)
			{
				int num = i;
				while (num < message.Length && message[num] >= ' ')
				{
					num++;
				}
				if (num != i)
				{
					consolePrinter.WriteSubString(consoleWriter, message, i, num);
				}
				if (num >= message.Length)
				{
					i = num;
					break;
				}
				char c = message[num];
				char c2 = '\0';
				if (num + 1 < message.Length)
				{
					c2 = message[num + 1];
				}
				if (c == '\a' && c2 == '\a')
				{
					consolePrinter.WriteChar(consoleWriter, '\a');
					i = num + 2;
				}
				else if (c == '\r' || c == '\n')
				{
					consolePrinter.ResetDefaultColors(consoleWriter, defaultForegroundColor, defaultBackgroundColor);
					consolePrinter.WriteChar(consoleWriter, c);
					i = num + 1;
					if (c2 == '\n')
					{
						consolePrinter.WriteChar(consoleWriter, c2);
						i = num + 2;
					}
					consolePrinter.ChangeForegroundColor(consoleWriter, stack.Peek().Key);
					consolePrinter.ChangeBackgroundColor(consoleWriter, stack.Peek().Value);
				}
				else if (c == '\a')
				{
					if (c2 == 'X')
					{
						KeyValuePair<ConsoleColor?, ConsoleColor?> keyValuePair = stack.Pop();
						KeyValuePair<ConsoleColor?, ConsoleColor?> keyValuePair2 = stack.Peek();
						ConsoleColor? consoleColor = keyValuePair2.Key;
						ConsoleColor? consoleColor2 = keyValuePair.Key;
						if (!((consoleColor.GetValueOrDefault() == consoleColor2.GetValueOrDefault()) & (consoleColor != null == (consoleColor2 != null))))
						{
							goto IL_0181;
						}
						consoleColor2 = keyValuePair2.Value;
						consoleColor = keyValuePair.Value;
						if (!((consoleColor2.GetValueOrDefault() == consoleColor.GetValueOrDefault()) & (consoleColor2 != null == (consoleColor != null))))
						{
							goto IL_0181;
						}
						IL_01F1:
						i = num + 2;
						continue;
						IL_0181:
						if ((keyValuePair.Key != null && keyValuePair2.Key == null) || (keyValuePair.Value != null && keyValuePair2.Value == null))
						{
							consolePrinter.ResetDefaultColors(consoleWriter, defaultForegroundColor, defaultBackgroundColor);
						}
						consolePrinter.ChangeForegroundColor(consoleWriter, keyValuePair2.Key);
						consolePrinter.ChangeBackgroundColor(consoleWriter, keyValuePair2.Value);
						goto IL_01F1;
					}
					ConsoleColor? key = stack.Peek().Key;
					ConsoleColor? value = stack.Peek().Value;
					ConsoleOutputColor consoleOutputColor = (ConsoleOutputColor)(c2 - 'A');
					ConsoleOutputColor consoleOutputColor2 = (ConsoleOutputColor)(message[num + 2] - 'A');
					if (consoleOutputColor != ConsoleOutputColor.NoChange)
					{
						key = new ConsoleColor?((ConsoleColor)consoleOutputColor);
						consolePrinter.ChangeForegroundColor(consoleWriter, key);
					}
					if (consoleOutputColor2 != ConsoleOutputColor.NoChange)
					{
						value = new ConsoleColor?((ConsoleColor)consoleOutputColor2);
						consolePrinter.ChangeBackgroundColor(consoleWriter, value);
					}
					stack.Push(new KeyValuePair<ConsoleColor?, ConsoleColor?>(key, value));
					i = num + 3;
				}
				else
				{
					consolePrinter.WriteChar(consoleWriter, c);
					i = num + 1;
				}
			}
			if (i < message.Length)
			{
				consolePrinter.WriteSubString(consoleWriter, message, i, message.Length);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000AF4D File Offset: 0x0000914D
		private TextWriter GetOutput()
		{
			if (!this.ErrorStream)
			{
				return Console.Out;
			}
			return Console.Error;
		}

		// Token: 0x04000068 RID: 104
		private bool _pauseLogging;

		// Token: 0x04000069 RID: 105
		private IColoredConsolePrinter _consolePrinter;

		// Token: 0x0400006C RID: 108
		private Encoding _encoding;
	}
}
