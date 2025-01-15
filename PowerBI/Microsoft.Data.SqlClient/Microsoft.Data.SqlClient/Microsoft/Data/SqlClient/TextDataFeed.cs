using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000090 RID: 144
	internal class TextDataFeed : DataFeed
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0002227E File Offset: 0x0002047E
		internal TextDataFeed(TextReader source)
		{
			this._source = source;
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00022290 File Offset: 0x00020490
		internal static UnicodeEncoding DefaultEncoding
		{
			get
			{
				UnicodeEncoding unicodeEncoding = TextDataFeed.s_defaultEncoding;
				if (unicodeEncoding == null)
				{
					unicodeEncoding = new UnicodeEncoding(false, false);
					unicodeEncoding = Interlocked.CompareExchange<UnicodeEncoding>(ref TextDataFeed.s_defaultEncoding, unicodeEncoding, null) ?? unicodeEncoding;
				}
				return unicodeEncoding;
			}
		}

		// Token: 0x04000300 RID: 768
		private static UnicodeEncoding s_defaultEncoding;

		// Token: 0x04000301 RID: 769
		internal TextReader _source;
	}
}
