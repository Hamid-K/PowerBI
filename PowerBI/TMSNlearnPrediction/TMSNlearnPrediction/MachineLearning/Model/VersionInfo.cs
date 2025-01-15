using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x02000473 RID: 1139
	public struct VersionInfo
	{
		// Token: 0x060017B6 RID: 6070 RVA: 0x000886D8 File Offset: 0x000868D8
		public VersionInfo(string modelSignature, uint verWrittenCur, uint verReadableCur, uint verWeCanReadBack, string loaderSignature = null, string loaderSignatureAlt = null)
		{
			Contracts.Check(Utils.Size(modelSignature) == 8, "Model signature must be eight characters");
			this.ModelSignature = 0UL;
			for (int i = 0; i < modelSignature.Length; i++)
			{
				char c = modelSignature[i];
				Contracts.Check(c <= 'ÿ');
				if (c != ' ')
				{
					this.ModelSignature |= (ulong)c << i * 8;
				}
			}
			this.VerWrittenCur = verWrittenCur;
			this.VerReadableCur = verReadableCur;
			this.VerWeCanReadBack = verWeCanReadBack;
			this.LoaderSignature = loaderSignature;
			this.LoaderSignatureAlt = loaderSignatureAlt;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0008876A File Offset: 0x0008696A
		public VersionInfo(ulong modelSignature, uint verWrittenCur, uint verReadableCur, uint verWeCanReadBack, string loaderSignature = null, string loaderSignatureAlt = null)
		{
			this.ModelSignature = modelSignature;
			this.VerWrittenCur = verWrittenCur;
			this.VerReadableCur = verReadableCur;
			this.VerWeCanReadBack = verWeCanReadBack;
			this.LoaderSignature = loaderSignature;
			this.LoaderSignatureAlt = loaderSignatureAlt;
		}

		// Token: 0x04000E58 RID: 3672
		public readonly ulong ModelSignature;

		// Token: 0x04000E59 RID: 3673
		public readonly uint VerWrittenCur;

		// Token: 0x04000E5A RID: 3674
		public readonly uint VerReadableCur;

		// Token: 0x04000E5B RID: 3675
		public readonly uint VerWeCanReadBack;

		// Token: 0x04000E5C RID: 3676
		public readonly string LoaderSignature;

		// Token: 0x04000E5D RID: 3677
		public readonly string LoaderSignatureAlt;
	}
}
