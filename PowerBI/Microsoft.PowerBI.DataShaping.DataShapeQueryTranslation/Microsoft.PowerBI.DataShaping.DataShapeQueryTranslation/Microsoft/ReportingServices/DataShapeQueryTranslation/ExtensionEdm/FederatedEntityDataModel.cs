using System;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm
{
	// Token: 0x020000A8 RID: 168
	public sealed class FederatedEntityDataModel
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x0001D734 File Offset: 0x0001B934
		internal FederatedEntityDataModel(EntityDataModel baseModel, ExtensionEntityDataModel extensionModel)
		{
			this.m_baseModel = baseModel;
			this.m_extensionModel = extensionModel;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001D74A File Offset: 0x0001B94A
		public EntityDataModel BaseModel
		{
			get
			{
				return this.m_baseModel;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001D752 File Offset: 0x0001B952
		public ExtensionEntityDataModel ExtensionModel
		{
			get
			{
				return this.m_extensionModel;
			}
		}

		// Token: 0x040003CD RID: 973
		private readonly EntityDataModel m_baseModel;

		// Token: 0x040003CE RID: 974
		private readonly ExtensionEntityDataModel m_extensionModel;
	}
}
