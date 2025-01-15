using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200008D RID: 141
	public sealed class PhrasingLabel
	{
		// Token: 0x0600027D RID: 637 RVA: 0x00005DAF File Offset: 0x00003FAF
		public PhrasingLabel(string relationshipName, int phrasingIndex, string label, string contextualLabel, string proposedIdentifier, string phrasingID)
		{
			this.RelationshipName = relationshipName;
			this.PhrasingIndex = phrasingIndex;
			this.PhrasingID = phrasingID;
			this.Label = label;
			this.ContextualLabel = contextualLabel;
			this.ProposedIdentifier = proposedIdentifier;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public string RelationshipName { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00005DEC File Offset: 0x00003FEC
		public int PhrasingIndex { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public string PhrasingID { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00005DFC File Offset: 0x00003FFC
		public string Label { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00005E04 File Offset: 0x00004004
		public string ContextualLabel { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00005E0C File Offset: 0x0000400C
		public string ProposedIdentifier { get; }
	}
}
