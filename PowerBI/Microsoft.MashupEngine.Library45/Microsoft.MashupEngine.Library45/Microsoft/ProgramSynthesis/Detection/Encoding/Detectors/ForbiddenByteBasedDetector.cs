using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000AE0 RID: 2784
	internal abstract class ForbiddenByteBasedDetector : IEncodingDetector
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x000D9C19 File Offset: 0x000D7E19
		// (set) Token: 0x060045B9 RID: 17849 RVA: 0x000D9C21 File Offset: 0x000D7E21
		private protected HashSet<byte> ForbiddenBytes { protected get; private set; }

		// Token: 0x060045BA RID: 17850 RVA: 0x000D9C2A File Offset: 0x000D7E2A
		protected ForbiddenByteBasedDetector(params byte[] forbiddenBytes)
		{
			this.ForbiddenBytes = new HashSet<byte>(forbiddenBytes);
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x000D9C3E File Offset: 0x000D7E3E
		protected ForbiddenByteBasedDetector(IEnumerable<byte> forbiddenBytes)
		{
			this.ForbiddenBytes = new HashSet<byte>(forbiddenBytes);
			this.DetectedType = EncodingType.Unknown;
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060045BC RID: 17852
		public abstract int Precedence { get; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x000D9C5D File Offset: 0x000D7E5D
		// (set) Token: 0x060045BE RID: 17854 RVA: 0x000D9C65 File Offset: 0x000D7E65
		public float Confidence { get; protected set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000D9C6E File Offset: 0x000D7E6E
		// (set) Token: 0x060045C0 RID: 17856 RVA: 0x000D9C76 File Offset: 0x000D7E76
		public EncodingType DetectedType { get; protected set; }

		// Token: 0x060045C1 RID: 17857
		public abstract void ConsumeHeader(byte[] buffer);
	}
}
