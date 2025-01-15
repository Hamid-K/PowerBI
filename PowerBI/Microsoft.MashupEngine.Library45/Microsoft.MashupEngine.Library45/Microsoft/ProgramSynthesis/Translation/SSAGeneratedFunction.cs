using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002EC RID: 748
	public abstract class SSAGeneratedFunction : GeneratedFunction
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0002E8CB File Offset: 0x0002CACB
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0002E8D3 File Offset: 0x0002CAD3
		public List<SSAStep> SSASequence { get; private set; }

		// Token: 0x06001028 RID: 4136 RVA: 0x0002E8DC File Offset: 0x0002CADC
		protected SSAGeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType, IEnumerable<SSAStep> ssaSequence = null)
			: base(parameters, returnType)
		{
			this.SSASequence = ((ssaSequence != null) ? ssaSequence.ToList<SSAStep>() : null) ?? new List<SSAStep>();
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0002E901 File Offset: 0x0002CB01
		public override void Optimize(IOptimizer optimizer)
		{
			base.Optimize(optimizer);
			this.SSASequence = optimizer.Optimize(this.SSASequence);
		}
	}
}
