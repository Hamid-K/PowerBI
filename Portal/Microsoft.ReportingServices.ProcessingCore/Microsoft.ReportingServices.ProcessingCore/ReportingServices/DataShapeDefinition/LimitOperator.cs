using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200059F RID: 1439
	[DataContract]
	[KnownType(typeof(TopLimitOperator))]
	[KnownType(typeof(SampleLimitOperator))]
	[KnownType(typeof(BottomLimitOperator))]
	internal abstract class LimitOperator
	{
		// Token: 0x060051FE RID: 20990
		public abstract DataShapeLimitOperator TranslateToRIF();
	}
}
