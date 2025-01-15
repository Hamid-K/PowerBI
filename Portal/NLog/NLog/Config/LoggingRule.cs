using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using NLog.Filters;
using NLog.Targets;

namespace NLog.Config
{
	// Token: 0x02000194 RID: 404
	[NLogConfigurationItem]
	public class LoggingRule
	{
		// Token: 0x0600127D RID: 4733 RVA: 0x000323DC File Offset: 0x000305DC
		public LoggingRule()
			: this(null)
		{
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000323E8 File Offset: 0x000305E8
		public LoggingRule(string ruleName)
		{
			this.RuleName = ruleName;
			this.Filters = new List<Filter>();
			this.ChildRules = new List<LoggingRule>();
			this.Targets = new List<Target>();
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00032446 File Offset: 0x00030646
		public LoggingRule(string loggerNamePattern, LogLevel minLevel, LogLevel maxLevel, Target target)
			: this()
		{
			this.LoggerNamePattern = loggerNamePattern;
			this.Targets.Add(target);
			this.EnableLoggingForLevels(minLevel, maxLevel);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0003246A File Offset: 0x0003066A
		public LoggingRule(string loggerNamePattern, LogLevel minLevel, Target target)
			: this()
		{
			this.LoggerNamePattern = loggerNamePattern;
			this.Targets.Add(target);
			this.EnableLoggingForLevels(minLevel, LogLevel.MaxLevel);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00032491 File Offset: 0x00030691
		public LoggingRule(string loggerNamePattern, Target target)
			: this()
		{
			this.LoggerNamePattern = loggerNamePattern;
			this.Targets.Add(target);
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x000324AC File Offset: 0x000306AC
		public string RuleName { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x000324B4 File Offset: 0x000306B4
		public IList<Target> Targets { get; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x000324BC File Offset: 0x000306BC
		public IList<LoggingRule> ChildRules { get; }

		// Token: 0x06001285 RID: 4741 RVA: 0x000324C4 File Offset: 0x000306C4
		internal List<LoggingRule> GetChildRulesThreadSafe()
		{
			IList<LoggingRule> childRules = this.ChildRules;
			List<LoggingRule> list;
			lock (childRules)
			{
				list = this.ChildRules.ToList<LoggingRule>();
			}
			return list;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0003250C File Offset: 0x0003070C
		internal List<Target> GetTargetsThreadSafe()
		{
			IList<Target> targets = this.Targets;
			List<Target> list;
			lock (targets)
			{
				list = this.Targets.ToList<Target>();
			}
			return list;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00032554 File Offset: 0x00030754
		internal bool RemoveTargetThreadSafe(Target target)
		{
			IList<Target> targets = this.Targets;
			bool flag2;
			lock (targets)
			{
				flag2 = this.Targets.Remove(target);
			}
			return flag2;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0003259C File Offset: 0x0003079C
		public IList<Filter> Filters { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000325A4 File Offset: 0x000307A4
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x000325AC File Offset: 0x000307AC
		public bool Final { get; set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000325B5 File Offset: 0x000307B5
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x000325C2 File Offset: 0x000307C2
		public string LoggerNamePattern
		{
			get
			{
				return this._loggerNameMatcher.Pattern;
			}
			set
			{
				this._loggerNameMatcher = LoggerNameMatcher.Create(value);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x000325D0 File Offset: 0x000307D0
		[NLogConfigurationIgnoreProperty]
		public ReadOnlyCollection<LogLevel> Levels
		{
			get
			{
				List<LogLevel> list = new List<LogLevel>();
				for (int i = LogLevel.MinLevel.Ordinal; i <= LogLevel.MaxLevel.Ordinal; i++)
				{
					if (this._logLevels[i])
					{
						list.Add(LogLevel.FromOrdinal(i));
					}
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0003261D File Offset: 0x0003081D
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x00032625 File Offset: 0x00030825
		public FilterResult DefaultFilterResult { get; set; }

		// Token: 0x06001290 RID: 4752 RVA: 0x0003262E File Offset: 0x0003082E
		public void EnableLoggingForLevel(LogLevel level)
		{
			if (level == LogLevel.Off)
			{
				return;
			}
			this._logLevels[level.Ordinal] = true;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0003264C File Offset: 0x0003084C
		public void EnableLoggingForLevels(LogLevel minLevel, LogLevel maxLevel)
		{
			for (int i = minLevel.Ordinal; i <= maxLevel.Ordinal; i++)
			{
				this.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003267B File Offset: 0x0003087B
		public void DisableLoggingForLevel(LogLevel level)
		{
			if (level == LogLevel.Off)
			{
				return;
			}
			this._logLevels[level.Ordinal] = false;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0003269C File Offset: 0x0003089C
		public void DisableLoggingForLevels(LogLevel minLevel, LogLevel maxLevel)
		{
			for (int i = minLevel.Ordinal; i <= maxLevel.Ordinal; i++)
			{
				this.DisableLoggingForLevel(LogLevel.FromOrdinal(i));
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000326CB File Offset: 0x000308CB
		public void SetLoggingLevels(LogLevel minLevel, LogLevel maxLevel)
		{
			this.DisableLoggingForLevels(LogLevel.MinLevel, LogLevel.MaxLevel);
			this.EnableLoggingForLevels(minLevel, maxLevel);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000326E8 File Offset: 0x000308E8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._loggerNameMatcher.ToString());
			stringBuilder.Append(" levels: [ ");
			for (int i = 0; i < this._logLevels.Length; i++)
			{
				if (this._logLevels[i])
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} ", new object[] { LogLevel.FromOrdinal(i).ToString() });
				}
			}
			stringBuilder.Append("] appendTo: [ ");
			foreach (Target target in this.GetTargetsThreadSafe())
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} ", new object[] { target.Name });
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000327D8 File Offset: 0x000309D8
		public bool IsLoggingEnabledForLevel(LogLevel level)
		{
			return !(level == LogLevel.Off) && this._logLevels[level.Ordinal];
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000327F6 File Offset: 0x000309F6
		public bool NameMatches(string loggerName)
		{
			return this._loggerNameMatcher.NameMatches(loggerName);
		}

		// Token: 0x040004F6 RID: 1270
		private readonly bool[] _logLevels = new bool[LogLevel.MaxLevel.Ordinal + 1];

		// Token: 0x040004F7 RID: 1271
		private LoggerNameMatcher _loggerNameMatcher = LoggerNameMatcher.Create(null);
	}
}
