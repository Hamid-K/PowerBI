using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Common;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000206 RID: 518
	public sealed class ODataEntityPropertyMappingCollection : IEnumerable<EntityPropertyMappingAttribute>, IEnumerable
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x0003731B File Offset: 0x0003551B
		public ODataEntityPropertyMappingCollection()
		{
			this.mappings = new List<EntityPropertyMappingAttribute>();
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003732E File Offset: 0x0003552E
		public ODataEntityPropertyMappingCollection(IEnumerable<EntityPropertyMappingAttribute> other)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<EntityPropertyMappingAttribute>>(other, "other");
			this.mappings = new List<EntityPropertyMappingAttribute>(other);
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0003734D File Offset: 0x0003554D
		internal int Count
		{
			get
			{
				return this.mappings.Count;
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003735A File Offset: 0x0003555A
		public void Add(EntityPropertyMappingAttribute mapping)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityPropertyMappingAttribute>(mapping, "mapping");
			this.mappings.Add(mapping);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00037373 File Offset: 0x00035573
		public IEnumerator<EntityPropertyMappingAttribute> GetEnumerator()
		{
			return this.mappings.GetEnumerator();
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00037385 File Offset: 0x00035585
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.mappings.GetEnumerator();
		}

		// Token: 0x040005C2 RID: 1474
		private readonly List<EntityPropertyMappingAttribute> mappings;
	}
}
