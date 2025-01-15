using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000092 RID: 146
	internal class FacetValues
	{
		// Token: 0x170003C4 RID: 964
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00018BA1 File Offset: 0x00016DA1
		internal FacetValueContainer<bool?> Nullable
		{
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00018BAA File Offset: 0x00016DAA
		internal FacetValueContainer<int?> MaxLength
		{
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00018BB3 File Offset: 0x00016DB3
		internal FacetValueContainer<bool?> Unicode
		{
			set
			{
				this._unicode = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00018BBC File Offset: 0x00016DBC
		internal FacetValueContainer<bool?> FixedLength
		{
			set
			{
				this._fixedLength = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00018BC5 File Offset: 0x00016DC5
		internal FacetValueContainer<byte?> Precision
		{
			set
			{
				this._precision = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x00018BCE File Offset: 0x00016DCE
		internal FacetValueContainer<byte?> Scale
		{
			set
			{
				this._scale = value;
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00018BD8 File Offset: 0x00016DD8
		internal bool TryGetFacet(FacetDescription description, out Facet facet)
		{
			if (description.FacetName == "Nullable")
			{
				if (this._nullable.HasValue)
				{
					facet = Facet.Create(description, this._nullable.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "MaxLength")
			{
				if (this._maxLength.HasValue)
				{
					facet = Facet.Create(description, this._maxLength.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "Unicode")
			{
				if (this._unicode.HasValue)
				{
					facet = Facet.Create(description, this._unicode.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "FixedLength")
			{
				if (this._fixedLength.HasValue)
				{
					facet = Facet.Create(description, this._fixedLength.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "Precision")
			{
				if (this._precision.HasValue)
				{
					facet = Facet.Create(description, this._precision.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "Scale" && this._scale.HasValue)
			{
				facet = Facet.Create(description, this._scale.GetValueAsObject());
				return true;
			}
			facet = null;
			return false;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00018D2C File Offset: 0x00016F2C
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
					Unicode = null
				};
			}
		}

		// Token: 0x04000843 RID: 2115
		private FacetValueContainer<bool?> _nullable;

		// Token: 0x04000844 RID: 2116
		private FacetValueContainer<int?> _maxLength;

		// Token: 0x04000845 RID: 2117
		private FacetValueContainer<bool?> _unicode;

		// Token: 0x04000846 RID: 2118
		private FacetValueContainer<bool?> _fixedLength;

		// Token: 0x04000847 RID: 2119
		private FacetValueContainer<byte?> _precision;

		// Token: 0x04000848 RID: 2120
		private FacetValueContainer<byte?> _scale;
	}
}
