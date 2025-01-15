using System;

namespace Microsoft.ReportingServices.Portal.ODataWebApi
{
	// Token: 0x02000006 RID: 6
	internal static class UrlParser
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000022E0 File Offset: 0x000004E0
		internal static bool TryGetNamespaceFromUri(Uri uri, out string nameSpace)
		{
			nameSpace = null;
			if (3 < uri.Segments.Length)
			{
				string text = uri.Segments[3];
				char[] array = new char[] { '/', 'v', 'V', '.' };
				int num;
				if (int.TryParse(text.Split(array, StringSplitOptions.RemoveEmptyEntries)[0], out num))
				{
					nameSpace = "V" + num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000038 RID: 56
		private const int VersionSegmentIndex = 3;
	}
}
