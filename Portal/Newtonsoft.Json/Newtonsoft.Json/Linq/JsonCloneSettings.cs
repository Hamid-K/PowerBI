using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C3 RID: 195
	public class JsonCloneSettings
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x0002B27D File Offset: 0x0002947D
		public JsonCloneSettings()
		{
			this.CopyAnnotations = true;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002B28C File Offset: 0x0002948C
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0002B294 File Offset: 0x00029494
		public bool CopyAnnotations { get; set; }

		// Token: 0x0400038A RID: 906
		[Nullable(1)]
		internal static readonly JsonCloneSettings SkipCopyAnnotations = new JsonCloneSettings
		{
			CopyAnnotations = false
		};
	}
}
