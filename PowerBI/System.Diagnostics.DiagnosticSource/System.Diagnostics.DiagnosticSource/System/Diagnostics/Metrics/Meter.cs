using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200004A RID: 74
	[NullableContext(2)]
	[Nullable(0)]
	[SecuritySafeCritical]
	public class Meter : IDisposable
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000099F6 File Offset: 0x00007BF6
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000099FE File Offset: 0x00007BFE
		internal bool Disposed { get; private set; }

		// Token: 0x0600023C RID: 572 RVA: 0x00009A07 File Offset: 0x00007C07
		[NullableContext(1)]
		public Meter(string name)
			: this(name, null)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009A14 File Offset: 0x00007C14
		[NullableContext(1)]
		public Meter(string name, [Nullable(2)] string version)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.Version = version;
			object syncObject = Instrument.SyncObject;
			lock (syncObject)
			{
				Meter.s_allMeters.Add(this);
			}
			GC.KeepAlive(MetricsEventSource.Log);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009A90 File Offset: 0x00007C90
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009A98 File Offset: 0x00007C98
		public string Version { get; }

		// Token: 0x06000240 RID: 576 RVA: 0x00009AA0 File Offset: 0x00007CA0
		[return: Nullable(new byte[] { 1, 0 })]
		public Counter<T> CreateCounter<[Nullable(0)] T>([Nullable(1)] string name, string unit = null, string description = null) where T : struct
		{
			return new Counter<T>(this, name, unit, description);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009AAB File Offset: 0x00007CAB
		[return: Nullable(new byte[] { 1, 0 })]
		public Histogram<T> CreateHistogram<[Nullable(0)] T>([Nullable(1)] string name, string unit = null, string description = null) where T : struct
		{
			return new Histogram<T>(this, name, unit, description);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009AB6 File Offset: 0x00007CB6
		[return: Nullable(new byte[] { 1, 0 })]
		public ObservableCounter<T> CreateObservableCounter<[Nullable(0)] T>([Nullable(1)] string name, [Nullable(new byte[] { 1, 0 })] Func<T> observeValue, string unit = null, string description = null) where T : struct
		{
			return new ObservableCounter<T>(this, name, observeValue, unit, description);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009AC3 File Offset: 0x00007CC3
		[return: Nullable(new byte[] { 1, 0 })]
		public ObservableCounter<T> CreateObservableCounter<[Nullable(0)] T>([Nullable(1)] string name, [Nullable(new byte[] { 1, 0, 0 })] Func<Measurement<T>> observeValue, string unit = null, string description = null) where T : struct
		{
			return new ObservableCounter<T>(this, name, observeValue, unit, description);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009AD0 File Offset: 0x00007CD0
		[return: Nullable(new byte[] { 1, 0 })]
		public ObservableCounter<T> CreateObservableCounter<[Nullable(0)] T>([Nullable(1)] string name, [Nullable(new byte[] { 1, 1, 0, 0 })] Func<IEnumerable<Measurement<T>>> observeValues, string unit = null, string description = null) where T : struct
		{
			return new ObservableCounter<T>(this, name, observeValues, unit, description);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009ADD File Offset: 0x00007CDD
		[return: Nullable(new byte[] { 1, 0 })]
		public ObservableGauge<T> CreateObservableGauge<[Nullable(0)] T>([Nullable(1)] string name, [Nullable(new byte[] { 1, 0 })] Func<T> observeValue, string unit = null, string description = null) where T : struct
		{
			return new ObservableGauge<T>(this, name, observeValue, unit, description);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009AEA File Offset: 0x00007CEA
		[return: Nullable(new byte[] { 1, 0 })]
		public ObservableGauge<T> CreateObservableGauge<[Nullable(0)] T>([Nullable(1)] string name, [Nullable(new byte[] { 1, 0, 0 })] Func<Measurement<T>> observeValue, string unit = null, string description = null) where T : struct
		{
			return new ObservableGauge<T>(this, name, observeValue, unit, description);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009AF7 File Offset: 0x00007CF7
		[return: Nullable(new byte[] { 1, 0 })]
		public ObservableGauge<T> CreateObservableGauge<[Nullable(0)] T>([Nullable(1)] string name, [Nullable(new byte[] { 1, 1, 0, 0 })] Func<IEnumerable<Measurement<T>>> observeValues, string unit = null, string description = null) where T : struct
		{
			return new ObservableGauge<T>(this, name, observeValues, unit, description);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009B04 File Offset: 0x00007D04
		public void Dispose()
		{
			List<Instrument> list = null;
			object syncObject = Instrument.SyncObject;
			lock (syncObject)
			{
				if (this.Disposed)
				{
					return;
				}
				this.Disposed = true;
				Meter.s_allMeters.Remove(this);
				list = this._instruments;
				this._instruments = new List<Instrument>();
			}
			if (list != null)
			{
				foreach (Instrument instrument in list)
				{
					instrument.NotifyForUnpublishedInstrument();
				}
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009BB0 File Offset: 0x00007DB0
		internal bool AddInstrument(Instrument instrument)
		{
			if (!this._instruments.Contains(instrument))
			{
				this._instruments.Add(instrument);
				return true;
			}
			return false;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009BD0 File Offset: 0x00007DD0
		internal static List<Instrument> GetPublishedInstruments()
		{
			List<Instrument> list = null;
			if (Meter.s_allMeters.Count > 0)
			{
				list = new List<Instrument>();
				foreach (Meter meter in Meter.s_allMeters)
				{
					foreach (Instrument instrument in meter._instruments)
					{
						list.Add(instrument);
					}
				}
			}
			return list;
		}

		// Token: 0x04000101 RID: 257
		private static readonly List<Meter> s_allMeters = new List<Meter>();

		// Token: 0x04000102 RID: 258
		private List<Instrument> _instruments = new List<Instrument>();
	}
}
