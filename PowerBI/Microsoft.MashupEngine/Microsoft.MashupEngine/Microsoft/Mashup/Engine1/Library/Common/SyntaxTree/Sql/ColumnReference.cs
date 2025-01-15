using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C1 RID: 4545
	internal sealed class ColumnReference : ScalarExpression
	{
		// Token: 0x0600782A RID: 30762 RVA: 0x001A0E40 File Offset: 0x0019F040
		public ColumnReference(Alias name)
			: this(null, name)
		{
		}

		// Token: 0x0600782B RID: 30763 RVA: 0x001A0E4A File Offset: 0x0019F04A
		public ColumnReference(Alias qualifier, Alias name)
		{
			this.qualifier = qualifier;
			this.name = name;
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x0600782C RID: 30764 RVA: 0x001A0E60 File Offset: 0x0019F060
		public Alias Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x0600782D RID: 30765 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x0600782E RID: 30766 RVA: 0x001A0E68 File Offset: 0x0019F068
		public Alias Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x0600782F RID: 30767 RVA: 0x001A0E70 File Offset: 0x0019F070
		public override void WriteCreateScript(ScriptWriter writer)
		{
			if (this.qualifier != null)
			{
				writer.WriteIdentifier(this.qualifier, this.Name);
				return;
			}
			writer.WriteIdentifier(this.Name);
		}

		// Token: 0x0400415D RID: 16733
		private readonly Alias name;

		// Token: 0x0400415E RID: 16734
		private readonly Alias qualifier;
	}
}
