using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	internal sealed class MissingElementException : ReportCatalogException
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00003CF6 File Offset: 0x00001EF6
		public MissingElementException(string elementName)
			: base(ErrorCode.rsMissingElement, ErrorStringsWrapper.rsMissingElement(elementName), null, null, Array.Empty<object>())
		{
			this.m_elementName = elementName;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003D13 File Offset: 0x00001F13
		private MissingElementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003D1D File Offset: 0x00001F1D
		public string MissingElementName
		{
			get
			{
				return this.m_elementName;
			}
		}

		// Token: 0x04000013 RID: 19
		private string m_elementName;
	}
}
