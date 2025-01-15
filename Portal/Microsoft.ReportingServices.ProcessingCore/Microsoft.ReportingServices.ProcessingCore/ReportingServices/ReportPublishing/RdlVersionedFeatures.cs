using System;
using System.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200038D RID: 909
	internal sealed class RdlVersionedFeatures
	{
		// Token: 0x06002527 RID: 9511 RVA: 0x000B1AC0 File Offset: 0x000AFCC0
		public RdlVersionedFeatures()
		{
			int length = Enum.GetValues(typeof(RdlFeatures)).Length;
			this.m_rdlFeatureVersioningStructure = new RdlVersionedFeatures.FeatureDescriptor[length];
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000B1AF4 File Offset: 0x000AFCF4
		internal void Add(RdlFeatures featureType, int addedInCompatVersion, RenderMode allowedRenderModes)
		{
			this.m_rdlFeatureVersioningStructure[(int)featureType] = new RdlVersionedFeatures.FeatureDescriptor(addedInCompatVersion, allowedRenderModes);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000B1B14 File Offset: 0x000AFD14
		internal bool IsRdlFeatureAllowed(RdlFeatures feature, int configVersion, RenderMode renderMode)
		{
			RdlVersionedFeatures.FeatureDescriptor featureDescriptor = this.m_rdlFeatureVersioningStructure[(int)feature];
			bool flag = configVersion == 0 || featureDescriptor.AddedInCompatVersion <= configVersion;
			bool flag2 = (featureDescriptor.AllowedRenderModes & renderMode) == renderMode;
			return flag && flag2;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000B1B4A File Offset: 0x000AFD4A
		internal void VerifyAllFeaturesAreAdded()
		{
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000B1B4C File Offset: 0x000AFD4C
		[Conditional("DEBUG")]
		private void VerifyAllFeaturesAreAdded(RdlVersionedFeatures.FeatureDescriptor[] rdlFeatureVersioningStructure)
		{
			for (int i = 0; i < rdlFeatureVersioningStructure.Length; i++)
			{
				Global.Tracer.Assert(rdlFeatureVersioningStructure[i] != null, "Missing RDL feature for: {0}", new object[] { (RdlFeatures)i });
			}
		}

		// Token: 0x04001591 RID: 5521
		private readonly RdlVersionedFeatures.FeatureDescriptor[] m_rdlFeatureVersioningStructure;

		// Token: 0x02000955 RID: 2389
		private sealed class FeatureDescriptor
		{
			// Token: 0x06007FF6 RID: 32758 RVA: 0x002104D9 File Offset: 0x0020E6D9
			public FeatureDescriptor(int addedInCompatVersion, RenderMode allowedRenderModes)
			{
				this.m_addedInCompatVersion = addedInCompatVersion;
				this.m_allowedRenderModes = allowedRenderModes;
			}

			// Token: 0x1700297A RID: 10618
			// (get) Token: 0x06007FF7 RID: 32759 RVA: 0x002104EF File Offset: 0x0020E6EF
			public int AddedInCompatVersion
			{
				get
				{
					return this.m_addedInCompatVersion;
				}
			}

			// Token: 0x1700297B RID: 10619
			// (get) Token: 0x06007FF8 RID: 32760 RVA: 0x002104F7 File Offset: 0x0020E6F7
			public RenderMode AllowedRenderModes
			{
				get
				{
					return this.m_allowedRenderModes;
				}
			}

			// Token: 0x04004077 RID: 16503
			private readonly int m_addedInCompatVersion;

			// Token: 0x04004078 RID: 16504
			private readonly RenderMode m_allowedRenderModes;
		}
	}
}
