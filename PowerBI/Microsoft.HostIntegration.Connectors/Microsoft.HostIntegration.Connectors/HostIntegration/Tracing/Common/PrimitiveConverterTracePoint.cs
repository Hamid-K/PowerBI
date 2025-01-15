using System;
using Microsoft.HostIntegration.Tracing.DrdaAs;

namespace Microsoft.HostIntegration.Tracing.Common
{
	// Token: 0x020006D2 RID: 1746
	public class PrimitiveConverterTracePoint : FlagBasedTracePoint
	{
		// Token: 0x0600385C RID: 14428 RVA: 0x000BC094 File Offset: 0x000BA294
		protected PrimitiveConverterTracePoint()
		{
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000BD52A File Offset: 0x000BB72A
		public PrimitiveConverterTracePoint(AggregateConverterTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.PrimitiveConverter))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.PrimitiveConverter);
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000BD52A File Offset: 0x000BB72A
		public PrimitiveConverterTracePoint(DataConverterTracePoint parentTracePoint)
			: base(parentTracePoint, parentTracePoint.TraceContainer.MapSharedTracePointToSpecific(SharedTracePoints.PrimitiveConverter))
		{
			this.mapProperty = parentTracePoint.TraceContainer.SharedPropertiesToSpecific(SharedTracePoints.PrimitiveConverter);
		}

		// Token: 0x17000C7E RID: 3198
		public virtual object this[PrimitiveConverterPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				if (this.mapProperty == null)
				{
					throw new InvalidProgramException("No Mapping set up for properties");
				}
				if (propertyIdentifier >= (PrimitiveConverterPropertyIdentifiers)this.mapProperty.Length)
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

		// Token: 0x04002082 RID: 8322
		private int[] mapProperty;
	}
}
