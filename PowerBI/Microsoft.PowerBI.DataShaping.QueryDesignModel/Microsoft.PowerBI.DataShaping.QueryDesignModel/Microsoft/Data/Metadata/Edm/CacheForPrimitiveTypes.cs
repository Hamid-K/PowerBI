using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200006D RID: 109
	internal class CacheForPrimitiveTypes
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x00014848 File Offset: 0x00012A48
		internal void Add(PrimitiveType type)
		{
			List<PrimitiveType> list = EntityUtil.CheckArgumentOutOfRange<List<PrimitiveType>>(this._primitiveTypeMap, (int)type.PrimitiveTypeKind, "primitiveTypeKind");
			if (list == null)
			{
				list = new List<PrimitiveType>();
				list.Add(type);
				this._primitiveTypeMap[(int)type.PrimitiveTypeKind] = list;
				return;
			}
			list.Add(type);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00014894 File Offset: 0x00012A94
		internal bool TryGetType(PrimitiveTypeKind primitiveTypeKind, IEnumerable<Facet> facets, out PrimitiveType type)
		{
			type = null;
			List<PrimitiveType> list = EntityUtil.CheckArgumentOutOfRange<List<PrimitiveType>>(this._primitiveTypeMap, (int)primitiveTypeKind, "primitiveTypeKind");
			if (list == null || 0 >= list.Count)
			{
				return false;
			}
			if (list.Count == 1)
			{
				type = list[0];
				return true;
			}
			if (facets == null)
			{
				FacetDescription[] initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(primitiveTypeKind);
				if (initialFacetDescriptions == null)
				{
					type = list[0];
					return true;
				}
				facets = CacheForPrimitiveTypes.CreateInitialFacets(initialFacetDescriptions);
			}
			bool flag = false;
			foreach (Facet facet in facets)
			{
				if ((primitiveTypeKind == PrimitiveTypeKind.String || primitiveTypeKind == PrimitiveTypeKind.Binary) && facet.Value != null && facet.Name == "MaxLength" && Helper.IsUnboundedFacetValue(facet))
				{
					flag = true;
				}
			}
			int num = 0;
			foreach (PrimitiveType primitiveType in list)
			{
				if (!flag)
				{
					type = primitiveType;
					break;
				}
				if (type == null)
				{
					type = primitiveType;
					num = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength").MaxValue.Value;
				}
				else
				{
					int value = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength").MaxValue.Value;
					if (value > num)
					{
						type = primitiveType;
						num = value;
					}
				}
			}
			return true;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00014A08 File Offset: 0x00012C08
		private static Facet[] CreateInitialFacets(FacetDescription[] facetDescriptions)
		{
			Facet[] array = new Facet[facetDescriptions.Length];
			for (int i = 0; i < facetDescriptions.Length; i++)
			{
				string facetName = facetDescriptions[i].FacetName;
				if (!(facetName == "MaxLength"))
				{
					if (!(facetName == "Unicode"))
					{
						if (!(facetName == "FixedLength"))
						{
							if (!(facetName == "Precision"))
							{
								if (facetName == "Scale")
								{
									array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultScaleFacetValue);
								}
							}
							else
							{
								array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultPrecisionFacetValue);
							}
						}
						else
						{
							array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultFixedLengthFacetValue);
						}
					}
					else
					{
						array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultUnicodeFacetValue);
					}
				}
				else
				{
					array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultMaxLengthFacetValue);
				}
			}
			return array;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00014AE4 File Offset: 0x00012CE4
		internal ReadOnlyCollection<PrimitiveType> GetTypes()
		{
			List<PrimitiveType> list = new List<PrimitiveType>();
			foreach (List<PrimitiveType> list2 in this._primitiveTypeMap)
			{
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x0400071E RID: 1822
		private List<PrimitiveType>[] _primitiveTypeMap = new List<PrimitiveType>[15];
	}
}
