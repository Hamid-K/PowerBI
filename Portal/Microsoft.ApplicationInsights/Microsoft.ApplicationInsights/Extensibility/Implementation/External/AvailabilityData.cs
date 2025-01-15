using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B0 RID: 176
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class AvailabilityData : Domain, ISerializableWithWriter
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x00015DB8 File Offset: 0x00013FB8
		public AvailabilityData DeepClone()
		{
			AvailabilityData availabilityData = new AvailabilityData();
			availabilityData.ver = this.ver;
			availabilityData.id = this.id;
			availabilityData.name = this.name;
			availabilityData.duration = this.duration;
			availabilityData.success = this.success;
			availabilityData.runLocation = this.runLocation;
			availabilityData.message = this.message;
			Utils.CopyDictionary<string>(this.properties, availabilityData.properties);
			Utils.CopyDictionary<double>(this.measurements, availabilityData.measurements);
			return availabilityData;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00015E44 File Offset: 0x00014044
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(this.ver));
			serializationWriter.WriteProperty("id", this.id);
			serializationWriter.WriteProperty("name", this.name);
			serializationWriter.WriteProperty("duration", new TimeSpan?(this.duration));
			serializationWriter.WriteProperty("success", new bool?(this.success));
			serializationWriter.WriteProperty("runLocation", this.runLocation);
			serializationWriter.WriteProperty("message", this.message);
			serializationWriter.WriteProperty("properties", this.properties);
			serializationWriter.WriteProperty("measurements", this.measurements);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00015EF9 File Offset: 0x000140F9
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00015F01 File Offset: 0x00014101
		public int ver { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00015F0A File Offset: 0x0001410A
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00015F12 File Offset: 0x00014112
		public string id { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00015F1B File Offset: 0x0001411B
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00015F23 File Offset: 0x00014123
		public string name { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00015F2C File Offset: 0x0001412C
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00015F34 File Offset: 0x00014134
		public TimeSpan duration { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00015F3D File Offset: 0x0001413D
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00015F45 File Offset: 0x00014145
		public bool success { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00015F4E File Offset: 0x0001414E
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00015F56 File Offset: 0x00014156
		public string runLocation { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00015F5F File Offset: 0x0001415F
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00015F67 File Offset: 0x00014167
		public string message { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00015F70 File Offset: 0x00014170
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00015F78 File Offset: 0x00014178
		public IDictionary<string, string> properties { get; set; }

		// Token: 0x0600055D RID: 1373 RVA: 0x00015F81 File Offset: 0x00014181
		public AvailabilityData()
			: this("AI.AvailabilityData", "AvailabilityData")
		{
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00015F94 File Offset: 0x00014194
		protected AvailabilityData(string fullName, string name)
		{
			this.ver = 2;
			this.id = "";
			this.name = "";
			this.duration = TimeSpan.Zero;
			this.runLocation = "";
			this.message = "";
			this.properties = new ConcurrentDictionary<string, string>();
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00015FF0 File Offset: 0x000141F0
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0001601C File Offset: 0x0001421C
		public IDictionary<string, double> measurements
		{
			get
			{
				return LazyInitializer.EnsureInitialized<IDictionary<string, double>>(ref this.measurementsInternal, () => new ConcurrentDictionary<string, double>());
			}
			set
			{
				this.measurementsInternal = value;
			}
		}

		// Token: 0x04000224 RID: 548
		private IDictionary<string, double> measurementsInternal;
	}
}
