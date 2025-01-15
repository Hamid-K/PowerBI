using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000AC RID: 172
	public class InputOutputs<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000DFB5 File Offset: 0x0000C1B5
		public TRegion Input { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000DFBD File Offset: 0x0000C1BD
		public string InputName { get; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000DFC5 File Offset: 0x0000C1C5
		public List<TRegion> Outputs { get; }

		// Token: 0x060003FC RID: 1020 RVA: 0x0000DFCD File Offset: 0x0000C1CD
		public InputOutputs(TRegion input, string inputName, IEnumerable<TRegion> outputs)
		{
			this.Input = input;
			this.InputName = inputName;
			this.Outputs = outputs.ToList<TRegion>();
		}
	}
}
