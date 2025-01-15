using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000ABF RID: 2751
	public static class HtmlDocumentResult
	{
		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x000FDA68 File Offset: 0x000FBC68
		public static TableTypeValue Type
		{
			get
			{
				if (HtmlDocumentResult.type == null)
				{
					Keys keys = Keys.New("Kind", "Name", "Children", "Text");
					RecordValue recordValue = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.Text,
						LogicalValue.False
					});
					RecordValue recordValue2 = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						NullableTypeValue.Text,
						LogicalValue.False
					});
					RecordValue recordValue3 = RecordValue.New(RecordTypeValue.RecordFieldKeys, delegate(int index)
					{
						if (index == 0)
						{
							return HtmlDocumentResult.type.Nullable;
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return LogicalValue.False;
					});
					RecordValue recordValue4 = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						NullableTypeValue.Text,
						LogicalValue.False
					});
					HtmlDocumentResult.type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keys, new Value[] { recordValue, recordValue2, recordValue3, recordValue4 }), false));
				}
				return HtmlDocumentResult.type;
			}
		}

		// Token: 0x040028EB RID: 10475
		private static TableTypeValue type;
	}
}
