using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BB RID: 187
	public sealed class MiningModel : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x0002B5C4 File Offset: 0x000297C4
		internal MiningModel(DataRow miningModelRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId, MiningStructure parentObject)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningModelRow, parentObject, null, catalog, sessionId);
			this.populationTime = populationTime;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002B5F4 File Offset: 0x000297F4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0002B5FC File Offset: 0x000297FC
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.miningModelNameColumn).ToString();
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0002B613 File Offset: 0x00029813
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.descriptionColumn).ToString();
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0002B62A File Offset: 0x0002982A
		public string Algorithm
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.miningModelService).ToString();
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002B641 File Offset: 0x00029841
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.miningModelIsPopulated), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002B65D File Offset: 0x0002985D
		public DateTime LastUpdated
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.lastModifiedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002B679 File Offset: 0x00029879
		public DateTime LastProcessed
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.lastProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002B695 File Offset: 0x00029895
		public DateTime Created
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.createdColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0002B6B1 File Offset: 0x000298B1
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002B6B9 File Offset: 0x000298B9
		public MiningModelColumnCollection Columns
		{
			get
			{
				if (this.miningModelColumns == null)
				{
					this.miningModelColumns = new MiningModelColumnCollection(this.Connection, this);
				}
				else
				{
					this.miningModelColumns.CollectionInternal.CheckCache();
				}
				return this.miningModelColumns;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002B6ED File Offset: 0x000298ED
		public MiningContentNodeCollection Content
		{
			get
			{
				if (this.content == null)
				{
					this.content = new MiningContentNodeCollection(this.Connection, this);
				}
				else
				{
					this.content.CollectionInternal.CheckCache();
				}
				return this.content;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002B724 File Offset: 0x00029924
		public MiningContentNode GetNodeFromUniqueName(string nodeUniqueName)
		{
			MiningContentNodeCollection miningContentNodeCollection = new MiningContentNodeCollection(this.Connection, this, nodeUniqueName);
			MiningContentNode miningContentNode = null;
			if (miningContentNodeCollection.Count > 0)
			{
				miningContentNode = miningContentNodeCollection[0];
			}
			return miningContentNode;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0002B753 File Offset: 0x00029953
		public bool AllowDrillThrough
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.drillThroughEnabledColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002B76F File Offset: 0x0002996F
		public string Filter
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.caseFilterColumn).ToString();
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002B786 File Offset: 0x00029986
		public long TrainingSetSize
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.trainingSetSizeColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002B7A4 File Offset: 0x000299A4
		public MiningStructure Parent
		{
			get
			{
				if (this.baseData.ParentObject == null)
				{
					string text = AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.structureNameColumn).ToString();
					MiningStructure miningStructure = this.ParentConnection.MiningStructures[text];
					this.baseData.ParentObject = miningStructure;
				}
				return (MiningStructure)this.baseData.ParentObject;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002B802 File Offset: 0x00029A02
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertyCollection == null)
				{
					this.propertyCollection = new PropertyCollection(this.MiningModelRow, this);
				}
				return this.propertyCollection;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002B824 File Offset: 0x00029A24
		public MiningParameterCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new MiningParameterCollection(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.miningModelParameters).ToString());
				}
				return this.parameters;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002B854 File Offset: 0x00029A54
		public MiningAttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new MiningAttributeCollection(this);
				}
				return this.attributes;
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002B870 File Offset: 0x00029A70
		public MiningAttributeCollection GetAttributes(MiningFeatureSelection filter)
		{
			if (filter == MiningFeatureSelection.All)
			{
				return this.Attributes;
			}
			return new MiningAttributeCollection(this, filter);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002B883 File Offset: 0x00029A83
		public MiningAttribute FindAttribute(int attributeId)
		{
			return this.Attributes[attributeId];
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002B891 File Offset: 0x00029A91
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002B89E File Offset: 0x00029A9E
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0002B8AB File Offset: 0x00029AAB
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

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002B8B9 File Offset: 0x00029AB9
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002B8C6 File Offset: 0x00029AC6
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

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002B8D4 File Offset: 0x00029AD4
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x0002B8D7 File Offset: 0x00029AD7
		IAdomdBaseObject IAdomdBaseObject.ParentObject
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002B8D9 File Offset: 0x00029AD9
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002B8E1 File Offset: 0x00029AE1
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002B8EC File Offset: 0x00029AEC
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002B8F4 File Offset: 0x00029AF4
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002B901 File Offset: 0x00029B01
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002B90E File Offset: 0x00029B0E
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002B916 File Offset: 0x00029B16
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002B91E File Offset: 0x00029B1E
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningModel);
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002B92A File Offset: 0x00029B2A
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002B94D File Offset: 0x00029B4D
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002B95B File Offset: 0x00029B5B
		public static bool operator ==(MiningModel o1, MiningModel o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002B964 File Offset: 0x00029B64
		public static bool operator !=(MiningModel o1, MiningModel o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002B970 File Offset: 0x00029B70
		internal DataRow MiningModelRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0002B982 File Offset: 0x00029B82
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002B98A File Offset: 0x00029B8A
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x040006FB RID: 1787
		private BaseObjectData baseData;

		// Token: 0x040006FC RID: 1788
		private DateTime populationTime;

		// Token: 0x040006FD RID: 1789
		private MiningModelColumnCollection miningModelColumns;

		// Token: 0x040006FE RID: 1790
		private MiningContentNodeCollection content;

		// Token: 0x040006FF RID: 1791
		private MiningParameterCollection parameters;

		// Token: 0x04000700 RID: 1792
		private PropertyCollection propertyCollection;

		// Token: 0x04000701 RID: 1793
		private MiningAttributeCollection attributes;

		// Token: 0x04000702 RID: 1794
		private int hashCode;

		// Token: 0x04000703 RID: 1795
		private bool hashCodeCalculated;

		// Token: 0x04000704 RID: 1796
		internal static string miningModelNameColumn = "MODEL_NAME";

		// Token: 0x04000705 RID: 1797
		internal static string miningModelNameRest = MiningModel.miningModelNameColumn;

		// Token: 0x04000706 RID: 1798
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000707 RID: 1799
		internal static string lastModifiedColumn = "DATE_MODIFIED";

		// Token: 0x04000708 RID: 1800
		internal static string lastProcessedColumn = "LAST_PROCESSED";

		// Token: 0x04000709 RID: 1801
		internal static string miningModelParameters = "MINING_PARAMETERS";

		// Token: 0x0400070A RID: 1802
		internal static string miningModelService = "SERVICE_NAME";

		// Token: 0x0400070B RID: 1803
		internal static string miningModelIsPopulated = "IS_POPULATED";

		// Token: 0x0400070C RID: 1804
		internal static string createdColumn = "DATE_CREATED";

		// Token: 0x0400070D RID: 1805
		internal static string drillThroughEnabledColumn = "MSOLAP_IS_DRILLTHROUGH_ENABLED";

		// Token: 0x0400070E RID: 1806
		internal static string caseFilterColumn = "FILTER";

		// Token: 0x0400070F RID: 1807
		internal static string trainingSetSizeColumn = "TRAINING_SET_SIZE";

		// Token: 0x04000710 RID: 1808
		internal static string structureNameColumn = "MINING_STRUCTURE";
	}
}
