using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public sealed class DomainInstance : IName, IObjectReferenceContainer
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0001161E File Offset: 0x0000F81E
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00011626 File Offset: 0x0000F826
		public int Id { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0001162F File Offset: 0x0000F82F
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00011637 File Offset: 0x0000F837
		public string Name { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00011640 File Offset: 0x0000F840
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00011648 File Offset: 0x0000F848
		public IRecordTokenizer Tokenizer { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00011651 File Offset: 0x0000F851
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00011659 File Offset: 0x0000F859
		public ITokenWeightProvider TokenWeightProvider { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00011662 File Offset: 0x0000F862
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0001166A File Offset: 0x0000F86A
		public TransformationProviderAggregator LeftTransformationProvider { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00011673 File Offset: 0x0000F873
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0001167B File Offset: 0x0000F87B
		public TransformationProviderAggregator RightTransformationProvider { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00011684 File Offset: 0x0000F884
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0001168C File Offset: 0x0000F88C
		public PairSpecificTransformationProviderAggregator PairSpecificTransformationProvider { get; private set; }

		// Token: 0x06000398 RID: 920 RVA: 0x00011698 File Offset: 0x0000F898
		public DomainInstance(int id, string name)
		{
			this.Id = id;
			this.Name = name;
			this.TokenWeightProvider = new ConstantWeightProvider();
			this.LeftTransformationProvider = new TransformationProviderAggregator();
			this.RightTransformationProvider = new TransformationProviderAggregator();
			this.PairSpecificTransformationProvider = new PairSpecificTransformationProviderAggregator();
			this.TokenClusterHasBeenComputed = false;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000116EC File Offset: 0x0000F8EC
		public void AcquireReferences()
		{
			if (this.Tokenizer is IObjectReferenceContainer)
			{
				(this.Tokenizer as IObjectReferenceContainer).AcquireReferences();
			}
			if (this.TokenWeightProvider is IObjectReferenceContainer)
			{
				(this.TokenWeightProvider as IObjectReferenceContainer).AcquireReferences();
			}
			if (this.LeftTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.LeftTransformationProvider).AcquireReferences();
			}
			if (this.RightTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.RightTransformationProvider).AcquireReferences();
			}
			if (this.PairSpecificTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.PairSpecificTransformationProvider).AcquireReferences();
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001176C File Offset: 0x0000F96C
		public void UpdateReferences()
		{
			if (this.Tokenizer is IObjectReferenceContainer)
			{
				(this.Tokenizer as IObjectReferenceContainer).UpdateReferences();
			}
			if (this.TokenWeightProvider is IObjectReferenceContainer)
			{
				(this.TokenWeightProvider as IObjectReferenceContainer).UpdateReferences();
			}
			if (this.LeftTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.LeftTransformationProvider).UpdateReferences();
			}
			if (this.RightTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.RightTransformationProvider).UpdateReferences();
			}
			if (this.PairSpecificTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.PairSpecificTransformationProvider).UpdateReferences();
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000117EC File Offset: 0x0000F9EC
		public void ReleaseReferences()
		{
			if (this.Tokenizer is IObjectReferenceContainer)
			{
				(this.Tokenizer as IObjectReferenceContainer).ReleaseReferences();
			}
			if (this.TokenWeightProvider is IObjectReferenceContainer)
			{
				(this.TokenWeightProvider as IObjectReferenceContainer).ReleaseReferences();
			}
			if (this.LeftTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.LeftTransformationProvider).ReleaseReferences();
			}
			if (this.RightTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.RightTransformationProvider).ReleaseReferences();
			}
			if (this.PairSpecificTransformationProvider != null)
			{
				((IObjectReferenceContainer)this.PairSpecificTransformationProvider).ReleaseReferences();
			}
		}

		// Token: 0x0400013B RID: 315
		internal bool TokenClusterHasBeenComputed;
	}
}
