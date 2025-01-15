using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011CB RID: 4555
	internal sealed class DynamicParameter : ScalarExpression
	{
		// Token: 0x06007856 RID: 30806 RVA: 0x001A12C1 File Offset: 0x0019F4C1
		public DynamicParameter(object value)
		{
			this.value = value;
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06007857 RID: 30807 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06007858 RID: 30808 RVA: 0x001A12D0 File Offset: 0x0019F4D0
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06007859 RID: 30809 RVA: 0x001A12D8 File Offset: 0x0019F4D8
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteDynamicParameter(this);
		}

		// Token: 0x040041A8 RID: 16808
		private readonly object value;
	}
}
