using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001164 RID: 4452
	internal static class DatabaseResource
	{
		// Token: 0x060074A3 RID: 29859 RVA: 0x0019062C File Offset: 0x0018E82C
		public static IResource New(string resourceKind, string server, string database = null)
		{
			return Resource.New(resourceKind, DatabaseResource.ToPath(server, database));
		}

		// Token: 0x060074A4 RID: 29860 RVA: 0x0019063B File Offset: 0x0018E83B
		public static string ToPath(string server, string database)
		{
			if (database == null)
			{
				return server;
			}
			return server + ";" + database;
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x00190650 File Offset: 0x0018E850
		public static bool TryParsePath(string path, out string server, out string database)
		{
			if (path != null && DatabaseResource.AreCharactersValid(path))
			{
				int num = path.IndexOf(';');
				if (num == -1)
				{
					server = path;
					database = null;
					return true;
				}
				if (path.IndexOf(';', num + 1) == -1)
				{
					server = path.Substring(0, num);
					database = path.Substring(num + 1);
					return true;
				}
			}
			server = null;
			database = null;
			return false;
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x001906A9 File Offset: 0x0018E8A9
		public static bool IsSubPath(string path, string candidateSubPath)
		{
			return candidateSubPath.StartsWith(path, StringComparison.Ordinal) && (candidateSubPath.Length == path.Length || DatabaseResource.OccursExactlyOnceAtIndex(candidateSubPath, ';', path.Length));
		}

		// Token: 0x060074A7 RID: 29863 RVA: 0x000E3341 File Offset: 0x000E1541
		private static bool OccursExactlyOnceAtIndex(string s, char c, int index)
		{
			return s.IndexOf(c) == index && s.IndexOf(c, index + 1) == -1;
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x001906D8 File Offset: 0x0018E8D8
		private static bool AreCharactersValid(string path)
		{
			for (int i = 0; i < path.Length; i++)
			{
				if (char.IsControl(path[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04004021 RID: 16417
		private const char InstanceDatabaseSeparator = ';';
	}
}
