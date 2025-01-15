using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003A6 RID: 934
	[DataContract]
	[KnownType("GetStandardConcepts")]
	public abstract class ConceptRule : OperatorRule
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x0003D599 File Offset: 0x0003B799
		protected ConceptRule(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		private static IEnumerable<Type> GetStandardConcepts()
		{
			return from type in typeof(ConceptRule).GetTypeInfo().Assembly.GetTypes()
				where typeof(ConceptRule).IsAssignableFrom(type) && type.GetTypeInfo().GetCustomAttribute(true) != null
				select type;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0003D5E4 File Offset: 0x0003B7E4
		public IReadOnlyList<bool> IsChildLambdaInConceptAst
		{
			get
			{
				bool[] array;
				if ((array = this._isChildLambdaInConceptAst) == null)
				{
					array = (this._isChildLambdaInConceptAst = base.Body.Select((Symbol sym) => sym.LambdaRule != null).ToArray<bool>());
				}
				return array;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0003D634 File Offset: 0x0003B834
		private static Dictionary<string, ConceptInfo> Concepts
		{
			get
			{
				if (ConceptRule._concepts == null)
				{
					ConceptRule._concepts = (from type in ConceptRule.GetStandardConcepts()
						let typeInfo = type.GetTypeInfo()
						let attr = typeInfo.GetCustomAttribute(true)
						where attr != null && !typeInfo.IsAbstract
						select new ConceptInfo(attr.Name, attr.Lazy, type)).ToDictionary((ConceptInfo c) => c.Name);
				}
				return ConceptRule._concepts;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0003D70E File Offset: 0x0003B90E
		public static ImmutableHashSet<string> AllKnownConcepts
		{
			get
			{
				if (ConceptRule._allKnownConcepts == null)
				{
					ConceptRule._allKnownConcepts = new HashSet<string>(ConceptRule.Concepts.Keys).ToImmutableHashSet<string>();
				}
				return ConceptRule._allKnownConcepts;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0003D738 File Offset: 0x0003B938
		public ReadOnlyCollection<Symbol> DSLBody
		{
			get
			{
				List<Symbol> list;
				if ((list = this._dslBody) == null)
				{
					list = this.DslBodyMapping.Select((ConceptParameterUsage t) => t.DSLParameter).ToList<Symbol>();
				}
				this._dslBody = list;
				return this._dslBody.AsReadOnly();
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0003D790 File Offset: 0x0003B990
		internal static ConceptRule Create(string dslName, List<ConceptParameterUsage> dslBodyMapping, string conceptName, Symbol head, params Symbol[] parameters)
		{
			Type[] array = (from p in typeof(ConceptRule).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0].GetParameters()
				select p.ParameterType).ToArray<Type>();
			object[] array2 = new object[] { conceptName, head, parameters };
			ConceptInfo conceptInfo = ConceptRule.Concepts[conceptName];
			ConceptRule conceptRule = (ConceptRule)conceptInfo.Type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, array).Invoke(array2);
			conceptRule.Id = dslName;
			conceptRule.DslBodyMapping = dslBodyMapping;
			conceptRule.RecursionLimit = dslBodyMapping.Select((ConceptParameterUsage a) => a.RecursionLimit).ToArray<Optional<int>>();
			conceptRule.Lazy = conceptInfo.Lazy;
			return conceptRule;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600150F RID: 5391
		protected internal abstract Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature { get; }

		// Token: 0x06001510 RID: 5392 RVA: 0x0003D864 File Offset: 0x0003BA64
		internal override void ValidateSemantics(DiagnosticsContext diagnosticsContext)
		{
			ImmutableDictionary<string, Type> immutableDictionary = ImmutableDictionary.Create<string, Type>();
			foreach (Record<Symbol, ConceptRule.TypeParam> record in base.Body.ZipWith(this.Signature.Item1))
			{
				Symbol item = record.Item1;
				ConceptRule.TypeParam item2 = record.Item2;
				ImmutableDictionary<string, Type> immutableDictionary2 = item2.Unify(immutableDictionary, item.ResolvedType);
				if (immutableDictionary2 == null)
				{
					diagnosticsContext.AddDiagnostic(new Diagnostic.Semantics_IncompatibleConceptArgumentType(item.OriginLocation, new object[]
					{
						item.ResolvedType.CsName(true),
						item,
						base.Name,
						item2.Instantiate(immutableDictionary)
					}));
				}
				else
				{
					immutableDictionary = immutableDictionary2;
				}
			}
			if (this.Signature.Item2.Unify(immutableDictionary, base.ReturnResolvedType) == null)
			{
				diagnosticsContext.AddDiagnostic(new Diagnostic.Semantics_IncompatibleConceptResultType(base.Head.OriginLocation, new object[]
				{
					base.ReturnResolvedType.CsName(true),
					base.Head,
					base.Name,
					this.Signature.Item2.Instantiate(immutableDictionary)
				}));
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0003D98C File Offset: 0x0003BB8C
		public override TResult Accept<TResult, TArgs>(GrammarRuleVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitConceptRule(this, args);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0003D998 File Offset: 0x0003BB98
		public ProgramNode BuildDslASTFromConceptAST(ProgramNode program)
		{
			int[] array = new int[base.Body.Count];
			ProgramNode[] array2 = new ProgramNode[this.DSLBody.Count];
			for (int i = 0; i < this.DslBodyMapping.Count; i++)
			{
				ConceptParameterUsage conceptParameterUsage = this.DslBodyMapping[i];
				int conceptIndex = conceptParameterUsage.ConceptIndex;
				ProgramNode[] array3 = array2;
				int num = i;
				ProgramNode programNode;
				if (conceptParameterUsage.Usage != ParameterUsage.FillIn)
				{
					ProgramNode[] children = program.Children[conceptIndex].Children;
					int[] array4 = array;
					int num2 = conceptIndex;
					int num3 = array4[num2];
					array4[num2] = num3 + 1;
					programNode = children[num3];
				}
				else
				{
					programNode = program.Children[conceptIndex];
				}
				array3[num] = programNode;
			}
			return new NonterminalNode(program.Id, this, array2);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0003A97D File Offset: 0x00038B7D
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return NonterminalRule.GenericTopKStream(programSet, feature, k, fcc, logListener);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		public ProgramNode BuildConceptASTFromDslAST(params ProgramNode[] children)
		{
			ProgramNode[] array = new ProgramNode[base.Body.Count];
			int i;
			int i2;
			for (i = 0; i < base.Body.Count; i = i2 + 1)
			{
				int num = this.DslBodyMapping.FindIndex((ConceptParameterUsage m) => m.ConceptIndex == i && m.Usage == ParameterUsage.FillIn);
				if (num >= 0)
				{
					array[i] = children[num];
				}
				else
				{
					List<ProgramNode> list = new List<ProgramNode>();
					for (int j = 0; j < this.DslBodyMapping.Count; j++)
					{
						if (this.DslBodyMapping[j].ConceptIndex == i)
						{
							list.Add(children[j]);
						}
					}
					array[i] = base.Body[i].RHS.Single<GrammarRule>().BuildASTNode(list.ToArray());
				}
				i2 = i;
			}
			return new NonterminalNode(this, array);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0003DB3C File Offset: 0x0003BD3C
		internal int[] GetParameterPositionInConcept(int dslParameterIndex)
		{
			if (dslParameterIndex < 0 || dslParameterIndex >= base.Body.Count)
			{
				throw new ArgumentOutOfRangeException("dslParameterIndex");
			}
			int[] array = new int[base.Body.Count];
			for (int i = 0; i < dslParameterIndex; i++)
			{
				array[this.DslBodyMapping[i].ConceptIndex]++;
			}
			int conceptIndex = this.DslBodyMapping[dslParameterIndex].ConceptIndex;
			if (this.DslBodyMapping[dslParameterIndex].Usage == ParameterUsage.FillIn)
			{
				return new int[] { conceptIndex };
			}
			int[] array2 = new int[2];
			array2[0] = conceptIndex;
			int num = 1;
			int[] array3 = array;
			int num2 = conceptIndex;
			int num3 = array3[num2];
			array3[num2] = num3 + 1;
			array2[num] = num3;
			return array2;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0003DBF8 File Offset: 0x0003BDF8
		internal override Dictionary<object, ProgramSet> Cluster(JoinProgramSet space, State inputState)
		{
			bool[] isLambda = base.Body.Select((Symbol s) => s.LambdaRule != null).ToArray<bool>();
			if (!isLambda.Any<bool>())
			{
				return base.Cluster(space, inputState);
			}
			Dictionary<object, ProgramSet>[] parameterClusters = new Dictionary<object, ProgramSet>[base.Body.Count];
			ProgramNode[] array = base.Body.Select((Symbol p) => new Hole(p, null)).Cast<ProgramNode>().ToArray<ProgramNode>();
			for (int i = 0; i < array.Length; i++)
			{
				int iClosure2 = i;
				array[i].OnEvaluationFinished += delegate(object _, EvaluationFinishedEventArgs args)
				{
					parameterClusters[iClosure2] = (isLambda[iClosure2] ? null : space.ParameterSpaces[iClosure2].ClusterOnInput(args.Input));
				};
			}
			base.BuildASTNode(array).Invoke(inputState);
			IDictionary<int, Dictionary<object, ProgramSet>> dictionary = (from x in parameterClusters.Select((Dictionary<object, ProgramSet> c, int j) => new
				{
					Clusters = c,
					Index = j
				})
				where !isLambda[x.Index]
				select x).ToDictionary(x => x.Index, x => x.Clusters);
			MultiValueDictionary<object, ProgramSet> multiValueDictionary = new MultiValueDictionary<object, ProgramSet>(ValueEquality.Comparer);
			foreach (IImmutableDictionary<int, KeyValuePair<object, ProgramSet>> immutableDictionary in dictionary.CartesianProduct<int, Dictionary<object, ProgramSet>, KeyValuePair<object, ProgramSet>>())
			{
				MultiValueDictionary<int, State> statesForLambdaBodies = new MultiValueDictionary<int, State>();
				ProgramNode[] array2 = new ProgramNode[base.Body.Count];
				int num = 0;
				for (int l = 0; l < base.Body.Count; l++)
				{
					if (!isLambda[l])
					{
						array2[l] = new LiteralNode(base.Body[l], immutableDictionary[l].Key);
					}
					else
					{
						num++;
						LambdaRule lambdaRule = base.Body[l].LambdaRule;
						object @return = lambdaRule.LambdaBody.ResolvedType.CreateValidValue();
						CustomBehaviorHole customBehaviorHole = new CustomBehaviorHole(lambdaRule.LambdaBody, (State _) => @return);
						int iClosure = l;
						customBehaviorHole.OnEvaluationFinished += delegate(object _, EvaluationFinishedEventArgs args)
						{
							statesForLambdaBodies.Add(iClosure, args.Input);
						};
						array2[l] = lambdaRule.BuildASTNode(customBehaviorHole);
					}
				}
				object obj = base.BuildASTNode(array2).Invoke(inputState);
				if (statesForLambdaBodies.Count < num && obj is IEnumerable)
				{
					obj.ToEnumerable<object>().ToArray<object>();
				}
				Dictionary<int, Dictionary<object[], ProgramSet>> dictionary2 = new Dictionary<int, Dictionary<object[], ProgramSet>>();
				foreach (KeyValuePair<int, IReadOnlyCollection<State>> keyValuePair in statesForLambdaBodies)
				{
					int key = keyValuePair.Key;
					dictionary2[key] = ConceptRule.LambdaChildren(space.ParameterSpaces[key]).ClusterOnInputTuple(keyValuePair.Value);
				}
				foreach (IImmutableDictionary<int, KeyValuePair<object[], ProgramSet>> immutableDictionary2 in dictionary2.CartesianProduct<int, Dictionary<object[], ProgramSet>, KeyValuePair<object[], ProgramSet>>())
				{
					foreach (int num2 in immutableDictionary2.Keys)
					{
						LambdaRule lambdaRule2 = base.Body[num2].LambdaRule;
						Dictionary<State, object> @switch = new Dictionary<State, object>();
						foreach (Record<State, object> record in statesForLambdaBodies[num2].ZipWith(immutableDictionary2[num2].Key))
						{
							@switch[record.Item1] = record.Item2;
						}
						CustomBehaviorHole customBehaviorHole2 = new CustomBehaviorHole(lambdaRule2.LambdaBody, (State s) => @switch[s].BottomToNull());
						array2[num2] = lambdaRule2.BuildASTNode(customBehaviorHole2);
					}
					object obj2 = base.BuildASTNode(array2).Invoke(inputState);
					ProgramSet[] array3 = new ProgramSet[base.Body.Count];
					bool flag = true;
					for (int k = 0; k < base.Body.Count; k++)
					{
						if (!isLambda[k])
						{
							array3[k] = immutableDictionary[k].Value;
						}
						else
						{
							LambdaRule lambdaRule3 = base.Body[k].LambdaRule;
							KeyValuePair<object[], ProgramSet> keyValuePair2;
							Optional<ProgramSet> optional = (immutableDictionary2.TryGetValue(k, out keyValuePair2) ? keyValuePair2.Value.Some<ProgramSet>() : lambdaRule3.LambdaBody.TryGetAllPrograms(true, true));
							if (!optional.HasValue)
							{
								flag = false;
								break;
							}
							array3[k] = new JoinProgramSet(lambdaRule3, new ProgramSet[] { optional.Value });
						}
					}
					if (flag)
					{
						multiValueDictionary.Add(obj2.NullToBottom(), new JoinProgramSet(this, array3));
					}
				}
			}
			return multiValueDictionary.ToDictionary((KeyValuePair<object, IReadOnlyCollection<ProgramSet>> kvp) => kvp.Key, (KeyValuePair<object, IReadOnlyCollection<ProgramSet>> kvp) => kvp.Value.NormalizedUnion(), ValueEquality.Comparer);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0003E230 File Offset: 0x0003C430
		private static ProgramSet LambdaChildren(ProgramSet lambdaSet)
		{
			JoinProgramSet joinProgramSet = lambdaSet as JoinProgramSet;
			if (joinProgramSet != null)
			{
				return joinProgramSet.ParameterSpaces[0];
			}
			DirectProgramSet directProgramSet = lambdaSet as DirectProgramSet;
			if (directProgramSet != null)
			{
				return ProgramSet.List(lambdaSet.Symbol.LambdaRule.LambdaBody, directProgramSet.RealizedPrograms.Select((ProgramNode p) => p.Children[0]));
			}
			UnionProgramSet unionProgramSet = lambdaSet as UnionProgramSet;
			if (unionProgramSet == null)
			{
				return null;
			}
			IEnumerable<ProgramSet> unionSpaces = unionProgramSet.UnionSpaces;
			Func<ProgramSet, ProgramSet> func;
			if ((func = ConceptRule.<>O.<0>__LambdaChildren) == null)
			{
				func = (ConceptRule.<>O.<0>__LambdaChildren = new Func<ProgramSet, ProgramSet>(ConceptRule.LambdaChildren));
			}
			return unionSpaces.Select(func).NormalizedUnion();
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0003E2D0 File Offset: 0x0003C4D0
		protected override MethodReference<OperatorRule.OperatorSemantics> InitializeSemantics()
		{
			return MethodReference.WithoutReference<OperatorRule.OperatorSemantics>(new OperatorRule.OperatorSemantics(this.Evaluate));
		}

		// Token: 0x06001519 RID: 5401
		protected new abstract object Evaluate(object[] args);

		// Token: 0x04000A5A RID: 2650
		private static Dictionary<string, ConceptInfo> _concepts;

		// Token: 0x04000A5B RID: 2651
		private bool[] _isChildLambdaInConceptAst;

		// Token: 0x04000A5C RID: 2652
		private static ImmutableHashSet<string> _allKnownConcepts;

		// Token: 0x04000A5D RID: 2653
		[DataMember]
		internal List<ConceptParameterUsage> DslBodyMapping;

		// Token: 0x04000A5E RID: 2654
		[DataMember]
		private List<Symbol> _dslBody;

		// Token: 0x020003A7 RID: 935
		protected static class TP
		{
			// Token: 0x0600151A RID: 5402 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
			public static ConceptRule.TypeParam Generic(string name)
			{
				return new ConceptRule.TypeParam.Generic(name);
			}

			// Token: 0x0600151B RID: 5403 RVA: 0x0003E2EC File Offset: 0x0003C4EC
			public static ConceptRule.TypeParam Primitive(Type type)
			{
				return new ConceptRule.TypeParam.Primitive(type);
			}

			// Token: 0x0600151C RID: 5404 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
			public static ConceptRule.TypeParam Constructor(Type openGenericType, params ConceptRule.TypeParam[] typeParams)
			{
				return new ConceptRule.TypeParam.Constructor(openGenericType, typeParams);
			}
		}

		// Token: 0x020003A8 RID: 936
		protected internal abstract class TypeParam
		{
			// Token: 0x0600151D RID: 5405 RVA: 0x00002130 File Offset: 0x00000330
			private TypeParam()
			{
			}

			// Token: 0x0600151E RID: 5406
			public abstract ImmutableDictionary<string, Type> Unify(ImmutableDictionary<string, Type> context, Type type);

			// Token: 0x0600151F RID: 5407
			public abstract ConceptRule.TypeParam Instantiate(IDictionary<string, Type> context);

			// Token: 0x020003A9 RID: 937
			public sealed class Generic : ConceptRule.TypeParam
			{
				// Token: 0x06001520 RID: 5408 RVA: 0x0003E2FD File Offset: 0x0003C4FD
				public Generic(string name)
				{
					this._name = name;
				}

				// Token: 0x06001521 RID: 5409 RVA: 0x0003E30C File Offset: 0x0003C50C
				public override ImmutableDictionary<string, Type> Unify(ImmutableDictionary<string, Type> context, Type type)
				{
					Type type2;
					if (!context.TryGetValue(this._name, out type2))
					{
						return context.Add(this._name, type);
					}
					if (!type.IsConvertibleTo(type2))
					{
						return null;
					}
					if (!type2.IsAssignableFrom(type))
					{
						return context;
					}
					return context.SetItem(this._name, type);
				}

				// Token: 0x06001522 RID: 5410 RVA: 0x0003E35C File Offset: 0x0003C55C
				public override ConceptRule.TypeParam Instantiate(IDictionary<string, Type> context)
				{
					Type type;
					if (context.TryGetValue(this._name, out type))
					{
						return ConceptRule.TP.Primitive(type);
					}
					return this;
				}

				// Token: 0x06001523 RID: 5411 RVA: 0x0003E381 File Offset: 0x0003C581
				public override string ToString()
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("'{0}", new object[] { this._name }));
				}

				// Token: 0x04000A5F RID: 2655
				private readonly string _name;
			}

			// Token: 0x020003AA RID: 938
			public sealed class Primitive : ConceptRule.TypeParam
			{
				// Token: 0x06001524 RID: 5412 RVA: 0x0003E3A1 File Offset: 0x0003C5A1
				public Primitive(Type type)
				{
					this._type = type;
				}

				// Token: 0x06001525 RID: 5413 RVA: 0x0003E3B0 File Offset: 0x0003C5B0
				public override ImmutableDictionary<string, Type> Unify(ImmutableDictionary<string, Type> context, Type type)
				{
					if (!type.IsConvertibleTo(this._type))
					{
						return null;
					}
					return context;
				}

				// Token: 0x06001526 RID: 5414 RVA: 0x00004FAE File Offset: 0x000031AE
				public override ConceptRule.TypeParam Instantiate(IDictionary<string, Type> context)
				{
					return this;
				}

				// Token: 0x06001527 RID: 5415 RVA: 0x0003E3C3 File Offset: 0x0003C5C3
				public override string ToString()
				{
					return this._type.Name;
				}

				// Token: 0x04000A60 RID: 2656
				private readonly Type _type;
			}

			// Token: 0x020003AB RID: 939
			public sealed class Constructor : ConceptRule.TypeParam
			{
				// Token: 0x06001528 RID: 5416 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
				public Constructor(Type openGenericType, params ConceptRule.TypeParam[] typeParams)
				{
					this._openGenericType = openGenericType;
					this._typeParams = typeParams;
				}

				// Token: 0x06001529 RID: 5417 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
				public override ImmutableDictionary<string, Type> Unify(ImmutableDictionary<string, Type> context, Type type)
				{
					if (this._openGenericType == typeof(Func<, >) && typeof(IFunctionalSymbol1).IsAssignableFrom(type))
					{
						return context;
					}
					if (type.IsArray && this._openGenericType == typeof(IEnumerable<>))
					{
						return this.UnifyImpl(context, new Type[] { type.GetElementType() });
					}
					Type[] array = type.InheritsGeneric(this._openGenericType);
					if (array != null)
					{
						return this.UnifyImpl(context, array);
					}
					if (typeof(IEnumerable).IsAssignableFrom(type) && this._openGenericType == typeof(IEnumerable<>))
					{
						return this.UnifyImpl(context, new Type[] { typeof(object) });
					}
					return null;
				}

				// Token: 0x0600152A RID: 5418 RVA: 0x0003E4B4 File Offset: 0x0003C6B4
				private ImmutableDictionary<string, Type> UnifyImpl(ImmutableDictionary<string, Type> context, params Type[] genericArguments)
				{
					if (genericArguments.Length != this._typeParams.Length)
					{
						return null;
					}
					ImmutableDictionary<string, Type> immutableDictionary = context;
					foreach (Record<ConceptRule.TypeParam, Type> record in this._typeParams.ZipWith(genericArguments))
					{
						ImmutableDictionary<string, Type> immutableDictionary2 = record.Item1.Unify(immutableDictionary, record.Item2);
						if (immutableDictionary2 == null)
						{
							return null;
						}
						immutableDictionary = immutableDictionary2;
					}
					return immutableDictionary;
				}

				// Token: 0x0600152B RID: 5419 RVA: 0x0003E534 File Offset: 0x0003C734
				public override ConceptRule.TypeParam Instantiate(IDictionary<string, Type> context)
				{
					return ConceptRule.TP.Constructor(this._openGenericType, this._typeParams.Select((ConceptRule.TypeParam t) => t.Instantiate(context)).ToArray<ConceptRule.TypeParam>());
				}

				// Token: 0x0600152C RID: 5420 RVA: 0x0003E578 File Offset: 0x0003C778
				public override string ToString()
				{
					string text = string.Join<ConceptRule.TypeParam>(", ", this._typeParams);
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}<{1}>", new object[]
					{
						this._openGenericType.Name.TrimEnd(ConceptRule.TypeParam.Constructor.GenericChars),
						text
					}));
				}

				// Token: 0x04000A61 RID: 2657
				private readonly Type _openGenericType;

				// Token: 0x04000A62 RID: 2658
				private readonly ConceptRule.TypeParam[] _typeParams;

				// Token: 0x04000A63 RID: 2659
				private static readonly char[] GenericChars = "`0123456789".ToCharArray();
			}
		}

		// Token: 0x020003AD RID: 941
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000A65 RID: 2661
			public static Func<ProgramSet, ProgramSet> <0>__LambdaChildren;
		}
	}
}
