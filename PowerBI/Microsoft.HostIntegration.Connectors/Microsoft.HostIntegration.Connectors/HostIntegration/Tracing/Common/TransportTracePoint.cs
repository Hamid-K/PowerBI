using System;
using Microsoft.HostIntegration.Tracing.TiHip;
using Microsoft.HostIntegration.Tracing.TiWip;

namespace Microsoft.HostIntegration.Tracing.Common
{
	// Token: 0x020006D6 RID: 1750
	public class TransportTracePoint : FlagBasedTracePoint
	{
		// Token: 0x06003863 RID: 14435 RVA: 0x000BC094 File Offset: 0x000BA294
		protected TransportTracePoint()
		{
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000BD5B2 File Offset: 0x000BB7B2
		public TransportTracePoint(TBGenTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.Transport))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.Transport);
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000BD5B2 File Offset: 0x000BB7B2
		public TransportTracePoint(HipGenTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.Transport))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.Transport);
		}

		// Token: 0x17000C80 RID: 3200
		public virtual object this[TransportPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				if (this.mapProperty == null)
				{
					throw new InvalidProgramException("No Mapping set up for properties");
				}
				if (propertyIdentifier >= (TransportPropertyIdentifiers)this.mapProperty.Length)
				{
					throw new InvalidProgramException("Property identifier is not valid");
				}
				int num = this.mapProperty[(int)propertyIdentifier];
				if (num == -1)
				{
					throw new InvalidProgramException("Property identifier cannot be mapped to one valid in this context");
				}
				base[num] = value;
			}
		}

		// Token: 0x04002092 RID: 8338
		private int[] mapProperty;
	}
}
