using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000022 RID: 34
	[XmlElementClass("Field")]
	internal class FieldEx : Field
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002E61 File Offset: 0x00001061
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002E69 File Offset: 0x00001069
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public new string TypeName
		{
			get
			{
				return this.m_typeName;
			}
			set
			{
				this.m_typeName = value;
			}
		}

		// Token: 0x0400002C RID: 44
		private string m_typeName;
	}
}
