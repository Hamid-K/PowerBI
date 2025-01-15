using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000059 RID: 89
	internal struct ModelingReference
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0000C206 File Offset: 0x0000A406
		public ModelingReference(QName referenceByID, string propertyName, bool multipleInScope)
		{
			this.m_reference = referenceByID;
			this.m_propertyName = propertyName ?? string.Empty;
			this.m_multipleInScope = multipleInScope;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000C22B File Offset: 0x0000A42B
		public ModelingReference(string referenceByName, string propertyName, bool multipleInScope)
		{
			this.m_reference = referenceByName ?? string.Empty;
			this.m_propertyName = propertyName ?? string.Empty;
			this.m_multipleInScope = multipleInScope;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000C254 File Offset: 0x0000A454
		public QName? ReferenceByID
		{
			get
			{
				if (!(this.m_reference is QName))
				{
					return null;
				}
				return new QName?((QName)this.m_reference);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000C288 File Offset: 0x0000A488
		public string ReferenceByName
		{
			get
			{
				return this.m_reference as string;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000C295 File Offset: 0x0000A495
		public string ReferenceString
		{
			get
			{
				return this.m_reference.ToString();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000C2A2 File Offset: 0x0000A4A2
		public string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000C2AA File Offset: 0x0000A4AA
		public bool MultipleInScope
		{
			get
			{
				return this.m_multipleInScope;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000C2B2 File Offset: 0x0000A4B2
		public override string ToString()
		{
			return StringUtil.FormatInvariant("[{0}:{1}]", new object[] { this.m_propertyName, this.m_reference });
		}

		// Token: 0x04000217 RID: 535
		private readonly object m_reference;

		// Token: 0x04000218 RID: 536
		private readonly string m_propertyName;

		// Token: 0x04000219 RID: 537
		private readonly bool m_multipleInScope;
	}
}
