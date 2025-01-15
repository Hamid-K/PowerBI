using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000034 RID: 52
	internal class XEventInteropObject : IXEObjectMetadata
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000E380 File Offset: 0x0000E380
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000E39C File Offset: 0x0000E39C
		public virtual IPackage Package
		{
			get
			{
				return this.m_package;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000E3B8 File Offset: 0x0000E3B8
		public virtual string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000E3D4 File Offset: 0x0000E3D4
		protected unsafe XEventInteropObject(char* name, char* description, XEventInteropMetadataManager md, IPackage package)
		{
			this.m_metadata = md;
			IntPtr intPtr = (IntPtr)((void*)name);
			this.m_name = Marshal.PtrToStringUni(intPtr);
			IntPtr intPtr2 = (IntPtr)((void*)description);
			this.m_description = Marshal.PtrToStringUni(intPtr2);
			this.m_package = package;
		}

		// Token: 0x040001BD RID: 445
		private string m_name;

		// Token: 0x040001BE RID: 446
		private string m_description;

		// Token: 0x040001BF RID: 447
		private XEventInteropMetadataManager m_metadata;

		// Token: 0x040001C0 RID: 448
		private IPackage m_package;
	}
}
