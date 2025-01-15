using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	internal sealed class ModelRetrievalAbortedException : ReportCatalogException
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x000046D3 File Offset: 0x000028D3
		public ModelRetrievalAbortedException(ModelRetrievalAbortedException.CancelationTrigger cancelationTrigger)
			: base(ErrorCode.rsModelRetrievalCanceled, ErrorStringsWrapper.rsModelRetrievalCanceled, null, ModelRetrievalAbortedException.CreateAdditionalTraceMessage(cancelationTrigger), Array.Empty<object>())
		{
			this.m_cancelationTrigger = cancelationTrigger;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000046F8 File Offset: 0x000028F8
		private ModelRetrievalAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00004702 File Offset: 0x00002902
		internal ModelRetrievalAbortedException.CancelationTrigger Trigger
		{
			get
			{
				return this.m_cancelationTrigger;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000470A File Offset: 0x0000290A
		private static string CreateAdditionalTraceMessage(ModelRetrievalAbortedException.CancelationTrigger trigger)
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0}]", trigger.ToString());
		}

		// Token: 0x04000014 RID: 20
		private readonly ModelRetrievalAbortedException.CancelationTrigger m_cancelationTrigger;

		// Token: 0x020000D6 RID: 214
		internal enum CancelationTrigger
		{
			// Token: 0x0400007A RID: 122
			None,
			// Token: 0x0400007B RID: 123
			ModelResolutionAfterConnectionOpen,
			// Token: 0x0400007C RID: 124
			ModelResolutionBeforeConnectionOpen,
			// Token: 0x0400007D RID: 125
			ModelResolutionDuringConnectionOpenException,
			// Token: 0x0400007E RID: 126
			ModelResolutionDuringConnectionOpenRSException,
			// Token: 0x0400007F RID: 127
			ModelResolutionDuringModelResolution,
			// Token: 0x04000080 RID: 128
			ModelResolutionDuringModelResolutionException,
			// Token: 0x04000081 RID: 129
			ModelResolutionDuringModelResolutionRSException,
			// Token: 0x04000082 RID: 130
			ServerDataSourceResolverAfterDataSourceResolution,
			// Token: 0x04000083 RID: 131
			ServerDataSourceResolverBeforeDataSourceResolution,
			// Token: 0x04000084 RID: 132
			ServerDataSourceResolverDuringModelResolution,
			// Token: 0x04000085 RID: 133
			ServerDataSourceResolverDuringModelResolutionException,
			// Token: 0x04000086 RID: 134
			ServerDataSourceResolverDuringModelResolutionRSException
		}
	}
}
