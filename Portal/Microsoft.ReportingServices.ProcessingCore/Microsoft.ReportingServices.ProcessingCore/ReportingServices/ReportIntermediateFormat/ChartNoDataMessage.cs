using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000484 RID: 1156
	[Serializable]
	internal class ChartNoDataMessage : ChartTitle
	{
		// Token: 0x060035DA RID: 13786 RVA: 0x000EBC15 File Offset: 0x000E9E15
		internal ChartNoDataMessage()
		{
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000EBC1D File Offset: 0x000E9E1D
		internal ChartNoDataMessage(Chart chart)
			: base(chart)
		{
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000EBC26 File Offset: 0x000E9E26
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartNoDataMessageStart();
			base.InitializeInternal(context);
			context.ExprHostBuilder.ChartNoDataMessageEnd();
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000EBC47 File Offset: 0x000E9E47
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartNoDataMessage;
		}
	}
}
