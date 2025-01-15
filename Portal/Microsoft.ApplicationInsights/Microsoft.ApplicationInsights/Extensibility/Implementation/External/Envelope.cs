using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B7 RID: 183
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class Envelope
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001658E File Offset: 0x0001478E
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00016596 File Offset: 0x00014796
		public int ver { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001659F File Offset: 0x0001479F
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x000165A7 File Offset: 0x000147A7
		public string name { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000165B0 File Offset: 0x000147B0
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x000165B8 File Offset: 0x000147B8
		public string time { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000165C1 File Offset: 0x000147C1
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x000165C9 File Offset: 0x000147C9
		public double sampleRate { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x000165D2 File Offset: 0x000147D2
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x000165DA File Offset: 0x000147DA
		public string seq { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000165E3 File Offset: 0x000147E3
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x000165EB File Offset: 0x000147EB
		public string iKey { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000165F4 File Offset: 0x000147F4
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000165FC File Offset: 0x000147FC
		public long flags { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00016605 File Offset: 0x00014805
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0001660D File Offset: 0x0001480D
		public IDictionary<string, string> tags { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00016616 File Offset: 0x00014816
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0001661E File Offset: 0x0001481E
		public Base data { get; set; }

		// Token: 0x060005C7 RID: 1479 RVA: 0x00016627 File Offset: 0x00014827
		public Envelope()
			: this("AI.Envelope", "Envelope")
		{
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001663C File Offset: 0x0001483C
		protected Envelope(string fullName, string name)
		{
			this.ver = 1;
			this.name = "";
			this.time = "";
			this.sampleRate = 100.0;
			this.seq = "";
			this.iKey = "";
			this.tags = new ConcurrentDictionary<string, string>();
		}
	}
}
