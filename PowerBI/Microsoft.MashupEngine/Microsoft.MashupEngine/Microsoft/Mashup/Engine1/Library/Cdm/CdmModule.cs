using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.Cdm
{
	// Token: 0x0200026B RID: 619
	public sealed class CdmModule : Module45
	{
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00034C0B File Offset: 0x00032E0B
		public override string Name
		{
			get
			{
				return "CdmView";
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x00034C12 File Offset: 0x00032E12
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Cdm.Contents";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x0400079B RID: 1947
		private const string CdmContents = "Cdm.Contents";

		// Token: 0x0400079C RID: 1948
		private Keys exportKeys;

		// Token: 0x0200026C RID: 620
		private enum Exports
		{
			// Token: 0x0400079E RID: 1950
			CdmContents,
			// Token: 0x0400079F RID: 1951
			Count
		}
	}
}
