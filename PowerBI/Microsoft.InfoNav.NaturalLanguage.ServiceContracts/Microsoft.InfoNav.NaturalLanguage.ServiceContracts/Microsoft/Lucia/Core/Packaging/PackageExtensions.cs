using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;

namespace Microsoft.Lucia.Core.Packaging
{
	// Token: 0x02000172 RID: 370
	public static class PackageExtensions
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		public static IEnumerable<PackageRelationship> GetRelationshipsByType(this Package package, string relationshipType)
		{
			foreach (PackageRelationship packageRelationship in package.GetRelationshipsByType(relationshipType))
			{
				if (packageRelationship.TargetMode == TargetMode.Internal)
				{
					yield return packageRelationship;
				}
			}
			IEnumerator<PackageRelationship> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		public static bool TryGetRelationshipTargetUri(this Package package, string relationshipType, out Uri uri)
		{
			PackageRelationship packageRelationship = package.GetRelationshipsByType(relationshipType).FirstOrDefault<PackageRelationship>();
			if (packageRelationship != null)
			{
				uri = packageRelationship.TargetUri;
				return true;
			}
			uri = null;
			return false;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000C3E7 File Offset: 0x0000A5E7
		public static string GetItemNameFromContentItemUri(Uri uri)
		{
			string text = uri.ToString();
			return text.Substring(text.LastIndexOf('/') + 1);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000C3FE File Offset: 0x0000A5FE
		public static Uri CreatePartUri(string uri)
		{
			return PackUriHelper.CreatePartUri(new Uri(uri, UriKind.Relative));
		}
	}
}
