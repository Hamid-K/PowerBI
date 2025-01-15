using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000AA RID: 170
	internal class HeartbeatProvider : IDisposable, IHeartbeatProvider, IHeartbeatPropertyManager
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00015540 File Offset: 0x00013740
		public HeartbeatProvider()
		{
			this.interval = HeartbeatProvider.DefaultHeartbeatInterval;
			this.heartbeatProperties = new ConcurrentDictionary<string, HeartbeatPropertyPayload>(StringComparer.OrdinalIgnoreCase);
			this.heartbeatsSent = 0UL;
			this.isEnabled = true;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00015595 File Offset: 0x00013795
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0001559D File Offset: 0x0001379D
		public TimeSpan HeartbeatInterval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= HeartbeatProvider.MinimumHeartbeatInterval)
				{
					this.interval = HeartbeatProvider.MinimumHeartbeatInterval;
					return;
				}
				this.interval = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000155BF File Offset: 0x000137BF
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x000155D2 File Offset: 0x000137D2
		public string InstrumentationKey
		{
			get
			{
				TelemetryClient telemetryClient = this.telemetryClient;
				if (telemetryClient == null)
				{
					return null;
				}
				return telemetryClient.InstrumentationKey;
			}
			set
			{
				if (this.telemetryClient != null)
				{
					this.telemetryClient.InstrumentationKey = value;
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000155E8 File Offset: 0x000137E8
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x000155F2 File Offset: 0x000137F2
		public bool IsHeartbeatEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				if (!this.isEnabled && value)
				{
					this.HeartbeatTimer.Change(this.HeartbeatInterval, this.HeartbeatInterval);
				}
				this.isEnabled = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00015624 File Offset: 0x00013824
		public IList<string> ExcludedHeartbeatPropertyProviders
		{
			get
			{
				return this.disabledHeartbeatPropertyProviders;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001562C File Offset: 0x0001382C
		public IList<string> ExcludedHeartbeatProperties
		{
			get
			{
				return this.disabledDefaultFields;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00015634 File Offset: 0x00013834
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0001563C File Offset: 0x0001383C
		private Timer HeartbeatTimer { get; set; }

		// Token: 0x06000527 RID: 1319 RVA: 0x00015648 File Offset: 0x00013848
		public void Initialize(TelemetryConfiguration configuration)
		{
			if (this.telemetryClient == null)
			{
				this.telemetryClient = new TelemetryClient(configuration);
			}
			Task.Factory.StartNew<Task<bool>>(async () => await HeartbeatDefaultPayload.PopulateDefaultPayload(this.ExcludedHeartbeatProperties, this.ExcludedHeartbeatPropertyProviders, this).ConfigureAwait(false));
			if (this.HeartbeatTimer == null)
			{
				int num = (this.IsHeartbeatEnabled ? ((int)this.HeartbeatInterval.TotalMilliseconds) : (-1));
				this.HeartbeatTimer = new Timer(new TimerCallback(this.HeartbeatPulse), this, num, num);
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000156BD File Offset: 0x000138BD
		public bool AddHeartbeatProperty(string heartbeatPropertyName, string heartbeatPropertyValue, bool isHealthy)
		{
			return this.AddHeartbeatProperty(heartbeatPropertyName, false, heartbeatPropertyValue, isHealthy);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000156C9 File Offset: 0x000138C9
		public bool SetHeartbeatProperty(string heartbeatPropertyName, string heartbeatPropertyValue = null, bool? isHealthy = null)
		{
			return this.SetHeartbeatProperty(heartbeatPropertyName, false, heartbeatPropertyValue, isHealthy);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000156D5 File Offset: 0x000138D5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000156E4 File Offset: 0x000138E4
		public bool AddHeartbeatProperty(string propertyName, bool overrideDefaultField, string propertyValue, bool isHealthy)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(propertyName) && (overrideDefaultField || !HeartbeatDefaultPayload.IsDefaultKeyword(propertyName)))
			{
				try
				{
					this.heartbeatProperties.GetOrAdd(propertyName, delegate(string key)
					{
						isAdded = true;
						return new HeartbeatPropertyPayload
						{
							IsHealthy = isHealthy,
							PayloadValue = propertyValue
						};
					});
					goto IL_0085;
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.FailedToAddHeartbeatProperty(propertyName, propertyValue, ex.ToString(), "Incorrect");
					goto IL_0085;
				}
			}
			CoreEventSource.Log.HeartbeatPropertyAddedWithoutAnyName(propertyValue, isHealthy, "Incorrect");
			IL_0085:
			return isAdded;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001578C File Offset: 0x0001398C
		public bool SetHeartbeatProperty(string propertyName, bool overrideDefaultField, string propertyValue = null, bool? isHealthy = null)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(propertyName) && (overrideDefaultField || !HeartbeatDefaultPayload.IsDefaultKeyword(propertyName)))
			{
				try
				{
					this.heartbeatProperties.AddOrUpdate(propertyName, delegate(string key)
					{
						throw new Exception("Cannot set a heartbeat property without adding it first.");
					}, delegate(string key, HeartbeatPropertyPayload property)
					{
						if (isHealthy != null)
						{
							property.IsHealthy = isHealthy.Value;
						}
						if (propertyValue != null)
						{
							property.PayloadValue = propertyValue;
						}
						return property;
					});
					return true;
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.FailedToSetHeartbeatProperty(propertyName, propertyValue, isHealthy != null, isHealthy.GetValueOrDefault(false), ex.ToString(), "Incorrect");
					return flag;
				}
			}
			CoreEventSource.Log.CannotSetHeartbeatPropertyWithNoNameOrDefaultName(propertyName, propertyValue, isHealthy != null, isHealthy.GetValueOrDefault(false), "Incorrect");
			return flag;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00015874 File Offset: 0x00013A74
		internal ITelemetry GatherData()
		{
			MetricTelemetry metricTelemetry = new MetricTelemetry(HeartbeatProvider.heartbeatSyntheticMetricName, 0.0);
			foreach (KeyValuePair<string, HeartbeatPropertyPayload> keyValuePair in this.heartbeatProperties)
			{
				metricTelemetry.Properties.Add(keyValuePair.Key, keyValuePair.Value.PayloadValue);
				metricTelemetry.Sum += (double)(keyValuePair.Value.IsHealthy ? 0 : 1);
			}
			MetricTelemetry metricTelemetry2 = metricTelemetry;
			IFormatProvider currentCulture = CultureInfo.CurrentCulture;
			string text = "{0}";
			object[] array = new object[1];
			int num = 0;
			ulong num2 = this.heartbeatsSent;
			this.heartbeatsSent = num2 + 1UL;
			array[num] = num2;
			metricTelemetry2.Sequence = string.Format(currentCulture, text, array);
			return metricTelemetry;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00015944 File Offset: 0x00013B44
		protected void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing && this.HeartbeatTimer != null)
				{
					this.isEnabled = false;
					try
					{
						this.HeartbeatTimer.Dispose();
					}
					catch (Exception)
					{
						CoreEventSource.Log.LogError("Error occurred when disposing heartbeat timer within HeartbeatProvider", "Incorrect");
					}
				}
				this.disposedValue = true;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000159A8 File Offset: 0x00013BA8
		private void Send()
		{
			if (this.telemetryClient.TelemetryConfiguration.TelemetryChannel == null)
			{
				return;
			}
			MetricTelemetry metricTelemetry = (MetricTelemetry)this.GatherData();
			metricTelemetry.Context.Operation.SyntheticSource = HeartbeatProvider.heartbeatSyntheticMetricName;
			metricTelemetry.Context.GetInternalContext().SdkVersion = HeartbeatProvider.sdkVersionPropertyValue;
			this.telemetryClient.Track(metricTelemetry);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015A0C File Offset: 0x00013C0C
		private void HeartbeatPulse(object state)
		{
			HeartbeatProvider heartbeatProvider;
			if ((heartbeatProvider = state as HeartbeatProvider) != null && heartbeatProvider.IsHeartbeatEnabled)
			{
				try
				{
					this.HeartbeatTimer.Change(-1, -1);
					heartbeatProvider.Send();
					return;
				}
				catch (ObjectDisposedException)
				{
					CoreEventSource.Log.LogError("Heartbeat timer change during dispose occurred.", "Incorrect");
					return;
				}
				finally
				{
					if (this.IsHeartbeatEnabled)
					{
						this.HeartbeatTimer.Change(this.HeartbeatInterval, this.HeartbeatInterval);
					}
				}
			}
			CoreEventSource.Log.LogError("Heartbeat pulse being sent without valid instance of HeartbeatProvider as its state", "Incorrect");
		}

		// Token: 0x0400020B RID: 523
		public static readonly TimeSpan DefaultHeartbeatInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400020C RID: 524
		public static readonly TimeSpan MinimumHeartbeatInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x0400020D RID: 525
		private static string sdkVersionPropertyValue = SdkVersionUtils.GetSdkVersion("hbnet:");

		// Token: 0x0400020E RID: 526
		private static string heartbeatSyntheticMetricName = "HeartbeatState";

		// Token: 0x0400020F RID: 527
		private readonly List<string> disabledDefaultFields = new List<string>();

		// Token: 0x04000210 RID: 528
		private readonly List<string> disabledHeartbeatPropertyProviders = new List<string>();

		// Token: 0x04000211 RID: 529
		private ulong heartbeatsSent;

		// Token: 0x04000212 RID: 530
		private ConcurrentDictionary<string, HeartbeatPropertyPayload> heartbeatProperties;

		// Token: 0x04000213 RID: 531
		private bool disposedValue;

		// Token: 0x04000214 RID: 532
		private TimeSpan interval;

		// Token: 0x04000215 RID: 533
		private TelemetryClient telemetryClient;

		// Token: 0x04000216 RID: 534
		private volatile bool isEnabled;
	}
}
