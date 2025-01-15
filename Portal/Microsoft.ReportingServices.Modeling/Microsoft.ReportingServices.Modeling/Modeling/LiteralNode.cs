using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A7 RID: 167
	public sealed class LiteralNode : ExpressionNode
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x0001C61D File Offset: 0x0001A81D
		public LiteralNode(string value)
		{
			this.ValueAsString = value;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001C62C File Offset: 0x0001A82C
		public LiteralNode(ICollection<string> values)
		{
			this.ValueAsStringSet = values;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001C63B File Offset: 0x0001A83B
		public LiteralNode(long value)
		{
			this.ValueAsInteger = value;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001C64A File Offset: 0x0001A84A
		public LiteralNode(ICollection<long> values)
		{
			this.ValueAsIntegerSet = values;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001C659 File Offset: 0x0001A859
		public LiteralNode(decimal value)
		{
			this.ValueAsDecimal = value;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001C668 File Offset: 0x0001A868
		public LiteralNode(ICollection<decimal> values)
		{
			this.ValueAsDecimalSet = values;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001C677 File Offset: 0x0001A877
		public LiteralNode(double value)
		{
			this.ValueAsFloat = value;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001C686 File Offset: 0x0001A886
		public LiteralNode(ICollection<double> values)
		{
			this.ValueAsFloatSet = values;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001C695 File Offset: 0x0001A895
		public LiteralNode(bool value)
		{
			this.ValueAsBoolean = value;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		public LiteralNode(ICollection<bool> values)
		{
			this.ValueAsBooleanSet = values;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001C6B3 File Offset: 0x0001A8B3
		public LiteralNode(DateTime value)
		{
			this.ValueAsDateTime = value;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001C6C2 File Offset: 0x0001A8C2
		public LiteralNode(ICollection<DateTime> values)
		{
			this.ValueAsDateTimeSet = values;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001C6D1 File Offset: 0x0001A8D1
		public LiteralNode(TimeSpan value)
		{
			this.ValueAsTime = value;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		public LiteralNode(ICollection<TimeSpan> values)
		{
			this.ValueAsTimeSet = values;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001C6EF File Offset: 0x0001A8EF
		public LiteralNode(EntityKey value)
		{
			this.ValueAsEntityKey = value;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001C6FE File Offset: 0x0001A8FE
		public LiteralNode(ICollection<EntityKey> values)
		{
			this.ValueAsEntityKeySet = values;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001C70D File Offset: 0x0001A90D
		internal LiteralNode()
		{
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001C715 File Offset: 0x0001A915
		private LiteralNode(LiteralNode nodeToCopy)
		{
			this.m_dataType = nodeToCopy.m_dataType;
			this.m_cardinality = nodeToCopy.m_cardinality;
			this.m_value = nodeToCopy.m_value;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001C741 File Offset: 0x0001A941
		public override bool IsConstantValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0001C744 File Offset: 0x0001A944
		internal override IQueryEntity SourceEntity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001C747 File Offset: 0x0001A947
		public DataType DataType
		{
			get
			{
				return this.m_dataType;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001C74F File Offset: 0x0001A94F
		public Cardinality Cardinality
		{
			get
			{
				return this.m_cardinality;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001C757 File Offset: 0x0001A957
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001C75F File Offset: 0x0001A95F
		public override ExpressionNode Clone(ExpressionCopyManager copyManager)
		{
			return new LiteralNode(this);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001C768 File Offset: 0x0001A968
		public override bool IsSameAs(ExpressionNode other)
		{
			LiteralNode literalNode = other as LiteralNode;
			if (literalNode == null)
			{
				return false;
			}
			if (literalNode.DataType != this.m_dataType || literalNode.Cardinality != this.m_cardinality)
			{
				return false;
			}
			this.AssertValidState();
			literalNode.AssertValidState();
			if (this.m_cardinality == Cardinality.One)
			{
				return this.m_value.Equals(literalNode.m_value);
			}
			switch (this.m_dataType)
			{
			case DataType.String:
				return CollectionUtil.ElementsEqual<string>(this.ValueAsStringSet, literalNode.ValueAsStringSet);
			case DataType.Integer:
				return CollectionUtil.ElementsEqual<long>(this.ValueAsIntegerSet, literalNode.ValueAsIntegerSet);
			case DataType.Decimal:
				return CollectionUtil.ElementsEqual<decimal>(this.ValueAsDecimalSet, literalNode.ValueAsDecimalSet);
			case DataType.Float:
				return CollectionUtil.ElementsEqual<double>(this.ValueAsFloatSet, literalNode.ValueAsFloatSet);
			case DataType.Boolean:
				return CollectionUtil.ElementsEqual<bool>(this.ValueAsBooleanSet, literalNode.ValueAsBooleanSet);
			case DataType.DateTime:
				return CollectionUtil.ElementsEqual<DateTime>(this.ValueAsDateTimeSet, literalNode.ValueAsDateTimeSet);
			case DataType.EntityKey:
				return CollectionUtil.ElementsEqual<EntityKey>(this.ValueAsEntityKeySet, literalNode.ValueAsEntityKeySet);
			case DataType.Time:
				return CollectionUtil.ElementsEqual<TimeSpan>(this.ValueAsTimeSet, literalNode.ValueAsTimeSet);
			}
			throw new InternalModelingException("Unknown Literal DataType '" + this.m_dataType.ToString() + "'");
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
		public override bool IsSubtreeAnchored()
		{
			return false;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001C8B7 File Offset: 0x0001AAB7
		internal override bool HasInvalidRefs(Bag<Expression> processedExpressions)
		{
			return false;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001C8BC File Offset: 0x0001AABC
		public override string ToString()
		{
			string str = this.m_dataType.ToString() + ":";
			Action<object> action = delegate(object value)
			{
				if (value == null)
				{
					str += "[NULL]";
					return;
				}
				switch (this.m_dataType)
				{
				case DataType.String:
					str = str + "\"" + value.ToString() + "\"";
					return;
				case DataType.Integer:
				case DataType.Decimal:
				case DataType.Float:
				case DataType.Boolean:
					str += Convert.ToString(value, CultureInfo.InvariantCulture);
					return;
				case DataType.DateTime:
					str += XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
					return;
				case DataType.Binary:
					str += "...";
					return;
				case DataType.EntityKey:
					str += ((EntityKey)value).ToBase64String();
					return;
				case DataType.Time:
					str += XmlConvert.ToString((TimeSpan)value);
					return;
				}
				str += Convert.ToString(value, CultureInfo.InvariantCulture);
			};
			ICollection collection = this.m_value as ICollection;
			if (collection != null)
			{
				bool flag = true;
				using (IEnumerator enumerator = collection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						if (!flag)
						{
							str += ", ";
						}
						else
						{
							flag = false;
						}
						action(obj);
					}
					goto IL_00B0;
				}
			}
			action(this.m_value);
			IL_00B0:
			return str;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001C990 File Offset: 0x0001AB90
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001C998 File Offset: 0x0001AB98
		public string ValueAsString
		{
			get
			{
				return this.GetValue<string>();
			}
			set
			{
				this.SetValue<string>(value ?? string.Empty);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001C9AA File Offset: 0x0001ABAA
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001C9B2 File Offset: 0x0001ABB2
		public ICollection<string> ValueAsStringSet
		{
			get
			{
				return this.GetValueSet<string>();
			}
			set
			{
				this.SetValueSet<string>(value);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001C9BB File Offset: 0x0001ABBB
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001C9C3 File Offset: 0x0001ABC3
		public long ValueAsInteger
		{
			get
			{
				return this.GetValue<long>();
			}
			set
			{
				this.SetValue<long>(value);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0001C9D4 File Offset: 0x0001ABD4
		public ICollection<long> ValueAsIntegerSet
		{
			get
			{
				return this.GetValueSet<long>();
			}
			set
			{
				this.SetValueSet<long>(value);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001C9DD File Offset: 0x0001ABDD
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001C9E5 File Offset: 0x0001ABE5
		public decimal ValueAsDecimal
		{
			get
			{
				return this.GetValue<decimal>();
			}
			set
			{
				this.SetValue<decimal>(value);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001C9EE File Offset: 0x0001ABEE
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0001C9F6 File Offset: 0x0001ABF6
		public ICollection<decimal> ValueAsDecimalSet
		{
			get
			{
				return this.GetValueSet<decimal>();
			}
			set
			{
				this.SetValueSet<decimal>(value);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001C9FF File Offset: 0x0001ABFF
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0001CA07 File Offset: 0x0001AC07
		public double ValueAsFloat
		{
			get
			{
				return this.GetValue<double>();
			}
			set
			{
				this.SetValue<double>(value);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001CA10 File Offset: 0x0001AC10
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0001CA18 File Offset: 0x0001AC18
		public ICollection<double> ValueAsFloatSet
		{
			get
			{
				return this.GetValueSet<double>();
			}
			set
			{
				this.SetValueSet<double>(value);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001CA21 File Offset: 0x0001AC21
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0001CA29 File Offset: 0x0001AC29
		public bool ValueAsBoolean
		{
			get
			{
				return this.GetValue<bool>();
			}
			set
			{
				this.SetValue<bool>(value);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001CA32 File Offset: 0x0001AC32
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0001CA3A File Offset: 0x0001AC3A
		public ICollection<bool> ValueAsBooleanSet
		{
			get
			{
				return this.GetValueSet<bool>();
			}
			set
			{
				this.SetValueSet<bool>(value);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001CA43 File Offset: 0x0001AC43
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001CA4B File Offset: 0x0001AC4B
		public DateTime ValueAsDateTime
		{
			get
			{
				return this.GetValue<DateTime>();
			}
			set
			{
				if (value.Kind == DateTimeKind.Unspecified)
				{
					this.SetValue<DateTime>(value);
					return;
				}
				this.SetValue<DateTime>(DateTime.SpecifyKind(value, DateTimeKind.Unspecified));
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001CA6B File Offset: 0x0001AC6B
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0001CA74 File Offset: 0x0001AC74
		public ICollection<DateTime> ValueAsDateTimeSet
		{
			get
			{
				return this.GetValueSet<DateTime>();
			}
			set
			{
				if (value != null && value.Count > 0)
				{
					DateTime[] array = ArrayUtil.ToArray<DateTime>(value);
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = DateTime.SpecifyKind(array[i], DateTimeKind.Unspecified);
					}
					this.SetValueSet<DateTime>(array);
					return;
				}
				this.SetValueSet<DateTime>(value);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001CAC5 File Offset: 0x0001ACC5
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001CACD File Offset: 0x0001ACCD
		public TimeSpan ValueAsTime
		{
			get
			{
				return this.GetValue<TimeSpan>();
			}
			set
			{
				this.SetValue<TimeSpan>(value);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001CAD6 File Offset: 0x0001ACD6
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0001CADE File Offset: 0x0001ACDE
		public ICollection<TimeSpan> ValueAsTimeSet
		{
			get
			{
				return this.GetValueSet<TimeSpan>();
			}
			set
			{
				this.SetValueSet<TimeSpan>(value);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001CAE7 File Offset: 0x0001ACE7
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0001CAEF File Offset: 0x0001ACEF
		public EntityKey ValueAsEntityKey
		{
			get
			{
				return this.GetValue<EntityKey>();
			}
			set
			{
				this.SetValue<EntityKey>(value);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0001CB00 File Offset: 0x0001AD00
		public ICollection<EntityKey> ValueAsEntityKeySet
		{
			get
			{
				return this.GetValueSet<EntityKey>();
			}
			set
			{
				this.SetValueSet<EntityKey>(value);
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001CB09 File Offset: 0x0001AD09
		private T GetValue<T>()
		{
			this.CheckState(LiteralNode.MapToDataType(typeof(T)), Cardinality.One);
			return (T)((object)this.m_value);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001CB2C File Offset: 0x0001AD2C
		private ICollection<T> GetValueSet<T>()
		{
			this.CheckState(LiteralNode.MapToDataType(typeof(T)), Cardinality.Many);
			return (ICollection<T>)this.m_value;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001CB50 File Offset: 0x0001AD50
		private void SetValue<T>(T value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			base.CheckWriteable();
			this.m_dataType = LiteralNode.MapToDataType(typeof(T));
			this.m_cardinality = Cardinality.One;
			this.m_value = value;
			this.AssertValidState();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
		private void SetValueSet<T>(ICollection<T> values)
		{
			if (values == null || values.Count == 0)
			{
				throw new ArgumentNullException();
			}
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == null)
					{
						throw new ArgumentException();
					}
				}
			}
			base.CheckWriteable();
			this.m_dataType = LiteralNode.MapToDataType(typeof(T));
			this.m_cardinality = Cardinality.Many;
			this.m_value = Array.AsReadOnly<T>(ArrayUtil.ToArray<T>(values));
			this.AssertValidState();
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001CC38 File Offset: 0x0001AE38
		private void CheckState(DataType dataType, Cardinality cardinality)
		{
			if (this.m_dataType != dataType)
			{
				throw new InvalidOperationException(DevExceptionMessages.LiteralNode_DataTypeMismatch);
			}
			if (this.m_cardinality == cardinality)
			{
				return;
			}
			if (cardinality == Cardinality.One)
			{
				throw new InvalidOperationException(DevExceptionMessages.LiteralNode_ScalarRequired);
			}
			throw new InvalidOperationException(DevExceptionMessages.LiteralNode_SetRequired);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001CC70 File Offset: 0x0001AE70
		public LiteralNode ToSet()
		{
			this.AssertValidState();
			if (this.m_cardinality == Cardinality.One)
			{
				Array array = Array.CreateInstance(LiteralNode.MapToClrType(this.m_dataType), 1);
				array.SetValue(this.m_value, 0);
				return LiteralNode.FromArray(array, this.m_dataType);
			}
			return (LiteralNode)base.Clone();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001CCC0 File Offset: 0x0001AEC0
		public object[] ToObjectArray()
		{
			if (this.m_cardinality == Cardinality.One)
			{
				return new object[] { this.m_value };
			}
			switch (this.m_dataType)
			{
			case DataType.String:
				return ArrayUtil.ToObjectArray<string>(this.ValueAsStringSet);
			case DataType.Integer:
				return ArrayUtil.ToObjectArray<long>(this.ValueAsIntegerSet);
			case DataType.Decimal:
				return ArrayUtil.ToObjectArray<decimal>(this.ValueAsDecimalSet);
			case DataType.Float:
				return ArrayUtil.ToObjectArray<double>(this.ValueAsFloatSet);
			case DataType.Boolean:
				return ArrayUtil.ToObjectArray<bool>(this.ValueAsBooleanSet);
			case DataType.DateTime:
				return ArrayUtil.ToObjectArray<DateTime>(this.ValueAsDateTimeSet);
			case DataType.EntityKey:
				return ArrayUtil.ToObjectArray<EntityKey>(this.ValueAsEntityKeySet);
			case DataType.Time:
				return ArrayUtil.ToObjectArray<TimeSpan>(this.ValueAsTimeSet);
			}
			throw new InternalModelingException("Unrecognized DataType '" + this.m_dataType.ToString() + "'");
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
		public static LiteralNode FromObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			Type type = value.GetType();
			Array array = value as Array;
			if (array != null)
			{
				type = array.GetType().GetElementType();
				if (array.Length == 0)
				{
					throw new ArgumentException();
				}
			}
			DataType? dataType = DataTypeMapper.TranslateClrType(type);
			if (dataType == null)
			{
				throw new ArgumentException();
			}
			if (array != null)
			{
				return LiteralNode.FromObjectList(array, dataType.Value);
			}
			return LiteralNode.FromObject(value, dataType.Value);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001CE1C File Offset: 0x0001B01C
		internal static LiteralNode FromObject(object value, DataType dataType)
		{
			LiteralNode literalNode = new LiteralNode();
			literalNode.m_dataType = dataType;
			literalNode.m_cardinality = Cardinality.One;
			literalNode.m_value = LiteralNode.ConvertValue(value, dataType);
			if (literalNode.m_value == null)
			{
				return null;
			}
			literalNode.AssertValidState();
			return literalNode;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001CE5C File Offset: 0x0001B05C
		public static LiteralNode FromObjectList(IList valueList, DataType dataType)
		{
			if (valueList == null || valueList.Count == 0)
			{
				return null;
			}
			Array array = Array.CreateInstance(LiteralNode.MapToClrType(dataType), valueList.Count);
			for (int i = 0; i < valueList.Count; i++)
			{
				object obj = LiteralNode.ConvertValue(valueList[i], dataType);
				if (obj == null)
				{
					return null;
				}
				array.SetValue(obj, i);
			}
			return LiteralNode.FromArray(array, dataType);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001CEBC File Offset: 0x0001B0BC
		internal static LiteralNode FromString(string value, DataType dataType, ValidationContext vctx)
		{
			if (value == null)
			{
				throw new InternalModelingException("value is null");
			}
			LiteralNode literalNode = new LiteralNode();
			literalNode.m_value = value;
			literalNode.m_dataType = dataType;
			literalNode.m_cardinality = Cardinality.One;
			if (!literalNode.ParseXmlValue(vctx))
			{
				return null;
			}
			literalNode.AssertValidState();
			return literalNode;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001CF04 File Offset: 0x0001B104
		private static LiteralNode FromArray(Array array, DataType dataType)
		{
			LiteralNode literalNode = new LiteralNode();
			literalNode.m_dataType = dataType;
			literalNode.m_cardinality = Cardinality.Many;
			switch (dataType)
			{
			case DataType.String:
				literalNode.m_value = Array.AsReadOnly<string>((string[])array);
				goto IL_010A;
			case DataType.Integer:
				literalNode.m_value = Array.AsReadOnly<long>((long[])array);
				goto IL_010A;
			case DataType.Decimal:
				literalNode.m_value = Array.AsReadOnly<decimal>((decimal[])array);
				goto IL_010A;
			case DataType.Float:
				literalNode.m_value = Array.AsReadOnly<double>((double[])array);
				goto IL_010A;
			case DataType.Boolean:
				literalNode.m_value = Array.AsReadOnly<bool>((bool[])array);
				goto IL_010A;
			case DataType.DateTime:
				literalNode.m_value = Array.AsReadOnly<DateTime>((DateTime[])array);
				goto IL_010A;
			case DataType.EntityKey:
				literalNode.m_value = Array.AsReadOnly<EntityKey>((EntityKey[])array);
				goto IL_010A;
			case DataType.Time:
				literalNode.m_value = Array.AsReadOnly<TimeSpan>((TimeSpan[])array);
				goto IL_010A;
			}
			throw new InternalModelingException("Unrecognized DataType '" + dataType.ToString() + "'");
			IL_010A:
			literalNode.AssertValidState();
			return literalNode;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001D024 File Offset: 0x0001B224
		private static DataType MapToDataType(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode == TypeCode.Boolean)
			{
				return DataType.Boolean;
			}
			switch (typeCode)
			{
			case TypeCode.Int64:
				return DataType.Integer;
			case TypeCode.Double:
				return DataType.Float;
			case TypeCode.Decimal:
				return DataType.Decimal;
			case TypeCode.DateTime:
				return DataType.DateTime;
			case TypeCode.String:
				return DataType.String;
			}
			if (type == typeof(EntityKey))
			{
				return DataType.EntityKey;
			}
			if (type == typeof(TimeSpan))
			{
				return DataType.Time;
			}
			throw new InternalModelingException("Unrecognized type '" + type.FullName + "'");
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		private static Type MapToClrType(DataType dataType)
		{
			switch (dataType)
			{
			case DataType.String:
				return typeof(string);
			case DataType.Integer:
				return typeof(long);
			case DataType.Decimal:
				return typeof(decimal);
			case DataType.Float:
				return typeof(double);
			case DataType.Boolean:
				return typeof(bool);
			case DataType.DateTime:
				return typeof(DateTime);
			case DataType.EntityKey:
				return typeof(EntityKey);
			case DataType.Time:
				return typeof(TimeSpan);
			}
			throw new InternalModelingException("Unrecognized DataType '" + dataType.ToString() + "'");
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001D170 File Offset: 0x0001B370
		private static object ConvertValue(object value, DataType dataType)
		{
			if (value == null)
			{
				return null;
			}
			object obj;
			try
			{
				if (dataType == DataType.EntityKey && value is string)
				{
					obj = EntityKey.FromBase64String((string)value);
				}
				else if (dataType == DataType.Time && value is string)
				{
					TimeSpan timeSpan;
					if (TimeSpan.TryParse((string)value, out timeSpan))
					{
						obj = timeSpan;
					}
					else
					{
						obj = XmlConvert.ToTimeSpan((string)value);
					}
				}
				else
				{
					obj = Convert.ChangeType(value, LiteralNode.MapToClrType(dataType), CultureInfo.CurrentCulture);
				}
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001D200 File Offset: 0x0001B400
		private void AssertValidState()
		{
			Type type = LiteralNode.MapToClrType(this.m_dataType);
			if (this.m_cardinality != Cardinality.One)
			{
				if (this.m_cardinality != Cardinality.Many)
				{
					throw new InternalModelingException("Unrecognized Cardinality '" + this.m_cardinality.ToString() + "'");
				}
				type = typeof(ICollection<>).MakeGenericType(new Type[] { type });
			}
			if (!type.IsInstanceOfType(this.m_value))
			{
				throw new InternalModelingException(StringUtil.FormatInvariant("m_value type '{0}' does not match or implement expected type '{1}'", new object[]
				{
					this.m_value.GetType(),
					type
				}));
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001D2A2 File Offset: 0x0001B4A2
		internal override void Load(ModelingXmlReader xr, bool topLevel)
		{
			base.Load(xr, topLevel);
			if (this.m_value == null)
			{
				this.AddInvalidLiteralError(xr.Validation);
				return;
			}
			if (this.ParseXmlValue(xr.Validation))
			{
				this.AssertValidState();
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
		private bool ParseXmlValue(ValidationContext vctx)
		{
			switch (this.m_dataType)
			{
			case DataType.String:
				return this.ParseXmlValueCore<string>(new Converter<string, string>(Convert.ToString), vctx);
			case DataType.Integer:
				return this.ParseXmlValueCore<long>(new Converter<string, long>(XmlConvert.ToInt64), vctx);
			case DataType.Decimal:
				return this.ParseXmlValueCore<decimal>(new Converter<string, decimal>(XmlConvert.ToDecimal), vctx);
			case DataType.Float:
				return this.ParseXmlValueCore<double>(new Converter<string, double>(XmlConvert.ToDouble), vctx);
			case DataType.Boolean:
				return this.ParseXmlValueCore<bool>(new Converter<string, bool>(XmlConvert.ToBoolean), vctx);
			case DataType.DateTime:
				return this.ParseXmlValueCore<DateTime>(new Converter<string, DateTime>(LiteralNode.ToDateTime), vctx);
			case DataType.EntityKey:
				return this.ParseXmlValueCore<EntityKey>(new Converter<string, EntityKey>(EntityKey.FromBase64String), vctx);
			case DataType.Time:
				return this.ParseXmlValueCore<TimeSpan>(new Converter<string, TimeSpan>(XmlConvert.ToTimeSpan), vctx);
			}
			throw new InternalModelingException("Unrecognized DataType '" + this.m_dataType.ToString() + "'");
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		private static DateTime ToDateTime(string s)
		{
			return XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.Unspecified);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		private bool ParseXmlValueCore<T>(Converter<string, T> convert, ValidationContext vctx)
		{
			bool flag = false;
			if (this.m_cardinality == Cardinality.One)
			{
				string text = null;
				try
				{
					this.m_value = convert((string)this.m_value);
				}
				catch (FormatException ex)
				{
					text = ex.Message ?? string.Empty;
				}
				catch (OverflowException ex2)
				{
					text = ex2.Message ?? string.Empty;
				}
				if (text != null)
				{
					flag = true;
					this.AddInvalidLiteralValueError(vctx, (string)this.m_value, text);
				}
			}
			else
			{
				List<string> list = (List<string>)this.m_value;
				T[] array = new T[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					string text2 = null;
					try
					{
						array[i] = convert(list[i]);
					}
					catch (FormatException ex3)
					{
						text2 = ex3.Message ?? string.Empty;
					}
					catch (OverflowException ex4)
					{
						text2 = ex4.Message ?? string.Empty;
					}
					if (text2 != null)
					{
						flag = true;
						this.AddInvalidLiteralValueError(vctx, list[i], text2);
					}
				}
				this.m_value = Array.AsReadOnly<T>(array);
			}
			return !flag;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001D530 File Offset: 0x0001B730
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "DataType")
				{
					this.m_dataType = xr.ReadValueAsEnum<DataType>();
					return true;
				}
				if (localName == "Value" || localName == "Values")
				{
					if (this.m_value != null)
					{
						this.AddInvalidLiteralError(xr.Validation);
						xr.Skip();
					}
					else if (xr.LocalName == "Value")
					{
						this.m_cardinality = Cardinality.One;
						this.m_value = xr.ReadValueAsString();
					}
					else
					{
						List<string> list = new List<string>();
						this.m_cardinality = Cardinality.Many;
						this.m_value = list;
						xr.LoadObject(new LiteralNode.ValuesLoader(list));
					}
					return true;
				}
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001D5F2 File Offset: 0x0001B7F2
		private void AddInvalidLiteralError(ValidationContext ctx)
		{
			ctx.AddScopedError(ModelingErrorCode.InvalidLiteral, SRErrors.InvalidLiteral(ctx.CurrentObjectDescriptor));
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001D607 File Offset: 0x0001B807
		private void AddInvalidLiteralValueError(ValidationContext ctx, string value, string errorDetails)
		{
			ctx.AddScopedError(ModelingErrorCode.InvalidLiteralValue, SRErrors.InvalidLiteralValue(ctx.CurrentObjectDescriptor, value, this.m_dataType, errorDetails));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001D624 File Offset: 0x0001B824
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Literal");
			xw.WriteElement("DataType", this.m_dataType);
			switch (this.m_dataType)
			{
			case DataType.String:
				this.WriteXmlValue<string>(xw);
				goto IL_00C6;
			case DataType.Integer:
				this.WriteXmlValue<long>(xw);
				goto IL_00C6;
			case DataType.Decimal:
				this.WriteXmlValue<decimal>(xw);
				goto IL_00C6;
			case DataType.Float:
				this.WriteXmlValue<double>(xw);
				goto IL_00C6;
			case DataType.Boolean:
				this.WriteXmlValue<bool>(xw);
				goto IL_00C6;
			case DataType.DateTime:
				this.WriteXmlValue<DateTime>(xw);
				goto IL_00C6;
			case DataType.EntityKey:
				this.WriteXmlValue<EntityKey>(xw);
				goto IL_00C6;
			case DataType.Time:
				this.WriteXmlValue<TimeSpan>(xw);
				goto IL_00C6;
			}
			throw new InternalModelingException("Unrecognized DataType '" + this.m_dataType.ToString() + "'");
			IL_00C6:
			xw.WriteEndElement();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001D700 File Offset: 0x0001B900
		private void WriteXmlValue<T>(ModelingXmlWriter xw)
		{
			if (this.m_cardinality != Cardinality.One)
			{
				xw.WriteStartElement("Values");
				foreach (T t in ((ICollection<T>)this.m_value))
				{
					object obj = t;
					if (obj == null || !(obj is T))
					{
						throw new InternalModelingException("Unexpected or null value");
					}
					xw.WriteElement("Value", obj);
				}
				xw.WriteEndElement();
				return;
			}
			if (this.m_value == null || !(this.m_value is T))
			{
				throw new InternalModelingException("Unexpected or null value");
			}
			xw.WriteElement("Value", this.m_value);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		internal override ResultType? Compile(CompilationContext ctx, bool topLevel, out Expression replacementExpr)
		{
			base.Compile(ctx.ShouldPersist);
			replacementExpr = null;
			this.AssertValidState();
			return new ResultType?(new ResultType(this.m_dataType, this.m_cardinality, false));
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001D7EE File Offset: 0x0001B9EE
		internal override void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("LiteralNode is not compiled.");
			}
			if (this.m_dataType != DataType.EntityKey)
			{
				throw new InternalModelingException("Data type must be EntityKey.");
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001D817 File Offset: 0x0001BA17
		internal override ExpressionNode CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("LiteralNode is not compiled.");
			}
			return this;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001D830 File Offset: 0x0001BA30
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(LiteralNode.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.DataType)
				{
					if (memberName != MemberName.Cardinality)
					{
						if (memberName != MemberName.Values)
						{
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
						ArrayList arrayList = new ArrayList();
						if (this.m_cardinality == Cardinality.One)
						{
							arrayList.Add(LiteralNode.PrepareValueForSerialization(this.m_dataType, this.m_value));
						}
						else
						{
							foreach (object obj in ((ICollection)this.m_value))
							{
								arrayList.Add(LiteralNode.PrepareValueForSerialization(this.m_dataType, obj));
							}
						}
						writer.WriteArrayListOfPrimitives(arrayList);
					}
					else
					{
						writer.Write((byte)this.m_cardinality);
					}
				}
				else
				{
					writer.Write((byte)this.m_dataType);
				}
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001D960 File Offset: 0x0001BB60
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(LiteralNode.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.DataType)
				{
					if (memberName != MemberName.Cardinality)
					{
						if (memberName != MemberName.Values)
						{
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
						if (this.m_cardinality != Cardinality.One)
						{
							switch (this.m_dataType)
							{
							case DataType.String:
								this.m_value = LiteralNode.DeserializeValues<string>(reader, this.m_dataType);
								continue;
							case DataType.Integer:
								this.m_value = LiteralNode.DeserializeValues<long>(reader, this.m_dataType);
								continue;
							case DataType.Decimal:
								this.m_value = LiteralNode.DeserializeValues<decimal>(reader, this.m_dataType);
								continue;
							case DataType.Float:
								this.m_value = LiteralNode.DeserializeValues<double>(reader, this.m_dataType);
								continue;
							case DataType.Boolean:
								this.m_value = LiteralNode.DeserializeValues<bool>(reader, this.m_dataType);
								continue;
							case DataType.DateTime:
								this.m_value = LiteralNode.DeserializeValues<DateTime>(reader, this.m_dataType);
								continue;
							case DataType.EntityKey:
								this.m_value = LiteralNode.DeserializeValues<EntityKey>(reader, this.m_dataType);
								continue;
							case DataType.Time:
								this.m_value = LiteralNode.DeserializeValues<TimeSpan>(reader, this.m_dataType);
								continue;
							}
							throw new InternalModelingException("Unrecognized DataType '" + this.m_dataType.ToString() + "'");
						}
						List<object> list = reader.ReadListOfPrimitives<object>();
						if (list.Count != 1)
						{
							throw new InternalModelingException(StringUtil.FormatInvariant("Unexpected number of values for a scalar literal: {0}", new object[] { list.Count }));
						}
						this.m_value = LiteralNode.PrepareValueFromDeserialization<object>(this.m_dataType, list[0]);
					}
					else
					{
						this.m_cardinality = (Cardinality)reader.ReadByte();
					}
				}
				else
				{
					this.m_dataType = (DataType)reader.ReadByte();
				}
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001DB68 File Offset: 0x0001BD68
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001DB74 File Offset: 0x0001BD74
		internal override ObjectType GetObjectType()
		{
			return ObjectType.LiteralNode;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001DB78 File Offset: 0x0001BD78
		private static object PrepareValueForSerialization(DataType dataType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (dataType)
			{
			case DataType.String:
			case DataType.Integer:
			case DataType.Decimal:
			case DataType.Float:
			case DataType.Boolean:
			case DataType.DateTime:
			case DataType.Time:
				return value;
			case DataType.EntityKey:
				return ((EntityKey)value).ToBase64String();
			}
			throw new InternalModelingException("Unrecognized DataType '" + dataType.ToString() + "'");
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		private static ICollection<T> DeserializeValues<T>(IntermediateFormatReader reader, DataType dataType)
		{
			if (dataType == DataType.EntityKey)
			{
				List<string> list = reader.ReadListOfPrimitives<string>();
				if (list.Count < 1)
				{
					throw new InternalModelingException(StringUtil.FormatInvariant("Unexpected number of values for a literal set: {0}", new object[] { list.Count }));
				}
				T[] array = new T[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = LiteralNode.PrepareValueFromDeserialization<T>(dataType, list[i]);
				}
				return Array.AsReadOnly<T>(array);
			}
			else
			{
				List<T> list2 = reader.ReadListOfPrimitives<T>();
				if (list2.Count < 1)
				{
					throw new InternalModelingException(StringUtil.FormatInvariant("Unexpected number of values for a literal set: {0}", new object[] { list2.Count }));
				}
				for (int j = 0; j < list2.Count; j++)
				{
					list2[j] = LiteralNode.PrepareValueFromDeserialization<T>(dataType, list2[j]);
				}
				return Array.AsReadOnly<T>(ArrayUtil.ToArray<T>(list2));
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		private static T PrepareValueFromDeserialization<T>(DataType dataType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((dataType != DataType.EntityKey && dataType != LiteralNode.MapToDataType(value.GetType())) || (dataType == DataType.EntityKey && !(value is string)))
			{
				throw new InvalidOperationException(DevExceptionMessages.LiteralNode_DataTypeMismatch);
			}
			switch (dataType)
			{
			case DataType.String:
			case DataType.Integer:
			case DataType.Decimal:
			case DataType.Float:
			case DataType.Boolean:
			case DataType.DateTime:
			case DataType.Time:
				return (T)((object)value);
			case DataType.EntityKey:
				return (T)((object)EntityKey.FromBase64String((string)value));
			}
			throw new InternalModelingException("Unrecognized DataType '" + dataType.ToString() + "'");
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0001DD91 File Offset: 0x0001BF91
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref LiteralNode.__declaration, LiteralNode.__declarationLock, () => new Declaration(ObjectType.LiteralNode, ObjectType.ExpressionNode, new List<MemberInfo>
				{
					new MemberInfo(MemberName.DataType, Token.Byte),
					new MemberInfo(MemberName.Cardinality, Token.Byte),
					new MemberInfo(MemberName.Values, ObjectType.PrimitiveList, Token.Object)
				}));
			}
		}

		// Token: 0x040003D2 RID: 978
		internal const string LiteralElem = "Literal";

		// Token: 0x040003D3 RID: 979
		private const string DataTypeElem = "DataType";

		// Token: 0x040003D4 RID: 980
		private const string ValueElem = "Value";

		// Token: 0x040003D5 RID: 981
		private const string ValuesElem = "Values";

		// Token: 0x040003D6 RID: 982
		private DataType m_dataType;

		// Token: 0x040003D7 RID: 983
		private Cardinality m_cardinality;

		// Token: 0x040003D8 RID: 984
		private object m_value;

		// Token: 0x040003D9 RID: 985
		private static Declaration __declaration;

		// Token: 0x040003DA RID: 986
		private static readonly object __declarationLock = new object();

		// Token: 0x020001A0 RID: 416
		private class ValuesLoader : ModelingXmlLoaderBase<List<string>>
		{
			// Token: 0x060010A0 RID: 4256 RVA: 0x000343F9 File Offset: 0x000325F9
			internal ValuesLoader(List<string> item)
				: base(item)
			{
			}

			// Token: 0x060010A1 RID: 4257 RVA: 0x00034402 File Offset: 0x00032602
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Value")
				{
					base.Item.Add(xr.ReadValueAsString());
					return true;
				}
				return base.LoadXmlElement(xr);
			}
		}
	}
}
