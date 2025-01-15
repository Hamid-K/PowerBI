using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F19 RID: 3865
	internal sealed class AnalysisServicesMdxCubeMetadataProvider : MdxCubeMetadataProvider
	{
		// Token: 0x0600664A RID: 26186 RVA: 0x0015FEE4 File Offset: 0x0015E0E4
		public AnalysisServicesMdxCubeMetadataProvider(AnalysisServicesService service, string name, string baseCubeName)
		{
			this.service = service;
			this.name = name;
			this.baseCubeName = baseCubeName;
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x0015FF01 File Offset: 0x0015E101
		public override IEnumerable<MdxMeasureMetadata> GetMeasures()
		{
			using (IDataReader reader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [MEASURE_UNIQUE_NAME], [MEASURE_CAPTION], [DATA_TYPE], [MEASUREGROUP_NAME], [MEASURE_DISPLAY_FOLDER] from $system.mdschema_measures where [CUBE_NAME] = @CubeName and [MEASURE_IS_VISIBLE]", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("CubeName", this.name)
			}))
			{
				while (reader.Read())
				{
					yield return new MdxMeasureMetadata
					{
						Caption = (reader["MEASURE_CAPTION"] as string),
						DataType = (OleDbType)((ushort)reader["DATA_TYPE"]),
						DisplayFolder = (reader["MEASURE_DISPLAY_FOLDER"] as string),
						MeasureGroupName = (reader["MEASUREGROUP_NAME"] as string),
						UniqueName = (string)reader["MEASURE_UNIQUE_NAME"]
					};
				}
			}
			IDataReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x0015FF11 File Offset: 0x0015E111
		public override IEnumerable<MdxDimensionMetadata> GetDimensions()
		{
			using (IDataReader reader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [DIMENSION_UNIQUE_NAME], [DIMENSION_CAPTION] from $system.mdschema_dimensions where [CUBE_NAME] = @CubeName and [DIMENSION_UNIQUE_NAME] <> '[Measures]'", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("CubeName", this.name)
			}))
			{
				while (reader.Read())
				{
					yield return new MdxDimensionMetadata
					{
						Caption = (reader["DIMENSION_CAPTION"] as string),
						UniqueName = (string)reader["DIMENSION_UNIQUE_NAME"],
						DimensionType = MdxDimensionType.Unknown,
						DataType = OleDbType.BSTR
					};
				}
			}
			IDataReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x0015FF21 File Offset: 0x0015E121
		public override IEnumerable<MdxKpiMetadata> GetKPIs()
		{
			using (IDataReader reader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [KPI_NAME], [KPI_CAPTION], [MEASUREGROUP_NAME], [KPI_DISPLAY_FOLDER], [KPI_GOAL], [KPI_STATUS], [KPI_TREND], [KPI_VALUE] from $system.mdschema_kpis where [CUBE_NAME] = @CubeName", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("CubeName", this.name)
			}))
			{
				while (reader.Read())
				{
					yield return new MdxKpiMetadata
					{
						Caption = (reader["KPI_CAPTION"] as string),
						DisplayFolder = (reader["KPI_DISPLAY_FOLDER"] as string),
						Goal = (string)reader["KPI_GOAL"],
						MeasureGroupName = (reader["MEASUREGROUP_NAME"] as string),
						Name = (string)reader["KPI_NAME"],
						Status = (string)reader["KPI_STATUS"],
						Trend = (string)reader["KPI_TREND"],
						Value = (string)reader["KPI_VALUE"]
					};
				}
			}
			IDataReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x0015FF31 File Offset: 0x0015E131
		public override IEnumerable<MdxHierarchyMetadata> GetHierarchies()
		{
			using (IDataReader reader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [DIMENSION_UNIQUE_NAME], [HIERARCHY_UNIQUE_NAME], [HIERARCHY_CAPTION], [HIERARCHY_DISPLAY_FOLDER], [HIERARCHY_ORIGIN], [HIERARCHY_IS_VISIBLE] from $system.mdschema_hierarchies where [CUBE_NAME] = @CubeName and [DIMENSION_UNIQUE_NAME] <> '[Measures]'", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("CubeName", this.name)
			}))
			{
				while (reader.Read())
				{
					yield return new MdxHierarchyMetadata
					{
						Caption = (string)reader["HIERARCHY_CAPTION"],
						DimensionUniqueName = (string)reader["DIMENSION_UNIQUE_NAME"],
						DisplayFolder = (string)reader["HIERARCHY_DISPLAY_FOLDER"],
						IsVisible = (bool)reader["HIERARCHY_IS_VISIBLE"],
						Origin = (int)((ushort)reader["HIERARCHY_ORIGIN"]),
						UniqueName = (string)reader["HIERARCHY_UNIQUE_NAME"]
					};
				}
			}
			IDataReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x0015FF41 File Offset: 0x0015E141
		public override IEnumerable<MdxLevelMetadata> GetLevels()
		{
			using (IDataReader reader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [DIMENSION_UNIQUE_NAME], [HIERARCHY_UNIQUE_NAME], [LEVEL_UNIQUE_NAME], [LEVEL_NUMBER], [LEVEL_CAPTION] from $system.mdschema_levels where [CUBE_NAME] = @CubeName and [LEVEL_NAME] <> '(All)' and [DIMENSION_UNIQUE_NAME] <> '[Measures]'", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("CubeName", this.name)
			}))
			{
				while (reader.Read())
				{
					yield return new MdxLevelMetadata
					{
						Caption = (reader["LEVEL_CAPTION"] as string),
						DimensionUniqueName = (string)reader["DIMENSION_UNIQUE_NAME"],
						HierarchyUniqueName = (string)reader["HIERARCHY_UNIQUE_NAME"],
						Number = (int)((uint)reader["LEVEL_NUMBER"]),
						UniqueName = (string)reader["LEVEL_UNIQUE_NAME"]
					};
				}
			}
			IDataReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x0015FF54 File Offset: 0x0015E154
		public override IEnumerable<MdxMeasureGroupMetadata> GetMeasureGroups()
		{
			MdxMeasureGroupMetadata[] array = this.GetMeasureGroups(this.name).ToArray<MdxMeasureGroupMetadata>();
			if (array.Length == 0 && this.baseCubeName != null && this.service.IsInTabularMode)
			{
				return this.GetMeasureGroups(this.baseCubeName);
			}
			return array;
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x0015FF9A File Offset: 0x0015E19A
		private IEnumerable<MdxMeasureGroupMetadata> GetMeasureGroups(string cubeName)
		{
			using (IDataReader reader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [MEASUREGROUP_NAME], [MEASUREGROUP_CAPTION] from $system.mdschema_measuregroups where [CUBE_NAME] = @CubeName", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("CubeName", this.name)
			}))
			{
				while (reader.Read())
				{
					yield return new MdxMeasureGroupMetadata
					{
						Caption = (reader["MEASUREGROUP_CAPTION"] as string),
						Name = (string)reader["MEASUREGROUP_NAME"]
					};
				}
			}
			IDataReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x0015FFAA File Offset: 0x0015E1AA
		public override IEnumerable<MdxPropertyMetadata> GetProperties(MdxDimension dimension)
		{
			using (IDataReader reader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [DIMENSION_UNIQUE_NAME], [HIERARCHY_UNIQUE_NAME], [LEVEL_UNIQUE_NAME], [PROPERTY_CAPTION], [DATA_TYPE], [PROPERTY_NAME] from $system.mdschema_properties where [CUBE_NAME] = @CubeName and [DIMENSION_UNIQUE_NAME] = @DimensionUniqueName and PROPERTY_IS_VISIBLE and LEN([PROPERTY_ATTRIBUTE_HIERARCHY_NAME]) = 0", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("CubeName", this.name),
				new KeyValuePair<string, object>("DimensionUniqueName", dimension.MdxIdentifier)
			}))
			{
				while (reader.Read())
				{
					string text = (string)reader["LEVEL_UNIQUE_NAME"];
					string text2 = text + ".[" + (string)reader["PROPERTY_NAME"] + "]";
					yield return new MdxPropertyMetadata
					{
						UniqueName = text2,
						DimensionUniqueName = (string)reader["DIMENSION_UNIQUE_NAME"],
						HierarchyUniqueName = (string)reader["HIERARCHY_UNIQUE_NAME"],
						LevelUniqueName = text,
						KeyUniqueName = MdxIdentifier.QuotePart("MEMBER_UNIQUE_NAME"),
						Caption = (reader["PROPERTY_CAPTION"] as string),
						DataType = (OleDbType)((ushort)reader["DATA_TYPE"]),
						PropertyKind = MdxPropertyKind.UserDefined
					};
				}
			}
			IDataReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x0400382A RID: 14378
		private readonly AnalysisServicesService service;

		// Token: 0x0400382B RID: 14379
		private readonly string name;

		// Token: 0x0400382C RID: 14380
		private readonly string baseCubeName;

		// Token: 0x0400382D RID: 14381
		internal const string MeasuresQuery = "select [MEASURE_UNIQUE_NAME], [MEASURE_CAPTION], [DATA_TYPE], [MEASUREGROUP_NAME], [MEASURE_DISPLAY_FOLDER] from $system.mdschema_measures where [CUBE_NAME] = @CubeName and [MEASURE_IS_VISIBLE]";

		// Token: 0x0400382E RID: 14382
		internal const string DimensionsQuery = "select [DIMENSION_UNIQUE_NAME], [DIMENSION_CAPTION] from $system.mdschema_dimensions where [CUBE_NAME] = @CubeName and [DIMENSION_UNIQUE_NAME] <> '[Measures]'";

		// Token: 0x0400382F RID: 14383
		internal const string KPIsQuery = "select [KPI_NAME], [KPI_CAPTION], [MEASUREGROUP_NAME], [KPI_DISPLAY_FOLDER], [KPI_GOAL], [KPI_STATUS], [KPI_TREND], [KPI_VALUE] from $system.mdschema_kpis where [CUBE_NAME] = @CubeName";

		// Token: 0x04003830 RID: 14384
		internal const string HierarchiesQuery = "select [DIMENSION_UNIQUE_NAME], [HIERARCHY_UNIQUE_NAME], [HIERARCHY_CAPTION], [HIERARCHY_DISPLAY_FOLDER], [HIERARCHY_ORIGIN], [HIERARCHY_IS_VISIBLE] from $system.mdschema_hierarchies where [CUBE_NAME] = @CubeName and [DIMENSION_UNIQUE_NAME] <> '[Measures]'";

		// Token: 0x04003831 RID: 14385
		internal const string GetLevelsCommand = "select [DIMENSION_UNIQUE_NAME], [HIERARCHY_UNIQUE_NAME], [LEVEL_UNIQUE_NAME], [LEVEL_NUMBER], [LEVEL_CAPTION] from $system.mdschema_levels where [CUBE_NAME] = @CubeName and [LEVEL_NAME] <> '(All)' and [DIMENSION_UNIQUE_NAME] <> '[Measures]'";

		// Token: 0x04003832 RID: 14386
		internal const string MeasureGroupsQuery = "select [MEASUREGROUP_NAME], [MEASUREGROUP_CAPTION] from $system.mdschema_measuregroups where [CUBE_NAME] = @CubeName";

		// Token: 0x04003833 RID: 14387
		internal const string IntrinsicPropertiesQuery = "select [DIMENSION_UNIQUE_NAME], [HIERARCHY_UNIQUE_NAME], [LEVEL_UNIQUE_NAME], [PROPERTY_CAPTION], [DATA_TYPE], [PROPERTY_NAME] from $system.mdschema_properties where [CUBE_NAME] = @CubeName and [DIMENSION_UNIQUE_NAME] = @DimensionUniqueName and PROPERTY_IS_VISIBLE and LEN([PROPERTY_ATTRIBUTE_HIERARCHY_NAME]) = 0";
	}
}
