using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000DB RID: 219
	[Obsolete("Session state events are no longer used. This telemetry item will be sent as EventTelemetry.")]
	public sealed class SessionStateTelemetry : ITelemetry, IAiSerializableTelemetry
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0001A2B3 File Offset: 0x000184B3
		public SessionStateTelemetry()
			: this(SessionState.Start)
		{
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001A2BC File Offset: 0x000184BC
		public SessionStateTelemetry(SessionState state)
		{
			this.startEventName = "Session started";
			this.endEventName = "Session ended";
			base..ctor();
			this.Data = new EventTelemetry();
			this.State = state;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001A2EC File Offset: 0x000184EC
		private SessionStateTelemetry(SessionStateTelemetry source)
		{
			this.startEventName = "Session started";
			this.endEventName = "Session ended";
			base..ctor();
			this.Data = (EventTelemetry)source.Data.DeepClone();
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001A320 File Offset: 0x00018520
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return ((IAiSerializableTelemetry)this.Data).TelemetryName;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001A32D File Offset: 0x0001852D
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return ((IAiSerializableTelemetry)this.Data).BaseType;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001A33A File Offset: 0x0001853A
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001A347 File Offset: 0x00018547
		public DateTimeOffset Timestamp
		{
			get
			{
				return this.Data.Timestamp;
			}
			set
			{
				this.Data.Timestamp = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001A355 File Offset: 0x00018555
		public TelemetryContext Context
		{
			get
			{
				return this.Data.Context;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001A362 File Offset: 0x00018562
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0001A36F File Offset: 0x0001856F
		public IExtension Extension
		{
			get
			{
				return this.Data.Extension;
			}
			set
			{
				this.Data.Extension = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001A37D File Offset: 0x0001857D
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0001A38A File Offset: 0x0001858A
		public string Sequence
		{
			get
			{
				return this.Data.Sequence;
			}
			set
			{
				this.Data.Sequence = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001A398 File Offset: 0x00018598
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001A3B5 File Offset: 0x000185B5
		public SessionState State
		{
			get
			{
				if (this.Data.Name == this.startEventName)
				{
					return SessionState.Start;
				}
				return SessionState.End;
			}
			set
			{
				if (value == SessionState.Start)
				{
					this.Data.Name = this.startEventName;
					return;
				}
				this.Data.Name = this.endEventName;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001A3DD File Offset: 0x000185DD
		public ITelemetry DeepClone()
		{
			return new SessionStateTelemetry(this);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001A3E5 File Offset: 0x000185E5
		void ITelemetry.Sanitize()
		{
			((ITelemetry)this.Data).Sanitize();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001A3F2 File Offset: 0x000185F2
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			this.Data.SerializeData(serializationWriter);
		}

		// Token: 0x04000300 RID: 768
		internal readonly EventTelemetry Data;

		// Token: 0x04000301 RID: 769
		private readonly string startEventName;

		// Token: 0x04000302 RID: 770
		private readonly string endEventName;
	}
}
