using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000010 RID: 16
	public class MeterCollector
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000035BC File Offset: 0x000017BC
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000035E8 File Offset: 0x000017E8
		public static MeterCollector Global
		{
			get
			{
				if (MeterCollector._global == null)
				{
					Logger.Info("Creating default Global MeterCollector", Array.Empty<object>());
					MeterCollector._global = new MeterCollector("Global");
				}
				return MeterCollector._global;
			}
			set
			{
				MeterCollector._global = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000035F0 File Offset: 0x000017F0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000035F8 File Offset: 0x000017F8
		public ReadOnlyDictionary<string, MeterCollector.MeterData> MeterDataDictionary
		{
			get
			{
				return new ReadOnlyDictionary<string, MeterCollector.MeterData>(this._meters);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003605 File Offset: 0x00001805
		public MeterCollector(string name)
		{
			this._name = name;
			this._meters = new ConcurrentDictionary<string, MeterCollector.MeterData>();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000361F File Offset: 0x0000181F
		public MeterCollector.MeterFactory Factory(string name)
		{
			return new MeterCollector.MeterFactory(this.GetOrCreateMeterData(name));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000362D File Offset: 0x0000182D
		public IDisposable Meter(string name)
		{
			return new TimeMeter(this.GetOrCreateMeterData(name));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000363C File Offset: 0x0000183C
		private MeterCollector.MeterData GetOrCreateMeterData(string name)
		{
			MeterCollector.MeterData meterData;
			if (!this.MeterDataDictionary.TryGetValue(name, out meterData))
			{
				meterData = new MeterCollector.MeterData(name);
				this._meters[name] = meterData;
			}
			return meterData;
		}

		// Token: 0x0400006A RID: 106
		private static MeterCollector _global;

		// Token: 0x0400006B RID: 107
		private readonly string _name;

		// Token: 0x0400006C RID: 108
		private readonly ConcurrentDictionary<string, MeterCollector.MeterData> _meters;

		// Token: 0x02000057 RID: 87
		public class MeterData
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006A75 File Offset: 0x00004C75
			public long TotalTicks
			{
				get
				{
					return Thread.VolatileRead(ref this._totalTicks);
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006A82 File Offset: 0x00004C82
			public double TotalMillis
			{
				get
				{
					return (double)(this.TotalTicks / 10000L);
				}
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x00006A92 File Offset: 0x00004C92
			public override string ToString()
			{
				return string.Format("{0} = {1}ms", this.Name, this.TotalMillis);
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x00006AAF File Offset: 0x00004CAF
			public long Throughput(long operations)
			{
				return operations * 10000000L / this.TotalTicks;
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006AC0 File Offset: 0x00004CC0
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x00006AC8 File Offset: 0x00004CC8
			public MeterData(string name)
			{
				this._name = name;
				this._totalTicks = 0L;
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x00006ADF File Offset: 0x00004CDF
			public void AddTicks(long toAdd)
			{
				Interlocked.Add(ref this._totalTicks, toAdd);
			}

			// Token: 0x0400013B RID: 315
			private readonly string _name;

			// Token: 0x0400013C RID: 316
			private long _totalTicks;
		}

		// Token: 0x02000058 RID: 88
		public class MeterFactory
		{
			// Token: 0x060001EA RID: 490 RVA: 0x00006AEE File Offset: 0x00004CEE
			internal MeterFactory(MeterCollector.MeterData data)
			{
				this._data = data;
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060001EB RID: 491 RVA: 0x00006AFD File Offset: 0x00004CFD
			public MeterCollector.MeterData Data
			{
				get
				{
					return this._data;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060001EC RID: 492 RVA: 0x00006B05 File Offset: 0x00004D05
			public string Name
			{
				get
				{
					return this._data.Name;
				}
			}

			// Token: 0x060001ED RID: 493 RVA: 0x00006B12 File Offset: 0x00004D12
			public TimeMeter Get()
			{
				return new TimeMeter(this._data);
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00006B1F File Offset: 0x00004D1F
			public TimeMeter LoggedGet()
			{
				Logger.Info(this._data.Name, Array.Empty<object>());
				return new TimeMeter(this._data);
			}

			// Token: 0x0400013D RID: 317
			private readonly MeterCollector.MeterData _data;
		}
	}
}
