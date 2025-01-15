using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200000D RID: 13
	internal class FormatStringProvider
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00003D3C File Offset: 0x00001F3C
		public FormatStringProvider(SapBwConnection connection, string cubeName, List<MdxColumn> columns)
		{
			this.columns = columns;
			this.settings = new FormatStringProvider.FormatStringSettings(connection, cubeName, columns);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003D5C File Offset: 0x00001F5C
		public object ComputeFormatString(object value, string currencyOrUnit, string cellStatus, int measureIndex, bool isVirtual = false)
		{
			int? num = null;
			SapBwDataType valueOrDefault = this.columns[measureIndex].ValueType.GetValueOrDefault(SapBwDataType.Fltp);
			if (valueOrDefault == SapBwDataType.Dats)
			{
				return this.settings.DateFormat;
			}
			if (valueOrDefault == SapBwDataType.Tims)
			{
				return this.settings.TimeFormat;
			}
			if (value == DBNull.Value || !(value is decimal) || cellStatus == "E")
			{
				return string.Empty;
			}
			decimal num2 = (decimal)value;
			if (num2 == 0m)
			{
				if (this.settings.ZeroPresentation == FormatStringProvider.ZeroPresentation.ZeroWithoutUnit)
				{
					return FormatStringProvider.GetBaseFormat(new int?(0), valueOrDefault);
				}
				if (this.settings.ZeroPresentation == FormatStringProvider.ZeroPresentation.ZeroAsString && this.settings.ZeroAsString != null)
				{
					return this.settings.ZeroAsString;
				}
			}
			bool flag = this.settings.SignPresentation == FormatStringProvider.SignPresentation.Before && (num2 != 0m || this.settings.ZeroPresentation != FormatStringProvider.ZeroPresentation.ZeroWithUnit) && !isVirtual;
			string text = string.Empty;
			string text2 = string.Empty;
			if (!string.IsNullOrEmpty(currencyOrUnit))
			{
				if (!flag)
				{
					num = new int?(0);
				}
				if (currencyOrUnit == this.settings.MixedCurrencySymbol && !this.settings.ShowMixedCurrencyValues)
				{
					return FormatStringProvider.Quote(this.settings.MixedCurrencySymbol, '"', '"');
				}
				FormatStringProvider.AltCurrencyDisplay altCurrencyDisplay;
				if (this.settings.AlternateCurrencyDisplay.TryGetValue(currencyOrUnit, out altCurrencyDisplay))
				{
					if (altCurrencyDisplay.UnitsPresentation == FormatStringProvider.UnitsPresentation.After)
					{
						num = null;
						text = text + " " + FormatStringProvider.Quote(altCurrencyDisplay.Text, '"', '"');
					}
					else
					{
						text2 = FormatStringProvider.Quote(altCurrencyDisplay.Text, '"', '"') + " ";
					}
				}
				else
				{
					num = null;
					text = text + " " + FormatStringProvider.Quote(currencyOrUnit, '"', '"');
				}
				int num3;
				if (num == null && this.settings.PerCurrencyDecimalsOverride.TryGetValue(currencyOrUnit, out num3))
				{
					num = new int?(num3);
				}
			}
			if (num == null)
			{
				int num3;
				num = new int?(this.settings.MeasureDecimals.TryGetValue(measureIndex, out num3) ? num3 : this.columns[measureIndex].Precision.GetValueOrDefault(2));
			}
			if (isVirtual)
			{
				text2 = "[" + text2;
				text += "]";
			}
			if (!flag)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2};{0}{3}{2}", new object[]
				{
					text2,
					FormatStringProvider.GetBaseFormat(num, valueOrDefault),
					text,
					this.GetNegativeFormat(num, valueOrDefault)
				});
			}
			return string.Join(string.Empty, new string[]
			{
				text2,
				FormatStringProvider.GetBaseFormat(num, valueOrDefault),
				text
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004024 File Offset: 0x00002224
		private string GetNegativeFormat(int? precision, SapBwDataType valueType)
		{
			FormatStringProvider.SignPresentation signPresentation = this.settings.SignPresentation;
			if (signPresentation == FormatStringProvider.SignPresentation.After)
			{
				return FormatStringProvider.GetBaseFormat(precision, valueType) + "-";
			}
			if (signPresentation != FormatStringProvider.SignPresentation.Brackets)
			{
				return "- " + FormatStringProvider.GetBaseFormat(precision, valueType);
			}
			return FormatStringProvider.Quote(FormatStringProvider.GetBaseFormat(precision, valueType), '(', ')');
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000407C File Offset: 0x0000227C
		private static string GetBaseFormat(int? precision, SapBwDataType dataType)
		{
			if (dataType != SapBwDataType.Fltp && dataType != SapBwDataType.Char && dataType != SapBwDataType.Int4)
			{
				return "0.000000000E+00";
			}
			if (precision != null && precision.Value >= 0 && precision.Value <= FormatStringProvider.baseFormats.Length)
			{
				return FormatStringProvider.baseFormats[precision.Value];
			}
			return FormatStringProvider.baseFormats[8];
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000040D5 File Offset: 0x000022D5
		private static string Quote(string value, char openQuote = '"', char closeQuote = '"')
		{
			return openQuote.ToString() + value + closeQuote.ToString();
		}

		// Token: 0x04000026 RID: 38
		private static readonly string[] baseFormats = new string[]
		{
			"#,##0", "#,##0.0", "#,##0.00", "#,##0.000", "#,##0.0000", "#,##0.00000", "#,##0.000000", "#,##0.0000000", "#,##0.00000000", "#,##0.000000000",
			"#,##0.0000000000", "#,##0.00000000000", "#,##0.000000000000", "#,##0.0000000000000", "#,##0.00000000000000"
		};

		// Token: 0x04000027 RID: 39
		private readonly List<MdxColumn> columns;

		// Token: 0x04000028 RID: 40
		private readonly FormatStringProvider.FormatStringSettings settings;

		// Token: 0x0200004B RID: 75
		private class FormatStringSettings
		{
			// Token: 0x06000317 RID: 791 RVA: 0x0000C578 File Offset: 0x0000A778
			public FormatStringSettings(SapBwConnection connection, string cubeName, List<MdxColumn> columns)
			{
				this.connection = connection;
				this.columns = columns;
				this.SignPresentation = FormatStringProvider.SignPresentation.Before;
				this.ZeroPresentation = FormatStringProvider.ZeroPresentation.ZeroWithUnit;
				string text;
				string text2;
				if (FormatStringProvider.FormatStringSettings.TryExtractQueryName(cubeName, out text) && this.TryGetCubeId(text, out text2))
				{
					this.GetPresentationInfo(text2);
				}
				this.ObtainCustomizations();
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000318 RID: 792 RVA: 0x0000C5C9 File Offset: 0x0000A7C9
			public string DateFormat
			{
				get
				{
					return this.connection.SapBwUser.DateFormat;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C5DB File Offset: 0x0000A7DB
			public string TimeFormat
			{
				get
				{
					return this.connection.SapBwUser.TimeFormat;
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C5ED File Offset: 0x0000A7ED
			// (set) Token: 0x0600031B RID: 795 RVA: 0x0000C5F5 File Offset: 0x0000A7F5
			public string MixedCurrencySymbol { get; private set; }

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C5FE File Offset: 0x0000A7FE
			// (set) Token: 0x0600031D RID: 797 RVA: 0x0000C606 File Offset: 0x0000A806
			public bool ShowMixedCurrencyValues { get; private set; }

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C60F File Offset: 0x0000A80F
			// (set) Token: 0x0600031F RID: 799 RVA: 0x0000C617 File Offset: 0x0000A817
			public FormatStringProvider.SignPresentation SignPresentation { get; private set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000320 RID: 800 RVA: 0x0000C620 File Offset: 0x0000A820
			// (set) Token: 0x06000321 RID: 801 RVA: 0x0000C628 File Offset: 0x0000A828
			public FormatStringProvider.ZeroPresentation ZeroPresentation { get; private set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000322 RID: 802 RVA: 0x0000C631 File Offset: 0x0000A831
			// (set) Token: 0x06000323 RID: 803 RVA: 0x0000C639 File Offset: 0x0000A839
			public string ZeroAsString { get; private set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C644 File Offset: 0x0000A844
			public Dictionary<string, FormatStringProvider.AltCurrencyDisplay> AlternateCurrencyDisplay
			{
				get
				{
					if (this.alternateCurrencyDisplay == null)
					{
						this.alternateCurrencyDisplay = new Dictionary<string, FormatStringProvider.AltCurrencyDisplay>();
						this.alternateCurrencyDisplay["DEM"] = new FormatStringProvider.AltCurrencyDisplay
						{
							Text = "DM",
							UnitsPresentation = FormatStringProvider.UnitsPresentation.After
						};
						this.alternateCurrencyDisplay["USD"] = new FormatStringProvider.AltCurrencyDisplay
						{
							Text = "$",
							UnitsPresentation = FormatStringProvider.UnitsPresentation.Before
						};
						this.alternateCurrencyDisplay["GBP"] = new FormatStringProvider.AltCurrencyDisplay
						{
							Text = "£",
							UnitsPresentation = FormatStringProvider.UnitsPresentation.Before
						};
						foreach (object[] array in new TableQuery(this.connection)
						{
							Table = "RSRCURRDISP",
							Fields = "WAERS,TEXT,FLAG"
						}.EnumerateAndCacheTable(true))
						{
							string text = array[0] as string;
							if (!string.IsNullOrEmpty(text))
							{
								string text2 = array[1] as string;
								this.alternateCurrencyDisplay[text] = new FormatStringProvider.AltCurrencyDisplay
								{
									Text = ((!string.IsNullOrEmpty(text2)) ? text2 : text),
									UnitsPresentation = ((array[2] as string == "B") ? FormatStringProvider.UnitsPresentation.Before : FormatStringProvider.UnitsPresentation.After)
								};
							}
						}
					}
					return this.alternateCurrencyDisplay;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C798 File Offset: 0x0000A998
			public Dictionary<string, int> PerCurrencyDecimalsOverride
			{
				get
				{
					if (this.perCurrencyDecimalsOverride == null)
					{
						this.perCurrencyDecimalsOverride = new Dictionary<string, int>();
						foreach (object[] array in new TableQuery(this.connection)
						{
							Table = "TCURX",
							Fields = "CURRKEY,CURRDEC"
						}.EnumerateAndCacheTable(true))
						{
							string text = array[0] as string;
							if (!string.IsNullOrEmpty(text))
							{
								int num;
								this.perCurrencyDecimalsOverride[text] = (int.TryParse(array[1] as string, NumberStyles.Number, CultureInfo.InvariantCulture, out num) ? num : 0);
							}
						}
					}
					return this.perCurrencyDecimalsOverride;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C854 File Offset: 0x0000AA54
			public Dictionary<int, int> MeasureDecimals
			{
				get
				{
					if (this.measureDecimals == null)
					{
						this.measureDecimals = new Dictionary<int, int>();
						Dictionary<string, int> dictionary = new Dictionary<string, int>();
						Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
						foreach (KeyValuePair<string, int> keyValuePair in from c in this.columns
							where c.IsKeyFigure
							select new KeyValuePair<string, int>(Utils.ExtractMeasureName(c.ColumnName), c.ColumnOrdinal))
						{
							if (keyValuePair.Key.Length >= 25)
							{
								dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
							}
							else
							{
								dictionary.Add(keyValuePair.Key, keyValuePair.Value);
							}
						}
						if (dictionary2.Any<KeyValuePair<string, int>>())
						{
							TableQuery tableQuery = new TableQuery(this.connection);
							tableQuery.Table = "RSZELTDIR";
							tableQuery.Fields = "ELTUID,DEFAULTHINT";
							tableQuery.Where = "ELTUID IN (" + string.Join(", ", dictionary2.Keys.Select((string e) => "'" + e + "'")) + ") AND OBJVERS = 'A'";
							foreach (object[] array in tableQuery.EnumerateAndCacheTable(true))
							{
								string enterpriseId = array[0] as string;
								string text = array[1] as string;
								if (!string.IsNullOrEmpty(enterpriseId) && !string.IsNullOrEmpty(text))
								{
									int num;
									if (dictionary2.TryGetValue(enterpriseId, out num))
									{
										dictionary.Add(text, dictionary2[enterpriseId]);
									}
									else
									{
										KeyValuePair<string, int> keyValuePair2 = dictionary2.FirstOrDefault((KeyValuePair<string, int> id) => id.Key.StartsWith(enterpriseId, StringComparison.Ordinal));
										if (!keyValuePair2.Equals(default(KeyValuePair<string, int>)))
										{
											dictionary.Add(text, keyValuePair2.Value);
										}
									}
								}
							}
						}
						if (dictionary.Any<KeyValuePair<string, int>>())
						{
							TableQuery tableQuery2 = new TableQuery(this.connection);
							tableQuery2.Table = "RSDKYF";
							tableQuery2.Fields = "KYFNM,KYFDECIM";
							tableQuery2.Where = "KYFNM IN (" + string.Join(", ", dictionary.Keys.Select((string e) => "'" + e + "'")) + ") AND OBJVERS = 'A' AND KYFTP = 'QUA'";
							foreach (object[] array2 in tableQuery2.EnumerateAndCacheTable(true))
							{
								string text2 = array2[0] as string;
								int num2;
								if (!string.IsNullOrEmpty(text2) && dictionary.TryGetValue(text2, out num2))
								{
									string text3 = array2[1] as string;
									int num3;
									if (string.IsNullOrWhiteSpace(text3) || text3 == string.Empty)
									{
										this.measureDecimals[num2] = 0;
									}
									else if (int.TryParse(text3, NumberStyles.Number, CultureInfo.InvariantCulture, out num3))
									{
										this.measureDecimals[num2] = num3;
									}
								}
							}
						}
					}
					return this.measureDecimals;
				}
			}

			// Token: 0x06000327 RID: 807 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
			private static bool TryExtractQueryName(string name, out string queryName)
			{
				if (!string.IsNullOrEmpty(name))
				{
					int num = name.IndexOf('/');
					if (num > 0)
					{
						queryName = name.Substring(num + 1);
						return true;
					}
				}
				queryName = null;
				return false;
			}

			// Token: 0x06000328 RID: 808 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
			private bool TryGetCubeId(string queryName, out string cubeId)
			{
				object[] array;
				if (new TableQuery(this.connection)
				{
					Table = "RSZCOMPDIR",
					Fields = "COMPUID,COMPID",
					Where = string.Format(CultureInfo.InvariantCulture, "COMPID = '{0}'", queryName)
				}.TryExtractSingleRowUsingCache(true, out array))
				{
					cubeId = array[0] as string;
					return !string.IsNullOrEmpty(cubeId);
				}
				cubeId = null;
				return false;
			}

			// Token: 0x06000329 RID: 809 RVA: 0x0000CC60 File Offset: 0x0000AE60
			private void GetPresentationInfo(string cubeId)
			{
				object[] array;
				if (new TableQuery(this.connection)
				{
					Table = "RSZELTPROP",
					Fields = "ELTUID,SIGNPRSNT,ZEROPRSNT,ZEROSTRING",
					Where = string.Format(CultureInfo.InvariantCulture, "ELTUID = '{0}'", cubeId)
				}.TryExtractSingleRowUsingCache(true, out array))
				{
					this.SignPresentation = FormatStringProvider.FormatStringSettings.ParseSignPresentation(array[1] as string);
					this.ZeroPresentation = FormatStringProvider.FormatStringSettings.ParseZeroPresentation(array[2] as string);
					if (this.ZeroPresentation == FormatStringProvider.ZeroPresentation.ZeroAsString)
					{
						this.ZeroAsString = array[3] as string;
					}
				}
			}

			// Token: 0x0600032A RID: 810 RVA: 0x0000CCEC File Offset: 0x0000AEEC
			private void ObtainCustomizations()
			{
				object[] array;
				if (new TableQuery(this.connection)
				{
					Table = "RSADMINC",
					Fields = "RRXDATAMIXCUR,RRXSHOWMIXCURVAL",
					RowCount = new int?(1)
				}.TryExtractSingleRowUsingCache(true, out array))
				{
					string text = array[0] as string;
					this.MixedCurrencySymbol = (string.IsNullOrEmpty(text) ? "*" : text);
					string text2 = array[1] as string;
					this.ShowMixedCurrencyValues = !string.IsNullOrEmpty(text2) && Utils.ToBoolean(text2[0]);
					return;
				}
				this.MixedCurrencySymbol = "*";
				this.ShowMixedCurrencyValues = false;
			}

			// Token: 0x0600032B RID: 811 RVA: 0x0000CD88 File Offset: 0x0000AF88
			private static FormatStringProvider.SignPresentation ParseSignPresentation(string value)
			{
				if (value == "2")
				{
					return FormatStringProvider.SignPresentation.After;
				}
				if (!(value == "3"))
				{
					return FormatStringProvider.SignPresentation.Before;
				}
				return FormatStringProvider.SignPresentation.Brackets;
			}

			// Token: 0x0600032C RID: 812 RVA: 0x0000CDAB File Offset: 0x0000AFAB
			private static FormatStringProvider.ZeroPresentation ParseZeroPresentation(string value)
			{
				if (value == "1")
				{
					return FormatStringProvider.ZeroPresentation.ZeroWithoutUnit;
				}
				if (value == "2")
				{
					return FormatStringProvider.ZeroPresentation.ZeroAsSpace;
				}
				if (!(value == "3"))
				{
					return FormatStringProvider.ZeroPresentation.ZeroWithUnit;
				}
				return FormatStringProvider.ZeroPresentation.ZeroAsString;
			}

			// Token: 0x04000216 RID: 534
			private const string DefaultMixedCurrencySymbol = "*";

			// Token: 0x04000217 RID: 535
			private readonly SapBwConnection connection;

			// Token: 0x04000218 RID: 536
			private readonly List<MdxColumn> columns;

			// Token: 0x04000219 RID: 537
			private Dictionary<string, FormatStringProvider.AltCurrencyDisplay> alternateCurrencyDisplay;

			// Token: 0x0400021A RID: 538
			private Dictionary<string, int> perCurrencyDecimalsOverride;

			// Token: 0x0400021B RID: 539
			private Dictionary<int, int> measureDecimals;
		}

		// Token: 0x0200004C RID: 76
		private class AltCurrencyDisplay
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600032D RID: 813 RVA: 0x0000CDDD File Offset: 0x0000AFDD
			// (set) Token: 0x0600032E RID: 814 RVA: 0x0000CDE5 File Offset: 0x0000AFE5
			public FormatStringProvider.UnitsPresentation UnitsPresentation { get; set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x0600032F RID: 815 RVA: 0x0000CDEE File Offset: 0x0000AFEE
			// (set) Token: 0x06000330 RID: 816 RVA: 0x0000CDF6 File Offset: 0x0000AFF6
			public string Text { get; set; }
		}

		// Token: 0x0200004D RID: 77
		private enum SignPresentation
		{
			// Token: 0x04000224 RID: 548
			Before = 1,
			// Token: 0x04000225 RID: 549
			After,
			// Token: 0x04000226 RID: 550
			Brackets
		}

		// Token: 0x0200004E RID: 78
		private enum ZeroPresentation
		{
			// Token: 0x04000228 RID: 552
			ZeroWithUnit,
			// Token: 0x04000229 RID: 553
			ZeroWithoutUnit,
			// Token: 0x0400022A RID: 554
			ZeroAsSpace,
			// Token: 0x0400022B RID: 555
			ZeroAsString
		}

		// Token: 0x0200004F RID: 79
		private enum UnitsPresentation
		{
			// Token: 0x0400022D RID: 557
			After,
			// Token: 0x0400022E RID: 558
			Before
		}
	}
}
