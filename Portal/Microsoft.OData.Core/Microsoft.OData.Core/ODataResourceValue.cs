using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x020000E7 RID: 231
	public sealed class ODataResourceValue : ODataValue
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0001CB80 File Offset: 0x0001AD80
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0001CB88 File Offset: 0x0001AD88
		public string TypeName { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001CB91 File Offset: 0x0001AD91
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0001CB99 File Offset: 0x0001AD99
		public IEnumerable<ODataProperty> Properties { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x000035C8 File Offset: 0x000017C8
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
