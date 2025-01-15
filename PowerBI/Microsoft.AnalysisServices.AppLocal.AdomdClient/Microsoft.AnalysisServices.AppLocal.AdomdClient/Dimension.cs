using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000082 RID: 130
	public sealed class Dimension : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x00026CFC File Offset: 0x00024EFC
		internal Dimension(AdomdConnection connection, DataRow dimensionRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, dimensionRow, parentCube, null, catalog, sessionId);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00026D24 File Offset: 0x00024F24
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00026D2C File Offset: 0x00024F2C
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.dimensionNameColumn).ToString();
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00026D43 File Offset: 0x00024F43
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00026D4B File Offset: 0x00024F4B
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00026D62 File Offset: 0x00024F62
		public CubeDef ParentCube
		{
			get
			{
				return (CubeDef)this.baseData.ParentObject;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00026D74 File Offset: 0x00024F74
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00026DAC File Offset: 0x00024FAC
		public bool WriteEnabled
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.DimensionRow, Dimension.writeEnabledColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00026DC8 File Offset: 0x00024FC8
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.captionColumn).ToString();
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00026DDF File Offset: 0x00024FDF
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00026E1C File Offset: 0x0002501C
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00026E6C File Offset: 0x0002506C
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00026E8E File Offset: 0x0002508E
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00026E9B File Offset: 0x0002509B
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00026EA8 File Offset: 0x000250A8
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00026EB6 File Offset: 0x000250B6
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00026EC3 File Offset: 0x000250C3
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00026ED1 File Offset: 0x000250D1
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00026EDE File Offset: 0x000250DE
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00026EEC File Offset: 0x000250EC
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return ((CubeDef)this.baseData.ParentObject).Name;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00026F03 File Offset: 0x00025103
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00026F06 File Offset: 0x00025106
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.DimensionRow, Dimension.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00026F1D File Offset: 0x0002511D
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00026F2A File Offset: 0x0002512A
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00026F37 File Offset: 0x00025137
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00026F44 File Offset: 0x00025144
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00026F4C File Offset: 0x0002514C
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00026F54 File Offset: 0x00025154
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Dimension);
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00026F60 File Offset: 0x00025160
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00026F83 File Offset: 0x00025183
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00026F91 File Offset: 0x00025191
		public static bool operator ==(Dimension o1, Dimension o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00026F9A File Offset: 0x0002519A
		public static bool operator !=(Dimension o1, Dimension o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00026FA6 File Offset: 0x000251A6
		internal DataRow DimensionRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x04000580 RID: 1408
		private BaseObjectData baseData;

		// Token: 0x04000581 RID: 1409
		internal HierarchyCollection hierarchies;

		// Token: 0x04000582 RID: 1410
		private HierarchyCollection attribHierarchies;

		// Token: 0x04000583 RID: 1411
		private PropertyCollection propertiesCollection;

		// Token: 0x04000584 RID: 1412
		private int hashCode;

		// Token: 0x04000585 RID: 1413
		private bool hashCodeCalculated;

		// Token: 0x04000586 RID: 1414
		internal static string dimensionNameColumn = "DIMENSION_NAME";

		// Token: 0x04000587 RID: 1415
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000588 RID: 1416
		internal static string uniqueNameColumn = "DIMENSION_UNIQUE_NAME";

		// Token: 0x04000589 RID: 1417
		private static string typeColumn = "DIMENSION_TYPE";

		// Token: 0x0400058A RID: 1418
		private static string writeEnabledColumn = "IS_READWRITE";

		// Token: 0x0400058B RID: 1419
		private static string captionColumn = "DIMENSION_CAPTION";
	}
}
