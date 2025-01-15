using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C5 RID: 709
	internal sealed class GetDocumentMapAction : SnapshotUpdatingEvent<NoEventParameters, Microsoft.ReportingServices.Library.Soap2005.DocumentMapNode>
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x00066624 File Offset: 0x00064824
		public GetDocumentMapAction(ClientRequest session, RSService service)
			: base(session, service, null)
		{
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00066630 File Offset: 0x00064830
		protected override Microsoft.ReportingServices.Library.Soap2005.DocumentMapNode RunEvent(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc)
		{
			OnDemandProcessingResult onDemandProcessingResult;
			Microsoft.ReportingServices.Library.Soap2005.DocumentMapNode documentMapNode;
			using (IDocumentMap documentMap = base.ProcessingEngine.GetDocumentMap(base.Session.SessionReport.EventInfo, pc, out onDemandProcessingResult))
			{
				base.ProcessingResult = onDemandProcessingResult;
				documentMapNode = Microsoft.ReportingServices.Library.Soap2005.DocumentMapNode.CollectionToSoapStruct(documentMap);
			}
			return documentMapNode;
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0002AD4A File Offset: 0x00028F4A
		internal override ReportEventType EventType
		{
			get
			{
				return ReportEventType.GetDocumentMap;
			}
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00066688 File Offset: 0x00064888
		internal override void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			base.AddExecutionInfo(execInfo);
		}
	}
}
