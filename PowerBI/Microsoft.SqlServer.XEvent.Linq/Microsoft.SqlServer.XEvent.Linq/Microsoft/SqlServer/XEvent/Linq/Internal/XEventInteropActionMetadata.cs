using System;
using Microsoft.SqlServer.XEvent.TypeSystem;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000037 RID: 55
	internal class XEventInteropActionMetadata : XEventInteropObject, IActionMetadata
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000E708 File Offset: 0x0000E708
		public virtual Type Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000E724 File Offset: 0x0000E724
		internal unsafe XEventInteropActionMetadata(XEAction* actionObj, XEventInteropMetadataManager md, IPackage package)
			: base(*(long*)(actionObj + 8L / (long)sizeof(XEAction)), *(long*)(actionObj + 16L / (long)sizeof(XEAction)), md, package)
		{
			this.m_action = actionObj;
			XEAction* ptr = actionObj + 32L / (long)sizeof(XEAction);
			Type type = XETypeMappings.ManagedTypeFromXEType((uint)(*(int*)ptr));
			this.m_type = type;
			if (type == null && <Module>.XE_Compare(*(XERelativeObjectId*)ptr, <Module>.XET_NONE) == null)
			{
				throw new TypeNotMappedException(Resources.GetString("TypeNotMappedExceptionString"));
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000E5C4 File Offset: 0x0000E5C4
		internal unsafe static IntPtr GetKey(XEAction* actionObj)
		{
			return (IntPtr)((void*)actionObj);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000E79C File Offset: 0x0000E79C
		internal unsafe IntPtr GetKey()
		{
			return (IntPtr)((void*)this.m_action);
		}

		// Token: 0x040001C7 RID: 455
		private Type m_type;

		// Token: 0x040001C8 RID: 456
		private unsafe XEAction* m_action;
	}
}
