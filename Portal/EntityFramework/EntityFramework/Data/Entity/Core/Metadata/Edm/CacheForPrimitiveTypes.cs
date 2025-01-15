using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm.Provider;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200048B RID: 1163
	internal class CacheForPrimitiveTypes
	{
		// Token: 0x060039AD RID: 14765 RVA: 0x000BDC88 File Offset: 0x000BBE88
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

		// Token: 0x060039AE RID: 14766 RVA: 0x000BDCD4 File Offset: 0x000BBED4
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

		// Token: 0x060039AF RID: 14767 RVA: 0x000BDE48 File Offset: 0x000BC048
		private static Facet[] CreateInitialFacets(FacetDescription[] facetDescriptions)
		{
			Facet[] array = new Facet[facetDescriptions.Length];
			for (int i = 0; i < facetDescriptions.Length; i++)
			{
				string facetName = facetDescriptions[i].FacetName;
				if (facetName != null)
				{
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
								array[i] = Facet.Create(facetDescriptions[i], false);
							}
						}
						else
						{
							array[i] = Facet.Create(facetDescriptions[i], true);
						}
					}
					else
					{
						array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultMaxLengthFacetValue);
					}
				}
			}
			return array;
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000BDF20 File Offset: 0x000BC120
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
			return new ReadOnlyCollection<PrimitiveType>(list);
		}

		// Token: 0x04001340 RID: 4928
		private readonly List<PrimitiveType>[] _primitiveTypeMap = new List<PrimitiveType>[32];
	}
}
