using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000050 RID: 80
	[EditorBrowsable(1)]
	public sealed class DeletedOccurrenceInfoCollection : ComplexPropertyCollection<DeletedOccurrenceInfo>
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0000D325 File Offset: 0x0000C325
		internal DeletedOccurrenceInfoCollection()
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D32D File Offset: 0x0000C32D
		internal override DeletedOccurrenceInfo CreateComplexProperty(string xmlElementName)
		{
			if (xmlElementName == "DeletedOccurrence")
			{
				return new DeletedOccurrenceInfo();
			}
			return null;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D343 File Offset: 0x0000C343
		internal override DeletedOccurrenceInfo CreateDefaultComplexProperty()
		{
			return new DeletedOccurrenceInfo();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D34A File Offset: 0x0000C34A
		internal override string GetCollectionItemXmlElementName(DeletedOccurrenceInfo complexProperty)
		{
			return "Occurrence";
		}
	}
}
