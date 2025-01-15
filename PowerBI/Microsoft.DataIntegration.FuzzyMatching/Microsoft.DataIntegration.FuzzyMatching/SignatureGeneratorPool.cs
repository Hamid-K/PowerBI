using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000036 RID: 54
	internal class SignatureGeneratorPool
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000840D File Offset: 0x0000660D
		public SignatureGeneratorPool()
		{
			this.m_objectPool = new TemporalObjectPool(new TemporalObjectPool.CreateObjectDelegate(this.CreateInstance))
			{
				MaxCount = SqlClr.ObjectManager.SignatureGeneratorPoolSize
			};
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000843C File Offset: 0x0000663C
		public void ReturnInstance(TemporalHandle context)
		{
			this.m_objectPool.ReturnObject(context);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000844A File Offset: 0x0000664A
		private void ResetInstance(SignatureGeneratorContext instance)
		{
			instance.Reset();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008454 File Offset: 0x00006654
		public TemporalHandle GetInstance(IObjectManager objectManager)
		{
			TemporalHandle temporalHandle = this.m_objectPool.TryGetObject(objectManager);
			SignatureGeneratorContext signatureGeneratorContext = temporalHandle.TryGetObject() as SignatureGeneratorContext;
			if (signatureGeneratorContext == null)
			{
				throw new Exception("Object returned from object pool was unexpectedly null.");
			}
			this.ResetInstance(signatureGeneratorContext);
			return temporalHandle;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008490 File Offset: 0x00006690
		private TemporalHandle CreateInstance(IObjectManager objectManager)
		{
			SignatureGeneratorContext signatureGeneratorContext = new SignatureGeneratorContext();
			lock (this)
			{
				if (this.m_signatureGeneratorInstance == null)
				{
					this.m_signatureGeneratorInstance = this.SignatureGenerator.CreateInstance();
					if (this.m_signatureGeneratorInstance is ISignatureGeneratorInitialize)
					{
						(this.m_signatureGeneratorInstance as ISignatureGeneratorInitialize).Initialize(signatureGeneratorContext.TokenClusterProvider);
					}
				}
				signatureGeneratorContext.OneDimSignatureGenerator = (IOneDimSignatureGenerator)this.m_signatureGeneratorInstance;
				signatureGeneratorContext.MultiDimSignatureGenerator = signatureGeneratorContext.OneDimSignatureGenerator as IMultiDimSignatureGenerator;
				if (this.m_signatureGeneratorInstance is ISessionable)
				{
					signatureGeneratorContext.signatureGeneratorSession = (this.m_signatureGeneratorInstance as ISessionable).CreateSession();
				}
				else
				{
					this.m_signatureGeneratorInstance = null;
				}
			}
			return objectManager.GetObjectHandle(objectManager.CreateReference(signatureGeneratorContext));
		}

		// Token: 0x04000076 RID: 118
		private TemporalObjectPool m_objectPool;

		// Token: 0x04000077 RID: 119
		public SignatureGenerator SignatureGenerator;

		// Token: 0x04000078 RID: 120
		private object m_signatureGeneratorInstance;
	}
}
