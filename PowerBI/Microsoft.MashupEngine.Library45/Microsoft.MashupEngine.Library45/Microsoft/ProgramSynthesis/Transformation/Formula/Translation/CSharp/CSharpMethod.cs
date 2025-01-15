using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200193F RID: 6463
	internal class CSharpMethod : FormulaExpression
	{
		// Token: 0x0600D364 RID: 54116 RVA: 0x002D1334 File Offset: 0x002CF534
		public CSharpMethod(string name, Type returnType, IEnumerable<CSharpMethodParameter> parameters, CSharpBlock body, string accessModifier)
		{
			this.Name = name;
			this.ReturnType = returnType;
			this.Parameters = parameters.ToReadOnlyList<CSharpMethodParameter>();
			this.AccessModifier = accessModifier;
			this.Body = body;
			base.Children = this.Parameters.Concat(this.Body.Yield<FormulaExpression>()).ToReadOnlyList<FormulaExpression>();
		}

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x0600D365 RID: 54117 RVA: 0x002D1392 File Offset: 0x002CF592
		public string AccessModifier { get; }

		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x0600D366 RID: 54118 RVA: 0x002D139A File Offset: 0x002CF59A
		public CSharpBlock Body { get; }

		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x0600D367 RID: 54119 RVA: 0x002D13A2 File Offset: 0x002CF5A2
		public string Name { get; }

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x0600D368 RID: 54120 RVA: 0x002D13AA File Offset: 0x002CF5AA
		public IReadOnlyList<CSharpMethodParameter> Parameters { get; }

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x0600D369 RID: 54121 RVA: 0x002D13B2 File Offset: 0x002CF5B2
		public Type ReturnType { get; }

		// Token: 0x0600D36A RID: 54122 RVA: 0x002D13BA File Offset: 0x002CF5BA
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpMethod(this.Name, this.ReturnType, this.Parameters.Accept(visitor), (CSharpBlock)this.Body.Accept<FormulaExpression>(visitor), this.AccessModifier);
		}

		// Token: 0x0600D36B RID: 54123 RVA: 0x002D13F0 File Offset: 0x002CF5F0
		public CSharpMethod With(string name = null, Type returnType = null, IEnumerable<CSharpMethodParameter> parameters = null, CSharpBlock body = null, string accessModifier = null)
		{
			return new CSharpMethod(name ?? this.Name, returnType ?? this.ReturnType, parameters ?? this.Parameters, body ?? this.Body, accessModifier ?? this.AccessModifier);
		}

		// Token: 0x0600D36C RID: 54124 RVA: 0x002D1430 File Offset: 0x002CF630
		protected override string ToCodeString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			string text = string.Join(", ", this.Parameters.Select((CSharpMethodParameter p) => p.ToString()));
			codeBuilder.AppendLine(string.Concat(new string[]
			{
				this.AccessModifier,
				" static ",
				this.ReturnType.CsName(false),
				" ",
				this.Name,
				"(",
				text,
				")"
			}));
			codeBuilder.AppendIndented(this.Body.ToString());
			return codeBuilder.GetCode();
		}
	}
}
