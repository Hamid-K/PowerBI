using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A6 RID: 166
	public class ApplicationSwitches : Block, IApplicationSwitches
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x000111D7 File Offset: 0x0000F3D7
		public ApplicationSwitches()
			: this("ApplicationSwitches")
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000111E4 File Offset: 0x0000F3E4
		public ApplicationSwitches(string name)
			: this(name, "", ApplicationSwitchesTypes.AppConfig | ApplicationSwitchesTypes.EnvironmentVariables)
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public ApplicationSwitches(ApplicationSwitchesTypes switchesType)
			: this("", switchesType)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011202 File Offset: 0x0000F402
		public ApplicationSwitches(string[] commandLine, ApplicationSwitchesTypes switchesType)
			: this("ApplicationSwitches", commandLine, switchesType)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00011211 File Offset: 0x0000F411
		public ApplicationSwitches(string commandLine, ApplicationSwitchesTypes switchesType)
			: this("ApplicationSwitches", commandLine, switchesType)
		{
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00011220 File Offset: 0x0000F420
		public ApplicationSwitches(string name, string[] commandLine, ApplicationSwitchesTypes switchesType)
			: base(name)
		{
			this.m_switchesMap = new ApplicationSwitches.SwitchesMap();
			this.m_applicationSwitchesProviders = ApplicationSwitchesProviderFactory.GetApplicationSwitchProviders(switchesType, commandLine);
			this.RegisterHelpSwitch();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00011247 File Offset: 0x0000F447
		public ApplicationSwitches(string name, string commandLine, ApplicationSwitchesTypes switchesType)
			: base(name)
		{
			this.m_switchesMap = new ApplicationSwitches.SwitchesMap();
			this.m_applicationSwitchesProviders = ApplicationSwitchesProviderFactory.GetApplicationSwitchProviders(switchesType, commandLine);
			this.RegisterHelpSwitch();
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00011270 File Offset: 0x0000F470
		public void RegisterSwitch(string switchFullName, string switchShortName, string helpInfo, ParameterType switchType, bool required, string defaultValue)
		{
			ApplicationSwitches.SwitchInfo switchInfo = this.m_switchesMap.RegisterSwitch(switchFullName, switchShortName, helpInfo, switchType, required, defaultValue);
			try
			{
				this.UpdateAndVerifyRequiredArgument(switchInfo);
			}
			catch (ApplicationSwitchesException ex)
			{
				this.AddApplicationSwitchesException(ex);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000112B8 File Offset: 0x0000F4B8
		public void RegisterRequiredSwitch(string switchFullName, string switchShortName, string helpInfo, ParameterType switchType)
		{
			this.RegisterSwitch(switchFullName, switchShortName, helpInfo, switchType, true, null);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000112C7 File Offset: 0x0000F4C7
		public string GetStringSwitch(string name)
		{
			if (!this.IsHelpRequested)
			{
				this.ThrowOnExistingExceptions();
			}
			return this.m_switchesMap[name];
		}

		// Token: 0x170000CE RID: 206
		public string this[string name]
		{
			get
			{
				return this.GetStringSwitch(name);
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000112EC File Offset: 0x0000F4EC
		public bool GetBoolSwitch(string name)
		{
			if (!this.IsHelpRequested)
			{
				this.ThrowOnExistingExceptions();
			}
			return bool.Parse(this[name]);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00011308 File Offset: 0x0000F508
		public string UsageString
		{
			get
			{
				string text = this.m_applicationSwitchesProviders.StringJoin(" OR ", (IApplicationSwitchesProvider provider) => provider.Name);
				return string.Concat(new string[]
				{
					this.m_switchesMap.UsageString,
					Environment.NewLine,
					"(Use ",
					text,
					" for the application switches)"
				});
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001137A File Offset: 0x0000F57A
		public bool IsHelpRequested
		{
			get
			{
				return bool.Parse(this.m_switchesMap["help"]);
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00011391 File Offset: 0x0000F591
		protected override BlockInitializationStatus OnInitialize()
		{
			base.BlockHost.PublishService(this, typeof(IApplicationSwitches), BlockServiceProviderIdentity.Implementation, this);
			return BlockInitializationStatus.Done;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000113AD File Offset: 0x0000F5AD
		private void AddApplicationSwitchesException(ApplicationSwitchesException ex)
		{
			if (this.m_exceptions == null)
			{
				this.m_exceptions = new CompositeApplicationSwitchesException();
			}
			this.m_exceptions.Add(ex);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000113CE File Offset: 0x0000F5CE
		private void ThrowOnExistingExceptions()
		{
			if (this.m_exceptions != null && !this.m_exceptions.IsEmpty())
			{
				throw this.m_exceptions;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000113EC File Offset: 0x0000F5EC
		private void UpdateAndVerifyRequiredArgument(ApplicationSwitches.SwitchInfo switchInfo)
		{
			foreach (IApplicationSwitchesProvider applicationSwitchesProvider in this.m_applicationSwitchesProviders)
			{
				if (switchInfo.Type == ParameterType.String)
				{
					string text = applicationSwitchesProvider[switchInfo.ShortName];
					if (text != null)
					{
						switchInfo.Value = text;
						return;
					}
					text = applicationSwitchesProvider[switchInfo.FullName];
					if (text != null)
					{
						switchInfo.Value = text;
						return;
					}
				}
				else
				{
					bool flag2;
					bool flag = applicationSwitchesProvider.GetBoolSwitch(switchInfo.ShortName, out flag2);
					if (flag2)
					{
						switchInfo.Value = flag;
						return;
					}
					flag = applicationSwitchesProvider.GetBoolSwitch(switchInfo.FullName, out flag2);
					if (flag2)
					{
						switchInfo.Value = flag;
						return;
					}
				}
			}
			if (switchInfo.Required && switchInfo.Type == ParameterType.String)
			{
				MissingRequiredArgumentsException ex = new MissingRequiredArgumentsException(switchInfo.FullName, switchInfo.ShortName);
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { ex.Message });
				throw ex;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00011508 File Offset: 0x0000F708
		private void RegisterHelpSwitch()
		{
			this.RegisterSwitch("help", "?", "Get some help (this string)", ParameterType.Boolean, false, "false");
		}

		// Token: 0x0400019A RID: 410
		private const string c_helpSwitchFullName = "help";

		// Token: 0x0400019B RID: 411
		private const string c_helpSwitchShortName = "?";

		// Token: 0x0400019C RID: 412
		private ICollection<IApplicationSwitchesProvider> m_applicationSwitchesProviders;

		// Token: 0x0400019D RID: 413
		private ApplicationSwitches.SwitchesMap m_switchesMap;

		// Token: 0x0400019E RID: 414
		private CompositeApplicationSwitchesException m_exceptions;

		// Token: 0x020005B7 RID: 1463
		internal class SwitchInfo
		{
			// Token: 0x06002B45 RID: 11077 RVA: 0x0009A0CC File Offset: 0x000982CC
			internal SwitchInfo(string switchFullName, string switchShortName, string helpInfo, ParameterType switchType, bool required, string defaultValue)
			{
				this.m_switchFullName = switchFullName;
				this.m_switchShortName = switchShortName;
				this.m_helpInfo = helpInfo;
				this.m_switchType = switchType;
				this.m_required = required;
				this.m_switchValue = defaultValue;
			}

			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x06002B46 RID: 11078 RVA: 0x0009A101 File Offset: 0x00098301
			internal string FullName
			{
				get
				{
					return this.m_switchFullName;
				}
			}

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x06002B47 RID: 11079 RVA: 0x0009A109 File Offset: 0x00098309
			internal string ShortName
			{
				get
				{
					return this.m_switchShortName;
				}
			}

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x06002B48 RID: 11080 RVA: 0x0009A111 File Offset: 0x00098311
			internal string HelpInformation
			{
				get
				{
					return this.m_helpInfo;
				}
			}

			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x06002B49 RID: 11081 RVA: 0x0009A119 File Offset: 0x00098319
			internal ParameterType Type
			{
				get
				{
					return this.m_switchType;
				}
			}

			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x06002B4A RID: 11082 RVA: 0x0009A121 File Offset: 0x00098321
			internal bool Required
			{
				get
				{
					return this.m_required;
				}
			}

			// Token: 0x170006F5 RID: 1781
			// (get) Token: 0x06002B4B RID: 11083 RVA: 0x0009A129 File Offset: 0x00098329
			internal bool Changed
			{
				get
				{
					return this.m_changed;
				}
			}

			// Token: 0x170006F6 RID: 1782
			// (get) Token: 0x06002B4C RID: 11084 RVA: 0x0009A131 File Offset: 0x00098331
			// (set) Token: 0x06002B4D RID: 11085 RVA: 0x0009A139 File Offset: 0x00098339
			internal object Value
			{
				get
				{
					return this.m_switchValue;
				}
				set
				{
					this.m_switchValue = value;
					this.m_changed = true;
				}
			}

			// Token: 0x04000F87 RID: 3975
			private string m_switchFullName;

			// Token: 0x04000F88 RID: 3976
			private string m_switchShortName;

			// Token: 0x04000F89 RID: 3977
			private string m_helpInfo;

			// Token: 0x04000F8A RID: 3978
			private ParameterType m_switchType;

			// Token: 0x04000F8B RID: 3979
			private bool m_required;

			// Token: 0x04000F8C RID: 3980
			private object m_switchValue;

			// Token: 0x04000F8D RID: 3981
			private bool m_changed;
		}

		// Token: 0x020005B8 RID: 1464
		internal class SwitchesMap
		{
			// Token: 0x06002B4E RID: 11086 RVA: 0x0009A149 File Offset: 0x00098349
			internal SwitchesMap()
			{
				this.m_shortNamesMap = new Dictionary<string, ApplicationSwitches.SwitchInfo>(StringComparer.OrdinalIgnoreCase);
				this.m_fullNamesMap = new Dictionary<string, ApplicationSwitches.SwitchInfo>(StringComparer.OrdinalIgnoreCase);
				this.m_switchMapLocker = new object();
			}

			// Token: 0x06002B4F RID: 11087 RVA: 0x0009A17C File Offset: 0x0009837C
			internal ApplicationSwitches.SwitchInfo RegisterSwitch(string switchFullName, string switchShortName, string helpInfo, ParameterType switchType, bool required, string defaultValue)
			{
				ApplicationSwitches.SwitchInfo switchInfo = null;
				object switchMapLocker = this.m_switchMapLocker;
				lock (switchMapLocker)
				{
					if (this.m_shortNamesMap.ContainsKey(switchShortName))
					{
						switchInfo = this.m_shortNamesMap[switchShortName];
						string fullName = switchInfo.FullName;
						if (fullName != switchFullName)
						{
							TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "Switch '{0}' already registered with a different full name: '{1}'", new object[] { switchShortName, switchFullName });
							throw new ExistingSwitchException(string.Concat(new string[] { "Switch '", switchShortName, "' already registered with a different full name: '", fullName, "'" }));
						}
					}
					else
					{
						switchInfo = new ApplicationSwitches.SwitchInfo(switchFullName, switchShortName, helpInfo, switchType, required, defaultValue);
						this.m_shortNamesMap.Add(switchShortName, switchInfo);
						this.m_fullNamesMap.Add(switchFullName, switchInfo);
					}
				}
				return switchInfo;
			}

			// Token: 0x170006F7 RID: 1783
			internal string this[string name]
			{
				get
				{
					object value = this.GetSwitchInfo(name).Value;
					if (value == null)
					{
						return null;
					}
					return value.ToString();
				}
			}

			// Token: 0x06002B51 RID: 11089 RVA: 0x0009A278 File Offset: 0x00098478
			private ApplicationSwitches.SwitchInfo GetSwitchInfo(string name)
			{
				object switchMapLocker = this.m_switchMapLocker;
				lock (switchMapLocker)
				{
					if (this.m_shortNamesMap.ContainsKey(name))
					{
						return this.m_shortNamesMap[name];
					}
					if (this.m_fullNamesMap.ContainsKey(name))
					{
						return this.m_fullNamesMap[name];
					}
				}
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "Switch '{0}' was not registered", new object[] { name });
				throw new UnregisteredSwitchException("Switch '" + name + "' was not registered");
			}

			// Token: 0x170006F8 RID: 1784
			// (get) Token: 0x06002B52 RID: 11090 RVA: 0x0009A320 File Offset: 0x00098520
			internal string UsageString
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					StringBuilder stringBuilder2 = new StringBuilder();
					object switchMapLocker = this.m_switchMapLocker;
					lock (switchMapLocker)
					{
						foreach (ApplicationSwitches.SwitchInfo switchInfo in this.m_shortNamesMap.Values)
						{
							if (switchInfo.Required)
							{
								stringBuilder.AppendLine(this.GetArgumentUsage(switchInfo));
							}
							else
							{
								stringBuilder2.AppendLine(this.GetArgumentUsage(switchInfo));
							}
						}
					}
					return string.Concat(new string[]
					{
						"Usage: ",
						Path.GetFileName(Environment.GetCommandLineArgs()[0]),
						" <options>",
						Environment.NewLine,
						Environment.NewLine,
						(stringBuilder.Length == 0) ? string.Empty : string.Concat(new string[]
						{
							"Mandatory switches:",
							Environment.NewLine,
							stringBuilder.ToString(),
							Environment.NewLine,
							Environment.NewLine
						}),
						(stringBuilder2.Length == 0) ? string.Empty : ("Optional Arguments:" + Environment.NewLine + stringBuilder2.ToString())
					});
				}
			}

			// Token: 0x06002B53 RID: 11091 RVA: 0x0009A478 File Offset: 0x00098678
			private string GetArgumentUsage(ApplicationSwitches.SwitchInfo switchInfo)
			{
				string text = "";
				if (switchInfo.Type == ParameterType.String)
				{
					text = ":<value>";
				}
				string text2 = Environment.NewLine + new string(' ', 15);
				string text3 = new string(' ', 5);
				string text5;
				if (switchInfo.FullName.StartsWith(switchInfo.ShortName, StringComparison.Ordinal))
				{
					string text4 = switchInfo.FullName.Substring(switchInfo.ShortName.Length);
					text5 = string.Concat(new string[]
					{
						text3,
						"/",
						switchInfo.ShortName,
						"[",
						text4,
						"]",
						text,
						Environment.NewLine
					});
				}
				else
				{
					text5 = string.Concat(new string[]
					{
						text3,
						"/",
						switchInfo.FullName,
						text,
						Environment.NewLine,
						text3,
						"/",
						switchInfo.ShortName,
						text,
						Environment.NewLine
					});
				}
				return text5 + text2 + switchInfo.HelpInformation.Replace(Environment.NewLine, text2).Replace(". ", "." + text2) + Environment.NewLine;
			}

			// Token: 0x04000F8E RID: 3982
			private const int c_leadingSpacesCountHelpInfo = 15;

			// Token: 0x04000F8F RID: 3983
			private const int c_leadingSpacesCountSwitch = 5;

			// Token: 0x04000F90 RID: 3984
			private Dictionary<string, ApplicationSwitches.SwitchInfo> m_shortNamesMap;

			// Token: 0x04000F91 RID: 3985
			private Dictionary<string, ApplicationSwitches.SwitchInfo> m_fullNamesMap;

			// Token: 0x04000F92 RID: 3986
			private object m_switchMapLocker;
		}
	}
}
