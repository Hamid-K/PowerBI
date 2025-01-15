using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.MetaAnalytics.RDataSupport;

namespace Microsoft.Mashup.Engine1.Library.RData
{
	// Token: 0x0200052D RID: 1325
	public static class RToValue
	{
		// Token: 0x06002A71 RID: 10865 RVA: 0x0007F2CC File Offset: 0x0007D4CC
		private static RToValue.ColumnDescriptor Convert(RObject column)
		{
			RObject<int?> robject = RToValue.IsClass<int?>(column, new string[] { "factor" });
			if (robject != null)
			{
				RObject<string> levels = column.GetAttribute<string>("levels");
				if (levels != null)
				{
					RToValue.ColumnDescriptor columnDescriptor = new RToValue.ColumnDescriptor
					{
						Values = robject.TypedValues.Select(delegate(int? k)
						{
							if (k == null)
							{
								return Value.Null;
							}
							return TextValue.NewOrNull(levels.TypedValues[k.Value - 1]);
						}),
						Type = (robject.HasMissings ? NullableTypeValue.Character : TypeValue.Character)
					};
					return columnDescriptor;
				}
			}
			RObject<double?> robject2 = RToValue.IsClass<double?>(column, new string[] { "Date" });
			if (robject2 != null)
			{
				RToValue.ColumnDescriptor columnDescriptor = default(RToValue.ColumnDescriptor);
				columnDescriptor.Values = robject2.TypedValues.Select(delegate(double? d)
				{
					if (d == null)
					{
						return Value.Null;
					}
					return DateValue.New(RToValue.FromEpoch(d.Value * 86400.0));
				});
				columnDescriptor.Type = (robject2.HasMissings ? NullableTypeValue.Date : TypeValue.Date);
				return columnDescriptor;
			}
			RObject<RObject> robject3 = RToValue.IsClass<RObject>(column, new string[] { "POSIXlt", "POSIXt" });
			if (robject3 != null)
			{
				RToValue.ColumnDescriptor columnDescriptor;
				PosixLtVector posixLtVector;
				if (!PosixLtVector.TryNew(robject3, out posixLtVector))
				{
					columnDescriptor = new RToValue.ColumnDescriptor
					{
						Values = Enumerable.Repeat<IValueReference>(new ExceptionValueReference(ValueException.NewDataFormatError<Message0>(Strings.R_DataTypeNotSupported, Value.Null, null)), robject3.TypedValues[0].Values.Length),
						Type = TypeValue.None
					};
					return columnDescriptor;
				}
				columnDescriptor = new RToValue.ColumnDescriptor
				{
					Values = posixLtVector.Values(),
					Type = (robject3.HasMissings ? NullableTypeValue.DateTime : TypeValue.DateTime)
				};
				return columnDescriptor;
			}
			else
			{
				RObject<double?> robject4 = RToValue.IsClass<double?>(column, new string[] { "POSIXct", "POSIXt" });
				if (robject4 != null)
				{
					RToValue.ColumnDescriptor columnDescriptor = default(RToValue.ColumnDescriptor);
					columnDescriptor.Values = robject4.TypedValues.Select(delegate(double? d)
					{
						if (d == null)
						{
							return Value.Null;
						}
						return DateTimeValue.New(RToValue.FromEpoch(d.Value));
					});
					columnDescriptor.Type = (robject4.HasMissings ? NullableTypeValue.DateTime : TypeValue.DateTime);
					return columnDescriptor;
				}
				RObject<double?> robject5 = RToValue.IsClass<double?>(column, new string[] { "difftime" });
				if (robject5 != null)
				{
					RObject<string> units = robject5.GetAttribute<string>("units");
					RToValue.ColumnDescriptor columnDescriptor;
					if (units == null || units.TypedValues.Length != 1)
					{
						columnDescriptor = new RToValue.ColumnDescriptor
						{
							Values = Enumerable.Repeat<IValueReference>(new ExceptionValueReference(ValueException.NewDataFormatError<Message0>(Strings.R_DataTypeNotSupported, Value.Null, null)), column.Values.Length),
							Type = TypeValue.None
						};
						return columnDescriptor;
					}
					columnDescriptor = new RToValue.ColumnDescriptor
					{
						Values = robject5.TypedValues.Select(delegate(double? d)
						{
							if (d == null)
							{
								return Value.Null;
							}
							return DurationValue.New(RToValue.FromDiffTime(d.Value, units.TypedValues.First<string>()));
						}),
						Type = (robject5.HasMissings ? NullableTypeValue.Duration : TypeValue.Duration)
					};
					return columnDescriptor;
				}
				else
				{
					Func<RObject, RToValue.ColumnDescriptor> func;
					if (!RToValue.Readers.TryGetValue(column.GetType(), out func))
					{
						RToValue.ColumnDescriptor columnDescriptor = new RToValue.ColumnDescriptor
						{
							Values = Enumerable.Repeat<IValueReference>(new ExceptionValueReference(ValueException.NewDataFormatError<Message0>(Strings.R_DataTypeNotSupported, Value.Null, null)), column.Values.Length),
							Type = TypeValue.None
						};
						return columnDescriptor;
					}
					return func(column);
				}
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0007F624 File Offset: 0x0007D824
		private static DateTime FromEpoch(double seconds)
		{
			return RToValue.EpochOrigin.AddSeconds(seconds);
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0007F640 File Offset: 0x0007D840
		private static TimeSpan FromDiffTime(double value, string units)
		{
			if (units == "secs")
			{
				return TimeSpan.FromSeconds(value);
			}
			if (units == "mins")
			{
				return TimeSpan.FromMinutes(value);
			}
			if (units == "hours")
			{
				return TimeSpan.FromHours(value);
			}
			if (units == "days")
			{
				return TimeSpan.FromDays(value);
			}
			if (!(units == "weeks"))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.R_BrokenTimeDiff, Value.Null, null);
			}
			return TimeSpan.FromDays(value * 7.0);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0007F6D0 File Offset: 0x0007D8D0
		private static RObject<T> IsClass<T>(RObject value, params string[] classes)
		{
			RObject<T> robject = value as RObject<T>;
			if (robject == null)
			{
				return null;
			}
			RObject<string> className = value.GetAttribute<string>("class");
			if (className == null || className.TypedValues.Length != classes.Length)
			{
				return null;
			}
			if (!classes.Where((string t, int i) => !t.Equals(className.TypedValues[i], StringComparison.Ordinal)).Any<string>())
			{
				return robject;
			}
			return null;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0007F738 File Offset: 0x0007D938
		public static Dictionary<string, Value> GetDataFrames(BinaryValue stream)
		{
			Dictionary<string, Value> dictionary = new Dictionary<string, Value>();
			KeyValuePair<string, RObject>[] array = RReader.ReadWorkspace(stream.AsBytes, true);
			int i = 0;
			while (i < array.Length)
			{
				KeyValuePair<string, RObject> keyValuePair = array[i];
				RObject value = keyValuePair.Value;
				string text = value.ClassName;
				if (text != null)
				{
					goto IL_0052;
				}
				RObject<string> attribute = value.GetAttribute<string>("class");
				if (attribute != null)
				{
					text = attribute.TypedValues[0];
					goto IL_0052;
				}
				IL_0084:
				i++;
				continue;
				IL_0052:
				if (text.Equals("data.frame", StringComparison.Ordinal) || text.Equals("data.table", StringComparison.Ordinal))
				{
					dictionary[keyValuePair.Key] = RToValue.CreateDataTable(value);
					goto IL_0084;
				}
				goto IL_0084;
			}
			return dictionary;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0007F7D4 File Offset: 0x0007D9D4
		private static Value CreateDataTable(RObject robject)
		{
			RObject<string> robject2 = robject.GetAttribute("names") as RObject<string>;
			RObject attribute = robject.GetAttribute("row.names");
			RObject<RObject> robject3 = robject as RObject<RObject>;
			if (robject3 == null || robject2 == null || attribute == null || robject3.TypedValues.Length == 0)
			{
				return TableValue.Empty;
			}
			IList<RObject> typedValues = robject3.TypedValues;
			IList<string> list = robject2.TypedValues;
			if (list.Count < typedValues.Count)
			{
				list = list.ToList<string>();
			}
			while (list.Count < typedValues.Count)
			{
				list.Add("Column" + (list.Count + 1).ToString());
			}
			if (list.Distinct<string>().Count<string>() != list.Count)
			{
				throw ValueException.NewDataFormatError<Message1>(Strings.R_DuplicateColumnNames((from str in list
					group str by str into @group
					where @group.Count<string>() > 1
					select @group.Key).Aggregate(new StringBuilder(Environment.NewLine), (StringBuilder builder, string str) => builder.AppendLine(str)).ToString()), Value.Null, null);
			}
			IEnumerable<RToValue.ColumnDescriptor> enumerable = typedValues.Select(new Func<RObject, RToValue.ColumnDescriptor>(RToValue.Convert));
			RToValue.ColumnDescriptor[] array = (enumerable as RToValue.ColumnDescriptor[]) ?? enumerable.ToArray<RToValue.ColumnDescriptor>();
			ListValue listValue = ListValue.New(array.Select((RToValue.ColumnDescriptor c) => ListValue.New(c.Values)));
			IEnumerable<Value> enumerable2 = array.Select((RToValue.ColumnDescriptor x) => RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				x.Type,
				LogicalValue.False
			}));
			RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(Keys.New(list.ToArray<string>()), enumerable2.ToArray<Value>()));
			return TableModule.Table.FromColumns.Invoke(listValue, TableTypeValue.New(recordTypeValue));
		}

		// Token: 0x0400127F RID: 4735
		private const int SecondsPerYear = 86400;

		// Token: 0x04001280 RID: 4736
		private static readonly DateTime EpochOrigin = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc);

