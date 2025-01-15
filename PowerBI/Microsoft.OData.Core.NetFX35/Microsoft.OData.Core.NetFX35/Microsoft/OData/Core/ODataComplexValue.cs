using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x02000157 RID: 343
	public sealed class ODataComplexValue : ODataValue
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x000305A2 File Offset: 0x0002E7A2
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x000305AA File Offset: 0x0002E7AA
		public IEnumerable<ODataProperty> Properties { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x000305B3 File Offset: 0x0002E7B3
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x000305BB File Offset: 0x0002E7BB
		public string TypeName { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000305C4 File Offset: 0x0002E7C4
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x000305CC File Offset: 0x0002E7CC
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "We want to allow the same instance annotation collection instance to be shared across ODataLib OM instances.")]
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}
	}
}
