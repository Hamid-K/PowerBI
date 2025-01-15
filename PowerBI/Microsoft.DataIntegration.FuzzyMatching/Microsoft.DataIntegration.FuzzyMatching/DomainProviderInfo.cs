using System;
using System.Runtime.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	internal class DomainProviderInfo : IDeserializationCallback
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		public DomainProviderInfo(IDomainManager domainManager, string domainName, int domainId, JoinSide joinSide)
		{
			this.DomainId = domainId;
			this.Tokenizer = domainManager.GetTokenizer(domainName);
			if (joinSide == JoinSide.Left)
			{
				this.TransformationProvider = domainManager.GetLeftTransformationProvider(domainName);
			}
			else
			{
				if (joinSide != JoinSide.Right)
				{
					throw new ArgumentException("JoinSide must be either Left or Right.");
				}
				this.TransformationProvider = domainManager.GetRightTransformationProvider(domainName);
			}
			if (this.TransformationProvider is ISessionable)
			{
				this.TransformationProviderSession = (this.TransformationProvider as ISessionable).CreateSession();
			}
			this.TokenWeightProvider = domainManager.GetTokenWeightProvider(domainName);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001F162 File Offset: 0x0001D362
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.TransformationProviderSession = (this.TransformationProvider as ISessionable).CreateSession();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001F17A File Offset: 0x0001D37A
		public void ResetSession()
		{
			if (this.TransformationProviderSession != null)
			{
				this.TransformationProviderSession.Reset();
			}
		}

		// Token: 0x040002A3 RID: 675
		public IRecordTokenizer Tokenizer;

		// Token: 0x040002A4 RID: 676
		public TokenizerContext TokenizerContext;

		// Token: 0x040002A5 RID: 677
		public ITransformationProvider TransformationProvider;

		// Token: 0x040002A6 RID: 678
		[NonSerialized]
		public ISession TransformationProviderSession;

		// Token: 0x040002A7 RID: 679
		public ITokenWeightProvider TokenWeightProvider;

		// Token: 0x040002A8 RID: 680
		public int DomainId;
	}
}
