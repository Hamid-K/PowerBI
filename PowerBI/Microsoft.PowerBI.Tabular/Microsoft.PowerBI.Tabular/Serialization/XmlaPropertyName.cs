using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000191 RID: 401
	internal static class XmlaPropertyName
	{
		// Token: 0x020003B3 RID: 947
		public static class Model
		{
			// Token: 0x04001160 RID: 4448
			public const string DefaultMeasure = "DefaultMeasureID";

			// Token: 0x04001161 RID: 4449
			public const string Name = "Name";

			// Token: 0x04001162 RID: 4450
			public const string Description = "Description";

			// Token: 0x04001163 RID: 4451
			public const string StorageLocation = "StorageLocation";

			// Token: 0x04001164 RID: 4452
			public const string DefaultMode = "DefaultMode";

			// Token: 0x04001165 RID: 4453
			public const string DefaultDataView = "DefaultDataView";

			// Token: 0x04001166 RID: 4454
			public const string Culture = "Culture";

			// Token: 0x04001167 RID: 4455
			public const string Collation = "Collation";

			// Token: 0x04001168 RID: 4456
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001169 RID: 4457
			public const string StructureModifiedTime = "StructureModifiedTime";

			// Token: 0x0400116A RID: 4458
			public const string DataAccessOptions = "DataAccessOptions";

			// Token: 0x0400116B RID: 4459
			public const string DefaultPowerBIDataSourceVersion = "DefaultPowerBIDataSourceVersion";

			// Token: 0x0400116C RID: 4460
			public const string ForceUniqueNames = "ForceUniqueNames";

			// Token: 0x0400116D RID: 4461
			public const string DiscourageImplicitMeasures = "DiscourageImplicitMeasures";

			// Token: 0x0400116E RID: 4462
			public const string DiscourageReportMeasures = "DiscourageReportMeasures";

			// Token: 0x0400116F RID: 4463
			public const string DataSourceVariablesOverrideBehavior = "DataSourceVariablesOverrideBehavior";

			// Token: 0x04001170 RID: 4464
			public const string DataSourceDefaultMaxConnections = "DataSourceDefaultMaxConnections";

			// Token: 0x04001171 RID: 4465
			public const string SourceQueryCulture = "SourceQueryCulture";

			// Token: 0x04001172 RID: 4466
			public const string MAttributes = "MAttributes";

			// Token: 0x04001173 RID: 4467
			public const string DiscourageCompositeModels = "DiscourageCompositeModels";

			// Token: 0x04001174 RID: 4468
			public const string AutomaticAggregationOptions = "AutomaticAggregationOptions";

			// Token: 0x04001175 RID: 4469
			public const string DisableAutoExists = "DisableAutoExists";

			// Token: 0x04001176 RID: 4470
			public const string MaxParallelismPerRefresh = "MaxParallelismPerRefresh";

			// Token: 0x04001177 RID: 4471
			public const string MaxParallelismPerQuery = "MaxParallelismPerQuery";

			// Token: 0x04001178 RID: 4472
			public const string DirectLakeBehavior = "DirectLakeBehavior";

			// Token: 0x04001179 RID: 4473
			public const string ValueFilterBehavior = "ValueFilterBehavior";
		}

		// Token: 0x020003B4 RID: 948
		public static class DataSource
		{
			// Token: 0x0400117A RID: 4474
			public const string Model = "ModelID";

			// Token: 0x0400117B RID: 4475
			public const string Name = "Name";

			// Token: 0x0400117C RID: 4476
			public const string Description = "Description";

			// Token: 0x0400117D RID: 4477
			public const string Type = "Type";

			// Token: 0x0400117E RID: 4478
			public const string ConnectionString = "ConnectionString";

			// Token: 0x0400117F RID: 4479
			public const string ImpersonationMode = "ImpersonationMode";

			// Token: 0x04001180 RID: 4480
			public const string Account = "Account";

			// Token: 0x04001181 RID: 4481
			public const string Password = "Password";

			// Token: 0x04001182 RID: 4482
			public const string MaxConnections = "MaxConnections";

			// Token: 0x04001183 RID: 4483
			public const string Isolation = "Isolation";

			// Token: 0x04001184 RID: 4484
			public const string Timeout = "Timeout";

			// Token: 0x04001185 RID: 4485
			public const string Provider = "Provider";

			// Token: 0x04001186 RID: 4486
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001187 RID: 4487
			public const string ConnectionDetails = "ConnectionDetails";

			// Token: 0x04001188 RID: 4488
			public const string Options = "Options";

			// Token: 0x04001189 RID: 4489
			public const string Credential = "Credential";

			// Token: 0x0400118A RID: 4490
			public const string ContextExpression = "ContextExpression";
		}

		// Token: 0x020003B5 RID: 949
		public static class Table
		{
			// Token: 0x0400118B RID: 4491
			public const string Model = "ModelID";

			// Token: 0x0400118C RID: 4492
			public const string DefaultDetailRowsDefinition = "DefaultDetailRowsDefinitionID";

			// Token: 0x0400118D RID: 4493
			public const string RefreshPolicy = "RefreshPolicyID";

			// Token: 0x0400118E RID: 4494
			public const string CalculationGroup = "CalculationGroupID";

			// Token: 0x0400118F RID: 4495
			public const string Name = "Name";

			// Token: 0x04001190 RID: 4496
			public const string DataCategory = "DataCategory";

			// Token: 0x04001191 RID: 4497
			public const string Description = "Description";

			// Token: 0x04001192 RID: 4498
			public const string IsHidden = "IsHidden";

			// Token: 0x04001193 RID: 4499
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001194 RID: 4500
			public const string StructureModifiedTime = "StructureModifiedTime";

			// Token: 0x04001195 RID: 4501
			public const string ShowAsVariationsOnly = "ShowAsVariationsOnly";

			// Token: 0x04001196 RID: 4502
			public const string IsPrivate = "IsPrivate";

			// Token: 0x04001197 RID: 4503
			public const string AlternateSourcePrecedence = "AlternateSourcePrecedence";

			// Token: 0x04001198 RID: 4504
			public const string ExcludeFromModelRefresh = "ExcludeFromModelRefresh";

			// Token: 0x04001199 RID: 4505
			public const string LineageTag = "LineageTag";

			// Token: 0x0400119A RID: 4506
			public const string SourceLineageTag = "SourceLineageTag";

			// Token: 0x0400119B RID: 4507
			public const string SystemManaged = "SystemManaged";

			// Token: 0x0400119C RID: 4508
			public const string ExcludeFromAutomaticAggregations = "ExcludeFromAutomaticAggregations";
		}

		// Token: 0x020003B6 RID: 950
		public static class Column
		{
			// Token: 0x0400119D RID: 4509
			public const string Table = "TableID";

			// Token: 0x0400119E RID: 4510
			public const string ColumnOrigin = "ColumnOriginID";

			// Token: 0x0400119F RID: 4511
			public const string SortByColumn = "SortByColumnID";

			// Token: 0x040011A0 RID: 4512
			public const string AttributeHierarchy = "AttributeHierarchyID";

			// Token: 0x040011A1 RID: 4513
			public const string RelatedColumnDetails = "RelatedColumnDetailsID";

			// Token: 0x040011A2 RID: 4514
			public const string AlternateOf = "AlternateOfID";

			// Token: 0x040011A3 RID: 4515
			public const string ExplicitName = "ExplicitName";

			// Token: 0x040011A4 RID: 4516
			public const string InferredName = "InferredName";

			// Token: 0x040011A5 RID: 4517
			public const string ExplicitDataType = "ExplicitDataType";

			// Token: 0x040011A6 RID: 4518
			public const string InferredDataType = "InferredDataType";

			// Token: 0x040011A7 RID: 4519
			public const string DataCategory = "DataCategory";

			// Token: 0x040011A8 RID: 4520
			public const string Description = "Description";

			// Token: 0x040011A9 RID: 4521
			public const string IsHidden = "IsHidden";

			// Token: 0x040011AA RID: 4522
			public const string State = "State";

			// Token: 0x040011AB RID: 4523
			public const string IsUnique = "IsUnique";

			// Token: 0x040011AC RID: 4524
			public const string IsKey = "IsKey";

			// Token: 0x040011AD RID: 4525
			public const string IsNullable = "IsNullable";

			// Token: 0x040011AE RID: 4526
			public const string Alignment = "Alignment";

			// Token: 0x040011AF RID: 4527
			public const string TableDetailPosition = "TableDetailPosition";

			// Token: 0x040011B0 RID: 4528
			public const string IsDefaultLabel = "IsDefaultLabel";

			// Token: 0x040011B1 RID: 4529
			public const string IsDefaultImage = "IsDefaultImage";

			// Token: 0x040011B2 RID: 4530
			public const string SummarizeBy = "SummarizeBy";

			// Token: 0x040011B3 RID: 4531
			public const string Type = "Type";

			// Token: 0x040011B4 RID: 4532
			public const string SourceColumn = "SourceColumn";

			// Token: 0x040011B5 RID: 4533
			public const string Expression = "Expression";

			// Token: 0x040011B6 RID: 4534
			public const string FormatString = "FormatString";

			// Token: 0x040011B7 RID: 4535
			public const string IsAvailableInMDX = "IsAvailableInMDX";

			// Token: 0x040011B8 RID: 4536
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040011B9 RID: 4537
			public const string StructureModifiedTime = "StructureModifiedTime";

			// Token: 0x040011BA RID: 4538
			public const string RefreshedTime = "RefreshedTime";

			// Token: 0x040011BB RID: 4539
			public const string KeepUniqueRows = "KeepUniqueRows";

			// Token: 0x040011BC RID: 4540
			public const string DisplayOrdinal = "DisplayOrdinal";

			// Token: 0x040011BD RID: 4541
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x040011BE RID: 4542
			public const string SourceProviderType = "SourceProviderType";

			// Token: 0x040011BF RID: 4543
			public const string DisplayFolder = "DisplayFolder";

			// Token: 0x040011C0 RID: 4544
			public const string EncodingHint = "EncodingHint";

			// Token: 0x040011C1 RID: 4545
			public const string LineageTag = "LineageTag";

			// Token: 0x040011C2 RID: 4546
			public const string SourceLineageTag = "SourceLineageTag";

			// Token: 0x040011C3 RID: 4547
			public const string EvaluationBehavior = "EvaluationBehavior";
		}

		// Token: 0x020003B7 RID: 951
		public static class AttributeHierarchy
		{
			// Token: 0x040011C4 RID: 4548
			public const string Column = "ColumnID";

			// Token: 0x040011C5 RID: 4549
			public const string State = "State";

			// Token: 0x040011C6 RID: 4550
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040011C7 RID: 4551
			public const string RefreshedTime = "RefreshedTime";
		}

		// Token: 0x020003B8 RID: 952
		public static class Partition
		{
			// Token: 0x040011C8 RID: 4552
			public const string Table = "TableID";

			// Token: 0x040011C9 RID: 4553
			public const string DataSource = "DataSourceID";

			// Token: 0x040011CA RID: 4554
			public const string QueryGroup = "QueryGroupID";

			// Token: 0x040011CB RID: 4555
			public const string ExpressionSource = "ExpressionSourceID";

			// Token: 0x040011CC RID: 4556
			public const string DataCoverageDefinition = "DataCoverageDefinitionID";

			// Token: 0x040011CD RID: 4557
			public const string Name = "Name";

			// Token: 0x040011CE RID: 4558
			public const string Description = "Description";

			// Token: 0x040011CF RID: 4559
			public const string QueryDefinition = "QueryDefinition";

			// Token: 0x040011D0 RID: 4560
			public const string State = "State";

			// Token: 0x040011D1 RID: 4561
			public const string Type = "Type";

			// Token: 0x040011D2 RID: 4562
			public const string Mode = "Mode";

			// Token: 0x040011D3 RID: 4563
			public const string DataView = "DataView";

			// Token: 0x040011D4 RID: 4564
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040011D5 RID: 4565
			public const string RefreshedTime = "RefreshedTime";

			// Token: 0x040011D6 RID: 4566
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x040011D7 RID: 4567
			public const string RetainDataTillForceCalculate = "RetainDataTillForceCalculate";

			// Token: 0x040011D8 RID: 4568
			public const string RangeStart = "RangeStart";

			// Token: 0x040011D9 RID: 4569
			public const string RangeEnd = "RangeEnd";

			// Token: 0x040011DA RID: 4570
			public const string RangeGranularity = "RangeGranularity";

			// Token: 0x040011DB RID: 4571
			public const string RefreshBookmark = "RefreshBookmark";

			// Token: 0x040011DC RID: 4572
			public const string MAttributes = "MAttributes";

			// Token: 0x040011DD RID: 4573
			public const string SchemaName = "SchemaName";
		}

		// Token: 0x020003B9 RID: 953
		public static class Relationship
		{
			// Token: 0x040011DE RID: 4574
			public const string Model = "ModelID";

			// Token: 0x040011DF RID: 4575
			public const string FromTable = "FromTableID";

			// Token: 0x040011E0 RID: 4576
			public const string FromColumn = "FromColumnID";

			// Token: 0x040011E1 RID: 4577
			public const string ToTable = "ToTableID";

			// Token: 0x040011E2 RID: 4578
			public const string ToColumn = "ToColumnID";

			// Token: 0x040011E3 RID: 4579
			public const string Name = "Name";

			// Token: 0x040011E4 RID: 4580
			public const string IsActive = "IsActive";

			// Token: 0x040011E5 RID: 4581
			public const string Type = "Type";

			// Token: 0x040011E6 RID: 4582
			public const string CrossFilteringBehavior = "CrossFilteringBehavior";

			// Token: 0x040011E7 RID: 4583
			public const string JoinOnDateBehavior = "JoinOnDateBehavior";

			// Token: 0x040011E8 RID: 4584
			public const string RelyOnReferentialIntegrity = "RelyOnReferentialIntegrity";

			// Token: 0x040011E9 RID: 4585
			public const string FromCardinality = "FromCardinality";

			// Token: 0x040011EA RID: 4586
			public const string ToCardinality = "ToCardinality";

			// Token: 0x040011EB RID: 4587
			public const string State = "State";

			// Token: 0x040011EC RID: 4588
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040011ED RID: 4589
			public const string RefreshedTime = "RefreshedTime";

			// Token: 0x040011EE RID: 4590
			public const string SecurityFilteringBehavior = "SecurityFilteringBehavior";
		}

		// Token: 0x020003BA RID: 954
		public static class Measure
		{
			// Token: 0x040011EF RID: 4591
			public const string Table = "TableID";

			// Token: 0x040011F0 RID: 4592
			public const string KPI = "KPIID";

			// Token: 0x040011F1 RID: 4593
			public const string DetailRowsDefinition = "DetailRowsDefinitionID";

			// Token: 0x040011F2 RID: 4594
			public const string FormatStringDefinition = "FormatStringDefinitionID";

			// Token: 0x040011F3 RID: 4595
			public const string Name = "Name";

			// Token: 0x040011F4 RID: 4596
			public const string Description = "Description";

			// Token: 0x040011F5 RID: 4597
			public const string DataType = "DataType";

			// Token: 0x040011F6 RID: 4598
			public const string Expression = "Expression";

			// Token: 0x040011F7 RID: 4599
			public const string FormatString = "FormatString";

			// Token: 0x040011F8 RID: 4600
			public const string IsHidden = "IsHidden";

			// Token: 0x040011F9 RID: 4601
			public const string State = "State";

			// Token: 0x040011FA RID: 4602
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040011FB RID: 4603
			public const string StructureModifiedTime = "StructureModifiedTime";

			// Token: 0x040011FC RID: 4604
			public const string IsSimpleMeasure = "IsSimpleMeasure";

			// Token: 0x040011FD RID: 4605
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x040011FE RID: 4606
			public const string DisplayFolder = "DisplayFolder";

			// Token: 0x040011FF RID: 4607
			public const string DataCategory = "DataCategory";

			// Token: 0x04001200 RID: 4608
			public const string LineageTag = "LineageTag";

			// Token: 0x04001201 RID: 4609
			public const string SourceLineageTag = "SourceLineageTag";
		}

		// Token: 0x020003BB RID: 955
		public static class Hierarchy
		{
			// Token: 0x04001202 RID: 4610
			public const string Table = "TableID";

			// Token: 0x04001203 RID: 4611
			public const string Name = "Name";

			// Token: 0x04001204 RID: 4612
			public const string Description = "Description";

			// Token: 0x04001205 RID: 4613
			public const string IsHidden = "IsHidden";

			// Token: 0x04001206 RID: 4614
			public const string State = "State";

			// Token: 0x04001207 RID: 4615
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001208 RID: 4616
			public const string StructureModifiedTime = "StructureModifiedTime";

			// Token: 0x04001209 RID: 4617
			public const string RefreshedTime = "RefreshedTime";

			// Token: 0x0400120A RID: 4618
			public const string DisplayFolder = "DisplayFolder";

			// Token: 0x0400120B RID: 4619
			public const string HideMembers = "HideMembers";

			// Token: 0x0400120C RID: 4620
			public const string LineageTag = "LineageTag";

			// Token: 0x0400120D RID: 4621
			public const string SourceLineageTag = "SourceLineageTag";
		}

		// Token: 0x020003BC RID: 956
		public static class Level
		{
			// Token: 0x0400120E RID: 4622
			public const string Hierarchy = "HierarchyID";

			// Token: 0x0400120F RID: 4623
			public const string Column = "ColumnID";

			// Token: 0x04001210 RID: 4624
			public const string Ordinal = "Ordinal";

			// Token: 0x04001211 RID: 4625
			public const string Name = "Name";

			// Token: 0x04001212 RID: 4626
			public const string Description = "Description";

			// Token: 0x04001213 RID: 4627
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001214 RID: 4628
			public const string LineageTag = "LineageTag";

			// Token: 0x04001215 RID: 4629
			public const string SourceLineageTag = "SourceLineageTag";
		}

		// Token: 0x020003BD RID: 957
		public static class Annotation
		{
			// Token: 0x04001216 RID: 4630
			public const string Object = "ObjectID";

			// Token: 0x04001217 RID: 4631
			public const string ObjectType = "ObjectType";

			// Token: 0x04001218 RID: 4632
			public const string Name = "Name";

			// Token: 0x04001219 RID: 4633
			public const string Value = "Value";

			// Token: 0x0400121A RID: 4634
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003BE RID: 958
		public static class KPI
		{
			// Token: 0x0400121B RID: 4635
			public const string Measure = "MeasureID";

			// Token: 0x0400121C RID: 4636
			public const string Description = "Description";

			// Token: 0x0400121D RID: 4637
			public const string TargetDescription = "TargetDescription";

			// Token: 0x0400121E RID: 4638
			public const string TargetExpression = "TargetExpression";

			// Token: 0x0400121F RID: 4639
			public const string TargetFormatString = "TargetFormatString";

			// Token: 0x04001220 RID: 4640
			public const string StatusGraphic = "StatusGraphic";

			// Token: 0x04001221 RID: 4641
			public const string StatusDescription = "StatusDescription";

			// Token: 0x04001222 RID: 4642
			public const string StatusExpression = "StatusExpression";

			// Token: 0x04001223 RID: 4643
			public const string TrendGraphic = "TrendGraphic";

			// Token: 0x04001224 RID: 4644
			public const string TrendDescription = "TrendDescription";

			// Token: 0x04001225 RID: 4645
			public const string TrendExpression = "TrendExpression";

			// Token: 0x04001226 RID: 4646
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003BF RID: 959
		public static class Culture
		{
			// Token: 0x04001227 RID: 4647
			public const string Model = "ModelID";

			// Token: 0x04001228 RID: 4648
			public const string LinguisticMetadata = "LinguisticMetadataID";

			// Token: 0x04001229 RID: 4649
			public const string Name = "Name";

			// Token: 0x0400122A RID: 4650
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x0400122B RID: 4651
			public const string StructureModifiedTime = "StructureModifiedTime";
		}

		// Token: 0x020003C0 RID: 960
		public static class ObjectTranslation
		{
			// Token: 0x0400122C RID: 4652
			public const string Culture = "CultureID";

			// Token: 0x0400122D RID: 4653
			public const string Object = "ObjectID";

			// Token: 0x0400122E RID: 4654
			public const string ObjectType = "ObjectType";

			// Token: 0x0400122F RID: 4655
			public const string Property = "Property";

			// Token: 0x04001230 RID: 4656
			public const string Value = "Value";

			// Token: 0x04001231 RID: 4657
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001232 RID: 4658
			public const string Altered = "Altered";
		}

		// Token: 0x020003C1 RID: 961
		public static class LinguisticMetadata
		{
			// Token: 0x04001233 RID: 4659
			public const string Culture = "CultureID";

			// Token: 0x04001234 RID: 4660
			public const string Content = "Content";

			// Token: 0x04001235 RID: 4661
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001236 RID: 4662
			public const string ContentType = "ContentType";
		}

		// Token: 0x020003C2 RID: 962
		public static class Perspective
		{
			// Token: 0x04001237 RID: 4663
			public const string Model = "ModelID";

			// Token: 0x04001238 RID: 4664
			public const string Name = "Name";

			// Token: 0x04001239 RID: 4665
			public const string Description = "Description";

			// Token: 0x0400123A RID: 4666
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003C3 RID: 963
		public static class PerspectiveTable
		{
			// Token: 0x0400123B RID: 4667
			public const string Perspective = "PerspectiveID";

			// Token: 0x0400123C RID: 4668
			public const string Table = "TableID";

			// Token: 0x0400123D RID: 4669
			public const string IncludeAll = "IncludeAll";

			// Token: 0x0400123E RID: 4670
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003C4 RID: 964
		public static class PerspectiveColumn
		{
			// Token: 0x0400123F RID: 4671
			public const string PerspectiveTable = "PerspectiveTableID";

			// Token: 0x04001240 RID: 4672
			public const string Column = "ColumnID";

			// Token: 0x04001241 RID: 4673
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003C5 RID: 965
		public static class PerspectiveHierarchy
		{
			// Token: 0x04001242 RID: 4674
			public const string PerspectiveTable = "PerspectiveTableID";

			// Token: 0x04001243 RID: 4675
			public const string Hierarchy = "HierarchyID";

			// Token: 0x04001244 RID: 4676
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003C6 RID: 966
		public static class PerspectiveMeasure
		{
			// Token: 0x04001245 RID: 4677
			public const string PerspectiveTable = "PerspectiveTableID";

			// Token: 0x04001246 RID: 4678
			public const string Measure = "MeasureID";

			// Token: 0x04001247 RID: 4679
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003C7 RID: 967
		public static class ModelRole
		{
			// Token: 0x04001248 RID: 4680
			public const string Model = "ModelID";

			// Token: 0x04001249 RID: 4681
			public const string Name = "Name";

			// Token: 0x0400124A RID: 4682
			public const string Description = "Description";

			// Token: 0x0400124B RID: 4683
			public const string ModelPermission = "ModelPermission";

			// Token: 0x0400124C RID: 4684
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003C8 RID: 968
		public static class ModelRoleMember
		{
			// Token: 0x0400124D RID: 4685
			public const string Role = "RoleID";

			// Token: 0x0400124E RID: 4686
			public const string MemberName = "MemberName";

			// Token: 0x0400124F RID: 4687
			public const string MemberID = "MemberID";

			// Token: 0x04001250 RID: 4688
			public const string IdentityProvider = "IdentityProvider";

			// Token: 0x04001251 RID: 4689
			public const string MemberType = "MemberType";

			// Token: 0x04001252 RID: 4690
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003C9 RID: 969
		public static class TablePermission
		{
			// Token: 0x04001253 RID: 4691
			public const string Role = "RoleID";

			// Token: 0x04001254 RID: 4692
			public const string Table = "TableID";

			// Token: 0x04001255 RID: 4693
			public const string FilterExpression = "FilterExpression";

			// Token: 0x04001256 RID: 4694
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001257 RID: 4695
			public const string State = "State";

			// Token: 0x04001258 RID: 4696
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x04001259 RID: 4697
			public const string MetadataPermission = "MetadataPermission";
		}

		// Token: 0x020003CA RID: 970
		public static class Variation
		{
			// Token: 0x0400125A RID: 4698
			public const string Column = "ColumnID";

			// Token: 0x0400125B RID: 4699
			public const string Relationship = "RelationshipID";

			// Token: 0x0400125C RID: 4700
			public const string DefaultHierarchy = "DefaultHierarchyID";

			// Token: 0x0400125D RID: 4701
			public const string DefaultColumn = "DefaultColumnID";

			// Token: 0x0400125E RID: 4702
			public const string Name = "Name";

			// Token: 0x0400125F RID: 4703
			public const string Description = "Description";

			// Token: 0x04001260 RID: 4704
			public const string IsDefault = "IsDefault";
		}

		// Token: 0x020003CB RID: 971
		public static class Set
		{
			// Token: 0x04001261 RID: 4705
			public const string Table = "TableID";

			// Token: 0x04001262 RID: 4706
			public const string Name = "Name";

			// Token: 0x04001263 RID: 4707
			public const string Description = "Description";

			// Token: 0x04001264 RID: 4708
			public const string Expression = "Expression";

			// Token: 0x04001265 RID: 4709
			public const string IsDynamic = "IsDynamic";

			// Token: 0x04001266 RID: 4710
			public const string IsHidden = "IsHidden";

			// Token: 0x04001267 RID: 4711
			public const string State = "State";

			// Token: 0x04001268 RID: 4712
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001269 RID: 4713
			public const string StructureModifiedTime = "StructureModifiedTime";

			// Token: 0x0400126A RID: 4714
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x0400126B RID: 4715
			public const string DisplayFolder = "DisplayFolder";
		}

		// Token: 0x020003CC RID: 972
		public static class PerspectiveSet
		{
			// Token: 0x0400126C RID: 4716
			public const string PerspectiveTable = "PerspectiveTableID";

			// Token: 0x0400126D RID: 4717
			public const string Set = "SetID";

			// Token: 0x0400126E RID: 4718
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003CD RID: 973
		public static class ExtendedProperty
		{
			// Token: 0x0400126F RID: 4719
			public const string Object = "ObjectID";

			// Token: 0x04001270 RID: 4720
			public const string ObjectType = "ObjectType";

			// Token: 0x04001271 RID: 4721
			public const string Name = "Name";

			// Token: 0x04001272 RID: 4722
			public const string Type = "Type";

			// Token: 0x04001273 RID: 4723
			public const string Value = "Value";

			// Token: 0x04001274 RID: 4724
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003CE RID: 974
		public static class NamedExpression
		{
			// Token: 0x04001275 RID: 4725
			public const string Model = "ModelID";

			// Token: 0x04001276 RID: 4726
			public const string QueryGroup = "QueryGroupID";

			// Token: 0x04001277 RID: 4727
			public const string ParameterValuesColumn = "ParameterValuesColumnID";

			// Token: 0x04001278 RID: 4728
			public const string ExpressionSource = "ExpressionSourceID";

			// Token: 0x04001279 RID: 4729
			public const string Name = "Name";

			// Token: 0x0400127A RID: 4730
			public const string Description = "Description";

			// Token: 0x0400127B RID: 4731
			public const string Kind = "Kind";

			// Token: 0x0400127C RID: 4732
			public const string Expression = "Expression";

			// Token: 0x0400127D RID: 4733
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x0400127E RID: 4734
			public const string MAttributes = "MAttributes";

			// Token: 0x0400127F RID: 4735
			public const string LineageTag = "LineageTag";

			// Token: 0x04001280 RID: 4736
			public const string SourceLineageTag = "SourceLineageTag";

			// Token: 0x04001281 RID: 4737
			public const string RemoteParameterName = "RemoteParameterName";
		}

		// Token: 0x020003CF RID: 975
		public static class ColumnPermission
		{
			// Token: 0x04001282 RID: 4738
			public const string TablePermission = "TablePermissionID";

			// Token: 0x04001283 RID: 4739
			public const string Column = "ColumnID";

			// Token: 0x04001284 RID: 4740
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001285 RID: 4741
			public const string MetadataPermission = "MetadataPermission";
		}

		// Token: 0x020003D0 RID: 976
		public static class DetailRowsDefinition
		{
			// Token: 0x04001286 RID: 4742
			public const string Object = "ObjectID";

			// Token: 0x04001287 RID: 4743
			public const string ObjectType = "ObjectType";

			// Token: 0x04001288 RID: 4744
			public const string Expression = "Expression";

			// Token: 0x04001289 RID: 4745
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x0400128A RID: 4746
			public const string State = "State";

			// Token: 0x0400128B RID: 4747
			public const string ErrorMessage = "ErrorMessage";
		}

		// Token: 0x020003D1 RID: 977
		public static class RelatedColumnDetails
		{
			// Token: 0x0400128C RID: 4748
			public const string Column = "ColumnID";

			// Token: 0x0400128D RID: 4749
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003D2 RID: 978
		public static class GroupByColumn
		{
			// Token: 0x0400128E RID: 4750
			public const string RelatedColumnDetails = "RelatedColumnDetailsID";

			// Token: 0x0400128F RID: 4751
			public const string GroupingColumn = "GroupingColumnID";

			// Token: 0x04001290 RID: 4752
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003D3 RID: 979
		public static class CalculationGroup
		{
			// Token: 0x04001291 RID: 4753
			public const string Table = "TableID";

			// Token: 0x04001292 RID: 4754
			public const string Description = "Description";

			// Token: 0x04001293 RID: 4755
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x04001294 RID: 4756
			public const string Precedence = "Precedence";
		}

		// Token: 0x020003D4 RID: 980
		public static class CalculationItem
		{
			// Token: 0x04001295 RID: 4757
			public const string CalculationGroup = "CalculationGroupID";

			// Token: 0x04001296 RID: 4758
			public const string FormatStringDefinition = "FormatStringDefinitionID";

			// Token: 0x04001297 RID: 4759
			public const string Name = "Name";

			// Token: 0x04001298 RID: 4760
			public const string Description = "Description";

			// Token: 0x04001299 RID: 4761
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x0400129A RID: 4762
			public const string State = "State";

			// Token: 0x0400129B RID: 4763
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x0400129C RID: 4764
			public const string Expression = "Expression";

			// Token: 0x0400129D RID: 4765
			public const string Ordinal = "Ordinal";
		}

		// Token: 0x020003D5 RID: 981
		public static class AlternateOf
		{
			// Token: 0x0400129E RID: 4766
			public const string Column = "ColumnID";

			// Token: 0x0400129F RID: 4767
			public const string BaseColumn = "BaseColumnID";

			// Token: 0x040012A0 RID: 4768
			public const string BaseTable = "BaseTableID";

			// Token: 0x040012A1 RID: 4769
			public const string Summarization = "Summarization";
		}

		// Token: 0x020003D6 RID: 982
		public static class RefreshPolicy
		{
			// Token: 0x040012A2 RID: 4770
			public const string Table = "TableID";

			// Token: 0x040012A3 RID: 4771
			public const string PolicyType = "PolicyType";

			// Token: 0x040012A4 RID: 4772
			public const string RollingWindowGranularity = "RollingWindowGranularity";

			// Token: 0x040012A5 RID: 4773
			public const string RollingWindowPeriods = "RollingWindowPeriods";

			// Token: 0x040012A6 RID: 4774
			public const string IncrementalGranularity = "IncrementalGranularity";

			// Token: 0x040012A7 RID: 4775
			public const string IncrementalPeriods = "IncrementalPeriods";

			// Token: 0x040012A8 RID: 4776
			public const string IncrementalPeriodsOffset = "IncrementalPeriodsOffset";

			// Token: 0x040012A9 RID: 4777
			public const string PollingExpression = "PollingExpression";

			// Token: 0x040012AA RID: 4778
			public const string SourceExpression = "SourceExpression";

			// Token: 0x040012AB RID: 4779
			public const string Mode = "Mode";
		}

		// Token: 0x020003D7 RID: 983
		public static class FormatStringDefinition
		{
			// Token: 0x040012AC RID: 4780
			public const string Object = "ObjectID";

			// Token: 0x040012AD RID: 4781
			public const string ObjectType = "ObjectType";

			// Token: 0x040012AE RID: 4782
			public const string Expression = "Expression";

			// Token: 0x040012AF RID: 4783
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040012B0 RID: 4784
			public const string State = "State";

			// Token: 0x040012B1 RID: 4785
			public const string ErrorMessage = "ErrorMessage";
		}

		// Token: 0x020003D8 RID: 984
		public static class QueryGroup
		{
			// Token: 0x040012B2 RID: 4786
			public const string Model = "ModelID";

			// Token: 0x040012B3 RID: 4787
			public const string Folder = "Folder";

			// Token: 0x040012B4 RID: 4788
			public const string Description = "Description";
		}

		// Token: 0x020003D9 RID: 985
		public static class AnalyticsAIMetadata
		{
			// Token: 0x040012B5 RID: 4789
			public const string Model = "ModelID";

			// Token: 0x040012B6 RID: 4790
			public const string Name = "Name";

			// Token: 0x040012B7 RID: 4791
			public const string MeasureAnalysisDefinition = "MeasureAnalysisDefinition";
		}

		// Token: 0x020003DA RID: 986
		public static class ChangedProperty
		{
			// Token: 0x040012B8 RID: 4792
			public const string Object = "ObjectID";

			// Token: 0x040012B9 RID: 4793
			public const string ObjectType = "ObjectType";

			// Token: 0x040012BA RID: 4794
			public const string Property = "Property";
		}

		// Token: 0x020003DB RID: 987
		public static class ExcludedArtifact
		{
			// Token: 0x040012BB RID: 4795
			public const string Object = "ObjectID";

			// Token: 0x040012BC RID: 4796
			public const string ObjectType = "ObjectType";

			// Token: 0x040012BD RID: 4797
			public const string ArtifactType = "ArtifactType";

			// Token: 0x040012BE RID: 4798
			public const string Reference = "Reference";
		}

		// Token: 0x020003DC RID: 988
		public static class DataCoverageDefinition
		{
			// Token: 0x040012BF RID: 4799
			public const string Partition = "PartitionID";

			// Token: 0x040012C0 RID: 4800
			public const string Description = "Description";

			// Token: 0x040012C1 RID: 4801
			public const string Expression = "Expression";

			// Token: 0x040012C2 RID: 4802
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040012C3 RID: 4803
			public const string State = "State";

			// Token: 0x040012C4 RID: 4804
			public const string ErrorMessage = "ErrorMessage";
		}

		// Token: 0x020003DD RID: 989
		public static class CalculationGroupExpression
		{
			// Token: 0x040012C5 RID: 4805
			public const string CalculationGroup = "CalculationGroupID";

			// Token: 0x040012C6 RID: 4806
			public const string FormatStringDefinition = "FormatStringDefinitionID";

			// Token: 0x040012C7 RID: 4807
			public const string Description = "Description";

			// Token: 0x040012C8 RID: 4808
			public const string Expression = "Expression";

			// Token: 0x040012C9 RID: 4809
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040012CA RID: 4810
			public const string State = "State";

			// Token: 0x040012CB RID: 4811
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x040012CC RID: 4812
			public const string SelectionMode = "SelectionMode";
		}

		// Token: 0x020003DE RID: 990
		public static class Calendar
		{
			// Token: 0x040012CD RID: 4813
			public const string Table = "TableID";

			// Token: 0x040012CE RID: 4814
			public const string Name = "Name";

			// Token: 0x040012CF RID: 4815
			public const string Description = "Description";

			// Token: 0x040012D0 RID: 4816
			public const string LineageTag = "LineageTag";

			// Token: 0x040012D1 RID: 4817
			public const string SourceLineageTag = "SourceLineageTag";

			// Token: 0x040012D2 RID: 4818
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003DF RID: 991
		public static class TimeUnitColumnAssociation
		{
			// Token: 0x040012D3 RID: 4819
			public const string Calendar = "CalendarID";

			// Token: 0x040012D4 RID: 4820
			public const string TimeUnit = "TimeUnit";

			// Token: 0x040012D5 RID: 4821
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003E0 RID: 992
		public static class CalendarColumnReference
		{
			// Token: 0x040012D6 RID: 4822
			public const string TimeUnitColumnAssociation = "TimeUnitColumnAssociationID";

			// Token: 0x040012D7 RID: 4823
			public const string Column = "ColumnID";

			// Token: 0x040012D8 RID: 4824
			public const string IsPrimaryColumn = "IsPrimaryColumn";

			// Token: 0x040012D9 RID: 4825
			public const string ModifiedTime = "ModifiedTime";
		}

		// Token: 0x020003E1 RID: 993
		public static class Function
		{
			// Token: 0x040012DA RID: 4826
			public const string Model = "ModelID";

			// Token: 0x040012DB RID: 4827
			public const string Name = "Name";

			// Token: 0x040012DC RID: 4828
			public const string Description = "Description";

			// Token: 0x040012DD RID: 4829
			public const string Expression = "Expression";

			// Token: 0x040012DE RID: 4830
			public const string IsHidden = "IsHidden";

			// Token: 0x040012DF RID: 4831
			public const string State = "State";

			// Token: 0x040012E0 RID: 4832
			public const string ErrorMessage = "ErrorMessage";

			// Token: 0x040012E1 RID: 4833
			public const string ModifiedTime = "ModifiedTime";

			// Token: 0x040012E2 RID: 4834
			public const string StructureModifiedTime = "StructureModifiedTime";

			// Token: 0x040012E3 RID: 4835
			public const string LineageTag = "LineageTag";

			// Token: 0x040012E4 RID: 4836
			public const string SourceLineageTag = "SourceLineageTag";
		}

		// Token: 0x020003E2 RID: 994
		public static class BindingInfo
		{
			// Token: 0x040012E5 RID: 4837
			public const string Model = "ModelID";

			// Token: 0x040012E6 RID: 4838
			public const string Name = "Name";

			// Token: 0x040012E7 RID: 4839
			public const string Description = "Description";

			// Token: 0x040012E8 RID: 4840
			public const string Type = "Type";

			// Token: 0x040012E9 RID: 4841
			public const string ConnectionId = "ConnectionId";
		}
	}
}
