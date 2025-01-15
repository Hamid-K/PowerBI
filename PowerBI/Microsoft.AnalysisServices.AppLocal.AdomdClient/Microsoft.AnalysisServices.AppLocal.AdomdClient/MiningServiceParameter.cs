using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000CB RID: 203
	public sealed class MiningServiceParameter : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
		internal MiningServiceParameter(AdomdConnection connection, DataRow miningServiceParameterRow, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningServiceParameterRow, parentObject, null, catalog, sessionId);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002CFFC File Offset: 0x0002B1FC
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002D004 File Offset: 0x0002B204
		public string ServiceName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.serviceNameColumn).ToString();
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0002D01B File Offset: 0x0002B21B
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.serviceParameterColumn).ToString();
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002D032 File Offset: 0x0002B232
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002D049 File Offset: 0x0002B249
		public bool IsRequired
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.isRequiredColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002D065 File Offset: 0x0002B265
		public string DefaultValue
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.defaultValueColumn).ToString();
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002D07C File Offset: 0x0002B27C
		public string ValueEnumeration
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.valueEnumerationColumn).ToString();
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002D093 File Offset: 0x0002B293
		public string ParameterType
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.parameterTypeColumn).ToString();
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002D0AA File Offset: 0x0002B2AA
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0002D0CC File Offset: 0x0002B2CC
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002D0D9 File Offset: 0x0002B2D9
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x0002D0E6 File Offset: 0x0002B2E6
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

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0002D101 File Offset: 0x0002B301
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

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002D10F File Offset: 0x0002B30F
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x0002D11C File Offset: 0x0002B31C
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

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002D12A File Offset: 0x0002B32A
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.baseData.ParentObject.CubeName;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002D13C File Offset: 0x0002B33C
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeMiningServiceParameter;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002D140 File Offset: 0x0002B340
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceParameterRow, MiningServiceParameter.serviceParameterColumn).ToString();
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002D157 File Offset: 0x0002B357
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002D164 File Offset: 0x0002B364
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002D171 File Offset: 0x0002B371
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002D17E File Offset: 0x0002B37E
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002D186 File Offset: 0x0002B386
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002D18E File Offset: 0x0002B38E
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningServiceParameter);
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002D19A File Offset: 0x0002B39A
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D1BD File Offset: 0x0002B3BD
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D1CB File Offset: 0x0002B3CB
		public static bool operator ==(MiningServiceParameter o1, MiningServiceParameter o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		public static bool operator !=(MiningServiceParameter o1, MiningServiceParameter o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002D1E0 File Offset: 0x0002B3E0
		internal DataRow MiningServiceParameterRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x04000781 RID: 1921
		private BaseObjectData baseData;

		// Token: 0x04000782 RID: 1922
		private PropertyCollection propertiesCollection;

		// Token: 0x04000783 RID: 1923
		private int hashCode;

		// Token: 0x04000784 RID: 1924
		private bool hashCodeCalculated;

		// Token: 0x04000785 RID: 1925
		internal static string serviceNameColumn = "SERVICE_NAME";

		// Token: 0x04000786 RID: 1926
		internal static string serviceParameterColumn = "PARAMETER_NAME";

		// Token: 0x04000787 RID: 1927
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000788 RID: 1928
		private static string isRequiredColumn = "IS_REQUIRED";

		// Token: 0x04000789 RID: 1929
		private static string defaultValueColumn = "DEFAULT_VALUE";

		// Token: 0x0400078A RID: 1930
		private static string valueEnumerationColumn = "VALUE_ENUMERATION";

		// Token: 0x0400078B RID: 1931
		private static string parameterTypeColumn = "PARAMETER_TYPE";
	}
}
