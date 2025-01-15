using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models
{
	// Token: 0x02001728 RID: 5928
	public class ConditionalPathList : ReadOnlyListBase<ConditionalBranch[]>
	{
		// Token: 0x0600C545 RID: 50501 RVA: 0x002A754E File Offset: 0x002A574E
		public ConditionalPathList()
			: this(new ConditionalBranch[][] { new ConditionalBranch[0] }, new ConditionalClusterList())
		{
		}

		// Token: 0x0600C546 RID: 50502 RVA: 0x002A756A File Offset: 0x002A576A
		public ConditionalPathList(ConditionalClusterList outputClusters)
			: this(new ConditionalBranch[][] { new ConditionalBranch[0] }, new ConditionalClusterList())
		{
			this.OutputClusters = outputClusters;
		}

		// Token: 0x0600C547 RID: 50503 RVA: 0x002A758D File Offset: 0x002A578D
		public ConditionalPathList(IEnumerable<ConditionalBranch[]> sources, ConditionalClusterList outputClusters)
			: base(sources)
		{
			this.OutputClusters = outputClusters;
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x0600C548 RID: 50504 RVA: 0x002A759D File Offset: 0x002A579D
		public ConditionalClusterList OutputClusters { get; }

		// Token: 0x0600C549 RID: 50505 RVA: 0x002A75A8 File Offset: 0x002A57A8
		public string Render()
		{
			if (this.None<ConditionalBranch[]>())
			{
				return "<none>";
			}
			TextTableBuilder textTableBuilder = TextTableBuilder.Create(TextTableBorder.None, null, null);
			string text = "";
			int? num = new int?(0);
			TextTableBuilder textTableBuilder2 = textTableBuilder.AddIdentityColumn(text, null, num).AddStaticColumn(":", false, true, false);
			string text2 = "";
			int num2 = 0;
			num = null;
			int? num3 = num;
			bool flag = false;
			string text3 = null;
			string text4 = null;
			num = null;
			int? num4 = num;
			num = null;
			TextTableBuilder textTableBuilder3 = textTableBuilder2.AddColumn(text2, num2, num3, flag, text3, text4, num4, num);
			foreach (ConditionalBranch[] array in this)
			{
				TextTableBuilder textTableBuilder4 = textTableBuilder3;
				object[] array2 = new object[1];
				array2[0] = string.Join(" -> ", array.Select((ConditionalBranch i) => i.ToString()));
				textTableBuilder4.AddDataRow(array2);
			}
			return textTableBuilder3.Render();
		}
	}
}
