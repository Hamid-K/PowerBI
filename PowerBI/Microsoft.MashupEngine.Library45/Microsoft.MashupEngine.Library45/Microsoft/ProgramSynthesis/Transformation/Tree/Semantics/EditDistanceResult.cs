using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001ED6 RID: 7894
	internal class EditDistanceResult
	{
		// Token: 0x17002C10 RID: 11280
		// (get) Token: 0x06010A78 RID: 68216 RVA: 0x00395877 File Offset: 0x00393A77
		// (set) Token: 0x06010A79 RID: 68217 RVA: 0x0039587F File Offset: 0x00393A7F
		internal List<Edit> Edits { get; set; } = new List<Edit>();

		// Token: 0x17002C11 RID: 11281
		// (get) Token: 0x06010A7A RID: 68218 RVA: 0x00395888 File Offset: 0x00393A88
		// (set) Token: 0x06010A7B RID: 68219 RVA: 0x00395890 File Offset: 0x00393A90
		internal int Distance { get; set; }

		// Token: 0x17002C12 RID: 11282
		// (get) Token: 0x06010A7C RID: 68220 RVA: 0x00395899 File Offset: 0x00393A99
		// (set) Token: 0x06010A7D RID: 68221 RVA: 0x003958A1 File Offset: 0x00393AA1
		internal Dictionary<Node, Node> Mapping { get; set; } = new Dictionary<Node, Node>();
	}
}
