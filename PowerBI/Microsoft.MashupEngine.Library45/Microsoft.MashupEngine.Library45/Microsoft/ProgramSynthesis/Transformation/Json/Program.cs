using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json
{
	// Token: 0x020019FA RID: 6650
	[DebuggerDisplay("{ProgramNode}")]
	public class Program : TransformationProgram<Program, Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken>
	{
		// Token: 0x0600D8A3 RID: 55459 RVA: 0x002DF4A2 File Offset: 0x002DD6A2
		internal Program(ProgramNode node)
			: base(node, node.GetFeatureValue<double>(Learner.Instance.ScoreFeature, null), null)
		{
		}

		// Token: 0x0600D8A4 RID: 55460 RVA: 0x002DF4BD File Offset: 0x002DD6BD
		public override Newtonsoft.Json.Linq.JToken Run(Newtonsoft.Json.Linq.JToken input)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.From(input))) as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
			if (jtoken == null)
			{
				return null;
			}
			return jtoken.Wrapped;
		}

		// Token: 0x0600D8A5 RID: 55461 RVA: 0x002DF4F0 File Offset: 0x002DD6F0
		public Newtonsoft.Json.Linq.JToken RunOnArray(Newtonsoft.Json.Linq.JToken input)
		{
			Newtonsoft.Json.Linq.JToken jtoken = this.Run(input);
			if (jtoken != null)
			{
				return jtoken;
			}
			Newtonsoft.Json.Linq.JArray jarray = input as Newtonsoft.Json.Linq.JArray;
			if (jarray == null)
			{
				return null;
			}
			List<Newtonsoft.Json.Linq.JToken> list = new List<Newtonsoft.Json.Linq.JToken>(jarray.Count);
			bool flag = false;
			foreach (Newtonsoft.Json.Linq.JToken jtoken2 in jarray)
			{
				Newtonsoft.Json.Linq.JToken jtoken3 = this.Run(jtoken2);
				list.Add(jtoken3);
				if (jtoken3 != null)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return new Newtonsoft.Json.Linq.JArray(list);
		}

		// Token: 0x0600D8A6 RID: 55462 RVA: 0x002DF584 File Offset: 0x002DD784
		public Newtonsoft.Json.Linq.JToken Run(string input)
		{
			return this.Run(Newtonsoft.Json.Linq.JToken.Parse(input));
		}

		// Token: 0x0600D8A7 RID: 55463 RVA: 0x002DF592 File Offset: 0x002DD792
		public Newtonsoft.Json.Linq.JToken RunOnArray(string input)
		{
			return this.RunOnArray(Newtonsoft.Json.Linq.JToken.Parse(input));
		}

		// Token: 0x0600D8A8 RID: 55464 RVA: 0x002DF5A0 File Offset: 0x002DD7A0
		public string Serialize()
		{
			return base.ProgramNode.PrintAST(ASTSerializationFormat.XML);
		}

		// Token: 0x0600D8A9 RID: 55465 RVA: 0x002DF5AE File Offset: 0x002DD7AE
		public bool Equals(Program other)
		{
			return other != null && (this == other || object.Equals(base.ProgramNode, other.ProgramNode));
		}

		// Token: 0x0600D8AA RID: 55466 RVA: 0x002DF5CC File Offset: 0x002DD7CC
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Program)obj)));
		}

		// Token: 0x0600D8AB RID: 55467 RVA: 0x002DF5FA File Offset: 0x002DD7FA
		public override int GetHashCode()
		{
			ProgramNode programNode = base.ProgramNode;
			if (programNode == null)
			{
				return 0;
			}
			return programNode.GetHashCode();
		}

		// Token: 0x0600D8AC RID: 55468 RVA: 0x002DF60D File Offset: 0x002DD80D
		public override string ToString()
		{
			return base.ProgramNode.ToString();
		}
	}
}
