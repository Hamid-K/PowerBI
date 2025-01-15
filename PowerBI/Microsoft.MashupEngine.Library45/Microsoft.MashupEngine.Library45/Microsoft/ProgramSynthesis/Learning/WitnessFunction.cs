using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006CC RID: 1740
	[DataContract(IsReference = true)]
	public abstract class WitnessFunction : IComparable<WitnessFunction>
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000695EE File Offset: 0x000677EE
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x000695F6 File Offset: 0x000677F6
		[DataMember]
		public Type RuleSpecType { get; private set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000695FF File Offset: 0x000677FF
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x00069607 File Offset: 0x00067807
		[DataMember]
		public Type ReturnSpecType { get; private set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x00069610 File Offset: 0x00067810
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x00069618 File Offset: 0x00067818
		[DataMember]
		public Type RuleType { get; private set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00069621 File Offset: 0x00067821
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x00069629 File Offset: 0x00067829
		[DataMember]
		public WitnessFunction.PrereqInfo[] Prerequisites { get; private set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x00069632 File Offset: 0x00067832
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x0006963A File Offset: 0x0006783A
		[DataMember]
		public int ParameterIndex { get; private set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x00069643 File Offset: 0x00067843
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x0006964B File Offset: 0x0006784B
		[DataMember]
		public bool Verify { get; private set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00069654 File Offset: 0x00067854
		private MethodInfo Method
		{
			get
			{
				return this._originalMethod ?? this.DelegateAsMethodInfo;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060025DA RID: 9690
		protected abstract MethodInfo DelegateAsMethodInfo { get; }

		// Token: 0x060025DB RID: 9691
		public abstract Spec ConstructWitness(GrammarRule rule, Spec spec, params Spec[] prerequisites);

		// Token: 0x060025DC RID: 9692 RVA: 0x00069668 File Offset: 0x00067868
		public WitnessFunction(MethodInfo witnessFunction, WitnessFunctionAttribute attr, GrammarRule rule)
		{
			this.RuleType = rule.GetType();
			this.ReturnSpecType = witnessFunction.ReturnType;
			this.RuleSpecType = witnessFunction.GetParameters()[WitnessFunction.RuleSpecIndex].ParameterType;
			this.ParameterIndex = attr.ParameterIndex(rule);
			this.Prerequisites = this.ExtractPrereqs(attr.PrerequisiteIndexes(rule), witnessFunction);
			this.Verify = attr.Verify;
			this._originalMethod = witnessFunction;
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000696E0 File Offset: 0x000678E0
		private WitnessFunction.PrereqInfo[] ExtractPrereqs(int[] prerequisiteIndexes, MethodInfo witnessFunction)
		{
			if (prerequisiteIndexes.Length == 0)
			{
				return new WitnessFunction.PrereqInfo[0];
			}
			ParameterInfo[] parameters = witnessFunction.GetParameters();
			Type parameterType = parameters[parameters.Length - 1].ParameterType;
			if (parameterType.IsArray)
			{
				Type specType = parameterType.GetElementType();
				return prerequisiteIndexes.Select((int p) => new WitnessFunction.PrereqInfo(p, specType)).ToArray<WitnessFunction.PrereqInfo>();
			}
			IEnumerable<Type> enumerable = (from p in witnessFunction.GetParameters()
				select p.ParameterType).TakeLast(prerequisiteIndexes.Length);
			return prerequisiteIndexes.Zip(enumerable, (int p, Type t) => new WitnessFunction.PrereqInfo(p, t)).ToArray<WitnessFunction.PrereqInfo>();
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x0006979C File Offset: 0x0006799C
		private bool IsUserDefined
		{
			get
			{
				return !this.Method.DeclaringType.GetTypeInfo().Assembly.Equals(typeof(WitnessFunction).GetTypeInfo().Assembly) && !typeof(IWitnessingPlan).IsAssignableFrom(this.Method.DeclaringType);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000697F8 File Offset: 0x000679F8
		internal string Origin
		{
			get
			{
				return this.Method.DeclaringType.Name + "." + this.Method.Name;
			}
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00069820 File Offset: 0x00067A20
		public bool SatisfiedByPrerequisite(int prereqParam, Spec prereqSpec)
		{
			return this.Prerequisites.First((WitnessFunction.PrereqInfo q) => q.ParamIndex == prereqParam).SatisfiedBy(prereqSpec.GetType());
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0006985C File Offset: 0x00067A5C
		public int CompareTo(WitnessFunction other)
		{
			if (this.IsUserDefined && !other.IsUserDefined)
			{
				return -1;
			}
			if (other.IsUserDefined && !this.IsUserDefined)
			{
				return 1;
			}
			if (this.Verify && !other.Verify)
			{
				return 1;
			}
			if (other.Verify && !this.Verify)
			{
				return -1;
			}
			int num = MoreSpecificTypeComparer.Instance.Compare(this.RuleSpecType, other.RuleSpecType);
			if (num != 0)
			{
				return num;
			}
			num = MoreSpecificTypeComparer.Instance.Compare(this.RuleType, other.RuleType);
			if (num != 0)
			{
				return num;
			}
			return MoreSpecificTypeComparer.Instance.Compare(other.ReturnSpecType, this.ReturnSpecType);
		}

		// Token: 0x040011F3 RID: 4595
		private readonly MethodInfo _originalMethod;

		// Token: 0x040011F4 RID: 4596
		private static readonly int RuleSpecIndex = Array.FindIndex<ParameterInfo>(typeof(WitnessFunction.Static.Delegate).GetMethod("Invoke").GetParameters(), (ParameterInfo p) => typeof(Spec).IsAssignableFrom(p.ParameterType));

		// Token: 0x020006CD RID: 1741
		[DataContract]
		public sealed class PrereqInfo
		{
			// Token: 0x060025E3 RID: 9699 RVA: 0x00069934 File Offset: 0x00067B34
			public PrereqInfo(int paramIndex, Type specType)
			{
				this.SpecType = specType;
				this.ParamIndex = paramIndex;
			}

			// Token: 0x170006AF RID: 1711
			// (get) Token: 0x060025E4 RID: 9700 RVA: 0x0006994A File Offset: 0x00067B4A
			// (set) Token: 0x060025E5 RID: 9701 RVA: 0x00069952 File Offset: 0x00067B52
			[DataMember]
			public int ParamIndex { get; private set; }

			// Token: 0x170006B0 RID: 1712
			// (get) Token: 0x060025E6 RID: 9702 RVA: 0x0006995B File Offset: 0x00067B5B
			// (set) Token: 0x060025E7 RID: 9703 RVA: 0x00069963 File Offset: 0x00067B63
			[DataMember]
			public Type SpecType { get; private set; }

			// Token: 0x060025E8 RID: 9704 RVA: 0x0006996C File Offset: 0x00067B6C
			public bool SatisfiedBy(Type actualPrereqSpecType)
			{
				return this.SpecType.IsAssignableFrom(actualPrereqSpecType);
			}
		}

		// Token: 0x020006CE RID: 1742
		[DataContract]
		public sealed class Static : WitnessFunction
		{
			// Token: 0x170006B1 RID: 1713
			// (get) Token: 0x060025E9 RID: 9705 RVA: 0x0006997A File Offset: 0x00067B7A
			protected override MethodInfo DelegateAsMethodInfo
			{
				get
				{
					return this._delegate;
				}
			}

			// Token: 0x060025EA RID: 9706 RVA: 0x00069987 File Offset: 0x00067B87
			public Static(MethodInfo witnessFunction, WitnessFunctionAttribute attr, GrammarRule rule)
				: base(witnessFunction, attr, rule)
			{
				this._delegate = MethodReference.CreateWithParams<WitnessFunction.Static.Delegate>(witnessFunction, false);
			}

			// Token: 0x060025EB RID: 9707 RVA: 0x0006999F File Offset: 0x00067B9F
			public Static(WitnessFunction.Static.Delegate @delegate, MethodInfo witnessFunction, WitnessFunctionAttribute attr, GrammarRule rule)
				: base(witnessFunction, attr, rule)
			{
				this._delegate = MethodReference.Create<WitnessFunction.Static.Delegate>(witnessFunction, @delegate);
			}

			// Token: 0x060025EC RID: 9708 RVA: 0x000699B8 File Offset: 0x00067BB8
			public override Spec ConstructWitness(GrammarRule rule, Spec spec, params Spec[] prerequisites)
			{
				return this._delegate.Invoke(rule, spec, prerequisites);
			}

			// Token: 0x040011F7 RID: 4599
			[DataMember]
			private MethodReference<WitnessFunction.Static.Delegate> _delegate;

			// Token: 0x020006CF RID: 1743
			// (Invoke) Token: 0x060025EE RID: 9710
			public delegate Spec Delegate(GrammarRule rule, Spec spec, params Spec[] prerequisites);
		}

		// Token: 0x020006D0 RID: 1744
		public sealed class Instance : WitnessFunction
		{
			// Token: 0x170006B2 RID: 1714
			// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000699CD File Offset: 0x00067BCD
			protected override MethodInfo DelegateAsMethodInfo
			{
				get
				{
					return this._delegate;
				}
			}

			// Token: 0x060025F2 RID: 9714 RVA: 0x000699DA File Offset: 0x00067BDA
			public Instance(MethodInfo witnessFunction, WitnessFunctionAttribute attr, GrammarRule rule, DomainLearningLogic domainLogic)
				: base(witnessFunction, attr, rule)
			{
				this._domainLogic = domainLogic;
				this._delegate = MethodReference.CreateWithParams<WitnessFunction.Instance.Delegate>(witnessFunction, true);
			}

			// Token: 0x060025F3 RID: 9715 RVA: 0x000699FA File Offset: 0x00067BFA
			public override Spec ConstructWitness(GrammarRule rule, Spec spec, params Spec[] prerequisites)
			{
				return this._delegate.Invoke(this._domainLogic, rule, spec, prerequisites);
			}

			// Token: 0x040011F8 RID: 4600
			private readonly MethodReference<WitnessFunction.Instance.Delegate> _delegate;

			// Token: 0x040011F9 RID: 4601
			private readonly DomainLearningLogic _domainLogic;

			// Token: 0x020006D1 RID: 1745
			// (Invoke) Token: 0x060025F5 RID: 9717
			public delegate Spec Delegate(DomainLearningLogic logic, GrammarRule rule, Spec spec, params Spec[] prerequisites);
		}
	}
}
