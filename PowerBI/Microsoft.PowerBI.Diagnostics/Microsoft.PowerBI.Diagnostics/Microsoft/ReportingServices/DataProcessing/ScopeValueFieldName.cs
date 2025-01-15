using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000032 RID: 50
	internal class ScopeValueFieldName
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000037D0 File Offset: 0x000019D0
		internal ScopeValueFieldName(string fieldName, object value)
		{
			this.m_fieldName = fieldName;
			this.m_value = value;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000037E6 File Offset: 0x000019E6
		internal object ScopeValue
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000037EE File Offset: 0x000019EE
		internal string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x04000067 RID: 103
		private readonly string m_fieldName;

		// Token: 0x04000068 RID: 104
		private readonly object m_value;
	}
}
