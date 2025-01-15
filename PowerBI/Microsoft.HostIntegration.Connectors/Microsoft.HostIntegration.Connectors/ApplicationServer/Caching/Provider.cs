using System;
using System.Diagnostics;
using System.Diagnostics.Eventing;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C9 RID: 457
	internal static class Provider
	{
		// Token: 0x06000EFE RID: 3838 RVA: 0x00032FD8 File Offset: 0x000311D8
		static Provider()
		{
			if (Provider._vistaProvider)
			{
				Provider._provider = new EventProviderVersionTwo(Provider._providerId);
				return;
			}
			Provider._providerLegacy = new EventProviderVersionOne(Provider._providerId);
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x000330D8 File Offset: 0x000312D8
		public static Guid ProviderGuid
		{
			get
			{
				return Provider._providerId;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000330DF File Offset: 0x000312DF
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000330E6 File Offset: 0x000312E6
		internal static TraceLevel DiagnosticTraceLevel
		{
			get
			{
				return Provider._diagnosticTraceLevel;
			}
			set
			{
				Provider._diagnosticTraceLevel = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000330EE File Offset: 0x000312EE
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x000330F5 File Offset: 0x000312F5
		internal static TraceLevel DiagnosticTraceSourceLevel
		{
			get
			{
				return Provider._diagnosticTraceSourceLevel;
			}
			set
			{
				Provider._diagnosticTraceSourceLevel = value;
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000330FD File Offset: 0x000312FD
		public static void OverrideProviderLevel(TraceLevel overrideLevel)
		{
			Provider._isProviderLevelOverriden = true;
			Provider._providerOverrideLevel = overrideLevel;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003310B File Offset: 0x0003130B
		public static bool IsEnabled()
		{
			if (Provider._vistaProvider)
			{
				return Provider._provider.IsEnabled() || Provider._diagnosticTraceLevel != TraceLevel.Off || Provider._diagnosticTraceSourceLevel != TraceLevel.Off;
			}
			return Provider._providerLegacy.IsEnabled();
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00033140 File Offset: 0x00031340
		public static bool IsEnabled(TraceLevel level)
		{
			if (!Provider._vistaProvider)
			{
				return Provider._providerLegacy.IsEnabled((byte)(level + 1), 0U);
			}
			if (Provider._isProviderLevelOverriden)
			{
				return level <= Provider._providerOverrideLevel;
			}
			bool flag = Provider._provider.IsEnabled((byte)(level + 1), long.MinValue);
			bool flag2 = level <= Provider._diagnosticTraceLevel;
			bool flag3 = level <= Provider._diagnosticTraceSourceLevel;
			return flag || flag2 || flag3;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000331B0 File Offset: 0x000313B0
		public static bool IsEnabled(TraceLevel level, long flags)
		{
			if (Provider._vistaProvider)
			{
				return Provider._provider.IsEnabled() && Provider._provider.IsEnabled((byte)(level + 1), flags);
			}
			return Provider._providerLegacy.IsEnabled() && Provider._providerLegacy.IsEnabled((byte)(level + 1), (uint)flags);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00033200 File Offset: 0x00031400
		public static bool EventWriteVerboseEvent(string Source, string Param)
		{
			if (Provider._vistaProvider)
			{
				return !Provider._provider.IsEnabled() || Provider._provider.TemplateStringTemplate(ref Provider.VerboseEvent, Source, Param);
			}
			return !Provider._providerLegacy.IsEnabled() || Provider._providerLegacy.TemplateStringTemplate(ref Provider.VerboseEvent, ref Provider.TaskId, Source, Param);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00033258 File Offset: 0x00031458
		public static bool EventWriteInformationalEvent(string Source, string Param)
		{
			if (Provider._vistaProvider)
			{
				return !Provider._provider.IsEnabled() || Provider._provider.TemplateStringTemplate(ref Provider.InformationalEvent, Source, Param);
			}
			return !Provider._providerLegacy.IsEnabled() || Provider._providerLegacy.TemplateStringTemplate(ref Provider.InformationalEvent, ref Provider.TaskId, Source, Param);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000332B0 File Offset: 0x000314B0
		public static bool EventWriteWarningEvent(string Source, string Param)
		{
			if (Provider._vistaProvider)
			{
				return !Provider._provider.IsEnabled() || Provider._provider.TemplateStringTemplate(ref Provider.WarningEvent, Source, Param);
			}
			return !Provider._providerLegacy.IsEnabled() || Provider._providerLegacy.TemplateStringTemplate(ref Provider.WarningEvent, ref Provider.TaskId, Source, Param);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00033308 File Offset: 0x00031508
		public static bool EventWriteErrorEvent(string Source, string Param)
		{
			if (Provider._vistaProvider)
			{
				return !Provider._provider.IsEnabled() || Provider._provider.TemplateStringTemplate(ref Provider.ErrorEvent, Source, Param);
			}
			return !Provider._providerLegacy.IsEnabled() || Provider._providerLegacy.TemplateStringTemplate(ref Provider.ErrorEvent, ref Provider.TaskId, Source, Param);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00033360 File Offset: 0x00031560
		public static bool EventWriteCriticalEvent(string Source, string Param)
		{
			if (Provider._vistaProvider)
			{
				return !Provider._provider.IsEnabled() || Provider._provider.TemplateStringTemplate(ref Provider.CriticalEvent, Source, Param);
			}
			return !Provider._providerLegacy.IsEnabled() || Provider._providerLegacy.TemplateStringTemplate(ref Provider.CriticalEvent, ref Provider.TaskId, Source, Param);
		}

		// Token: 0x04000A46 RID: 2630
		private static EventProviderVersionTwo _provider;

		// Token: 0x04000A47 RID: 2631
		private static EventProviderVersionOne _providerLegacy;

		// Token: 0x04000A48 RID: 2632
		private static bool _vistaProvider = Environment.OSVersion.Version.Major > 5;

		// Token: 0x04000A49 RID: 2633
		private static Guid _providerId = new Guid("a77dcf21-545f-4191-b3d0-c396cf2683f2");

		// Token: 0x04000A4A RID: 2634
		private static Guid TaskId = new Guid("d406518c-e017-4f4e-93a0-d6aacfc79e76");

		// Token: 0x04000A4B RID: 2635
		private static EventDescriptor VerboseEvent = new EventDescriptor(101, 0, 18, 5, 101, 1, 2305843009213693952L);

		// Token: 0x04000A4C RID: 2636
		private static EventDescriptor InformationalEvent = new EventDescriptor(102, 0, 20, 4, 102, 1, 576460752303423488L);

		// Token: 0x04000A4D RID: 2637
		private static EventDescriptor WarningEvent = new EventDescriptor(103, 0, 20, 3, 103, 1, 576460752303423488L);

		// Token: 0x04000A4E RID: 2638
		private static EventDescriptor ErrorEvent = new EventDescriptor(104, 0, 20, 2, 104, 1, 576460752303423488L);

		// Token: 0x04000A4F RID: 2639
		private static EventDescriptor CriticalEvent = new EventDescriptor(105, 0, 20, 1, 105, 1, 576460752303423488L);

		// Token: 0x04000A50 RID: 2640
		private static TraceLevel _diagnosticTraceLevel = TraceLevel.Off;

		// Token: 0x04000A51 RID: 2641
		private static TraceLevel _diagnosticTraceSourceLevel = TraceLevel.Off;

		// Token: 0x04000A52 RID: 2642
		private static bool _isProviderLevelOverriden;

		// Token: 0x04000A53 RID: 2643
		private static TraceLevel _providerOverrideLevel;
	}
}
