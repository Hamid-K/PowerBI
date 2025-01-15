using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F2 RID: 242
	public class DynamicTemplateFileProcessor : TemplateFileProcessor
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x00017F7E File Offset: 0x0001617E
		public DynamicTemplateFileProcessor(string placeholderStartMarker, string placeholderEndMarker, IEnumerable<string> providedDynamicParts)
			: this(placeholderStartMarker, placeholderEndMarker, providedDynamicParts, TemplateFileProcessor.DefaultFileContentEncoding)
		{
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00017F8E File Offset: 0x0001618E
		public DynamicTemplateFileProcessor(string placeholderStartMarker, string placeholderEndMarker, IEnumerable<string> providedDynamicParts, Encoding fileContentEncoding)
			: base(placeholderStartMarker, placeholderEndMarker, fileContentEncoding, CacheUsage.None)
		{
			this.m_dynamicParts = providedDynamicParts.ToList<string>();
			this.m_templateTextHandler = new TemplateTextHandler(base.PlaceholderStartMarker, base.PlaceholderEndMarker);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00017FBE File Offset: 0x000161BE
		public override bool CanProcessFile([NotNull] FileProcessorInfo fileProcessorInfo)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<FileProcessorInfo>(fileProcessorInfo, "requestContext");
			return fileProcessorInfo.ReplacementStrings != null && !this.m_dynamicParts.Except(fileProcessorInfo.ReplacementStrings.Keys, StringComparer.OrdinalIgnoreCase).Any<string>();
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00017FF8 File Offset: 0x000161F8
		protected override IFileContentInfo ProcessExecute([NotNull] FileProcessorInfo fileProcessorInfo, IFileContentInfo fileContentInfo)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<FileProcessorInfo>(fileProcessorInfo, "fileProcessorInfo");
			ExtendedDiagnostics.EnsureArgumentNotNull<IDictionary<string, string>>(fileProcessorInfo.ReplacementStrings, "DynamicContent");
			Dictionary<string, string> dictionary = this.m_dynamicParts.Intersect(fileProcessorInfo.ReplacementStrings.Keys, StringComparer.OrdinalIgnoreCase).ToDictionary((string part) => part, (string part) => fileProcessorInfo.ReplacementStrings[part]);
			string @string = base.FileContentEncoding.GetString(fileContentInfo.FileContents);
			string text = this.m_templateTextHandler.ReplacePlaceholders(@string, dictionary);
			byte[] bytes = base.FileContentEncoding.GetBytes(text);
			return new FileContentInfo(fileContentInfo, bytes);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00014B8A File Offset: 0x00012D8A
		protected override string GetCacheKey(FileProcessorInfo context, IFileContentInfo fileInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400024A RID: 586
		private readonly List<string> m_dynamicParts;

		// Token: 0x0400024B RID: 587
		private readonly TemplateTextHandler m_templateTextHandler;
	}
}
