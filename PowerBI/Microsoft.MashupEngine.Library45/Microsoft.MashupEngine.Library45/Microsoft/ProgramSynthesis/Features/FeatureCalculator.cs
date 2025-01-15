using System;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007D1 RID: 2001
	[DataContract]
	public abstract class FeatureCalculator
	{
		// Token: 0x06002A9E RID: 10910 RVA: 0x0007781D File Offset: 0x00075A1D
		protected FeatureCalculator(FeatureInfo feature, bool supportsLearningInfo)
		{
			this.Feature = feature;
			this.SupportsLearningInfo = supportsLearningInfo;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00077833 File Offset: 0x00075A33
		// (set) Token: 0x06002AA0 RID: 10912 RVA: 0x0007783B File Offset: 0x00075A3B
		[DataMember]
		public FeatureInfo Feature { get; private set; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x00077844 File Offset: 0x00075A44
		// (set) Token: 0x06002AA2 RID: 10914 RVA: 0x0007784C File Offset: 0x00075A4C
		[DataMember]
		public bool SupportsLearningInfo { get; private set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x00002188 File Offset: 0x00000388
		internal virtual MethodInfo Method
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002AA4 RID: 10916
		public abstract object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null);

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0000CC37 File Offset: 0x0000AE37
		internal virtual void Validate(DiagnosticsContext diagnosticsContext)
		{
		}
	}
}
