using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200208B RID: 8331
	public class QueryPermissionXml : QueryPermission
	{
		// Token: 0x0600CBDE RID: 52190 RVA: 0x0028866B File Offset: 0x0028686B
		public QueryPermissionXml()
		{
		}

		// Token: 0x0600CBDF RID: 52191 RVA: 0x0028961F File Offset: 0x0028781F
		public QueryPermissionXml(Resource resource, QueryPermissionChallengeType type, string queryPermissionData)
			: base(resource, type, queryPermissionData)
		{
		}

		// Token: 0x17003116 RID: 12566
		// (get) Token: 0x0600CBE0 RID: 52192 RVA: 0x0028868D File Offset: 0x0028688D
		// (set) Token: 0x0600CBE1 RID: 52193 RVA: 0x00288695 File Offset: 0x00286895
		[XmlAttribute("QueryPermission")]
		public override string QueryPermissionData
		{
			get
			{
				return this.queryPermissionData;
			}
			set
			{
				this.queryPermissionData = value;
			}
		}
	}
}
