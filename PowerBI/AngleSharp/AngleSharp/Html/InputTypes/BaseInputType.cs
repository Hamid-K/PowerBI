using System;
using System.Globalization;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000CF RID: 207
	internal abstract class BaseInputType
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0002F075 File Offset: 0x0002D275
		public BaseInputType(IHtmlInputElement input, string name, bool validate)
		{
			this._input = input;
			this._validate = validate;
			this._name = name;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0002F092 File Offset: 0x0002D292
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0002F09A File Offset: 0x0002D29A
		public bool CanBeValidated
		{
			get
			{
				return this._validate;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0002F0A2 File Offset: 0x0002D2A2
		public IHtmlInputElement Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0002F0AA File Offset: 0x0002D2AA
		public virtual bool IsAppendingData(IHtmlElement submitter)
		{
			return true;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00003C25 File Offset: 0x00001E25
		public virtual void Check(ValidityState state)
		{
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0002F0B0 File Offset: 0x0002D2B0
		public virtual double? ConvertToNumber(string value)
		{
			return null;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002F0C6 File Offset: 0x0002D2C6
		public virtual string ConvertFromNumber(double value)
		{
			throw new DomException(DomError.InvalidState);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
		public virtual DateTime? ConvertToDate(string value)
		{
			return null;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002F0C6 File Offset: 0x0002D2C6
		public virtual string ConvertFromDate(DateTime value)
		{
			throw new DomException(DomError.InvalidState);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002F0E6 File Offset: 0x0002D2E6
		public virtual void ConstructDataSet(FormDataSet dataSet)
		{
			dataSet.Append(this._input.Name, this._input.Value, this._input.Type);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002F0C6 File Offset: 0x0002D2C6
		public virtual void DoStep(int n)
		{
			throw new DomException(DomError.InvalidState);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002F110 File Offset: 0x0002D310
		protected bool IsStepMismatch()
		{
			double step = this.GetStep();
			double? num = this.ConvertToNumber(this._input.Value);
			double stepBase = this.GetStepBase();
			return step != 0.0 && (num - stepBase) % step != 0.0;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002F1C4 File Offset: 0x0002D3C4
		protected double GetStep()
		{
			string step = this._input.Step;
			if (string.IsNullOrEmpty(step))
			{
				return this.GetDefaultStep() * this.GetStepScaleFactor();
			}
			if (step.Isi(Keywords.Any))
			{
				return 0.0;
			}
			double? num = BaseInputType.ToNumber(step);
			if (num == null || num <= 0.0)
			{
				return this.GetDefaultStep() * this.GetStepScaleFactor();
			}
			return num.Value * this.GetStepScaleFactor();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002F258 File Offset: 0x0002D458
		private double GetStepBase()
		{
			double? num = this.ConvertToNumber(this._input.Minimum);
			if (num != null)
			{
				return num.Value;
			}
			num = this.ConvertToNumber(this._input.DefaultValue);
			if (num != null)
			{
				return num.Value;
			}
			return this.GetDefaultStepBase();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002F2B1 File Offset: 0x0002D4B1
		protected virtual double GetDefaultStepBase()
		{
			return 0.0;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected virtual double GetDefaultStep()
		{
			return 1.0;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected virtual double GetStepScaleFactor()
		{
			return 1.0;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		protected static bool IsInvalidPattern(string pattern, string value)
		{
			if (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(value))
			{
				try
				{
					return !new Regex(pattern, RegexOptions.ECMAScript).IsMatch(value);
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002F314 File Offset: 0x0002D514
		protected static double? ToNumber(string value)
		{
			if (!string.IsNullOrEmpty(value) && BaseInputType.Number.IsMatch(value))
			{
				return new double?(double.Parse(value, CultureInfo.InvariantCulture));
			}
			return null;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002F350 File Offset: 0x0002D550
		protected static TimeSpan? ToTime(string value, ref int position)
		{
			int num = position;
			int num2 = 0;
			int num3 = 0;
			if (value.Length >= 5 + num)
			{
				int num4 = position;
				position = num4 + 1;
				if (value[num4].IsDigit())
				{
					num4 = position;
					position = num4 + 1;
					if (value[num4].IsDigit())
					{
						num4 = position;
						position = num4 + 1;
						if (value[num4] == ':')
						{
							int num5 = int.Parse(value.Substring(num, 2));
							if (BaseInputType.IsLegalHour(num5))
							{
								num4 = position;
								position = num4 + 1;
								if (value[num4].IsDigit())
								{
									num4 = position;
									position = num4 + 1;
									if (value[num4].IsDigit())
									{
										int num6 = int.Parse(value.Substring(3 + num, 2));
										if (!BaseInputType.IsLegalMinute(num6))
										{
											return null;
										}
										if (value.Length >= 8 + num && value[position] == ':')
										{
											position++;
											num4 = position;
											position = num4 + 1;
											if (value[num4].IsDigit())
											{
												num4 = position;
												position = num4 + 1;
												if (value[num4].IsDigit())
												{
													num2 = int.Parse(value.Substring(6 + num, 2));
													if (!BaseInputType.IsLegalSecond(num2))
													{
														return null;
													}
													if (position + 1 < value.Length && value[position] == '.')
													{
														position++;
														int num7 = position;
														while (position < value.Length && value[position].IsDigit())
														{
															position++;
														}
														string text = value.Substring(num7, position - num7);
														num3 = int.Parse(text) * (int)Math.Pow(10.0, (double)(3 - text.Length));
														goto IL_01C2;
													}
													goto IL_01C2;
												}
											}
											return null;
										}
										IL_01C2:
										return new TimeSpan?(new TimeSpan(0, num5, num6, num2, num3));
									}
								}
							}
							return null;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002F53B File Offset: 0x0002D73B
		protected static int GetWeekOfYear(DateTime value)
		{
			return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002F54F File Offset: 0x0002D74F
		protected static bool IsLegalHour(int value)
		{
			return value >= 0 && value <= 23;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002F55F File Offset: 0x0002D75F
		protected static bool IsLegalSecond(int value)
		{
			return value >= 0 && value <= 59;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002F55F File Offset: 0x0002D75F
		protected static bool IsLegalMinute(int value)
		{
			return value >= 0 && value <= 59;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002F56F File Offset: 0x0002D76F
		protected static bool IsLegalMonth(int value)
		{
			return value >= 1 && value <= 12;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002F57F File Offset: 0x0002D77F
		protected static bool IsLegalYear(int value)
		{
			return value >= 0 && value <= 9999;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002F594 File Offset: 0x0002D794
		protected static bool IsLegalDay(int day, int month, int year)
		{
			if (BaseInputType.IsLegalYear(year) && BaseInputType.IsLegalMonth(month))
			{
				Calendar calendar = CultureInfo.InvariantCulture.Calendar;
				return day >= 1 && day <= calendar.GetDaysInMonth(year, month);
			}
			return false;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002F5D4 File Offset: 0x0002D7D4
		protected static bool IsLegalWeek(int week, int year)
		{
			if (BaseInputType.IsLegalYear(year))
			{
				int weekOfYear = BaseInputType.GetWeekOfYear(new DateTime(year, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc));
				return week >= 0 && week < weekOfYear;
			}
			return false;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002F60A File Offset: 0x0002D80A
		protected static bool IsTimeSeparator(char chr)
		{
			return chr == ' ' || chr == 'T';
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002F618 File Offset: 0x0002D818
		protected static int FetchDigits(string value)
		{
			int num = 0;
			while (num < value.Length && value[num].IsDigit())
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002F648 File Offset: 0x0002D848
		protected static bool PositionIsValidForDateTime(string value, int position)
		{
			return position >= 4 && position <= value.Length - 13 && value[position] == '-' && value[position + 1].IsDigit() && value[position + 2].IsDigit() && value[position + 3] == '-' && value[position + 4].IsDigit() && value[position + 5].IsDigit();
		}

		// Token: 0x040005FA RID: 1530
		protected static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x040005FB RID: 1531
		protected static readonly Regex Number = new Regex("^\\-?\\d+(\\.\\d+)?([eE][\\-\\+]?\\d+)?$");

		// Token: 0x040005FC RID: 1532
		private readonly IHtmlInputElement _input;

		// Token: 0x040005FD RID: 1533
		private readonly bool _validate;

		// Token: 0x040005FE RID: 1534
		private readonly string _name;
	}
}
