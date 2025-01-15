using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000C9 RID: 201
	public sealed class AvailabilityTelemetry : ITelemetry, ISupportProperties, ISupportMetrics, IAiSerializableTelemetry
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x00017D14 File Offset: 0x00015F14
		public AvailabilityTelemetry()
		{
			this.Data = new AvailabilityData();
			this.context = new TelemetryContext(this.Data.properties);
			this.Data.id = Convert.ToBase64String(BitConverter.GetBytes(WeakConcurrentRandom.Instance.Next()));
			this.Success = true;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00017D70 File Offset: 0x00015F70
		public AvailabilityTelemetry(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation, bool success, string message = null)
			: this()
		{
			this.Data.name = name;
			this.Data.duration = duration;
			this.Data.success = success;
			this.Data.runLocation = runLocation;
			this.Data.message = message;
			this.Timestamp = timeStamp;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00017DCC File Offset: 0x00015FCC
		private AvailabilityTelemetry(AvailabilityTelemetry source)
		{
			this.Data = source.Data.DeepClone();
			this.context = source.context.DeepClone(this.Data.properties);
			this.Sequence = source.Sequence;
			this.Timestamp = source.Timestamp;
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00017E3C File Offset: 0x0001603C
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "Availability";
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00017E43 File Offset: 0x00016043
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "AvailabilityData";
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00017E4A File Offset: 0x0001604A
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00017E57 File Offset: 0x00016057
		public string Id
		{
			get
			{
				return this.Data.id;
			}
			set
			{
				this.Data.id = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00017E65 File Offset: 0x00016065
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00017E72 File Offset: 0x00016072
		public string Name
		{
			get
			{
				return this.Data.name;
			}
			set
			{
				this.Data.name = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00017E80 File Offset: 0x00016080
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00017E8D File Offset: 0x0001608D
		public TimeSpan Duration
		{
			get
			{
				return this.Data.duration;
			}
			set
			{
				this.Data.duration = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00017E9B File Offset: 0x0001609B
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00017EA8 File Offset: 0x000160A8
		public bool Success
		{
			get
			{
				return this.Data.success;
			}
			set
			{
				this.Data.success = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00017EB6 File Offset: 0x000160B6
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00017EC3 File Offset: 0x000160C3
		public string RunLocation
		{
			get
			{
				return this.Data.runLocation;
			}
			set
			{
				this.Data.runLocation = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00017ED1 File Offset: 0x000160D1
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00017EDE File Offset: 0x000160DE
		public string Message
		{
			get
			{
				return this.Data.message;
			}
			set
			{
				this.Data.message = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00017EEC File Offset: 0x000160EC
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00017EF4 File Offset: 0x000160F4
		public string Sequence { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00017EFD File Offset: 0x000160FD
		public TelemetryContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00017F05 File Offset: 0x00016105
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00017F0D File Offset: 0x0001610D
		public IExtension Extension
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

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00017F16 File Offset: 0x00016116
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.properties;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00017F23 File Offset: 0x00016123
		public IDictionary<string, double> Metrics
		{
			get
			{
				return this.Data.measurements;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00017F30 File Offset: 0x00016130
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x00017F38 File Offset: 0x00016138
		public DateTimeOffset Timestamp { get; set; }

		// Token: 0x060006AC RID: 1708 RVA: 0x00017F41 File Offset: 0x00016141
		public ITelemetry DeepClone()
		{
			return new AvailabilityTelemetry(this);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00017F49 File Offset: 0x00016149
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty(this.Data);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00017F58 File Offset: 0x00016158
		void ITelemetry.Sanitize()
		{
			this.Message = ((this.Data.success && string.IsNullOrEmpty(this.Message)) ? "Passed" : ((!this.Data.success && string.IsNullOrEmpty(this.Message)) ? "Failed" : this.Message));
			this.Name = this.Name.SanitizeTestName();
			this.Name = Utils.PopulateRequiredStringValue(this.Name, "TestName", typeof(AvailabilityTelemetry).FullName);
			this.RunLocation = this.RunLocation.SanitizeRunLocation();
			this.Message = this.Message.SanitizeAvailabilityMessage();
			this.Data.properties.SanitizeProperties();
			this.Data.measurements.SanitizeMeasurements();
		}

		// Token: 0x040002A5 RID: 677
		internal const string TelemetryName = "Availability";

		// Token: 0x040002A6 RID: 678
		internal readonly AvailabilityData Data;

		// Token: 0x040002A7 RID: 679
		private readonly TelemetryContext context;

		// Token: 0x040002A8 RID: 680
		private IExtension extension;
	}
}
