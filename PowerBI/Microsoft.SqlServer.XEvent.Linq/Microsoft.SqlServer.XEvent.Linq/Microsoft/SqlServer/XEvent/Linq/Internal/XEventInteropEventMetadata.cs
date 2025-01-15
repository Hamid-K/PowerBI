using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.XEvent.TypeSystem;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000035 RID: 53
	internal class XEventInteropEventMetadata : XEventInteropObject, IEventMetadata
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000E424 File Offset: 0x0000E424
		public virtual ReadOnlyCollection<IEventFieldMetadata> Fields
		{
			get
			{
				return Array.AsReadOnly<IEventFieldMetadata>(this.m_fieldMetadata);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000E444 File Offset: 0x0000E444
		public virtual Guid UUID
		{
			get
			{
				return this.m_uuid;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000E464 File Offset: 0x0000E464
		public virtual int Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000E480 File Offset: 0x0000E480
		internal unsafe XEventInteropEventMetadata(XEEvent* eventObj, XEventInteropMetadataManager md, IPackage package)
			: base(*(long*)(eventObj + 8L / (long)sizeof(XEEvent)), *(long*)(eventObj + 16L / (long)sizeof(XEEvent)), md, package)
		{
			this.m_eventType = eventObj;
			long num = ((*(ushort*)eventObj < 10) ? 1L : 0L) * 40L + *(long*)(eventObj + 48L / (long)sizeof(XEEvent));
			this.m_uuid = (Guid)XETypeMappings.UnloadXeVariant(*(num + 16L), *num, 16);
			XEEvent* eventType = this.m_eventType;
			long num2 = ((*(ushort*)eventType < 10) ? 2L : 1L) * 40L + *(long*)(eventType + 48L / (long)sizeof(XEEvent));
			this.m_version = (int)((byte)XETypeMappings.UnloadXeVariant(*(num2 + 16L), *num2, 1));
			this.m_fieldMetadata = new IEventFieldMetadata[(int)(*(ushort*)(eventObj + 36L / (long)sizeof(XEEvent)))];
			int num3 = 0;
			if (0 < *(ushort*)(eventObj + 36L / (long)sizeof(XEEvent)))
			{
				long num4 = 0L;
				do
				{
					string text = Marshal.PtrToStringUni((IntPtr)(*(num4 + *(long*)(eventObj + 64L / (long)sizeof(XEEvent)) + 8L)));
					Type type = XETypeMappings.ManagedTypeFromXEType((uint)(*(*(long*)(eventObj + 64L / (long)sizeof(XEEvent)) + num4)));
					if (type == null)
					{
						goto IL_00FE;
					}
					this.m_fieldMetadata[num3] = new GenericEventFieldMetadata(text, type);
					num3++;
					num4 += 40L;
				}
				while (num3 < (int)(*(ushort*)(eventObj + 36L / (long)sizeof(XEEvent))));
				return;
				IL_00FE:
				throw new TypeNotMappedException(Resources.GetString("TypeNotMappedExceptionString"));
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000E5C4 File Offset: 0x0000E5C4
		internal unsafe static IntPtr GetKey(XEEvent* eventObj)
		{
			return (IntPtr)((void*)eventObj);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000E5A4 File Offset: 0x0000E5A4
		internal unsafe IntPtr GetKey()
		{
			return (IntPtr)((void*)this.m_eventType);
		}

		// Token: 0x040001C1 RID: 449
		private unsafe XEEvent* m_eventType;

		// Token: 0x040001C2 RID: 450
		private Guid m_uuid;

		// Token: 0x040001C3 RID: 451
		private int m_version;

		// Token: 0x040001C4 RID: 452
		private IEventFieldMetadata[] m_fieldMetadata;
	}
}
