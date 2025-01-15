using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B0 RID: 176
	public sealed class MiningAttribute
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x00029EE8 File Offset: 0x000280E8
		internal MiningAttribute(MiningModel parentModel, string attributeDisplayName)
		{
			this.attributeID = -1;
			this.parentModel = parentModel;
			this.name = attributeDisplayName;
			this.shortName = "";
			this.valueColumn = null;
			this.keyColumn = null;
			this.featureSelection = MiningFeatureSelection.NotSelected;
			this.isInput = false;
			this.isPredictable = false;
			this.ParseAttributeName();
			if (this.valueColumn != null)
			{
				this.isInput = this.valueColumn.IsInput;
				this.isPredictable = this.valueColumn.IsPredictable;
				return;
			}
			if (this.parentColumn != null)
			{
				this.isInput = this.parentColumn.IsInput;
				this.isPredictable = this.parentColumn.IsPredictable;
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00029FA8 File Offset: 0x000281A8
		internal MiningAttribute(MiningModel parentModel)
		{
			this.attributeID = -1;
			this.parentModel = parentModel;
			this.name = "";
			this.shortName = "";
			this.valueColumn = null;
			this.keyColumn = null;
			this.featureSelection = MiningFeatureSelection.NotSelected;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00029FF4 File Offset: 0x000281F4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00029FFC File Offset: 0x000281FC
		public string ShortName
		{
			get
			{
				return this.shortName;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0002A004 File Offset: 0x00028204
		public int AttributeID
		{
			get
			{
				if (this.attributeID == -1 && this.parentModel != null && this.parentModel.Attributes != null && this.parentModel.Attributes.hashAttrIDs != null && this.parentModel.Attributes.hashAttrIDs.ContainsKey(this.Name))
				{
					this.attributeID = (int)this.parentModel.Attributes.hashAttrIDs[this.Name];
				}
				return this.attributeID;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002A090 File Offset: 0x00028290
		public MiningModelColumn ValueColumn
		{
			get
			{
				return this.valueColumn;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002A098 File Offset: 0x00028298
		public MiningModelColumn KeyColumn
		{
			get
			{
				return this.keyColumn;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002A0A0 File Offset: 0x000282A0
		public bool IsInput
		{
			get
			{
				return this.isInput;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002A0A8 File Offset: 0x000282A8
		public bool IsPredictable
		{
			get
			{
				return this.isPredictable;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002A0B0 File Offset: 0x000282B0
		private void ParseAttributeName()
		{
			if (this.name.Length == 0)
			{
				return;
			}
			int num = this.name.IndexOf('(');
			int num2 = this.name.IndexOf(')');
			int num3 = this.name.IndexOf('.');
			bool flag = false;
			if (num > 0 && num2 > num && (num3 < 0 || num3 == num2 + 1))
			{
				flag = true;
			}
			if (!flag)
			{
				this.valueColumn = this.parentModel.Columns[this.name];
				if (this.valueColumn != null)
				{
					this.shortName = this.name;
					return;
				}
			}
			else
			{
				bool flag2 = num3 < 0;
				string text = this.name.Substring(0, num);
				this.parentColumn = this.parentModel.Columns[text];
				if (this.parentColumn == null)
				{
					return;
				}
				if (flag2)
				{
					this.valueColumn = null;
					foreach (MiningModelColumn miningModelColumn in this.parentColumn.Columns)
					{
						if (miningModelColumn.Content == "KEY")
						{
							this.keyColumn = miningModelColumn;
							this.shortName = this.name;
						}
					}
					return;
				}
				string text2 = this.name.Substring(num3 + 1);
				this.valueColumn = this.parentColumn.Columns[text2];
				this.shortName = text2;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002A20D File Offset: 0x0002840D
		public MiningFeatureSelection FeatureSelection
		{
			get
			{
				return this.featureSelection;
			}
		}

		// Token: 0x04000688 RID: 1672
		internal MiningModel parentModel;

		// Token: 0x04000689 RID: 1673
		internal string name;

		// Token: 0x0400068A RID: 1674
		internal string shortName;

		// Token: 0x0400068B RID: 1675
		internal MiningModelColumn valueColumn;

		// Token: 0x0400068C RID: 1676
		internal MiningModelColumn keyColumn;

		// Token: 0x0400068D RID: 1677
		internal MiningModelColumn parentColumn;

		// Token: 0x0400068E RID: 1678
		internal int attributeID;

		// Token: 0x0400068F RID: 1679
		internal MiningFeatureSelection featureSelection;

		// Token: 0x04000690 RID: 1680
		internal bool isInput;

		// Token: 0x04000691 RID: 1681
		internal bool isPredictable;
	}
}
