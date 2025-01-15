using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000089 RID: 137
	public sealed class ODataProperty : ODataAnnotatable
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000EE12 File Offset: 0x0000D012
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000EE1A File Offset: 0x0000D01A
		public string Name { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000EE23 File Offset: 0x0000D023
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000EE3A File Offset: 0x0000D03A
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

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00009CAD File Offset: 0x00007EAD
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00009CB5 File Offset: 0x00007EB5
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000EE48 File Offset: 0x0000D048
		internal ODataValue ODataValue
		{
			get
			{
				return this.odataValue;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000EE50 File Offset: 0x0000D050
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000EE58 File Offset: 0x0000D058
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

		// Token: 0x0400028F RID: 655
		private ODataValue odataValue;

		// Token: 0x04000290 RID: 656
		private ODataPropertySerializationInfo serializationInfo;
	}
}
