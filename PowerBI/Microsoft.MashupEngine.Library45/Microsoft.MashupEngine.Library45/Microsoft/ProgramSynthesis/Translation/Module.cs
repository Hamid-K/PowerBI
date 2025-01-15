using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002E3 RID: 739
	public abstract class Module
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
		public string Name { get; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0002E1B8 File Offset: 0x0002C3B8
		protected Dictionary<string, IGeneratedFunction> BoundFunctions { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0002E1C0 File Offset: 0x0002C3C0
		protected List<Record<string, IGeneratedFunction>> Functions { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0002E1C8 File Offset: 0x0002C3C8
		protected List<KeyValuePair<string, string>> AuxiliaryCode { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0002E1D0 File Offset: 0x0002C3D0
		private Dictionary<string, string> AuxiliaryCodeDictionary { get; }

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		protected Module(string name)
		{
			this.Name = name;
			this.BoundFunctions = new Dictionary<string, IGeneratedFunction>();
			this.Functions = new List<Record<string, IGeneratedFunction>>();
			this.AuxiliaryCode = new List<KeyValuePair<string, string>>();
			this.AuxiliaryCodeDictionary = new Dictionary<string, string>();
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0002E213 File Offset: 0x0002C413
		public virtual void Bind(string functionName, IGeneratedFunction function)
		{
			this.BoundFunctions[functionName] = function;
			this.Functions.Add(Record.Create<string, IGeneratedFunction>(functionName, function));
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002E234 File Offset: 0x0002C434
		public virtual void ClearBindings()
		{
			this.BoundFunctions.Clear();
			this.Functions.Clear();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0002E24C File Offset: 0x0002C44C
		public virtual void AppendAuxiliaryCode(string codeUnitName, string auxiliaryCode)
		{
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("{0} has not been implemented in type {1}", new object[]
			{
				"AppendAuxiliaryCode",
				base.GetType()
			})));
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0002E279 File Offset: 0x0002C479
		protected void _AppendAuxiliaryCodeImpl(string codeUnitName, string auxiliaryCode)
		{
			this.AuxiliaryCode.Add(new KeyValuePair<string, string>(codeUnitName, auxiliaryCode));
			this.AuxiliaryCodeDictionary[codeUnitName] = auxiliaryCode;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0002E29A File Offset: 0x0002C49A
		public void ClearAuxiliaryCode()
		{
			this.AuxiliaryCode.Clear();
			this.AuxiliaryCodeDictionary.Clear();
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002E2B2 File Offset: 0x0002C4B2
		public bool TryGetAuxiliaryCode(string name, out string code)
		{
			return this.AuxiliaryCodeDictionary.TryGetValue(name, out code);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002E213 File Offset: 0x0002C413
		protected internal void BindLambda(string lambdaName, IGeneratedFunction lambda)
		{
			this.BoundFunctions[lambdaName] = lambda;
			this.Functions.Add(Record.Create<string, IGeneratedFunction>(lambdaName, lambda));
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002E2C4 File Offset: 0x0002C4C4
		protected internal void Unbind(string fNameToRemove)
		{
			this.BoundFunctions.Remove(fNameToRemove);
			this.Functions.RemoveAll((Record<string, IGeneratedFunction> x) => x.Item1.Equals(fNameToRemove));
		}

		// Token: 0x06000FFF RID: 4095
		public abstract string GenerateCode(OptimizeFor optimization);

		// Token: 0x06001000 RID: 4096 RVA: 0x0002E308 File Offset: 0x0002C508
		public virtual SSARValue GenerateFunctionApplication(NonterminalNode node, string functionName, OptimizeFor optimization, ProgramNodeVisitor<SSAValue, OptimizeFor> childTranslationVisitor)
		{
			IReadOnlyList<SSAValue> readOnlyList = node.Children.Select((ProgramNode child) => child.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization)).ToList<SSAValue>();
			if (!functionName.StartsWith("operators."))
			{
				this.SetModuleUsesHeader();
			}
			return new SSAFunctionApplication(node.Rule.ReturnResolvedType, functionName, readOnlyList, false);
		}

		// Token: 0x06001001 RID: 4097
		public abstract string GenerateQualifiedName(string functionName);

		// Token: 0x06001002 RID: 4098 RVA: 0x0002E36D File Offset: 0x0002C56D
		public virtual string GenerateUnisolatedCode(OptimizeFor optimization)
		{
			return this.GenerateCode(optimization);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0002E376 File Offset: 0x0002C576
		public virtual string ResolveType(Type originalType)
		{
			throw new NotImplementedException("This should be used and overriden by module subclasses that are designed for stongly typed languages.");
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public virtual void AddImports(params string[] imports)
		{
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public virtual void SetModuleUsesHeader()
		{
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0002E384 File Offset: 0x0002C584
		public void Optimize(IModuleOptimizer optimizer)
		{
			List<string> list;
			foreach (KeyValuePair<string, IGeneratedFunction> keyValuePair in optimizer.Optimize(this.BoundFunctions, out list))
			{
				this.Unbind(keyValuePair.Key);
				this.Bind(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (string text in list)
			{
				this.Unbind(text);
			}
		}
	}
}
