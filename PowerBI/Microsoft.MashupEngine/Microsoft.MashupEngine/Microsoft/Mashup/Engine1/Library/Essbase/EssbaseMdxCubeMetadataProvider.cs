using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C70 RID: 3184
	internal class EssbaseMdxCubeMetadataProvider : MdxCubeMetadataProvider
	{
		// Token: 0x0600565D RID: 22109 RVA: 0x0012B690 File Offset: 0x00129890
		public EssbaseMdxCubeMetadataProvider(EssbaseService service, string serverSourceInfo, string applicationName, EssbaseCube cube)
		{
			this.service = service;
			this.serverSourceInfo = serverSourceInfo;
			this.applicationName = applicationName;
			this.cube = cube;
			this.measuresMetadata = null;
			this.dimensionMetadata = null;
			this.hierarchyMetadata = null;
			this.levelMetadata = null;
			this.dimensionAliasMap = null;
			this.measureAliasMap = new Dictionary<string, string>();
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x0600565E RID: 22110 RVA: 0x0012B6F0 File Offset: 0x001298F0
		public Dictionary<string, string> DimensionAliasMap
		{
			get
			{
				if (this.dimensionAliasMap == null)
				{
					IList<KeyValuePair<EssbaseXmlaElement, string>> list = new List<KeyValuePair<EssbaseXmlaElement, string>>
					{
						new KeyValuePair<EssbaseXmlaElement, string>(EssbaseXmlaElement.LEVEL_NUMBER, "0")
					};
					StreamReader streamReader = this.service.ExecuteDiscoverRequest("MDSCHEMA_MEMBERS", this.serverSourceInfo, this.applicationName, this.cube.Name, list);
					this.dimensionAliasMap = EssbaseXmlaParser.ParseMembersResponse(streamReader);
				}
				return this.dimensionAliasMap;
			}
		}

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x0012B757 File Offset: 0x00129957
		// (set) Token: 0x06005660 RID: 22112 RVA: 0x0012B75F File Offset: 0x0012995F
		public Dictionary<string, string> MeasureAliasMap
		{
			get
			{
				return this.measureAliasMap;
			}
			private set
			{
				this.measureAliasMap = value;
			}
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x06005661 RID: 22113 RVA: 0x0012B768 File Offset: 0x00129968
		public string MeasuresDimensionName
		{
			get
			{
				if (this.dimensionMetadata == null)
				{
					this.GetDimensions();
				}
				if (this.measureDimensionMetaData == null)
				{
					return string.Empty;
				}
				return this.measureDimensionMetaData.UniqueName;
			}
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x0012B794 File Offset: 0x00129994
		public override IEnumerable<MdxDimensionMetadata> GetDimensions()
		{
			if (this.dimensionMetadata == null)
			{
				StreamReader streamReader = this.service.ExecuteDiscoverRequest("MDSCHEMA_DIMENSIONS", this.serverSourceInfo, this.applicationName, this.cube.Name, null);
				this.dimensionMetadata = EssbaseXmlaParser.ParseDimensionsResponse(streamReader, out this.measureDimensionMetaData);
				if (this.DimensionAliasMap.Count > 0)
				{
					foreach (MdxDimensionMetadata mdxDimensionMetadata in this.dimensionMetadata)
					{
						string text;
						if (this.DimensionAliasMap.TryGetValue(mdxDimensionMetadata.UniqueName, out text))
						{
							mdxDimensionMetadata.Caption = text;
						}
					}
				}
			}
			return this.dimensionMetadata;
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x0012B850 File Offset: 0x00129A50
		public override IEnumerable<MdxHierarchyMetadata> GetHierarchies()
		{
			if (this.hierarchyMetadata == null)
			{
				StreamReader streamReader = this.service.ExecuteDiscoverRequest("MDSCHEMA_HIERARCHIES", this.serverSourceInfo, this.applicationName, this.cube.Name, null);
				this.hierarchyMetadata = EssbaseXmlaParser.ParseHierarchiesResponse(streamReader, this.MeasuresDimensionName);
				if (this.DimensionAliasMap.Count > 0)
				{
					foreach (MdxHierarchyMetadata mdxHierarchyMetadata in this.hierarchyMetadata)
					{
						string text;
						if (this.DimensionAliasMap.TryGetValue(mdxHierarchyMetadata.UniqueName, out text))
						{
							mdxHierarchyMetadata.Caption = text;
						}
					}
				}
			}
			return this.hierarchyMetadata;
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x000033E7 File Offset: 0x000015E7
		public override IEnumerable<MdxKpiMetadata> GetKPIs()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x0012B90C File Offset: 0x00129B0C
		public override IEnumerable<MdxLevelMetadata> GetLevels()
		{
			if (this.levelMetadata == null)
			{
				StreamReader streamReader = this.service.ExecuteDiscoverRequest("MDSCHEMA_LEVELS", this.serverSourceInfo, this.applicationName, this.cube.Name, null);
				this.levelMetadata = EssbaseXmlaParser.ParseLevelsResponse(streamReader, this.MeasuresDimensionName);
				Dictionary<string, string> dictionary = this.DimensionAliasMap;
				if (dictionary.Count > 0)
				{
					foreach (MdxLevelMetadata mdxLevelMetadata in this.levelMetadata)
					{
						string text;
						if (dictionary.TryGetValue(mdxLevelMetadata.HierarchyUniqueName, out text))
						{
							mdxLevelMetadata.Caption = mdxLevelMetadata.Caption.Replace(mdxLevelMetadata.HierarchyUniqueName, text);
						}
					}
				}
			}
			return this.levelMetadata;
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x000033E7 File Offset: 0x000015E7
		public override IEnumerable<MdxMeasureGroupMetadata> GetMeasureGroups()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x0012B9D8 File Offset: 0x00129BD8
		public override IEnumerable<MdxMeasureMetadata> GetMeasures()
		{
			if (this.measuresMetadata == null)
			{
				IList<MdxMeasureMetadata> list = EssbaseXmlaParser.ParseMeasuresResponse(this.service.ExecuteDiscoverRequest("MDSCHEMA_MEASURES", this.serverSourceInfo, this.applicationName, this.cube.Name, null));
				if (!string.IsNullOrEmpty(this.MeasuresDimensionName))
				{
					IList<KeyValuePair<EssbaseXmlaElement, string>> list2 = new List<KeyValuePair<EssbaseXmlaElement, string>>
					{
						new KeyValuePair<EssbaseXmlaElement, string>(EssbaseXmlaElement.DIMENSION_UNIQUE_NAME, this.MeasuresDimensionName)
					};
					Dictionary<string, string> dictionary = EssbaseXmlaParser.ParseMembersResponse(this.service.ExecuteDiscoverRequest("MDSCHEMA_MEMBERS", this.serverSourceInfo, this.applicationName, this.cube.Name, list2));
					if (dictionary.Count > 0)
					{
						foreach (MdxMeasureMetadata mdxMeasureMetadata in list)
						{
							string text;
							if (dictionary.TryGetValue(mdxMeasureMetadata.UniqueName, out text))
							{
								mdxMeasureMetadata.Caption = text;
							}
						}
						Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
						foreach (KeyValuePair<string, string> keyValuePair in dictionary)
						{
							string text2 = "[" + keyValuePair.Value + "]";
							dictionary2[text2] = keyValuePair.Key;
						}
						this.MeasureAliasMap = dictionary2;
					}
				}
				this.measuresMetadata = list;
			}
			return this.measuresMetadata;
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x0012BB50 File Offset: 0x00129D50
		public override IEnumerable<MdxPropertyMetadata> GetProperties(MdxDimension dimension)
		{
			return EssbaseMdxCubeMetadataProvider.propertyMetadata;
		}

		// Token: 0x04003090 RID: 12432
		private readonly EssbaseService service;

		// Token: 0x04003091 RID: 12433
		private readonly string serverSourceInfo;

		// Token: 0x04003092 RID: 12434
		private readonly string applicationName;

		// Token: 0x04003093 RID: 12435
		private readonly EssbaseCube cube;

		// Token: 0x04003094 RID: 12436
		private IList<MdxMeasureMetadata> measuresMetadata;

		// Token: 0x04003095 RID: 12437
		private IList<MdxDimensionMetadata> dimensionMetadata;

		// Token: 0x04003096 RID: 12438
		private IList<MdxHierarchyMetadata> hierarchyMetadata;

		// Token: 0x04003097 RID: 12439
		private IList<MdxLevelMetadata> levelMetadata;

		// Token: 0x04003098 RID: 12440
		private static readonly IList<MdxPropertyMetadata> propertyMetadata = new List<MdxPropertyMetadata>();

		// Token: 0x04003099 RID: 12441
		private MdxDimensionMetadata measureDimensionMetaData;

		// Token: 0x0400309A RID: 12442
		private Dictionary<string, string> dimensionAliasMap;

		// Token: 0x0400309B RID: 12443
		private Dictionary<string, string> measureAliasMap;
	}
}
