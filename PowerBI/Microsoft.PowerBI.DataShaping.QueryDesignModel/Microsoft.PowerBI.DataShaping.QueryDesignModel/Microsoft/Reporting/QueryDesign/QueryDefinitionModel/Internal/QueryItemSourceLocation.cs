using System;
using Microsoft.InfoNav.Utils;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000113 RID: 275
	public sealed class QueryItemSourceLocation
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0002BE59 File Offset: 0x0002A059
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x0002BE61 File Offset: 0x0002A061
		internal IContainsTelemetryMarkup ItemSource { get; private set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0002BE6A File Offset: 0x0002A06A
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x0002BE72 File Offset: 0x0002A072
		public ItemSourceType SourceType { get; private set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0002BE7B File Offset: 0x0002A07B
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x0002BE83 File Offset: 0x0002A083
		public int WrapperLineStart { get; internal set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0002BE8C File Offset: 0x0002A08C
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x0002BE94 File Offset: 0x0002A094
		public int SourceLineStart { get; private set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0002BE9D File Offset: 0x0002A09D
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x0002BEA5 File Offset: 0x0002A0A5
		public int SourceLineEnd { get; private set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0002BEAE File Offset: 0x0002A0AE
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0002BEB6 File Offset: 0x0002A0B6
		public int WrapperLineEnd { get; internal set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0002BEBF File Offset: 0x0002A0BF
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0002BEC7 File Offset: 0x0002A0C7
		public int SourceIndent { get; private set; }

		// Token: 0x06000FDF RID: 4063 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		internal QueryItemSourceLocation(IContainsTelemetryMarkup itemSource, int wrapperLineStart, int sourceLineStart, int sourceLineEnd, int wrapperLineEnd, ItemSourceType sourceType)
		{
			this.ItemSource = itemSource;
			this.SourceType = sourceType;
			this.WrapperLineStart = wrapperLineStart;
			this.SourceLineStart = sourceLineStart;
			this.SourceLineEnd = sourceLineEnd;
			this.WrapperLineEnd = wrapperLineEnd;
			this.SourceIndent = 0;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0002BF0C File Offset: 0x0002A10C
		public void AddLineNumberOffset(int offset)
		{
			this.WrapperLineStart += offset;
			this.SourceLineStart += offset;
			this.SourceLineEnd += offset;
			this.WrapperLineEnd += offset;
		}
	}
}
