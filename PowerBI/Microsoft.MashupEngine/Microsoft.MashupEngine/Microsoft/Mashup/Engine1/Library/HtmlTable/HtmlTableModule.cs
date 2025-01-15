using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.HtmlTable
{
	// Token: 0x02000277 RID: 631
	public sealed class HtmlTableModule : Module45
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00034DC5 File Offset: 0x00032FC5
		public override string Name
		{
			get
			{
				return "HtmlTable";
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00034DCC File Offset: 0x00032FCC
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
							return "Html.Table";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x040007B7 RID: 1975
		public const string HtmlTableName = "Html.Table";

		// Token: 0x040007B8 RID: 1976
		private Keys exportKeys;

		// Token: 0x02000278 RID: 632
		private enum Exports
		{
			// Token: 0x040007BA RID: 1978
			Html_Table,
			// Token: 0x040007BB RID: 1979
			Count
		}
	}
}
