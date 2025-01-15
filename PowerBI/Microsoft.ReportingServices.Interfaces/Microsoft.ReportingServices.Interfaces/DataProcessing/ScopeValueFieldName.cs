using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000020 RID: 32
	internal class ScopeValueFieldName
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002050 File Offset: 0x00000250
		internal ScopeValueFieldName(string fieldName, object value)
		{
			this.m_fieldName = fieldName;
			this.m_value = value;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002066 File Offset: 0x00000266
		internal object ScopeValue
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000206E File Offset: 0x0000026E
		internal string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x0400016A RID: 362
		private readonly string m_fieldName;

		// Token: 0x0400016B RID: 363
		private readonly object m_value;
	}
}
