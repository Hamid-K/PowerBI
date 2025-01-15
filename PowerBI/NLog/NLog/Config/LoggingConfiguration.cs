using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Internal;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace NLog.Config
{
	// Token: 0x0200018D RID: 397
	public class LoggingConfiguration
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0002E6EF File Offset: 0x0002C8EF
		public LogFactory LogFactory { get; }

		// Token: 0x060011EB RID: 4587 RVA: 0x0002E6F7 File Offset: 0x0002C8F7
		public LoggingConfiguration()
			: this(LogManager.LogFactory)
		{
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0002E704 File Offset: 0x0002C904
		public LoggingConfiguration(LogFactory logFactory)
		{
			this.LogFactory = logFactory;
			this.LoggingRules = new List<LoggingRule>();
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0002E754 File Offset: 0x0002C954
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0002E75C File Offset: 0x0002C95C
		[Obsolete("This option will be removed in NLog 5. Marked obsolete on NLog 4.1")]
		public bool ExceptionLoggingOldStyle { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0002E765 File Offset: 0x0002C965
		public IDictionary<string, SimpleLayout> Variables
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0002E76D File Offset: 0x0002C96D
		public ReadOnlyCollection<Target> ConfiguredNamedTargets
		{
			get
			{
				return this.GetAllTargetsThreadSafe().AsReadOnly();
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0002E77A File Offset: 0x0002C97A
		public virtual IEnumerable<string> FileNamesToWatch
		{
			get
			{
				return ArrayHelper.Empty<string>();
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0002E781 File Offset: 0x0002C981
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0002E789 File Offset: 0x0002C989
		public IList<LoggingRule> LoggingRules { get; private set; }

		// Token: 0x060011F4 RID: 4596 RVA: 0x0002E794 File Offset: 0x0002C994
		internal List<LoggingRule> GetLoggingRulesThreadSafe()
		{
			IList<LoggingRule> loggingRules = this.LoggingRules;
			List<LoggingRule> list;
			lock (loggingRules)
			{
				list = this.LoggingRules.ToList<LoggingRule>();
			}
			return list;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0002E7DC File Offset: 0x0002C9DC
		private void AddLoggingRulesThreadSafe(LoggingRule rule)
		{
			IList<LoggingRule> loggingRules = this.LoggingRules;
			lock (loggingRules)
			{
				this.LoggingRules.Add(rule);
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0002E824 File Offset: 0x0002CA24
		private bool TryGetTargetThreadSafe(string name, out Target target)
		{
			IDictionary<string, Target> targets = this._targets;
			bool flag2;
			lock (targets)
			{
				flag2 = this._targets.TryGetValue(name, out target);
			}
			return flag2;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0002E870 File Offset: 0x0002CA70
		private List<Target> GetAllTargetsThreadSafe()
		{
			IDictionary<string, Target> targets = this._targets;
			List<Target> list;
			lock (targets)
			{
				list = this._targets.Values.ToList<Target>();
			}
			return list;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0002E8BC File Offset: 0x0002CABC
		private Target RemoveTargetThreadSafe(string name)
		{
			IDictionary<string, Target> targets = this._targets;
			Target target;
			lock (targets)
			{
				if (this._targets.TryGetValue(name, out target))
				{
					this._targets.Remove(name);
				}
			}
			if (target != null)
			{
				InternalLogger.Debug<string, string>("Unregistered target {0}: {1}", name, target.GetType().FullName);
			}
			return target;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0002E930 File Offset: 0x0002CB30
		private void AddTargetThreadSafe(string name, Target target, bool forceOverwrite)
		{
			if (string.IsNullOrEmpty(name) && !forceOverwrite)
			{
				return;
			}
			IDictionary<string, Target> targets = this._targets;
			lock (targets)
			{
				if (!forceOverwrite && this._targets.ContainsKey(name))
				{
					return;
				}
				this._targets[name] = target;
			}
			if (!string.IsNullOrEmpty(target.Name) && target.Name != name)
			{
				InternalLogger.Info<string, string, string>("Registered target {0}: {1} (Target created with different name: {2})", name, target.GetType().FullName, target.Name);
				return;
			}
			InternalLogger.Debug<string, string>("Registered target {0}: {1}", name, target.GetType().FullName);
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0002E9E4 File Offset: 0x0002CBE4
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0002E9EC File Offset: 0x0002CBEC
		[CanBeNull]
		public CultureInfo DefaultCultureInfo { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0002E9F5 File Offset: 0x0002CBF5
		public ReadOnlyCollection<Target> AllTargets
		{
			get
			{
				return this._configItems.OfType<Target>().Concat(this.GetAllTargetsThreadSafe()).Distinct(LoggingConfiguration.TargetNameComparer)
					.ToList<Target>()
					.AsReadOnly();
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0002EA21 File Offset: 0x0002CC21
		public void AddTarget([NotNull] Target target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.Name == null)
			{
				throw new ArgumentNullException("target.Name cannot be null.");
			}
			this.AddTargetThreadSafe(target.Name, target, true);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0002EA52 File Offset: 0x0002CC52
		public void AddTarget(string name, Target target)
		{
			if (name == null)
			{
				throw new ArgumentException("Target name cannot be null", "name");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.AddTargetThreadSafe(name, target, true);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0002EA80 File Offset: 0x0002CC80
		public Target FindTargetByName(string name)
		{
			Target target;
			if (!this.TryGetTargetThreadSafe(name, out target))
			{
				return null;
			}
			return target;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0002EA9B File Offset: 0x0002CC9B
		public TTarget FindTargetByName<TTarget>(string name) where TTarget : Target
		{
			return this.FindTargetByName(name) as TTarget;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		public void AddRule(LogLevel minLevel, LogLevel maxLevel, string targetName, string loggerNamePattern = "*")
		{
			Target target = this.FindTargetByName(targetName);
			if (target == null)
			{
				throw new NLogRuntimeException("Target '{0}' not found", new object[] { targetName });
			}
			this.AddRule(minLevel, maxLevel, target, loggerNamePattern, false);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0002EAE9 File Offset: 0x0002CCE9
		public void AddRule(LogLevel minLevel, LogLevel maxLevel, Target target, string loggerNamePattern = "*")
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.AddRule(minLevel, maxLevel, target, loggerNamePattern, false);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0002EB05 File Offset: 0x0002CD05
		public void AddRule(LogLevel minLevel, LogLevel maxLevel, Target target, string loggerNamePattern, bool final)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.AddLoggingRulesThreadSafe(new LoggingRule(loggerNamePattern, minLevel, maxLevel, target)
			{
				Final = final
			});
			this.AddTargetThreadSafe(target.Name, target, false);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		public void AddRuleForOneLevel(LogLevel level, string targetName, string loggerNamePattern = "*")
		{
			Target target = this.FindTargetByName(targetName);
			if (target == null)
			{
				throw new NLogConfigurationException("Target '{0}' not found", new object[] { targetName });
			}
			this.AddRuleForOneLevel(level, target, loggerNamePattern, false);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0002EB73 File Offset: 0x0002CD73
		public void AddRuleForOneLevel(LogLevel level, Target target, string loggerNamePattern = "*")
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.AddRuleForOneLevel(level, target, loggerNamePattern, false);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0002EB90 File Offset: 0x0002CD90
		public void AddRuleForOneLevel(LogLevel level, Target target, string loggerNamePattern, bool final)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			LoggingRule loggingRule = new LoggingRule(loggerNamePattern, target)
			{
				Final = final
			};
			loggingRule.EnableLoggingForLevel(level);
			this.AddLoggingRulesThreadSafe(loggingRule);
			this.AddTargetThreadSafe(target.Name, target, false);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
		public void AddRuleForAllLevels(string targetName, string loggerNamePattern = "*")
		{
			Target target = this.FindTargetByName(targetName);
			if (target == null)
			{
				throw new NLogRuntimeException("Target '{0}' not found", new object[] { targetName });
			}
			this.AddRuleForAllLevels(target, loggerNamePattern, false);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0002EC0E File Offset: 0x0002CE0E
		public void AddRuleForAllLevels(Target target, string loggerNamePattern = "*")
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.AddRuleForAllLevels(target, loggerNamePattern, false);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0002EC28 File Offset: 0x0002CE28
		public void AddRuleForAllLevels(Target target, string loggerNamePattern, bool final)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			LoggingRule loggingRule = new LoggingRule(loggerNamePattern, target)
			{
				Final = final
			};
			loggingRule.EnableLoggingForLevels(LogLevel.MinLevel, LogLevel.MaxLevel);
			this.AddLoggingRulesThreadSafe(loggingRule);
			this.AddTargetThreadSafe(target.Name, target, false);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0002EC78 File Offset: 0x0002CE78
		public LoggingRule FindRuleByName(string ruleName)
		{
			if (ruleName == null)
			{
				return null;
			}
			List<LoggingRule> loggingRulesThreadSafe = this.GetLoggingRulesThreadSafe();
			for (int i = loggingRulesThreadSafe.Count - 1; i >= 0; i--)
			{
				if (string.Equals(loggingRulesThreadSafe[i].RuleName, ruleName, StringComparison.OrdinalIgnoreCase))
				{
					return loggingRulesThreadSafe[i];
				}
			}
			return null;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		public bool RemoveRuleByName(string ruleName)
		{
			if (ruleName == null)
			{
				return false;
			}
			HashSet<LoggingRule> hashSet = new HashSet<LoggingRule>();
			foreach (LoggingRule loggingRule in this.GetLoggingRulesThreadSafe())
			{
				if (string.Equals(loggingRule.RuleName, ruleName, StringComparison.OrdinalIgnoreCase))
				{
					hashSet.Add(loggingRule);
				}
			}
			if (hashSet.Count > 0)
			{
				IList<LoggingRule> loggingRules = this.LoggingRules;
				lock (loggingRules)
				{
					for (int i = this.LoggingRules.Count - 1; i >= 0; i--)
					{
						if (hashSet.Contains(this.LoggingRules[i]))
						{
							this.LoggingRules.RemoveAt(i);
						}
					}
				}
			}
			return hashSet.Count > 0;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0002EDB0 File Offset: 0x0002CFB0
		public virtual LoggingConfiguration Reload()
		{
			return this;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0002EDB4 File Offset: 0x0002CFB4
		internal LoggingConfiguration ReloadNewConfig()
		{
			LoggingConfiguration loggingConfiguration = this.Reload();
			if (loggingConfiguration != null)
			{
				XmlLoggingConfiguration xmlLoggingConfiguration;
				if ((xmlLoggingConfiguration = loggingConfiguration as XmlLoggingConfiguration) != null)
				{
					bool? initializeSucceeded = xmlLoggingConfiguration.InitializeSucceeded;
					bool flag = true;
					if (!((initializeSucceeded.GetValueOrDefault() == flag) & (initializeSucceeded != null)))
					{
						InternalLogger.Warn("NLog Config Reload() failed. Invalid XML?");
						return null;
					}
				}
				LogFactory logFactory = this.LogFactory ?? LogManager.LogFactory;
				if (logFactory.KeepVariablesOnReload)
				{
					LoggingConfiguration loggingConfiguration2 = logFactory._config ?? this;
					if (loggingConfiguration != loggingConfiguration2)
					{
						loggingConfiguration.CopyVariables(loggingConfiguration2.Variables);
					}
				}
			}
			return loggingConfiguration;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0002EE38 File Offset: 0x0002D038
		public void RemoveTarget(string name)
		{
			HashSet<Target> hashSet = new HashSet<Target>();
			Target target = this.RemoveTargetThreadSafe(name);
			if (target != null)
			{
				hashSet.Add(target);
			}
			if (!string.IsNullOrEmpty(name) || target != null)
			{
				this.CleanupRulesForRemovedTarget(name, target, hashSet);
			}
			if (hashSet.Count > 0)
			{
				this.ValidateConfig();
				if (this.LogFactory != null)
				{
					this.LogFactory.ReconfigExistingLoggers();
				}
				else
				{
					LogManager.ReconfigExistingLoggers();
				}
				ManualResetEvent flushCompleted = new ManualResetEvent(false);
				AsyncContinuation <>9__0;
				foreach (Target target2 in hashSet)
				{
					flushCompleted.Reset();
					AsyncContinuation asyncContinuation;
					if ((asyncContinuation = <>9__0) == null)
					{
						asyncContinuation = (<>9__0 = delegate(Exception ex)
						{
							flushCompleted.Set();
						});
					}
					target2.Flush(asyncContinuation);
					flushCompleted.WaitOne(TimeSpan.FromSeconds(15.0));
					target2.Close();
				}
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0002EF3C File Offset: 0x0002D13C
		private void CleanupRulesForRemovedTarget(string name, Target removedTarget, HashSet<Target> removedTargets)
		{
			foreach (LoggingRule loggingRule in this.GetLoggingRulesThreadSafe())
			{
				foreach (Target target in loggingRule.GetTargetsThreadSafe())
				{
					if (removedTarget == target || (!string.IsNullOrEmpty(name) && target.Name == name))
					{
						removedTargets.Add(target);
						loggingRule.RemoveTargetThreadSafe(target);
					}
				}
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0002EFF0 File Offset: 0x0002D1F0
		public void Install(InstallationContext installationContext)
		{
			if (installationContext == null)
			{
				throw new ArgumentNullException("installationContext");
			}
			this.InitializeAll();
			foreach (IInstallable installable in this.GetInstallableItems())
			{
				installationContext.Info("Installing '{0}'", new object[] { installable });
				try
				{
					installable.Install(installationContext);
					installationContext.Info("Finished installing '{0}'.", new object[] { installable });
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Install of '{0}' failed.", new object[] { installable });
					if (ex.MustBeRethrownImmediately() || installationContext.ThrowExceptions)
					{
						throw;
					}
					installationContext.Error("Install of '{0}' failed: {1}.", new object[] { installable, ex });
				}
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0002F0D8 File Offset: 0x0002D2D8
		public void Uninstall(InstallationContext installationContext)
		{
			if (installationContext == null)
			{
				throw new ArgumentNullException("installationContext");
			}
			this.InitializeAll();
			foreach (IInstallable installable in this.GetInstallableItems())
			{
				installationContext.Info("Uninstalling '{0}'", new object[] { installable });
				try
				{
					installable.Uninstall(installationContext);
					installationContext.Info("Finished uninstalling '{0}'.", new object[] { installable });
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Uninstall of '{0}' failed.", new object[] { installable });
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					installationContext.Error("Uninstall of '{0}' failed: {1}.", new object[] { installable, ex });
				}
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		internal void Close()
		{
			InternalLogger.Debug("Closing logging configuration...");
			foreach (ISupportsInitialize supportsInitialize in this.GetSupportsInitializes(false))
			{
				InternalLogger.Trace<ISupportsInitialize>("Closing {0}", supportsInitialize);
				try
				{
					supportsInitialize.Close();
				}
				catch (Exception ex)
				{
					InternalLogger.Warn(ex, "Exception while closing.");
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			InternalLogger.Debug("Finished closing logging configuration.");
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0002F24C File Offset: 0x0002D44C
		internal void Dump()
		{
			if (!InternalLogger.IsDebugEnabled)
			{
				return;
			}
			InternalLogger.Debug("--- NLog configuration dump ---");
			InternalLogger.Debug("Targets:");
			foreach (Target target in this.GetAllTargetsThreadSafe())
			{
				InternalLogger.Debug<Target>("{0}", target);
			}
			InternalLogger.Debug("Rules:");
			foreach (LoggingRule loggingRule in this.GetLoggingRulesThreadSafe())
			{
				InternalLogger.Debug<LoggingRule>("{0}", loggingRule);
			}
			InternalLogger.Debug("--- End of NLog configuration dump ---");
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0002F31C File Offset: 0x0002D51C
		internal void FlushAllTargets(AsyncContinuation asyncContinuation)
		{
			InternalLogger.Trace("Flushing all targets...");
			List<Target> list = new List<Target>();
			foreach (LoggingRule loggingRule in this.GetLoggingRulesThreadSafe())
			{
				foreach (Target target2 in loggingRule.GetTargetsThreadSafe())
				{
					if (!list.Contains(target2))
					{
						list.Add(target2);
					}
				}
			}
			AsyncHelpers.ForEachItemInParallel<Target>(list, asyncContinuation, delegate(Target target, AsyncContinuation cont)
			{
				target.Flush(cont);
			});
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0002F3E8 File Offset: 0x0002D5E8
		internal void ValidateConfig()
		{
			List<object> list = new List<object>();
			foreach (LoggingRule loggingRule in this.GetLoggingRulesThreadSafe())
			{
				list.Add(loggingRule);
			}
			foreach (Target target in this.GetAllTargetsThreadSafe())
			{
				list.Add(target);
			}
			this._configItems = ObjectGraphScanner.FindReachableObjects<object>(true, list.ToArray());
			InternalLogger.Info<int>("Found {0} configuration items", this._configItems.Count);
			foreach (object obj in this._configItems)
			{
				PropertyHelper.CheckRequiredParameters(obj);
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0002F4EC File Offset: 0x0002D6EC
		internal void InitializeAll()
		{
			this.ValidateConfig();
			foreach (ISupportsInitialize supportsInitialize in this.GetSupportsInitializes(true))
			{
				InternalLogger.Trace<ISupportsInitialize>("Initializing {0}", supportsInitialize);
				try
				{
					supportsInitialize.Initialize(this);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					if (LogManager.ThrowExceptions)
					{
						throw new NLogConfigurationException(string.Format("Error during initialization of {0}", supportsInitialize), ex);
					}
				}
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0002F588 File Offset: 0x0002D788
		private List<IInstallable> GetInstallableItems()
		{
			return this._configItems.OfType<IInstallable>().ToList<IInstallable>();
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0002F59C File Offset: 0x0002D79C
		private List<ISupportsInitialize> GetSupportsInitializes(bool reverse = false)
		{
			IEnumerable<ISupportsInitialize> enumerable = this._configItems.OfType<ISupportsInitialize>();
			if (reverse)
			{
				enumerable = enumerable.Reverse<ISupportsInitialize>();
			}
			return enumerable.ToList<ISupportsInitialize>();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0002F5C5 File Offset: 0x0002D7C5
		internal void CopyVariables(IDictionary<string, SimpleLayout> masterVariables)
		{
			this._variables.CopyFrom(masterVariables);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0002F5D4 File Offset: 0x0002D7D4
		[NotNull]
		internal string ExpandSimpleVariables(string input)
		{
			string text = input;
			if (this.Variables.Count > 0 && text != null && text.IndexOf("${") >= 0)
			{
				foreach (KeyValuePair<string, SimpleLayout> keyValuePair in this.Variables.ToList<KeyValuePair<string, SimpleLayout>>())
				{
					SimpleLayout value = keyValuePair.Value;
					if (value != null)
					{
						text = text.Replace("${" + keyValuePair.Key + "}", value.OriginalText);
					}
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0002F680 File Offset: 0x0002D880
		internal void CheckUnusedTargets()
		{
			ReadOnlyCollection<Target> configuredNamedTargets = this.ConfiguredNamedTargets;
			InternalLogger.Debug<int, int>("Unused target checking is started... Rule Count: {0}, Target Count: {1}", this.LoggingRules.Count, configuredNamedTargets.Count);
			HashSet<string> targetNamesAtRules = new HashSet<string>(from t in this.GetLoggingRulesThreadSafe().SelectMany((LoggingRule r) => r.Targets)
				select t.Name);
			ILookup<Target, WrapperTargetBase> wrappedTargets = configuredNamedTargets.OfType<WrapperTargetBase>().ToLookup((WrapperTargetBase wt) => wt.WrappedTarget, (WrapperTargetBase wt) => wt);
			ILookup<Target, Target> compoundTargets = configuredNamedTargets.OfType<CompoundTargetBase>().SelectMany((CompoundTargetBase wt) => wt.Targets.Select((Target t) => new KeyValuePair<Target, Target>(t, wt))).ToLookup((KeyValuePair<Target, Target> p) => p.Key, (KeyValuePair<Target, Target> p) => p.Value);
			int num = configuredNamedTargets.Count(delegate(Target target)
			{
				if (targetNamesAtRules.Contains(target.Name))
				{
					return false;
				}
				if (!base.<CheckUnusedTargets>g__IsUnusedInList|7<WrapperTargetBase>(target, wrappedTargets))
				{
					return false;
				}
				if (!base.<CheckUnusedTargets>g__IsUnusedInList|7<Target>(target, compoundTargets))
				{
					return false;
				}
				InternalLogger.Warn<string>("Unused target detected. Add a rule for this target to the configuration. TargetName: {0}", target.Name);
				return true;
			});
			InternalLogger.Debug<int, int, int>("Unused target checking is completed. Total Rule Count: {0}, Total Target Count: {1}, Unused Target Count: {2}", this.LoggingRules.Count, configuredNamedTargets.Count, num);
		}

		// Token: 0x040004E2 RID: 1250
		private readonly IDictionary<string, Target> _targets = new Dictionary<string, Target>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040004E3 RID: 1251
		private List<object> _configItems = new List<object>();

		// Token: 0x040004E4 RID: 1252
		private readonly ThreadSafeDictionary<string, SimpleLayout> _variables = new ThreadSafeDictionary<string, SimpleLayout>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040004E9 RID: 1257
		private static readonly IEqualityComparer<Target> TargetNameComparer = new LoggingConfiguration.TargetNameEqualityComparer();

		// Token: 0x020002A9 RID: 681
		private class TargetNameEqualityComparer : IEqualityComparer<Target>
		{
			// Token: 0x06001718 RID: 5912 RVA: 0x0003C54B File Offset: 0x0003A74B
			public bool Equals(Target x, Target y)
			{
				return string.Equals(x.Name, y.Name);
			}

			// Token: 0x06001719 RID: 5913 RVA: 0x0003C55E File Offset: 0x0003A75E
			public int GetHashCode(Target obj)
			{
				if (obj.Name == null)
				{
					return 0;
				}
				return obj.Name.GetHashCode();
			}
		}
	}
}
