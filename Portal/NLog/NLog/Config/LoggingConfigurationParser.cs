using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Filters;
using NLog.Internal;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;
using NLog.Time;

namespace NLog.Config
{
	// Token: 0x02000190 RID: 400
	public abstract class LoggingConfigurationParser : LoggingConfiguration
	{
		// Token: 0x06001235 RID: 4661 RVA: 0x0002FB23 File Offset: 0x0002DD23
		protected LoggingConfigurationParser(LogFactory logFactory)
			: base(logFactory)
		{
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0002FB2C File Offset: 0x0002DD2C
		protected void LoadConfig(ILoggingConfigurationElement nlogConfig, string basePath)
		{
			InternalLogger.Trace("ParseNLogConfig");
			nlogConfig.AssertName(new string[] { "nlog" });
			this.SetNLogElementSettings(nlogConfig);
			List<ILoggingConfigurationElement> list = nlogConfig.Children.ToList<ILoggingConfigurationElement>();
			foreach (ILoggingConfigurationElement loggingConfigurationElement in list.Where((ILoggingConfigurationElement child) => child.MatchesName("extensions")).ToList<ILoggingConfigurationElement>())
			{
				this.ParseExtensionsElement(loggingConfigurationElement, basePath);
			}
			List<ILoggingConfigurationElement> list2 = new List<ILoggingConfigurationElement>();
			foreach (ILoggingConfigurationElement loggingConfigurationElement2 in list)
			{
				if (loggingConfigurationElement2.MatchesName("rules"))
				{
					list2.Add(loggingConfigurationElement2);
				}
				else if (!loggingConfigurationElement2.MatchesName("extensions") && !this.ParseNLogSection(loggingConfigurationElement2))
				{
					InternalLogger.Warn<string>("Skipping unknown 'NLog' child node: {0}", loggingConfigurationElement2.Name);
				}
			}
			foreach (ILoggingConfigurationElement loggingConfigurationElement3 in list2)
			{
				this.ParseRulesElement(loggingConfigurationElement3, base.LoggingRules);
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0002FC98 File Offset: 0x0002DE98
		private void SetNLogElementSettings(ILoggingConfigurationElement nlogConfig)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = LoggingConfigurationParser.CreateUniqueSortedListFromConfig(nlogConfig);
			bool? flag = null;
			bool flag2 = false;
			foreach (KeyValuePair<string, string> keyValuePair in enumerable)
			{
				string text = keyValuePair.Key.ToUpperInvariant();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2177058782U)
				{
					if (num <= 537616144U)
					{
						if (num != 101525956U)
						{
							if (num != 209464181U)
							{
								if (num == 537616144U)
								{
									if (text == "INTERNALLOGLEVEL")
									{
										InternalLogger.LogLevel = LoggingConfigurationParser.ParseLogLevelSafe(keyValuePair.Key, keyValuePair.Value, InternalLogger.LogLevel);
										flag2 = InternalLogger.LogLevel != LogLevel.Off;
										continue;
									}
								}
							}
							else if (text == "EXCEPTIONLOGGINGOLDSTYLE")
							{
								base.ExceptionLoggingOldStyle = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, base.ExceptionLoggingOldStyle);
								continue;
							}
						}
						else if (text == "INTERNALLOGFILE")
						{
							string value = keyValuePair.Value;
							string text2 = ((value != null) ? value.Trim() : null);
							if (!string.IsNullOrEmpty(text2))
							{
								text2 = LoggingConfigurationParser.ExpandFilePathVariables(text2);
								InternalLogger.LogFile = text2;
								continue;
							}
							continue;
						}
					}
					else if (num <= 1054224279U)
					{
						if (num != 687008442U)
						{
							if (num == 1054224279U)
							{
								if (text == "THROWCONFIGEXCEPTIONS")
								{
									base.LogFactory.ThrowConfigExceptions = (StringHelpers.IsNullOrWhiteSpace(keyValuePair.Value) ? null : new bool?(LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, false)));
									continue;
								}
							}
						}
						else if (text == "INTERNALLOGINCLUDETIMESTAMP")
						{
							InternalLogger.IncludeTimestamp = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, InternalLogger.IncludeTimestamp);
							continue;
						}
					}
					else if (num != 1862897841U)
					{
						if (num == 2177058782U)
						{
							if (text == "PARSEMESSAGETEMPLATES")
							{
								flag = (string.IsNullOrEmpty(keyValuePair.Value) ? null : new bool?(LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, true)));
								continue;
							}
						}
					}
					else if (text == "THROWEXCEPTIONS")
					{
						base.LogFactory.ThrowExceptions = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, base.LogFactory.ThrowExceptions);
						continue;
					}
				}
				else if (num <= 3093534028U)
				{
					if (num != 2250104299U)
					{
						if (num != 2312586692U)
						{
							if (num == 3093534028U)
							{
								if (text == "INTERNALLOGTOCONSOLE")
								{
									InternalLogger.LogToConsole = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, InternalLogger.LogToConsole);
									continue;
								}
							}
						}
						else if (text == "INTERNALLOGTOTRACE")
						{
							InternalLogger.LogToTrace = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, InternalLogger.LogToTrace);
							continue;
						}
					}
					else if (text == "AUTORELOAD")
					{
						continue;
					}
				}
				else if (num <= 3509751930U)
				{
					if (num != 3318309041U)
					{
						if (num == 3509751930U)
						{
							if (text == "INTERNALLOGTOCONSOLEERROR")
							{
								InternalLogger.LogToConsoleError = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, InternalLogger.LogToConsoleError);
								continue;
							}
						}
					}
					else if (text == "KEEPVARIABLESONRELOAD")
					{
						base.LogFactory.KeepVariablesOnReload = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, base.LogFactory.KeepVariablesOnReload);
						continue;
					}
				}
				else if (num != 3521590841U)
				{
					if (num == 4052264974U)
					{
						if (text == "USEINVARIANTCULTURE")
						{
							if (LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, false))
							{
								base.DefaultCultureInfo = CultureInfo.InvariantCulture;
								continue;
							}
							continue;
						}
					}
				}
				else if (text == "GLOBALTHRESHOLD")
				{
					base.LogFactory.GlobalThreshold = LoggingConfigurationParser.ParseLogLevelSafe(keyValuePair.Key, keyValuePair.Value, base.LogFactory.GlobalThreshold);
					continue;
				}
				InternalLogger.Debug<string, string>("Skipping unknown 'NLog' property {0}={1}", keyValuePair.Key, keyValuePair.Value);
			}
			if (!flag2 && !InternalLogger.HasActiveLoggers())
			{
				InternalLogger.LogLevel = LogLevel.Off;
			}
			this._configurationItemFactory = ConfigurationItemFactory.Default;
			this._configurationItemFactory.ParseMessageTemplates = flag;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000301B4 File Offset: 0x0002E3B4
		private static IList<KeyValuePair<string, string>> CreateUniqueSortedListFromConfig(ILoggingConfigurationElement nlogConfig)
		{
			LoggingConfigurationParser.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<string, string> keyValuePair in nlogConfig.Values)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key))
				{
					string text = keyValuePair.Key.Trim();
					if (CS$<>8__locals1.dict.ContainsKey(text))
					{
						InternalLogger.Debug<string, string>("Skipping duplicate value for 'NLog' property {0}. Value={1}", keyValuePair.Key, CS$<>8__locals1.dict[text]);
					}
					CS$<>8__locals1.dict[text] = keyValuePair.Value;
				}
			}
			CS$<>8__locals1.sortedList = new List<KeyValuePair<string, string>>(CS$<>8__locals1.dict.Count);
			LoggingConfigurationParser.<CreateUniqueSortedListFromConfig>g__AddHighPrioritySetting|4_0("ThrowExceptions", ref CS$<>8__locals1);
			LoggingConfigurationParser.<CreateUniqueSortedListFromConfig>g__AddHighPrioritySetting|4_0("ThrowConfigExceptions", ref CS$<>8__locals1);
			LoggingConfigurationParser.<CreateUniqueSortedListFromConfig>g__AddHighPrioritySetting|4_0("InternalLogLevel", ref CS$<>8__locals1);
			LoggingConfigurationParser.<CreateUniqueSortedListFromConfig>g__AddHighPrioritySetting|4_0("InternalLogFile", ref CS$<>8__locals1);
			LoggingConfigurationParser.<CreateUniqueSortedListFromConfig>g__AddHighPrioritySetting|4_0("InternalLogToConsole", ref CS$<>8__locals1);
			foreach (KeyValuePair<string, string> keyValuePair2 in CS$<>8__locals1.dict)
			{
				CS$<>8__locals1.sortedList.Add(keyValuePair2);
			}
			return CS$<>8__locals1.sortedList;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00030308 File Offset: 0x0002E508
		private static string ExpandFilePathVariables(string internalLogFile)
		{
			string text4;
			try
			{
				string text;
				if (LoggingConfigurationParser.ContainsSubStringIgnoreCase(internalLogFile, "${currentdir}", out text))
				{
					internalLogFile = internalLogFile.Replace(text, Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString());
				}
				string text2;
				if (LoggingConfigurationParser.ContainsSubStringIgnoreCase(internalLogFile, "${basedir}", out text2))
				{
					internalLogFile = internalLogFile.Replace(text2, LogFactory.CurrentAppDomain.BaseDirectory + Path.DirectorySeparatorChar.ToString());
				}
				string text3;
				if (LoggingConfigurationParser.ContainsSubStringIgnoreCase(internalLogFile, "${tempdir}", out text3))
				{
					internalLogFile = internalLogFile.Replace(text3, Path.GetTempPath() + Path.DirectorySeparatorChar.ToString());
				}
				if (internalLogFile.IndexOf("%", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					internalLogFile = Environment.ExpandEnvironmentVariables(internalLogFile);
				}
				text4 = internalLogFile;
			}
			catch
			{
				text4 = internalLogFile;
			}
			return text4;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000303DC File Offset: 0x0002E5DC
		private static bool ContainsSubStringIgnoreCase(string haystack, string needle, out string result)
		{
			int num = haystack.IndexOf(needle, StringComparison.OrdinalIgnoreCase);
			result = ((num >= 0) ? haystack.Substring(num, needle.Length) : null);
			return result != null;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00030410 File Offset: 0x0002E610
		private static LogLevel ParseLogLevelSafe(string attributeName, string attributeValue, LogLevel @default)
		{
			LogLevel logLevel;
			try
			{
				logLevel = LogLevel.FromString((attributeValue != null) ? attributeValue.Trim() : null);
			}
			catch (Exception ex)
			{
				if (new NLogConfigurationException(ex, "attribute '{0}': '{1}' isn't valid LogLevel. {2} will be used.", new object[] { attributeName, attributeValue, @default }).MustBeRethrown())
				{
					throw;
				}
				logLevel = @default;
			}
			return logLevel;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0003046C File Offset: 0x0002E66C
		protected virtual bool ParseNLogSection(ILoggingConfigurationElement configSection)
		{
			string name = configSection.Name;
			string text = ((name != null) ? name.Trim().ToUpperInvariant() : null);
			if (text == "TIME")
			{
				this.ParseTimeElement(configSection);
				return true;
			}
			if (text == "VARIABLE")
			{
				this.ParseVariableElement(configSection);
				return true;
			}
			if (text == "VARIABLES")
			{
				this.ParseVariablesElement(configSection);
				return true;
			}
			if (!(text == "APPENDERS") && !(text == "TARGETS"))
			{
				return false;
			}
			this.ParseTargetsElement(configSection);
			return true;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000304FC File Offset: 0x0002E6FC
		private void ParseExtensionsElement(ILoggingConfigurationElement extensionsElement, string baseDirectory)
		{
			extensionsElement.AssertName(new string[] { "extensions" });
			foreach (ILoggingConfigurationElement loggingConfigurationElement in extensionsElement.Children)
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				foreach (KeyValuePair<string, string> keyValuePair in loggingConfigurationElement.Values)
				{
					if (LoggingConfigurationParser.MatchesName(keyValuePair.Key, "prefix"))
					{
						text = keyValuePair.Value + ".";
					}
					else if (LoggingConfigurationParser.MatchesName(keyValuePair.Key, "type"))
					{
						text2 = keyValuePair.Value;
					}
					else if (LoggingConfigurationParser.MatchesName(keyValuePair.Key, "assemblyFile"))
					{
						text3 = keyValuePair.Value;
					}
					else if (LoggingConfigurationParser.MatchesName(keyValuePair.Key, "assembly"))
					{
						text4 = keyValuePair.Value;
					}
					else
					{
						InternalLogger.Debug<string, string, string>("Skipping unknown property {0} for element {1} in section {2}", keyValuePair.Key, loggingConfigurationElement.Name, extensionsElement.Name);
					}
				}
				if (!StringHelpers.IsNullOrWhiteSpace(text2))
				{
					this.RegisterExtension(text2, text);
				}
				if (!StringHelpers.IsNullOrWhiteSpace(text3))
				{
					this.ParseExtensionWithAssemblyFile(baseDirectory, text3, text);
				}
				else if (!StringHelpers.IsNullOrWhiteSpace(text4))
				{
					this.ParseExtensionWithAssembly(text4, text);
				}
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00030694 File Offset: 0x0002E894
		private void RegisterExtension(string type, string itemNamePrefix)
		{
			try
			{
				this._configurationItemFactory.RegisterType(Type.GetType(type, true), itemNamePrefix);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Error(ex, "Error loading extensions.");
				NLogConfigurationException ex2 = new NLogConfigurationException("Error loading extensions: " + type, ex);
				if (ex2.MustBeRethrown())
				{
					throw ex2;
				}
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000306FC File Offset: 0x0002E8FC
		private void ParseExtensionWithAssemblyFile(string baseDirectory, string assemblyFile, string prefix)
		{
			try
			{
				Assembly assembly = AssemblyHelpers.LoadFromPath(assemblyFile, baseDirectory);
				this._configurationItemFactory.RegisterItemsFromAssembly(assembly, prefix);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Error(ex, "Error loading extensions.");
				NLogConfigurationException ex2 = new NLogConfigurationException("Error loading extensions: " + assemblyFile, ex);
				if (ex2.MustBeRethrown())
				{
					throw ex2;
				}
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00030764 File Offset: 0x0002E964
		private void ParseExtensionWithAssembly(string assemblyName, string prefix)
		{
			try
			{
				Assembly assembly = AssemblyHelpers.LoadFromName(assemblyName);
				this._configurationItemFactory.RegisterItemsFromAssembly(assembly, prefix);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Error(ex, "Error loading extensions.");
				NLogConfigurationException ex2 = new NLogConfigurationException("Error loading extensions: " + assemblyName, ex);
				if (ex2.MustBeRethrown())
				{
					throw ex2;
				}
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000307CC File Offset: 0x0002E9CC
		private void ParseVariableElement(ILoggingConfigurationElement variableElement)
		{
			string text = null;
			string text2 = null;
			foreach (KeyValuePair<string, string> keyValuePair in variableElement.Values)
			{
				if (LoggingConfigurationParser.MatchesName(keyValuePair.Key, "name"))
				{
					text = keyValuePair.Value;
				}
				else if (LoggingConfigurationParser.MatchesName(keyValuePair.Key, "value"))
				{
					text2 = keyValuePair.Value;
				}
				else
				{
					InternalLogger.Debug<string, string, string>("Skipping unknown property {0} for element {1} in section {2}", keyValuePair.Key, variableElement.Name, "variables");
				}
			}
			if (!LoggingConfigurationParser.AssertNonEmptyValue(text, "name", variableElement.Name, "variables"))
			{
				return;
			}
			if (!LoggingConfigurationParser.AssertNotNullValue(text2, "value", variableElement.Name, "variables"))
			{
				return;
			}
			string text3 = base.ExpandSimpleVariables(text2);
			base.Variables[text] = text3;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000308BC File Offset: 0x0002EABC
		private void ParseVariablesElement(ILoggingConfigurationElement variableElement)
		{
			variableElement.AssertName(new string[] { "variables" });
			foreach (ILoggingConfigurationElement loggingConfigurationElement in variableElement.Children)
			{
				this.ParseVariableElement(loggingConfigurationElement);
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00030920 File Offset: 0x0002EB20
		private void ParseTimeElement(ILoggingConfigurationElement timeElement)
		{
			timeElement.AssertName(new string[] { "time" });
			string text = null;
			foreach (KeyValuePair<string, string> keyValuePair in timeElement.Values)
			{
				if (LoggingConfigurationParser.MatchesName(keyValuePair.Key, "type"))
				{
					text = keyValuePair.Value;
				}
				else
				{
					InternalLogger.Debug<string, string, string>("Skipping unknown property {0} for element {1} in section {2}", keyValuePair.Key, timeElement.Name, timeElement.Name);
				}
			}
			if (!LoggingConfigurationParser.AssertNonEmptyValue(text, "type", timeElement.Name, string.Empty))
			{
				return;
			}
			TimeSource timeSource = this._configurationItemFactory.TimeSources.CreateInstance(text);
			this.ConfigureObjectFromAttributes(timeSource, timeElement, true);
			InternalLogger.Info<TimeSource>("Selecting time source {0}", timeSource);
			TimeSource.Current = timeSource;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000309FC File Offset: 0x0002EBFC
		[ContractAnnotation("value:notnull => true")]
		private static bool AssertNotNullValue(string value, string propertyName, string elementName, string sectionName)
		{
			return value != null || LoggingConfigurationParser.AssertNonEmptyValue(string.Empty, propertyName, elementName, sectionName);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00030A10 File Offset: 0x0002EC10
		[ContractAnnotation("value:null => false")]
		private static bool AssertNonEmptyValue(string value, string propertyName, string elementName, string sectionName)
		{
			if (!StringHelpers.IsNullOrWhiteSpace(value))
			{
				return true;
			}
			if (LogManager.ThrowConfigExceptions ?? LogManager.ThrowExceptions)
			{
				throw new NLogConfigurationException(string.Concat(new string[] { "Expected property ", propertyName, " on element name: ", elementName, " in section: ", sectionName }));
			}
			InternalLogger.Warn<string, string, string>("Skipping element name: {0} in section: {1} because property {2} is blank", elementName, sectionName, propertyName);
			return false;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00030A8C File Offset: 0x0002EC8C
		private void ParseRulesElement(ILoggingConfigurationElement rulesElement, IList<LoggingRule> rulesCollection)
		{
			InternalLogger.Trace("ParseRulesElement");
			rulesElement.AssertName(new string[] { "rules" });
			foreach (ILoggingConfigurationElement loggingConfigurationElement in rulesElement.Children)
			{
				LoggingRule loggingRule = this.ParseRuleElement(loggingConfigurationElement);
				if (loggingRule != null)
				{
					lock (rulesCollection)
					{
						rulesCollection.Add(loggingRule);
					}
				}
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00030B28 File Offset: 0x0002ED28
		private LogLevel LogLevelFromString(string text)
		{
			return LogLevel.FromString(base.ExpandSimpleVariables(text));
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00030B38 File Offset: 0x0002ED38
		private LoggingRule ParseRuleElement(ILoggingConfigurationElement loggerElement)
		{
			IEnumerable<LogLevel> enumerable = null;
			int num = 0;
			int num2 = LogLevel.MaxLevel.Ordinal;
			string text = null;
			string text2 = null;
			bool flag = true;
			bool flag2 = false;
			string text3 = null;
			foreach (KeyValuePair<string, string> keyValuePair in loggerElement.Values)
			{
				string key = keyValuePair.Key;
				string text4 = ((key != null) ? key.Trim().ToUpperInvariant() : null);
				uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text4);
				if (num3 <= 2092745706U)
				{
					if (num3 <= 1138186663U)
					{
						if (num3 != 1055981173U)
						{
							if (num3 == 1138186663U)
							{
								if (text4 == "MINLEVEL")
								{
									num = this.LogLevelFromString(keyValuePair.Value).Ordinal;
									continue;
								}
							}
						}
						else if (text4 == "LOGGER")
						{
							text2 = keyValuePair.Value;
							continue;
						}
					}
					else if (num3 != 1387956774U)
					{
						if (num3 != 1743562261U)
						{
							if (num3 == 2092745706U)
							{
								if (text4 == "LEVELS")
								{
									enumerable = keyValuePair.Value.SplitAndTrimTokens(',').Select(new Func<string, LogLevel>(this.LogLevelFromString));
									continue;
								}
							}
						}
						else if (text4 == "WRITETO")
						{
							text3 = keyValuePair.Value;
							continue;
						}
					}
					else if (text4 == "NAME")
					{
						if (loggerElement.MatchesName("logger"))
						{
							text2 = keyValuePair.Value;
							continue;
						}
						text = keyValuePair.Value;
						continue;
					}
				}
				else if (num3 <= 2294480894U)
				{
					if (num3 != 2129446653U)
					{
						if (num3 != 2243211505U)
						{
							if (num3 == 2294480894U)
							{
								if (text4 == "ENABLED")
								{
									flag = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, true);
									continue;
								}
							}
						}
						else if (text4 == "MAXLEVEL")
						{
							num2 = this.LogLevelFromString(keyValuePair.Value).Ordinal;
							continue;
						}
					}
					else if (text4 == "LEVEL")
					{
						enumerable = new LogLevel[] { this.LogLevelFromString(keyValuePair.Value) };
						continue;
					}
				}
				else if (num3 != 2571808600U)
				{
					if (num3 != 3085611424U)
					{
						if (num3 == 3187958807U)
						{
							if (text4 == "FINAL")
							{
								flag2 = LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, false);
								continue;
							}
						}
					}
					else if (text4 == "APPENDTO")
					{
						text3 = keyValuePair.Value;
						continue;
					}
				}
				else if (text4 == "RULENAME")
				{
					text = keyValuePair.Value;
					continue;
				}
				InternalLogger.Debug<string, string, string>("Skipping unknown property {0} for element {1} in section {2}", keyValuePair.Key, loggerElement.Name, "rules");
			}
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2) && string.IsNullOrEmpty(text3) && !flag2)
			{
				InternalLogger.Debug("Logging rule without name or filter or targets is ignored");
				return null;
			}
			text2 = text2 ?? "*";
			if (!flag)
			{
				InternalLogger.Debug<string, string>("Logging rule {0} with filter `{1}` is disabled", text, text2);
				return null;
			}
			LoggingRule loggingRule = new LoggingRule(text)
			{
				LoggerNamePattern = text2
			};
			this.ParseLoggingRuleTargets(text3, loggingRule);
			loggingRule.Final = flag2;
			LoggingConfigurationParser.EnableLevelsForRule(loggingRule, enumerable, num, num2);
			this.ParseLoggingRuleChildren(loggerElement, loggingRule);
			return loggingRule;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00030F08 File Offset: 0x0002F108
		private static void EnableLevelsForRule(LoggingRule rule, IEnumerable<LogLevel> enableLevels, int minLevel, int maxLevel)
		{
			if (enableLevels != null)
			{
				using (IEnumerator<LogLevel> enumerator = enableLevels.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LogLevel logLevel = enumerator.Current;
						rule.EnableLoggingForLevel(logLevel);
					}
					return;
				}
			}
			for (int i = minLevel; i <= maxLevel; i++)
			{
				rule.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00030F6C File Offset: 0x0002F16C
		private void ParseLoggingRuleTargets(string writeTargets, LoggingRule rule)
		{
			if (string.IsNullOrEmpty(writeTargets))
			{
				return;
			}
			foreach (string text in writeTargets.SplitAndTrimTokens(','))
			{
				Target target = base.FindTargetByName(text);
				if (target == null)
				{
					throw new NLogConfigurationException(string.Concat(new string[]
					{
						"Target '",
						text,
						"' not found for logging rule: ",
						string.IsNullOrEmpty(rule.RuleName) ? rule.LoggerNamePattern : rule.RuleName,
						"."
					}));
				}
				rule.Targets.Add(target);
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00031004 File Offset: 0x0002F204
		private void ParseLoggingRuleChildren(ILoggingConfigurationElement loggerElement, LoggingRule rule)
		{
			foreach (ILoggingConfigurationElement loggingConfigurationElement in loggerElement.Children)
			{
				LoggingRule loggingRule = null;
				if (loggingConfigurationElement.MatchesName("filters"))
				{
					this.ParseFilters(rule, loggingConfigurationElement);
				}
				else if (loggingConfigurationElement.MatchesName("logger") && loggerElement.MatchesName("logger"))
				{
					loggingRule = this.ParseRuleElement(loggingConfigurationElement);
				}
				else if (loggingConfigurationElement.MatchesName("rule") && loggerElement.MatchesName("rule"))
				{
					loggingRule = this.ParseRuleElement(loggingConfigurationElement);
				}
				else
				{
					InternalLogger.Debug<string, string, string>("Skipping unknown child {0} for element {1} in section {2}", loggingConfigurationElement.Name, loggerElement.Name, "rules");
				}
				if (loggingRule != null)
				{
					IList<LoggingRule> childRules = rule.ChildRules;
					lock (childRules)
					{
						rule.ChildRules.Add(loggingRule);
					}
				}
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00031108 File Offset: 0x0002F308
		private void ParseFilters(LoggingRule rule, ILoggingConfigurationElement filtersElement)
		{
			filtersElement.AssertName(new string[] { "filters" });
			string optionalValue = filtersElement.GetOptionalValue("defaultAction", null);
			if (optionalValue != null)
			{
				PropertyHelper.SetPropertyFromString(rule, "DefaultFilterResult", optionalValue, this._configurationItemFactory);
			}
			foreach (ILoggingConfigurationElement loggingConfigurationElement in filtersElement.Children)
			{
				string name = loggingConfigurationElement.Name;
				Filter filter = this._configurationItemFactory.Filters.CreateInstance(name);
				this.ConfigureObjectFromAttributes(filter, loggingConfigurationElement, false);
				rule.Filters.Add(filter);
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000311B8 File Offset: 0x0002F3B8
		private void ParseTargetsElement(ILoggingConfigurationElement targetsElement)
		{
			targetsElement.AssertName(new string[] { "targets", "appenders" });
			KeyValuePair<string, string> keyValuePair = targetsElement.Values.FirstOrDefault((KeyValuePair<string, string> configItem) => LoggingConfigurationParser.MatchesName(configItem.Key, "async"));
			bool flag = !string.IsNullOrEmpty(keyValuePair.Value) && LoggingConfigurationParser.ParseBooleanValue(keyValuePair.Key, keyValuePair.Value, false);
			ILoggingConfigurationElement loggingConfigurationElement = null;
			Dictionary<string, ILoggingConfigurationElement> dictionary = new Dictionary<string, ILoggingConfigurationElement>(StringComparer.OrdinalIgnoreCase);
			foreach (ILoggingConfigurationElement loggingConfigurationElement2 in targetsElement.Children)
			{
				string configItemTypeAttribute = LoggingConfigurationParser.GetConfigItemTypeAttribute(loggingConfigurationElement2, null);
				string text = loggingConfigurationElement2.GetOptionalValue("name", null);
				Target target = null;
				if (!string.IsNullOrEmpty(text))
				{
					text = loggingConfigurationElement2.Name + "(Name=" + text + ")";
				}
				else
				{
					text = loggingConfigurationElement2.Name;
				}
				string name = loggingConfigurationElement2.Name;
				string text2 = ((name != null) ? name.Trim().ToUpperInvariant() : null);
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1970515547U)
				{
					if (num != 934512166U)
					{
						if (num != 1924432750U)
						{
							if (num != 1970515547U)
							{
								goto IL_0262;
							}
							if (!(text2 == "DEFAULT-TARGET-PARAMETERS"))
							{
								goto IL_0262;
							}
							if (LoggingConfigurationParser.AssertNonEmptyValue(configItemTypeAttribute, "type", text, targetsElement.Name))
							{
								this.ParseDefaultTargetParameters(loggingConfigurationElement2, configItemTypeAttribute, dictionary);
							}
						}
						else
						{
							if (!(text2 == "WRAPPER-TARGET"))
							{
								goto IL_0262;
							}
							goto IL_022B;
						}
					}
					else
					{
						if (!(text2 == "COMPOUND-TARGET"))
						{
							goto IL_0262;
						}
						goto IL_022B;
					}
				}
				else if (num <= 3098859044U)
				{
					if (num != 2730440272U)
					{
						if (num != 3098859044U)
						{
							goto IL_0262;
						}
						if (!(text2 == "WRAPPER"))
						{
							goto IL_0262;
						}
						goto IL_022B;
					}
					else
					{
						if (!(text2 == "DEFAULT-WRAPPER"))
						{
							goto IL_0262;
						}
						if (LoggingConfigurationParser.AssertNonEmptyValue(configItemTypeAttribute, "type", text, targetsElement.Name))
						{
							loggingConfigurationElement = loggingConfigurationElement2;
						}
					}
				}
				else if (num != 3535797018U)
				{
					if (num != 4080126536U)
					{
						goto IL_0262;
					}
					if (!(text2 == "TARGET"))
					{
						goto IL_0262;
					}
					goto IL_022B;
				}
				else
				{
					if (!(text2 == "APPENDER"))
					{
						goto IL_0262;
					}
					goto IL_022B;
				}
				IL_0274:
				if (target != null)
				{
					if (flag)
					{
						target = LoggingConfigurationParser.WrapWithAsyncTargetWrapper(target);
					}
					if (loggingConfigurationElement != null)
					{
						target = this.WrapWithDefaultWrapper(target, loggingConfigurationElement);
					}
					InternalLogger.Info<string, string>("Adding target {0}(Name={1})", target.GetType().Name, target.Name);
					base.AddTarget(target.Name, target);
					continue;
				}
				continue;
				IL_022B:
				if (LoggingConfigurationParser.AssertNonEmptyValue(configItemTypeAttribute, "type", text, targetsElement.Name))
				{
					target = this._configurationItemFactory.Targets.CreateInstance(configItemTypeAttribute);
					this.ParseTargetElement(target, loggingConfigurationElement2, dictionary);
					goto IL_0274;
				}
				goto IL_0274;
				IL_0262:
				InternalLogger.Debug<string, string>("Skipping unknown element {0} in section {1}", text, targetsElement.Name);
				goto IL_0274;
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000314BC File Offset: 0x0002F6BC
		private void ParseDefaultTargetParameters(ILoggingConfigurationElement defaultTargetElement, string targetType, Dictionary<string, ILoggingConfigurationElement> typeNameToDefaultTargetParameters)
		{
			typeNameToDefaultTargetParameters[targetType.Trim()] = defaultTargetElement;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000314CC File Offset: 0x0002F6CC
		private void ParseTargetElement(Target target, ILoggingConfigurationElement targetElement, Dictionary<string, ILoggingConfigurationElement> typeNameToDefaultTargetParameters = null)
		{
			string configItemTypeAttribute = LoggingConfigurationParser.GetConfigItemTypeAttribute(targetElement, "targets");
			ILoggingConfigurationElement loggingConfigurationElement;
			if (typeNameToDefaultTargetParameters != null && typeNameToDefaultTargetParameters.TryGetValue(configItemTypeAttribute, out loggingConfigurationElement))
			{
				this.ParseTargetElement(target, loggingConfigurationElement, null);
			}
			CompoundTargetBase compoundTargetBase = target as CompoundTargetBase;
			WrapperTargetBase wrapperTargetBase = target as WrapperTargetBase;
			this.ConfigureObjectFromAttributes(target, targetElement, true);
			foreach (ILoggingConfigurationElement loggingConfigurationElement2 in targetElement.Children)
			{
				string name = loggingConfigurationElement2.Name;
				if ((compoundTargetBase == null || !this.ParseCompoundTarget(typeNameToDefaultTargetParameters, name, loggingConfigurationElement2, compoundTargetBase, null)) && (wrapperTargetBase == null || !this.ParseTargetWrapper(typeNameToDefaultTargetParameters, name, loggingConfigurationElement2, wrapperTargetBase)))
				{
					this.SetPropertyFromElement(target, loggingConfigurationElement2);
				}
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00031588 File Offset: 0x0002F788
		private bool ParseTargetWrapper(Dictionary<string, ILoggingConfigurationElement> typeNameToDefaultTargetParameters, string name, ILoggingConfigurationElement childElement, WrapperTargetBase wrapper)
		{
			if (LoggingConfigurationParser.IsTargetRefElement(name))
			{
				string requiredValue = childElement.GetRequiredValue("name", LoggingConfigurationParser.GetName(wrapper));
				Target target = base.FindTargetByName(requiredValue);
				if (target == null)
				{
					throw new NLogConfigurationException("Referenced target '" + requiredValue + "' not found.");
				}
				wrapper.WrappedTarget = target;
				return true;
			}
			else
			{
				if (LoggingConfigurationParser.IsTargetElement(name))
				{
					string configItemTypeAttribute = LoggingConfigurationParser.GetConfigItemTypeAttribute(childElement, LoggingConfigurationParser.GetName(wrapper));
					Target target2 = this._configurationItemFactory.Targets.CreateInstance(configItemTypeAttribute);
					if (target2 != null)
					{
						this.ParseTargetElement(target2, childElement, typeNameToDefaultTargetParameters);
						if (target2.Name != null)
						{
							base.AddTarget(target2.Name, target2);
						}
						if (wrapper.WrappedTarget != null)
						{
							throw new NLogConfigurationException("Wrapped target already defined.");
						}
						wrapper.WrappedTarget = target2;
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00031643 File Offset: 0x0002F843
		private static string GetConfigItemTypeAttribute(ILoggingConfigurationElement childElement, string sectionNameForRequiredValue = null)
		{
			return LoggingConfigurationParser.StripOptionalNamespacePrefix((sectionNameForRequiredValue != null) ? childElement.GetRequiredValue("type", sectionNameForRequiredValue) : childElement.GetOptionalValue("type", null));
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00031668 File Offset: 0x0002F868
		private bool ParseCompoundTarget(Dictionary<string, ILoggingConfigurationElement> typeNameToDefaultTargetParameters, string name, ILoggingConfigurationElement childElement, CompoundTargetBase compound, string targetName)
		{
			if (LoggingConfigurationParser.MatchesName(name, "targets") || LoggingConfigurationParser.MatchesName(name, "appenders"))
			{
				foreach (ILoggingConfigurationElement loggingConfigurationElement in childElement.Children)
				{
					this.ParseCompoundTarget(typeNameToDefaultTargetParameters, loggingConfigurationElement.Name, loggingConfigurationElement, compound, null);
				}
				return true;
			}
			if (LoggingConfigurationParser.IsTargetRefElement(name))
			{
				targetName = childElement.GetRequiredValue("name", LoggingConfigurationParser.GetName(compound));
				Target target = base.FindTargetByName(targetName);
				if (target == null)
				{
					throw new NLogConfigurationException("Referenced target '" + targetName + "' not found.");
				}
				compound.Targets.Add(target);
				return true;
			}
			else
			{
				if (LoggingConfigurationParser.IsTargetElement(name))
				{
					string configItemTypeAttribute = LoggingConfigurationParser.GetConfigItemTypeAttribute(childElement, LoggingConfigurationParser.GetName(compound));
					Target target2 = this._configurationItemFactory.Targets.CreateInstance(configItemTypeAttribute);
					if (target2 != null)
					{
						if (targetName != null)
						{
							target2.Name = targetName;
						}
						this.ParseTargetElement(target2, childElement, typeNameToDefaultTargetParameters);
						if (target2.Name != null)
						{
							base.AddTarget(target2.Name, target2);
						}
						compound.Targets.Add(target2);
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00031798 File Offset: 0x0002F998
		private void ConfigureObjectFromAttributes(object targetObject, ILoggingConfigurationElement element, bool ignoreType)
		{
			foreach (KeyValuePair<string, string> keyValuePair in element.Values)
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (!ignoreType || !LoggingConfigurationParser.MatchesName(key, "type"))
				{
					try
					{
						PropertyHelper.SetPropertyFromString(targetObject, key, base.ExpandSimpleVariables(value), this._configurationItemFactory);
					}
					catch (Exception ex)
					{
						InternalLogger.Warn(ex, "Error when setting '{0}' on attibute '{1}'", new object[] { value, key });
						throw;
					}
				}
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0003183C File Offset: 0x0002FA3C
		private void SetPropertyFromElement(object o, ILoggingConfigurationElement element)
		{
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(o, element.Name, out propertyInfo))
			{
				return;
			}
			if (this.AddArrayItemFromElement(o, propertyInfo, element))
			{
				return;
			}
			if (this.SetLayoutFromElement(o, propertyInfo, element))
			{
				return;
			}
			if (this.SetFilterFromElement(o, propertyInfo, element))
			{
				return;
			}
			this.SetItemFromElement(o, propertyInfo, element);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00031888 File Offset: 0x0002FA88
		private bool AddArrayItemFromElement(object o, PropertyInfo propInfo, ILoggingConfigurationElement element)
		{
			Type arrayItemType = PropertyHelper.GetArrayItemType(propInfo);
			if (arrayItemType != null)
			{
				IList list = (IList)propInfo.GetValue(o, null);
				if (string.Equals(propInfo.Name, element.Name, StringComparison.OrdinalIgnoreCase))
				{
					List<ILoggingConfigurationElement> list2 = element.Children.ToList<ILoggingConfigurationElement>();
					if (list2.Count > 0)
					{
						foreach (ILoggingConfigurationElement loggingConfigurationElement in list2)
						{
							list.Add(this.ParseArrayItemFromElement(arrayItemType, loggingConfigurationElement));
						}
						return true;
					}
				}
				object obj = this.ParseArrayItemFromElement(arrayItemType, element);
				list.Add(obj);
				return true;
			}
			return false;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00031944 File Offset: 0x0002FB44
		private object ParseArrayItemFromElement(Type elementType, ILoggingConfigurationElement element)
		{
			object obj = this.TryCreateLayoutInstance(element, elementType);
			if (obj == null)
			{
				obj = FactoryHelper.CreateInstance(elementType);
			}
			this.ConfigureObjectFromAttributes(obj, element, true);
			this.ConfigureObjectFromElement(obj, element);
			return obj;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00031978 File Offset: 0x0002FB78
		private bool SetLayoutFromElement(object o, PropertyInfo propInfo, ILoggingConfigurationElement element)
		{
			Layout layout = this.TryCreateLayoutInstance(element, propInfo.PropertyType);
			if (layout != null)
			{
				this.SetItemOnProperty(o, propInfo, element, layout);
				return true;
			}
			return false;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000319A4 File Offset: 0x0002FBA4
		private bool SetFilterFromElement(object o, PropertyInfo propInfo, ILoggingConfigurationElement element)
		{
			Type propertyType = propInfo.PropertyType;
			Filter filter = this.TryCreateFilterInstance(element, propertyType);
			if (filter != null)
			{
				this.SetItemOnProperty(o, propInfo, element, filter);
				return true;
			}
			return false;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000319D1 File Offset: 0x0002FBD1
		private Layout TryCreateLayoutInstance(ILoggingConfigurationElement element, Type type)
		{
			return this.TryCreateInstance<Layout>(element, type, this._configurationItemFactory.Layouts);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000319E6 File Offset: 0x0002FBE6
		private Filter TryCreateFilterInstance(ILoggingConfigurationElement element, Type type)
		{
			return this.TryCreateInstance<Filter>(element, type, this._configurationItemFactory.Filters);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000319FC File Offset: 0x0002FBFC
		private T TryCreateInstance<T>(ILoggingConfigurationElement element, Type type, INamedItemFactory<T, Type> factory) where T : class
		{
			if (!LoggingConfigurationParser.IsAssignableFrom<T>(type))
			{
				return default(T);
			}
			string configItemTypeAttribute = LoggingConfigurationParser.GetConfigItemTypeAttribute(element, null);
			if (configItemTypeAttribute == null)
			{
				return default(T);
			}
			return factory.CreateInstance(base.ExpandSimpleVariables(configItemTypeAttribute));
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00031A3D File Offset: 0x0002FC3D
		private static bool IsAssignableFrom<T>(Type type)
		{
			return typeof(T).IsAssignableFrom(type);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00031A4F File Offset: 0x0002FC4F
		private void SetItemOnProperty(object o, PropertyInfo propInfo, ILoggingConfigurationElement element, object properyValue)
		{
			this.ConfigureObjectFromAttributes(properyValue, element, true);
			this.ConfigureObjectFromElement(properyValue, element);
			propInfo.SetValue(o, properyValue, null);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00031A70 File Offset: 0x0002FC70
		private void SetItemFromElement(object o, PropertyInfo propInfo, ILoggingConfigurationElement element)
		{
			object value = propInfo.GetValue(o, null);
			this.ConfigureObjectFromAttributes(value, element, true);
			this.ConfigureObjectFromElement(value, element);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00031A98 File Offset: 0x0002FC98
		private void ConfigureObjectFromElement(object targetObject, ILoggingConfigurationElement element)
		{
			foreach (ILoggingConfigurationElement loggingConfigurationElement in element.Children)
			{
				this.SetPropertyFromElement(targetObject, loggingConfigurationElement);
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00031AE8 File Offset: 0x0002FCE8
		private static Target WrapWithAsyncTargetWrapper(Target target)
		{
			AsyncTargetWrapper asyncTargetWrapper = new AsyncTargetWrapper();
			asyncTargetWrapper.WrappedTarget = target;
			asyncTargetWrapper.Name = target.Name;
			target.Name += "_wrapped";
			InternalLogger.Debug<string, string>("Wrapping target '{0}' with AsyncTargetWrapper and renaming to '{1}", asyncTargetWrapper.Name, target.Name);
			target = asyncTargetWrapper;
			return target;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00031B40 File Offset: 0x0002FD40
		private Target WrapWithDefaultWrapper(Target t, ILoggingConfigurationElement defaultParameters)
		{
			string configItemTypeAttribute = LoggingConfigurationParser.GetConfigItemTypeAttribute(defaultParameters, "targets");
			Target target = this._configurationItemFactory.Targets.CreateInstance(configItemTypeAttribute);
			WrapperTargetBase wrapperTargetBase = target as WrapperTargetBase;
			if (wrapperTargetBase == null)
			{
				throw new NLogConfigurationException("Target type specified on <default-wrapper /> is not a wrapper.");
			}
			this.ParseTargetElement(target, defaultParameters, null);
			while (wrapperTargetBase.WrappedTarget != null)
			{
				wrapperTargetBase = wrapperTargetBase.WrappedTarget as WrapperTargetBase;
				if (wrapperTargetBase == null)
				{
					throw new NLogConfigurationException("Child target type specified on <default-wrapper /> is not a wrapper.");
				}
			}
			wrapperTargetBase.WrappedTarget = t;
			target.Name = t.Name;
			t.Name += "_wrapped";
			InternalLogger.Debug<string, string, string>("Wrapping target '{0}' with '{1}' and renaming to '{2}", target.Name, target.GetType().Name, t.Name);
			return target;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00031BF8 File Offset: 0x0002FDF8
		private static bool MatchesName(string key, string expectedKey)
		{
			return string.Equals((key != null) ? key.Trim() : null, expectedKey, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00031C10 File Offset: 0x0002FE10
		private static bool ParseBooleanValue(string propertyName, string value, bool defaultValue)
		{
			bool flag;
			try
			{
				flag = Convert.ToBoolean((value != null) ? value.Trim() : null, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				if (new NLogConfigurationException(ex, "'{0}' hasn't a valid boolean value '{1}'. {2} will be used", new object[] { propertyName, value, defaultValue }).MustBeRethrown())
				{
					throw;
				}
				flag = defaultValue;
			}
			return flag;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00031C78 File Offset: 0x0002FE78
		private static bool IsTargetElement(string name)
		{
			return name.Equals("target", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper-target", StringComparison.OrdinalIgnoreCase) || name.Equals("compound-target", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00031CB2 File Offset: 0x0002FEB2
		private static bool IsTargetRefElement(string name)
		{
			return name.Equals("target-ref", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper-target-ref", StringComparison.OrdinalIgnoreCase) || name.Equals("compound-target-ref", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00031CE0 File Offset: 0x0002FEE0
		private static string StripOptionalNamespacePrefix(string attributeValue)
		{
			if (attributeValue == null)
			{
				return null;
			}
			int num = attributeValue.IndexOf(':');
			if (num < 0)
			{
				return attributeValue;
			}
			return attributeValue.Substring(num + 1);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00031D0A File Offset: 0x0002FF0A
		private static string GetName(Target target)
		{
			if (!string.IsNullOrEmpty(target.Name))
			{
				return target.Name;
			}
			return target.GetType().Name;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00031D2B File Offset: 0x0002FF2B
		[CompilerGenerated]
		internal static void <CreateUniqueSortedListFromConfig>g__AddHighPrioritySetting|4_0(string settingName, ref LoggingConfigurationParser.<>c__DisplayClass4_0 A_1)
		{
			if (A_1.dict.ContainsKey(settingName))
			{
				A_1.sortedList.Add(new KeyValuePair<string, string>(settingName, A_1.dict[settingName]));
				A_1.dict.Remove(settingName);
			}
		}

		// Token: 0x040004EE RID: 1262
		private ConfigurationItemFactory _configurationItemFactory;
	}
}
