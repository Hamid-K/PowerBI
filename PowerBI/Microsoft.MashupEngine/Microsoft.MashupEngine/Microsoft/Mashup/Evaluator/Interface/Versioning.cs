using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E4C RID: 7756
	public static class Versioning
	{
		// Token: 0x0600BE95 RID: 48789 RVA: 0x002689B8 File Offset: 0x00266BB8
		public static bool TryParseVersion(string text, out Version version)
		{
			try
			{
				if (text != null)
				{
					string[] array = text.Split(new char[] { ' ', '-', '+' });
					version = new Version(array[0]);
					return true;
				}
			}
			catch (OverflowException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			version = null;
			return false;
		}

		// Token: 0x0600BE96 RID: 48790 RVA: 0x00268A28 File Offset: 0x00266C28
		public static VersionRange GetCoreDependency(this KeyValuePair<string, VersionRange>[] dependencies)
		{
			if (dependencies == null)
			{
				return null;
			}
			return dependencies.FirstOrDefault((KeyValuePair<string, VersionRange> d) => d.Key == "Core").Value;
		}
	}
}
