using System;
using System.Runtime.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	internal class ComparerPool : IDeserializationCallback
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001EF16 File Offset: 0x0001D116
		public ComparerPool()
		{
			this.m_instancePool = new TemporalObjectPool(new TemporalObjectPool.CreateObjectDelegate(this.CreateInstance))
			{
				MaxCount = GlobalConfiguration.ComparerPoolSize
			};
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001EF40 File Offset: 0x0001D140
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.m_instancePool = new TemporalObjectPool(new TemporalObjectPool.CreateObjectDelegate(this.CreateInstance))
			{
				MaxCount = GlobalConfiguration.ComparerPoolSize
			};
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001EF64 File Offset: 0x0001D164
		public void ReturnInstance(TemporalHandle instanceHandle)
		{
			this.m_instancePool.ReturnObject(instanceHandle);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001EF74 File Offset: 0x0001D174
		public TemporalHandle GetInstance(TemporalHandle domainManagerHandle, IObjectManager objectManager)
		{
			TemporalHandle temporalHandle = this.m_instancePool.TryGetObject(objectManager);
			ComparerInstance comparerInstance = temporalHandle.TryGetObject() as ComparerInstance;
			if (comparerInstance == null)
			{
				throw new Exception("Object returned from object pool was unexpectedly null.");
			}
			this.ResetInstance(comparerInstance, domainManagerHandle);
			return temporalHandle;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		private void ResetInstance(ComparerInstance instance, TemporalHandle domainManagerHandle)
		{
			DomainManager domainManager = (DomainManager)domainManagerHandle.GetObject();
			instance.Reset();
			instance.TransientTokenIdProvider.SetPrimaryTokenIdProvider(domainManager.TokenIdProvider);
			DomainManager domainManager2;
			if (instance.DomainManagerHandle == null || (domainManager2 = instance.DomainManagerHandle.TryGetObject() as DomainManager) == null || domainManager2 != domainManager)
			{
				instance.ComparerSession = instance.Comparer.CreateSession(domainManager, instance.TransientTokenIdProvider);
				instance.DomainManagerHandle = domainManagerHandle;
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001F020 File Offset: 0x0001D220
		private TemporalHandle CreateInstance(IObjectManager objectManager)
		{
			ComparerInstance comparerInstance = new ComparerInstance
			{
				Comparer = (IFuzzyComparer)this.Comparer.CreateInstance()
			};
			comparerInstance.Comparer.Initialize(this.Comparer.LeftRecordBinding, this.Comparer.RightRecordBinding, this.Comparer.Domains.ConvertAll<string>((DomainName n) => n.Name), this.Comparer.ExactMatchDomains.ConvertAll<string>((DomainName n) => n.Name));
			return objectManager.GetObjectHandle(objectManager.CreateReference(comparerInstance));
		}

		// Token: 0x040002A1 RID: 673
		public Comparer Comparer;

		// Token: 0x040002A2 RID: 674
		[NonSerialized]
		private TemporalObjectPool m_instancePool;
	}
}
