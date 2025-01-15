using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing.Parsers;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200039D RID: 925
	public class EtwProvidersManifests
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0006C24F File Offset: 0x0006A44F
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x0006C257 File Offset: 0x0006A457
		[CLSCompliant(false)]
		public IEnumerable<ProviderManifest> ProviderManifests { get; private set; }

		// Token: 0x06001C77 RID: 7287 RVA: 0x0006C260 File Offset: 0x0006A460
		internal EtwProvidersManifests(IEnumerable<ProviderManifest> providerManifests)
		{
			this.ProviderManifests = providerManifests.Materialize<ProviderManifest>();
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0006C274 File Offset: 0x0006A474
		public EtwProvidersManifests(IEnumerable<string> manifests)
		{
			List<ProviderManifest> list = new List<ProviderManifest>();
			foreach (string text in manifests)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (StreamWriter streamWriter = new StreamWriter(memoryStream))
					{
						streamWriter.Write(text);
						streamWriter.Flush();
						memoryStream.Seek(0L, SeekOrigin.Begin);
						list.Add(new ProviderManifest(memoryStream, int.MaxValue));
					}
				}
			}
			this.ProviderManifests = list;
		}
	}
}
