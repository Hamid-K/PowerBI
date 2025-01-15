using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Cloud.Platform.ConfigurationClasses.Tracing;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D3 RID: 723
	public static class Tracing
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x00042E5C File Offset: 0x0004105C
		internal static void SetTweakToExposePii(bool exposePii)
		{
			string text = (exposePii ? "true" : "false");
			Anchor.Tweaks.SetProgrammaticAppSwitch("Microsoft.Cloud.Platform.Utils.Tracing.RemovePIIFromTracesSuppressed", text);
			Tracing.Initialize();
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00042E90 File Offset: 0x00041090
		internal static bool GetTweakToExposePii()
		{
			bool flag;
			return bool.TryParse(Anchor.Tweaks.GetProgrammaticAppSwitch("Microsoft.Cloud.Platform.Utils.Tracing.RemovePIIFromTracesSuppressed"), out flag) && flag;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00042EB8 File Offset: 0x000410B8
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x00042EBF File Offset: 0x000410BF
		public static string InstanceId
		{
			get
			{
				return Tracing.s_instanceId;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && Interlocked.CompareExchange<string>(ref Tracing.s_instanceId, value, string.Empty).Length == 0)
				{
					Tracing.SetHeaderLength();
				}
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00042EE6 File Offset: 0x000410E6
		public static TraceSourcesRegistrar TraceSourcesRegistrar
		{
			get
			{
				return Tracing.s_traceSourcesRegistrar;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00042EED File Offset: 0x000410ED
		public static string TraceSourceInstancePropertyName
		{
			get
			{
				return "Tracer";
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x00042EF4 File Offset: 0x000410F4
		public static string TraceSourceIdentification
		{
			get
			{
				return "ID";
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00042EFB File Offset: 0x000410FB
		public static string TraceSourceVerbosityPropertyName
		{
			get
			{
				return "DefaultVerbosity";
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x00042F04 File Offset: 0x00041104
		public static TraceVerbosity SuppressedTracesLevel
		{
			get
			{
				TraceVerbosity tracingSuppressedLevel = UtilsContext.Current.TracingSuppressedLevel;
				if (tracingSuppressedLevel != (TraceVerbosity)0)
				{
					return tracingSuppressedLevel;
				}
				return TraceVerbosity.Verbose;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00042F22 File Offset: 0x00041122
		public static bool TracingForced
		{
			get
			{
				return UtilsContext.Current.TracingForced;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x00042F2E File Offset: 0x0004112E
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x00042F35 File Offset: 0x00041135
		public static TraceVerbosity ForcedTracesTraceLevel
		{
			get
			{
				return Tracing.s_forcedTracesTraceLevel;
			}
			set
			{
				Tracing.s_forcedTracesTraceLevel = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x00042F3D File Offset: 0x0004113D
		public static bool RemovePersonallyIdentifiableInformationFromTraces
		{
			get
			{
				return Tracing.s_removePersonallyIdentifiableInformationFromTraces;
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00042F44 File Offset: 0x00041144
		public static string GetHeader()
		{
			return Tracing.GetHeader(UtilsContext.Current.Activity);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00042F58 File Offset: 0x00041158
		public static string GetHeader(Activity activity)
		{
			StringBuilder stringBuilder = new StringBuilder(Tracing.s_headerLength);
			stringBuilder.Append(Tracing.InstanceId);
			stringBuilder.Append('\t');
			stringBuilder.Append(activity.ActivityId);
			stringBuilder.Append('\t');
			stringBuilder.Append(activity.RootActivityId);
			stringBuilder.Append('\t');
			stringBuilder.Append(activity.ActivityType.ShortName);
			stringBuilder.Append('\t');
			stringBuilder.Append(activity.ClientActivityId);
			return stringBuilder.ToString();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00042FE8 File Offset: 0x000411E8
		public static string GetFormatedMessage(params object[] data)
		{
			DateTime dateTime = (DateTime)data[0];
			StringBuilder stringBuilder = new StringBuilder();
			char c = '\t';
			stringBuilder.Append(data[1].ToString());
			stringBuilder.Append(c);
			stringBuilder.Append(data[2].ToString());
			stringBuilder.Append(c);
			stringBuilder.Append(data[3].ToString());
			stringBuilder.Append(c);
			stringBuilder.Append(data[4].ToString());
			stringBuilder.Append(c);
			stringBuilder.Append(data[5].ToString());
			return "{0} {1}\t{2} {3}".FormatWithInvariantCulture(new object[]
			{
				dateTime.ToString("O", CultureInfo.InvariantCulture),
				stringBuilder.ToString(),
				data[6],
				data[7]
			});
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000430AC File Offset: 0x000412AC
		public static bool ParseTrace(string delimitedTrace, out DateTime traceTimeStamp, out string instanceId, out string sourceId, out string eventText, out string traceActivityId, out string rootActivityId, out string activityType, out string clientActivityId)
		{
			traceTimeStamp = DateTime.UtcNow;
			instanceId = string.Empty;
			sourceId = string.Empty;
			eventText = string.Empty;
			traceActivityId = string.Empty;
			rootActivityId = string.Empty;
			activityType = string.Empty;
			clientActivityId = string.Empty;
			bool flag;
			try
			{
				string[] array = delimitedTrace.Split(new char[] { '\t' });
				if (array.Length >= 6)
				{
					int num = array[0].IndexOf(' ');
					if (num > 0)
					{
						if (!DateTime.TryParse(array[0].Substring(0, num), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out traceTimeStamp))
						{
							return false;
						}
						instanceId = array[0].Substring(num + 1);
					}
					traceActivityId = array[1];
					rootActivityId = array[2];
					activityType = array[3];
					clientActivityId = array[4];
					num = array[5].IndexOf(' ');
					if (num > 0)
					{
						sourceId = array[5].Substring(0, num);
						eventText = array[5].Substring(num + 1);
						if (array.Length > 6)
						{
							eventText += string.Join(" ", array, 6, array.Length - 6);
						}
						return true;
					}
				}
				else if (array.Length == 1)
				{
					string[] array2 = delimitedTrace.Split(new char[] { ' ' });
					if (array2.Length > 7)
					{
						traceTimeStamp = DateTime.Parse(array2[0], CultureInfo.InvariantCulture).ToUniversalTime();
						instanceId = array2[1];
						traceActivityId = array[2];
						rootActivityId = array[3];
						activityType = array[4];
						clientActivityId = array[5];
						sourceId = array[6];
						eventText = string.Join(" ", array2, 7, array2.Length - 7);
						return true;
					}
				}
				flag = false;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00043258 File Offset: 0x00041458
		public static char Delimiter
		{
			get
			{
				return '\t';
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0004325C File Offset: 0x0004145C
		public static bool ShouldTrace(TraceVerbosity currentTraceLevel, TraceVerbosity maximumTraceLevel)
		{
			bool tracingForced = Tracing.TracingForced;
			return (currentTraceLevel <= Tracing.SuppressedTracesLevel || tracingForced) && (currentTraceLevel <= maximumTraceLevel || (tracingForced && Tracing.ForcedTracesTraceLevel >= maximumTraceLevel));
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00043290 File Offset: 0x00041490
		public static IEnumerable<Type> GetTraceSourceTypes(Assembly assembly)
		{
			IEnumerable<Type> enumerable = new List<Type>();
			try
			{
				enumerable = (from type in DynamicLoader.GetTypes(assembly)
					where !type.IsGenericType & (from i in type.GetInterfaces()
						where i.FullName != null && i.FullName.Equals(typeof(ITraceSource).FullName, StringComparison.Ordinal)
						select i).Any<Type>()
					select type).ToList<Type>();
			}
			catch (ReflectionTypeLoadException)
			{
			}
			return enumerable;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000432EC File Offset: 0x000414EC
		public static IEnumerable<TraceSourceConfig> GetGeneratedTraceSourcesConfiguration(TraceSourcesConfiguration traceSourcesConfigurationClass, IEnumerable<Assembly> assemblies)
		{
			List<Type> list = new List<Type>();
			List<TraceSourceConfig> list2 = new List<TraceSourceConfig>();
			list.AddRange(assemblies.Where((Assembly a) => a != null).SelectMany(new Func<Assembly, IEnumerable<Type>>(Tracing.GetTraceSourceTypes)).Distinct((Type traceSource) => traceSource.FullName));
			foreach (Type type in list)
			{
				object value = type.GetProperty(Tracing.TraceSourceInstancePropertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null, null);
				string traceSourceId = type.GetProperty(Tracing.TraceSourceIdentification, BindingFlags.Instance | BindingFlags.Public).GetValue(value, null).ToString();
				string text = type.GetProperty(Tracing.TraceSourceVerbosityPropertyName, BindingFlags.Instance | BindingFlags.Public).GetValue(value, null).ToString();
				if (!traceSourcesConfigurationClass.TraceSourceConfigList.Any((TraceSourceConfig configuration) => configuration.ID.Name.Equals(traceSourceId, StringComparison.OrdinalIgnoreCase)))
				{
					TraceVerbosity traceVerbosity = ExtendedEnum.Parse<TraceVerbosity>(text);
					list2.Add(new TraceSourceConfig
					{
						Name = traceSourceId,
						Verbosity = traceVerbosity
					});
				}
			}
			return list2;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00043440 File Offset: 0x00041640
		public static bool AssemblyHasTraceSource(string filename)
		{
			return AssemblyWalker.AssemblyHasResourceName(filename, "Microsoft.Cloud.Platform.TraceSource.Defined");
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004344D File Offset: 0x0004164D
		internal static void Initialize()
		{
			Tracing.s_removePersonallyIdentifiableInformationFromTraces = !Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.Tracing.RemovePIIFromTracesSuppressed", "When set, Personally Identifiable Information (PII) removal mechanism from traces will be disabled, so that PII is available in traces; cannot be modified during runtime", false).Value;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00043471 File Offset: 0x00041671
		private static int SetHeaderLength()
		{
			Tracing.s_headerLength = Tracing.s_instanceId.Length + 1 + 36 + 1 + 36 + 1 + 36 + 1 + 4;
			return Tracing.s_headerLength;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0004349A File Offset: 0x0004169A
		internal static int HeaderLength
		{
			get
			{
				return Tracing.s_headerLength;
			}
		}

		// Token: 0x04000744 RID: 1860
		internal const string c_removePersonallyIdentifiableInformationFromTracesSuppressedName = "Microsoft.Cloud.Platform.Utils.Tracing.RemovePIIFromTracesSuppressed";

		// Token: 0x04000745 RID: 1861
		private const char c_delimiter = '\t';

		// Token: 0x04000746 RID: 1862
		private const string c_traceSourceResourceName = "Microsoft.Cloud.Platform.TraceSource.Defined";

		// Token: 0x04000747 RID: 1863
		private static string s_instanceId = string.Empty;

		// Token: 0x04000748 RID: 1864
		private static TraceVerbosity s_forcedTracesTraceLevel = TraceVerbosity.Verbose;

		// Token: 0x04000749 RID: 1865
		private static bool s_removePersonallyIdentifiableInformationFromTraces = true;

		// Token: 0x0400074A RID: 1866
		private static TraceSourcesRegistrar s_traceSourcesRegistrar = new TraceSourcesRegistrar();

		// Token: 0x0400074B RID: 1867
		private static int s_headerLength = Tracing.SetHeaderLength();
	}
}
