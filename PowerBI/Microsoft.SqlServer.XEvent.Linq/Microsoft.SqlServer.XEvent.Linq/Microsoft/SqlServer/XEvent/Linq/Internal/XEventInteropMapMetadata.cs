using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000036 RID: 54
	internal class XEventInteropMapMetadata : XEventInteropObject, IMapMetadata
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000E5E0 File Offset: 0x0000E5E0
		public virtual ReadOnlyCollection<MapValue> Entries
		{
			get
			{
				MapValue[] array = new MapValue[this.m_entries.Count];
				this.m_entries.Values.CopyTo(array, 0);
				return Array.AsReadOnly<MapValue>(array);
			}
		}

		// Token: 0x17000019 RID: 25
		public virtual MapValue this[uint value]
		{
			get
			{
				return (MapValue)this.m_entries[value];
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000E648 File Offset: 0x0000E648
		internal unsafe XEventInteropMapMetadata(XEMap* mapObj, XEventInteropMetadataManager md, IPackage package)
			: base(*(long*)(mapObj + 8L / (long)sizeof(XEMap)), *(long*)(mapObj + 16L / (long)sizeof(XEMap)), md, package)
		{
			this.m_map = mapObj;
			this.m_entries = new Hashtable((int)(*(ushort*)(mapObj + 34L / (long)sizeof(XEMap))));
			int num = 0;
			if (0 < *(ushort*)(mapObj + 34L / (long)sizeof(XEMap)))
			{
				long num2 = 0L;
				do
				{
					IntPtr intPtr = (IntPtr)(*(num2 + *(long*)(mapObj + 40L / (long)sizeof(XEMap)) + 8L));
					MapValue mapValue = new MapValue((uint)(*(*(long*)(mapObj + 40L / (long)sizeof(XEMap)) + num2)), Marshal.PtrToStringUni(intPtr));
					this.m_entries[mapValue.Key] = mapValue;
					num++;
					num2 += 16L;
				}
				while (num < (int)(*(ushort*)(mapObj + 34L / (long)sizeof(XEMap))));
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000E5C4 File Offset: 0x0000E5C4
		internal unsafe static IntPtr GetKey(XEMap* mapObj)
		{
			return (IntPtr)((void*)mapObj);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000E6E8 File Offset: 0x0000E6E8
		internal unsafe IntPtr GetKey()
		{
			return (IntPtr)((void*)this.m_map);
		}

		// Token: 0x040001C5 RID: 453
		private Hashtable m_entries;

		// Token: 0x040001C6 RID: 454
		private unsafe XEMap* m_map;
	}
}
