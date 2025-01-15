using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000886 RID: 2182
	internal sealed class ODataReaderEnumerable : IEnumerable<IValueReference>, IEnumerable
	{
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000CD48B File Offset: 0x000CB68B
		public ODataReaderEnumerable(ODataQueryMetadata metadata, TypeValue requestType, Uri requestUri)
		{
			this.metadata = metadata;
			this.requestType = requestType;
			this.requestUri = requestUri;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000CD4A8 File Offset: 0x000CB6A8
		public IEnumerator<IValueReference> GetEnumerator()
		{
			ODataEnvironment environment = this.metadata.Environment;
			if (environment.Annotations.SupportsBatch)
			{
				return new BatchEnumerator(new ODataReaderEnumerator(environment, this.requestType, this.requestUri, this.metadata.NavigationSource, !this.metadata.IsSingleton, false, null, new Func<GetReaderArgs, Lazy<ODataReaderWrapper>>(new RequestBatcher(environment).GetResponse)), environment);
			}
			return new ODataReaderEnumerator(environment, this.requestType, this.requestUri, this.metadata.NavigationSource, !this.metadata.IsSingleton, false, null, null);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000CD541 File Offset: 0x000CB741
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040020EE RID: 8430
		private readonly ODataQueryMetadata metadata;

		// Token: 0x040020EF RID: 8431
		private readonly TypeValue requestType;

		// Token: 0x040020F0 RID: 8432
		private readonly Uri requestUri;
	}
}
