using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000090 RID: 144
	public abstract class Program<TInput, TOutput> : IProgram, IEquatable<Program<TInput, TOutput>>
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000BDFC File Offset: 0x00009FFC
		protected Program(ProgramNode programNode, double score, Func<ProgramNode, ProgramNode> programNormalizingFunc = null)
		{
			this.RawProgramNode = programNode;
			this.ProgramNode = ((programNormalizingFunc == null) ? programNode : programNormalizingFunc(programNode));
			this.Score = score;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000BE32 File Offset: 0x0000A032
		public ProgramNode ProgramNode { get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000BE3A File Offset: 0x0000A03A
		public ProgramNode RawProgramNode { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000BE42 File Offset: 0x0000A042
		public double Score { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000BE4A File Offset: 0x0000A04A
		public Version Version { get; } = new Version(0, 2);

		// Token: 0x06000326 RID: 806 RVA: 0x0000BE52 File Offset: 0x0000A052
		public virtual bool Equals(Program<TInput, TOutput> other)
		{
			return other != null && (this == other || object.Equals(this.ProgramNode, other.ProgramNode));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BE70 File Offset: 0x0000A070
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Program<TInput, TOutput>)obj)));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Program<TInput, TOutput> left, Program<TInput, TOutput> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Program<TInput, TOutput> left, Program<TInput, TOutput> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BEB3 File Offset: 0x0000A0B3
		public override int GetHashCode()
		{
			ProgramNode programNode = this.ProgramNode;
			if (programNode == null)
			{
				return 0;
			}
			return programNode.GetHashCode();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
		public override string ToString()
		{
			return this.ProgramNode.PrintAST(ASTSerializationFormat.HumanReadable);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public virtual string Serialize(ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML)
		{
			return this.Serialize(serializationFormat.AsASTSerializationSettings());
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000BEE2 File Offset: 0x0000A0E2
		public virtual string Serialize(ASTSerializationSettings serializationSettings)
		{
			return this.ProgramNode.PrintAST(serializationSettings);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		public virtual string SerializeAnonymized(ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML)
		{
			return this.SerializeAnonymized(serializationFormat.AsASTSerializationSettings());
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		public virtual string SerializeAnonymized(ASTSerializationSettings serializationSettings)
		{
			return this.Serialize(serializationSettings.WithOmitLiterals);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00002188 File Offset: 0x00000388
		public virtual string Describe(CultureInfo cultureInfo = null)
		{
			return null;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000BF0D File Offset: 0x0000A10D
		object IProgram.Run(object input)
		{
			return this.Run((TInput)((object)input));
		}

		// Token: 0x06000332 RID: 818
		public abstract TOutput Run(TInput input);
	}
}
