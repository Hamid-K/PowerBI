using System;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F5 RID: 245
	public sealed class CssLocalizationData
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000181DA File Offset: 0x000163DA
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x000181E2 File Offset: 0x000163E2
		public string CssPlaceholder { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000181EB File Offset: 0x000163EB
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x000181F3 File Offset: 0x000163F3
		public string LtrCssValue { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x000181FC File Offset: 0x000163FC
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00018204 File Offset: 0x00016404
		public string RtlCssValue { get; private set; }

		// Token: 0x060006DF RID: 1759 RVA: 0x00018210 File Offset: 0x00016410
		public static CssLocalizationData GetCssLocalizationDataFromFile([NotNull] string cssPlaceholder, [NotNull] string ltrCssFilePath, [NotNull] string rtlCssFilePath, [NotNull] IFileContentInfoFactory fileInfoFactory, [NotNull] Encoding encoding)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(cssPlaceholder, "cssPlaceholder");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(ltrCssFilePath, "ltrCssFilePath");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(rtlCssFilePath, "rtlCssFilePath");
			ExtendedDiagnostics.EnsureArgumentNotNull<IFileContentInfoFactory>(fileInfoFactory, "fileInfoFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<Encoding>(encoding, "encoding");
			IFileContentInfo fileContentInfo = fileInfoFactory.CreateFileInfo(ltrCssFilePath);
			IFileContentInfo fileContentInfo2 = fileInfoFactory.CreateFileInfo(rtlCssFilePath);
			string @string = encoding.GetString(fileContentInfo.FileContents);
			string string2 = encoding.GetString(fileContentInfo2.FileContents);
			return new CssLocalizationData(cssPlaceholder, @string, string2);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00018289 File Offset: 0x00016489
		public CssLocalizationData([NotNull] string cssPlaceholder, [NotNull] string ltrCssValue, [NotNull] string rtlCssValue)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(cssPlaceholder, "cssPlaceholder");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(ltrCssValue, "ltrCssValue");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(rtlCssValue, "rtlCssValue");
			this.CssPlaceholder = cssPlaceholder;
			this.LtrCssValue = ltrCssValue;
			this.RtlCssValue = rtlCssValue;
		}
	}
}
