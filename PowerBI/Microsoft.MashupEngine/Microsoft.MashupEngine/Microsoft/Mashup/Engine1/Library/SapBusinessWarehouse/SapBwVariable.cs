using System;
using System.Data;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004F4 RID: 1268
	internal sealed class SapBwVariable
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x0007B708 File Offset: 0x00079908
		// (set) Token: 0x06002940 RID: 10560 RVA: 0x0007B710 File Offset: 0x00079910
		public string MdxIdentifier { get; set; }

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x0007B719 File Offset: 0x00079919
		// (set) Token: 0x06002942 RID: 10562 RVA: 0x0007B721 File Offset: 0x00079921
		public string Caption { get; set; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x0007B72A File Offset: 0x0007992A
		// (set) Token: 0x06002944 RID: 10564 RVA: 0x0007B732 File Offset: 0x00079932
		public int Ordinal { get; set; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x0007B73B File Offset: 0x0007993B
		// (set) Token: 0x06002946 RID: 10566 RVA: 0x0007B743 File Offset: 0x00079943
		public SapBwVariableSelectionType SelectionType { get; set; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x0007B74C File Offset: 0x0007994C
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x0007B754 File Offset: 0x00079954
		public SapBwVariableEntryType EntryType { get; set; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x0007B75D File Offset: 0x0007995D
		// (set) Token: 0x0600294A RID: 10570 RVA: 0x0007B765 File Offset: 0x00079965
		public SapBwVariableType Type { get; set; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x0007B76E File Offset: 0x0007996E
		// (set) Token: 0x0600294C RID: 10572 RVA: 0x0007B776 File Offset: 0x00079976
		public string Dimension { get; set; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x0007B77F File Offset: 0x0007997F
		// (set) Token: 0x0600294E RID: 10574 RVA: 0x0007B787 File Offset: 0x00079987
		public string Hierarchy { get; set; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x0007B790 File Offset: 0x00079990
		// (set) Token: 0x06002950 RID: 10576 RVA: 0x0007B798 File Offset: 0x00079998
		public SapBwDataType DataType { get; set; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x0007B7A1 File Offset: 0x000799A1
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x0007B7A9 File Offset: 0x000799A9
		public object DefaultLow { get; set; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x0007B7B2 File Offset: 0x000799B2
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x0007B7BA File Offset: 0x000799BA
		public string DefaultLowValueCaption { get; set; }

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x0007B7C3 File Offset: 0x000799C3
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x0007B7CB File Offset: 0x000799CB
		public object DefaultHigh { get; set; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x0007B7D4 File Offset: 0x000799D4
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x0007B7DC File Offset: 0x000799DC
		public string DefaultHighValueCaption { get; set; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x0007B7E5 File Offset: 0x000799E5
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x0007B7ED File Offset: 0x000799ED
		public string Description { get; set; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x0007B7F6 File Offset: 0x000799F6
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x0007B7FE File Offset: 0x000799FE
		public string InfoObject { get; set; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x0007B808 File Offset: 0x00079A08
		public bool IsDate
		{
			get
			{
				if (this.Type == SapBwVariableType.CharacteristicValue)
				{
					if (this.DataType.Name == "DATS")
					{
						return true;
					}
					if (this.Dimension != null && (this.Dimension.Equals("[0DATE]", StringComparison.Ordinal) || this.Dimension.Equals("[0CALDAY]", StringComparison.Ordinal)))
					{
						return true;
					}
					if (this.InfoObject != null && (this.InfoObject.Equals("0DATE", StringComparison.Ordinal) || this.InfoObject.Equals("0CALDAY", StringComparison.Ordinal)))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x0007B898 File Offset: 0x00079A98
		// (set) Token: 0x0600295F RID: 10591 RVA: 0x0007B8A0 File Offset: 0x00079AA0
		public bool IsLanguageDependent { get; set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x0007B8A9 File Offset: 0x00079AA9
		// (set) Token: 0x06002961 RID: 10593 RVA: 0x0007B8B1 File Offset: 0x00079AB1
		public bool IsTimeDependent { get; set; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x0007B8BA File Offset: 0x00079ABA
		// (set) Token: 0x06002963 RID: 10595 RVA: 0x0007B8C2 File Offset: 0x00079AC2
		public string CaptionSource { get; set; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x0007B8CB File Offset: 0x00079ACB
		// (set) Token: 0x06002965 RID: 10597 RVA: 0x0007B8D3 File Offset: 0x00079AD3
		public string MasterDataTable { get; set; }

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x0007B8DC File Offset: 0x00079ADC
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x0007B8E4 File Offset: 0x00079AE4
		public int? InternalLength { get; set; }

		// Token: 0x06002968 RID: 10600 RVA: 0x0007B8F0 File Offset: 0x00079AF0
		public void EnsureInfoObjectDetails(ISapBwService service)
		{
			if (this.InfoObject == null)
			{
				this.InfoObject = SapBwExtensions.UnquoteIdentifier(this.Dimension);
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				if (service.TryGetInfoObjectsDetail(new string[] { this.InfoObject }, out dataReaderWithTableSchema))
				{
					using (dataReaderWithTableSchema)
					{
						if (dataReaderWithTableSchema.Read())
						{
							this.ApplyInfo(dataReaderWithTableSchema, false);
						}
					}
				}
			}
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0007B960 File Offset: 0x00079B60
		public void ApplyInfo(IDataReader reader, bool initializing = true)
		{
			if (reader.FieldCount > 8)
			{
				this.InfoObject = reader.GetString(17);
				this.IsLanguageDependent = !reader.GetBoolean(4);
				this.IsTimeDependent = reader.GetBoolean(2) || reader.GetBoolean(3);
				this.CaptionSource = SapBwVariable.GetCaptionSource(reader.GetBoolean(1), reader.GetBoolean(7), reader.GetBoolean(6), reader.GetBoolean(5));
				this.InternalLength = new int?(reader.GetInt32(20));
				this.MasterDataTable = reader.GetString(11);
				if (string.IsNullOrEmpty(this.MasterDataTable))
				{
					this.MasterDataTable = reader.GetString(9);
				}
				SapBwDataType sapBwDataType;
				if (initializing && reader.GetString(8) == "DATS" && SapBwDataType.TryGetByName("DATS", out sapBwDataType))
				{
					this.DataType = sapBwDataType;
				}
			}
			else
			{
				this.InfoObject = reader.GetString(0);
				this.IsLanguageDependent = !SapBwVariable.ToBoolean(reader.GetString(4));
				this.IsTimeDependent = SapBwVariable.ToBoolean(reader.GetString(2)) || SapBwVariable.ToBoolean(reader.GetString(3));
				this.CaptionSource = SapBwVariable.GetCaptionSource(SapBwVariable.ToBoolean(reader.GetString(1)), SapBwVariable.ToBoolean(reader.GetString(7)), SapBwVariable.ToBoolean(reader.GetString(6)), SapBwVariable.ToBoolean(reader.GetString(5)));
			}
			if (string.IsNullOrEmpty(this.MasterDataTable))
			{
				string text = ((this.CaptionSource == null) ? "S" : "T");
				this.MasterDataTable = (this.InfoObject.StartsWith("Z", StringComparison.Ordinal) ? ("/BIC/" + text + this.InfoObject) : ("/BI0/" + text + this.InfoObject.Substring(1)));
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0007BB31 File Offset: 0x00079D31
		private static bool ToBoolean(string flag)
		{
			return flag.Equals("X", StringComparison.OrdinalIgnoreCase) || flag.Equals("1", StringComparison.Ordinal);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0007BB4F File Offset: 0x00079D4F
		private static string GetCaptionSource(bool textTableExists, bool longTextExists, bool mediumTextExists, bool shortTextExists)
		{
			if (textTableExists)
			{
				if (longTextExists)
				{
					return "TXTLG";
				}
				if (mediumTextExists)
				{
					return "TXTMD";
				}
				if (shortTextExists)
				{
					return "TXTSH";
				}
			}
			return null;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0007BB70 File Offset: 0x00079D70
		public static string ExtractInfoObject(string infoObject)
		{
			if (string.IsNullOrEmpty(infoObject))
			{
				return null;
			}
			int num = infoObject.IndexOf("__", StringComparison.Ordinal);
			if (num != -1)
			{
				return infoObject.Substring(num + 2);
			}
			return infoObject;
		}

		// Token: 0x040011CD RID: 4557
		private const string AbapDateDataType = "DATS";

		// Token: 0x040011CE RID: 4558
		public const int CharacteristicIndex = 0;

		// Token: 0x040011CF RID: 4559
		public const int InfoObjectIndex = 17;

		// Token: 0x040011D0 RID: 4560
		private const int TextTableExistsIndex = 1;

		// Token: 0x040011D1 RID: 4561
		private const int TextsAreTimeDependentIndex = 2;

		// Token: 0x040011D2 RID: 4562
		private const int MasterDataIsTimeDependentIndex = 3;

		// Token: 0x040011D3 RID: 4563
		private const int TextsAreLanguageIndependentIndex = 4;

		// Token: 0x040011D4 RID: 4564
		private const int ShortTextExistsIndex = 5;

		// Token: 0x040011D5 RID: 4565
		private const int MediumTextExistsIndex = 6;

		// Token: 0x040011D6 RID: 4566
		private const int LongTextExistsIndex = 7;

		// Token: 0x040011D7 RID: 4567
		private const int AbapDataTypeIndex = 8;

		// Token: 0x040011D8 RID: 4568
		private const int TimeIndependentMasterDataTableIndex = 9;

		// Token: 0x040011D9 RID: 4569
		private const int TimeDependentMasterDataTableIndex = 10;

		// Token: 0x040011DA RID: 4570
		private const int TextTableIndex = 11;

		// Token: 0x040011DB RID: 4571
		private const int HierarchyTableIndex = 12;

		// Token: 0x040011DC RID: 4572
		private const int HierachyIntervalTableIndex = 13;

		// Token: 0x040011DD RID: 4573
		private const int ViewOfMasterDataTablesIndex = 14;

		// Token: 0x040011DE RID: 4574
		private const int KeyFigureDecimalsIndex = 15;

		// Token: 0x040011DF RID: 4575
		private const int KeyFigurePresentationIndex = 16;

		// Token: 0x040011E0 RID: 4576
		private const int InfoObjectTypeIndex = 18;

		// Token: 0x040011E1 RID: 4577
		private const int CharacteristicReferencesAnother = 19;

		// Token: 0x040011E2 RID: 4578
		private const int InternalLengthIndex = 20;
	}
}
