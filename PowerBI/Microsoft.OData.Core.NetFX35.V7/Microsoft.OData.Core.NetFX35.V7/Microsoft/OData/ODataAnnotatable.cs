using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.OData
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public abstract class ODataAnnotatable
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000051BF File Offset: 0x000033BF
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000051C7 File Offset: 0x000033C7
		public ODataTypeAnnotation TypeAnnotation { get; set; }

		// Token: 0x06000109 RID: 265 RVA: 0x000051D0 File Offset: 0x000033D0
		internal ICollection<ODataInstanceAnnotation> GetInstanceAnnotations()
		{
			return this.instanceAnnotations;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000051D8 File Offset: 0x000033D8
		internal void SetInstanceAnnotations(ICollection<ODataInstanceAnnotation> value)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<ODataInstanceAnnotation>>(value, "value");
			this.instanceAnnotations = value;
		}

		// Token: 0x040000C2 RID: 194
		[NonSerialized]
		private ICollection<ODataInstanceAnnotation> instanceAnnotations = new Collection<ODataInstanceAnnotation>();
	}
}
