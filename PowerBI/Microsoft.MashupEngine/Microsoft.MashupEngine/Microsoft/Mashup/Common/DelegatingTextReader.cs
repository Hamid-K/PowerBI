using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE7 RID: 7143
	public abstract class DelegatingTextReader : TextReader
	{
		// Token: 0x0600B277 RID: 45687 RVA: 0x00245779 File Offset: 0x00243979
		protected DelegatingTextReader(TextReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600B278 RID: 45688 RVA: 0x00245788 File Offset: 0x00243988
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x0600B279 RID: 45689 RVA: 0x00245795 File Offset: 0x00243995
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.reader.Dispose();
			}
		}

		// Token: 0x0600B27A RID: 45690 RVA: 0x002457A5 File Offset: 0x002439A5
		public override int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x0600B27B RID: 45691 RVA: 0x002457B2 File Offset: 0x002439B2
		public override int Read()
		{
			return this.reader.Read();
		}

		// Token: 0x0600B27C RID: 45692 RVA: 0x002457BF File Offset: 0x002439BF
		public override int Read(char[] buffer, int index, int count)
		{
			return this.reader.Read(buffer, index, count);
		}

		// Token: 0x0600B27D RID: 45693 RVA: 0x002457CF File Offset: 0x002439CF
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			return this.reader.ReadBlock(buffer, index, count);
		}

		// Token: 0x0600B27E RID: 45694 RVA: 0x002457DF File Offset: 0x002439DF
		public override string ReadLine()
		{
			return this.reader.ReadLine();
		}

		// Token: 0x0600B27F RID: 45695 RVA: 0x002457EC File Offset: 0x002439EC
		public override string ReadToEnd()
		{
			return this.reader.ReadToEnd();
		}

		// Token: 0x04005B37 RID: 23351
		private readonly TextReader reader;
	}
}
