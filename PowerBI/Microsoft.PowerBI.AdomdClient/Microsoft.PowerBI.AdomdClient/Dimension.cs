using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000082 RID: 130
	public sealed class Dimension : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x000269CC File Offset: 0x00024BCC
		internal Dimension(AdomdConnection connection, DataRow dimensionRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, dimensionRow, parentCube, null, catalog, sessionId);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000269F4 File Offset: 0x00024BF4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000269FC File Offset: 0x00024BFC
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.dimensionNameColumn).ToString();
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00026A13 File Offset: 0x00024C13
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00026A1B File Offset: 0x00024C1B
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00026A32 File Offset: 0x00024C32
		public CubeDef ParentCube
		{
			get
			{
				return (CubeDef)this.baseData.ParentObject;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00026A44 File Offset: 0x00024C44
		public DimensionTypeEnum DimensionType
		{
			get
			{
				long num = (long)Convert.ToInt32(AdomdUtils.GetProperty(this.DimensionRow, Dimension.typeColumn), CultureInfo.InvariantCulture);
				if (num >= 0L && num <= 17L)
				{
					return (DimensionTypeEnum)num;
				}
				return DimensionTypeEnum.Other;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00026A7C File Offset: 0x00024C7C
		public bool WriteEnabled
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.DimensionRow, Dimension.writeEnabledColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00026A98 File Offset: 0x00024C98
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.captionColumn).ToString();
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00026AAF File Offset: 0x00024CAF
		public HierarchyCollection Hierarchies
		{
			get
			{
				if (this.hierarchies == null)
				{
					this.hierarchies = new HierarchyCollection(this.baseData.Connection, this, false);
				}
				else
				{
					this.hierarchies.CollectionInternal.CheckCache();
				}
				return this.hierarchies;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00026AEC File Offset: 0x00024CEC
		public HierarchyCollection AttributeHierarchies
		{
			get
			{
				if (!this.baseData.Connection.IsPostYukonProvider())
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				if (this.attribHierarchies == null)
				{
					this.attribHierarchies = new HierarchyCollection(this.baseData.Connection, this, true);
				}
				return this.attribHierarchies;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00026B3C File Offset: 0x00024D3C
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.DimensionRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00026B5E File Offset: 0x00024D5E
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00026B6B File Offset: 0x00024D6B
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00026B78 File Offset: 0x00024D78
		bool IAdomdBaseObject.IsMetadata
		{
			get
			{
				return this.baseData.IsMetadata;
			}
			set
			{
				this.baseData.IsMetadata = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00026B86 File Offset: 0x00024D86
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00026B93 File Offset: 0x00024D93
		object IAdomdBaseObject.MetadataData
		{
			get
			{
				return this.baseData.MetadataData;
			}
			set
			{
				this.baseData.MetadataData = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00026BA1 File Offset: 0x00024DA1
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00026BAE File Offset: 0x00024DAE
		IAdomdBaseObject IAdomdBaseObject.ParentObject
		{
			get
			{
				return this.baseData.ParentObject;
			}
			set
			{
				this.baseData.ParentObject = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00026BBC File Offset: 0x00024DBC
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return ((CubeDef)this.baseData.ParentObject).Name;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00026BD3 File Offset: 0x00024DD3
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00026BD6 File Offset: 0x00024DD6
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00026BED File Offset: 0x00024DED
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00026BFA File Offset: 0x00024DFA
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00026C07 File Offset: 0x00024E07
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00026C14 File Offset: 0x00024E14
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00026C1C File Offset: 0x00024E1C
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00026C24 File Offset: 0x00024E24
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Dimension);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00026C30 File Offset: 0x00024E30
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00026C53 File Offset: 0x00024E53
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00026C61 File Offset: 0x00024E61
		public static bool operator ==(Dimension o1, Dimension o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00026C6A File Offset: 0x00024E6A
		public static bool operator !=(Dimension o1, Dimension o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00026C76 File Offset: 0x00024E76
		internal DataRow DimensionRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x04000573 RID: 1395
		private BaseObjectData baseData;

		// Token: 0x04000574 RID: 1396
		internal HierarchyCollection hierarchies;

		// Token: 0x04000575 RID: 1397
		private HierarchyCollection attribHierarchies;

		// Token: 0x04000576 RID: 1398
		private PropertyCollection propertiesCollection;

		// Token: 0x04000577 RID: 1399
		private int hashCode;

		// Token: 0x04000578 RID: 1400
		private bool hashCodeCalculated;

		// Token: 0x04000579 RID: 1401
		internal static string dimensionNameColumn = "DIMENSION_NAME";

		// Token: 0x0400057A RID: 1402
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x0400057B RID: 1403
		internal static string uniqueNameColumn = "DIMENSION_UNIQUE_NAME";

		// Token: 0x0400057C RID: 1404
		private static string typeColumn = "DIMENSION_TYPE";

		// Token: 0x0400057D RID: 1405
		private static string writeEnabledColumn = "IS_READWRITE";

		// Token: 0x0400057E RID: 1406
		private static string captionColumn = "DIMENSION_CAPTION";
	}
}
