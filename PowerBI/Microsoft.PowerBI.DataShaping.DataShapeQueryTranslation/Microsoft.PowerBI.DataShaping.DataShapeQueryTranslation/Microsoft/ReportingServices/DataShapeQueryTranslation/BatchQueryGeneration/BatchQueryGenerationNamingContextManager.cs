using System;
using Microsoft.DataShaping;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200013F RID: 319
	internal sealed class BatchQueryGenerationNamingContextManager
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002F218 File Offset: 0x0002D418
		internal BatchQueryGenerationNamingContext GetOrCreateNamingContext()
		{
			if (this.m_activeNamingContext == null)
			{
				return new BatchQueryGenerationNamingContext();
			}
			return this.m_activeNamingContext;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002F22E File Offset: 0x0002D42E
		internal void SetActiveNamingContext(BatchQueryGenerationNamingContext namingContext)
		{
			Contract.RetailAssert(this.m_activeNamingContext == null, "There already is an activeNamingContext");
			this.m_activeNamingContext = namingContext;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002F24A File Offset: 0x0002D44A
		internal void ResetActiveNamingContext()
		{
			this.m_activeNamingContext = null;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0002F253 File Offset: 0x0002D453
		internal bool HasActiveNamingContext
		{
			get
			{
				return this.m_activeNamingContext != null;
			}
		}

		// Token: 0x040005F2 RID: 1522
		private BatchQueryGenerationNamingContext m_activeNamingContext;
	}
}
