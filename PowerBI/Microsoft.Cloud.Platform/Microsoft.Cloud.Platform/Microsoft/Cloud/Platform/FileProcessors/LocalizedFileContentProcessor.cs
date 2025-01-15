using System;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F3 RID: 243
	public class LocalizedFileContentProcessor : TemplateFileProcessor
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x000180BE File Offset: 0x000162BE
		public LocalizedFileContentProcessor([NotNull] ILocalizedStringsManager resourcesManager, string placeholderStartMarker, string placeholderEndMarker, Encoding fileContentEncoding, CacheUsage cacheUsage)
			: base(placeholderStartMarker, placeholderEndMarker, fileContentEncoding, cacheUsage)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ILocalizedStringsManager>(resourcesManager, "resourcesManager");
			this.m_resourcesManager = resourcesManager;
			this.m_templateTextHandler = new TemplateTextHandler(placeholderStartMarker, placeholderEndMarker);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000180EB File Offset: 0x000162EB
		public override bool CanProcessFile([NotNull] FileProcessorInfo fileProcessorInfo)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<FileProcessorInfo>(fileProcessorInfo, "requestContext");
			return fileProcessorInfo.Culture != null;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00018104 File Offset: 0x00016304
		protected override IFileContentInfo ProcessExecute(FileProcessorInfo fileProcessorInfo, IFileContentInfo fileContentInfo)
		{
			CultureInfo cultureInfo = fileProcessorInfo.Culture;
			ExtendedDiagnostics.EnsureArgumentNotNull<CultureInfo>(cultureInfo, "No language was specified in request context");
			string @string = base.FileContentEncoding.GetString(fileContentInfo.FileContents);
			string text = this.m_templateTextHandler.ReplacePlaceholders(@string, (string placeholder) => this.CustomizeLocalizedString(placeholder, this.m_resourcesManager.GetLocalizedResource(placeholder, cultureInfo)));
			byte[] bytes = base.FileContentEncoding.GetBytes(text);
			return new FileContentInfo(fileContentInfo, bytes);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00018179 File Offset: 0x00016379
		protected override string GetCacheKey(FileProcessorInfo context, IFileContentInfo fileInfo)
		{
			return "{0}_{1}".FormatWithInvariantCulture(new object[] { fileInfo.HashString, context.Culture });
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001819D File Offset: 0x0001639D
		protected virtual string CustomizeLocalizedString(string key, string localizedResource)
		{
			return localizedResource;
		}

		// Token: 0x0400024C RID: 588
		private readonly TemplateTextHandler m_templateTextHandler;

		// Token: 0x0400024D RID: 589
		private readonly ILocalizedStringsManager m_resourcesManager;
	}
}
