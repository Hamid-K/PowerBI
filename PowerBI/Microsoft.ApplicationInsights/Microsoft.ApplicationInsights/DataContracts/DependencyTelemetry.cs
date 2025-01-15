using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000CA RID: 202
	public sealed class DependencyTelemetry : OperationTelemetry, ITelemetry, ISupportProperties, ISupportSampling, ISupportMetrics, IAiSerializableTelemetry
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0001802C File Offset: 0x0001622C
		public DependencyTelemetry()
		{
			this.successFieldSet = true;
			this.context = new TelemetryContext();
			base.GenerateId();
			this.Name = string.Empty;
			this.ResultCode = string.Empty;
			this.Duration = TimeSpan.Zero;
			this.Target = string.Empty;
			this.Type = string.Empty;
			this.Data = string.Empty;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000180A0 File Offset: 0x000162A0
		[Obsolete("Use other constructors which allows to define dependency call with all the properties.")]
		public DependencyTelemetry(string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
			: this()
		{
			this.Name = dependencyName;
			this.Data = data;
			this.Duration = duration;
			this.Success = new bool?(success);
			this.Timestamp = startTime;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000180D2 File Offset: 0x000162D2
		public DependencyTelemetry(string dependencyTypeName, string target, string dependencyName, string data)
			: this()
		{
			this.Type = dependencyTypeName;
			this.Target = target;
			this.Name = dependencyName;
			this.Data = data;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000180F8 File Offset: 0x000162F8
		public DependencyTelemetry(string dependencyTypeName, string target, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, string resultCode, bool success)
			: this()
		{
			this.Type = dependencyTypeName;
			this.Target = target;
			this.Name = dependencyName;
			this.Data = data;
			this.Timestamp = startTime;
			this.Duration = duration;
			this.ResultCode = resultCode;
			this.Success = new bool?(success);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00018150 File Offset: 0x00016350
		private DependencyTelemetry(DependencyTelemetry source)
		{
			if (source.measurementsValue != null)
			{
				Utils.CopyDictionary<double>(source.Metrics, this.Metrics);
			}
			this.context = source.context.DeepClone();
			this.Sequence = source.Sequence;
			this.Timestamp = source.Timestamp;
			this.samplingPercentage = source.samplingPercentage;
			this.successFieldSet = source.successFieldSet;
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
			this.Name = source.Name;
			this.Id = source.Id;
			this.ResultCode = source.ResultCode;
			this.Duration = source.Duration;
			this.Success = source.Success;
			this.Data = source.Data;
			this.Target = source.Target;
			this.Type = source.Type;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001823C File Offset: 0x0001643C
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "RemoteDependency";
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00018243 File Offset: 0x00016443
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "RemoteDependencyData";
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001824A File Offset: 0x0001644A
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00018252 File Offset: 0x00016452
		public override DateTimeOffset Timestamp { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001825B File Offset: 0x0001645B
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00018263 File Offset: 0x00016463
		public override string Sequence { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001826C File Offset: 0x0001646C
		public override TelemetryContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00018274 File Offset: 0x00016474
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001827C File Offset: 0x0001647C
		public override IExtension Extension
		{
			get
			{
				return this.extension;
			}
			set
			{
				this.extension = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00018285 File Offset: 0x00016485
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001828D File Offset: 0x0001648D
		public override string Id { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00018296 File Offset: 0x00016496
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001829E File Offset: 0x0001649E
		public string ResultCode { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000182A7 File Offset: 0x000164A7
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x000182AF File Offset: 0x000164AF
		public override string Name { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000182B8 File Offset: 0x000164B8
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x000182C0 File Offset: 0x000164C0
		[Obsolete("Renamed to Data")]
		public string CommandName
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x000182C9 File Offset: 0x000164C9
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x000182D1 File Offset: 0x000164D1
		public string Data { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x000182DA File Offset: 0x000164DA
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x000182E2 File Offset: 0x000164E2
		public string Target { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x000182EB File Offset: 0x000164EB
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x000182F3 File Offset: 0x000164F3
		[Obsolete("Renamed to Type")]
		public string DependencyTypeName
		{
			get
			{
				return this.Type;
			}
			set
			{
				this.Type = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000182FC File Offset: 0x000164FC
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x00018304 File Offset: 0x00016504
		public string Type { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001830D File Offset: 0x0001650D
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x00018315 File Offset: 0x00016515
		public override TimeSpan Duration { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00018320 File Offset: 0x00016520
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0001834A File Offset: 0x0001654A
		public override bool? Success
		{
			get
			{
				if (this.successFieldSet)
				{
					return new bool?(this.success);
				}
				return null;
			}
			set
			{
				if (value != null && value != null)
				{
					this.success = value.Value;
					this.successFieldSet = true;
					return;
				}
				this.success = true;
				this.successFieldSet = false;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00018384 File Offset: 0x00016584
		public override IDictionary<string, string> Properties
		{
			get
			{
				if (!string.IsNullOrEmpty(this.MetricExtractorInfo) && !this.Context.Properties.ContainsKey("_MS.ProcessedByMetricExtractors"))
				{
					this.Context.Properties["_MS.ProcessedByMetricExtractors"] = this.MetricExtractorInfo;
				}
				return this.Context.Properties;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000183DB File Offset: 0x000165DB
		public override IDictionary<string, double> Metrics
		{
			get
			{
				return LazyInitializer.EnsureInitialized<IDictionary<string, double>>(ref this.measurementsValue, () => new ConcurrentDictionary<string, double>());
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00018407 File Offset: 0x00016607
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0001840F File Offset: 0x0001660F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Type")]
		public string DependencyKind
		{
			get
			{
				return this.DependencyTypeName;
			}
			set
			{
				this.DependencyTypeName = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00018418 File Offset: 0x00016618
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x00018420 File Offset: 0x00016620
		double? ISupportSampling.SamplingPercentage
		{
			get
			{
				return this.samplingPercentage;
			}
			set
			{
				this.samplingPercentage = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00018429 File Offset: 0x00016629
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00018431 File Offset: 0x00016631
		internal string MetricExtractorInfo { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001843A File Offset: 0x0001663A
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x00018453 File Offset: 0x00016653
		internal RemoteDependencyData InternalData
		{
			get
			{
				return LazyInitializer.EnsureInitialized<RemoteDependencyData>(ref this.internalDataPrivate, () => new RemoteDependencyData
				{
					duration = this.Duration,
					id = this.Id,
					measurements = this.measurementsValue,
					name = this.Name,
					properties = this.context.PropertiesValue,
					resultCode = this.ResultCode,
					target = this.Target,
					success = this.success,
					data = this.Data,
					type = this.Type
				});
			}
			private set
			{
				this.internalDataPrivate = value;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001845C File Offset: 0x0001665C
		public override ITelemetry DeepClone()
		{
			return new DependencyTelemetry(this);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00018464 File Offset: 0x00016664
		public bool TryGetOperationDetail(string key, out object detail)
		{
			return this.Context.TryGetRawObject(key, out detail);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00018473 File Offset: 0x00016673
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetOperationDetail(string key, object detail)
		{
			this.Context.StoreRawObject(key, detail, true);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00018483 File Offset: 0x00016683
		public override void SerializeData(ISerializationWriter serializationWriter)
		{
			this.internalDataPrivate = null;
			serializationWriter.WriteProperty(this.InternalData);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00018498 File Offset: 0x00016698
		void ITelemetry.Sanitize()
		{
			this.Name = this.Name.SanitizeName();
			this.Name = Utils.PopulateRequiredStringValue(this.Name, "name", typeof(DependencyTelemetry).FullName);
			this.Id.SanitizeName();
			this.ResultCode = this.ResultCode.SanitizeResultCode();
			this.Type = this.Type.SanitizeDependencyType();
			this.Data = this.Data.SanitizeData();
			this.Properties.SanitizeProperties();
			this.Metrics.SanitizeMeasurements();
		}

		// Token: 0x040002AB RID: 683
		internal new const string TelemetryName = "RemoteDependency";

		// Token: 0x040002AC RID: 684
		private readonly TelemetryContext context;

		// Token: 0x040002AD RID: 685
		private IExtension extension;

		// Token: 0x040002AE RID: 686
		private double? samplingPercentage;

		// Token: 0x040002AF RID: 687
		private bool successFieldSet;

		// Token: 0x040002B0 RID: 688
		private bool success = true;

		// Token: 0x040002B1 RID: 689
		private IDictionary<string, double> measurementsValue;

		// Token: 0x040002B2 RID: 690
		private RemoteDependencyData internalDataPrivate;
	}
}
