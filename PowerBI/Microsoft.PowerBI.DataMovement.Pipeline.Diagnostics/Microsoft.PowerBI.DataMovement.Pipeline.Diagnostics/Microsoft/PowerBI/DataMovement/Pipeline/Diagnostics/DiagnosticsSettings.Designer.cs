using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Configuration;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200000C RID: 12
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class DiagnosticsSettings : ApplicationSettingsBase
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000023C9 File Offset: 0x000005C9
		public static DiagnosticsSettings Default
		{
			get
			{
				return DiagnosticsSettings.defaultInstance;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000023D0 File Offset: 0x000005D0
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("4")]
		public int TracingVerbosity
		{
			get
			{
				return (int)this["TracingVerbosity"];
			}
		}

		// Token: 0x04000030 RID: 48
		private static DiagnosticsSettings defaultInstance = (DiagnosticsSettings)SettingsBase.Synchronized(new DiagnosticsSettings());
	}
}
