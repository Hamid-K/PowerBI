using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002FF RID: 767
	public class ValueProp : IOptimizer
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0002FC87 File Offset: 0x0002DE87
		public static ValueProp Instance { get; } = new ValueProp();

		// Token: 0x060010B7 RID: 4279 RVA: 0x0002FC8E File Offset: 0x0002DE8E
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			return new ValueProp.InnerValueProp().Optimize(steps);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00002130 File Offset: 0x00000330
		private ValueProp()
		{
		}

		// Token: 0x02000300 RID: 768
		private class InnerValueProp
		{
			// Token: 0x060010BA RID: 4282 RVA: 0x0002FCA8 File Offset: 0x0002DEA8
			public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
			{
				LinkedList<SSAStep> linkedList = new LinkedList<SSAStep>(steps);
				foreach (LinkedListNode<SSAStep> linkedListNode in linkedList.Nodes<SSAStep>())
				{
					this.TryOptimize(linkedListNode);
				}
				foreach (LinkedListNode<SSAStep> linkedListNode2 in this.nodesToRemove)
				{
					linkedList.Remove(linkedListNode2);
				}
				return linkedList.ToList<SSAStep>();
			}

			// Token: 0x060010BB RID: 4283 RVA: 0x0002FD40 File Offset: 0x0002DF40
			private void TryOptimize(LinkedListNode<SSAStep> stepNode)
			{
				SSAStep value = stepNode.Value;
				SSARegister lvalue = value.LValue;
				SSARValue rvalue = value.RValue;
				if (this.TryConstantProp(lvalue, rvalue) || this.TryCopyProp(lvalue, rvalue) || this.TryCSE(lvalue, rvalue))
				{
					this.nodesToRemove.Add(stepNode);
				}
			}

			// Token: 0x060010BC RID: 4284 RVA: 0x0002FD8C File Offset: 0x0002DF8C
			private void PopulateCSETable(SSARegister lValue, SSARValue rValue)
			{
				KeyValuePair<SSARValue, SSARegister> keyValuePair = this.cseLookupTable.FirstOrDefault((KeyValuePair<SSARValue, SSARegister> x) => x.Key.Equals(rValue));
				if (!keyValuePair.Equals(default(KeyValuePair<SSARValue, SSARegister>)))
				{
					this.cseCandidates[lValue] = keyValuePair.Value;
					return;
				}
				this.cseLookupTable.Add(new KeyValuePair<SSARValue, SSARegister>(rValue, lValue));
			}

			// Token: 0x060010BD RID: 4285 RVA: 0x0002FE06 File Offset: 0x0002E006
			private bool TryConstantProp(SSARegister lValue, SSARValue rValue)
			{
				return this.ShouldConstantProp(rValue) && this.SubstituteValue(lValue, rValue);
			}

			// Token: 0x060010BE RID: 4286 RVA: 0x0002FE1B File Offset: 0x0002E01B
			private bool TryCopyProp(SSARegister lValue, SSARValue rValue)
			{
				return rValue is SSAVariable && this.SubstituteValue(lValue, rValue);
			}

			// Token: 0x060010BF RID: 4287 RVA: 0x0002FE30 File Offset: 0x0002E030
			private bool TryCSE(SSARegister lValue, SSARValue rValue)
			{
				this.PopulateCSETable(lValue, rValue);
				if (this.cseCandidates.ContainsKey(lValue))
				{
					SSARegister ssaregister = this.cseCandidates[lValue];
					return this.SubstituteRegister(lValue, ssaregister);
				}
				return false;
			}

			// Token: 0x060010C0 RID: 4288 RVA: 0x0002FE6C File Offset: 0x0002E06C
			private bool SubstituteValue(SSARegister lValue, SSARValue newValue)
			{
				bool flag = lValue.ImmediateDownLinks.Count > 0;
				foreach (SSAValue ssavalue in lValue.ImmediateDownLinks.ToList<SSAValue>())
				{
					((SSARValue)ssavalue).SubstituteInPlace(lValue, newValue);
				}
				return flag;
			}

			// Token: 0x060010C1 RID: 4289 RVA: 0x0002FED8 File Offset: 0x0002E0D8
			private bool SubstituteRegister(SSARegister lValue, SSARegister newRegister)
			{
				bool flag = lValue.ImmediateDownLinks.Count > 0;
				foreach (SSAValue ssavalue in lValue.ImmediateDownLinks.ToList<SSAValue>())
				{
					((SSARValue)ssavalue).SubstituteInPlace(lValue, newRegister);
				}
				return flag;
			}

			// Token: 0x060010C2 RID: 4290 RVA: 0x0002FF44 File Offset: 0x0002E144
			private bool ShouldConstantProp(SSARValue rValue)
			{
				SSALiteral ssaliteral = rValue as SSALiteral;
				return ssaliteral != null && (ssaliteral.ValueType.IsPrimitive || ssaliteral.ValueType == typeof(string) || ssaliteral.ValueType == typeof(decimal));
			}

			// Token: 0x04000815 RID: 2069
			private readonly List<KeyValuePair<SSARValue, SSARegister>> cseLookupTable = new List<KeyValuePair<SSARValue, SSARegister>>();

			// Token: 0x04000816 RID: 2070
			private readonly Dictionary<SSARegister, SSARegister> cseCandidates = new Dictionary<SSARegister, SSARegister>();

			// Token: 0x04000817 RID: 2071
			private readonly ISet<LinkedListNode<SSAStep>> nodesToRemove = new HashSet<LinkedListNode<SSAStep>>();
		}
	}
}
