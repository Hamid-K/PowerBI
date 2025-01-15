using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.OData
{
	// Token: 0x02000051 RID: 81
	public abstract class ODataAnnotatable
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00007D19 File Offset: 0x00005F19
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00007D21 File Offset: 0x00005F21
		public ODataTypeAnnotation TypeAnnotation { get; set; }

		// Token: 0x06000280 RID: 640 RVA: 0x00007D2A File Offset: 0x00005F2A
		internal ICollection<ODataInstanceAnnotation> GetInstanceAnnotations()
		{
			return this.instanceAnnotations;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007D32 File Offset: 0x00005F32
		internal void SetInstanceAnnotations(ICollection<ODataInstanceAnnotation> value)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<ODataInstanceAnnotation>>(value, "value");
			this.instanceAnnotations = value;
		}

		// Token: 0x0400012C RID: 300
		private ICollection<ODataInstanceAnnotation> instanceAnnotations = new Collection<ODataInstanceAnnotation>();
	}
}
