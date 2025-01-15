using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F1 RID: 241
	public abstract class StaticTemplateFileProcessor : TemplateFileProcessor
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00017EFB File Offset: 0x000160FB
		protected StaticTemplateFileProcessor(string placeholderStartMarker, string placeholderEndMarker, CacheUsage cacheUsage)
			: this(placeholderStartMarker, placeholderEndMarker, TemplateFileProcessor.DefaultFileContentEncoding, cacheUsage)
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00017F0B File Offset: 0x0001610B
		public StaticTemplateFileProcessor(string placeholderStartMarker, string placeholderEndMarker, Encoding fileContentEncoding, CacheUsage cacheUsage)
			: base(placeholderStartMarker, placeholderEndMarker, fileContentEncoding, cacheUsage)
		{
			this.m_templateTextHandler = new TemplateTextHandler(base.PlaceholderStartMarker, base.PlaceholderEndMarker);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006C8 RID: 1736
		protected abstract Dictionary<string, string> PlaceHolderValues { get; }

		// Token: 0x060006C9 RID: 1737 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool CanProcessFile(FileProcessorInfo requestContext)
		{
			return true;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00017F30 File Offset: 0x00016130
		protected override IFileContentInfo ProcessExecute(FileProcessorInfo requestContext, IFileContentInfo fileContentInfo)
		{
			string @string = base.FileContentEncoding.GetString(fileContentInfo.FileContents);
			string text = this.m_templateTextHandler.ReplacePlaceholders(@string, this.PlaceHolderValues);
			byte[] bytes = base.FileContentEncoding.GetBytes(text);
			return new FileContentInfo(fileContentInfo, bytes);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00017F76 File Offset: 0x00016176
		protected override string GetCacheKey(FileProcessorInfo context, IFileContentInfo fileInfo)
		{
			return fileInfo.HashString;
		}

		// Token: 0x04000249 RID: 585
		private readonly TemplateTextHandler m_templateTextHandler;
	}
}
