using System;
using System.Text.RegularExpressions;

namespace NLog.Config
{
	// Token: 0x0200018C RID: 396
	internal abstract class LoggerNameMatcher
	{
		// Token: 0x060011E3 RID: 4579 RVA: 0x0002E5E0 File Offset: 0x0002C7E0
		public static LoggerNameMatcher Create(string loggerNamePattern)
		{
			if (loggerNamePattern == null)
			{
				return LoggerNameMatcher.NoneLoggerNameMatcher.Instance;
			}
			int num = loggerNamePattern.IndexOf('*');
			int num2 = loggerNamePattern.IndexOf('*', num + 1);
			int num3 = loggerNamePattern.IndexOf('?');
			if (num < 0 && num3 < 0)
			{
				return new LoggerNameMatcher.EqualsLoggerNameMatcher(loggerNamePattern);
			}
			if (loggerNamePattern == "*")
			{
				return LoggerNameMatcher.AllLoggerNameMatcher.Instance;
			}
			if (num3 < 0)
			{
				if (num == 0 && num2 == loggerNamePattern.Length - 1)
				{
					return new LoggerNameMatcher.ContainsLoggerNameMatcher(loggerNamePattern);
				}
				if (num2 < 0)
				{
					LoggerNameMatcher loggerNameMatcher = LoggerNameMatcher.CreateStartsOrEndsWithLoggerNameMatcher(loggerNamePattern, num);
					if (loggerNameMatcher != null)
					{
						return loggerNameMatcher;
					}
				}
			}
			return new LoggerNameMatcher.MultiplePatternLoggerNameMatcher(loggerNamePattern);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0002E666 File Offset: 0x0002C866
		private static LoggerNameMatcher CreateStartsOrEndsWithLoggerNameMatcher(string loggerNamePattern, int starPos1)
		{
			if (starPos1 == 0)
			{
				return new LoggerNameMatcher.EndsWithLoggerNameMatcher(loggerNamePattern);
			}
			if (starPos1 == loggerNamePattern.Length - 1)
			{
				return new LoggerNameMatcher.StartsWithLoggerNameMatcher(loggerNamePattern);
			}
			return null;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0002E685 File Offset: 0x0002C885
		public string Pattern { get; }

		// Token: 0x060011E6 RID: 4582 RVA: 0x0002E690 File Offset: 0x0002C890
		protected LoggerNameMatcher(string pattern, string matchingArgument)
		{
			this.Pattern = pattern;
			this._matchingArgument = matchingArgument;
			this._toString = string.Concat(new string[] { "logNamePattern: (", matchingArgument, ":", this.Name, ")" });
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0002E6E7 File Offset: 0x0002C8E7
		public override string ToString()
		{
			return this._toString;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060011E8 RID: 4584
		protected abstract string Name { get; }

		// Token: 0x060011E9 RID: 4585
		public abstract bool NameMatches(string loggerName);

		// Token: 0x040004E0 RID: 1248
		protected readonly string _matchingArgument;

		// Token: 0x040004E1 RID: 1249
		private readonly string _toString;

		// Token: 0x020002A2 RID: 674
		private class NoneLoggerNameMatcher : LoggerNameMatcher
		{
			// Token: 0x17000439 RID: 1081
			// (get) Token: 0x06001700 RID: 5888 RVA: 0x0003C3B7 File Offset: 0x0003A5B7
			protected override string Name
			{
				get
				{
					return "None";
				}
			}

			// Token: 0x06001701 RID: 5889 RVA: 0x0003C3BE File Offset: 0x0003A5BE
			private NoneLoggerNameMatcher()
				: base(null, null)
			{
			}

			// Token: 0x06001702 RID: 5890 RVA: 0x0003C3C8 File Offset: 0x0003A5C8
			public override bool NameMatches(string loggerName)
			{
				return false;
			}

			// Token: 0x0400074E RID: 1870
			public static readonly LoggerNameMatcher.NoneLoggerNameMatcher Instance = new LoggerNameMatcher.NoneLoggerNameMatcher();
		}

		// Token: 0x020002A3 RID: 675
		private class AllLoggerNameMatcher : LoggerNameMatcher
		{
			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x06001704 RID: 5892 RVA: 0x0003C3D7 File Offset: 0x0003A5D7
			protected override string Name
			{
				get
				{
					return "All";
				}
			}

			// Token: 0x06001705 RID: 5893 RVA: 0x0003C3DE File Offset: 0x0003A5DE
			private AllLoggerNameMatcher()
				: base("*", null)
			{
			}

			// Token: 0x06001706 RID: 5894 RVA: 0x0003C3EC File Offset: 0x0003A5EC
			public override bool NameMatches(string loggerName)
			{
				return true;
			}

			// Token: 0x0400074F RID: 1871
			public static readonly LoggerNameMatcher.AllLoggerNameMatcher Instance = new LoggerNameMatcher.AllLoggerNameMatcher();
		}

		// Token: 0x020002A4 RID: 676
		private class EqualsLoggerNameMatcher : LoggerNameMatcher
		{
			// Token: 0x1700043B RID: 1083
			// (get) Token: 0x06001708 RID: 5896 RVA: 0x0003C3FB File Offset: 0x0003A5FB
			protected override string Name
			{
				get
				{
					return "Equals";
				}
			}

			// Token: 0x06001709 RID: 5897 RVA: 0x0003C402 File Offset: 0x0003A602
			public EqualsLoggerNameMatcher(string pattern)
				: base(pattern, pattern)
			{
			}

			// Token: 0x0600170A RID: 5898 RVA: 0x0003C40C File Offset: 0x0003A60C
			public override bool NameMatches(string loggerName)
			{
				if (loggerName == null)
				{
					return this._matchingArgument == null;
				}
				return loggerName.Equals(this._matchingArgument, StringComparison.Ordinal);
			}
		}

		// Token: 0x020002A5 RID: 677
		private class StartsWithLoggerNameMatcher : LoggerNameMatcher
		{
			// Token: 0x1700043C RID: 1084
			// (get) Token: 0x0600170B RID: 5899 RVA: 0x0003C428 File Offset: 0x0003A628
			protected override string Name
			{
				get
				{
					return "StartsWith";
				}
			}

			// Token: 0x0600170C RID: 5900 RVA: 0x0003C42F File Offset: 0x0003A62F
			public StartsWithLoggerNameMatcher(string pattern)
				: base(pattern, pattern.Substring(0, pattern.Length - 1))
			{
			}

			// Token: 0x0600170D RID: 5901 RVA: 0x0003C447 File Offset: 0x0003A647
			public override bool NameMatches(string loggerName)
			{
				if (loggerName == null)
				{
					return this._matchingArgument == null;
				}
				return loggerName.StartsWith(this._matchingArgument, StringComparison.Ordinal);
			}
		}

		// Token: 0x020002A6 RID: 678
		private class EndsWithLoggerNameMatcher : LoggerNameMatcher
		{
			// Token: 0x1700043D RID: 1085
			// (get) Token: 0x0600170E RID: 5902 RVA: 0x0003C463 File Offset: 0x0003A663
			protected override string Name
			{
				get
				{
					return "EndsWith";
				}
			}

			// Token: 0x0600170F RID: 5903 RVA: 0x0003C46A File Offset: 0x0003A66A
			public EndsWithLoggerNameMatcher(string pattern)
				: base(pattern, pattern.Substring(1))
			{
			}

			// Token: 0x06001710 RID: 5904 RVA: 0x0003C47A File Offset: 0x0003A67A
			public override bool NameMatches(string loggerName)
			{
				if (loggerName == null)
				{
					return this._matchingArgument == null;
				}
				return loggerName.EndsWith(this._matchingArgument, StringComparison.Ordinal);
			}
		}

		// Token: 0x020002A7 RID: 679
		private class ContainsLoggerNameMatcher : LoggerNameMatcher
		{
			// Token: 0x1700043E RID: 1086
			// (get) Token: 0x06001711 RID: 5905 RVA: 0x0003C496 File Offset: 0x0003A696
			protected override string Name
			{
				get
				{
					return "Contains";
				}
			}

			// Token: 0x06001712 RID: 5906 RVA: 0x0003C49D File Offset: 0x0003A69D
			public ContainsLoggerNameMatcher(string pattern)
				: base(pattern, pattern.Substring(1, pattern.Length - 2))
			{
			}

			// Token: 0x06001713 RID: 5907 RVA: 0x0003C4B5 File Offset: 0x0003A6B5
			public override bool NameMatches(string loggerName)
			{
				if (loggerName == null)
				{
					return this._matchingArgument == null;
				}
				return loggerName.IndexOf(this._matchingArgument, StringComparison.Ordinal) >= 0;
			}
		}

		// Token: 0x020002A8 RID: 680
		private class MultiplePatternLoggerNameMatcher : LoggerNameMatcher
		{
			// Token: 0x1700043F RID: 1087
			// (get) Token: 0x06001714 RID: 5908 RVA: 0x0003C4D7 File Offset: 0x0003A6D7
			protected override string Name
			{
				get
				{
					return "MultiplePattern";
				}
			}

			// Token: 0x06001715 RID: 5909 RVA: 0x0003C4DE File Offset: 0x0003A6DE
			private static string ConvertToRegex(string wildcardsPattern)
			{
				return "^" + Regex.Escape(wildcardsPattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			}

			// Token: 0x06001716 RID: 5910 RVA: 0x0003C513 File Offset: 0x0003A713
			public MultiplePatternLoggerNameMatcher(string pattern)
				: base(pattern, LoggerNameMatcher.MultiplePatternLoggerNameMatcher.ConvertToRegex(pattern))
			{
				this._regex = new Regex(this._matchingArgument, RegexOptions.CultureInvariant);
			}

			// Token: 0x06001717 RID: 5911 RVA: 0x0003C538 File Offset: 0x0003A738
			public override bool NameMatches(string loggerName)
			{
				return loggerName != null && this._regex.IsMatch(loggerName);
			}

			// Token: 0x04000750 RID: 1872
			private readonly Regex _regex;
		}
	}
}
