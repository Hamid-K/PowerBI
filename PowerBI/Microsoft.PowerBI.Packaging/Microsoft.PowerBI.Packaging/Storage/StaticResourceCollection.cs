using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000054 RID: 84
	public sealed class StaticResourceCollection : IEnumerable<KeyValuePair<string, byte[]>>, IEnumerable
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00007D5B File Offset: 0x00005F5B
		public void RemoveResource(string qualifiedPath)
		{
			this.staticResources.Remove(qualifiedPath);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007D6A File Offset: 0x00005F6A
		public void AddResource(string qualifiedPath, byte[] content)
		{
			this.staticResources[qualifiedPath] = content;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007D79 File Offset: 0x00005F79
		public void AddResource(string path, string uniqueFileName, byte[] content)
		{
			this.AddResource(StaticResourceCollection.MakeQualifiedPath(path, uniqueFileName), content);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007D89 File Offset: 0x00005F89
		public bool TryGetValue(string path, string fileName, out byte[] content)
		{
			return this.staticResources.TryGetValue(StaticResourceCollection.MakeQualifiedPath(path, fileName), out content);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007D9E File Offset: 0x00005F9E
		private static string MakeQualifiedPath(string path, string fileName)
		{
			return path + "/" + fileName;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007DAC File Offset: 0x00005FAC
		public IEnumerator<KeyValuePair<string, byte[]>> GetEnumerator()
		{
			return this.staticResources.GetEnumerator();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007DBE File Offset: 0x00005FBE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.staticResources.Values).GetEnumerator();
		}

		// Token: 0x04000159 RID: 345
		private readonly Dictionary<string, byte[]> staticResources = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
	}
}
