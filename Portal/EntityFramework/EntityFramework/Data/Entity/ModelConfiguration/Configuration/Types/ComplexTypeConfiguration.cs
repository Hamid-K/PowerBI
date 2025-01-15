using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Types
{
	// Token: 0x020001FD RID: 509
	internal class ComplexTypeConfiguration : StructuralTypeConfiguration
	{
		// Token: 0x06001AD3 RID: 6867 RVA: 0x000487D8 File Offset: 0x000469D8
		internal ComplexTypeConfiguration(Type structuralType)
			: base(structuralType)
		{
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000487E1 File Offset: 0x000469E1
		private ComplexTypeConfiguration(ComplexTypeConfiguration source)
			: base(source)
		{
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000487EA File Offset: 0x000469EA
		internal virtual ComplexTypeConfiguration Clone()
		{
			return new ComplexTypeConfiguration(this);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000487F2 File Offset: 0x000469F2
		internal virtual void Configure(ComplexType complexType)
		{
			base.Configure(complexType.Name, complexType.Properties, complexType.GetMetadataProperties());
		}
	}
}
