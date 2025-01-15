using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x0200002F RID: 47
	internal class XEventInteropMetadataGeneration : IMetadataGeneration
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000EBCC File Offset: 0x0000EBCC
		internal XEventInteropPackage GetPackage(ushort packageId)
		{
			return this.m_packages[(int)packageId];
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000EB6C File Offset: 0x0000EB6C
		public virtual IPackage GetPackage(Guid moduleId, string name)
		{
			IEnumerator enumerator = this.m_packages.GetEnumerator();
			if (enumerator.MoveNext())
			{
				IPackage package;
				for (;;)
				{
					package = (IPackage)enumerator.Current;
					if (package != null && package.ModuleId == moduleId && package.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
					{
						break;
					}
					if (!enumerator.MoveNext())
					{
						goto IL_004C;
					}
				}
				return package;
			}
			IL_004C:
			return null;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000EA38 File Offset: 0x0000EA38
		public virtual ReadOnlyCollection<IPackage> Packages
		{
			get
			{
				List<IPackage> list = new List<IPackage>();
				int num = 0;
				do
				{
					XEventInteropPackage xeventInteropPackage = this.m_packages[num];
					if (xeventInteropPackage != null)
					{
						list.Add(xeventInteropPackage);
					}
					num++;
				}
				while (num < 1024);
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000EA78 File Offset: 0x0000EA78
		public unsafe virtual KeyValuePair<Guid, ushort> GenerationId
		{
			get
			{
				Guid guid = (Guid)Marshal.PtrToStructure((IntPtr)((void*)(this.m_logMetadata + 8L / (long)sizeof(XE_LogDefaultMetadataPackageHeader))), typeof(Guid));
				KeyValuePair<Guid, ushort> keyValuePair = new KeyValuePair<Guid, ushort>(guid, *(ushort*)(this.m_logMetadata + 24L / (long)sizeof(XE_LogDefaultMetadataPackageHeader)));
				return keyValuePair;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000EACC File Offset: 0x0000EACC
		public unsafe virtual int PointerSize
		{
			get
			{
				if (this.m_ptrSize == 0)
				{
					XEPackageMetadata* packageMd = this.m_packages[0].GetPackageMd();
					XE_TCollection<1,0> xe_TCollection<1,0>;
					<Module>.XE_TCollection<1,0>.{ctor}(ref xe_TCollection<1,0>, *(long*)(packageMd + 56L / (long)sizeof(XEPackageMetadata)));
					XEType* ptr = <Module>.XE_TCollection<1,0>.Get(ref xe_TCollection<1,0>, 13U);
					int num;
					if (ptr != null)
					{
						num = (int)(*(byte*)(ptr + 32L / (long)sizeof(XEType)));
					}
					else
					{
						num = 0;
					}
					this.m_ptrSize = num;
				}
				GC.KeepAlive(this);
				return this.m_ptrSize;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000EB30 File Offset: 0x0000EB30
		public virtual int GenerationRevision
		{
			get
			{
				return this.m_revision;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000EB4C File Offset: 0x0000EB4C
		public void IncrementRevision()
		{
			Interlocked.Increment(ref this.m_revision);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000E9A8 File Offset: 0x0000E9A8
		internal unsafe XEventInteropMetadataGeneration(XE_LogDefaultMetadataPackageHeader* logMetadata)
		{
			this.m_revision = 0;
			this.m_ptrSize = 0;
			this.m_logMetadata = logMetadata;
			this.m_packages = new XEventInteropPackage[1024];
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000E9E8 File Offset: 0x0000E9E8
		internal void AddPackage(ushort packageId, XEventInteropPackage package)
		{
			this.m_packages[(int)packageId] = package;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000EA04 File Offset: 0x0000EA04
		internal unsafe void GetTicksConfig(XETicksConfig* pConfig)
		{
			XE_LogDefaultMetadataPackageHeader* logMetadata = this.m_logMetadata;
			if (logMetadata != null)
			{
				cpblk(pConfig, logMetadata + 32L / (long)sizeof(XE_LogDefaultMetadataPackageHeader), 24);
			}
			else
			{
				initblk(pConfig, 0, 24L);
			}
		}

		// Token: 0x04000199 RID: 409
		private XEventInteropPackage[] m_packages;

		// Token: 0x0400019A RID: 410
		private unsafe XE_LogDefaultMetadataPackageHeader* m_logMetadata;

		// Token: 0x0400019B RID: 411
		private int m_revision;

		// Token: 0x0400019C RID: 412
		private int m_ptrSize;
	}
}
