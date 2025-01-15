using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Edm
{
	// Token: 0x020000E0 RID: 224
	internal static class EdmXNames
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x00027A5F File Offset: 0x00025C5F
		public static string ActionAttribute(this XElement element)
		{
			return (string)element.Attribute("Action");
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00027A76 File Offset: 0x00025C76
		public static string ColumnNameAttribute(this XElement element)
		{
			return (string)element.Attribute("ColumnName");
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00027A8D File Offset: 0x00025C8D
		public static string EntitySetAttribute(this XElement element)
		{
			return (string)element.Attribute("EntitySet");
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00027AA4 File Offset: 0x00025CA4
		public static string NameAttribute(this XElement element)
		{
			return (string)element.Attribute("Name");
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00027ABB File Offset: 0x00025CBB
		public static string NamespaceAttribute(this XElement element)
		{
			return (string)element.Attribute("Namespace");
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00027AD2 File Offset: 0x00025CD2
		public static string EntityTypeAttribute(this XElement element)
		{
			return (string)element.Attribute("EntityType");
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00027AE9 File Offset: 0x00025CE9
		public static string FromRoleAttribute(this XElement element)
		{
			return (string)element.Attribute("FromRole");
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00027B00 File Offset: 0x00025D00
		public static string ToRoleAttribute(this XElement element)
		{
			return (string)element.Attribute("ToRole");
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00027B17 File Offset: 0x00025D17
		public static string NullableAttribute(this XElement element)
		{
			return (string)element.Attribute("Nullable");
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00027B2E File Offset: 0x00025D2E
		public static string MaxLengthAttribute(this XElement element)
		{
			return (string)element.Attribute("MaxLength");
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00027B45 File Offset: 0x00025D45
		public static string MultiplicityAttribute(this XElement element)
		{
			return (string)element.Attribute("Multiplicity");
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00027B5C File Offset: 0x00025D5C
		public static string FixedLengthAttribute(this XElement element)
		{
			return (string)element.Attribute("FixedLength");
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00027B73 File Offset: 0x00025D73
		public static string PrecisionAttribute(this XElement element)
		{
			return (string)element.Attribute("Precision");
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00027B8A File Offset: 0x00025D8A
		public static string ProviderAttribute(this XElement element)
		{
			return (string)element.Attribute("Provider");
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00027BA1 File Offset: 0x00025DA1
		public static string ProviderManifestTokenAttribute(this XElement element)
		{
			return (string)element.Attribute("ProviderManifestToken");
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00027BB8 File Offset: 0x00025DB8
		public static string RelationshipAttribute(this XElement element)
		{
			return (string)element.Attribute("Relationship");
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00027BCF File Offset: 0x00025DCF
		public static string ScaleAttribute(this XElement element)
		{
			return (string)element.Attribute("Scale");
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00027BE6 File Offset: 0x00025DE6
		public static string StoreGeneratedPatternAttribute(this XElement element)
		{
			return (string)element.Attribute("StoreGeneratedPattern");
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00027BFD File Offset: 0x00025DFD
		public static string UnicodeAttribute(this XElement element)
		{
			return (string)element.Attribute("Unicode");
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00027C14 File Offset: 0x00025E14
		public static string RoleAttribute(this XElement element)
		{
			return (string)element.Attribute("Role");
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00027C2B File Offset: 0x00025E2B
		public static string SchemaAttribute(this XElement element)
		{
			return (string)element.Attribute("Schema");
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00027C42 File Offset: 0x00025E42
		public static string StoreEntitySetAttribute(this XElement element)
		{
			return (string)element.Attribute("StoreEntitySet");
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00027C59 File Offset: 0x00025E59
		public static string TableAttribute(this XElement element)
		{
			return (string)element.Attribute("Table");
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00027C70 File Offset: 0x00025E70
		public static string TypeAttribute(this XElement element)
		{
			return (string)element.Attribute("Type");
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00027C87 File Offset: 0x00025E87
		public static string TypeNameAttribute(this XElement element)
		{
			return (string)element.Attribute("TypeName");
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00027C9E File Offset: 0x00025E9E
		public static string ValueAttribute(this XElement element)
		{
			return (string)element.Attribute("Value");
		}

		// Token: 0x040008CD RID: 2253
		private static readonly XNamespace _csdlNamespaceV2 = XNamespace.Get("http://schemas.microsoft.com/ado/2008/09/edm");

		// Token: 0x040008CE RID: 2254
		private static readonly XNamespace _mslNamespaceV2 = XNamespace.Get("http://schemas.microsoft.com/ado/2008/09/mapping/cs");

		// Token: 0x040008CF RID: 2255
		private static readonly XNamespace _ssdlNamespaceV2 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/02/edm/ssdl");

		// Token: 0x040008D0 RID: 2256
		private static readonly XNamespace _csdlNamespaceV3 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/edm");

		// Token: 0x040008D1 RID: 2257
		private static readonly XNamespace _mslNamespaceV3 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/mapping/cs");

		// Token: 0x040008D2 RID: 2258
		private static readonly XNamespace _ssdlNamespaceV3 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/edm/ssdl");

		// Token: 0x020007BD RID: 1981
		public static class Csdl
		{
			// Token: 0x06005825 RID: 22565 RVA: 0x0013860B File Offset: 0x0013680B
			private static IEnumerable<XName> Names(string elementName)
			{
				return new List<XName>
				{
					EdmXNames._csdlNamespaceV3 + elementName,
					EdmXNames._csdlNamespaceV2 + elementName
				};
			}

			// Token: 0x0400211B RID: 8475
			public static readonly IEnumerable<XName> AssociationNames = EdmXNames.Csdl.Names("Association");

			// Token: 0x0400211C RID: 8476
			public static readonly IEnumerable<XName> ComplexTypeNames = EdmXNames.Csdl.Names("ComplexType");

			// Token: 0x0400211D RID: 8477
			public static readonly IEnumerable<XName> EndNames = EdmXNames.Csdl.Names("End");

			// Token: 0x0400211E RID: 8478
			public static readonly IEnumerable<XName> EntityContainerNames = EdmXNames.Csdl.Names("EntityContainer");

			// Token: 0x0400211F RID: 8479
			public static readonly IEnumerable<XName> EntitySetNames = EdmXNames.Csdl.Names("EntitySet");

			// Token: 0x04002120 RID: 8480
			public static readonly IEnumerable<XName> EntityTypeNames = EdmXNames.Csdl.Names("EntityType");

			// Token: 0x04002121 RID: 8481
			public static readonly IEnumerable<XName> NavigationPropertyNames = EdmXNames.Csdl.Names("NavigationProperty");

			// Token: 0x04002122 RID: 8482
			public static readonly IEnumerable<XName> PropertyNames = EdmXNames.Csdl.Names("Property");

			// Token: 0x04002123 RID: 8483
			public static readonly IEnumerable<XName> SchemaNames = EdmXNames.Csdl.Names("Schema");
		}

		// Token: 0x020007BE RID: 1982
		public static class Msl
		{
			// Token: 0x06005827 RID: 22567 RVA: 0x001386C8 File Offset: 0x001368C8
			private static IEnumerable<XName> Names(string elementName)
			{
				return new List<XName>
				{
					EdmXNames._mslNamespaceV3 + elementName,
					EdmXNames._mslNamespaceV2 + elementName
				};
			}

			// Token: 0x04002124 RID: 8484
			public static readonly IEnumerable<XName> AssociationSetMappingNames = EdmXNames.Msl.Names("AssociationSetMapping");

			// Token: 0x04002125 RID: 8485
			public static readonly IEnumerable<XName> ComplexPropertyNames = EdmXNames.Msl.Names("ComplexProperty");

			// Token: 0x04002126 RID: 8486
			public static readonly IEnumerable<XName> ConditionNames = EdmXNames.Msl.Names("Condition");

			// Token: 0x04002127 RID: 8487
			public static readonly IEnumerable<XName> EntityContainerMappingNames = EdmXNames.Msl.Names("EntityContainerMapping");

			// Token: 0x04002128 RID: 8488
			public static readonly IEnumerable<XName> EntitySetMappingNames = EdmXNames.Msl.Names("EntitySetMapping");

			// Token: 0x04002129 RID: 8489
			public static readonly IEnumerable<XName> EntityTypeMappingNames = EdmXNames.Msl.Names("EntityTypeMapping");

			// Token: 0x0400212A RID: 8490
			public static readonly IEnumerable<XName> MappingNames = EdmXNames.Msl.Names("Mapping");

			// Token: 0x0400212B RID: 8491
			public static readonly IEnumerable<XName> MappingFragmentNames = EdmXNames.Msl.Names("MappingFragment");

			// Token: 0x0400212C RID: 8492
			public static readonly IEnumerable<XName> ScalarPropertyNames = EdmXNames.Msl.Names("ScalarProperty");
		}

		// Token: 0x020007BF RID: 1983
		public static class Ssdl
		{
			// Token: 0x06005829 RID: 22569 RVA: 0x00138788 File Offset: 0x00136988
			private static IEnumerable<XName> Names(string elementName)
			{
				return new List<XName>
				{
					EdmXNames._ssdlNamespaceV3 + elementName,
					EdmXNames._ssdlNamespaceV2 + elementName
				};
			}

			// Token: 0x0400212D RID: 8493
			public static readonly IEnumerable<XName> AssociationNames = EdmXNames.Ssdl.Names("Association");

			// Token: 0x0400212E RID: 8494
			public static readonly IEnumerable<XName> DependentNames = EdmXNames.Ssdl.Names("Dependent");

			// Token: 0x0400212F RID: 8495
			public static readonly IEnumerable<XName> EndNames = EdmXNames.Ssdl.Names("End");

			// Token: 0x04002130 RID: 8496
			public static readonly IEnumerable<XName> EntityContainerNames = EdmXNames.Ssdl.Names("EntityContainer");

			// Token: 0x04002131 RID: 8497
			public static readonly IEnumerable<XName> EntitySetNames = EdmXNames.Ssdl.Names("EntitySet");

			// Token: 0x04002132 RID: 8498
			public static readonly IEnumerable<XName> EntityTypeNames = EdmXNames.Ssdl.Names("EntityType");

			// Token: 0x04002133 RID: 8499
			public static readonly IEnumerable<XName> KeyNames = EdmXNames.Ssdl.Names("Key");

			// Token: 0x04002134 RID: 8500
			public static readonly IEnumerable<XName> OnDeleteNames = EdmXNames.Ssdl.Names("OnDelete");

			// Token: 0x04002135 RID: 8501
			public static readonly IEnumerable<XName> PrincipalNames = EdmXNames.Ssdl.Names("Principal");

			// Token: 0x04002136 RID: 8502
			public static readonly IEnumerable<XName> PropertyNames = EdmXNames.Ssdl.Names("Property");

			// Token: 0x04002137 RID: 8503
			public static readonly IEnumerable<XName> PropertyRefNames = EdmXNames.Ssdl.Names("PropertyRef");

			// Token: 0x04002138 RID: 8504
			public static readonly IEnumerable<XName> SchemaNames = EdmXNames.Ssdl.Names("Schema");
		}
	}
}
