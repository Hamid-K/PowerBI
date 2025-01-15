using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000076 RID: 118
	public abstract class OperationTelemetry : ITelemetry, ISupportMetrics, ISupportProperties
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000101E4 File Offset: 0x0000E3E4
		// (set) Token: 0x0600039B RID: 923 RVA: 0x000101EC File Offset: 0x0000E3EC
		[Obsolete("Use Timestamp")]
		public DateTimeOffset StartTime
		{
			get
			{
				return this.Timestamp;
			}
			set
			{
				this.Timestamp = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600039C RID: 924
		// (set) Token: 0x0600039D RID: 925
		public abstract string Id { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600039E RID: 926
		// (set) Token: 0x0600039F RID: 927
		public abstract string Name { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003A0 RID: 928
		// (set) Token: 0x060003A1 RID: 929
		public abstract bool? Success { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003A2 RID: 930
		// (set) Token: 0x060003A3 RID: 931
		public abstract TimeSpan Duration { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A4 RID: 932
		public abstract IDictionary<string, double> Metrics { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003A5 RID: 933
		public abstract IDictionary<string, string> Properties { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003A6 RID: 934
		// (set) Token: 0x060003A7 RID: 935
		public abstract DateTimeOffset Timestamp { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003A8 RID: 936
		public abstract TelemetryContext Context { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003A9 RID: 937
		// (set) Token: 0x060003AA RID: 938
		public abstract string Sequence { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003AB RID: 939
		// (set) Token: 0x060003AC RID: 940
		public abstract IExtension Extension { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000101F5 File Offset: 0x0000E3F5
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000101FD File Offset: 0x0000E3FD
		internal long BeginTimeInTicks { get; set; }

		// Token: 0x060003AF RID: 943 RVA: 0x00010206 File Offset: 0x0000E406
		void ITelemetry.Sanitize()
		{
			this.Sanitize();
		}

		// Token: 0x060003B0 RID: 944
		public abstract ITelemetry DeepClone();

		// Token: 0x060003B1 RID: 945
		public abstract void SerializeData(ISerializationWriter serializationWriter);

		// Token: 0x060003B2 RID: 946 RVA: 0x0001020E File Offset: 0x0000E40E
		internal void GenerateId()
		{
			this.Id = Convert.ToBase64String(BitConverter.GetBytes(WeakConcurrentRandom.Instance.Next()));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001022A File Offset: 0x0000E42A
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is expected to be overloaded")]
		protected void Sanitize()
		{
		}

		// Token: 0x0400017C RID: 380
		internal const string TelemetryName = "Operation";
	}
}
