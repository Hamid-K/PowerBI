using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000CA RID: 202
	internal sealed class DataSourceValidator
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x00021149 File Offset: 0x0001F349
		private DataSourceValidator(TranslationErrorContext errorContext)
		{
			this.m_errorContext = errorContext;
			this.m_idValidator = new IdentifierValidator(errorContext);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00021164 File Offset: 0x0001F364
		public IdentifierValidator IdentifierValidator
		{
			get
			{
				return this.m_idValidator;
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002116C File Offset: 0x0001F36C
		private void ValidateDataSource(DataSource ds)
		{
			this.m_idValidator.ValidateId(ds);
			if (ds.DataSourceReference == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ds.ObjectType, ds.Id, "DataSourceReference"));
				return;
			}
			if (string.IsNullOrEmpty(ds.DataSourceReference.ItemPath))
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ds.ObjectType, ds.Id, "ItemPath"));
			}
			if (ds.DataSourceReference.DataSourceName != null && ds.DataSourceReference.DataSourceName == string.Empty)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ds.ObjectType, ds.Id, "DataSourceName"));
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002122C File Offset: 0x0001F42C
		public static void Validate(List<DataSource> dataSources, TranslationErrorContext errorContext)
		{
			if (dataSources == null)
			{
				return;
			}
			DataSourceValidator dataSourceValidator = new DataSourceValidator(errorContext);
			foreach (DataSource dataSource in dataSources)
			{
				dataSourceValidator.ValidateDataSource(dataSource);
			}
		}

		// Token: 0x04000427 RID: 1063
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000428 RID: 1064
		private readonly IdentifierValidator m_idValidator;
	}
}
