using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B2 RID: 434
	[DataContract]
	[Serializable]
	public class ScrubbedBlobUri : ScrubbedUri
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x00027224 File Offset: 0x00025424
		public ScrubbedBlobUri(string uri)
			: base(uri)
		{
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002722D File Offset: 0x0002542D
		public ScrubbedBlobUri(Uri uri)
			: base(uri)
		{
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00027238 File Offset: 0x00025438
		protected override string ScrubPath()
		{
			string[] array = this.m_uri.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			string text = array.FirstOrDefault<string>();
			string[] array2 = array.Select((string subfolder) => subfolder.MarkAsPrivate()).ToArray<string>();
			if (text != null)
			{
				array2[0] = text;
			}
			return string.Join("/", array2);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000272A4 File Offset: 0x000254A4
		protected override string TagPath()
		{
			string[] array = this.m_uri.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			string text = array.FirstOrDefault<string>();
			string[] array2 = array.Select((string subfolder) => subfolder.MarkAsInternal()).ToArray<string>();
			if (text != null)
			{
				array2[0] = text;
			}
			return string.Join("/", array2);
		}
	}
}
