using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000137 RID: 311
	internal sealed class UserRights
	{
		// Token: 0x06000C52 RID: 3154 RVA: 0x0002E3B3 File Offset: 0x0002C5B3
		internal UserRights(byte[] securityDescriptor, string xmlDescriptor, ExternalItemPath itemPath)
		{
			this.m_binaryDescriptor = securityDescriptor;
			this.m_xmlDescriptor = xmlDescriptor;
			this.m_AccessGranted = false;
			this.m_AccessChecked = false;
			this.m_modelItemPath = itemPath;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002E3DE File Offset: 0x0002C5DE
		internal UserRights()
		{
			this.m_binaryDescriptor = null;
			this.m_xmlDescriptor = null;
			this.m_AccessChecked = true;
			this.m_AccessGranted = true;
			this.m_modelItemPath = ExternalItemPath.Empty;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0002E40D File Offset: 0x0002C60D
		internal string XmlDescriptor
		{
			get
			{
				return this.m_xmlDescriptor;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0002E415 File Offset: 0x0002C615
		internal byte[] BinaryDescriptor
		{
			get
			{
				return this.m_binaryDescriptor;
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002E41D File Offset: 0x0002C61D
		internal bool CheckAccess(RSService service)
		{
			if (!this.m_AccessChecked)
			{
				this.m_AccessGranted = service.SecMgr.CheckAccess(this.m_binaryDescriptor, this.m_modelItemPath, ModelItemOperation.ReadProperties);
				this.m_AccessChecked = true;
			}
			return this.m_AccessGranted;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002E452 File Offset: 0x0002C652
		internal ExternalItemPath ModelItemPath
		{
			get
			{
				return this.m_modelItemPath;
			}
		}

		// Token: 0x04000506 RID: 1286
		private string m_xmlDescriptor;

		// Token: 0x04000507 RID: 1287
		private byte[] m_binaryDescriptor;

		// Token: 0x04000508 RID: 1288
		private bool m_AccessGranted;

		// Token: 0x04000509 RID: 1289
		private bool m_AccessChecked;

		// Token: 0x0400050A RID: 1290
		private ExternalItemPath m_modelItemPath;
	}
}
