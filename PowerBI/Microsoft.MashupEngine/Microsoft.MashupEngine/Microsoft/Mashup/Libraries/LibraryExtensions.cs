using System;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020D1 RID: 8401
	public static class LibraryExtensions
	{
		// Token: 0x0600CDD3 RID: 52691 RVA: 0x0028E478 File Offset: 0x0028C678
		public static string FullIdentifier(this ILibrary library)
		{
			return library.Provider.FullIdentifier(library.Identifier);
		}

		// Token: 0x0600CDD4 RID: 52692 RVA: 0x0028E48B File Offset: 0x0028C68B
		public static string FullIdentifier(this ILibraryProvider provider, string libraryIdentifier)
		{
			return provider.Identifier + ":" + libraryIdentifier;
		}
	}
}
