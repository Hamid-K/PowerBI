using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x02000193 RID: 403
	public sealed class ODataProperty : ODataAnnotatable
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0003514E File Offset: 0x0003334E
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00035156 File Offset: 0x00033356
		public string Name { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0003515F File Offset: 0x0003335F
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00035176 File Offset: 0x00033376
		public object Value
		{
			get
			{
				if (this.odataValue == null)
				{
					return null;
				}
				return this.odataValue.FromODataValue();
			}
			set
			{
				this.odataValue = value.ToODataValue();
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00035184 File Offset: 0x00033384
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x0003518C File Offset: 0x0003338C
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

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00035195 File Offset: 0x00033395
		internal ODataValue ODataValue
		{
			get
			{
				return this.odataValue;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0003519D File Offset: 0x0003339D
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x000351A5 File Offset: 0x000333A5
		internal ODataPropertySerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = value;
			}
		}

		// Token: 0x040006A0 RID: 1696
		private ODataValue odataValue;

		// Token: 0x040006A1 RID: 1697
		private ODataPropertySerializationInfo serializationInfo;
	}
}
