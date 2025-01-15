using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BB RID: 187
	public sealed class MiningModel : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x0002B294 File Offset: 0x00029494
		internal MiningModel(DataRow miningModelRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId, MiningStructure parentObject)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningModelRow, parentObject, null, catalog, sessionId);
			this.populationTime = populationTime;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002B2C4 File Offset: 0x000294C4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002B2CC File Offset: 0x000294CC
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.miningModelNameColumn).ToString();
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0002B2E3 File Offset: 0x000294E3
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0002B2FA File Offset: 0x000294FA
		public string Algorithm
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.miningModelService).ToString();
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0002B311 File Offset: 0x00029511
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.miningModelIsPopulated), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0002B32D File Offset: 0x0002952D
		public DateTime LastUpdated
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.lastModifiedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0002B349 File Offset: 0x00029549
		public DateTime LastProcessed
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.lastProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0002B365 File Offset: 0x00029565
		public DateTime Created
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.createdColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0002B381 File Offset: 0x00029581
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002B389 File Offset: 0x00029589
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002B3BD File Offset: 0x000295BD
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

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002B3F4 File Offset: 0x000295F4
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

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0002B423 File Offset: 0x00029623
		public bool AllowDrillThrough
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.drillThroughEnabledColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002B43F File Offset: 0x0002963F
		public string Filter
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.caseFilterColumn).ToString();
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0002B456 File Offset: 0x00029656
		public long TrainingSetSize
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningModelRow, MiningModel.trainingSetSizeColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0002B474 File Offset: 0x00029674
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

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0002B4D2 File Offset: 0x000296D2
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

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002B4F4 File Offset: 0x000296F4
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002B524 File Offset: 0x00029724
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

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002B540 File Offset: 0x00029740
		public MiningAttributeCollection GetAttributes(MiningFeatureSelection filter)
		{
			if (filter == MiningFeatureSelection.All)
			{
				return this.Attributes;
			}
			return new MiningAttributeCollection(this, filter);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002B553 File Offset: 0x00029753
		public MiningAttribute FindAttribute(int attributeId)
		{
			return this.Attributes[attributeId];
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0002B561 File Offset: 0x00029761
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002B56E File Offset: 0x0002976E
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0002B57B File Offset: 0x0002977B
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

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002B589 File Offset: 0x00029789
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0002B596 File Offset: 0x00029796
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

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002B5A4 File Offset: 0x000297A4
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0002B5A7 File Offset: 0x000297A7
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

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002B5A9 File Offset: 0x000297A9
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002B5B1 File Offset: 0x000297B1
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002B5B4 File Offset: 0x000297B4
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002B5BC File Offset: 0x000297BC
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002B5C4 File Offset: 0x000297C4
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0002B5D1 File Offset: 0x000297D1
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002B5DE File Offset: 0x000297DE
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002B5E6 File Offset: 0x000297E6
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002B5EE File Offset: 0x000297EE
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningModel);
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002B5FA File Offset: 0x000297FA
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002B61D File Offset: 0x0002981D
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002B62B File Offset: 0x0002982B
		public static bool operator ==(MiningModel o1, MiningModel o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002B634 File Offset: 0x00029834
		public static bool operator !=(MiningModel o1, MiningModel o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002B640 File Offset: 0x00029840
		internal DataRow MiningModelRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002B652 File Offset: 0x00029852
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002B65A File Offset: 0x0002985A
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x040006EE RID: 1774
		private BaseObjectData baseData;

		// Token: 0x040006EF RID: 1775
		private DateTime populationTime;

		// Token: 0x040006F0 RID: 1776
		private MiningModelColumnCollection miningModelColumns;

		// Token: 0x040006F1 RID: 1777
		private MiningContentNodeCollection content;

		// Token: 0x040006F2 RID: 1778
		private MiningParameterCollection parameters;

		// Token: 0x040006F3 RID: 1779
		private PropertyCollection propertyCollection;

		// Token: 0x040006F4 RID: 1780
		private MiningAttributeCollection attributes;

		// Token: 0x040006F5 RID: 1781
		private int hashCode;

		// Token: 0x040006F6 RID: 1782
		private bool hashCodeCalculated;

		// Token: 0x040006F7 RID: 1783
		internal static string miningModelNameColumn = "MODEL_NAME";

		// Token: 0x040006F8 RID: 1784
		internal static string miningModelNameRest = MiningModel.miningModelNameColumn;

		// Token: 0x040006F9 RID: 1785
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040006FA RID: 1786
		internal static string lastModifiedColumn = "DATE_MODIFIED";

		// Token: 0x040006FB RID: 1787
		internal static string lastProcessedColumn = "LAST_PROCESSED";

		// Token: 0x040006FC RID: 1788
		internal static string miningModelParameters = "MINING_PARAMETERS";

		// Token: 0x040006FD RID: 1789
		internal static string miningModelService = "SERVICE_NAME";

		// Token: 0x040006FE RID: 1790
		internal static string miningModelIsPopulated = "IS_POPULATED";

		// Token: 0x040006FF RID: 1791
		internal static string createdColumn = "DATE_CREATED";

		// Token: 0x04000700 RID: 1792
		internal static string drillThroughEnabledColumn = "MSOLAP_IS_DRILLTHROUGH_ENABLED";

		// Token: 0x04000701 RID: 1793
		internal static string caseFilterColumn = "FILTER";

		// Token: 0x04000702 RID: 1794
		internal static string trainingSetSizeColumn = "TRAINING_SET_SIZE";

		// Token: 0x04000703 RID: 1795
		internal static string structureNameColumn = "MINING_STRUCTURE";
	}
}