		// Token: 0x04001281 RID: 4737
		private static readonly Dictionary<Type, Func<RObject, RToValue.ColumnDescriptor>> Readers = new Dictionary<Type, Func<RObject, RToValue.ColumnDescriptor>>
		{
			{
				typeof(RObject<int?>),
				delegate(RObject o)
				{
					RToValue.ColumnDescriptor columnDescriptor = default(RToValue.ColumnDescriptor);
					columnDescriptor.Values = ((RObject<int?>)o).TypedValues.Select(delegate(int? k)
					{
						if (k == null)
						{
							return Value.Null;
						}
						return NumberValue.New(k.Value);
					});
					columnDescriptor.Type = (((RObject<int?>)o).HasMissings ? NullableTypeValue.Int32 : TypeValue.Int32);
					return columnDescriptor;
				}
			},
			{
				typeof(RObject<bool?>),
				delegate(RObject o)
				{
					RToValue.ColumnDescriptor columnDescriptor2 = default(RToValue.ColumnDescriptor);
					columnDescriptor2.Values = ((RObject<bool?>)o).TypedValues.Select(delegate(bool? k)
					{
						if (k == null)
						{
							return Value.Null;
						}
						return LogicalValue.New(k.Value);
					});
					columnDescriptor2.Type = (((RObject<bool?>)o).HasMissings ? NullableTypeValue.Logical : TypeValue.Logical);
					return columnDescriptor2;
				}
			},
			{
				typeof(RObject<double?>),
				delegate(RObject o)
				{
					RToValue.ColumnDescriptor columnDescriptor3 = default(RToValue.ColumnDescriptor);
					columnDescriptor3.Values = ((RObject<double?>)o).TypedValues.Select(delegate(double? k)
					{
						if (k == null)
						{
							return Value.Null;
						}
						return NumberValue.New(k.Value);
					});
					columnDescriptor3.Type = (((RObject<double?>)o).HasMissings ? NullableTypeValue.Number : TypeValue.Number);
					return columnDescriptor3;
				}
			},
			{
				typeof(RObject<string>),
				delegate(RObject o)
				{
					RToValue.ColumnDescriptor columnDescriptor4 = default(RToValue.ColumnDescriptor);
					columnDescriptor4.Values = ((RObject<string>)o).TypedValues.Select((string k) => TextValue.NewOrNull(k));
					columnDescriptor4.Type = (((RObject<string>)o).HasMissings ? NullableTypeValue.Character : TypeValue.Character);
					return columnDescriptor4;
				}
			}
		};

		// Token: 0x0200052E RID: 1326
		private struct ColumnDescriptor
		{
			// Token: 0x1700101A RID: 4122
			// (get) Token: 0x06002A78 RID: 10872 RVA: 0x0007FA9D File Offset: 0x0007DC9D
			// (set) Token: 0x06002A79 RID: 10873 RVA: 0x0007FAA5 File Offset: 0x0007DCA5
			public IEnumerable<IValueReference> Values { get; set; }

			// Token: 0x1700101B RID: 4123
			// (get) Token: 0x06002A7A RID: 10874 RVA: 0x0007FAAE File Offset: 0x0007DCAE
			// (set) Token: 0x06002A7B RID: 10875 RVA: 0x0007FAB6 File Offset: 0x0007DCB6
			public TypeValue Type { get; set; }
		}
	}
}
