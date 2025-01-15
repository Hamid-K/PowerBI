using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x0200177C RID: 6012
	public class ColumnDetail
	{
		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x0600C745 RID: 51013 RVA: 0x002AD30C File Offset: 0x002AB50C
		// (set) Token: 0x0600C746 RID: 51014 RVA: 0x002AD314 File Offset: 0x002AB514
		public bool AllDateTime { get; set; }

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x0600C747 RID: 51015 RVA: 0x002AD31D File Offset: 0x002AB51D
		// (set) Token: 0x0600C748 RID: 51016 RVA: 0x002AD325 File Offset: 0x002AB525
		public bool AllDecimal { get; set; }

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x0600C749 RID: 51017 RVA: 0x002AD32E File Offset: 0x002AB52E
		// (set) Token: 0x0600C74A RID: 51018 RVA: 0x002AD336 File Offset: 0x002AB536
		public bool AllDouble { get; set; }

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x0600C74B RID: 51019 RVA: 0x002AD33F File Offset: 0x002AB53F
		// (set) Token: 0x0600C74C RID: 51020 RVA: 0x002AD347 File Offset: 0x002AB547
		public bool AllInt { get; set; }

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x0600C74D RID: 51021 RVA: 0x002AD350 File Offset: 0x002AB550
		// (set) Token: 0x0600C74E RID: 51022 RVA: 0x002AD358 File Offset: 0x002AB558
		public bool AllNumber { get; set; }

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x0600C74F RID: 51023 RVA: 0x002AD361 File Offset: 0x002AB561
		// (set) Token: 0x0600C750 RID: 51024 RVA: 0x002AD369 File Offset: 0x002AB569
		public bool AllString { get; set; }

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x0600C751 RID: 51025 RVA: 0x002AD372 File Offset: 0x002AB572
		// (set) Token: 0x0600C752 RID: 51026 RVA: 0x002AD37A File Offset: 0x002AB57A
		public IReadOnlyList<ColumnDataType> DataTypes { get; set; }

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x0600C753 RID: 51027 RVA: 0x002AD383 File Offset: 0x002AB583
		// (set) Token: 0x0600C754 RID: 51028 RVA: 0x002AD38B File Offset: 0x002AB58B
		public bool HasDateTime { get; set; }

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x0600C755 RID: 51029 RVA: 0x002AD394 File Offset: 0x002AB594
		// (set) Token: 0x0600C756 RID: 51030 RVA: 0x002AD39C File Offset: 0x002AB59C
		public bool HasDecimal { get; set; }

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x0600C757 RID: 51031 RVA: 0x002AD3A5 File Offset: 0x002AB5A5
		// (set) Token: 0x0600C758 RID: 51032 RVA: 0x002AD3AD File Offset: 0x002AB5AD
		public bool HasDouble { get; set; }

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x0600C759 RID: 51033 RVA: 0x002AD3B6 File Offset: 0x002AB5B6
		// (set) Token: 0x0600C75A RID: 51034 RVA: 0x002AD3BE File Offset: 0x002AB5BE
		public bool HasInt { get; set; }

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x0600C75B RID: 51035 RVA: 0x002AD3C7 File Offset: 0x002AB5C7
		public bool HasMixedTypes
		{
			get
			{
				return !this.AllString && !this.AllNumber && !this.AllDateTime;
			}
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x0600C75C RID: 51036 RVA: 0x002AD3E4 File Offset: 0x002AB5E4
		public bool? HasNulls
		{
			get
			{
				bool flag = this._hasNulls.GetValueOrDefault();
				bool flag2;
				if (this._hasNulls == null)
				{
					flag = this.Values.Contains(null);
					this._hasNulls = new bool?(flag);
					flag2 = flag;
				}
				else
				{
					flag2 = flag;
				}
				return new bool?(flag2);
			}
		}

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x0600C75D RID: 51037 RVA: 0x002AD42C File Offset: 0x002AB62C
		// (set) Token: 0x0600C75E RID: 51038 RVA: 0x002AD434 File Offset: 0x002AB634
		public bool HasNumber { get; set; }

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x0600C75F RID: 51039 RVA: 0x002AD43D File Offset: 0x002AB63D
		// (set) Token: 0x0600C760 RID: 51040 RVA: 0x002AD445 File Offset: 0x002AB645
		public bool HasString { get; set; }

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x0600C761 RID: 51041 RVA: 0x002AD44E File Offset: 0x002AB64E
		// (set) Token: 0x0600C762 RID: 51042 RVA: 0x002AD456 File Offset: 0x002AB656
		public string Name { get; set; }

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x0600C763 RID: 51043 RVA: 0x002AD460 File Offset: 0x002AB660
		public Type Type
		{
			get
			{
				Type type;
				if ((type = this._type) == null)
				{
					type = (this._type = (this.AllString ? typeof(string) : (this.AllInt ? typeof(int?) : (this.AllDouble ? typeof(double?) : (this.AllDecimal ? typeof(decimal?) : (this.AllDateTime ? typeof(DateTime?) : typeof(object)))))));
				}
				return type;
			}
		}

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x0600C764 RID: 51044 RVA: 0x002AD4EE File Offset: 0x002AB6EE
		// (set) Token: 0x0600C765 RID: 51045 RVA: 0x002AD4F6 File Offset: 0x002AB6F6
		public IReadOnlyList<object> Values { get; set; }

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x0600C766 RID: 51046 RVA: 0x002AD500 File Offset: 0x002AB700
		public IReadOnlyList<decimal> NumberValues
		{
			get
			{
				IReadOnlyList<decimal> readOnlyList;
				if ((readOnlyList = this._numberValues) == null)
				{
					IEnumerable<object> enumerable = this.Values.Where((object v) => v.IsNumeric());
					Func<object, decimal> func;
					if ((func = ColumnDetail.<>O.<0>__ToDecimal) == null)
					{
						func = (ColumnDetail.<>O.<0>__ToDecimal = new Func<object, decimal>(Convert.ToDecimal));
					}
					readOnlyList = (this._numberValues = enumerable.Select(func).ToReadOnlyList<decimal>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600C767 RID: 51047 RVA: 0x002AD570 File Offset: 0x002AB770
		public static ColumnDetail Create(string columnName, IEnumerable<object> values)
		{
			IReadOnlyList<object> readOnlyList = values.ToReadOnlyList<object>();
			int num = readOnlyList.Count((object value) => value == null || value is string);
			int num2 = readOnlyList.Count((object value) => value.IsNumeric());
			int num3 = readOnlyList.Count((object value) => value is int);
			int num4 = readOnlyList.Count((object value) => value is double);
			int num5 = readOnlyList.Count((object value) => value is decimal);
			int num6 = readOnlyList.Count((object value) => value is DateTime);
			List<ColumnDataType> list = new List<ColumnDataType>();
			if (num > 0)
			{
				list.Add(ColumnDataType.String);
			}
			if (num2 > 0)
			{
				list.Add(ColumnDataType.Number);
			}
			if (num3 > 0)
			{
				list.Add(ColumnDataType.Int);
			}
			if (num4 > 0)
			{
				list.Add(ColumnDataType.Double);
			}
			if (num5 > 0)
			{
				list.Add(ColumnDataType.Decimal);
			}
			if (num6 > 0)
			{
				list.Add(ColumnDataType.DateTime);
			}
			return new ColumnDetail
			{
				Name = columnName,
				DataTypes = list,
				Values = readOnlyList,
				AllString = (num == readOnlyList.Count),
				AllNumber = (num2 == readOnlyList.Count),
				AllInt = (num3 == readOnlyList.Count),
				AllDouble = (num4 == readOnlyList.Count),
				AllDecimal = (num5 == readOnlyList.Count),
				AllDateTime = (num6 == readOnlyList.Count),
				HasString = (num > 0),
				HasNumber = (num2 > 0),
				HasInt = (num3 > 0),
				HasDouble = (num4 > 0),
				HasDecimal = (num5 > 0),
				HasDateTime = (num6 > 0)
			};
		}

		// Token: 0x0600C768 RID: 51048 RVA: 0x002AD774 File Offset: 0x002AB974
		public JObject ToJson()
		{
			return JObject.FromObject(new
			{
				Name = this.Name,
				HasMixedTypes = this.HasMixedTypes,
				AllString = this.AllString,
				AllNumber = this.AllNumber,
				AllDateTime = this.AllDateTime,
				AllInt = this.AllInt,
				AllDouble = this.AllDouble,
				AllDecimal = this.AllDecimal,
				HasString = this.HasString,
				HasNumber = this.HasNumber,
				HasDateTime = this.HasDateTime,
				HasInt = this.HasInt,
				HasDouble = this.HasDouble,
				HasDecimal = this.HasDecimal,
				Types = this.DataTypes.Select((ColumnDataType t) => t.ToString()),
				Values = this.Values
			});
		}

		// Token: 0x0600C769 RID: 51049 RVA: 0x002AD80F File Offset: 0x002ABA0F
		public override string ToString()
		{
			return this.Name + ", " + this.Type.CsName(true);
		}

		// Token: 0x04004E5C RID: 20060
		private bool? _hasNulls;

		// Token: 0x04004E5D RID: 20061
		private Type _type;

		// Token: 0x04004E5E RID: 20062
		private IReadOnlyList<decimal> _numberValues;

		// Token: 0x0200177D RID: 6013
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004E6E RID: 20078
			public static Func<object, decimal> <0>__ToDecimal;
		}
	}
}
