using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000078 RID: 120
	[EditorBrowsable(1)]
	public sealed class MeetingSuggestionCollection : ComplexPropertyCollection<MeetingSuggestion>
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x00012CDC File Offset: 0x00011CDC
		internal MeetingSuggestionCollection()
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00012CE4 File Offset: 0x00011CE4
		internal MeetingSuggestionCollection(IEnumerable<MeetingSuggestion> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<MeetingSuggestion>(base.InternalAdd));
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00012D01 File Offset: 0x00011D01
		internal override MeetingSuggestion CreateComplexProperty(string xmlElementName)
		{
			return new MeetingSuggestion();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00012D08 File Offset: 0x00011D08
		internal override MeetingSuggestion CreateDefaultComplexProperty()
		{
			return new MeetingSuggestion();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00012D0F File Offset: 0x00011D0F
		internal override string GetCollectionItemXmlElementName(MeetingSuggestion complexProperty)
		{
			return "MeetingSuggestion";
		}
	}
}
