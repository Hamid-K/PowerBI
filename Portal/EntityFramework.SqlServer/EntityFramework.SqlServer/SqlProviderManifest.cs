using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000F RID: 15
	internal class SqlProviderManifest : DbXmlEnabledProviderManifest
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00003B58 File Offset: 0x00001D58
		public SqlProviderManifest(string manifestToken)
			: base(SqlProviderManifest.GetProviderManifest())
		{
			this._version = SqlVersionUtils.GetSqlVersion(manifestToken);
			this.Initialize();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003B80 File Offset: 0x00001D80
		private void Initialize()
		{
			if (this._version == SqlVersion.Sql10 || this._version == SqlVersion.Sql11)
			{
				this._primitiveTypes = base.GetStoreTypes();
				this._functions = base.GetStoreFunctions();
				return;
			}
			List<PrimitiveType> list = new List<PrimitiveType>(base.GetStoreTypes());
			list.RemoveAll((PrimitiveType primitiveType) => primitiveType.Name.Equals("time", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("date", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("datetime2", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("datetimeoffset", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("hierarchyid", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("geography", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("geometry", StringComparison.OrdinalIgnoreCase));
			if (this._version == SqlVersion.Sql8)
			{
				list.RemoveAll((PrimitiveType primitiveType) => primitiveType.Name.Equals("xml", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.EndsWith("(max)", StringComparison.OrdinalIgnoreCase));
			}
			this._primitiveTypes = new ReadOnlyCollection<PrimitiveType>(list);
			IEnumerable<EdmFunction> enumerable = from f in base.GetStoreFunctions()
				where !SqlProviderManifest.IsKatmaiOrNewer(f)
				select f;
			if (this._version == SqlVersion.Sql8)
			{
				enumerable = enumerable.Where((EdmFunction f) => !SqlProviderManifest.IsYukonOrNewer(f));
			}
			this._functions = new ReadOnlyCollection<EdmFunction>(enumerable.ToList<EdmFunction>());
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003C94 File Offset: 0x00001E94
		internal SqlVersion SqlVersion
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003C9C File Offset: 0x00001E9C
		private static XmlReader GetXmlResource(string resourceName)
		{
			return XmlReader.Create(typeof(SqlProviderManifest).Assembly().GetManifestResourceStream(resourceName));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003CB8 File Offset: 0x00001EB8
		internal static XmlReader GetProviderManifest()
		{
			return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices.ProviderManifest.xml");
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003CC4 File Offset: 0x00001EC4
		internal static XmlReader GetStoreSchemaMapping(string mslName)
		{
			return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + mslName + ".msl");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003CDB File Offset: 0x00001EDB
		internal XmlReader GetStoreSchemaDescription(string ssdlName)
		{
			if (this._version == SqlVersion.Sql8)
			{
				return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + ssdlName + "_Sql8.ssdl");
			}
			return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + ssdlName + ".ssdl");
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003D14 File Offset: 0x00001F14
		internal static string EscapeLikeText(string text, bool alwaysEscapeEscapeChar, out bool usedEscapeChar)
		{
			usedEscapeChar = false;
			if (!text.Contains("%") && !text.Contains("_") && !text.Contains("[") && !text.Contains("^") && (!alwaysEscapeEscapeChar || !text.Contains("~")))
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (char c in text)
			{
				if (c == '%' || c == '_' || c == '[' || c == '^' || c == '~')
				{
					stringBuilder.Append('~');
					usedEscapeChar = true;
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003DC4 File Offset: 0x00001FC4
		protected override XmlReader GetDbInformation(string informationType)
		{
			if (informationType == "StoreSchemaDefinitionVersion3" || informationType == "StoreSchemaDefinition")
			{
				return this.GetStoreSchemaDescription(informationType);
			}
			if (informationType == "StoreSchemaMappingVersion3" || informationType == "StoreSchemaMapping")
			{
				return SqlProviderManifest.GetStoreSchemaMapping(informationType);
			}
			if (informationType == "ConceptualSchemaDefinitionVersion3" || informationType == "ConceptualSchemaDefinition")
			{
				return null;
			}
			throw new ProviderIncompatibleException(Strings.ProviderReturnedNullForGetDbInformation(informationType));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003E3B File Offset: 0x0000203B
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			return this._primitiveTypes;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003E43 File Offset: 0x00002043
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return this._functions;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003E4C File Offset: 0x0000204C
		private static bool IsKatmaiOrNewer(EdmFunction edmFunction)
		{
			if (edmFunction.ReturnParameter == null || !edmFunction.ReturnParameter.TypeUsage.IsSpatialType())
			{
				if (!edmFunction.Parameters.Any((FunctionParameter p) => p.TypeUsage.IsSpatialType()))
				{
					ReadOnlyMetadataCollection<FunctionParameter> parameters = edmFunction.Parameters;
					string text = edmFunction.Name.ToUpperInvariant();
					if (text != null)
					{
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
						if (num > 1900062112U)
						{
							if (num <= 2897371749U)
							{
								if (num <= 2725144690U)
								{
									if (num != 2585027932U)
									{
										if (num != 2725144690U)
										{
											return false;
										}
										if (!(text == "DATENAME"))
										{
											return false;
										}
									}
									else
									{
										if (!(text == "YEAR"))
										{
											return false;
										}
										goto IL_0305;
									}
								}
								else if (num != 2750919380U)
								{
									if (num != 2897371749U)
									{
										return false;
									}
									if (!(text == "COUNT_BIG"))
									{
										return false;
									}
									goto IL_02C2;
								}
								else
								{
									if (!(text == "COUNT"))
									{
										return false;
									}
									goto IL_02C2;
								}
							}
							else if (num <= 3457630659U)
							{
								if (num != 3224348880U)
								{
									if (num != 3457630659U)
									{
										return false;
									}
									if (!(text == "SYSDATETIME"))
									{
										return false;
									}
									return true;
								}
								else if (!(text == "DATEPART"))
								{
									return false;
								}
							}
							else if (num != 3865452901U)
							{
								if (num != 4246149173U)
								{
									return false;
								}
								if (!(text == "DATALENGTH"))
								{
									return false;
								}
								goto IL_0305;
							}
							else
							{
								if (!(text == "MONTH"))
								{
									return false;
								}
								goto IL_0305;
							}
							string name = parameters[1].TypeUsage.EdmType.Name;
							return name.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name.Equals("Time", StringComparison.OrdinalIgnoreCase);
						}
						if (num <= 688247133U)
						{
							if (num <= 437886384U)
							{
								if (num != 239465655U)
								{
									if (num != 437886384U)
									{
										return false;
									}
									if (!(text == "SYSDATETIMEOFFSET"))
									{
										return false;
									}
									return true;
								}
								else if (!(text == "MIN"))
								{
									return false;
								}
							}
							else if (num != 475632249U)
							{
								if (num != 688247133U)
								{
									return false;
								}
								if (!(text == "DAY"))
								{
									return false;
								}
								goto IL_0305;
							}
							else if (!(text == "MAX"))
							{
								return false;
							}
						}
						else
						{
							if (num > 999103698U)
							{
								if (num != 1674423462U)
								{
									if (num != 1900062112U)
									{
										return false;
									}
									if (!(text == "DATEADD"))
									{
										return false;
									}
								}
								else if (!(text == "DATEDIFF"))
								{
									return false;
								}
								string name2 = parameters[1].TypeUsage.EdmType.Name;
								string name3 = parameters[2].TypeUsage.EdmType.Name;
								return name2.Equals("Time", StringComparison.OrdinalIgnoreCase) || name3.Equals("Time", StringComparison.OrdinalIgnoreCase) || name2.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name3.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase);
							}
							if (num != 719394705U)
							{
								if (num != 999103698U)
								{
									return false;
								}
								if (!(text == "CHECKSUM"))
								{
									return false;
								}
								goto IL_0305;
							}
							else
							{
								if (!(text == "SYSUTCDATETIME"))
								{
									return false;
								}
								return true;
							}
						}
						IL_02C2:
						string name4 = ((CollectionType)parameters[0].TypeUsage.EdmType).TypeUsage.EdmType.Name;
						return name4.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name4.Equals("Time", StringComparison.OrdinalIgnoreCase);
						IL_0305:
						string name5 = parameters[0].TypeUsage.EdmType.Name;
						return name5.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name5.Equals("Time", StringComparison.OrdinalIgnoreCase);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000423C File Offset: 0x0000243C
		private static bool IsYukonOrNewer(EdmFunction edmFunction)
		{
			ReadOnlyMetadataCollection<FunctionParameter> parameters = edmFunction.Parameters;
			if (parameters == null || parameters.Count == 0)
			{
				return false;
			}
			string text = edmFunction.Name.ToUpperInvariant();
			if (text != null)
			{
				if (text == "COUNT" || text == "COUNT_BIG")
				{
					return ((CollectionType)parameters[0].TypeUsage.EdmType).TypeUsage.EdmType.Name.Equals("Guid", StringComparison.OrdinalIgnoreCase);
				}
				if (text == "CHARINDEX")
				{
					using (ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = parameters.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.TypeUsage.EdmType.Name.Equals("Int64", StringComparison.OrdinalIgnoreCase))
							{
								return true;
							}
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004328 File Offset: 0x00002528
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			Check.NotNull<TypeUsage>(storeType, "storeType");
			string text = storeType.EdmType.Name.ToLowerInvariant();
			if (!base.StoreTypeNameToEdmPrimitiveType.ContainsKey(text))
			{
				throw new ArgumentException(Strings.ProviderDoesNotSupportType(text));
			}
			PrimitiveType primitiveType = base.StoreTypeNameToEdmPrimitiveType[text];
			int num = 0;
			bool flag = true;
			if (text != null)
			{
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
				PrimitiveTypeKind primitiveTypeKind;
				bool flag2;
				bool flag3;
				if (num2 > 2746163861U)
				{
					if (num2 <= 3437915536U)
					{
						if (num2 <= 3008443898U)
						{
							if (num2 <= 2823553821U)
							{
								if (num2 != 2797886853U)
								{
									if (num2 != 2823553821U)
									{
										goto IL_0737;
									}
									if (!(text == "char"))
									{
										goto IL_0737;
									}
									primitiveTypeKind = PrimitiveTypeKind.String;
									flag2 = !storeType.TryGetMaxLength(out num);
									flag = false;
									flag3 = true;
									goto IL_0743;
								}
								else if (!(text == "float"))
								{
									goto IL_0737;
								}
							}
							else if (num2 != 2994984227U)
							{
								if (num2 != 3008443898U)
								{
									goto IL_0737;
								}
								if (!(text == "image"))
								{
									goto IL_0737;
								}
								goto IL_06A5;
							}
							else
							{
								if (!(text == "timestamp"))
								{
									goto IL_0737;
								}
								goto IL_06B3;
							}
						}
						else if (num2 <= 3286697625U)
						{
							if (num2 != 3185987134U)
							{
								if (num2 != 3286697625U)
								{
									goto IL_0737;
								}
								if (!(text == "geography"))
								{
									goto IL_0737;
								}
								goto IL_05E0;
							}
							else
							{
								if (!(text == "text"))
								{
									goto IL_0737;
								}
								goto IL_0653;
							}
						}
						else if (num2 != 3347933383U)
						{
							if (num2 != 3431564149U)
							{
								if (num2 != 3437915536U)
								{
									goto IL_0737;
								}
								if (!(text == "datetime"))
								{
									goto IL_0737;
								}
								goto IL_06FD;
							}
							else
							{
								if (!(text == "uniqueidentifier"))
								{
									goto IL_0737;
								}
								goto IL_05E0;
							}
						}
						else
						{
							if (!(text == "varbinary"))
							{
								goto IL_0737;
							}
							primitiveTypeKind = PrimitiveTypeKind.Binary;
							flag2 = !storeType.TryGetMaxLength(out num);
							flag3 = false;
							goto IL_0743;
						}
					}
					else if (num2 <= 3664801462U)
					{
						if (num2 <= 3604983901U)
						{
							if (num2 != 3564297305U)
							{
								if (num2 != 3604983901U)
								{
									goto IL_0737;
								}
								if (!(text == "real"))
								{
									goto IL_0737;
								}
							}
							else
							{
								if (!(text == "date"))
								{
									goto IL_0737;
								}
								return TypeUsage.CreateDefaultTypeUsage(primitiveType);
							}
						}
						else if (num2 != 3659634113U)
						{
							if (num2 != 3664801462U)
							{
								goto IL_0737;
							}
							if (!(text == "xml"))
							{
								goto IL_0737;
							}
							goto IL_0664;
						}
						else
						{
							if (!(text == "smalldatetime"))
							{
								goto IL_0737;
							}
							goto IL_06FD;
						}
					}
					else if (num2 <= 3761451113U)
					{
						if (num2 != 3716508924U)
						{
							if (num2 != 3761451113U)
							{
								goto IL_0737;
							}
							if (!(text == "nchar"))
							{
								goto IL_0737;
							}
							primitiveTypeKind = PrimitiveTypeKind.String;
							flag2 = !storeType.TryGetMaxLength(out num);
							flag = true;
							flag3 = true;
							goto IL_0743;
						}
						else
						{
							if (!(text == "binary"))
							{
								goto IL_0737;
							}
							primitiveTypeKind = PrimitiveTypeKind.Binary;
							flag2 = !storeType.TryGetMaxLength(out num);
							flag3 = true;
							goto IL_0743;
						}
					}
					else if (num2 != 3780168015U)
					{
						if (num2 != 3918255874U)
						{
							if (num2 != 4163743794U)
							{
								goto IL_0737;
							}
							if (!(text == "varchar"))
							{
								goto IL_0737;
							}
							primitiveTypeKind = PrimitiveTypeKind.String;
							flag2 = !storeType.TryGetMaxLength(out num);
							flag = false;
							flag3 = false;
							goto IL_0743;
						}
						else
						{
							if (!(text == "ntext"))
							{
								goto IL_0737;
							}
							goto IL_0664;
						}
					}
					else
					{
						if (!(text == "money"))
						{
							goto IL_0737;
						}
						return TypeUsage.CreateDecimalTypeUsage(primitiveType, 19, 4);
					}
					return TypeUsage.CreateDefaultTypeUsage(primitiveType);
				}
				if (num2 <= 1564253156U)
				{
					if (num2 <= 750634308U)
					{
						if (num2 <= 520654156U)
						{
							if (num2 != 132678327U)
							{
								if (num2 != 520654156U)
								{
									goto IL_0737;
								}
								if (!(text == "decimal"))
								{
									goto IL_0737;
								}
							}
							else
							{
								if (!(text == "nvarchar(max)"))
								{
									goto IL_0737;
								}
								goto IL_0664;
							}
						}
						else if (num2 != 711820689U)
						{
							if (num2 != 750634308U)
							{
								goto IL_0737;
							}
							if (!(text == "tinyint"))
							{
								goto IL_0737;
							}
							goto IL_05E0;
						}
						else
						{
							if (!(text == "geometry"))
							{
								goto IL_0737;
							}
							goto IL_05E0;
						}
					}
					else if (num2 <= 956906072U)
					{
						if (num2 != 923440646U)
						{
							if (num2 != 956906072U)
							{
								goto IL_0737;
							}
							if (!(text == "smallmoney"))
							{
								goto IL_0737;
							}
							return TypeUsage.CreateDecimalTypeUsage(primitiveType, 10, 4);
						}
						else
						{
							if (!(text == "datetime2"))
							{
								goto IL_0737;
							}
							goto IL_06FD;
						}
					}
					else if (num2 != 1498571224U)
					{
						if (num2 != 1539863742U)
						{
							if (num2 != 1564253156U)
							{
								goto IL_0737;
							}
							if (!(text == "time"))
							{
								goto IL_0737;
							}
							return TypeUsage.CreateTimeTypeUsage(primitiveType, null);
						}
						else
						{
							if (!(text == "nvarchar"))
							{
								goto IL_0737;
							}
							primitiveTypeKind = PrimitiveTypeKind.String;
							flag2 = !storeType.TryGetMaxLength(out num);
							flag = true;
							flag3 = false;
							goto IL_0743;
						}
					}
					else
					{
						if (!(text == "varbinary(max)"))
						{
							goto IL_0737;
						}
						goto IL_06A5;
					}
				}
				else if (num2 <= 2174562837U)
				{
					if (num2 <= 1761125480U)
					{
						if (num2 != 1623908912U)
						{
							if (num2 != 1761125480U)
							{
								goto IL_0737;
							}
							if (!(text == "numeric"))
							{
								goto IL_0737;
							}
						}
						else
						{
							if (!(text == "bit"))
							{
								goto IL_0737;
							}
							goto IL_05E0;
						}
					}
					else if (num2 != 1762504443U)
					{
						if (num2 != 2174562837U)
						{
							goto IL_0737;
						}
						if (!(text == "smallint"))
						{
							goto IL_0737;
						}
						goto IL_05E0;
					}
					else
					{
						if (!(text == "varchar(max)"))
						{
							goto IL_0737;
						}
						goto IL_0653;
					}
				}
				else if (num2 <= 2336348659U)
				{
					if (num2 != 2322048458U)
					{
						if (num2 != 2336348659U)
						{
							goto IL_0737;
						}
						if (!(text == "datetimeoffset"))
						{
							goto IL_0737;
						}
						return TypeUsage.CreateDateTimeOffsetTypeUsage(primitiveType, null);
					}
					else
					{
						if (!(text == "bigint"))
						{
							goto IL_0737;
						}
						goto IL_05E0;
					}
				}
				else if (num2 != 2515107422U)
				{
					if (num2 != 2603927413U)
					{
						if (num2 != 2746163861U)
						{
							goto IL_0737;
						}
						if (!(text == "hierarchyid"))
						{
							goto IL_0737;
						}
						goto IL_05E0;
					}
					else
					{
						if (!(text == "rowversion"))
						{
							goto IL_0737;
						}
						goto IL_06B3;
					}
				}
				else
				{
					if (!(text == "int"))
					{
						goto IL_0737;
					}
					goto IL_05E0;
				}
				byte b;
				byte b2;
				if (storeType.TryGetPrecision(out b) && storeType.TryGetScale(out b2))
				{
					return TypeUsage.CreateDecimalTypeUsage(primitiveType, b, b2);
				}
				return TypeUsage.CreateDecimalTypeUsage(primitiveType);
				IL_05E0:
				return TypeUsage.CreateDefaultTypeUsage(primitiveType);
				IL_0653:
				primitiveTypeKind = PrimitiveTypeKind.String;
				flag2 = true;
				flag = false;
				flag3 = false;
				goto IL_0743;
				IL_0664:
				primitiveTypeKind = PrimitiveTypeKind.String;
				flag2 = true;
				flag = true;
				flag3 = false;
				goto IL_0743;
				IL_06A5:
				primitiveTypeKind = PrimitiveTypeKind.Binary;
				flag2 = true;
				flag3 = false;
				goto IL_0743;
				IL_06B3:
				return TypeUsage.CreateBinaryTypeUsage(primitiveType, true, 8);
				IL_06FD:
				return TypeUsage.CreateDateTimeTypeUsage(primitiveType, null);
				IL_0743:
				if (primitiveTypeKind != PrimitiveTypeKind.Binary)
				{
					if (primitiveTypeKind != PrimitiveTypeKind.String)
					{
						throw new NotSupportedException(Strings.ProviderDoesNotSupportType(text));
					}
					if (!flag2)
					{
						return TypeUsage.CreateStringTypeUsage(primitiveType, flag, flag3, num);
					}
					return TypeUsage.CreateStringTypeUsage(primitiveType, flag, flag3);
				}
				else
				{
					if (!flag2)
					{
						return TypeUsage.CreateBinaryTypeUsage(primitiveType, flag3, num);
					}
					return TypeUsage.CreateBinaryTypeUsage(primitiveType, flag3);
				}
			}
			IL_0737:
			throw new NotSupportedException(Strings.ProviderDoesNotSupportType(text));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			Check.NotNull<TypeUsage>(edmType, "edmType");
			PrimitiveType primitiveType = edmType.EdmType as PrimitiveType;
			if (primitiveType == null)
			{
				throw new ArgumentException(Strings.ProviderDoesNotSupportType(edmType.EdmType.Name));
			}
			ReadOnlyMetadataCollection<Facet> facets = edmType.Facets;
			switch (primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			{
				bool flag = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet = facets["MaxLength"];
				bool flag2 = facet.IsUnbounded || facet.Value == null || (int)facet.Value > 8000;
				int num = ((!flag2) ? ((int)facet.Value) : int.MinValue);
				TypeUsage typeUsage;
				if (flag)
				{
					typeUsage = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["binary"], true, flag2 ? 8000 : num);
				}
				else if (flag2)
				{
					if (this._version != SqlVersion.Sql8)
					{
						typeUsage = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["varbinary(max)"], false);
					}
					else
					{
						typeUsage = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["varbinary"], false, 8000);
					}
				}
				else
				{
					typeUsage = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["varbinary"], false, num);
				}
				return typeUsage;
			}
			case PrimitiveTypeKind.Boolean:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["bit"]);
			case PrimitiveTypeKind.Byte:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["tinyint"]);
			case PrimitiveTypeKind.DateTime:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["datetime"]);
			case PrimitiveTypeKind.Decimal:
			{
				byte b;
				if (!edmType.TryGetPrecision(out b))
				{
					b = 18;
				}
				byte b2;
				if (!edmType.TryGetScale(out b2))
				{
					b2 = 0;
				}
				return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["decimal"], b, b2);
			}
			case PrimitiveTypeKind.Double:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["float"]);
			case PrimitiveTypeKind.Guid:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["uniqueidentifier"]);
			case PrimitiveTypeKind.Single:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["real"]);
			case PrimitiveTypeKind.Int16:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["smallint"]);
			case PrimitiveTypeKind.Int32:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["int"]);
			case PrimitiveTypeKind.Int64:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["bigint"]);
			case PrimitiveTypeKind.String:
			{
				bool flag3 = facets["Unicode"].Value == null || (bool)facets["Unicode"].Value;
				bool flag4 = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet2 = facets["MaxLength"];
				bool flag5 = facet2.IsUnbounded || facet2.Value == null || (int)facet2.Value > (flag3 ? 4000 : 8000);
				int num2 = ((!flag5) ? ((int)facet2.Value) : int.MinValue);
				TypeUsage typeUsage2;
				if (flag3)
				{
					if (flag4)
					{
						typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nchar"], true, true, flag5 ? 4000 : num2);
					}
					else if (flag5)
					{
						if (this._version != SqlVersion.Sql8)
						{
							typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nvarchar(max)"], true, false);
						}
						else
						{
							typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nvarchar"], true, false, 4000);
						}
					}
					else
					{
						typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nvarchar"], true, false, num2);
					}
				}
				else if (flag4)
				{
					typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["char"], false, true, flag5 ? 8000 : num2);
				}
				else if (flag5)
				{
					if (this._version != SqlVersion.Sql8)
					{
						typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["varchar(max)"], false, false);
					}
					else
					{
						typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["varchar"], false, false, 8000);
					}
				}
				else
				{
					typeUsage2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["varchar"], false, false, num2);
				}
				return typeUsage2;
			}
			case PrimitiveTypeKind.Time:
				return this.GetStorePrimitiveTypeIfPostSql9("time", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.DateTimeOffset:
				return this.GetStorePrimitiveTypeIfPostSql9("datetimeoffset", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.Geometry:
			case PrimitiveTypeKind.GeometryPoint:
			case PrimitiveTypeKind.GeometryLineString:
			case PrimitiveTypeKind.GeometryPolygon:
			case PrimitiveTypeKind.GeometryMultiPoint:
			case PrimitiveTypeKind.GeometryMultiLineString:
			case PrimitiveTypeKind.GeometryMultiPolygon:
			case PrimitiveTypeKind.GeometryCollection:
				return this.GetStorePrimitiveTypeIfPostSql9("geometry", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.Geography:
			case PrimitiveTypeKind.GeographyPoint:
			case PrimitiveTypeKind.GeographyLineString:
			case PrimitiveTypeKind.GeographyPolygon:
			case PrimitiveTypeKind.GeographyMultiPoint:
			case PrimitiveTypeKind.GeographyMultiLineString:
			case PrimitiveTypeKind.GeographyMultiPolygon:
			case PrimitiveTypeKind.GeographyCollection:
				return this.GetStorePrimitiveTypeIfPostSql9("geography", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.HierarchyId:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["hierarchyid"]);
			}
			throw new NotSupportedException(Strings.NoStoreTypeForEdmType(edmType.EdmType.Name, primitiveType.PrimitiveTypeKind));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000502C File Offset: 0x0000322C
		private TypeUsage GetStorePrimitiveTypeIfPostSql9(string storeTypeName, string nameForException, PrimitiveTypeKind primitiveTypeKind)
		{
			if (this.SqlVersion != SqlVersion.Sql8 && this.SqlVersion != SqlVersion.Sql9)
			{
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType[storeTypeName]);
			}
			throw new NotSupportedException(Strings.NoStoreTypeForEdmType(nameForException, primitiveTypeKind));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005065 File Offset: 0x00003265
		public override bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '~';
			return true;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000506C File Offset: 0x0000326C
		public override string EscapeLikeArgument(string argument)
		{
			Check.NotNull<string>(argument, "argument");
			bool flag;
			return SqlProviderManifest.EscapeLikeText(argument, true, out flag);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000508E File Offset: 0x0000328E
		public override bool SupportsParameterOptimizationInSchemaQueries()
		{
			return true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005091 File Offset: 0x00003291
		public override bool SupportsInExpression()
		{
			return true;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005094 File Offset: 0x00003294
		public override bool SupportsIntersectAndUnionAllFlattening()
		{
			return true;
		}

		// Token: 0x0400000A RID: 10
		internal const string TokenSql8 = "2000";

		// Token: 0x0400000B RID: 11
		internal const string TokenSql9 = "2005";

		// Token: 0x0400000C RID: 12
		internal const string TokenSql10 = "2008";

		// Token: 0x0400000D RID: 13
		internal const string TokenSql11 = "2012";

		// Token: 0x0400000E RID: 14
		internal const string TokenAzure11 = "2012.Azure";

		// Token: 0x0400000F RID: 15
		internal const char LikeEscapeChar = '~';

		// Token: 0x04000010 RID: 16
		internal const string LikeEscapeCharToString = "~";

		// Token: 0x04000011 RID: 17
		private readonly SqlVersion _version = SqlVersion.Sql9;

		// Token: 0x04000012 RID: 18
		private const int varcharMaxSize = 8000;

		// Token: 0x04000013 RID: 19
		private const int nvarcharMaxSize = 4000;

		// Token: 0x04000014 RID: 20
		private const int binaryMaxSize = 8000;

		// Token: 0x04000015 RID: 21
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000016 RID: 22
		private ReadOnlyCollection<EdmFunction> _functions;
	}
}
