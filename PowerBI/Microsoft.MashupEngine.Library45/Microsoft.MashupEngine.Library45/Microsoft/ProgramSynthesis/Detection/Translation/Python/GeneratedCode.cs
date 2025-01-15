using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B12 RID: 2834
	internal struct GeneratedCode
	{
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060046C1 RID: 18113 RVA: 0x000DD241 File Offset: 0x000DB441
		public readonly GeneratedCodeKind Kind { get; }

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060046C2 RID: 18114 RVA: 0x000DD249 File Offset: 0x000DB449
		public readonly IPythonColumnInfo ColumnInfo { get; }

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060046C3 RID: 18115 RVA: 0x000DD251 File Offset: 0x000DB451
		public readonly IRichDataType DetectedType { get; }

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x060046C4 RID: 18116 RVA: 0x000DD259 File Offset: 0x000DB459
		public readonly CodeBuilder OutOfLineFunction { get; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x060046C5 RID: 18117 RVA: 0x000DD261 File Offset: 0x000DB461
		public readonly string InlineExpression { get; }

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x060046C6 RID: 18118 RVA: 0x000DD269 File Offset: 0x000DB469
		public readonly PythonImports Imports { get; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x000DD271 File Offset: 0x000DB471
		public string OutOfLineFunctionIdentifier
		{
			get
			{
				if (this.Kind != GeneratedCodeKind.OutOfLineFunction)
				{
					return null;
				}
				return this.ColumnInfo.ParseFunctionName();
			}
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x000DD288 File Offset: 0x000DB488
		public string LambdaExpression(string lambdaVariable = "x")
		{
			string text;
			switch (this.Kind)
			{
			case GeneratedCodeKind.OutOfLineFunction:
				text = this.ColumnInfo.ParseFunctionName();
				break;
			case GeneratedCodeKind.InlineExpression:
				text = FormattableString.Invariant(FormattableStringFactory.Create("lambda {0}: {1}", new object[]
				{
					lambdaVariable,
					string.Format(this.InlineExpression, lambdaVariable)
				}));
				break;
			case GeneratedCodeKind.InlineFunctionName:
				text = this.InlineExpression;
				break;
			default:
				throw new NotImplementedException();
			}
			return text;
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x000DD2FC File Offset: 0x000DB4FC
		public string Expression(string variable = "x")
		{
			string text;
			if (this.Kind == GeneratedCodeKind.InlineExpression)
			{
				text = string.Format(this.InlineExpression, variable);
			}
			else
			{
				text = this.LambdaExpression("x") + "(" + variable + ")";
			}
			return text;
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x000DD33E File Offset: 0x000DB53E
		public GeneratedCode(CodeBuilder outOfLineFunction, IPythonColumnInfo columnInfo, IRichDataType detectedType, params string[] imports)
		{
			this = new GeneratedCode(outOfLineFunction, columnInfo, detectedType, imports);
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x000DD34C File Offset: 0x000DB54C
		public GeneratedCode(CodeBuilder outOfLineFunction, IPythonColumnInfo columnInfo, IRichDataType detectedType, IEnumerable<string> imports = null)
		{
			this.Kind = GeneratedCodeKind.OutOfLineFunction;
			this.ColumnInfo = columnInfo;
			this.DetectedType = detectedType;
			this.InlineExpression = FormattableString.Invariant(FormattableStringFactory.Create("{0}({{0}})", new object[] { this.ColumnInfo.ParseFunctionName() }));
			this.OutOfLineFunction = outOfLineFunction;
			this.Imports = new PythonImports();
			if (imports != null)
			{
				this.Imports.AddImports(imports);
			}
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x000DD3BA File Offset: 0x000DB5BA
		public GeneratedCode(string inlineExpression, IPythonColumnInfo columnInfo, IRichDataType detectedType, params string[] imports)
		{
			this = new GeneratedCode(inlineExpression, columnInfo, detectedType, imports);
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x000DD3C8 File Offset: 0x000DB5C8
		public GeneratedCode(string inlineExpression, IPythonColumnInfo columnInfo, IRichDataType detectedType, IEnumerable<string> imports = null)
		{
			this.Kind = (inlineExpression.All((char ch) => char.IsLetterOrDigit(ch) || ch == '_') ? GeneratedCodeKind.InlineFunctionName : GeneratedCodeKind.InlineExpression);
			this.ColumnInfo = columnInfo;
			this.DetectedType = detectedType;
			this.OutOfLineFunction = null;
			this.InlineExpression = inlineExpression;
			this.Imports = new PythonImports();
			if (imports != null)
			{
				this.Imports.AddImports(imports);
			}
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x000DD43E File Offset: 0x000DB63E
		public static GeneratedCode ForPassThrough(IPythonColumnInfo columnInfo, IRichDataType detectedType)
		{
			return new GeneratedCode("{0}", columnInfo, detectedType, null);
		}
	}
}
