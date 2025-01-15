using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;

namespace Microsoft.DataShaping.Engine.QueryTranslation
{
	// Token: 0x02000024 RID: 36
	public sealed class TranslateSemanticQueryResult
	{
		// Token: 0x060000DB RID: 219 RVA: 0x000037B5 File Offset: 0x000019B5
		private TranslateSemanticQueryResult(TranslatedQuery translatedQuery, DataShapeEngineErrorInfo errorInfo)
		{
			this.TranslatedQuery = translatedQuery;
			this.ErrorInfo = errorInfo;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000037CB File Offset: 0x000019CB
		public static TranslateSemanticQueryResult ForSuccess(TranslatedQuery translatedQuery)
		{
			return new TranslateSemanticQueryResult(translatedQuery, null);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000037D4 File Offset: 0x000019D4
		public static TranslateSemanticQueryResult ForError(TranslatedQuery translatedQuery, DataShapeEngineErrorInfo errorInfo)
		{
			return new TranslateSemanticQueryResult(translatedQuery, errorInfo);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000037DD File Offset: 0x000019DD
		public bool Succeeded
		{
			get
			{
				return this.ErrorInfo == null;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000037E8 File Offset: 0x000019E8
		public TranslatedQuery TranslatedQuery { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000037F0 File Offset: 0x000019F0
		public DataShapeEngineErrorInfo ErrorInfo { get; }
	}
}
