using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000160 RID: 352
	internal static class ComplexTypeExtensions
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x0003A074 File Offset: 0x00038274
		public static EdmProperty AddComplexProperty(this ComplexType complexType, string name, ComplexType targetComplexType)
		{
			EdmProperty edmProperty = EdmProperty.CreateComplex(name, targetComplexType);
			complexType.AddMember(edmProperty);
			return edmProperty;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0003A091 File Offset: 0x00038291
		public static object GetConfiguration(this ComplexType complexType)
		{
			return complexType.Annotations.GetConfiguration();
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0003A09E File Offset: 0x0003829E
		public static Type GetClrType(this ComplexType complexType)
		{
			return complexType.Annotations.GetClrType();
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0003A0AB File Offset: 0x000382AB
		internal static IEnumerable<ComplexType> ToHierarchy(this ComplexType edmType)
		{
			return EdmType.SafeTraverseHierarchy<ComplexType>(edmType);
		}
	}
}
