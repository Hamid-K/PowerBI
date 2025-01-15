using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200016A RID: 362
	[XmlRoot("ActivityTypes", Namespace = "")]
	[Serializable]
	public class ActivityTypeRecordCollection : List<ActivityTypeRecord>
	{
	}
}
