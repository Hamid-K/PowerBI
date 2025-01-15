using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x02000605 RID: 1541
	public abstract class CounterTelemetryContainer
	{
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000AE40D File Offset: 0x000AC60D
		private static uint NumberOfFeatures
		{
			get
			{
				return (uint)CounterTelemetryContainer.features.Length;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000AE416 File Offset: 0x000AC616
		// (set) Token: 0x0600343B RID: 13371 RVA: 0x000AE41E File Offset: 0x000AC61E
		public TelemetryCounterCollection[] CounterCollections { get; private set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000AE427 File Offset: 0x000AC627
		public uint NumberOfCounterCollections
		{
			get
			{
				return (uint)this.CounterCollections.Length;
			}
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000AE434 File Offset: 0x000AC634
		static CounterTelemetryContainer()
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			try
			{
				registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				registryKey2 = registryKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{50C60812-7CA9-43E6-8942-103098B638D4}");
				CounterTelemetryContainer.features = new TelemetryFeature[Enum.GetValues(typeof(FeatureType)).Length];
				if (registryKey2 != null)
				{
					CounterTelemetryContainer.telemetryKey = registryKey.OpenSubKey("Software\\Microsoft\\Host Integration Server\\Telemetry");
					if (CounterTelemetryContainer.telemetryKey != null)
					{
						CounterTelemetryContainer.hourlyTimer = new Timer(new TimerCallback(CounterTelemetryContainer.HourlyTimerCallback), null, 3600000, 3600000);
						AppDomain.CurrentDomain.DomainUnload += CounterTelemetryContainer.HandleDomainUnload;
						AppDomain.CurrentDomain.ProcessExit += CounterTelemetryContainer.ProcessExitHandler;
					}
				}
			}
			catch (Exception)
			{
				if (CounterTelemetryContainer.telemetryKey != null)
				{
					CounterTelemetryContainer.telemetryKey.Close();
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000AE53C File Offset: 0x000AC73C
		protected CounterTelemetryContainer(TelemetryFeatureInformation featureInformation, TelemetrySubFeatureInformation subFeatureInformation)
		{
			uint featureEnum = featureInformation.FeatureEnum;
			object obj = CounterTelemetryContainer.lockObject;
			lock (obj)
			{
				if (CounterTelemetryContainer.features[(int)featureEnum] == null)
				{
					CounterTelemetryContainer.features[(int)featureEnum] = new TelemetryFeature(featureInformation);
				}
				TelemetryFeature telemetryFeature = CounterTelemetryContainer.features[(int)featureEnum];
				uint subFeatureEnum = subFeatureInformation.SubFeatureEnum;
				telemetryFeature.SubFeatures[(int)subFeatureEnum] = new TelemetrySubFeature(subFeatureInformation);
				this.CounterCollections = subFeatureInformation.CounterCollections;
			}
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000AE5C0 File Offset: 0x000AC7C0
		protected void Increment(uint counterIdentifier)
		{
			this.Increment(0U, counterIdentifier);
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000AE5CC File Offset: 0x000AC7CC
		protected void Increment(uint counterCollectionIdentifier, uint counterIdentifier)
		{
			object obj = CounterTelemetryContainer.lockObject;
			lock (obj)
			{
				TelemetryCounterCollection telemetryCounterCollection = this.CounterCollections[(int)counterCollectionIdentifier];
				uint num = telemetryCounterCollection[counterIdentifier];
				telemetryCounterCollection[counterIdentifier] = num + 1U;
			}
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000AE620 File Offset: 0x000AC820
		protected void Increment(string counterIdentifier)
		{
			this.Increment(0U, counterIdentifier);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000AE62C File Offset: 0x000AC82C
		protected void Increment(uint counterCollectionIdentifier, string counterIdentifier)
		{
			object obj = CounterTelemetryContainer.lockObject;
			lock (obj)
			{
				TelemetryCounterCollection telemetryCounterCollection = this.CounterCollections[(int)counterCollectionIdentifier];
				uint num = telemetryCounterCollection[counterIdentifier];
				telemetryCounterCollection[counterIdentifier] = num + 1U;
			}
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000AE680 File Offset: 0x000AC880
		private static void HourlyTimerCallback(object stateInfo)
		{
			CounterTelemetryContainer.DoCounters(false);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000AE688 File Offset: 0x000AC888
		private static void HandleDomainUnload(object sender, EventArgs evt)
		{
			CounterTelemetryContainer.DoCounters(true);
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000AE688 File Offset: 0x000AC888
		private static void ProcessExitHandler(object o, EventArgs args)
		{
			CounterTelemetryContainer.DoCounters(true);
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000AE690 File Offset: 0x000AC890
		private unsafe static void DoCounters(bool shouldExit)
		{
			List<SentCounterInformation> list = null;
			uint num = 0U;
			object obj = CounterTelemetryContainer.lockObject;
			lock (obj)
			{
				if (CounterTelemetryContainer.exiting)
				{
					return;
				}
				if (shouldExit)
				{
					CounterTelemetryContainer.hourlyTimer.Change(-1, -1);
					CounterTelemetryContainer.hourlyTimer.Dispose();
				}
				bool flag2 = CounterTelemetryContainer.IsTelemetryEnabled();
				list = new List<SentCounterInformation>();
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)CounterTelemetryContainer.features.Length))
				{
					TelemetryFeature telemetryFeature = CounterTelemetryContainer.features[(int)num2];
					if (telemetryFeature != null)
					{
						for (uint num3 = 0U; num3 < telemetryFeature.NumberOfSubFeatures; num3 += 1U)
						{
							TelemetrySubFeature telemetrySubFeature = telemetryFeature.SubFeatures[(int)num3];
							if (telemetrySubFeature != null)
							{
								num += telemetrySubFeature.CreateSentCountersAndClear(num2, num3, list, flag2);
							}
						}
					}
					num2 += 1U;
				}
				CounterTelemetryContainer.exiting = shouldExit;
			}
			if (CounterTelemetryContainer.exiting)
			{
				CounterTelemetryContainer.telemetryKey.Close();
			}
			if (list.Count == 0)
			{
				return;
			}
			uint num4 = 8U + num;
			byte[] array = new byte[num4];
			fixed (byte* ptr = &array[0])
			{
				uint* ptr2 = (uint*)ptr;
				*(ptr2++) = num4;
				*(ptr2++) = (uint)list.Count;
				byte* ptr3 = (byte*)ptr2;
				foreach (SentCounterInformation sentCounterInformation in list)
				{
					sentCounterInformation.GetBytes(ref ptr3);
				}
			}
			try
			{
				using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", "HisVssWriter", PipeDirection.Out))
				{
					namedPipeClientStream.Connect(100);
					namedPipeClientStream.Write(array, 0, (int)num4);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000AE848 File Offset: 0x000ACA48
		private static bool IsTelemetryEnabled()
		{
			if (CounterTelemetryContainer.telemetryKey == null)
			{
				return false;
			}
			string text = (string)CounterTelemetryContainer.telemetryKey.GetValue("Enabled");
			bool flag;
			return !string.IsNullOrWhiteSpace(text) && bool.TryParse(text, out flag) && flag;
		}

		// Token: 0x04001D7B RID: 7547
		private static TelemetryFeature[] features;

		// Token: 0x04001D7C RID: 7548
		private const int HourInMilliseconds = 3600000;

		// Token: 0x04001D7D RID: 7549
		private static Timer hourlyTimer;

		// Token: 0x04001D7E RID: 7550
		private const int TenthOfSecond = 100;

		// Token: 0x04001D7F RID: 7551
		private static object lockObject = new object();

		// Token: 0x04001D80 RID: 7552
		private static bool exiting;

		// Token: 0x04001D81 RID: 7553
		private static RegistryKey telemetryKey;
	}
}
