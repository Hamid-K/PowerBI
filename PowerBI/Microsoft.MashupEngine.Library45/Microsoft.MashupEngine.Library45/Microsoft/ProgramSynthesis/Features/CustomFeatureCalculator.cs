using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x02000797 RID: 1943
	[DataContract]
	public abstract class CustomFeatureCalculator : FeatureCalculator
	{
		// Token: 0x060029AE RID: 10670 RVA: 0x00076290 File Offset: 0x00074490
		[DebuggerNonUserCode]
		private static CustomFeatureCalculator ResolveImpl(FeatureInfo feature, MethodInfo method, FeatureCalculatorAttribute attr)
		{
			switch (attr.Method)
			{
			case CalculationMethod.FromChildrenFeatureValues:
				if (attr.SupportsLearningInfo)
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromChildrenFeatureValues.InstanceWithLearningInfo(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenFeatureValues.InstanceWithLearningInfo.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromChildrenFeatureValues.StaticWithLearningInfo(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenFeatureValues.StaticWithLearningInfo.Delegate>(method, false));
				}
				else
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromChildrenFeatureValues.Instance(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenFeatureValues.Instance.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromChildrenFeatureValues.Static(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenFeatureValues.Static.Delegate>(method, false));
				}
				break;
			case CalculationMethod.FromChildrenNodes:
				if (attr.SupportsLearningInfo)
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromChildrenNodes.InstanceWithLearningInfo(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenNodes.InstanceWithLearningInfo.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromChildrenNodes.StaticWithLearningInfo(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenNodes.StaticWithLearningInfo.Delegate>(method, false));
				}
				else
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromChildrenNodes.Instance(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenNodes.Instance.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromChildrenNodes.Static(feature, MethodReference.CreateWithParams<CustomFeatureCalculator.FromChildrenNodes.Static.Delegate>(method, false));
				}
				break;
			case CalculationMethod.FromLiteral:
				if (attr.SupportsLearningInfo)
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromLiteral.InstanceWithLearningInfo(feature, MethodReference.Create<CustomFeatureCalculator.FromLiteral.InstanceWithLearningInfo.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromLiteral.StaticWithLearningInfo(feature, MethodReference.Create<CustomFeatureCalculator.FromLiteral.StaticWithLearningInfo.Delegate>(method, false));
				}
				else
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromLiteral.Instance(feature, MethodReference.Create<CustomFeatureCalculator.FromLiteral.Instance.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromLiteral.Static(feature, MethodReference.Create<CustomFeatureCalculator.FromLiteral.Static.Delegate>(method, false));
				}
				break;
			case CalculationMethod.FromProgramNode:
				if (attr.SupportsLearningInfo)
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromProgramNode.InstanceWithLearningInfo(feature, MethodReference.Create<CustomFeatureCalculator.FromProgramNode.InstanceWithLearningInfo.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromProgramNode.StaticWithLearningInfo(feature, MethodReference.Create<CustomFeatureCalculator.FromProgramNode.StaticWithLearningInfo.Delegate>(method, false));
				}
				else
				{
					if (!method.IsStatic)
					{
						return new CustomFeatureCalculator.FromProgramNode.Instance(feature, MethodReference.Create<CustomFeatureCalculator.FromProgramNode.Instance.Delegate>(method, true));
					}
					return new CustomFeatureCalculator.FromProgramNode.Static(feature, MethodReference.Create<CustomFeatureCalculator.FromProgramNode.Static.Delegate>(method, false));
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("method");
			}
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00076409 File Offset: 0x00074609
		private CustomFeatureCalculator(FeatureInfo feature, bool supportsLearningInfo)
			: base(feature, supportsLearningInfo)
		{
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x00076413 File Offset: 0x00074613
		internal override MethodInfo Method
		{
			get
			{
				return this._method;
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x0007641C File Offset: 0x0007461C
		internal override void Validate(DiagnosticsContext diagnosticsContext)
		{
			if (!base.SupportsLearningInfo)
			{
				return;
			}
			if (!this._method.GetParameters()[0].ParameterType.IsConvertibleTo(typeof(LearningInfo)))
			{
				diagnosticsContext.AddDiagnostic(new Diagnostic.Features_NoLearningInfoParameter(this.Location, Array.Empty<object>()));
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x0007646B File Offset: 0x0007466B
		private Location.Assembly Location
		{
			get
			{
				return new Location.Assembly(this._method);
			}
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x0000CC37 File Offset: 0x0000AE37
		protected virtual void OnCalculateRequest(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
		{
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00076478 File Offset: 0x00074678
		public static CustomFeatureCalculator Resolve(FeatureInfo feature, MethodInfo method, GrammarRule expectedRule, FeatureCalculatorAttribute attribute)
		{
			attribute = attribute ?? method.GetCustomAttributes<FeatureCalculatorAttribute>().FirstOrDefault((FeatureCalculatorAttribute a) => a.RuleName == expectedRule.Id);
			attribute = attribute ?? new FeatureCalculatorAttribute(expectedRule.Id);
			CustomFeatureCalculator customFeatureCalculator = CustomFeatureCalculator.ResolveImpl(feature, method, attribute);
			customFeatureCalculator._method = method;
			return customFeatureCalculator;
		}

		// Token: 0x04001449 RID: 5193
		private MethodInfo _method;

		// Token: 0x02000798 RID: 1944
		public abstract class FromChildrenFeatureValues : CustomFeatureCalculator
		{
			// Token: 0x060029B5 RID: 10677 RVA: 0x000764D6 File Offset: 0x000746D6
			private FromChildrenFeatureValues(FeatureInfo feature, bool supportsLearningInfo)
				: base(feature, supportsLearningInfo)
			{
			}

			// Token: 0x060029B6 RID: 10678 RVA: 0x000764E0 File Offset: 0x000746E0
			internal override void Validate(DiagnosticsContext diagnosticsContext)
			{
				base.Validate(diagnosticsContext);
				int num = ((base.SupportsLearningInfo > false) ? 1 : 0);
				foreach (ParameterInfo parameterInfo in this._method.GetParameters().Skip(num))
				{
					if (!base.Feature.PropertyType.IsConvertibleTo(parameterInfo.ParameterType))
					{
						diagnosticsContext.AddDiagnostic(new Diagnostic.Features_ExpectedRecursiveCalculator(base.Location, new object[]
						{
							base.Feature,
							this._method.Name,
							parameterInfo.Name,
							parameterInfo.ParameterType,
							base.Feature.PropertyType.Name
						}));
					}
				}
			}

			// Token: 0x02000799 RID: 1945
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Static : CustomFeatureCalculator.FromChildrenFeatureValues
			{
				// Token: 0x1700074F RID: 1871
				// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000765B0 File Offset: 0x000747B0
				// (set) Token: 0x060029B8 RID: 10680 RVA: 0x000765B8 File Offset: 0x000747B8
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.Static.Delegate> Calculator { get; set; }

				// Token: 0x060029B9 RID: 10681 RVA: 0x000765C1 File Offset: 0x000747C1
				internal Static(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.Static.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029BA RID: 10682 RVA: 0x000765D4 File Offset: 0x000747D4
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(program.Children.Select(delegate(ProgramNode p, int idx)
					{
						IFeature instance2 = instance;
						LearningInfo learningInfo2 = learningInfo;
						return p.GetFeatureValue(instance2, (learningInfo2 != null) ? learningInfo2.ForChild(idx) : null);
					}).ToArray<object>());
				}

				// Token: 0x0200079A RID: 1946
				// (Invoke) Token: 0x060029BC RID: 10684
				public delegate object Delegate(params object[] childrenFeatures);
			}

			// Token: 0x0200079C RID: 1948
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Instance : CustomFeatureCalculator.FromChildrenFeatureValues
			{
				// Token: 0x17000750 RID: 1872
				// (get) Token: 0x060029C1 RID: 10689 RVA: 0x00076655 File Offset: 0x00074855
				// (set) Token: 0x060029C2 RID: 10690 RVA: 0x0007665D File Offset: 0x0007485D
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.Instance.Delegate> Calculator { get; set; }

				// Token: 0x060029C3 RID: 10691 RVA: 0x00076666 File Offset: 0x00074866
				internal Instance(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.Instance.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029C4 RID: 10692 RVA: 0x00076678 File Offset: 0x00074878
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(instance, program.Children.Select(delegate(ProgramNode p, int idx)
					{
						IFeature instance2 = instance;
						LearningInfo learningInfo2 = learningInfo;
						return p.GetFeatureValue(instance2, (learningInfo2 != null) ? learningInfo2.ForChild(idx) : null);
					}).ToArray<object>());
				}

				// Token: 0x0200079D RID: 1949
				// (Invoke) Token: 0x060029C6 RID: 10694
				public delegate object Delegate(IFeature instance, params object[] childrenFeatures);
			}

			// Token: 0x0200079F RID: 1951
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class StaticWithLearningInfo : CustomFeatureCalculator.FromChildrenFeatureValues
			{
				// Token: 0x17000751 RID: 1873
				// (get) Token: 0x060029CB RID: 10699 RVA: 0x000766FF File Offset: 0x000748FF
				// (set) Token: 0x060029CC RID: 10700 RVA: 0x00076707 File Offset: 0x00074907
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.StaticWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x060029CD RID: 10701 RVA: 0x00076710 File Offset: 0x00074910
				internal StaticWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.StaticWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029CE RID: 10702 RVA: 0x00076724 File Offset: 0x00074924
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(learningInfo, program.Children.Select(delegate(ProgramNode p, int idx)
					{
						IFeature instance2 = instance;
						LearningInfo learningInfo2 = learningInfo;
						return p.GetFeatureValue(instance2, (learningInfo2 != null) ? learningInfo2.ForChild(idx) : null);
					}).ToArray<object>());
				}

				// Token: 0x020007A0 RID: 1952
				// (Invoke) Token: 0x060029D0 RID: 10704
				public delegate object Delegate(LearningInfo learningInfo, params object[] childrenFeatures);
			}

			// Token: 0x020007A2 RID: 1954
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class InstanceWithLearningInfo : CustomFeatureCalculator.FromChildrenFeatureValues
			{
				// Token: 0x17000752 RID: 1874
				// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000767AB File Offset: 0x000749AB
				// (set) Token: 0x060029D6 RID: 10710 RVA: 0x000767B3 File Offset: 0x000749B3
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.InstanceWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x060029D7 RID: 10711 RVA: 0x000767BC File Offset: 0x000749BC
				internal InstanceWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenFeatureValues.InstanceWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029D8 RID: 10712 RVA: 0x000767D0 File Offset: 0x000749D0
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(instance, learningInfo, program.Children.Select(delegate(ProgramNode p, int idx)
					{
						IFeature instance2 = instance;
						LearningInfo learningInfo2 = learningInfo;
						return p.GetFeatureValue(instance2, (learningInfo2 != null) ? learningInfo2.ForChild(idx) : null);
					}).ToArray<object>());
				}

				// Token: 0x020007A3 RID: 1955
				// (Invoke) Token: 0x060029DA RID: 10714
				public delegate object Delegate(IFeature instance, LearningInfo learningInfo, params object[] childrenFeatures);
			}
		}

		// Token: 0x020007A5 RID: 1957
		public abstract class FromChildrenNodes : CustomFeatureCalculator
		{
			// Token: 0x060029DF RID: 10719 RVA: 0x000764D6 File Offset: 0x000746D6
			private FromChildrenNodes(FeatureInfo feature, bool supportsLearningInfo)
				: base(feature, supportsLearningInfo)
			{
			}

			// Token: 0x060029E0 RID: 10720 RVA: 0x00076860 File Offset: 0x00074A60
			internal override void Validate(DiagnosticsContext diagnosticsContext)
			{
				base.Validate(diagnosticsContext);
				int num = ((base.SupportsLearningInfo > false) ? 1 : 0);
				foreach (ParameterInfo parameterInfo in this._method.GetParameters().Skip(num))
				{
					if (!typeof(ProgramNode).IsAssignableFrom(parameterInfo.ParameterType))
					{
						diagnosticsContext.AddDiagnostic(new Diagnostic.Features_ExpectedNonterminalCalculator(base.Location, new object[]
						{
							this._method.Name,
							parameterInfo.Name,
							parameterInfo.ParameterType
						}));
					}
				}
			}

			// Token: 0x020007A6 RID: 1958
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Static : CustomFeatureCalculator.FromChildrenNodes
			{
				// Token: 0x17000753 RID: 1875
				// (get) Token: 0x060029E1 RID: 10721 RVA: 0x00076910 File Offset: 0x00074B10
				// (set) Token: 0x060029E2 RID: 10722 RVA: 0x00076918 File Offset: 0x00074B18
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenNodes.Static.Delegate> Calculator { get; set; }

				// Token: 0x060029E3 RID: 10723 RVA: 0x00076921 File Offset: 0x00074B21
				internal Static(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenNodes.Static.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029E4 RID: 10724 RVA: 0x00076932 File Offset: 0x00074B32
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(program.Children);
				}

				// Token: 0x020007A7 RID: 1959
				// (Invoke) Token: 0x060029E6 RID: 10726
				public delegate object Delegate(params ProgramNode[] children);
			}

			// Token: 0x020007A8 RID: 1960
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Instance : CustomFeatureCalculator.FromChildrenNodes
			{
				// Token: 0x17000754 RID: 1876
				// (get) Token: 0x060029E9 RID: 10729 RVA: 0x00076953 File Offset: 0x00074B53
				// (set) Token: 0x060029EA RID: 10730 RVA: 0x0007695B File Offset: 0x00074B5B
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenNodes.Instance.Delegate> Calculator { get; set; }

				// Token: 0x060029EB RID: 10731 RVA: 0x00076964 File Offset: 0x00074B64
				internal Instance(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenNodes.Instance.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029EC RID: 10732 RVA: 0x00076975 File Offset: 0x00074B75
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(instance, program.Children);
				}

				// Token: 0x020007A9 RID: 1961
				// (Invoke) Token: 0x060029EE RID: 10734
				public delegate object Delegate(IFeature instance, params ProgramNode[] children);
			}

			// Token: 0x020007AA RID: 1962
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class StaticWithLearningInfo : CustomFeatureCalculator.FromChildrenNodes
			{
				// Token: 0x17000755 RID: 1877
				// (get) Token: 0x060029F1 RID: 10737 RVA: 0x00076997 File Offset: 0x00074B97
				// (set) Token: 0x060029F2 RID: 10738 RVA: 0x0007699F File Offset: 0x00074B9F
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenNodes.StaticWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x060029F3 RID: 10739 RVA: 0x000769A8 File Offset: 0x00074BA8
				internal StaticWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenNodes.StaticWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029F4 RID: 10740 RVA: 0x000769B9 File Offset: 0x00074BB9
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(learningInfo, program.Children);
				}

				// Token: 0x020007AB RID: 1963
				// (Invoke) Token: 0x060029F6 RID: 10742
				public delegate object Delegate(LearningInfo learningInfo, params ProgramNode[] children);
			}

			// Token: 0x020007AC RID: 1964
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class InstanceWithLearningInfo : CustomFeatureCalculator.FromChildrenNodes
			{
				// Token: 0x17000756 RID: 1878
				// (get) Token: 0x060029F9 RID: 10745 RVA: 0x000769DB File Offset: 0x00074BDB
				// (set) Token: 0x060029FA RID: 10746 RVA: 0x000769E3 File Offset: 0x00074BE3
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromChildrenNodes.InstanceWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x060029FB RID: 10747 RVA: 0x000769EC File Offset: 0x00074BEC
				internal InstanceWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromChildrenNodes.InstanceWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x060029FC RID: 10748 RVA: 0x000769FD File Offset: 0x00074BFD
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(instance, learningInfo, program.Children);
				}

				// Token: 0x020007AD RID: 1965
				// (Invoke) Token: 0x060029FE RID: 10750
				public delegate object Delegate(IFeature instance, LearningInfo learningInfo, params ProgramNode[] children);
			}
		}

		// Token: 0x020007AE RID: 1966
		public abstract class FromLiteral : CustomFeatureCalculator
		{
			// Token: 0x06002A01 RID: 10753 RVA: 0x000764D6 File Offset: 0x000746D6
			private FromLiteral(FeatureInfo feature, bool supportsLearningInfo)
				: base(feature, supportsLearningInfo)
			{
			}

			// Token: 0x06002A02 RID: 10754 RVA: 0x00076A20 File Offset: 0x00074C20
			internal override void Validate(DiagnosticsContext diagnosticsContext)
			{
				base.Validate(diagnosticsContext);
				int num = ((true + (base.SupportsLearningInfo > false)) ? 1 : 0);
				if (this._method.GetParameters().Length != num)
				{
					diagnosticsContext.AddDiagnostic(new Diagnostic.Features_ExpectedLiteralCalculator(base.Location, new object[]
					{
						this._method.Name,
						this._method.GetParameters().Length,
						num
					}));
				}
			}

			// Token: 0x06002A03 RID: 10755 RVA: 0x00076A93 File Offset: 0x00074C93
			protected override void OnCalculateRequest(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
			{
				if (!(program is LiteralNode))
				{
					throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Tried to calculate feature {0} with a literal calculator for a non-literal node {1}", new object[] { base.Feature, program })));
				}
			}

			// Token: 0x020007AF RID: 1967
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Static : CustomFeatureCalculator.FromLiteral
			{
				// Token: 0x17000757 RID: 1879
				// (get) Token: 0x06002A04 RID: 10756 RVA: 0x00076AC5 File Offset: 0x00074CC5
				// (set) Token: 0x06002A05 RID: 10757 RVA: 0x00076ACD File Offset: 0x00074CCD
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromLiteral.Static.Delegate> Calculator { get; set; }

				// Token: 0x06002A06 RID: 10758 RVA: 0x00076AD6 File Offset: 0x00074CD6
				internal Static(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromLiteral.Static.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A07 RID: 10759 RVA: 0x00076AE7 File Offset: 0x00074CE7
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					CustomFeatureCalculator.FromLiteral.Static.Delegate invoke = this.Calculator.Invoke;
					LiteralNode literalNode = program as LiteralNode;
					return invoke((literalNode != null) ? literalNode.Value : null);
				}

				// Token: 0x020007B0 RID: 1968
				// (Invoke) Token: 0x06002A09 RID: 10761
				public delegate object Delegate(object literal);
			}

			// Token: 0x020007B1 RID: 1969
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Instance : CustomFeatureCalculator.FromLiteral
			{
				// Token: 0x17000758 RID: 1880
				// (get) Token: 0x06002A0C RID: 10764 RVA: 0x00076B14 File Offset: 0x00074D14
				// (set) Token: 0x06002A0D RID: 10765 RVA: 0x00076B1C File Offset: 0x00074D1C
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromLiteral.Instance.Delegate> Calculator { get; set; }

				// Token: 0x06002A0E RID: 10766 RVA: 0x00076B25 File Offset: 0x00074D25
				internal Instance(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromLiteral.Instance.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A0F RID: 10767 RVA: 0x00076B36 File Offset: 0x00074D36
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					CustomFeatureCalculator.FromLiteral.Instance.Delegate invoke = this.Calculator.Invoke;
					LiteralNode literalNode = program as LiteralNode;
					return invoke(instance, (literalNode != null) ? literalNode.Value : null);
				}

				// Token: 0x020007B2 RID: 1970
				// (Invoke) Token: 0x06002A11 RID: 10769
				public delegate object Delegate(IFeature instance, object literal);
			}

			// Token: 0x020007B3 RID: 1971
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class StaticWithLearningInfo : CustomFeatureCalculator.FromLiteral
			{
				// Token: 0x17000759 RID: 1881
				// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00076B64 File Offset: 0x00074D64
				// (set) Token: 0x06002A15 RID: 10773 RVA: 0x00076B6C File Offset: 0x00074D6C
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromLiteral.StaticWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x06002A16 RID: 10774 RVA: 0x00076B75 File Offset: 0x00074D75
				internal StaticWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromLiteral.StaticWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A17 RID: 10775 RVA: 0x00076B86 File Offset: 0x00074D86
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					CustomFeatureCalculator.FromLiteral.StaticWithLearningInfo.Delegate invoke = this.Calculator.Invoke;
					LiteralNode literalNode = program as LiteralNode;
					return invoke(learningInfo, (literalNode != null) ? literalNode.Value : null);
				}

				// Token: 0x020007B4 RID: 1972
				// (Invoke) Token: 0x06002A19 RID: 10777
				public delegate object Delegate(LearningInfo learningInfo, object literal);
			}

			// Token: 0x020007B5 RID: 1973
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class InstanceWithLearningInfo : CustomFeatureCalculator.FromLiteral
			{
				// Token: 0x1700075A RID: 1882
				// (get) Token: 0x06002A1C RID: 10780 RVA: 0x00076BB4 File Offset: 0x00074DB4
				// (set) Token: 0x06002A1D RID: 10781 RVA: 0x00076BBC File Offset: 0x00074DBC
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromLiteral.InstanceWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x06002A1E RID: 10782 RVA: 0x00076BC5 File Offset: 0x00074DC5
				internal InstanceWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromLiteral.InstanceWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A1F RID: 10783 RVA: 0x00076BD6 File Offset: 0x00074DD6
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					CustomFeatureCalculator.FromLiteral.InstanceWithLearningInfo.Delegate invoke = this.Calculator.Invoke;
					LiteralNode literalNode = program as LiteralNode;
					return invoke(instance, learningInfo, (literalNode != null) ? literalNode.Value : null);
				}

				// Token: 0x020007B6 RID: 1974
				// (Invoke) Token: 0x06002A21 RID: 10785
				public delegate object Delegate(IFeature instance, LearningInfo learningInfo, object literal);
			}
		}

		// Token: 0x020007B7 RID: 1975
		public abstract class FromProgramNode : CustomFeatureCalculator
		{
			// Token: 0x06002A24 RID: 10788 RVA: 0x000764D6 File Offset: 0x000746D6
			private FromProgramNode(FeatureInfo feature, bool supportsLearningInfo)
				: base(feature, supportsLearningInfo)
			{
			}

			// Token: 0x06002A25 RID: 10789 RVA: 0x00076C08 File Offset: 0x00074E08
			internal override void Validate(DiagnosticsContext diagnosticsContext)
			{
				base.Validate(diagnosticsContext);
				int num = ((base.SupportsLearningInfo > false) ? 1 : 0);
				foreach (ParameterInfo parameterInfo in this._method.GetParameters().Skip(num).Take(1))
				{
					if (!typeof(ProgramNode).IsAssignableFrom(parameterInfo.ParameterType))
					{
						diagnosticsContext.AddDiagnostic(new Diagnostic.Features_ExpectedNonterminalCalculator(base.Location, new object[]
						{
							this._method.Name,
							parameterInfo.Name,
							parameterInfo.ParameterType
						}));
					}
				}
			}

			// Token: 0x020007B8 RID: 1976
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Static : CustomFeatureCalculator.FromProgramNode
			{
				// Token: 0x1700075B RID: 1883
				// (get) Token: 0x06002A26 RID: 10790 RVA: 0x00076CC0 File Offset: 0x00074EC0
				// (set) Token: 0x06002A27 RID: 10791 RVA: 0x00076CC8 File Offset: 0x00074EC8
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromProgramNode.Static.Delegate> Calculator { get; set; }

				// Token: 0x06002A28 RID: 10792 RVA: 0x00076CD1 File Offset: 0x00074ED1
				internal Static(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromProgramNode.Static.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A29 RID: 10793 RVA: 0x00076CE2 File Offset: 0x00074EE2
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(program);
				}

				// Token: 0x020007B9 RID: 1977
				// (Invoke) Token: 0x06002A2B RID: 10795
				public delegate object Delegate(ProgramNode node);
			}

			// Token: 0x020007BA RID: 1978
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class Instance : CustomFeatureCalculator.FromProgramNode
			{
				// Token: 0x1700075C RID: 1884
				// (get) Token: 0x06002A2E RID: 10798 RVA: 0x00076CFE File Offset: 0x00074EFE
				// (set) Token: 0x06002A2F RID: 10799 RVA: 0x00076D06 File Offset: 0x00074F06
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromProgramNode.Instance.Delegate> Calculator { get; set; }

				// Token: 0x06002A30 RID: 10800 RVA: 0x00076D0F File Offset: 0x00074F0F
				internal Instance(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromProgramNode.Instance.Delegate> calculator)
					: base(feature, false)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A31 RID: 10801 RVA: 0x00076D20 File Offset: 0x00074F20
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(instance, program);
				}

				// Token: 0x020007BB RID: 1979
				// (Invoke) Token: 0x06002A33 RID: 10803
				public delegate object Delegate(IFeature instance, ProgramNode node);
			}

			// Token: 0x020007BC RID: 1980
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class StaticWithLearningInfo : CustomFeatureCalculator.FromProgramNode
			{
				// Token: 0x1700075D RID: 1885
				// (get) Token: 0x06002A36 RID: 10806 RVA: 0x00076D3D File Offset: 0x00074F3D
				// (set) Token: 0x06002A37 RID: 10807 RVA: 0x00076D45 File Offset: 0x00074F45
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromProgramNode.StaticWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x06002A38 RID: 10808 RVA: 0x00076D4E File Offset: 0x00074F4E
				internal StaticWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromProgramNode.StaticWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A39 RID: 10809 RVA: 0x00076D5F File Offset: 0x00074F5F
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(learningInfo, program);
				}

				// Token: 0x020007BD RID: 1981
				// (Invoke) Token: 0x06002A3B RID: 10811
				public delegate object Delegate(LearningInfo learningInfo, ProgramNode node);
			}

			// Token: 0x020007BE RID: 1982
			[DataContract]
			[DebuggerNonUserCode]
			public sealed class InstanceWithLearningInfo : CustomFeatureCalculator.FromProgramNode
			{
				// Token: 0x1700075E RID: 1886
				// (get) Token: 0x06002A3E RID: 10814 RVA: 0x00076D7C File Offset: 0x00074F7C
				// (set) Token: 0x06002A3F RID: 10815 RVA: 0x00076D84 File Offset: 0x00074F84
				[DataMember]
				private MethodReference<CustomFeatureCalculator.FromProgramNode.InstanceWithLearningInfo.Delegate> Calculator { get; set; }

				// Token: 0x06002A40 RID: 10816 RVA: 0x00076D8D File Offset: 0x00074F8D
				internal InstanceWithLearningInfo(FeatureInfo feature, MethodReference<CustomFeatureCalculator.FromProgramNode.InstanceWithLearningInfo.Delegate> calculator)
					: base(feature, true)
				{
					this.Calculator = calculator;
				}

				// Token: 0x06002A41 RID: 10817 RVA: 0x00076D9E File Offset: 0x00074F9E
				public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
				{
					this.OnCalculateRequest(program, learningInfo, instance);
					return this.Calculator.Invoke(instance, learningInfo, program);
				}

				// Token: 0x020007BF RID: 1983
				// (Invoke) Token: 0x06002A43 RID: 10819
				public delegate object Delegate(IFeature instance, LearningInfo learningInfo, ProgramNode node);
			}
		}
	}
}
