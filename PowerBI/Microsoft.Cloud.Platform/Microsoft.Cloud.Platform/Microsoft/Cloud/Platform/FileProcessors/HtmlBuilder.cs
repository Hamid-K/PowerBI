using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000FC RID: 252
	[BlockServiceProvider(typeof(IHtmlBuilder))]
	public class HtmlBuilder : Block, IHtmlBuilder
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001873E File Offset: 0x0001693E
		public HtmlBuilder()
			: base(typeof(HtmlBuilder).Name)
		{
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00018758 File Offset: 0x00016958
		public string BuildHtml(HtmlTemplate template, CultureInfo cultureInfo)
		{
			List<IFileContentProcessor> list = new List<IFileContentProcessor>();
			list.Add(new DynamicTemplateFileProcessor("#", "#", template.PlaceholdersValues.Keys, template.Encoding));
			if (template.Css == null || template.Css.None<CssLocalizationData>())
			{
				list.Add(new LocalizedFileContentProcessor(this.m_stringsManager, "~", "~", template.Encoding, CacheUsage.UseCache));
			}
			else
			{
				list.Add(new EmbeddedCssAndLocalizedFileProcessor(this.m_stringsManager, this.m_fileContentInfoFactory, template.Css, template.Encoding));
			}
			IFileContentInfo fileContentInfo = this.m_fileContentInfoFactory.CreateFileInfo(template.FileContents, template.LastModified);
			FileProcessorInfo fileProcessorInfo = new FileProcessorInfo(cultureInfo, template.PlaceholdersValues);
			foreach (IFileContentProcessor fileContentProcessor in list)
			{
				if (fileContentProcessor.CanProcessFile(fileProcessorInfo))
				{
					fileContentInfo = fileContentProcessor.Process(fileProcessorInfo, fileContentInfo);
				}
			}
			return template.Encoding.GetString(fileContentInfo.FileContents);
		}

		// Token: 0x0400025F RID: 607
		private const string c_dynamicPlaceholderMarker = "#";

		// Token: 0x04000260 RID: 608
		private const string c_localizedPlaceholderMarker = "~";

		// Token: 0x04000261 RID: 609
		[BlockServiceDependency]
		private IFileContentInfoFactory m_fileContentInfoFactory;

		// Token: 0x04000262 RID: 610
		[BlockServiceDependency]
		private ILocalizedStringsManager m_stringsManager;
	}
}
