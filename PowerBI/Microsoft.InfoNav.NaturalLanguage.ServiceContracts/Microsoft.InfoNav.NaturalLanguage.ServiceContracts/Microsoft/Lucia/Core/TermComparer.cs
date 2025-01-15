using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000112 RID: 274
	public sealed class TermComparer : IEqualityComparer<Term>
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x0000A5CE File Offset: 0x000087CE
		public void SetBindingComparer(IEqualityComparer<TermBinding> bindingComparer)
		{
			this._bindingComparer = bindingComparer;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public bool Equals(Term x, Term y)
		{
			bool? flag = Util.AreEqual<Term>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			return x.StartIndex == y.StartIndex && x.Length == y.Length && (this.Equals(x.Binding, y.Binding) && this.Equals(x.AlternateBindings, y.AlternateBindings)) && this.Equals(x.SuggestedReplacements, y.SuggestedReplacements);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000A658 File Offset: 0x00008858
		public int GetHashCode(Term obj)
		{
			return Hashing.CombineHash(obj.StartIndex, obj.Length, (obj.Binding == null) ? (-1) : this._bindingComparer.GetHashCode(obj.Binding.Binding), (obj.AlternateBindings == null) ? 0 : obj.AlternateBindings.Count, (obj.SuggestedReplacements == null) ? 0 : obj.SuggestedReplacements.Count);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000A6C4 File Offset: 0x000088C4
		private bool Equals(TermBindingContainer x, TermBindingContainer y)
		{
			bool? flag = Util.AreEqual<TermBindingContainer>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			return this._bindingComparer.Equals(x.Binding, y.Binding);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000A704 File Offset: 0x00008904
		private bool Equals(IList<TermBindingContainer> x, IList<TermBindingContainer> y)
		{
			bool? flag = Util.AreEqual<IList<TermBindingContainer>>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			if (x.Count != y.Count)
			{
				return false;
			}
			for (int i = 0; i < x.Count; i++)
			{
				if (!this.Equals(x[i], y[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040005C3 RID: 1475
		private IEqualityComparer<TermBinding> _bindingComparer;
	}
}
