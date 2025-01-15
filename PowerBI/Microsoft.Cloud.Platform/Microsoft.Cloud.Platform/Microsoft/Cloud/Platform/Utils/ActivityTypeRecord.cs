using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200016B RID: 363
	[XmlType("ActivityType")]
	[Serializable]
	public struct ActivityTypeRecord
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x000208CD File Offset: 0x0001EACD
		public ActivityTypeRecord(string activityShortName, string activityClassName)
		{
			this = new ActivityTypeRecord(activityShortName, activityClassName, string.Empty, activityClassName, activityClassName, string.Empty);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000208E3 File Offset: 0x0001EAE3
		public ActivityTypeRecord(string activityShortName, string activityFullName, string activityAssembly, string activityClassName, string activityDescription, string activityComponentTeam)
		{
			this.m_activityShortName = activityShortName;
			this.m_activityFullName = activityFullName;
			this.m_activityAssemblyName = activityAssembly;
			this.m_activityClassName = activityClassName;
			this.m_activityDescription = activityDescription;
			this.m_activityComponentTeam = activityComponentTeam;
			this.ValidateParameters(activityShortName, activityFullName, activityAssembly, activityClassName, activityDescription, activityComponentTeam);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00020921 File Offset: 0x0001EB21
		private void ValidateParameters(string activityShortName, string activityFullName, string activityAssembly, string activityClassName, string activityDescription, string activityTeam)
		{
			if (activityShortName == null || activityShortName.Length != 4)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "ActivityShortName:{0}\nActivity short name is null or not 4 characters based.", new object[] { activityShortName }));
			}
		}

		// Token: 0x04000398 RID: 920
		[XmlAttribute(AttributeName = "ShortName")]
		public string m_activityShortName;

		// Token: 0x04000399 RID: 921
		[XmlAttribute(AttributeName = "FullName")]
		public string m_activityFullName;

		// Token: 0x0400039A RID: 922
		[XmlAttribute(AttributeName = "Assembly")]
		public string m_activityAssemblyName;

		// Token: 0x0400039B RID: 923
		[XmlAttribute(AttributeName = "ClassName")]
		public string m_activityClassName;

		// Token: 0x0400039C RID: 924
		[XmlAttribute(AttributeName = "Description")]
		public string m_activityDescription;

		// Token: 0x0400039D RID: 925
		[XmlAttribute(AttributeName = "ComponentTeam")]
		public string m_activityComponentTeam;
	}
}
