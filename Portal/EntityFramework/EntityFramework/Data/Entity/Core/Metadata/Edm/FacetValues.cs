using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C3 RID: 1219
	internal class FacetValues
	{
		// Token: 0x17000BCE RID: 3022
		// (set) Token: 0x06003C39 RID: 15417 RVA: 0x000C782D File Offset: 0x000C5A2D
		internal FacetValueContainer<bool?> Nullable
		{
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (set) Token: 0x06003C3A RID: 15418 RVA: 0x000C7836 File Offset: 0x000C5A36
		internal FacetValueContainer<int?> MaxLength
		{
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (set) Token: 0x06003C3B RID: 15419 RVA: 0x000C783F File Offset: 0x000C5A3F
		internal FacetValueContainer<bool?> Unicode
		{
			set
			{
				this._unicode = value;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (set) Token: 0x06003C3C RID: 15420 RVA: 0x000C7848 File Offset: 0x000C5A48
		internal FacetValueContainer<bool?> FixedLength
		{
			set
			{
				this._fixedLength = value;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (set) Token: 0x06003C3D RID: 15421 RVA: 0x000C7851 File Offset: 0x000C5A51
		internal FacetValueContainer<byte?> Precision
		{
			set
			{
				this._precision = value;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (set) Token: 0x06003C3E RID: 15422 RVA: 0x000C785A File Offset: 0x000C5A5A
		internal FacetValueContainer<byte?> Scale
		{
			set
			{
				this._scale = value;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (set) Token: 0x06003C3F RID: 15423 RVA: 0x000C7863 File Offset: 0x000C5A63
		internal object DefaultValue
		{
			set
			{
				this._defaultValue = value;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (set) Token: 0x06003C40 RID: 15424 RVA: 0x000C786C File Offset: 0x000C5A6C
		internal FacetValueContainer<string> Collation
		{
			set
			{
				this._collation = value;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (set) Token: 0x06003C41 RID: 15425 RVA: 0x000C7875 File Offset: 0x000C5A75
		internal FacetValueContainer<int?> Srid
		{
			set
			{
				this._srid = value;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (set) Token: 0x06003C42 RID: 15426 RVA: 0x000C787E File Offset: 0x000C5A7E
		internal FacetValueContainer<bool?> IsStrict
		{
			set
			{
				this._isStrict = value;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (set) Token: 0x06003C43 RID: 15427 RVA: 0x000C7887 File Offset: 0x000C5A87
		internal FacetValueContainer<StoreGeneratedPattern?> StoreGeneratedPattern
		{
			set
			{
				this._storeGeneratedPattern = value;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x000C7890 File Offset: 0x000C5A90
		internal FacetValueContainer<ConcurrencyMode?> ConcurrencyMode
		{
			set
			{
				this._concurrencyMode = value;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (set) Token: 0x06003C45 RID: 15429 RVA: 0x000C7899 File Offset: 0x000C5A99
		internal FacetValueContainer<CollectionKind?> CollectionKind
		{
			set
			{
				this._collectionKind = value;
			}
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x000C78A4 File Offset: 0x000C5AA4
		internal bool TryGetFacet(FacetDescription description, out Facet facet)
		{
			string facetName = description.FacetName;
			if (facetName != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(facetName);
				if (num <= 1564768107U)
				{
					if (num <= 1183764407U)
					{
						if (num != 676498961U)
						{
							if (num != 961465920U)
							{
								if (num == 1183764407U)
								{
									if (facetName == "Precision")
									{
										if (this._precision.HasValue)
										{
											facet = Facet.Create(description, this._precision.GetValueAsObject());
											return true;
										}
									}
								}
							}
							else if (facetName == "Unicode")
							{
								if (this._unicode.HasValue)
								{
									facet = Facet.Create(description, this._unicode.GetValueAsObject());
									return true;
								}
							}
						}
						else if (facetName == "Scale")
						{
							if (this._scale.HasValue)
							{
								facet = Facet.Create(description, this._scale.GetValueAsObject());
								return true;
							}
						}
					}
					else if (num != 1447003228U)
					{
						if (num != 1463310029U)
						{
							if (num == 1564768107U)
							{
								if (facetName == "ConcurrencyMode")
								{
									if (this._concurrencyMode.HasValue)
									{
										facet = Facet.Create(description, this._concurrencyMode.GetValueAsObject());
										return true;
									}
								}
							}
						}
						else if (facetName == "SRID")
						{
							if (this._srid.HasValue)
							{
								facet = Facet.Create(description, this._srid.GetValueAsObject());
								return true;
							}
						}
					}
					else if (facetName == "Nullable")
					{
						if (this._nullable.HasValue)
						{
							facet = Facet.Create(description, this._nullable.GetValueAsObject());
							return true;
						}
					}
				}
				else if (num <= 2958518788U)
				{
					if (num != 2638156065U)
					{
						if (num != 2732815151U)
						{
							if (num == 2958518788U)
							{
								if (facetName == "IsStrict")
								{
									if (this._isStrict.HasValue)
									{
										facet = Facet.Create(description, this._isStrict.GetValueAsObject());
										return true;
									}
								}
							}
						}
						else if (facetName == "StoreGeneratedPattern")
						{
							if (this._storeGeneratedPattern.HasValue)
							{
								facet = Facet.Create(description, this._storeGeneratedPattern.GetValueAsObject());
								return true;
							}
						}
					}
					else if (facetName == "MaxLength")
					{
						if (this._maxLength.HasValue)
						{
							facet = Facet.Create(description, this._maxLength.GetValueAsObject());
							return true;
						}
					}
				}
				else if (num <= 3829189484U)
				{
					if (num != 3439935391U)
					{
						if (num == 3829189484U)
						{
							if (facetName == "Collation")
							{
								if (this._collation.HasValue)
								{
									facet = Facet.Create(description, this._collation.GetValueAsObject());
									return true;
								}
							}
						}
					}
					else if (facetName == "DefaultValue")
					{
						if (this._defaultValue != null)
						{
							facet = Facet.Create(description, this._defaultValue);
							return true;
						}
					}
				}
				else if (num != 4242325569U)
				{
					if (num == 4293581999U)
					{
						if (facetName == "CollectionKind")
						{
							if (this._collectionKind.HasValue)
							{
								facet = Facet.Create(description, this._collectionKind.GetValueAsObject());
								return true;
							}
						}
					}
				}
				else if (facetName == "FixedLength")
				{
					if (this._fixedLength.HasValue)
					{
						facet = Facet.Create(description, this._fixedLength.GetValueAsObject());
						return true;
					}
				}
			}
			facet = null;
			return false;
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x000C7C68 File Offset: 0x000C5E68
		public static FacetValues Create(IEnumerable<Facet> facets)
		{
			FacetValues facetValues = new FacetValues();
			foreach (Facet facet in facets)
			{
				string facetName = facet.Description.FacetName;
				if (facetName != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(facetName);
					if (num <= 1564768107U)
					{
						if (num <= 1183764407U)
						{
							if (num != 676498961U)
							{
								if (num != 961465920U)
								{
									if (num == 1183764407U)
									{
										if (facetName == "Precision")
										{
											EdmConstants.Unbounded unbounded = facet.Value as EdmConstants.Unbounded;
											if (unbounded != null)
											{
												facetValues.Precision = unbounded;
											}
											else
											{
												facetValues.Precision = (byte?)facet.Value;
											}
										}
									}
								}
								else if (facetName == "Unicode")
								{
									facetValues.Unicode = (bool?)facet.Value;
								}
							}
							else if (facetName == "Scale")
							{
								EdmConstants.Unbounded unbounded2 = facet.Value as EdmConstants.Unbounded;
								if (unbounded2 != null)
								{
									facetValues.Scale = unbounded2;
								}
								else
								{
									facetValues.Scale = (byte?)facet.Value;
								}
							}
						}
						else if (num != 1447003228U)
						{
							if (num != 1463310029U)
							{
								if (num == 1564768107U)
								{
									if (facetName == "ConcurrencyMode")
									{
										facetValues.ConcurrencyMode = (ConcurrencyMode?)facet.Value;
									}
								}
							}
							else if (facetName == "SRID")
							{
								facetValues.Srid = (int?)facet.Value;
							}
						}
						else if (facetName == "Nullable")
						{
							facetValues.Nullable = (bool?)facet.Value;
						}
					}
					else if (num <= 2958518788U)
					{
						if (num != 2638156065U)
						{
							if (num != 2732815151U)
							{
								if (num == 2958518788U)
								{
									if (facetName == "IsStrict")
									{
										facetValues.IsStrict = (bool?)facet.Value;
									}
								}
							}
							else if (facetName == "StoreGeneratedPattern")
							{
								facetValues.StoreGeneratedPattern = (StoreGeneratedPattern?)facet.Value;
							}
						}
						else if (facetName == "MaxLength")
						{
							EdmConstants.Unbounded unbounded3 = facet.Value as EdmConstants.Unbounded;
							if (unbounded3 != null)
							{
								facetValues.MaxLength = unbounded3;
							}
							else
							{
								facetValues.MaxLength = (int?)facet.Value;
							}
						}
					}
					else if (num <= 3829189484U)
					{
						if (num != 3439935391U)
						{
							if (num == 3829189484U)
							{
								if (facetName == "Collation")
								{
									facetValues.Collation = (string)facet.Value;
								}
							}
						}
						else if (facetName == "DefaultValue")
						{
							facetValues.DefaultValue = facet.Value;
						}
					}
					else if (num != 4242325569U)
					{
						if (num == 4293581999U)
						{
							if (facetName == "CollectionKind")
							{
								facetValues.CollectionKind = (CollectionKind?)facet.Value;
							}
						}
					}
					else if (facetName == "FixedLength")
					{
						facetValues.FixedLength = (bool?)facet.Value;
					}
				}
			}
			return facetValues;
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x000C8078 File Offset: 0x000C6278
		internal static FacetValues NullFacetValues
		{
			get
			{
				return new FacetValues
				{
					FixedLength = null,
					MaxLength = null,
					Precision = null,
					Scale = null,
					Unicode = null,
					Collation = null,
					Srid = null,
					IsStrict = null,
					ConcurrencyMode = null,
					StoreGeneratedPattern = null,
					CollectionKind = null
				};
			}
		}

		// Token: 0x040014BB RID: 5307
		private FacetValueContainer<bool?> _nullable;

		// Token: 0x040014BC RID: 5308
		private FacetValueContainer<int?> _maxLength;

		// Token: 0x040014BD RID: 5309
		private FacetValueContainer<bool?> _unicode;

		// Token: 0x040014BE RID: 5310
		private FacetValueContainer<bool?> _fixedLength;

		// Token: 0x040014BF RID: 5311
		private FacetValueContainer<byte?> _precision;

		// Token: 0x040014C0 RID: 5312
		private FacetValueContainer<byte?> _scale;

		// Token: 0x040014C1 RID: 5313
		private object _defaultValue;

		// Token: 0x040014C2 RID: 5314
		private FacetValueContainer<string> _collation;

		// Token: 0x040014C3 RID: 5315
		private FacetValueContainer<int?> _srid;

		// Token: 0x040014C4 RID: 5316
		private FacetValueContainer<bool?> _isStrict;

		// Token: 0x040014C5 RID: 5317
		private FacetValueContainer<StoreGeneratedPattern?> _storeGeneratedPattern;

		// Token: 0x040014C6 RID: 5318
		private FacetValueContainer<ConcurrencyMode?> _concurrencyMode;

		// Token: 0x040014C7 RID: 5319
		private FacetValueContainer<CollectionKind?> _collectionKind;
	}
}
