using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B3D RID: 2877
	public class FuzzyJoinOptions
	{
		// Token: 0x06004FD3 RID: 20435 RVA: 0x0010B450 File Offset: 0x00109650
		private FuzzyJoinOptions(int concurrentRequests, string cultureKey, bool ignoreCase, bool ignoreSpace, int numberOfMatches, string similarityColumnName, double threshold, TableValue transformationTable)
		{
			this.concurrentRequests = concurrentRequests;
			this.cultureKey = cultureKey;
			this.ignoreCase = ignoreCase;
			this.ignoreSpace = ignoreSpace;
			this.numberOfMatches = numberOfMatches;
			this.similarityColumnName = similarityColumnName;
			this.threshold = threshold;
			this.transformationTable = transformationTable;
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x0010B4A0 File Offset: 0x001096A0
		public int ConcurrentRequests
		{
			get
			{
				return this.concurrentRequests;
			}
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x0010B4A8 File Offset: 0x001096A8
		public string CultureKey
		{
			get
			{
				return this.cultureKey;
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x06004FD6 RID: 20438 RVA: 0x0010B4B0 File Offset: 0x001096B0
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x0010B4B8 File Offset: 0x001096B8
		public bool IgnoreSpace
		{
			get
			{
				return this.ignoreSpace;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x0010B4C0 File Offset: 0x001096C0
		public int NumberOfMatches
		{
			get
			{
				return this.numberOfMatches;
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x06004FD9 RID: 20441 RVA: 0x0010B4C8 File Offset: 0x001096C8
		public string SimilarityColumnName
		{
			get
			{
				return this.similarityColumnName;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x06004FDA RID: 20442 RVA: 0x0010B4D0 File Offset: 0x001096D0
		public double Threshold
		{
			get
			{
				return this.threshold;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06004FDB RID: 20443 RVA: 0x0010B4D8 File Offset: 0x001096D8
		public TableValue TransformationTable
		{
			get
			{
				return this.transformationTable;
			}
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0010B4E0 File Offset: 0x001096E0
		public static FuzzyJoinOptions ToFuzzyJoinOptions(RecordValue optionsRecord, IEngineHost host)
		{
			int num = 1;
			ICulture defaultCulture = Culture.GetDefaultCulture(host);
			string text = ((defaultCulture != null) ? defaultCulture.Name : FuzzyJoinOptions.DefaultCultureKey);
			bool flag = true;
			bool flag2 = true;
			int num2 = int.MaxValue;
			string text2 = null;
			double num3 = 0.8;
			TableValue tableValue = null;
			try
			{
				if (!optionsRecord.IsEmpty)
				{
					Value value;
					optionsRecord.TryGetValue("Threshold", out value);
					if (!value.IsNull)
					{
						num3 = value.AsNumber.ToDouble();
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
					optionsRecord.TryGetValue("NumberOfMatches", out value4);
					if (!value4.IsNull)
					{
						num2 = value4.AsInteger32;
					}
					Value value5;
					optionsRecord.TryGetValue("ConcurrentRequests", out value5);
					if (!value5.IsNull)
					{
						num = value5.AsInteger32;
					}
					Value value6;
					optionsRecord.TryGetValue("Culture", out value6);
					if (!value6.IsNull)
					{
						text = value6.AsString;
					}
					Value value7;
					optionsRecord.TryGetValue("SimilarityColumnName", out value7);
					if (!value7.IsNull)
					{
						text2 = value7.AsString;
					}
					Value value8;
					optionsRecord.TryGetValue("TransformationTable", out value8);
					if (!value8.IsNull)
					{
						int num4 = -1;
						if (!value8.AsTable.Columns.TryGetKeyIndex("From", out num4) || value8.AsTable.GetColumnType(num4).TypeKind != ValueKind.Text)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidTransformationTableColumnType("From"), null, null);
						}
						int num5 = -1;
						if (!value8.AsTable.Columns.TryGetKeyIndex("To", out num5) || value8.AsTable.GetColumnType(num5).TypeKind != ValueKind.Text)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidTransformationTableColumnType("To"), null, null);
						}
						tableValue = value8.AsTable;
					}
				}
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.FuzzyUtilInvalidFuzzyOptionArgument(ex.Message), optionsRecord, ex);
			}
			return new FuzzyJoinOptions(num, text, flag, flag2, num2, text2, num3, tableValue);
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0010B708 File Offset: 0x00109908
		public RecordValue AsRecord()
		{
			List<NamedValue> list = new List<NamedValue>();
			list.Add(new NamedValue("ConcurrentRequests", NumberValue.New(this.concurrentRequests)));
			list.Add(new NamedValue("Culture", TextValue.New(this.cultureKey)));
			list.Add(new NamedValue("IgnoreCase", LogicalValue.New(this.ignoreCase)));
			list.Add(new NamedValue("IgnoreSpace", LogicalValue.New(this.ignoreSpace)));
			list.Add(new NamedValue("NumberOfMatches", NumberValue.New(this.numberOfMatches)));
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

		// Token: 0x04002ACA RID: 10954
		private const int DefaultConcurrentRequests = 1;

		// Token: 0x04002ACB RID: 10955
		private static readonly string DefaultCultureKey = CultureInfo.InvariantCulture.Name;

		// Token: 0x04002ACC RID: 10956
		private const bool DefaultIgnoreCase = true;

		// Token: 0x04002ACD RID: 10957
		private const bool DefaultIgnoreSpace = true;

		// Token: 0x04002ACE RID: 10958
		private const int DefaultNumberOfMatches = 2147483647;

		// Token: 0x04002ACF RID: 10959
		private const double DefaultThreshold = 0.8;

		// Token: 0x04002AD0 RID: 10960
		public const string FromColumnName = "From";

		// Token: 0x04002AD1 RID: 10961
		public const string ToColumnName = "To";

		// Token: 0x04002AD2 RID: 10962
		private int concurrentRequests;

		// Token: 0x04002AD3 RID: 10963
		private string cultureKey;

		// Token: 0x04002AD4 RID: 10964
		private bool ignoreCase;

		// Token: 0x04002AD5 RID: 10965
		private bool ignoreSpace;

		// Token: 0x04002AD6 RID: 10966
		private int numberOfMatches;

		// Token: 0x04002AD7 RID: 10967
		private string similarityColumnName;

		// Token: 0x04002AD8 RID: 10968
		private double threshold;

		// Token: 0x04002AD9 RID: 10969
		private TableValue transformationTable;
	}
}
