using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Converters;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json.Nodes
{
	// Token: 0x0200005E RID: 94
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonNode
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000216D1 File Offset: 0x0001F8D1
		public JsonNodeOptions? Options
		{
			get
			{
				if (this._options == null && this.Parent != null)
				{
					this._options = this.Parent.Options;
				}
				return this._options;
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000216FF File Offset: 0x0001F8FF
		internal JsonNode(JsonNodeOptions? options = null)
		{
			this._options = options;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00021710 File Offset: 0x0001F910
		public JsonArray AsArray()
		{
			JsonArray jsonArray = this as JsonArray;
			if (jsonArray == null)
			{
				ThrowHelper.ThrowInvalidOperationException_NodeWrongType("JsonArray");
			}
			return jsonArray;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00021734 File Offset: 0x0001F934
		public JsonObject AsObject()
		{
			JsonObject jsonObject = this as JsonObject;
			if (jsonObject == null)
			{
				ThrowHelper.ThrowInvalidOperationException_NodeWrongType("JsonObject");
			}
			return jsonObject;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00021758 File Offset: 0x0001F958
		public JsonValue AsValue()
		{
			JsonValue jsonValue = this as JsonValue;
			if (jsonValue == null)
			{
				ThrowHelper.ThrowInvalidOperationException_NodeWrongType("JsonValue");
			}
			return jsonValue;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0002177A File Offset: 0x0001F97A
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00021782 File Offset: 0x0001F982
		[Nullable(2)]
		public JsonNode Parent
		{
			[NullableContext(2)]
			get
			{
				return this._parent;
			}
			internal set
			{
				this._parent = value;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0002178C File Offset: 0x0001F98C
		public string GetPath()
		{
			if (this.Parent == null)
			{
				return "$";
			}
			List<string> list = new List<string>();
			this.GetPath(list, null);
			StringBuilder stringBuilder = new StringBuilder("$");
			for (int i = list.Count - 1; i >= 0; i--)
			{
				stringBuilder.Append(list[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000709 RID: 1801
		internal abstract void GetPath(List<string> path, JsonNode child);

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x000217E8 File Offset: 0x0001F9E8
		public JsonNode Root
		{
			get
			{
				JsonNode jsonNode = this.Parent;
				if (jsonNode == null)
				{
					return this;
				}
				while (jsonNode.Parent != null)
				{
					jsonNode = jsonNode.Parent;
				}
				return jsonNode;
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00021814 File Offset: 0x0001FA14
		public virtual T GetValue<[Nullable(2)] T>()
		{
			throw new InvalidOperationException(SR.Format(SR.NodeWrongType, "JsonValue"));
		}

		// Token: 0x1700017A RID: 378
		[Nullable(2)]
		public JsonNode this[int index]
		{
			[NullableContext(2)]
			get
			{
				return this.AsArray().GetItem(index);
			}
			[NullableContext(2)]
			set
			{
				this.AsArray().SetItem(index, value);
			}
		}

		// Token: 0x1700017B RID: 379
		[Nullable(2)]
		public JsonNode this[string propertyName]
		{
			[return: Nullable(2)]
			get
			{
				return this.AsObject().GetItem(propertyName);
			}
			[param: Nullable(2)]
			set
			{
				this.AsObject().SetItem(propertyName, value);
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0002186F File Offset: 0x0001FA6F
		public JsonNode DeepClone()
		{
			return this.DeepCloneCore();
		}

		// Token: 0x06000711 RID: 1809
		internal abstract JsonNode DeepCloneCore();

		// Token: 0x06000712 RID: 1810 RVA: 0x00021877 File Offset: 0x0001FA77
		public JsonValueKind GetValueKind()
		{
			return this.GetValueKindCore();
		}

		// Token: 0x06000713 RID: 1811
		internal abstract JsonValueKind GetValueKindCore();

		// Token: 0x06000714 RID: 1812 RVA: 0x00021880 File Offset: 0x0001FA80
		public string GetPropertyName()
		{
			JsonObject jsonObject = this._parent as JsonObject;
			if (jsonObject == null)
			{
				ThrowHelper.ThrowInvalidOperationException_NodeParentWrongType("JsonObject");
			}
			return jsonObject.GetPropertyName(this);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000218B0 File Offset: 0x0001FAB0
		public int GetElementIndex()
		{
			JsonArray jsonArray = this._parent as JsonArray;
			if (jsonArray == null)
			{
				ThrowHelper.ThrowInvalidOperationException_NodeParentWrongType("JsonArray");
			}
			return jsonArray.GetElementIndex(this);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000218DD File Offset: 0x0001FADD
		[NullableContext(2)]
		public static bool DeepEquals(JsonNode node1, JsonNode node2)
		{
			if (node1 == null)
			{
				return node2 == null;
			}
			return node1.DeepEqualsCore(node2);
		}

		// Token: 0x06000717 RID: 1815
		internal abstract bool DeepEqualsCore(JsonNode node);

		// Token: 0x06000718 RID: 1816 RVA: 0x000218F0 File Offset: 0x0001FAF0
		[RequiresUnreferencedCode("Creating JsonValue instances with non-primitive types is not compatible with trimming. It can result in non-primitive types being serialized, which may have their members trimmed.")]
		[RequiresDynamicCode("Creating JsonValue instances with non-primitive types requires generating code at runtime.")]
		public void ReplaceWith<[Nullable(2)] T>(T value)
		{
			JsonNode parent = this._parent;
			JsonObject jsonObject = parent as JsonObject;
			JsonNode jsonNode;
			if (jsonObject != null)
			{
				jsonNode = JsonNode.ConvertFromValue<T>(value, null);
				jsonObject.SetItem(this.GetPropertyName(), jsonNode);
				return;
			}
			JsonArray jsonArray = parent as JsonArray;
			if (jsonArray == null)
			{
				return;
			}
			jsonNode = JsonNode.ConvertFromValue<T>(value, null);
			jsonArray.SetItem(this.GetElementIndex(), jsonNode);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00021958 File Offset: 0x0001FB58
		internal void AssignParent(JsonNode parent)
		{
			if (this.Parent != null)
			{
				ThrowHelper.ThrowInvalidOperationException_NodeAlreadyHasParent();
			}
			for (JsonNode jsonNode = parent; jsonNode != null; jsonNode = jsonNode.Parent)
			{
				if (jsonNode == this)
				{
					ThrowHelper.ThrowInvalidOperationException_NodeCycleDetected();
				}
			}
			this.Parent = parent;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00021990 File Offset: 0x0001FB90
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal static JsonNode ConvertFromValue<T>(T value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			JsonNode jsonNode = value as JsonNode;
			if (jsonNode != null)
			{
				return jsonNode;
			}
			if (value is JsonElement)
			{
				JsonElement jsonElement = value as JsonElement;
				return JsonNodeConverter.Create(jsonElement, options);
			}
			JsonTypeInfo<T> jsonTypeInfo = (JsonTypeInfo<T>)JsonSerializerOptions.Default.GetTypeInfo(typeof(T));
			return new JsonValueCustomized<T>(value, jsonTypeInfo, options);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00021A00 File Offset: 0x0001FC00
		public static implicit operator JsonNode(bool value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00021A1C File Offset: 0x0001FC1C
		[NullableContext(2)]
		public static implicit operator JsonNode(bool? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00021A38 File Offset: 0x0001FC38
		public static implicit operator JsonNode(byte value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00021A54 File Offset: 0x0001FC54
		[NullableContext(2)]
		public static implicit operator JsonNode(byte? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00021A70 File Offset: 0x0001FC70
		public static implicit operator JsonNode(char value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00021A8C File Offset: 0x0001FC8C
		[NullableContext(2)]
		public static implicit operator JsonNode(char? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		public static implicit operator JsonNode(DateTime value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00021AC4 File Offset: 0x0001FCC4
		[NullableContext(2)]
		public static implicit operator JsonNode(DateTime? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		public static implicit operator JsonNode(DateTimeOffset value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00021AFC File Offset: 0x0001FCFC
		[NullableContext(2)]
		public static implicit operator JsonNode(DateTimeOffset? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00021B18 File Offset: 0x0001FD18
		public static implicit operator JsonNode(decimal value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00021B34 File Offset: 0x0001FD34
		[NullableContext(2)]
		public static implicit operator JsonNode(decimal? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00021B50 File Offset: 0x0001FD50
		public static implicit operator JsonNode(double value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00021B6C File Offset: 0x0001FD6C
		[NullableContext(2)]
		public static implicit operator JsonNode(double? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00021B88 File Offset: 0x0001FD88
		public static implicit operator JsonNode(Guid value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		[NullableContext(2)]
		public static implicit operator JsonNode(Guid? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00021BC0 File Offset: 0x0001FDC0
		public static implicit operator JsonNode(short value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00021BDC File Offset: 0x0001FDDC
		[NullableContext(2)]
		public static implicit operator JsonNode(short? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		public static implicit operator JsonNode(int value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00021C14 File Offset: 0x0001FE14
		[NullableContext(2)]
		public static implicit operator JsonNode(int? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00021C30 File Offset: 0x0001FE30
		public static implicit operator JsonNode(long value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00021C4C File Offset: 0x0001FE4C
		[NullableContext(2)]
		public static implicit operator JsonNode(long? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00021C68 File Offset: 0x0001FE68
		[CLSCompliant(false)]
		public static implicit operator JsonNode(sbyte value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00021C84 File Offset: 0x0001FE84
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static implicit operator JsonNode(sbyte? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00021CA0 File Offset: 0x0001FEA0
		public static implicit operator JsonNode(float value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00021CBC File Offset: 0x0001FEBC
		[NullableContext(2)]
		public static implicit operator JsonNode(float? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00021CD8 File Offset: 0x0001FED8
		[NullableContext(2)]
		[return: NotNullIfNotNull("value")]
		public static implicit operator JsonNode(string value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00021CF4 File Offset: 0x0001FEF4
		[CLSCompliant(false)]
		public static implicit operator JsonNode(ushort value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00021D10 File Offset: 0x0001FF10
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static implicit operator JsonNode(ushort? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00021D2C File Offset: 0x0001FF2C
		[CLSCompliant(false)]
		public static implicit operator JsonNode(uint value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00021D48 File Offset: 0x0001FF48
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static implicit operator JsonNode(uint? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00021D64 File Offset: 0x0001FF64
		[CLSCompliant(false)]
		public static implicit operator JsonNode(ulong value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00021D80 File Offset: 0x0001FF80
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static implicit operator JsonNode(ulong? value)
		{
			return JsonValue.Create(value, null);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00021D9C File Offset: 0x0001FF9C
		public static explicit operator bool(JsonNode value)
		{
			return value.GetValue<bool>();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		[NullableContext(2)]
		public static explicit operator bool?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new bool?(value.GetValue<bool>());
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00021DC9 File Offset: 0x0001FFC9
		public static explicit operator byte(JsonNode value)
		{
			return value.GetValue<byte>();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00021DD4 File Offset: 0x0001FFD4
		[NullableContext(2)]
		public static explicit operator byte?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new byte?(value.GetValue<byte>());
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00021DF9 File Offset: 0x0001FFF9
		public static explicit operator char(JsonNode value)
		{
			return value.GetValue<char>();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00021E04 File Offset: 0x00020004
		[NullableContext(2)]
		public static explicit operator char?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new char?(value.GetValue<char>());
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00021E29 File Offset: 0x00020029
		public static explicit operator DateTime(JsonNode value)
		{
			return value.GetValue<DateTime>();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00021E34 File Offset: 0x00020034
		[NullableContext(2)]
		public static explicit operator DateTime?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new DateTime?(value.GetValue<DateTime>());
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00021E59 File Offset: 0x00020059
		public static explicit operator DateTimeOffset(JsonNode value)
		{
			return value.GetValue<DateTimeOffset>();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00021E64 File Offset: 0x00020064
		[NullableContext(2)]
		public static explicit operator DateTimeOffset?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new DateTimeOffset?(value.GetValue<DateTimeOffset>());
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00021E89 File Offset: 0x00020089
		public static explicit operator decimal(JsonNode value)
		{
			return value.GetValue<decimal>();
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00021E94 File Offset: 0x00020094
		[NullableContext(2)]
		public static explicit operator decimal?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new decimal?(value.GetValue<decimal>());
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00021EB9 File Offset: 0x000200B9
		public static explicit operator double(JsonNode value)
		{
			return value.GetValue<double>();
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00021EC4 File Offset: 0x000200C4
		[NullableContext(2)]
		public static explicit operator double?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new double?(value.GetValue<double>());
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00021EE9 File Offset: 0x000200E9
		public static explicit operator Guid(JsonNode value)
		{
			return value.GetValue<Guid>();
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00021EF4 File Offset: 0x000200F4
		[NullableContext(2)]
		public static explicit operator Guid?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new Guid?(value.GetValue<Guid>());
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00021F19 File Offset: 0x00020119
		public static explicit operator short(JsonNode value)
		{
			return value.GetValue<short>();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00021F24 File Offset: 0x00020124
		[NullableContext(2)]
		public static explicit operator short?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new short?(value.GetValue<short>());
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00021F49 File Offset: 0x00020149
		public static explicit operator int(JsonNode value)
		{
			return value.GetValue<int>();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00021F54 File Offset: 0x00020154
		[NullableContext(2)]
		public static explicit operator int?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new int?(value.GetValue<int>());
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00021F79 File Offset: 0x00020179
		public static explicit operator long(JsonNode value)
		{
			return value.GetValue<long>();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00021F84 File Offset: 0x00020184
		[NullableContext(2)]
		public static explicit operator long?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new long?(value.GetValue<long>());
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00021FA9 File Offset: 0x000201A9
		[CLSCompliant(false)]
		public static explicit operator sbyte(JsonNode value)
		{
			return value.GetValue<sbyte>();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00021FB4 File Offset: 0x000201B4
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator sbyte?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new sbyte?(value.GetValue<sbyte>());
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00021FD9 File Offset: 0x000201D9
		public static explicit operator float(JsonNode value)
		{
			return value.GetValue<float>();
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00021FE4 File Offset: 0x000201E4
		[NullableContext(2)]
		public static explicit operator float?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new float?(value.GetValue<float>());
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00022009 File Offset: 0x00020209
		[NullableContext(2)]
		public static explicit operator string(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return value.GetValue<string>();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00022016 File Offset: 0x00020216
		[CLSCompliant(false)]
		public static explicit operator ushort(JsonNode value)
		{
			return value.GetValue<ushort>();
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00022020 File Offset: 0x00020220
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator ushort?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new ushort?(value.GetValue<ushort>());
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00022045 File Offset: 0x00020245
		[CLSCompliant(false)]
		public static explicit operator uint(JsonNode value)
		{
			return value.GetValue<uint>();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00022050 File Offset: 0x00020250
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator uint?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new uint?(value.GetValue<uint>());
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00022075 File Offset: 0x00020275
		[CLSCompliant(false)]
		public static explicit operator ulong(JsonNode value)
		{
			return value.GetValue<ulong>();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00022080 File Offset: 0x00020280
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator ulong?(JsonNode value)
		{
			if (value == null)
			{
				return null;
			}
			return new ulong?(value.GetValue<ulong>());
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000220A8 File Offset: 0x000202A8
		[NullableContext(2)]
		public static JsonNode Parse(ref Utf8JsonReader reader, JsonNodeOptions? nodeOptions = null)
		{
			JsonElement jsonElement = JsonElement.ParseValue(ref reader);
			return JsonNodeConverter.Create(jsonElement, nodeOptions);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000220C4 File Offset: 0x000202C4
		[return: Nullable(2)]
		public static JsonNode Parse([StringSyntax("Json")] string json, JsonNodeOptions? nodeOptions = null, JsonDocumentOptions documentOptions = default(JsonDocumentOptions))
		{
			if (json == null)
			{
				ThrowHelper.ThrowArgumentNullException("json");
			}
			JsonElement jsonElement = JsonElement.ParseValue(json, documentOptions);
			return JsonNodeConverter.Create(jsonElement, nodeOptions);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000220F0 File Offset: 0x000202F0
		[NullableContext(0)]
		[return: Nullable(2)]
		public static JsonNode Parse(ReadOnlySpan<byte> utf8Json, JsonNodeOptions? nodeOptions = null, JsonDocumentOptions documentOptions = default(JsonDocumentOptions))
		{
			JsonElement jsonElement = JsonElement.ParseValue(utf8Json, documentOptions);
			return JsonNodeConverter.Create(jsonElement, nodeOptions);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002210C File Offset: 0x0002030C
		[return: Nullable(2)]
		public static JsonNode Parse(Stream utf8Json, JsonNodeOptions? nodeOptions = null, JsonDocumentOptions documentOptions = default(JsonDocumentOptions))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonElement jsonElement = JsonElement.ParseValue(utf8Json, documentOptions);
			return JsonNodeConverter.Create(jsonElement, nodeOptions);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00022138 File Offset: 0x00020338
		[return: Nullable(new byte[] { 1, 2 })]
		public static async Task<JsonNode> ParseAsync(Stream utf8Json, JsonNodeOptions? nodeOptions = null, JsonDocumentOptions documentOptions = default(JsonDocumentOptions), CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonDocument jsonDocument = await JsonDocument.ParseAsyncCoreUnrented(utf8Json, documentOptions, cancellationToken).ConfigureAwait(false);
			JsonDocument jsonDocument2 = jsonDocument;
			return JsonNodeConverter.Create(jsonDocument2.RootElement, nodeOptions);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00022194 File Offset: 0x00020394
		public string ToJsonString([Nullable(2)] JsonSerializerOptions options = null)
		{
			string text;
			using (PooledByteBufferWriter pooledByteBufferWriter = this.WriteToPooledBuffer(options, (options != null) ? options.GetWriterOptions() : default(JsonWriterOptions), 16384))
			{
				text = JsonHelpers.Utf8GetString(pooledByteBufferWriter.WrittenMemory.Span);
			}
			return text;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000221F4 File Offset: 0x000203F4
		public override string ToString()
		{
			if (this is JsonValue)
			{
				JsonValue<string> jsonValue = this as JsonValue<string>;
				if (jsonValue != null)
				{
					return jsonValue.Value;
				}
				JsonValue<JsonElement> jsonValue2 = this as JsonValue<JsonElement>;
				if (jsonValue2 != null && jsonValue2.Value.ValueKind == JsonValueKind.String)
				{
					return jsonValue2.Value.GetString();
				}
			}
			string text;
			using (PooledByteBufferWriter pooledByteBufferWriter = this.WriteToPooledBuffer(null, new JsonWriterOptions
			{
				Indented = true
			}, 16384))
			{
				text = JsonHelpers.Utf8GetString(pooledByteBufferWriter.WrittenMemory.Span);
			}
			return text;
		}

		// Token: 0x06000764 RID: 1892
		public abstract void WriteTo(Utf8JsonWriter writer, [Nullable(2)] JsonSerializerOptions options = null);

		// Token: 0x06000765 RID: 1893 RVA: 0x00022290 File Offset: 0x00020490
		internal PooledByteBufferWriter WriteToPooledBuffer(JsonSerializerOptions options = null, JsonWriterOptions writerOptions = default(JsonWriterOptions), int bufferSize = 16384)
		{
			PooledByteBufferWriter pooledByteBufferWriter = new PooledByteBufferWriter(bufferSize);
			PooledByteBufferWriter pooledByteBufferWriter2;
			using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(pooledByteBufferWriter, writerOptions))
			{
				this.WriteTo(utf8JsonWriter, options);
				pooledByteBufferWriter2 = pooledByteBufferWriter;
			}
			return pooledByteBufferWriter2;
		}

		// Token: 0x04000282 RID: 642
		private JsonNode _parent;

		// Token: 0x04000283 RID: 643
		private JsonNodeOptions? _options;
	}
}
