using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200012C RID: 300
	public class ComplexTypeConfiguration : StructuralTypeConfiguration
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00029CD6 File Offset: 0x00027ED6
		public ComplexTypeConfiguration()
		{
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00029CDE File Offset: 0x00027EDE
		public ComplexTypeConfiguration(ODataModelBuilder modelBuilder, Type clrType)
			: base(modelBuilder, clrType)
		{
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00029CE8 File Offset: 0x00027EE8
		public override EdmTypeKind Kind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00029CEB File Offset: 0x00027EEB
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00029CF8 File Offset: 0x00027EF8
		public virtual ComplexTypeConfiguration BaseType
		{
			get
			{
				return this.BaseTypeInternal as ComplexTypeConfiguration;
			}
			set
			{
				this.DerivesFrom(value);
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00029D02 File Offset: 0x00027F02
		public virtual ComplexTypeConfiguration Abstract()
		{
			this.AbstractImpl();
			return this;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00029D0B File Offset: 0x00027F0B
		public virtual ComplexTypeConfiguration DerivesFromNothing()
		{
			this.DerivesFromNothingImpl();
			return this;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00029D14 File Offset: 0x00027F14
		public virtual ComplexTypeConfiguration DerivesFrom(ComplexTypeConfiguration baseType)
		{
			this.DerivesFromImpl(baseType);
			return this;
		}
	}
}
