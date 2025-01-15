using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200084D RID: 2125
	internal sealed class FeedTableValue : TableValue
	{
		// Token: 0x06003D4A RID: 15690 RVA: 0x000C752C File Offset: 0x000C572C
		public FeedTableValue(ODataEnvironment environment, Uri initialRequestUri, RecordTypeValue feedElementTypeValue, IList<TableKey> keys)
		{
			this.environment = environment;
			this.initialRequestUri = initialRequestUri;
			this.feedElementTypeValue = feedElementTypeValue;
			this.tableKeys = keys;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x000C7551 File Offset: 0x000C5751
		public FeedTableValue(ODataEnvironment environment, Uri initialRequestUri, RecordTypeValue feedElementTypeValue, IEnumerable<IValueReference> inlineEntries, Uri nextPage, IList<TableKey> keys)
		{
			this.environment = environment;
			this.initialRequestUri = initialRequestUri;
			this.feedElementTypeValue = feedElementTypeValue;
			this.inlineEntries = inlineEntries;
			this.nextPage = nextPage;
			this.tableKeys = keys;
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x000C7586 File Offset: 0x000C5786
		public override TypeValue Type
		{
			get
			{
				return TableTypeValue.New(this.feedElementTypeValue, this.tableKeys);
			}
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000C7599 File Offset: 0x000C5799
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			if (this.inlineEntries != null)
			{
				return new ODataReaderEnumerator(this.environment, this.feedElementTypeValue, this.nextPage, this.inlineEntries);
			}
			return new ODataReaderEnumerator(this.environment, this.feedElementTypeValue, this.initialRequestUri, null);
		}

		// Token: 0x04002012 RID: 8210
		private readonly ODataEnvironment environment;

		// Token: 0x04002013 RID: 8211
		private readonly Uri initialRequestUri;

		// Token: 0x04002014 RID: 8212
		private readonly RecordTypeValue feedElementTypeValue;

		// Token: 0x04002015 RID: 8213
		private readonly IEnumerable<IValueReference> inlineEntries;

		// Token: 0x04002016 RID: 8214
		private readonly Uri nextPage;

		// Token: 0x04002017 RID: 8215
		private readonly IList<TableKey> tableKeys;
	}
}
