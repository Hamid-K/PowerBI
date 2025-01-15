using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F9 RID: 249
	public class RegexReplacementFileContentProcessor : BaseFileContentProcessor
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x000185F8 File Offset: 0x000167F8
		public RegexReplacementFileContentProcessor(Regex pattern, string replacement)
			: base(CacheUsage.UseCache)
		{
			this.m_replacement = replacement;
			this.m_pattern = pattern;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool CanProcessFile(FileProcessorInfo ignoredContext)
		{
			return true;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00017F76 File Offset: 0x00016176
		protected override string GetCacheKey(FileProcessorInfo ignoredContext, IFileContentInfo fileInfo)
		{
			return fileInfo.HashString;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001861C File Offset: 0x0001681C
		protected override IFileContentInfo ProcessExecute(FileProcessorInfo ignoredContext, IFileContentInfo fileInfo)
		{
			byte[] fileContents = fileInfo.FileContents;
			string @string = this.DefaultFileContentEncoding.GetString(fileContents);
			string text = this.m_pattern.Replace(@string, this.m_replacement);
			return new FileContentInfo(fileInfo, this.DefaultFileContentEncoding.GetBytes(text));
		}

		// Token: 0x04000257 RID: 599
		private readonly string m_replacement;

		// Token: 0x04000258 RID: 600
		private readonly Regex m_pattern;

		// Token: 0x04000259 RID: 601
		private readonly Encoding DefaultFileContentEncoding = Encoding.UTF8;
	}
}
