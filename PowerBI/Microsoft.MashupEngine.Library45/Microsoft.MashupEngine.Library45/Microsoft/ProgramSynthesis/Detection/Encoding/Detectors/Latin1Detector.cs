using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000AE2 RID: 2786
	internal class Latin1Detector : ForbiddenByteBasedDetector
	{
		// Token: 0x060045C6 RID: 17862 RVA: 0x000D9C80 File Offset: 0x000D7E80
		internal Latin1Detector()
		{
			IEnumerable<int> enumerable = Enumerable.Range(0, 9).Concat(Enumerable.Range(14, 18)).AppendItem(11)
				.Concat(Enumerable.Range(127, 33));
			Func<int, byte> func;
			if ((func = Latin1Detector.<>O.<0>__ToByte) == null)
			{
				func = (Latin1Detector.<>O.<0>__ToByte = new Func<int, byte>(Convert.ToByte));
			}
			base..ctor(enumerable.Select(func));
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x000D9CDE File Offset: 0x000D7EDE
		public override int Precedence
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x000D9CE4 File Offset: 0x000D7EE4
		public override void ConsumeHeader(byte[] buffer)
		{
			HashSet<byte> hashSet = buffer.Where((byte b) => base.ForbiddenBytes.Contains(b)).ConvertToHashSet<byte>();
			if (hashSet.Count == 0)
			{
				base.Confidence = 1f;
				base.DetectedType = EncodingType.Iso88591;
				return;
			}
			if (hashSet.All((byte b) => b >= 127 && b < 160))
			{
				base.Confidence = 1f;
				base.DetectedType = EncodingType.Windows1252;
			}
		}

		// Token: 0x02000AE3 RID: 2787
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001FDA RID: 8154
			public static Func<int, byte> <0>__ToByte;
		}
	}
}
