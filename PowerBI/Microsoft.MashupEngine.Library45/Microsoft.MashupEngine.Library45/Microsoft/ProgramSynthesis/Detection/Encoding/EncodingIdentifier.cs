using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Detection.Encoding.Detectors;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Encoding
{
	// Token: 0x02000AD3 RID: 2771
	public class EncodingIdentifier
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x000D94D5 File Offset: 0x000D76D5
		// (set) Token: 0x0600457C RID: 17788 RVA: 0x000D94DD File Offset: 0x000D76DD
		internal byte[] HeaderSample { get; private set; }

		// Token: 0x0600457D RID: 17789 RVA: 0x000D94E6 File Offset: 0x000D76E6
		private void SampleStream(Stream stream)
		{
			this.HeaderSample = stream.RepeatReadAndAllocate(1048576);
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x000D94FC File Offset: 0x000D76FC
		private EncodingType DetectEncoding(Stream stream, bool enableAsciiDetector)
		{
			this.SampleStream(stream);
			List<IEncodingDetector> list = new List<IEncodingDetector>();
			list.Add(new Utf8DecodeBasedDetector());
			list.Add(new Latin1Detector());
			list.Add(new Utf32DecodeBasedDetector());
			list.Add(enableAsciiDetector ? new AsciiDetector() : null);
			list.Add(new Utf16DecodeBasedDetector());
			List<IEncodingDetector> list2 = list.Where((IEncodingDetector d) => d != null).ToList<IEncodingDetector>();
			List<Task> list3 = new List<Task>();
			using (List<IEncodingDetector>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEncodingDetector detector = enumerator.Current;
					list3.Add(Task.Run(delegate
					{
						detector.ConsumeHeader(this.HeaderSample);
					}));
				}
			}
			Task.WaitAll(list3.ToArray());
			IEnumerable<IEncodingDetector> enumerable = (from d in list2
				where (double)d.Confidence > 0.95
				group d by d.Confidence into g
				orderby g.Key descending
				select g).FirstOrDefault<IGrouping<float, IEncodingDetector>>();
			if (enumerable == null || !enumerable.Any<IEncodingDetector>())
			{
				return EncodingType.Unknown;
			}
			IEncodingDetector encodingDetector = enumerable.ArgMin((IEncodingDetector d) => d.Precedence);
			if (encodingDetector == null)
			{
				return EncodingType.Unknown;
			}
			return encodingDetector.DetectedType;
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x000D96B4 File Offset: 0x000D78B4
		public static EncodingType IdentifyEncoding(Stream stream, bool enableAsciiDetector = false)
		{
			return new EncodingIdentifier().DetectEncoding(stream, enableAsciiDetector);
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000D96C4 File Offset: 0x000D78C4
		public static EncodingType IdentifyEncoding(byte[] buffer, bool enableAsciiDetector = false)
		{
			EncodingType encodingType;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				encodingType = EncodingIdentifier.IdentifyEncoding(memoryStream, enableAsciiDetector);
			}
			return encodingType;
		}

		// Token: 0x04001FB3 RID: 8115
		private const int HeaderSampleSize = 1048576;
	}
}
