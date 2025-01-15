using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000CB RID: 203
	public sealed class MiningServiceParameter : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000B51 RID: 2897 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
		internal MiningServiceParameter(AdomdConnection connection, DataRow miningServiceParameterRow, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningServiceParameterRow, parentObject, null, catalog, sessionId);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002CCCC File Offset: 0x0002AECC
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0002CCD4 File Offset: 0x0002AED4
		public string ServiceName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.serviceNameColumn).ToString();
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002CCEB File Offset: 0x0002AEEB
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.serviceParameterColumn).ToString();
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002CD02 File Offset: 0x0002AF02
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.descriptionColumn).ToString();
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002CD19 File Offset: 0x0002AF19
		public bool IsRequired
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.isRequiredColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0002CD35 File Offset: 0x0002AF35
		public string DefaultValue
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.defaultValueColumn).ToString();
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0002CD4C File Offset: 0x0002AF4C
		public string ValueEnumeration
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.valueEnumerationColumn).ToString();
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0002CD63 File Offset: 0x0002AF63
		public string ParameterType
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.parameterTypeColumn).ToString();
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0002CD7A File Offset: 0x0002AF7A
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.MiningServiceParameterRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0002CD9C File Offset: 0x0002AF9C
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0002CDA9 File Offset: 0x0002AFA9
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0002CDB6 File Offset: 0x0002AFB6
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0002CDD1 File Offset: 0x0002AFD1
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

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002CDDF File Offset: 0x0002AFDF
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0002CDEC File Offset: 0x0002AFEC
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002CDFA File Offset: 0x0002AFFA
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.baseData.ParentObject.CubeName;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002CE0C File Offset: 0x0002B00C
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeMiningServiceParameter;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002CE10 File Offset: 0x0002B010
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.serviceParameterColumn).ToString();
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002CE27 File Offset: 0x0002B027
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002CE34 File Offset: 0x0002B034
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002CE41 File Offset: 0x0002B041
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0002CE4E File Offset: 0x0002B04E
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002CE56 File Offset: 0x0002B056
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002CE5E File Offset: 0x0002B05E
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningServiceParameter);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002CE6A File Offset: 0x0002B06A
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002CE8D File Offset: 0x0002B08D
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002CE9B File Offset: 0x0002B09B
		public static bool operator ==(MiningServiceParameter o1, MiningServiceParameter o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002CEA4 File Offset: 0x0002B0A4
		public static bool operator !=(MiningServiceParameter o1, MiningServiceParameter o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
		internal DataRow MiningServiceParameterRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x04000774 RID: 1908
		private BaseObjectData baseData;

		// Token: 0x04000775 RID: 1909
		private PropertyCollection propertiesCollection;

		// Token: 0x04000776 RID: 1910
		private int hashCode;

		// Token: 0x04000777 RID: 1911
		private bool hashCodeCalculated;

		// Token: 0x04000778 RID: 1912
		internal static string serviceNameColumn = "SERVICE_NAME";

		// Token: 0x04000779 RID: 1913
		internal static string serviceParameterColumn = "PARAMETER_NAME";

		// Token: 0x0400077A RID: 1914
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x0400077B RID: 1915
		private static string isRequiredColumn = "IS_REQUIRED";

		// Token: 0x0400077C RID: 1916
		private static string defaultValueColumn = "DEFAULT_VALUE";

		// Token: 0x0400077D RID: 1917
		private static string valueEnumerationColumn = "VALUE_ENUMERATION";

		// Token: 0x0400077E RID: 1918
		private static string parameterTypeColumn = "PARAMETER_TYPE";
	}
}
