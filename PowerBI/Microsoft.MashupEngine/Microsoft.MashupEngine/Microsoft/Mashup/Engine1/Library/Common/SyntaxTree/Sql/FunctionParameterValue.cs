using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D4 RID: 4564
	internal sealed class FunctionParameterValue : ScalarExpression
	{
		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x0600787E RID: 30846 RVA: 0x001A150E File Offset: 0x0019F70E
		// (set) Token: 0x0600787F RID: 30847 RVA: 0x001A1516 File Offset: 0x0019F716
		public SqlDataType ParameterType { get; set; }

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06007880 RID: 30848 RVA: 0x001A151F File Offset: 0x0019F71F
		public override int Precedence
		{
			get
			{
				if (this.Value == null)
				{
					return 10;
				}
				return this.Value.Precedence;
			}
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06007881 RID: 30849 RVA: 0x001A1537 File Offset: 0x0019F737
		// (set) Token: 0x06007882 RID: 30850 RVA: 0x001A153F File Offset: 0x0019F73F
		public SqlExpression Value { get; set; }

		// Token: 0x06007883 RID: 30851 RVA: 0x001A1548 File Offset: 0x0019F748
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSubexpression(FunctionReferenceBase.FunctionParameterPrecedence, this.Value);
		}
	}
}
