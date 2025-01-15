using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class ProviderReference<T> : IProviderInitialize, ISessionable, IRowsetConsumer, IMemoryLimit, IMemoryUsage, IObjectReferenceContainer
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000051F2 File Offset: 0x000033F2
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000051FA File Offset: 0x000033FA
		public string ObjectName { get; set; }

		// Token: 0x06000105 RID: 261 RVA: 0x00005203 File Offset: 0x00003403
		public void AcquireReferences()
		{
			if (this.m_interface == null)
			{
				this.UpdateReferences();
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005218 File Offset: 0x00003418
		public void UpdateReferences()
		{
			object @object = SqlClr.ObjectManager.GetObject(this.ObjectName);
			this.m_interface = (T)((object)@object);
			if (this.m_interface is IObjectReferenceContainer)
			{
				(this.m_interface as IObjectReferenceContainer).UpdateReferences();
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005269 File Offset: 0x00003469
		public void ReleaseReferences()
		{
			if (this.m_interface is IObjectReferenceContainer)
			{
				(this.m_interface as IObjectReferenceContainer).ReleaseReferences();
			}
			this.m_interface = default(T);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000529E File Offset: 0x0000349E
		public long MemoryUsage
		{
			get
			{
				if (this.m_interface is IMemoryUsage)
				{
					return (this.m_interface as IMemoryUsage).MemoryUsage;
				}
				return 0L;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000052CA File Offset: 0x000034CA
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000052F6 File Offset: 0x000034F6
		public long MemoryLimit
		{
			get
			{
				if (this.m_interface is IMemoryLimit)
				{
					return (this.m_interface as IMemoryLimit).MemoryLimit;
				}
				return 0L;
			}
			set
			{
				if (this.m_interface is IMemoryLimit)
				{
					(this.m_interface as IMemoryLimit).MemoryLimit = value;
					return;
				}
				if (value != 0L)
				{
					throw new Exception("Referenced object does not implement IMemoryLimit.");
				}
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000532F File Offset: 0x0000352F
		public void RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (this.m_interface is IRowsetConsumer)
			{
				(this.m_interface as IRowsetConsumer).RequestRowsets(rowsetDistributor);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005359 File Offset: 0x00003559
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				if (this.m_interface is IRowsetConsumer)
				{
					return (this.m_interface as IRowsetConsumer).RowsetSinks;
				}
				return new List<IRowsetSink>();
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005388 File Offset: 0x00003588
		public ISession CreateSession()
		{
			if (this.m_interface is ISessionable)
			{
				return (this.m_interface as ISessionable).CreateSession();
			}
			return null;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000053B3 File Offset: 0x000035B3
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			if (this.m_interface is IProviderInitialize)
			{
				(this.m_interface as IProviderInitialize).Initialize(domainManager, domainName);
			}
		}

		// Token: 0x04000044 RID: 68
		[NonSerialized]
		protected T m_interface;
	}
}
