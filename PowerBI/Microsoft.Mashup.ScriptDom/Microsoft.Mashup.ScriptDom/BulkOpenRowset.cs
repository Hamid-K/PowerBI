using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	internal class BulkOpenRowset : TableReferenceWithAliasAndColumns
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x0016196D File Offset: 0x0015FB6D
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x00161975 File Offset: 0x0015FB75
		public StringLiteral DataFile
		{
			get
			{
				return this._dataFile;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._dataFile = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x00161985 File Offset: 0x0015FB85
		public IList<BulkInsertOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0016198D File Offset: 0x0015FB8D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x0016199C File Offset: 0x0015FB9C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DataFile != null)
			{
				this.DataFile.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001AB7 RID: 6839
		private StringLiteral _dataFile;

		// Token: 0x04001AB8 RID: 6840
		private List<BulkInsertOption> _options = new List<BulkInsertOption>();
	}
}
