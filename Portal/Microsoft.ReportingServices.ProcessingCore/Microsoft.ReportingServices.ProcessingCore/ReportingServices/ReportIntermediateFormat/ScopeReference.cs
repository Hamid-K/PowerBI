using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C8 RID: 1224
	internal sealed class ScopeReference
	{
		// Token: 0x06003E33 RID: 15923 RVA: 0x0010A35A File Offset: 0x0010855A
		public ScopeReference(string scopeName)
			: this(scopeName, null)
		{
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x0010A364 File Offset: 0x00108564
		public ScopeReference(string scopeName, string fieldName)
		{
			this.m_scopeName = scopeName;
			this.m_fieldName = fieldName;
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x0010A37A File Offset: 0x0010857A
		public string ScopeName
		{
			get
			{
				return this.m_scopeName;
			}
		}

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x0010A382 File Offset: 0x00108582
		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x0010A38A File Offset: 0x0010858A
		public bool HasFieldName
		{
			get
			{
				return this.m_fieldName != null;
			}
		}

		// Token: 0x04001CFC RID: 7420
		private readonly string m_scopeName;

		// Token: 0x04001CFD RID: 7421
		private readonly string m_fieldName;
	}
}
