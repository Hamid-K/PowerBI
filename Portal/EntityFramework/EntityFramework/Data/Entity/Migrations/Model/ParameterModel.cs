using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C2 RID: 194
	public class ParameterModel : PropertyModel
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x00020B85 File Offset: 0x0001ED85
		public ParameterModel(PrimitiveTypeKind type)
			: this(type, null)
		{
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00020B8F File Offset: 0x0001ED8F
		public ParameterModel(PrimitiveTypeKind type, TypeUsage typeUsage)
			: base(type, typeUsage)
		{
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00020B99 File Offset: 0x0001ED99
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00020BA1 File Offset: 0x0001EDA1
		public bool IsOutParameter { get; set; }
	}
}
