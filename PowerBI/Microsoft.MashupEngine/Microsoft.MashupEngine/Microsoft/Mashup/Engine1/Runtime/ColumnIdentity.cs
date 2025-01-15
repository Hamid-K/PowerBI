using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001632 RID: 5682
	public class ColumnIdentity : IColumnIdentity
	{
		// Token: 0x06008F26 RID: 36646 RVA: 0x001DCF0A File Offset: 0x001DB10A
		public static string Escape(string identity)
		{
			return identity.Replace("{", "{{");
		}

		// Token: 0x06008F27 RID: 36647 RVA: 0x001DCF1C File Offset: 0x001DB11C
		public static ColumnIdentity New(string escapedIdentity, string name, int index)
		{
			return new ColumnIdentity(string.Concat(new string[]
			{
				escapedIdentity,
				".{",
				ColumnIdentity.Escape(name),
				",",
				index.ToString(),
				"}"
			}));
		}

		// Token: 0x06008F28 RID: 36648 RVA: 0x001DCF68 File Offset: 0x001DB168
		public ColumnIdentity(string identity)
		{
			this.identity = identity;
		}

		// Token: 0x17002583 RID: 9603
		// (get) Token: 0x06008F29 RID: 36649 RVA: 0x001DCF77 File Offset: 0x001DB177
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04004D79 RID: 19833
		private readonly string identity;
	}
}
