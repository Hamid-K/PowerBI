using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000031 RID: 49
	internal class XEventInteropPackage : IPackage
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000E7BC File Offset: 0x0000E7BC
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000E7D8 File Offset: 0x0000E7D8
		public virtual string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000E7F4 File Offset: 0x0000E7F4
		public virtual Guid PackageId
		{
			get
			{
				return this.m_packageId;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000E814 File Offset: 0x0000E814
		public virtual Guid ModuleId
		{
			get
			{
				return this.m_moduleId;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000E834 File Offset: 0x0000E834
		public virtual IMetadataGeneration Generation
		{
			get
			{
				return this.m_generation;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000E850 File Offset: 0x0000E850
		public virtual ReadOnlyCollection<IEventMetadata> Events
		{
			get
			{
				return this.m_events.AsReadOnly();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000E870 File Offset: 0x0000E870
		public virtual ReadOnlyCollection<IActionMetadata> Actions
		{
			get
			{
				return this.m_actions.AsReadOnly();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000E890 File Offset: 0x0000E890
		public virtual ReadOnlyCollection<IMapMetadata> Maps
		{
			get
			{
				return this.m_maps.AsReadOnly();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000E344 File Offset: 0x0000E344
		public virtual ReadOnlyCollection<ITargetMetadata> Targets
		{
			get
			{
				return new ReadOnlyCollection<ITargetMetadata>(new List<ITargetMetadata>());
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000E8B0 File Offset: 0x0000E8B0
		internal unsafe XEventInteropPackage(XEventInteropMetadataManager md, XEventInteropMetadataGeneration generation, XEPackageMetadata* packageMd)
		{
			this.m_generation = generation;
			this.m_metadata = md;
			this.m_packageMd = packageMd;
			IntPtr intPtr = (IntPtr)(*(*(long*)packageMd + 8L));
			this.m_name = Marshal.PtrToStringUni(intPtr);
			IntPtr intPtr2 = (IntPtr)(*(*(long*)packageMd + 16L));
			this.m_description = Marshal.PtrToStringUni(intPtr2);
			IntPtr intPtr3 = (IntPtr)((void*)(*(long*)packageMd + (byte*)32L));
			this.m_packageId = (Guid)Marshal.PtrToStructure(intPtr3, typeof(Guid));
			IntPtr intPtr4 = (IntPtr)((void*)(*(long*)packageMd + (byte*)56L));
			this.m_moduleId = (Guid)Marshal.PtrToStructure(intPtr4, typeof(Guid));
			this.m_events = new List<IEventMetadata>(0);
			this.m_actions = new List<IActionMetadata>(0);
			this.m_maps = new List<IMapMetadata>(0);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000FDE8 File Offset: 0x0000FDE8
		internal unsafe void MergeCollections(XEPackageMetadata* packageMd)
		{
			this.MarshalCollection<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropEventMetadata,XEEvent,Microsoft::SqlServer::XEvent::IEventMetadata>(packageMd, this.m_events);
			this.MarshalCollection<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropActionMetadata,XEAction,Microsoft::SqlServer::XEvent::IActionMetadata>(packageMd, this.m_actions);
			this.MarshalCollection<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMapMetadata,XEMap,Microsoft::SqlServer::XEvent::IMapMetadata>(packageMd, this.m_maps);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000E98C File Offset: 0x0000E98C
		internal unsafe XEPackageMetadata* GetPackageMd()
		{
			return this.m_packageMd;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000F520 File Offset: 0x0000F520
		private unsafe void MarshalCollection<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropEventMetadata,XEEvent,Microsoft::SqlServer::XEvent::IEventMetadata>(XEPackageMetadata* packageMd, List<IEventMetadata> dest)
		{
			XE_TCollection<0,1> xe_TCollection<0,1>;
			<Module>.XE_TCollection<0,1>.{ctor}(ref xe_TCollection<0,1>, *(long*)(packageMd + 8L / (long)sizeof(XEPackageMetadata)));
			uint num = 0U;
			if (0 < <Module>.XE_TCollection<0,1>.GetCount(ref xe_TCollection<0,1>))
			{
				do
				{
					XEEvent* ptr = <Module>.XE_TCollection<0,1>.GetTyped<struct\u0020XEEvent>(ref xe_TCollection<0,1>, num);
					if (ptr != null)
					{
						string text = new string(*(long*)(ptr + 8L / (long)sizeof(XEEvent)));
						List<IEventMetadata>.Enumerator enumerator = dest.GetEnumerator();
						if (enumerator.MoveNext())
						{
							while (!enumerator.Current.Name.Equals(text))
							{
								if (!enumerator.MoveNext())
								{
									goto IL_005E;
								}
							}
							goto IL_0073;
						}
						IL_005E:
						XEventInteropEventMetadata xeventInteropEventMetadata = new XEventInteropEventMetadata((XEEvent*)ptr, this.m_metadata, this);
						dest.Add(xeventInteropEventMetadata);
					}
					IL_0073:
					num += 1U;
				}
				while (num < <Module>.XE_TCollection<0,1>.GetCount(ref xe_TCollection<0,1>));
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000F5BC File Offset: 0x0000F5BC
		private unsafe void MarshalCollection<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropActionMetadata,XEAction,Microsoft::SqlServer::XEvent::IActionMetadata>(XEPackageMetadata* packageMd, List<IActionMetadata> dest)
		{
			XE_TCollection<0,1> xe_TCollection<0,1>;
			<Module>.XE_TCollection<0,1>.{ctor}(ref xe_TCollection<0,1>, *(long*)(packageMd + 16L / (long)sizeof(XEPackageMetadata)));
			uint num = 0U;
			if (0 < <Module>.XE_TCollection<0,1>.GetCount(ref xe_TCollection<0,1>))
			{
				do
				{
					XEAction* ptr = <Module>.XE_TCollection<0,1>.GetTyped<struct\u0020XEAction>(ref xe_TCollection<0,1>, num);
					if (ptr != null)
					{
						string text = new string(*(long*)(ptr + 8L / (long)sizeof(XEAction)));
						List<IActionMetadata>.Enumerator enumerator = dest.GetEnumerator();
						if (enumerator.MoveNext())
						{
							while (!enumerator.Current.Name.Equals(text))
							{
								if (!enumerator.MoveNext())
								{
									goto IL_005F;
								}
							}
							goto IL_0074;
						}
						IL_005F:
						XEventInteropActionMetadata xeventInteropActionMetadata = new XEventInteropActionMetadata((XEAction*)ptr, this.m_metadata, this);
						dest.Add(xeventInteropActionMetadata);
					}
					IL_0074:
					num += 1U;
				}
				while (num < <Module>.XE_TCollection<0,1>.GetCount(ref xe_TCollection<0,1>));
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000F658 File Offset: 0x0000F658
		private unsafe void MarshalCollection<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMapMetadata,XEMap,Microsoft::SqlServer::XEvent::IMapMetadata>(XEPackageMetadata* packageMd, List<IMapMetadata> dest)
		{
			XE_TCollection<0,1> xe_TCollection<0,1>;
			<Module>.XE_TCollection<0,1>.{ctor}(ref xe_TCollection<0,1>, *(long*)(packageMd + 32L / (long)sizeof(XEPackageMetadata)));
			uint num = 0U;
			if (0 < <Module>.XE_TCollection<0,1>.GetCount(ref xe_TCollection<0,1>))
			{
				do
				{
					XEMap* ptr = <Module>.XE_TCollection<0,1>.GetTyped<struct\u0020XEMap>(ref xe_TCollection<0,1>, num);
					if (ptr != null)
					{
						string text = new string(*(long*)(ptr + 8L / (long)sizeof(XEMap)));
						List<IMapMetadata>.Enumerator enumerator = dest.GetEnumerator();
						if (enumerator.MoveNext())
						{
							while (!enumerator.Current.Name.Equals(text))
							{
								if (!enumerator.MoveNext())
								{
									goto IL_005F;
								}
							}
							goto IL_0074;
						}
						IL_005F:
						XEventInteropMapMetadata xeventInteropMapMetadata = new XEventInteropMapMetadata((XEMap*)ptr, this.m_metadata, this);
						dest.Add(xeventInteropMapMetadata);
					}
					IL_0074:
					num += 1U;
				}
				while (num < <Module>.XE_TCollection<0,1>.GetCount(ref xe_TCollection<0,1>));
			}
			GC.KeepAlive(this);
		}

		// Token: 0x040001A4 RID: 420
		internal List<IEventMetadata> m_events;

		// Token: 0x040001A5 RID: 421
		internal List<IActionMetadata> m_actions;

		// Token: 0x040001A6 RID: 422
		internal List<IMapMetadata> m_maps;

		// Token: 0x040001A7 RID: 423
		private string m_name;

		// Token: 0x040001A8 RID: 424
		private string m_description;

		// Token: 0x040001A9 RID: 425
		private Guid m_moduleId;

		// Token: 0x040001AA RID: 426
		private Guid m_packageId;

		// Token: 0x040001AB RID: 427
		private XEventInteropMetadataGeneration m_generation;

		// Token: 0x040001AC RID: 428
		private XEventInteropMetadataManager m_metadata;

		// Token: 0x040001AD RID: 429
		private unsafe XEPackageMetadata* m_packageMd;
	}
}
