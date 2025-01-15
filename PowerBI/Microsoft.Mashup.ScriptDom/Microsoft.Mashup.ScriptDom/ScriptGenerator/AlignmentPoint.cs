using System;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x0200018E RID: 398
	internal class AlignmentPoint
	{
		// Token: 0x06002162 RID: 8546 RVA: 0x0015DDB2 File Offset: 0x0015BFB2
		public AlignmentPoint()
			: this(null)
		{
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0015DDBB File Offset: 0x0015BFBB
		public AlignmentPoint(string name)
		{
			this._name = name;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0015DDCA File Offset: 0x0015BFCA
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x040019A4 RID: 6564
		private string _name;
	}
}
