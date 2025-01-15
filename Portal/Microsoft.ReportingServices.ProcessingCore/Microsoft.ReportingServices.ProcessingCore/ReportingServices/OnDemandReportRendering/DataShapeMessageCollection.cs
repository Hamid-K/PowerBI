using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200009C RID: 156
	internal sealed class DataShapeMessageCollection : ReportElementCollectionBase<DataShapeErrorMessage>
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x0002724D File Offset: 0x0002544D
		internal DataShapeMessageCollection(List<DataShapeErrorMessage> errorMessages)
		{
			this.m_errorMessages = errorMessages;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002725C File Offset: 0x0002545C
		public void Add(DataShapeErrorMessage message)
		{
			this.m_errorMessages.Add(message);
		}

		// Token: 0x170005D9 RID: 1497
		public override DataShapeErrorMessage this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_errorMessages[index];
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000272C3 File Offset: 0x000254C3
		public override int Count
		{
			get
			{
				return this.m_errorMessages.Count;
			}
		}

		// Token: 0x04000270 RID: 624
		private readonly List<DataShapeErrorMessage> m_errorMessages;
	}
}
