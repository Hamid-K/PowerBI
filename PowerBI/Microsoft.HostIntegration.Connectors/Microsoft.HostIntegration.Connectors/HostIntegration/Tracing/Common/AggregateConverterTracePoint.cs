using System;
using Microsoft.HostIntegration.Tracing.ConversionPipeline;
using Microsoft.HostIntegration.Tracing.Ffp;
using Microsoft.HostIntegration.Tracing.HostFiles;
using Microsoft.HostIntegration.Tracing.TiHip;
using Microsoft.HostIntegration.Tracing.TiWip;

namespace Microsoft.HostIntegration.Tracing.Common
{
	// Token: 0x020006CE RID: 1742
	public class AggregateConverterTracePoint : FlagBasedTracePoint
	{
		// Token: 0x06003852 RID: 14418 RVA: 0x000BC094 File Offset: 0x000BA294
		protected AggregateConverterTracePoint()
		{
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000BD4A3 File Offset: 0x000BB6A3
		public AggregateConverterTracePoint(TBGenTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.AggregateConverter))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.AggregateConverter);
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000BD4A3 File Offset: 0x000BB6A3
		public AggregateConverterTracePoint(HipGenTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.AggregateConverter))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.AggregateConverter);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000BD4A3 File Offset: 0x000BB6A3
		public AggregateConverterTracePoint(HostFileClientTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.AggregateConverter))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.AggregateConverter);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000BD4A3 File Offset: 0x000BB6A3
		public AggregateConverterTracePoint(PipelineTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.AggregateConverter))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.AggregateConverter);
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000BD4A3 File Offset: 0x000BB6A3
		public AggregateConverterTracePoint(FlatFileProcessorTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.AggregateConverter))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.AggregateConverter);
		}

		// Token: 0x17000C7C RID: 3196
		public virtual object this[AggregateConverterPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				if (this.mapProperty == null)
				{
					throw new InvalidProgramException("No Mapping set up for properties");
				}
				if (propertyIdentifier >= (AggregateConverterPropertyIdentifiers)this.mapProperty.Length)
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

		// Token: 0x0400207B RID: 8315
		private int[] mapProperty;
	}
}
