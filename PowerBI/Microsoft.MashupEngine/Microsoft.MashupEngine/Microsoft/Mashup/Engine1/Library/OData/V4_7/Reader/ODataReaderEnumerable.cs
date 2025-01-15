using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x020007A0 RID: 1952
	internal sealed class ODataReaderEnumerable : IEnumerable<IValueReference>, IEnumerable
	{
		// Token: 0x06003925 RID: 14629 RVA: 0x000B7D03 File Offset: 0x000B5F03
		public ODataReaderEnumerable(ODataEnvironment environment, Uri requestUri, TypeValue itemType, bool isResourceSet, Microsoft.OData.Edm.IEdmNavigationSource navigationSource = null, IODataPayloadReader reader = null)
		{
			this.environment = environment;
			this.itemType = itemType;
			this.requestUri = requestUri;
			this.isResourceSet = isResourceSet;
			this.navigationSource = navigationSource;
			this.reader = reader;
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000B7D38 File Offset: 0x000B5F38
		public IEnumerator<IValueReference> GetEnumerator()
		{
			if (this.environment.Annotations.SupportsBatch)
			{
				return new BatchEnumerator(this.GetReaderEnumerator(new GetReader(new RequestBatcher(this.environment).GetResponse)), this.environment);
			}
			return this.GetReaderEnumerator(new GetReader(this.environment.GetRequestReader));
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000B7D96 File Offset: 0x000B5F96
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000B7DA0 File Offset: 0x000B5FA0
		private ODataReaderEnumerator GetReaderEnumerator(GetReader getReader)
		{
			IODataPayloadReader value;
			if (this.reader != null)
			{
				value = this.reader;
				this.reader = null;
			}
			else
			{
				value = getReader(new GetReaderArgs
				{
					Uri = this.requestUri,
					Catch404 = false
				}).Value;
			}
			ODataReaderWithResponse odataReaderWithResponse;
			using (value)
			{
				odataReaderWithResponse = value.ToResourceReader(this.isResourceSet);
			}
			return new ODataReaderEnumerator(this.environment, getReader, odataReaderWithResponse, this.itemType, this.isResourceSet, this.navigationSource);
		}

		// Token: 0x04001D77 RID: 7543
		private readonly ODataEnvironment environment;

		// Token: 0x04001D78 RID: 7544
		private readonly TypeValue itemType;

		// Token: 0x04001D79 RID: 7545
		private readonly Uri requestUri;

		// Token: 0x04001D7A RID: 7546
		private readonly bool isResourceSet;

		// Token: 0x04001D7B RID: 7547
		private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationSource;

		// Token: 0x04001D7C RID: 7548
		private IODataPayloadReader reader;
	}
}
