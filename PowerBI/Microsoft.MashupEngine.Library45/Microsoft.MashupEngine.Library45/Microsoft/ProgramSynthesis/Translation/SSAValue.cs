using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002F1 RID: 753
	public abstract class SSAValue : IEquatable<SSAValue>
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0002EDA7 File Offset: 0x0002CFA7
		public Type ValueType { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0002EDAF File Offset: 0x0002CFAF
		internal HashSet<SSAValue> ImmediateDownLinks { get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0002EDB7 File Offset: 0x0002CFB7
		internal HashSet<SSAValue> ImmediateUpLinks { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0002EDBF File Offset: 0x0002CFBF
		public IEnumerable<SSAValue> AllUsers
		{
			get
			{
				return this.ImmediateDownLinks.Concat(this.ImmediateDownLinks.SelectMany((SSAValue user) => user.AllUsers));
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0002EDF6 File Offset: 0x0002CFF6
		public virtual IEnumerable<SSAValue> AllDependencies
		{
			get
			{
				return this.ImmediateUpLinks.Concat(this.ImmediateUpLinks.SelectMany((SSAValue dep) => dep.AllDependencies));
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002EE2D File Offset: 0x0002D02D
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x0002EE35 File Offset: 0x0002D035
		public SSAStep StepWhereDefined { get; internal set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0000A5FD File Offset: 0x000087FD
		private bool IsPure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0002EE3E File Offset: 0x0002D03E
		internal bool IsDeadCode
		{
			get
			{
				return this.ImmediateDownLinks.Count == 0 && this.IsPure;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0002EE55 File Offset: 0x0002D055
		protected SSAValue(Type valueType)
		{
			this.ValueType = valueType;
			this.ImmediateDownLinks = new HashSet<SSAValue>(IdentityEquality.Comparer);
			this.ImmediateUpLinks = new HashSet<SSAValue>(IdentityEquality.Comparer);
		}

		// Token: 0x06001057 RID: 4183
		public abstract bool Equals(SSAValue other);

		// Token: 0x06001058 RID: 4184 RVA: 0x0002EE84 File Offset: 0x0002D084
		public override bool Equals(object other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals(other as SSAValue)));
		}

		// Token: 0x06001059 RID: 4185
		public abstract override int GetHashCode();

		// Token: 0x0600105A RID: 4186
		public abstract override string ToString();
	}
}
