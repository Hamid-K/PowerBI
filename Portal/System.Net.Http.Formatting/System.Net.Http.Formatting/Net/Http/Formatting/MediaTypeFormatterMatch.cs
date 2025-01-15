using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004D RID: 77
	public class MediaTypeFormatterMatch
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public MediaTypeFormatterMatch(MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, double? quality, MediaTypeFormatterMatchRanking ranking)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this.Formatter = formatter;
			this.MediaType = ((mediaType != null) ? mediaType.Clone<MediaTypeHeaderValue>() : MediaTypeConstants.ApplicationOctetStreamMediaType);
			this.Quality = quality ?? 1.0;
			this.Ranking = ranking;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000A121 File Offset: 0x00008321
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000A129 File Offset: 0x00008329
		public MediaTypeFormatter Formatter { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000A132 File Offset: 0x00008332
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000A13A File Offset: 0x0000833A
		public MediaTypeHeaderValue MediaType { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000A143 File Offset: 0x00008343
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000A14B File Offset: 0x0000834B
		public double Quality { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000A154 File Offset: 0x00008354
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000A15C File Offset: 0x0000835C
		public MediaTypeFormatterMatchRanking Ranking { get; private set; }
	}
}
