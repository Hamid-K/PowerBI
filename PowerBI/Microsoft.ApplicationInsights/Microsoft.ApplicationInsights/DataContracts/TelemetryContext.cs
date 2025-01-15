using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000DE RID: 222
	public sealed class TelemetryContext
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x0001A449 File Offset: 0x00018649
		public TelemetryContext()
			: this(null, null)
		{
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001A453 File Offset: 0x00018653
		internal TelemetryContext(IDictionary<string, string> properties)
			: this(properties, null)
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001A45D File Offset: 0x0001865D
		internal TelemetryContext(IDictionary<string, string> properties, IDictionary<string, string> globalProperties)
		{
			this.PropertiesValue = properties;
			this.GlobalPropertiesValue = globalProperties;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001A494 File Offset: 0x00018694
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0001A4A5 File Offset: 0x000186A5
		public string InstrumentationKey
		{
			get
			{
				return this.instrumentationKey ?? string.Empty;
			}
			set
			{
				Property.Set<string>(ref this.instrumentationKey, value);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0001A4B3 File Offset: 0x000186B3
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0001A4BB File Offset: 0x000186BB
		public long Flags { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001A4C4 File Offset: 0x000186C4
		public ComponentContext Component
		{
			get
			{
				return LazyInitializer.EnsureInitialized<ComponentContext>(ref this.component, () => new ComponentContext());
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0001A4F0 File Offset: 0x000186F0
		public DeviceContext Device
		{
			get
			{
				return LazyInitializer.EnsureInitialized<DeviceContext>(ref this.device, () => new DeviceContext(this.Properties));
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0001A509 File Offset: 0x00018709
		public CloudContext Cloud
		{
			get
			{
				return LazyInitializer.EnsureInitialized<CloudContext>(ref this.cloud, () => new CloudContext());
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001A535 File Offset: 0x00018735
		public SessionContext Session
		{
			get
			{
				return LazyInitializer.EnsureInitialized<SessionContext>(ref this.session, () => new SessionContext());
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001A561 File Offset: 0x00018761
		public UserContext User
		{
			get
			{
				return LazyInitializer.EnsureInitialized<UserContext>(ref this.user, () => new UserContext());
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001A58D File Offset: 0x0001878D
		public OperationContext Operation
		{
			get
			{
				return LazyInitializer.EnsureInitialized<OperationContext>(ref this.operation, () => new OperationContext());
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001A5B9 File Offset: 0x000187B9
		public LocationContext Location
		{
			get
			{
				return LazyInitializer.EnsureInitialized<LocationContext>(ref this.location, () => new LocationContext());
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001A5E5 File Offset: 0x000187E5
		[Obsolete("Use GlobalProperties to set global level properties. For properties at item level, use ISupportProperties.Properties.")]
		public IDictionary<string, string> Properties
		{
			get
			{
				return LazyInitializer.EnsureInitialized<IDictionary<string, string>>(ref this.PropertiesValue, () => new ConcurrentDictionary<string, string>());
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001A611 File Offset: 0x00018811
		public IDictionary<string, string> GlobalProperties
		{
			get
			{
				return LazyInitializer.EnsureInitialized<IDictionary<string, string>>(ref this.GlobalPropertiesValue, () => new ConcurrentDictionary<string, string>());
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001A63D File Offset: 0x0001883D
		internal InternalContext Internal
		{
			get
			{
				return this.internalContext;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001A648 File Offset: 0x00018848
		internal IDictionary<string, string> SanitizedTags
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				ComponentContext componentContext = this.component;
				if (componentContext != null)
				{
					componentContext.UpdateTags(dictionary);
				}
				DeviceContext deviceContext = this.device;
				if (deviceContext != null)
				{
					deviceContext.UpdateTags(dictionary);
				}
				CloudContext cloudContext = this.cloud;
				if (cloudContext != null)
				{
					cloudContext.UpdateTags(dictionary);
				}
				SessionContext sessionContext = this.session;
				if (sessionContext != null)
				{
					sessionContext.UpdateTags(dictionary);
				}
				UserContext userContext = this.user;
				if (userContext != null)
				{
					userContext.UpdateTags(dictionary);
				}
				OperationContext operationContext = this.operation;
				if (operationContext != null)
				{
					operationContext.UpdateTags(dictionary);
				}
				LocationContext locationContext = this.location;
				if (locationContext != null)
				{
					locationContext.UpdateTags(dictionary);
				}
				this.Internal.UpdateTags(dictionary);
				return dictionary;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A6E6 File Offset: 0x000188E6
		public bool TryGetRawObject(string key, out object rawObject)
		{
			if (key == null)
			{
				rawObject = null;
				return false;
			}
			return this.rawObjectsTemp.TryGetValue(key, out rawObject) || this.rawObjectsPerm.TryGetValue(key, out rawObject);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A70E File Offset: 0x0001890E
		public void StoreRawObject(string key, object rawObject, bool keepForInitializationOnly = true)
		{
			if (key == null)
			{
				return;
			}
			if (keepForInitializationOnly)
			{
				this.rawObjectsTemp[key] = rawObject;
				this.rawObjectsPerm.Remove(key);
				return;
			}
			this.rawObjectsPerm[key] = rawObject;
			this.rawObjectsTemp.Remove(key);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A74C File Offset: 0x0001894C
		internal void SanitizeGlobalProperties()
		{
			IDictionary<string, string> globalPropertiesValue = this.GlobalPropertiesValue;
			if (globalPropertiesValue == null)
			{
				return;
			}
			globalPropertiesValue.SanitizeProperties();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A75E File Offset: 0x0001895E
		internal void ClearTempRawObjects()
		{
			this.rawObjectsTemp.Clear();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001A76C File Offset: 0x0001896C
		internal TelemetryContext DeepClone(IDictionary<string, string> properties)
		{
			TelemetryContext telemetryContext = new TelemetryContext(properties);
			if (this.GlobalPropertiesValue != null)
			{
				Utils.CopyDictionary<string>(this.GlobalProperties, telemetryContext.GlobalProperties);
			}
			if (this.PropertiesValue != null)
			{
				Utils.CopyDictionary<string>(this.Properties, telemetryContext.Properties);
			}
			telemetryContext.Initialize(this, this.instrumentationKey);
			telemetryContext.rawObjectsTemp = this.rawObjectsTemp;
			telemetryContext.rawObjectsPerm = this.rawObjectsPerm;
			return telemetryContext;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001A7D8 File Offset: 0x000189D8
		internal TelemetryContext DeepClone()
		{
			return this.DeepClone(null);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001A7E4 File Offset: 0x000189E4
		internal void Initialize(TelemetryContext source, string instrumentationKey)
		{
			this.InitializeInstrumentationkey(instrumentationKey);
			this.Flags |= source.Flags;
			ComponentContext componentContext = source.component;
			if (componentContext != null)
			{
				componentContext.CopyTo(this.Component);
			}
			DeviceContext deviceContext = source.device;
			if (deviceContext != null)
			{
				deviceContext.CopyTo(this.Device);
			}
			CloudContext cloudContext = source.cloud;
			if (cloudContext != null)
			{
				cloudContext.CopyTo(this.Cloud);
			}
			SessionContext sessionContext = source.session;
			if (sessionContext != null)
			{
				sessionContext.CopyTo(this.Session);
			}
			UserContext userContext = source.user;
			if (userContext != null)
			{
				userContext.CopyTo(this.User);
			}
			OperationContext operationContext = source.operation;
			if (operationContext != null)
			{
				operationContext.CopyTo(this.Operation);
			}
			LocationContext locationContext = source.location;
			if (locationContext != null)
			{
				locationContext.CopyTo(this.Location);
			}
			source.Internal.CopyTo(this.Internal);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001A8BD File Offset: 0x00018ABD
		internal void InitializeInstrumentationkey(string instrumentationKey)
		{
			Property.Initialize(ref this.instrumentationKey, instrumentationKey);
		}

		// Token: 0x0400030A RID: 778
		public const long FlagDropIdentifiers = 2097152L;

		// Token: 0x0400030B RID: 779
		internal IDictionary<string, string> GlobalPropertiesValue;

		// Token: 0x0400030C RID: 780
		internal IDictionary<string, string> PropertiesValue;

		// Token: 0x0400030D RID: 781
		private readonly InternalContext internalContext = new InternalContext();

		// Token: 0x0400030E RID: 782
		private string instrumentationKey;

		// Token: 0x0400030F RID: 783
		private IDictionary<string, object> rawObjectsTemp = new Dictionary<string, object>();

		// Token: 0x04000310 RID: 784
		private IDictionary<string, object> rawObjectsPerm = new Dictionary<string, object>();

		// Token: 0x04000311 RID: 785
		private ComponentContext component;

		// Token: 0x04000312 RID: 786
		private DeviceContext device;

		// Token: 0x04000313 RID: 787
		private CloudContext cloud;

		// Token: 0x04000314 RID: 788
		private SessionContext session;

		// Token: 0x04000315 RID: 789
		private UserContext user;

		// Token: 0x04000316 RID: 790
		private OperationContext operation;

		// Token: 0x04000317 RID: 791
		private LocationContext location;
	}
}
