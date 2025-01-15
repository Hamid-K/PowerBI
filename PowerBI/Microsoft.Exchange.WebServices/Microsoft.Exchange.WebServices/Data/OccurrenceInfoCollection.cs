using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200007E RID: 126
	[EditorBrowsable(1)]
	public sealed class OccurrenceInfoCollection : ComplexPropertyCollection<OccurrenceInfo>
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00013814 File Offset: 0x00012814
		internal OccurrenceInfoCollection()
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001381C File Offset: 0x0001281C
		internal override OccurrenceInfo CreateComplexProperty(string xmlElementName)
		{
			if (xmlElementName == "Occurrence")
			{
				return new OccurrenceInfo();
			}
			return null;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00013832 File Offset: 0x00012832
		internal override OccurrenceInfo CreateDefaultComplexProperty()
		{
			return new OccurrenceInfo();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00013839 File Offset: 0x00012839
		internal override string GetCollectionItemXmlElementName(OccurrenceInfo complexProperty)
		{
			return "Occurrence";
		}
	}
}
