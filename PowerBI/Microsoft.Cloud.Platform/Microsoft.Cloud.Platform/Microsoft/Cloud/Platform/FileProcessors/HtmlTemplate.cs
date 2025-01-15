using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000FA RID: 250
	public class HtmlTemplate
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00018662 File Offset: 0x00016862
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0001866A File Offset: 0x0001686A
		public byte[] FileContents { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00018673 File Offset: 0x00016873
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001867B File Offset: 0x0001687B
		public DateTime LastModified { get; private set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00018684 File Offset: 0x00016884
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001868C File Offset: 0x0001688C
		public IEnumerable<CssLocalizationData> Css { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00018695 File Offset: 0x00016895
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001869D File Offset: 0x0001689D
		public IDictionary<string, string> PlaceholdersValues { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x000186A6 File Offset: 0x000168A6
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x000186AE File Offset: 0x000168AE
		public Encoding Encoding { get; private set; }

		// Token: 0x060006FF RID: 1791 RVA: 0x000186B7 File Offset: 0x000168B7
		public HtmlTemplate(string path, IDictionary<string, string> placeholdersValues)
			: this(path, Enumerable.Empty<CssLocalizationData>(), placeholdersValues, Encoding.UTF8)
		{
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000186CB File Offset: 0x000168CB
		public HtmlTemplate(string path, IEnumerable<CssLocalizationData> css, IDictionary<string, string> placeholdersValues)
			: this(path, css, placeholdersValues, Encoding.UTF8)
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000186DB File Offset: 0x000168DB
		public HtmlTemplate(string path, IEnumerable<CssLocalizationData> css, IDictionary<string, string> placeholdersValues, Encoding encoding)
		{
			this.FileContents = File.ReadAllBytes(path);
			this.LastModified = File.GetLastWriteTimeUtc(path);
			this.Css = css;
			this.PlaceholdersValues = placeholdersValues;
			this.Encoding = encoding;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00018711 File Offset: 0x00016911
		public HtmlTemplate(byte[] fileContents, DateTime fileLastModifiedTime, IEnumerable<CssLocalizationData> css, IDictionary<string, string> placeholdersValues, Encoding encoding)
		{
			this.FileContents = fileContents;
			this.LastModified = fileLastModifiedTime;
			this.Css = css;
			this.PlaceholdersValues = placeholdersValues;
			this.Encoding = encoding;
		}
	}
}
