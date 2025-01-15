using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DAB RID: 7595
	internal class PartitionedCode
	{
		// Token: 0x0600FEC4 RID: 65220 RVA: 0x00366CE8 File Offset: 0x00364EE8
		public PartitionedCode(SSAValue expr = null, IEnumerable<SSAStep> local = null, IEnumerable<string> imports = null, IEnumerable<SSAValue> inputs = null)
		{
			this.Expr = expr;
			this.Local = ((local != null) ? local.ToList<SSAStep>() : null) ?? new List<SSAStep>();
			this.Context = new Dictionary<string, PartitionedCode.GeneratedFunctionWithVersion>();
			this.Imports = new SortedSet<string>(imports ?? Enumerable.Empty<string>());
			this.InputRegisters = new HashSet<SSARegister>(((inputs != null) ? inputs.OfType<SSARegister>() : null) ?? Enumerable.Empty<SSARegister>());
		}

		// Token: 0x17002A6D RID: 10861
		// (get) Token: 0x0600FEC5 RID: 65221 RVA: 0x00366D5F File Offset: 0x00364F5F
		public SortedSet<string> Imports { get; }

		// Token: 0x17002A6E RID: 10862
		// (get) Token: 0x0600FEC6 RID: 65222 RVA: 0x00366D67 File Offset: 0x00364F67
		// (set) Token: 0x0600FEC7 RID: 65223 RVA: 0x00366D6F File Offset: 0x00364F6F
		public SSAValue Expr { get; private set; }

		// Token: 0x17002A6F RID: 10863
		// (get) Token: 0x0600FEC8 RID: 65224 RVA: 0x00366D78 File Offset: 0x00364F78
		// (set) Token: 0x0600FEC9 RID: 65225 RVA: 0x00366D80 File Offset: 0x00364F80
		public List<SSAStep> Local { get; private set; }

		// Token: 0x17002A70 RID: 10864
		// (get) Token: 0x0600FECA RID: 65226 RVA: 0x00366D89 File Offset: 0x00364F89
		// (set) Token: 0x0600FECB RID: 65227 RVA: 0x00366D91 File Offset: 0x00364F91
		public Dictionary<string, PartitionedCode.GeneratedFunctionWithVersion> Context { get; private set; }

		// Token: 0x17002A71 RID: 10865
		// (get) Token: 0x0600FECC RID: 65228 RVA: 0x00366D9A File Offset: 0x00364F9A
		// (set) Token: 0x0600FECD RID: 65229 RVA: 0x00366DA2 File Offset: 0x00364FA2
		public HashSet<SSARegister> InputRegisters { get; private set; }

		// Token: 0x0600FECE RID: 65230 RVA: 0x00366DAB File Offset: 0x00364FAB
		public void AddImport(string name)
		{
			this.Imports.Add(name);
		}

		// Token: 0x0600FECF RID: 65231 RVA: 0x00366DBC File Offset: 0x00364FBC
		public void AddContext(string fName, PartitionedCode code, out SSAValue funCall)
		{
			this.Imports.UnionWith(code.Imports);
			foreach (KeyValuePair<string, PartitionedCode.GeneratedFunctionWithVersion> keyValuePair in code.Context)
			{
				this.AddContextIfNew(keyValuePair.Key, keyValuePair.Value);
			}
			string funName = this.GetNewFuncName(fName);
			this.Context[funName] = new PartitionedCode.GeneratedFunctionWithVersion(() => code.ConvertToSSAGeneratedFunction(funName), 0);
			funCall = new SSAFunctionApplication(code.Expr.ValueType, funName, code.InputRegisters.Select((SSARegister x) => new SSARegister(x.Name, null, null)), true);
		}

		// Token: 0x0600FED0 RID: 65232 RVA: 0x00366EC4 File Offset: 0x003650C4
		public void AddContextIfNew(string funName, Func<IGeneratedFunction> func, int version = 0)
		{
			PartitionedCode.GeneratedFunctionWithVersion generatedFunctionWithVersion;
			if (!this.Context.TryGetValue(funName, out generatedFunctionWithVersion) || generatedFunctionWithVersion.Version < version)
			{
				this.Context[funName] = new PartitionedCode.GeneratedFunctionWithVersion(func, version);
			}
		}

		// Token: 0x0600FED1 RID: 65233 RVA: 0x00366EFD File Offset: 0x003650FD
		private void AddContextIfNew(string funName, PartitionedCode.GeneratedFunctionWithVersion func)
		{
			this.AddContextIfNew(funName, func.FuncGenerator, func.Version);
		}

		// Token: 0x0600FED2 RID: 65234 RVA: 0x00366F12 File Offset: 0x00365112
		public IEnumerable<Record<string, IGeneratedFunction>> GetFunctionBindings()
		{
			return this.Context.Select((KeyValuePair<string, PartitionedCode.GeneratedFunctionWithVersion> x) => Record.Create<string, IGeneratedFunction>(x.Key, x.Value.FuncGenerator()));
		}

		// Token: 0x0600FED3 RID: 65235 RVA: 0x00366F40 File Offset: 0x00365140
		private SSAGeneratedFunction ConvertToSSAGeneratedFunction(string funName)
		{
			IEnumerable<Record<string, Type>> enumerable = this.InputRegisters.Select((SSARegister x) => Record.Create<string, Type>(x.GetName(), x.ValueType));
			Type valueType = this.Expr.ValueType;
			SSARValue ssarvalue = this.Expr as SSARValue;
			if (ssarvalue != null)
			{
				this.Local.Add(new SSAStep(new SSARegister(null, valueType, "ret_val"), ssarvalue, ""));
			}
			return new PythonGeneratedFunction(enumerable, valueType, this.Local);
		}

		// Token: 0x0600FED4 RID: 65236 RVA: 0x00366FC0 File Offset: 0x003651C0
		public void SetExpr(SSAValue newExpr)
		{
			this.Expr = newExpr;
		}

		// Token: 0x0600FED5 RID: 65237 RVA: 0x00366FCC File Offset: 0x003651CC
		public void Merge(PartitionedCode other)
		{
			this.Imports.UnionWith(other.Imports);
			foreach (KeyValuePair<string, PartitionedCode.GeneratedFunctionWithVersion> keyValuePair in other.Context)
			{
				if (!this.Context.ContainsKey(keyValuePair.Key))
				{
					this.Context[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			this.Local.AddRange(other.Local);
			this.InputRegisters.UnionWith(other.InputRegisters);
		}

		// Token: 0x0600FED6 RID: 65238 RVA: 0x00367078 File Offset: 0x00365278
		public void LocalAddLine(SSAStep line)
		{
			this.Local.Add(line);
		}

		// Token: 0x0600FED7 RID: 65239 RVA: 0x00367088 File Offset: 0x00365288
		public SSARegister IntroduceNewVarIf(SSARegister var)
		{
			SSARValue ssarvalue = this.Expr as SSARValue;
			if (ssarvalue != null)
			{
				this.Local.Add(new SSAStep(var, ssarvalue, ""));
				this.SetExpr(var);
				return var;
			}
			return this.Expr as SSARegister;
		}

		// Token: 0x0600FED8 RID: 65240 RVA: 0x003670D0 File Offset: 0x003652D0
		public string GetNewFuncName(string name)
		{
			int num = 0;
			while (this.Context.ContainsKey(name))
			{
				name = FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}", new object[]
				{
					name,
					num++
				}));
			}
			return name;
		}

		// Token: 0x0600FED9 RID: 65241 RVA: 0x00367118 File Offset: 0x00365318
		public Record<string, CodeForGeneratedFunction> ToCodeBuilder()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			CodeBuilder codeBuilder2 = new CodeBuilder(4U);
			foreach (string text in this.Imports)
			{
				codeBuilder.AppendLine("import " + text);
			}
			foreach (KeyValuePair<string, PartitionedCode.GeneratedFunctionWithVersion> keyValuePair in this.Context)
			{
				IGeneratedFunction generatedFunction = keyValuePair.Value.FuncGenerator();
				CodeForGeneratedFunction codeForGeneratedFunction = generatedFunction.GenerateCode(string.Empty, OptimizeFor.Readability);
				codeBuilder.Append(codeForGeneratedFunction.StaticCode);
				string text2 = string.Join(", ", generatedFunction.Parameters.Select((Record<string, Type> r) => r.Item1));
				using (codeBuilder.NewScope(string.Concat(new string[] { "def ", keyValuePair.Key, "(", text2, "):" }), 1U))
				{
					codeBuilder.Append(codeForGeneratedFunction.DynamicCode);
				}
			}
			foreach (SSAStep ssastep in this.Local)
			{
				codeBuilder2.AppendLine(ssastep.LValue.ToPython("", null, true) + " = " + ssastep.RValue.ToPython("", null, true));
			}
			return Record.Create<string, CodeForGeneratedFunction>(this.Expr.ToPython("", null, true), new CodeForGeneratedFunction(codeBuilder, codeBuilder2));
		}

		// Token: 0x02001DAC RID: 7596
		public struct GeneratedFunctionWithVersion
		{
			// Token: 0x0600FEDA RID: 65242 RVA: 0x00367320 File Offset: 0x00365520
			public GeneratedFunctionWithVersion(Func<IGeneratedFunction> funcGenerator, int version = 0)
			{
				this.FuncGenerator = funcGenerator;
				this.Version = version;
			}

			// Token: 0x04005F95 RID: 24469
			public int Version;

			// Token: 0x04005F96 RID: 24470
			public Func<IGeneratedFunction> FuncGenerator;
		}
	}
}
