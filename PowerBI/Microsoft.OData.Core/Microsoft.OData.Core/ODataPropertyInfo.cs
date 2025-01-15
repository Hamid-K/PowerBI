using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000013 RID: 19
	public class ODataPropertyInfo : ODataItem
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000359E File Offset: 0x0000179E
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000035A6 File Offset: 0x000017A6
		public string Name { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000035AF File Offset: 0x000017AF
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000035B7 File Offset: 0x000017B7
		public virtual EdmPrimitiveTypeKind PrimitiveTypeKind { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x000035C8 File Offset: 0x000017C8
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000035D1 File Offset: 0x000017D1
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000035D9 File Offset: 0x000017D9
		internal ODataPropertySerializationInfo SerializationInfo { get; set; }
	}
}
