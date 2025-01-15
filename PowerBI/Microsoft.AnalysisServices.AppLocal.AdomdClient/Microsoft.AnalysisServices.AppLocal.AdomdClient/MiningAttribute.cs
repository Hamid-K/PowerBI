using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B0 RID: 176
	public sealed class MiningAttribute
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x0002A218 File Offset: 0x00028418
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

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002A2D8 File Offset: 0x000284D8
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002A324 File Offset: 0x00028524
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002A32C File Offset: 0x0002852C
		public string ShortName
		{
			get
			{
				return this.shortName;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002A334 File Offset: 0x00028534
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

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002A3C0 File Offset: 0x000285C0
		public MiningModelColumn ValueColumn
		{
			get
			{
				return this.valueColumn;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002A3C8 File Offset: 0x000285C8
		public MiningModelColumn KeyColumn
		{
			get
			{
				return this.keyColumn;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002A3D0 File Offset: 0x000285D0
		public bool IsInput
		{
			get
			{
				return this.isInput;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002A3D8 File Offset: 0x000285D8
		public bool IsPredictable
		{
			get
			{
				return this.isPredictable;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002A3E0 File Offset: 0x000285E0
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

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002A53D File Offset: 0x0002873D
		public MiningFeatureSelection FeatureSelection
		{
			get
			{
				return this.featureSelection;
			}
		}

		// Token: 0x04000695 RID: 1685
		internal MiningModel parentModel;

		// Token: 0x04000696 RID: 1686
		internal string name;

		// Token: 0x04000697 RID: 1687
		internal string shortName;

		// Token: 0x04000698 RID: 1688
		internal MiningModelColumn valueColumn;

		// Token: 0x04000699 RID: 1689
		internal MiningModelColumn keyColumn;

		// Token: 0x0400069A RID: 1690
		internal MiningModelColumn parentColumn;

		// Token: 0x0400069B RID: 1691
		internal int attributeID;

		// Token: 0x0400069C RID: 1692
		internal MiningFeatureSelection featureSelection;

		// Token: 0x0400069D RID: 1693
		internal bool isInput;

		// Token: 0x0400069E RID: 1694
		internal bool isPredictable;
	}
}
