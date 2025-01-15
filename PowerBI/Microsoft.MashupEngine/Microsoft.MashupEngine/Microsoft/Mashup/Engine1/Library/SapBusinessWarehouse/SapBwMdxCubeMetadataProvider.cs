using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004B5 RID: 1205
	internal class SapBwMdxCubeMetadataProvider : MdxCubeMetadataProvider
	{
		// Token: 0x0600279D RID: 10141 RVA: 0x00074574 File Offset: 0x00072774
		public SapBwMdxCubeMetadataProvider(ISapBwService service, string catalogName, SapBwMdxCube cube)
		{
			this.service = service;
			this.catalogName = catalogName;
			this.cube = cube;
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x00074591 File Offset: 0x00072791
		private Dictionary<string, SapBwMdxCubeMetadataProvider.SapBwMeasureMetadata> MeasuresMetadata
		{
			get
			{
				if (this.measuresMetadata == null)
				{
					this.measuresMetadata = new Dictionary<string, SapBwMdxCubeMetadataProvider.SapBwMeasureMetadata>();
					this.service.ExceptionHandler.InvokeWithoutRetry(delegate
					{
						using (IDataReader dataReader = this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_MEASURES", "MEASURES", this.GetBaseRestrictions(true)))
						{
							while (dataReader.Read())
							{
								string @string = dataReader.GetString(3);
								string string2 = dataReader.GetString(4);
								SapBwMdxCubeMetadataProvider.SapBwMeasureMetadata sapBwMeasureMetadata;
								if (this.measuresMetadata.TryGetValue(@string, out sapBwMeasureMetadata))
								{
									throw DataSourceException.NewDataSourceError<Message4>(this.service.Host, Strings.SapBwDuplicateMeasure(@string, sapBwMeasureMetadata.MeasureMetadata.Caption, string2, this.cube.Name ?? this.catalogName), this.service.Resource, null, null);
								}
								string string3 = dataReader.GetString(7);
								OleDbType oleDbType = OleDbType.Variant;
								SapBwDataType sapBwDataType;
								if (SapBwDataType.TryGetByName(string3, out sapBwDataType))
								{
									oleDbType = sapBwDataType.OleDbType;
								}
								int num;
								this.measuresMetadata.Add(@string, new SapBwMdxCubeMetadataProvider.SapBwMeasureMetadata
								{
									MeasureMetadata = new MdxMeasureMetadata
									{
										Caption = string2,
										UniqueName = @string,
										DisplayFolder = string.Empty,
										DataType = oleDbType
									},
									ValueType = string3,
									Precision = (int.TryParse(dataReader.GetString(8), NumberStyles.Number, CultureInfo.InvariantCulture, out num) ? new int?(num) : null)
								});
							}
						}
					});
				}
				return this.measuresMetadata;
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000745C8 File Offset: 0x000727C8
		public override IEnumerable<MdxDimensionMetadata> GetDimensions()
		{
			return this.service.ExceptionHandler.InvokeWithRetry<List<MdxDimensionMetadata>>(delegate
			{
				List<MdxDimensionMetadata> list2;
				using (IDataReader dataReader = this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_DIMENSIONS", "DIMENSIONS", this.GetBaseRestrictions(true)))
				{
					List<MdxDimensionMetadata> list = new List<MdxDimensionMetadata>();
					while (dataReader.Read())
					{
						string @string = dataReader.GetString(3);
						MdxDimensionType mdxDimensionType = SapBwMdxCubeMetadataProvider.dimensionTypesFromString[dataReader.GetString(7)];
						if (this.service.EnableStructures || @string != "[Measures]")
						{
							list.Add(new MdxDimensionMetadata
							{
								UniqueName = @string,
								Caption = dataReader.GetString(5),
								DimensionType = mdxDimensionType,
								DataType = (OleDbType)(this.service.SupportsEnhancedMetadata ? dataReader.GetInt32(11) : 8)
							});
						}
					}
					list2 = list;
				}
				return list2;
			});
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000745E6 File Offset: 0x000727E6
		public override IEnumerable<MdxMeasureMetadata> GetMeasures()
		{
			return this.MeasuresMetadata.Values.Select((SapBwMdxCubeMetadataProvider.SapBwMeasureMetadata m) => m.MeasureMetadata);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00074618 File Offset: 0x00072818
		public override bool TryGetDefaultMeasure(out string uniqueName)
		{
			MdxMeasureMetadata mdxMeasureMetadata = this.GetMeasures().FirstOrDefault<MdxMeasureMetadata>();
			if (mdxMeasureMetadata != null)
			{
				uniqueName = mdxMeasureMetadata.UniqueName;
				return true;
			}
			uniqueName = null;
			return false;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000033E7 File Offset: 0x000015E7
		public override IEnumerable<MdxKpiMetadata> GetKPIs()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x00074642 File Offset: 0x00072842
		public override IEnumerable<MdxHierarchyMetadata> GetHierarchies()
		{
			return this.service.ExceptionHandler.InvokeWithRetry<List<MdxHierarchyMetadata>>(delegate
			{
				List<MdxHierarchyMetadata> list = new List<MdxHierarchyMetadata>();
				List<MdxHierarchyMetadata> list2;
				using (IDataReader dataReader = this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_HIERARCHYS", "HIERARCHIES", this.GetBaseRestrictions(false)))
				{
					while (dataReader.Read())
					{
						string @string = dataReader.GetString(4);
						string string2 = dataReader.GetString(2);
						MdxDimensionType mdxDimensionType = SapBwMdxCubeMetadataProvider.dimensionTypesFromString[dataReader.GetString(7)];
						if (!this.service.EnableStructures || (mdxDimensionType != MdxDimensionType.Unknown && mdxDimensionType != MdxDimensionType.Measure))
						{
							list.Add(new MdxHierarchyMetadata
							{
								UniqueName = @string,
								DimensionUniqueName = string2,
								UniqueIdentifier = dataReader.GetString(5),
								Caption = dataReader.GetString(6),
								DisplayFolder = string.Empty,
								Origin = ((@string == string2) ? 2 : 1),
								IsVisible = true
							});
						}
					}
					list2 = list;
				}
				return list2;
			});
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00074660 File Offset: 0x00072860
		public override IEnumerable<MdxLevelMetadata> GetLevels()
		{
			return this.service.ExceptionHandler.InvokeWithRetry<List<MdxLevelMetadata>>(delegate
			{
				List<MdxLevelMetadata> list2;
				using (IDataReader dataReader = this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_LEVELS", "LEVELS", this.GetBaseRestrictions(false)))
				{
					List<MdxLevelMetadata> list = new List<MdxLevelMetadata>();
					while (dataReader.Read())
					{
						string @string = dataReader.GetString(5);
						string string2 = dataReader.GetString(8);
						string string3 = dataReader.GetString(2);
						string string4 = dataReader.GetString(3);
						string string5 = dataReader.GetString(10);
						int num;
						if (string2 != "00" || string3 != string4 || (this.service.EnableStructures && int.TryParse(string5, out num) && num != 1))
						{
							list.Add(new MdxLevelMetadata
							{
								Caption = dataReader.GetString(7),
								UniqueName = @string,
								DimensionUniqueName = string3,
								HierarchyUniqueName = string4,
								Number = int.Parse(dataReader.GetString(8), CultureInfo.InvariantCulture)
							});
						}
					}
					list2 = list;
				}
				return list2;
			});
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000033E7 File Offset: 0x000015E7
		public override IEnumerable<MdxMeasureGroupMetadata> GetMeasureGroups()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0007467E File Offset: 0x0007287E
		public override IEnumerable<MdxCellPropertyMetadata> GetCellProperties()
		{
			return this.service.GetCellProperties();
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0007468C File Offset: 0x0007288C
		public override IEnumerable<MdxPropertyMetadata> GetProperties(MdxDimension dimension)
		{
			if (!this.service.EnableStructures || dimension.DimensionType != MdxDimensionType.OtherNoAggregation)
			{
				return this.ExecutePropertiesCommand(new Dictionary<string, OleDbType> { { dimension.MdxIdentifier, dimension.Type } }, dimension.MdxIdentifier);
			}
			return new List<MdxPropertyMetadata>();
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000746D8 File Offset: 0x000728D8
		public IEnumerable<MdxPropertyMetadata> GetAllLevelProperties(Dictionary<string, OleDbType> dimensionTypes)
		{
			HashSet<string> dimensionSet = new HashSet<string>(this.cube.Dimensions.Keys);
			bool enableStructures = this.service.EnableStructures;
			IEnumerator<MdxLevel> enumerator2;
			foreach (MdxPropertyMetadata property in this.ExecutePropertiesCommand(dimensionTypes, null))
			{
				MdxDimension mdxDimension = this.cube.Dimensions[property.DimensionUniqueName];
				if (!enableStructures || mdxDimension.DimensionType != MdxDimensionType.OtherNoAggregation)
				{
					bool addIntrinsicProperty = dimensionSet.Remove(mdxDimension.MdxIdentifier);
					foreach (MdxLevel level in mdxDimension.Hierarchies.Values.SelectMany((MdxHierarchy h) => h.Levels))
					{
						if (addIntrinsicProperty)
						{
							yield return SapBwMdxCubeMetadataProvider.CreateUniqueNameProperty(level);
							yield return SapBwMdxCubeMetadataProvider.CreateCaptionProperty(level);
						}
						yield return new MdxPropertyMetadata
						{
							UniqueName = property.UniqueName,
							DimensionUniqueName = property.DimensionUniqueName,
							HierarchyUniqueName = property.HierarchyUniqueName,
							LevelUniqueName = level.MdxIdentifier,
							KeyUniqueName = property.KeyUniqueName,
							Caption = property.Caption,
							DataType = property.DataType,
							PropertyKind = property.PropertyKind
						};
						level = null;
					}
					enumerator2 = null;
					property = null;
				}
			}
			IEnumerator<MdxPropertyMetadata> enumerator = null;
			foreach (MdxLevel level in dimensionSet.SelectMany((string dimId) => this.cube.Dimensions[dimId].Hierarchies.Values.SelectMany((MdxHierarchy h) => h.Levels)))
			{
				yield return SapBwMdxCubeMetadataProvider.CreateUniqueNameProperty(level);
				yield return SapBwMdxCubeMetadataProvider.CreateCaptionProperty(level);
				level = null;
			}
			enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000746F0 File Offset: 0x000728F0
		private static MdxPropertyMetadata CreateUniqueNameProperty(MdxLevel level)
		{
			MdxDimension dimension = level.Hierarchy.Dimension;
			return new MdxPropertyMetadata
			{
				UniqueName = MdxMemberProperties.QuotedMemberUniqueName,
				DimensionUniqueName = dimension.MdxIdentifier,
				LevelUniqueName = level.MdxIdentifier,
				KeyUniqueName = SapBwMdxCubeMetadataProvider.GetKeyUniqueName(MdxMemberProperties.QuotedMemberUniqueName, dimension.MdxIdentifier),
				Caption = level.Caption + ".UniqueName",
				DataType = OleDbType.BSTR,
				PropertyKind = MdxPropertyKind.MemberUniqueName
			};
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0007476C File Offset: 0x0007296C
		private static MdxPropertyMetadata CreateCaptionProperty(MdxLevel level)
		{
			MdxDimension dimension = level.Hierarchy.Dimension;
			string text = MdxIdentifier.QuotePart("MEMBER_CAPTION");
			return new MdxPropertyMetadata
			{
				UniqueName = text,
				DimensionUniqueName = dimension.MdxIdentifier,
				LevelUniqueName = level.MdxIdentifier,
				KeyUniqueName = SapBwMdxCubeMetadataProvider.GetKeyUniqueName(text, dimension.MdxIdentifier),
				Caption = level.Caption + ".Caption",
				DataType = OleDbType.BSTR,
				PropertyKind = MdxPropertyKind.MemberCaption
			};
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000747EC File Offset: 0x000729EC
		private static string GetKeyUniqueName(string uniqueName, string dimensionUniqueName)
		{
			string text = uniqueName.Trim(new char[] { '[', ']' });
			string text2 = "2" + text.Substring(1);
			string text3 = dimensionUniqueName.Trim(new char[] { '[', ']' });
			if (!text.Contains(text3))
			{
				return MdxIdentifier.QuotePart(text2);
			}
			return MdxMemberProperties.QuotedMemberUniqueName;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00074850 File Offset: 0x00072A50
		private IEnumerable<MdxPropertyMetadata> ExecutePropertiesCommand(Dictionary<string, OleDbType> dimensionTypes, string dimensionId = null)
		{
			Func<MdxPropertyMetadata, bool> <>9__1;
			return this.service.ExceptionHandler.InvokeWithRetry<IEnumerable<MdxPropertyMetadata>>(delegate
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				if (dimensionId != null && this.service.TryGetDataReaderFromCache(SapBwService.GetBapiCommandText("BAPI_MDPROVIDER_GET_PROPERTIES", "PROPERTIES", this.GetPropertiesRestrictions(null)), out dataReaderWithTableSchema))
				{
					IEnumerable<MdxPropertyMetadata> enumerable = this.ExtractProperties(dimensionTypes, dataReaderWithTableSchema);
					Func<MdxPropertyMetadata, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (MdxPropertyMetadata p) => p.DimensionUniqueName == dimensionId);
					}
					return enumerable.Where(func);
				}
				return this.ExtractProperties(dimensionTypes, this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_PROPERTIES", "PROPERTIES", this.GetPropertiesRestrictions(dimensionId)));
			});
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x00074894 File Offset: 0x00072A94
		private List<MdxPropertyMetadata> ExtractProperties(Dictionary<string, OleDbType> dimensionTypes, IDataReader reader)
		{
			return this.service.ExceptionHandler.InvokeWithRetry<List<MdxPropertyMetadata>>(delegate
			{
				List<MdxPropertyMetadata> list2;
				using (reader)
				{
					List<MdxPropertyMetadata> list = new List<MdxPropertyMetadata>();
					while (reader.Read())
					{
						string @string = reader.GetString(7);
						string string2 = reader.GetString(2);
						string string3 = reader.GetString(3);
						string string4 = reader.GetString(4);
						string string5 = reader.GetString(8);
						OleDbType oleDbType = OleDbType.Variant;
						SapBwDataType sapBwDataType;
						if ((@string.Trim(new char[] { '[', ']' })[0] != '2' || !dimensionTypes.TryGetValue(string2, out oleDbType) || (oleDbType != OleDbType.DBDate && oleDbType != OleDbType.DBTime)) && SapBwDataType.TryGetByName(reader.GetString(9), out sapBwDataType))
						{
							oleDbType = sapBwDataType.OleDbType;
						}
						list.Add(new MdxPropertyMetadata
						{
							UniqueName = @string,
							DimensionUniqueName = string2,
							HierarchyUniqueName = string3,
							LevelUniqueName = string4,
							KeyUniqueName = SapBwMdxCubeMetadataProvider.GetKeyUniqueName(@string, string2),
							Caption = string5,
							DataType = oleDbType,
							PropertyKind = MdxPropertyKind.UserDefined
						});
					}
					list2 = list;
				}
				return list2;
			});
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000748D4 File Offset: 0x00072AD4
		private SapBwRestrictions GetBaseRestrictions(bool includeStructures = false)
		{
			SapBwRestrictions sapBwRestrictions = new SapBwRestrictions
			{
				{ "CAT_NAM", this.catalogName },
				{
					"CUBE_NAM",
					this.cube.Name
				}
			};
			if (includeStructures && this.service.EnableStructures)
			{
				sapBwRestrictions.Add("ONLY_STRUCTURES", "X");
			}
			return sapBwRestrictions;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00074930 File Offset: 0x00072B30
		private SapBwRestrictions GetPropertiesRestrictions(string dimensionId)
		{
			SapBwRestrictions sapBwRestrictions = new SapBwRestrictions { 
			{
				"CUBE_NAM",
				this.cube.Name
			} };
			if (dimensionId != null)
			{
				sapBwRestrictions.Add("DIM_UNAM", dimensionId);
			}
			sapBwRestrictions.Add("PRPTY_TYPE", 1);
			return sapBwRestrictions;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x0007497C File Offset: 0x00072B7C
		public bool TryGetMeasureInfo(string identifier, out string valueType, out int? precision)
		{
			SapBwMdxCubeMetadataProvider.SapBwMeasureMetadata sapBwMeasureMetadata;
			if (this.MeasuresMetadata.TryGetValue(identifier, out sapBwMeasureMetadata))
			{
				valueType = sapBwMeasureMetadata.ValueType;
				precision = sapBwMeasureMetadata.Precision;
				return true;
			}
			valueType = null;
			precision = null;
			return false;
		}

		// Token: 0x040010A6 RID: 4262
		private const char PropertyPrefixKeyText = '0';

		// Token: 0x040010A7 RID: 4263
		private const char PropertyPrefixText = '1';

		// Token: 0x040010A8 RID: 4264
		public const char PropertyPrefixKey = '2';

		// Token: 0x040010A9 RID: 4265
		private const char PropertyPrefixTextKey = '3';

		// Token: 0x040010AA RID: 4266
		private const char PropertyPrefixTextLong = '4';

		// Token: 0x040010AB RID: 4267
		private const char PropertyPrefixTextMedium = '5';

		// Token: 0x040010AC RID: 4268
		private const char PropertyPrefixNonCompoundedKey = '8';

		// Token: 0x040010AD RID: 4269
		private const char PropertyPrefixTextKeyShort = 'B';

		// Token: 0x040010AE RID: 4270
		private const char PropertyPrefixTextKeyMedium = 'C';

		// Token: 0x040010AF RID: 4271
		private const char PropertyPrefixTextKeyLong = 'D';

		// Token: 0x040010B0 RID: 4272
		private const char PropertyPrefixKeyTextShort = 'E';

		// Token: 0x040010B1 RID: 4273
		private const char PropertyPrefixKeyTextMedium = 'F';

		// Token: 0x040010B2 RID: 4274
		private const char PropertyPrefixKeyTextLong = 'G';

		// Token: 0x040010B3 RID: 4275
		private static readonly Dictionary<string, MdxDimensionType> dimensionTypesFromString = new Dictionary<string, MdxDimensionType>
		{
			{
				"0",
				MdxDimensionType.Unknown
			},
			{
				"1",
				MdxDimensionType.Time
			},
			{
				"2",
				MdxDimensionType.Measure
			},
			{
				"3",
				MdxDimensionType.Other
			},
			{
				"4",
				MdxDimensionType.OtherNoAggregation
			}
		};

		// Token: 0x040010B4 RID: 4276
		private readonly ISapBwService service;

		// Token: 0x040010B5 RID: 4277
		private readonly string catalogName;

		// Token: 0x040010B6 RID: 4278
		private readonly SapBwMdxCube cube;

		// Token: 0x040010B7 RID: 4279
		private Dictionary<string, SapBwMdxCubeMetadataProvider.SapBwMeasureMetadata> measuresMetadata;

		// Token: 0x020004B6 RID: 1206
		private class SapBwMeasureMetadata
		{
			// Token: 0x17000F89 RID: 3977
			// (get) Token: 0x060027B7 RID: 10167 RVA: 0x00074E88 File Offset: 0x00073088
			// (set) Token: 0x060027B8 RID: 10168 RVA: 0x00074E90 File Offset: 0x00073090
			public MdxMeasureMetadata MeasureMetadata { get; set; }

			// Token: 0x17000F8A RID: 3978
			// (get) Token: 0x060027B9 RID: 10169 RVA: 0x00074E99 File Offset: 0x00073099
			// (set) Token: 0x060027BA RID: 10170 RVA: 0x00074EA1 File Offset: 0x000730A1
			public string ValueType { get; set; }

			// Token: 0x17000F8B RID: 3979
			// (get) Token: 0x060027BB RID: 10171 RVA: 0x00074EAA File Offset: 0x000730AA
			// (set) Token: 0x060027BC RID: 10172 RVA: 0x00074EB2 File Offset: 0x000730B2
			public int? Precision { get; set; }
		}
	}
}
