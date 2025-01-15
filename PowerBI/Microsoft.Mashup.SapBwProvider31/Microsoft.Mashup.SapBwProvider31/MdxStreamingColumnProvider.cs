using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000028 RID: 40
	internal sealed class MdxStreamingColumnProvider : MdxColumnProvider
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000880E File Offset: 0x00006A0E
		public MdxStreamingColumnProvider(SapBwCommand command, MdxCommand mdxCommand, string cubeName)
			: base(command, mdxCommand, cubeName)
		{
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008819 File Offset: 0x00006A19
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00008821 File Offset: 0x00006A21
		public IRfcTable SubGroups { get; private set; }

		// Token: 0x060001EE RID: 494 RVA: 0x0000882C File Offset: 0x00006A2C
		private Dictionary<string, List<string>> GetAxisInfo(string datasetId)
		{
			IRfcFunction function = this.connection.GetFunction("BAPI_MDDATASET_GET_AXIS_INFO", true);
			function.SetValue("DATASETID", datasetId);
			this.connection.InvokeFunction(function, true, this.command, true);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (IRfcStructure rfcStructure in function.GetTable("AXIS_DIMENSIONS"))
			{
				dictionary[rfcStructure["DIM_KEY"].GetString()] = rfcStructure["DIM_UNAM"].GetString();
			}
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (IRfcStructure rfcStructure2 in function.GetTable("AXIS_LEVELS"))
			{
				string text;
				if (dictionary.TryGetValue(rfcStructure2["DIM_KEY"].GetString(), out text) && !hashSet.Contains(text))
				{
					dictionary2[rfcStructure2["LVL_KEY"].GetString()] = text;
					hashSet.Add(text);
				}
			}
			Dictionary<string, List<string>> dictionary3 = new Dictionary<string, List<string>>();
			foreach (IRfcStructure rfcStructure3 in function.GetTable("DIM_PRPTYS"))
			{
				string text2;
				if (dictionary2.TryGetValue(rfcStructure3["LVL_KEY"].GetString(), out text2))
				{
					string @string = rfcStructure3["PRPTY_NAM"].GetString();
					List<string> list;
					if (dictionary3.TryGetValue(text2, out list))
					{
						list.Add(@string);
					}
					else
					{
						dictionary3[text2] = new List<string> { @string };
					}
				}
			}
			return dictionary3;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008A18 File Offset: 0x00006C18
		public override bool TryRetrieveColumns(string datasetId, ColumnInfos columnInfos)
		{
			IRfcFunction function = this.connection.GetFunction("BAPI_MDDATASET_GET_STREAMINFO", true);
			function.SetValue("DATASETID", datasetId);
			this.connection.InvokeFunction(function, true, this.command, true);
			if (MdxStreamingColumnProvider.MeasuresOnly(function.GetTable("SUBGROUPS")))
			{
				return false;
			}
			Dictionary<string, List<string>> axisInfo = this.GetAxisInfo(datasetId);
			Dictionary<int, MdxStreamingColumnProvider.SapBwGroup> dictionary = new Dictionary<int, MdxStreamingColumnProvider.SapBwGroup>();
			foreach (IRfcStructure rfcStructure in function.GetTable("GROUPS"))
			{
				MdxStreamingColumnProvider.SapBwGroup sapBwGroup = new MdxStreamingColumnProvider.SapBwGroup
				{
					Ordinal = rfcStructure["ORDINAL"].GetInt(),
					Dimension = rfcStructure["DIMENSION"].GetString(),
					Description = rfcStructure["DSCRPTN"].GetString(),
					Measure = rfcStructure["MEASURE"].GetString()
				};
				List<string> list;
				if (axisInfo.TryGetValue(sapBwGroup.Dimension, out list))
				{
					sapBwGroup.Properties = list;
				}
				dictionary[sapBwGroup.Ordinal] = sapBwGroup;
			}
			this.SubGroups = function.GetTable("SUBGROUPS");
			int num = 0;
			MdxStreamingColumnProvider.SapBwGroup sapBwGroup2 = null;
			Dictionary<string, string[]> dictionary2 = new Dictionary<string, string[]>();
			foreach (IRfcStructure rfcStructure2 in this.SubGroups)
			{
				int @int = rfcStructure2["ORDINAL"].GetInt();
				if (sapBwGroup2 == null || sapBwGroup2.Ordinal != @int)
				{
					sapBwGroup2 = dictionary[@int];
					num = 0;
				}
				else if (num == sapBwGroup2.Properties.Count)
				{
					num = 0;
				}
				string text = null;
				char @char = rfcStructure2["SUBTYPE"].GetChar();
				if (@char == 'C' || @char == 'U' || @char == 'M')
				{
					string text2;
					text = ((!MdxStreamingColumnProvider.subtypeNames.TryGetValue(@char, out text2)) ? sapBwGroup2.Measure : string.Format(CultureInfo.InvariantCulture, "{0}.{1}", sapBwGroup2.Measure, text2));
					if (columnInfos != null && @char == 'M' && columnInfos.ColumnNames.Contains(Utils.BuildColumnName(text, "FORMAT_STRING")))
					{
						dictionary2.Add(text, new string[] { Utils.BuildColumnName(text, "UNIT_OF_MEASURE") });
					}
				}
				if (text == null)
				{
					string @string = rfcStructure2["LVL_UNAM"].GetString();
					string text3 = sapBwGroup2.Properties[num];
					text = (string.IsNullOrEmpty(text3) ? @string : string.Format(CultureInfo.InvariantCulture, "{0}.{1}", @string, text3));
				}
				this.Add(new MdxColumn((@char == 'M') ? MdxColumnType.KeyFigureValue : MdxColumnType.MandatoryDimensionProperty, text, rfcStructure2["DATA_TYPE"].GetString(), rfcStructure2["MAX_LENGTH"].GetInt(), null, null, -1));
				num++;
			}
			base.AddFormatStringColumns(dictionary2);
			return true;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008D3C File Offset: 0x00006F3C
		private static bool MeasuresOnly(IRfcTable subGroups)
		{
			foreach (IRfcStructure rfcStructure in subGroups)
			{
				char @char = rfcStructure["SUBTYPE"].GetChar();
				if (@char != 'M' && @char != 'C' && @char != 'U')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public override DbDataReader GetReader()
		{
			return new MdxStreamingDataReader(this.command, this.mdxCommand, this);
		}

		// Token: 0x040000D4 RID: 212
		private const char CharacteristicAsKey = 'K';

		// Token: 0x040000D5 RID: 213
		private const char CharacteristicAsText = 'D';

		// Token: 0x040000D6 RID: 214
		private const char KeyFigureValue = 'M';

		// Token: 0x040000D7 RID: 215
		private const char KeyFigureCurrency = 'C';

		// Token: 0x040000D8 RID: 216
		private const char KeyFigureUnit = 'U';

		// Token: 0x040000D9 RID: 217
		private const char AttributeAsKey = 'Q';

		// Token: 0x040000DA RID: 218
		private const char AttributeAsText = 'P';

		// Token: 0x040000DB RID: 219
		private const char JumpKey = ' ';

		// Token: 0x040000DC RID: 220
		private static readonly Dictionary<char, string> subtypeNames = new Dictionary<char, string>
		{
			{ 'C', "UNIT_OF_MEASURE" },
			{ 'U', "UNIT_OF_MEASURE" }
		};

		// Token: 0x0200006B RID: 107
		private class SapBwGroup
		{
			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000F747 File Offset: 0x0000D947
			// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000F74F File Offset: 0x0000D94F
			public int Ordinal { get; set; }

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000F758 File Offset: 0x0000D958
			// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000F760 File Offset: 0x0000D960
			public string Dimension { get; set; }

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000F769 File Offset: 0x0000D969
			// (set) Token: 0x060003DA RID: 986 RVA: 0x0000F771 File Offset: 0x0000D971
			public string Measure { get; set; }

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060003DB RID: 987 RVA: 0x0000F77A File Offset: 0x0000D97A
			// (set) Token: 0x060003DC RID: 988 RVA: 0x0000F782 File Offset: 0x0000D982
			public string Description { get; set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060003DD RID: 989 RVA: 0x0000F78B File Offset: 0x0000D98B
			// (set) Token: 0x060003DE RID: 990 RVA: 0x0000F793 File Offset: 0x0000D993
			public List<string> Properties { get; set; }
		}
	}
}
