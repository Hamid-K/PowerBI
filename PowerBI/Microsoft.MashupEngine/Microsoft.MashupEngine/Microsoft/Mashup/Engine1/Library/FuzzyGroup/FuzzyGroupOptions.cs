using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B68 RID: 2920
	internal class FuzzyGroupOptions
	{
		// Token: 0x060050B0 RID: 20656 RVA: 0x0010E283 File Offset: 0x0010C483
		private FuzzyGroupOptions(string cultureKey, bool ignoreCase, bool ignoreSpace, string similarityColumnName, double threshold, TableValue transformationTable)
		{
			this.cultureKey = cultureKey;
			this.ignoreCase = ignoreCase;
			this.ignoreSpace = ignoreSpace;
			this.similarityColumnName = similarityColumnName;
			this.threshold = threshold;
			this.transformationTable = transformationTable;
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x060050B1 RID: 20657 RVA: 0x0010E2B8 File Offset: 0x0010C4B8
		public string CultureKey
		{
			get
			{
				return this.cultureKey;
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x060050B2 RID: 20658 RVA: 0x0010E2C0 File Offset: 0x0010C4C0
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x060050B3 RID: 20659 RVA: 0x0010E2C8 File Offset: 0x0010C4C8
		public bool IgnoreSpace
		{
			get
			{
				return this.ignoreSpace;
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x060050B4 RID: 20660 RVA: 0x0010E2D0 File Offset: 0x0010C4D0
		public string SimilarityColumnName
		{
			get
			{
				return this.similarityColumnName;
			}
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x060050B5 RID: 20661 RVA: 0x0010E2D8 File Offset: 0x0010C4D8
		public double Threshold
		{
			get
			{
				return this.threshold;
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x0010E2E0 File Offset: 0x0010C4E0
		public TableValue TransformationTable
		{
			get
			{
				return this.transformationTable;
			}
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0010E2E8 File Offset: 0x0010C4E8
		public static FuzzyGroupOptions ToFuzzyGroupOptions(RecordValue optionsRecord, IEngineHost host)
		{
			ICulture defaultCulture = Culture.GetDefaultCulture(host);
			string text = ((defaultCulture != null) ? defaultCulture.Name : FuzzyGroupOptions.DefaultCultureKey);
			bool flag = true;
			bool flag2 = true;
			string text2 = null;
			double num = 0.8;
			TableValue tableValue = null;
			try
			{
				if (!optionsRecord.IsEmpty)
				{
					Value value;
					optionsRecord.TryGetValue("Threshold", out value);
					if (!value.IsNull)
					{
						num = value.AsNumber.ToDouble();
					}
					Value value2;
					optionsRecord.TryGetValue("IgnoreCase", out value2);
					if (!value2.IsNull)
					{
						flag = value2.AsBoolean;
					}
					Value value3;
					optionsRecord.TryGetValue("IgnoreSpace", out value3);
					if (!value3.IsNull)
					{
						flag2 = value3.AsBoolean;
					}
					Value value4;
					optionsRecord.TryGetValue("Culture", out value4);
					if (!value4.IsNull)
					{
						text = value4.AsString;
					}
					Value value5;
					optionsRecord.TryGetValue("SimilarityColumnName", out value5);
					if (!value5.IsNull)
					{
						text2 = value5.AsString;
					}
					Value value6;
					optionsRecord.TryGetValue("TransformationTable", out value6);
					if (!value6.IsNull)
					{
						int num2 = -1;
						if (!value6.AsTable.Columns.TryGetKeyIndex("From", out num2) || value6.AsTable.GetColumnType(num2).TypeKind != ValueKind.Text)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidTransformationTableColumnType("From"), null, null);
						}
						int num3 = -1;
						if (!value6.AsTable.Columns.TryGetKeyIndex("To", out num3) || value6.AsTable.GetColumnType(num3).TypeKind != ValueKind.Text)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidTransformationTableColumnType("To"), null, null);
						}
						tableValue = value6.AsTable;
					}
				}
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidFuzzyOptionArgument(ex.Message), optionsRecord, ex);
			}
			return new FuzzyGroupOptions(text, flag, flag2, text2, num, tableValue);
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0010E4C0 File Offset: 0x0010C6C0
		public RecordValue AsRecord()
		{
			List<NamedValue> list = new List<NamedValue>();
			list.Add(new NamedValue("Culture", TextValue.New(this.cultureKey)));
			list.Add(new NamedValue("IgnoreCase", LogicalValue.New(this.ignoreCase)));
			list.Add(new NamedValue("IgnoreSpace", LogicalValue.New(this.ignoreSpace)));
			list.Add(new NamedValue("Threshold", NumberValue.New(this.threshold)));
			if (this.similarityColumnName != null)
			{
				list.Add(new NamedValue("SimilarityColumnName", TextValue.New(this.similarityColumnName)));
			}
			if (this.transformationTable != null)
			{
				list.Add(new NamedValue("TransformationTable", this.transformationTable));
			}
			return RecordValue.New(list.ToArray());
		}

		// Token: 0x04002B4F RID: 11087
		private static readonly string DefaultCultureKey = CultureInfo.InvariantCulture.Name;

		// Token: 0x04002B50 RID: 11088
		private const bool DefaultIgnoreCase = true;

		// Token: 0x04002B51 RID: 11089
		private const bool DefaultIgnoreSpace = true;

		// Token: 0x04002B52 RID: 11090
		private const double DefaultThreshold = 0.8;

		// Token: 0x04002B53 RID: 11091
		private const string FromColumnName = "From";

		// Token: 0x04002B54 RID: 11092
		private const string ToColumnName = "To";

		// Token: 0x04002B55 RID: 11093
		private readonly string cultureKey;

		// Token: 0x04002B56 RID: 11094
		private readonly bool ignoreCase;

		// Token: 0x04002B57 RID: 11095
		private readonly bool ignoreSpace;

		// Token: 0x04002B58 RID: 11096
		private readonly string similarityColumnName;

		// Token: 0x04002B59 RID: 11097
		private readonly double threshold;

		// Token: 0x04002B5A RID: 11098
		private readonly TableValue transformationTable;
	}
}
