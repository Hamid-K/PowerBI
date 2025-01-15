using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications.Serialization;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000353 RID: 851
	public class InductiveConstraint : Spec
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x00037238 File Offset: 0x00035438
		public InductiveConstraint(IEnumerable<State> providedInputs, Func<State, object, bool> constraint)
			: this(providedInputs, (State i) => (object o) => constraint(i, o))
		{
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00037268 File Offset: 0x00035468
		public InductiveConstraint(IDictionary<State, Func<object, bool>> constraints)
			: this(constraints.Keys, (State i) => constraints[i])
		{
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003729F File Offset: 0x0003549F
		public InductiveConstraint(IEnumerable<State> providedInputs, Func<State, Func<object, bool>> constraint)
			: base(providedInputs, true)
		{
			this.Constraint = constraint;
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000372B0 File Offset: 0x000354B0
		public Func<State, Func<object, bool>> Constraint { get; }

		// Token: 0x060012D2 RID: 4818 RVA: 0x000372B8 File Offset: 0x000354B8
		protected override bool CorrectOnProvided(State state, object output)
		{
			return this.Constraint(state)(output);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000372CC File Offset: 0x000354CC
		protected override bool EqualsOnInput(State state, Spec other)
		{
			if (!(other is InductiveConstraint))
			{
				return false;
			}
			InductiveConstraint inductiveConstraint = (InductiveConstraint)other;
			return this.Constraint(state).Equals(inductiveConstraint.Constraint(state));
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00037307 File Offset: 0x00035507
		protected override int GetHashCodeOnInput(State state)
		{
			return this.Constraint(state).GetHashCode();
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003731A File Offset: 0x0003551A
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("Predicate", this.Constraint(input));
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00037337 File Offset: 0x00035537
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			throw new InvalidOperationException("We need an inverse transformer function to construct a transformed InductiveConstraint.");
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00037344 File Offset: 0x00035544
		internal InductiveConstraint TransformInputs(ConversionRule rule)
		{
			if (rule.IsTrivial)
			{
				return this;
			}
			Func<State, Func<object, bool>> constraint = this.Constraint;
			return new InductiveConstraint(base.ProvidedInputs.Select((State s) => rule.ApplySubstitutions(s, false)).ToList<State>(), (State state) => constraint(rule.ApplySubstitutions(state, true)));
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000373A6 File Offset: 0x000355A6
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			base.ThrowSerializationUnsupportedException();
			return null;
		}
	}
}
