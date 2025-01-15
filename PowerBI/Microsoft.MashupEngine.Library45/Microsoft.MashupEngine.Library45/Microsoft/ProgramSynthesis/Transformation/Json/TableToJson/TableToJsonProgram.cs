using System;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.TableToJson
{
	// Token: 0x02001A78 RID: 6776
	[DebuggerDisplay("{ProgramNode}")]
	public class TableToJsonProgram : TransformationProgram<TableToJsonProgram, Table, Newtonsoft.Json.Linq.JToken>
	{
		// Token: 0x0600DF16 RID: 57110 RVA: 0x002F59F3 File Offset: 0x002F3BF3
		internal TableToJsonProgram(ProgramNode node, double score)
			: base(node, score, null)
		{
		}

		// Token: 0x0600DF17 RID: 57111 RVA: 0x002F59FE File Offset: 0x002F3BFE
		internal TableToJsonProgram(ProgramNode node)
			: base(node, node.GetFeatureValue<double>(TableToJsonLearner.Instance.ScoreFeature, null), null)
		{
		}

		// Token: 0x0600DF18 RID: 57112 RVA: 0x002F5A1C File Offset: 0x002F3C1C
		public override Newtonsoft.Json.Linq.JToken Run(Table input)
		{
			object obj = Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.From(input.ToJArray());
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, obj)) as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
			if (jtoken == null)
			{
				return null;
			}
			return jtoken.Wrapped;
		}

		// Token: 0x0600DF19 RID: 57113 RVA: 0x002F5A60 File Offset: 0x002F3C60
		public string Serialize()
		{
			return base.ProgramNode.PrintAST(ASTSerializationFormat.XML);
		}

		// Token: 0x0600DF1A RID: 57114 RVA: 0x002F5A6E File Offset: 0x002F3C6E
		public bool Equals(TableToJsonProgram other)
		{
			return other != null && (this == other || object.Equals(base.ProgramNode, other.ProgramNode));
		}

		// Token: 0x0600DF1B RID: 57115 RVA: 0x002F5A8C File Offset: 0x002F3C8C
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((TableToJsonProgram)obj)));
		}

		// Token: 0x0600DF1C RID: 57116 RVA: 0x002F5ABA File Offset: 0x002F3CBA
		public override int GetHashCode()
		{
			ProgramNode programNode = base.ProgramNode;
			if (programNode == null)
			{
				return 0;
			}
			return programNode.GetHashCode();
		}

		// Token: 0x0600DF1D RID: 57117 RVA: 0x002F5ACD File Offset: 0x002F3CCD
		public override string ToString()
		{
			return base.ProgramNode.ToString();
		}
	}
}
