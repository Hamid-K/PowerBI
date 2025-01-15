using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000504 RID: 1284
	internal static class XmlConstants
	{
		// Token: 0x06003F87 RID: 16263 RVA: 0x000D384C File Offset: 0x000D1A4C
		public static string GetCsdlNamespace(double edmVersion)
		{
			if (object.Equals(edmVersion, 1.0))
			{
				return "http://schemas.microsoft.com/ado/2006/04/edm";
			}
			if (object.Equals(edmVersion, 1.1))
			{
				return "http://schemas.microsoft.com/ado/2007/05/edm";
			}
			if (object.Equals(edmVersion, 2.0))
			{
				return "http://schemas.microsoft.com/ado/2008/09/edm";
			}
			return "http://schemas.microsoft.com/ado/2009/11/edm";
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x000D38C4 File Offset: 0x000D1AC4
		public static string GetSsdlNamespace(double edmVersion)
		{
			if (object.Equals(edmVersion, 1.0))
			{
				return "http://schemas.microsoft.com/ado/2006/04/edm/ssdl";
			}
			if (object.Equals(edmVersion, 2.0))
			{
				return "http://schemas.microsoft.com/ado/2009/02/edm/ssdl";
			}
			return "http://schemas.microsoft.com/ado/2009/11/edm/ssdl";
		}

		// Token: 0x0400159D RID: 5533
		internal const string CustomAnnotationNamespace = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation";

		// Token: 0x0400159E RID: 5534
		internal const string CustomAnnotationPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:";

		// Token: 0x0400159F RID: 5535
		internal const string ClrTypeAnnotation = "ClrType";

		// Token: 0x040015A0 RID: 5536
		internal const string ClrTypeAnnotationWithPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:ClrType";

		// Token: 0x040015A1 RID: 5537
		internal const string UseClrTypesAnnotationWithPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:UseClrTypes";

		// Token: 0x040015A2 RID: 5538
		internal const string IndexAnnotationWithPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index";

		// Token: 0x040015A3 RID: 5539
		internal const string ModelNamespace_1 = "http://schemas.microsoft.com/ado/2006/04/edm";

		// Token: 0x040015A4 RID: 5540
		internal const string ModelNamespace_1_1 = "http://schemas.microsoft.com/ado/2007/05/edm";

		// Token: 0x040015A5 RID: 5541
		internal const string ModelNamespace_2 = "http://schemas.microsoft.com/ado/2008/09/edm";

		// Token: 0x040015A6 RID: 5542
		internal const string ModelNamespace_3 = "http://schemas.microsoft.com/ado/2009/11/edm";

		// Token: 0x040015A7 RID: 5543
		internal const string ProviderManifestNamespace = "http://schemas.microsoft.com/ado/2006/04/edm/providermanifest";

		// Token: 0x040015A8 RID: 5544
		internal const string TargetNamespace_1 = "http://schemas.microsoft.com/ado/2006/04/edm/ssdl";

		// Token: 0x040015A9 RID: 5545
		internal const string TargetNamespace_2 = "http://schemas.microsoft.com/ado/2009/02/edm/ssdl";

		// Token: 0x040015AA RID: 5546
		internal const string TargetNamespace_3 = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl";

		// Token: 0x040015AB RID: 5547
		internal const string CodeGenerationSchemaNamespace = "http://schemas.microsoft.com/ado/2006/04/codegeneration";

		// Token: 0x040015AC RID: 5548
		internal const string EntityStoreSchemaGeneratorNamespace = "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator";

		// Token: 0x040015AD RID: 5549
		internal const string AnnotationNamespace = "http://schemas.microsoft.com/ado/2009/02/edm/annotation";

		// Token: 0x040015AE RID: 5550
		internal const string StoreGeneratedPatternAnnotation = "http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern";

		// Token: 0x040015AF RID: 5551
		internal const string Alias = "Alias";

		// Token: 0x040015B0 RID: 5552
		internal const string Self = "Self";

		// Token: 0x040015B1 RID: 5553
		internal const string Provider = "Provider";

		// Token: 0x040015B2 RID: 5554
		internal const string ProviderManifestToken = "ProviderManifestToken";

		// Token: 0x040015B3 RID: 5555
		internal const string CSSpaceSchemaExtension = ".msl";

		// Token: 0x040015B4 RID: 5556
		internal const string CSpaceSchemaExtension = ".csdl";

		// Token: 0x040015B5 RID: 5557
		internal const string SSpaceSchemaExtension = ".ssdl";

		// Token: 0x040015B6 RID: 5558
		internal const double UndefinedVersion = 0.0;

		// Token: 0x040015B7 RID: 5559
		internal const double EdmVersionForV1 = 1.0;

		// Token: 0x040015B8 RID: 5560
		internal const double EdmVersionForV1_1 = 1.1;

		// Token: 0x040015B9 RID: 5561
		internal const double EdmVersionForV2 = 2.0;

		// Token: 0x040015BA RID: 5562
		internal const double EdmVersionForV3 = 3.0;

		// Token: 0x040015BB RID: 5563
		internal const double SchemaVersionLatest = 3.0;

		// Token: 0x040015BC RID: 5564
		internal const double StoreVersionForV1 = 1.0;

		// Token: 0x040015BD RID: 5565
		internal const double StoreVersionForV2 = 2.0;

		// Token: 0x040015BE RID: 5566
		internal const double StoreVersionForV3 = 3.0;

		// Token: 0x040015BF RID: 5567
		internal const string Association = "Association";

		// Token: 0x040015C0 RID: 5568
		internal const string AssociationSet = "AssociationSet";

		// Token: 0x040015C1 RID: 5569
		internal const string ComplexType = "ComplexType";

		// Token: 0x040015C2 RID: 5570
		internal const string DefiningQuery = "DefiningQuery";

		// Token: 0x040015C3 RID: 5571
		internal const string DefiningExpression = "DefiningExpression";

		// Token: 0x040015C4 RID: 5572
		internal const string Documentation = "Documentation";

		// Token: 0x040015C5 RID: 5573
		internal const string DependentRole = "Dependent";

		// Token: 0x040015C6 RID: 5574
		internal const string End = "End";

		// Token: 0x040015C7 RID: 5575
		internal const string EntityType = "EntityType";

		// Token: 0x040015C8 RID: 5576
		internal const string EntityContainer = "EntityContainer";

		// Token: 0x040015C9 RID: 5577
		internal const string FunctionImport = "FunctionImport";

		// Token: 0x040015CA RID: 5578
		internal const string Key = "Key";

		// Token: 0x040015CB RID: 5579
		internal const string NavigationProperty = "NavigationProperty";

		// Token: 0x040015CC RID: 5580
		internal const string OnDelete = "OnDelete";

		// Token: 0x040015CD RID: 5581
		internal const string PrincipalRole = "Principal";

		// Token: 0x040015CE RID: 5582
		internal const string Property = "Property";

		// Token: 0x040015CF RID: 5583
		internal const string PropertyRef = "PropertyRef";

		// Token: 0x040015D0 RID: 5584
		internal const string ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x040015D1 RID: 5585
		internal const string Role = "Role";

		// Token: 0x040015D2 RID: 5586
		internal const string Schema = "Schema";

		// Token: 0x040015D3 RID: 5587
		internal const string Summary = "Summary";

		// Token: 0x040015D4 RID: 5588
		internal const string LongDescription = "LongDescription";

		// Token: 0x040015D5 RID: 5589
		internal const string SampleValue = "SampleValue";

		// Token: 0x040015D6 RID: 5590
		internal const string EnumType = "EnumType";

		// Token: 0x040015D7 RID: 5591
		internal const string Member = "Member";

		// Token: 0x040015D8 RID: 5592
		internal const string ValueTerm = "ValueTerm";

		// Token: 0x040015D9 RID: 5593
		internal const string Annotations = "Annotations";

		// Token: 0x040015DA RID: 5594
		internal const string ValueAnnotation = "ValueAnnotation";

		// Token: 0x040015DB RID: 5595
		internal const string TypeAnnotation = "TypeAnnotation";

		// Token: 0x040015DC RID: 5596
		internal const string Using = "Using";

		// Token: 0x040015DD RID: 5597
		internal const string TypeAccess = "TypeAccess";

		// Token: 0x040015DE RID: 5598
		internal const string MethodAccess = "MethodAccess";

		// Token: 0x040015DF RID: 5599
		internal const string SetterAccess = "SetterAccess";

		// Token: 0x040015E0 RID: 5600
		internal const string GetterAccess = "GetterAccess";

		// Token: 0x040015E1 RID: 5601
		internal const string Abstract = "Abstract";

		// Token: 0x040015E2 RID: 5602
		internal const string OpenType = "OpenType";

		// Token: 0x040015E3 RID: 5603
		internal const string Action = "Action";

		// Token: 0x040015E4 RID: 5604
		internal const string BaseType = "BaseType";

		// Token: 0x040015E5 RID: 5605
		internal const string EntitySet = "EntitySet";

		// Token: 0x040015E6 RID: 5606
		internal const string EntitySetPath = "EntitySetPath";

		// Token: 0x040015E7 RID: 5607
		internal const string Extends = "Extends";

		// Token: 0x040015E8 RID: 5608
		internal const string FromRole = "FromRole";

		// Token: 0x040015E9 RID: 5609
		internal const string Multiplicity = "Multiplicity";

		// Token: 0x040015EA RID: 5610
		internal const string Name = "Name";

		// Token: 0x040015EB RID: 5611
		internal const string Namespace = "Namespace";

		// Token: 0x040015EC RID: 5612
		internal const string Table = "Table";

		// Token: 0x040015ED RID: 5613
		internal const string ToRole = "ToRole";

		// Token: 0x040015EE RID: 5614
		internal const string Relationship = "Relationship";

		// Token: 0x040015EF RID: 5615
		internal const string ElementType = "ElementType";

		// Token: 0x040015F0 RID: 5616
		internal const string StoreGeneratedPattern = "StoreGeneratedPattern";

		// Token: 0x040015F1 RID: 5617
		internal const string IsFlags = "IsFlags";

		// Token: 0x040015F2 RID: 5618
		internal const string IsBindable = "IsBindable";

		// Token: 0x040015F3 RID: 5619
		internal const string IsSideEffecting = "IsSideEffecting";

		// Token: 0x040015F4 RID: 5620
		internal const string UnderlyingType = "UnderlyingType";

		// Token: 0x040015F5 RID: 5621
		internal const string Value = "Value";

		// Token: 0x040015F6 RID: 5622
		internal const string ContainsTarget = "ContainsTarget";

		// Token: 0x040015F7 RID: 5623
		internal const string Max = "Max";

		// Token: 0x040015F8 RID: 5624
		internal const string None = "None";

		// Token: 0x040015F9 RID: 5625
		internal const string Identity = "Identity";

		// Token: 0x040015FA RID: 5626
		internal const string Computed = "Computed";

		// Token: 0x040015FB RID: 5627
		internal const string Fixed = "Fixed";

		// Token: 0x040015FC RID: 5628
		internal const string CollectionKind_None = "None";

		// Token: 0x040015FD RID: 5629
		internal const string CollectionKind_List = "List";

		// Token: 0x040015FE RID: 5630
		internal const string CollectionKind_Bag = "Bag";

		// Token: 0x040015FF RID: 5631
		internal const string CollectionKind = "CollectionKind";

		// Token: 0x04001600 RID: 5632
		internal const string In = "In";

		// Token: 0x04001601 RID: 5633
		internal const string Out = "Out";

		// Token: 0x04001602 RID: 5634
		internal const string InOut = "InOut";

		// Token: 0x04001603 RID: 5635
		internal const string Variable = "Variable";

		// Token: 0x04001604 RID: 5636
		internal const string True = "true";

		// Token: 0x04001605 RID: 5637
		internal const string False = "false";

		// Token: 0x04001606 RID: 5638
		internal const string Function = "Function";

		// Token: 0x04001607 RID: 5639
		internal const string ReturnType = "ReturnType";

		// Token: 0x04001608 RID: 5640
		internal const string Parameter = "Parameter";

		// Token: 0x04001609 RID: 5641
		internal const string Mode = "Mode";

		// Token: 0x0400160A RID: 5642
		internal const string StoreFunctionName = "StoreFunctionName";

		// Token: 0x0400160B RID: 5643
		internal const string ProviderManifestElement = "ProviderManifest";

		// Token: 0x0400160C RID: 5644
		internal const string TypesElement = "Types";

		// Token: 0x0400160D RID: 5645
		internal const string FunctionsElement = "Functions";

		// Token: 0x0400160E RID: 5646
		internal const string TypeElement = "Type";

		// Token: 0x0400160F RID: 5647
		internal const string FunctionElement = "Function";

		// Token: 0x04001610 RID: 5648
		internal const string ScaleElement = "Scale";

		// Token: 0x04001611 RID: 5649
		internal const string PrecisionElement = "Precision";

		// Token: 0x04001612 RID: 5650
		internal const string MaxLengthElement = "MaxLength";

		// Token: 0x04001613 RID: 5651
		internal const string FacetDescriptionsElement = "FacetDescriptions";

		// Token: 0x04001614 RID: 5652
		internal const string UnicodeElement = "Unicode";

		// Token: 0x04001615 RID: 5653
		internal const string FixedLengthElement = "FixedLength";

		// Token: 0x04001616 RID: 5654
		internal const string ReturnTypeElement = "ReturnType";

		// Token: 0x04001617 RID: 5655
		internal const string SridElement = "SRID";

		// Token: 0x04001618 RID: 5656
		internal const string IsStrictElement = "IsStrict";

		// Token: 0x04001619 RID: 5657
		internal const string TypeAttribute = "Type";

		// Token: 0x0400161A RID: 5658
		internal const string MinimumAttribute = "Minimum";

		// Token: 0x0400161B RID: 5659
		internal const string MaximumAttribute = "Maximum";

		// Token: 0x0400161C RID: 5660
		internal const string NamespaceAttribute = "Namespace";

		// Token: 0x0400161D RID: 5661
		internal const string DefaultValueAttribute = "DefaultValue";

		// Token: 0x0400161E RID: 5662
		internal const string ConstantAttribute = "Constant";

		// Token: 0x0400161F RID: 5663
		internal const string DestinationTypeAttribute = "DestinationType";

		// Token: 0x04001620 RID: 5664
		internal const string PrimitiveTypeKindAttribute = "PrimitiveTypeKind";

		// Token: 0x04001621 RID: 5665
		internal const string AggregateAttribute = "Aggregate";

		// Token: 0x04001622 RID: 5666
		internal const string BuiltInAttribute = "BuiltIn";

		// Token: 0x04001623 RID: 5667
		internal const string NameAttribute = "Name";

		// Token: 0x04001624 RID: 5668
		internal const string IgnoreFacetsAttribute = "IgnoreFacets";

		// Token: 0x04001625 RID: 5669
		internal const string NiladicFunction = "NiladicFunction";

		// Token: 0x04001626 RID: 5670
		internal const string IsComposable = "IsComposable";

		// Token: 0x04001627 RID: 5671
		internal const string CommandText = "CommandText";

		// Token: 0x04001628 RID: 5672
		internal const string ParameterTypeSemantics = "ParameterTypeSemantics";

		// Token: 0x04001629 RID: 5673
		internal const string CollectionType = "CollectionType";

		// Token: 0x0400162A RID: 5674
		internal const string ReferenceType = "ReferenceType";

		// Token: 0x0400162B RID: 5675
		internal const string RowType = "RowType";

		// Token: 0x0400162C RID: 5676
		internal const string TypeRef = "TypeRef";

		// Token: 0x0400162D RID: 5677
		internal const string UseStrongSpatialTypes = "UseStrongSpatialTypes";

		// Token: 0x0400162E RID: 5678
		internal const string XmlCommentStartString = "<!--";

		// Token: 0x0400162F RID: 5679
		internal const string XmlCommentEndString = "-->";
	}
}
