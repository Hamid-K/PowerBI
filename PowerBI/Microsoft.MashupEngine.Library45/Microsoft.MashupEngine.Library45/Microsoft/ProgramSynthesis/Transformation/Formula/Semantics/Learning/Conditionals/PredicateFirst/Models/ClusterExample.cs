using System;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.PredicateFirst.Models
{
	// Token: 0x0200174E RID: 5966
	public class ClusterExample
	{
		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x0600C612 RID: 50706 RVA: 0x002A9A6C File Offset: 0x002A7C6C
		// (set) Token: 0x0600C613 RID: 50707 RVA: 0x002A9A74 File Offset: 0x002A7C74
		public Example<IRow, object> Example { get; set; }

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x0600C614 RID: 50708 RVA: 0x002A9A7D File Offset: 0x002A7C7D
		public IRow Input
		{
			get
			{
				return this.Example.Input;
			}
		}

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x0600C615 RID: 50709 RVA: 0x002A9A8A File Offset: 0x002A7C8A
		public object Output
		{
			get
			{
				return this.Example.Output;
			}
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x0600C616 RID: 50710 RVA: 0x002A9A97 File Offset: 0x002A7C97
		// (set) Token: 0x0600C617 RID: 50711 RVA: 0x002A9A9F File Offset: 0x002A7C9F
		public int Position { get; set; }

		// Token: 0x0600C618 RID: 50712 RVA: 0x002A9AA8 File Offset: 0x002A7CA8
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = ((this.Example.GetType().Name == "TestExample") ? this.Example.ToString() : string.Format("{0,2}: {1}", this.Position, this.Example)));
			}
			return text;
		}

		// Token: 0x04004DBA RID: 19898
		private string _toString;
	}
}
