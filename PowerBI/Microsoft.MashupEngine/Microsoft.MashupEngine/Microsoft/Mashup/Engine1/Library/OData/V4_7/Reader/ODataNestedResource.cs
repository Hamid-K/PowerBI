using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x0200079D RID: 1949
	internal struct ODataNestedResource
	{
		// Token: 0x06003919 RID: 14617 RVA: 0x000B7C18 File Offset: 0x000B5E18
		public ODataNestedResource(ODataResource resource, IEnumerable<KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues>> nestedValueProperties)
		{
			this.resource = resource;
			this.nestedValueProperties = nestedValueProperties.ToDictionary((KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues> kvp) => kvp.Key, (KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues> kvp) => kvp.Value);
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x0600391A RID: 14618 RVA: 0x000B7C76 File Offset: 0x000B5E76
		public ODataResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x000B7C7E File Offset: 0x000B5E7E
		public IDictionary<ODataNestedResourceInfoWrapper, ODataNestedValues> NestedValueProperties
		{
			get
			{
				return this.nestedValueProperties;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x0600391C RID: 14620 RVA: 0x000B7C86 File Offset: 0x000B5E86
		public IEnumerable<ODataPropertyWrapper> SimpleProperties
		{
			get
			{
				return this.Resource.Properties.Select((ODataProperty p) => new ODataPropertyWrapper(p));
			}
		}

		// Token: 0x04001D6F RID: 7535
		private readonly ODataResource resource;

		// Token: 0x04001D70 RID: 7536
		private readonly Dictionary<ODataNestedResourceInfoWrapper, ODataNestedValues> nestedValueProperties;
	}
}
