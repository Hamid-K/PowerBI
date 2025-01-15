using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E4 RID: 484
	public abstract class ContainedObject : IContainedObject
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000262BB File Offset: 0x000244BB
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x000262C3 File Offset: 0x000244C3
		[XmlIgnore]
		public IContainedObject Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x0400056A RID: 1386
		private IContainedObject m_parent;
	}
}
