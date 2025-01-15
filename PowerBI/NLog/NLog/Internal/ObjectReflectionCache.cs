using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200012F RID: 303
	internal class ObjectReflectionCache
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x00025DF4 File Offset: 0x00023FF4
		public ObjectReflectionCache.ObjectPropertyList LookupObjectProperties(object value)
		{
			ObjectReflectionCache.ObjectPropertyList objectPropertyList;
			if (this.TryLookupExpandoObject(value, out objectPropertyList))
			{
				return objectPropertyList;
			}
			Type type = value.GetType();
			ObjectReflectionCache.ObjectPropertyInfos objectPropertyInfos = ObjectReflectionCache.BuildObjectPropertyInfos(value, type);
			this._objectTypeCache.TryAddValue(type, objectPropertyInfos);
			return new ObjectReflectionCache.ObjectPropertyList(value, objectPropertyInfos.Properties, objectPropertyInfos.FastLookup);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00025E40 File Offset: 0x00024040
		public bool TryLookupExpandoObject(object value, out ObjectReflectionCache.ObjectPropertyList objectPropertyList)
		{
			IDictionary<string, object> dictionary;
			if ((dictionary = value as IDictionary<string, object>) != null)
			{
				objectPropertyList = new ObjectReflectionCache.ObjectPropertyList(dictionary);
				return true;
			}
			DynamicObject dynamicObject;
			if ((dynamicObject = value as DynamicObject) != null)
			{
				Dictionary<string, object> dictionary2 = ObjectReflectionCache.DynamicObjectToDict(dynamicObject);
				objectPropertyList = new ObjectReflectionCache.ObjectPropertyList(dictionary2);
				return true;
			}
			Type type = value.GetType();
			ObjectReflectionCache.ObjectPropertyInfos simpleToString;
			if (this._objectTypeCache.TryGetValue(type, out simpleToString))
			{
				if (!simpleToString.HasFastLookup)
				{
					ObjectReflectionCache.FastPropertyLookup[] array = ObjectReflectionCache.BuildFastLookup(simpleToString.Properties, false);
					simpleToString = new ObjectReflectionCache.ObjectPropertyInfos(simpleToString.Properties, array);
					this._objectTypeCache.TryAddValue(type, simpleToString);
				}
				objectPropertyList = new ObjectReflectionCache.ObjectPropertyList(value, simpleToString.Properties, simpleToString.FastLookup);
				return true;
			}
			if (ObjectReflectionCache._objectTypeOverride.Count > 0)
			{
				Dictionary<Type, Func<object, IDictionary<string, object>>> objectTypeOverride = ObjectReflectionCache._objectTypeOverride;
				lock (objectTypeOverride)
				{
					Func<object, IDictionary<string, object>> func;
					if (ObjectReflectionCache._objectTypeOverride.TryGetValue(type, out func))
					{
						IDictionary<string, object> dictionary3 = func(value);
						if (dictionary3 != null && dictionary3.Count > 0)
						{
							objectPropertyList = new ObjectReflectionCache.ObjectPropertyList(dictionary3);
							return true;
						}
						simpleToString = ObjectReflectionCache.ObjectPropertyInfos.SimpleToString;
						objectPropertyList = new ObjectReflectionCache.ObjectPropertyList(value, simpleToString.Properties, simpleToString.FastLookup);
						return true;
					}
				}
			}
			if (ObjectReflectionCache.TryExtractExpandoObject(type, out simpleToString))
			{
				this._objectTypeCache.TryAddValue(type, simpleToString);
				objectPropertyList = new ObjectReflectionCache.ObjectPropertyList(value, simpleToString.Properties, simpleToString.FastLookup);
				return true;
			}
			objectPropertyList = default(ObjectReflectionCache.ObjectPropertyList);
			return false;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00025FC4 File Offset: 0x000241C4
		private static bool TryExtractExpandoObject(Type objectType, out ObjectReflectionCache.ObjectPropertyInfos propertyInfos)
		{
			foreach (Type type in objectType.GetInterfaces())
			{
				if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(IDictionary<, >) && type.GetGenericArguments()[0] == typeof(string))
				{
					ObjectReflectionCache.IDictionaryEnumerator dictionaryEnumerator = (ObjectReflectionCache.IDictionaryEnumerator)Activator.CreateInstance(typeof(ObjectReflectionCache.DictionaryEnumerator<, >).MakeGenericType(type.GetGenericArguments()));
					propertyInfos = new ObjectReflectionCache.ObjectPropertyInfos(null, new ObjectReflectionCache.FastPropertyLookup[]
					{
						new ObjectReflectionCache.FastPropertyLookup(string.Empty, TypeCode.Object, (object o, object[] p) => dictionaryEnumerator.GetEnumerator(o))
					});
					return true;
				}
			}
			propertyInfos = default(ObjectReflectionCache.ObjectPropertyInfos);
			return false;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00026090 File Offset: 0x00024290
		private static ObjectReflectionCache.ObjectPropertyInfos BuildObjectPropertyInfos(object value, Type objectType)
		{
			ObjectReflectionCache.ObjectPropertyInfos objectPropertyInfos;
			if (ObjectReflectionCache.ConvertSimpleToString(objectType))
			{
				objectPropertyInfos = ObjectReflectionCache.ObjectPropertyInfos.SimpleToString;
			}
			else
			{
				PropertyInfo[] publicProperties = ObjectReflectionCache.GetPublicProperties(objectType);
				if (value is Exception)
				{
					ObjectReflectionCache.FastPropertyLookup[] array = ObjectReflectionCache.BuildFastLookup(publicProperties, true);
					objectPropertyInfos = new ObjectReflectionCache.ObjectPropertyInfos(publicProperties, array);
				}
				else if (publicProperties.Length == 0)
				{
					objectPropertyInfos = ObjectReflectionCache.ObjectPropertyInfos.SimpleToString;
				}
				else
				{
					objectPropertyInfos = new ObjectReflectionCache.ObjectPropertyInfos(publicProperties, null);
				}
			}
			return objectPropertyInfos;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000260E8 File Offset: 0x000242E8
		private static bool ConvertSimpleToString(Type objectType)
		{
			return typeof(IFormattable).IsAssignableFrom(objectType) || typeof(Uri).IsAssignableFrom(objectType) || typeof(MemberInfo).IsAssignableFrom(objectType) || typeof(Assembly).IsAssignableFrom(objectType);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00026148 File Offset: 0x00024348
		private static PropertyInfo[] GetPublicProperties(Type type)
		{
			PropertyInfo[] array = null;
			try
			{
				array = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Failed to get object properties for type: {0}", new object[] { type });
			}
			if (array != null)
			{
				PropertyInfo[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					if (!ObjectReflectionCache.ValidPublicProperty(array2[i]))
					{
						array = array.Where((PropertyInfo p) => ObjectReflectionCache.ValidPublicProperty(p)).ToArray<PropertyInfo>();
						break;
					}
				}
			}
			return array ?? ArrayHelper.Empty<PropertyInfo>();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000261DC File Offset: 0x000243DC
		private static bool ValidPublicProperty(PropertyInfo p)
		{
			return p.CanRead && p.GetIndexParameters().Length == 0 && p.GetGetMethod() != null;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00026200 File Offset: 0x00024400
		private static ObjectReflectionCache.FastPropertyLookup[] BuildFastLookup(PropertyInfo[] properties, bool includeType)
		{
			int num = (includeType ? 1 : 0);
			ObjectReflectionCache.FastPropertyLookup[] array = new ObjectReflectionCache.FastPropertyLookup[properties.Length + num];
			if (includeType)
			{
				array[0] = new ObjectReflectionCache.FastPropertyLookup("Type", TypeCode.String, (object o, object[] p) => o.GetType().ToString());
			}
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				Type returnType = getMethod.ReturnType;
				ReflectionHelpers.LateBoundMethod lateBoundMethod = ReflectionHelpers.CreateLateBoundMethod(getMethod);
				TypeCode typeCode = Type.GetTypeCode(returnType);
				array[num++] = new ObjectReflectionCache.FastPropertyLookup(propertyInfo.Name, typeCode, lateBoundMethod);
			}
			return array;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000262A8 File Offset: 0x000244A8
		private static Dictionary<string, object> DynamicObjectToDict(DynamicObject d)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (string text in d.GetDynamicMemberNames())
			{
				object obj;
				if (d.TryGetMember(new ObjectReflectionCache.GetBinderAdapter(text), out obj))
				{
					dictionary[text] = obj;
				}
			}
			return dictionary;
		}

		// Token: 0x04000409 RID: 1033
		private readonly MruCache<Type, ObjectReflectionCache.ObjectPropertyInfos> _objectTypeCache = new MruCache<Type, ObjectReflectionCache.ObjectPropertyInfos>(10000);

		// Token: 0x0400040A RID: 1034
		private static readonly Dictionary<Type, Func<object, IDictionary<string, object>>> _objectTypeOverride = new Dictionary<Type, Func<object, IDictionary<string, object>>>();

		// Token: 0x0400040B RID: 1035
		private const BindingFlags PublicProperties = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x0400040C RID: 1036
		private static readonly IDictionary<string, object> EmptyDictionary = default(SortHelpers.ReadOnlySingleBucketDictionary<string, object>);

		// Token: 0x02000269 RID: 617
		public struct ObjectPropertyList : IEnumerable<ObjectReflectionCache.ObjectPropertyList.PropertyValue>, IEnumerable
		{
			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x06001617 RID: 5655 RVA: 0x0003A066 File Offset: 0x00038266
			public bool ConvertToString
			{
				get
				{
					PropertyInfo[] properties = this._properties;
					return properties != null && properties.Length == 0;
				}
			}

			// Token: 0x06001618 RID: 5656 RVA: 0x0003A078 File Offset: 0x00038278
			internal ObjectPropertyList(object value, PropertyInfo[] properties, ObjectReflectionCache.FastPropertyLookup[] fastLookup)
			{
				this._object = value;
				this._properties = properties;
				this._fastLookup = fastLookup;
			}

			// Token: 0x06001619 RID: 5657 RVA: 0x0003A08F File Offset: 0x0003828F
			public ObjectPropertyList(IDictionary<string, object> value)
			{
				this._object = value;
				this._properties = null;
				this._fastLookup = ObjectReflectionCache.ObjectPropertyList.CreateIDictionaryEnumerator;
			}

			// Token: 0x0600161A RID: 5658 RVA: 0x0003A0AC File Offset: 0x000382AC
			public bool TryGetPropertyValue(string name, out ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue)
			{
				if (this._properties != null)
				{
					if (this._fastLookup != null)
					{
						return this.TryFastLookupPropertyValue(name, out propertyValue);
					}
					return this.TrySlowLookupPropertyValue(name, out propertyValue);
				}
				else
				{
					IDictionary<string, object> dictionary;
					if ((dictionary = this._object as IDictionary<string, object>) == null)
					{
						return this.TryListLookupPropertyValue(name, out propertyValue);
					}
					object obj;
					if (dictionary.TryGetValue(name, out obj))
					{
						propertyValue = new ObjectReflectionCache.ObjectPropertyList.PropertyValue(name, obj, TypeCode.Object);
						return true;
					}
					propertyValue = default(ObjectReflectionCache.ObjectPropertyList.PropertyValue);
					return false;
				}
			}

			// Token: 0x0600161B RID: 5659 RVA: 0x0003A118 File Offset: 0x00038318
			private bool TryFastLookupPropertyValue(string name, out ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue)
			{
				int hashCode = ObjectReflectionCache.ObjectPropertyList.NameComparer.GetHashCode(name);
				foreach (ObjectReflectionCache.FastPropertyLookup fastPropertyLookup in this._fastLookup)
				{
					if (fastPropertyLookup.NameHashCode == hashCode && ObjectReflectionCache.ObjectPropertyList.NameComparer.Equals(fastPropertyLookup.Name, name))
					{
						propertyValue = new ObjectReflectionCache.ObjectPropertyList.PropertyValue(this._object, fastPropertyLookup);
						return true;
					}
				}
				propertyValue = default(ObjectReflectionCache.ObjectPropertyList.PropertyValue);
				return false;
			}

			// Token: 0x0600161C RID: 5660 RVA: 0x0003A188 File Offset: 0x00038388
			private bool TrySlowLookupPropertyValue(string name, out ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue)
			{
				foreach (PropertyInfo propertyInfo in this._properties)
				{
					if (ObjectReflectionCache.ObjectPropertyList.NameComparer.Equals(propertyInfo.Name, name))
					{
						propertyValue = new ObjectReflectionCache.ObjectPropertyList.PropertyValue(this._object, propertyInfo);
						return true;
					}
				}
				propertyValue = default(ObjectReflectionCache.ObjectPropertyList.PropertyValue);
				return false;
			}

			// Token: 0x0600161D RID: 5661 RVA: 0x0003A1E0 File Offset: 0x000383E0
			private bool TryListLookupPropertyValue(string name, out ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue)
			{
				foreach (ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue2 in this)
				{
					if (ObjectReflectionCache.ObjectPropertyList.NameComparer.Equals(propertyValue2.Name, name))
					{
						propertyValue = propertyValue2;
						return true;
					}
				}
				propertyValue = default(ObjectReflectionCache.ObjectPropertyList.PropertyValue);
				return false;
			}

			// Token: 0x0600161E RID: 5662 RVA: 0x0003A250 File Offset: 0x00038450
			public override string ToString()
			{
				object @object = this._object;
				return ((@object != null) ? @object.ToString() : null) ?? "null";
			}

			// Token: 0x0600161F RID: 5663 RVA: 0x0003A270 File Offset: 0x00038470
			public ObjectReflectionCache.ObjectPropertyList.Enumerator GetEnumerator()
			{
				if (this._properties != null)
				{
					return new ObjectReflectionCache.ObjectPropertyList.Enumerator(this._object, this._properties, this._fastLookup);
				}
				return new ObjectReflectionCache.ObjectPropertyList.Enumerator((IEnumerator<KeyValuePair<string, object>>)this._fastLookup[0].ValueLookup(this._object, null));
			}

			// Token: 0x06001620 RID: 5664 RVA: 0x0003A2C4 File Offset: 0x000384C4
			IEnumerator<ObjectReflectionCache.ObjectPropertyList.PropertyValue> IEnumerable<ObjectReflectionCache.ObjectPropertyList.PropertyValue>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001621 RID: 5665 RVA: 0x0003A2D1 File Offset: 0x000384D1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040006A5 RID: 1701
			internal static readonly StringComparer NameComparer = StringComparer.Ordinal;

			// Token: 0x040006A6 RID: 1702
			private static readonly ObjectReflectionCache.FastPropertyLookup[] CreateIDictionaryEnumerator = new ObjectReflectionCache.FastPropertyLookup[]
			{
				new ObjectReflectionCache.FastPropertyLookup(string.Empty, TypeCode.Object, (object o, object[] p) => ((IDictionary<string, object>)o).GetEnumerator())
			};

			// Token: 0x040006A7 RID: 1703
			private readonly object _object;

			// Token: 0x040006A8 RID: 1704
			private readonly PropertyInfo[] _properties;

			// Token: 0x040006A9 RID: 1705
			private readonly ObjectReflectionCache.FastPropertyLookup[] _fastLookup;

			// Token: 0x020002CF RID: 719
			public struct PropertyValue
			{
				// Token: 0x1700044C RID: 1100
				// (get) Token: 0x06001795 RID: 6037 RVA: 0x0003D7BA File Offset: 0x0003B9BA
				public TypeCode TypeCode
				{
					get
					{
						if (this.Value != null)
						{
							return this._typecode;
						}
						return TypeCode.Empty;
					}
				}

				// Token: 0x06001796 RID: 6038 RVA: 0x0003D7CC File Offset: 0x0003B9CC
				public PropertyValue(string name, object value, TypeCode typeCode)
				{
					this.Name = name;
					this.Value = value;
					this._typecode = typeCode;
				}

				// Token: 0x06001797 RID: 6039 RVA: 0x0003D7E3 File Offset: 0x0003B9E3
				public PropertyValue(object owner, PropertyInfo propertyInfo)
				{
					this.Name = propertyInfo.Name;
					this.Value = propertyInfo.GetValue(owner, null);
					this._typecode = TypeCode.Object;
				}

				// Token: 0x06001798 RID: 6040 RVA: 0x0003D806 File Offset: 0x0003BA06
				public PropertyValue(object owner, ObjectReflectionCache.FastPropertyLookup fastProperty)
				{
					this.Name = fastProperty.Name;
					this.Value = fastProperty.ValueLookup(owner, null);
					this._typecode = fastProperty.TypeCode;
				}

				// Token: 0x040007B4 RID: 1972
				public readonly string Name;

				// Token: 0x040007B5 RID: 1973
				public readonly object Value;

				// Token: 0x040007B6 RID: 1974
				private readonly TypeCode _typecode;
			}

			// Token: 0x020002D0 RID: 720
			public struct Enumerator : IEnumerator<ObjectReflectionCache.ObjectPropertyList.PropertyValue>, IDisposable, IEnumerator
			{
				// Token: 0x06001799 RID: 6041 RVA: 0x0003D833 File Offset: 0x0003BA33
				internal Enumerator(object owner, PropertyInfo[] properties, ObjectReflectionCache.FastPropertyLookup[] fastLookup)
				{
					this._owner = owner;
					this._properties = properties;
					this._fastLookup = fastLookup;
					this._index = -1;
					this._enumerator = null;
				}

				// Token: 0x0600179A RID: 6042 RVA: 0x0003D858 File Offset: 0x0003BA58
				internal Enumerator(IEnumerator<KeyValuePair<string, object>> enumerator)
				{
					this._owner = enumerator;
					this._properties = null;
					this._fastLookup = null;
					this._index = 0;
					this._enumerator = enumerator;
				}

				// Token: 0x1700044D RID: 1101
				// (get) Token: 0x0600179B RID: 6043 RVA: 0x0003D880 File Offset: 0x0003BA80
				public ObjectReflectionCache.ObjectPropertyList.PropertyValue Current
				{
					get
					{
						ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue;
						try
						{
							if (this._fastLookup != null)
							{
								propertyValue = new ObjectReflectionCache.ObjectPropertyList.PropertyValue(this._owner, this._fastLookup[this._index]);
							}
							else if (this._properties != null)
							{
								propertyValue = new ObjectReflectionCache.ObjectPropertyList.PropertyValue(this._owner, this._properties[this._index]);
							}
							else
							{
								KeyValuePair<string, object> keyValuePair = this._enumerator.Current;
								string key = keyValuePair.Key;
								keyValuePair = this._enumerator.Current;
								propertyValue = new ObjectReflectionCache.ObjectPropertyList.PropertyValue(key, keyValuePair.Value, TypeCode.Object);
							}
						}
						catch (Exception ex)
						{
							InternalLogger.Warn(ex, "Failed to get property value for object: {0}", new object[] { this._owner });
							propertyValue = default(ObjectReflectionCache.ObjectPropertyList.PropertyValue);
						}
						return propertyValue;
					}
				}

				// Token: 0x1700044E RID: 1102
				// (get) Token: 0x0600179C RID: 6044 RVA: 0x0003D93C File Offset: 0x0003BB3C
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600179D RID: 6045 RVA: 0x0003D949 File Offset: 0x0003BB49
				public void Dispose()
				{
					IEnumerator<KeyValuePair<string, object>> enumerator = this._enumerator;
					if (enumerator == null)
					{
						return;
					}
					enumerator.Dispose();
				}

				// Token: 0x0600179E RID: 6046 RVA: 0x0003D95C File Offset: 0x0003BB5C
				public bool MoveNext()
				{
					if (this._properties != null)
					{
						int num = this._index + 1;
						this._index = num;
						int num2 = num;
						ObjectReflectionCache.FastPropertyLookup[] fastLookup = this._fastLookup;
						return num2 < ((fastLookup != null) ? fastLookup.Length : this._properties.Length);
					}
					return this._enumerator.MoveNext();
				}

				// Token: 0x0600179F RID: 6047 RVA: 0x0003D9A6 File Offset: 0x0003BBA6
				public void Reset()
				{
					if (this._properties != null)
					{
						this._index = -1;
						return;
					}
					this._enumerator.Reset();
				}

				// Token: 0x040007B7 RID: 1975
				private readonly object _owner;

				// Token: 0x040007B8 RID: 1976
				private readonly PropertyInfo[] _properties;

				// Token: 0x040007B9 RID: 1977
				private readonly ObjectReflectionCache.FastPropertyLookup[] _fastLookup;

				// Token: 0x040007BA RID: 1978
				private readonly IEnumerator<KeyValuePair<string, object>> _enumerator;

				// Token: 0x040007BB RID: 1979
				private int _index;
			}
		}

		// Token: 0x0200026A RID: 618
		internal struct FastPropertyLookup
		{
			// Token: 0x06001623 RID: 5667 RVA: 0x0003A317 File Offset: 0x00038517
			public FastPropertyLookup(string name, TypeCode typeCode, ReflectionHelpers.LateBoundMethod valueLookup)
			{
				this.Name = name;
				this.ValueLookup = valueLookup;
				this.TypeCode = typeCode;
				this.NameHashCode = ObjectReflectionCache.ObjectPropertyList.NameComparer.GetHashCode(name);
			}

			// Token: 0x040006AA RID: 1706
			public readonly string Name;

			// Token: 0x040006AB RID: 1707
			public readonly ReflectionHelpers.LateBoundMethod ValueLookup;

			// Token: 0x040006AC RID: 1708
			public readonly TypeCode TypeCode;

			// Token: 0x040006AD RID: 1709
			public readonly int NameHashCode;
		}

		// Token: 0x0200026B RID: 619
		private struct ObjectPropertyInfos : IEquatable<ObjectReflectionCache.ObjectPropertyInfos>
		{
			// Token: 0x06001624 RID: 5668 RVA: 0x0003A33F File Offset: 0x0003853F
			public ObjectPropertyInfos(PropertyInfo[] properties, ObjectReflectionCache.FastPropertyLookup[] fastLookup)
			{
				this.Properties = properties;
				this.FastLookup = fastLookup;
			}

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06001625 RID: 5669 RVA: 0x0003A34F File Offset: 0x0003854F
			public bool HasFastLookup
			{
				get
				{
					return this.FastLookup != null;
				}
			}

			// Token: 0x06001626 RID: 5670 RVA: 0x0003A35C File Offset: 0x0003855C
			public bool Equals(ObjectReflectionCache.ObjectPropertyInfos other)
			{
				if (this.Properties == other.Properties)
				{
					ObjectReflectionCache.FastPropertyLookup[] fastLookup = this.FastLookup;
					int? num = ((fastLookup != null) ? new int?(fastLookup.Length) : null);
					ObjectReflectionCache.FastPropertyLookup[] fastLookup2 = other.FastLookup;
					int? num2 = ((fastLookup2 != null) ? new int?(fastLookup2.Length) : null);
					return (num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null));
				}
				return false;
			}

			// Token: 0x040006AE RID: 1710
			public readonly PropertyInfo[] Properties;

			// Token: 0x040006AF RID: 1711
			public readonly ObjectReflectionCache.FastPropertyLookup[] FastLookup;

			// Token: 0x040006B0 RID: 1712
			public static readonly ObjectReflectionCache.ObjectPropertyInfos SimpleToString = new ObjectReflectionCache.ObjectPropertyInfos(ArrayHelper.Empty<PropertyInfo>(), ArrayHelper.Empty<ObjectReflectionCache.FastPropertyLookup>());
		}

		// Token: 0x0200026C RID: 620
		private sealed class GetBinderAdapter : GetMemberBinder
		{
			// Token: 0x06001628 RID: 5672 RVA: 0x0003A3EA File Offset: 0x000385EA
			internal GetBinderAdapter(string name)
				: base(name, false)
			{
			}

			// Token: 0x06001629 RID: 5673 RVA: 0x0003A3F4 File Offset: 0x000385F4
			public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
			{
				return target;
			}
		}

		// Token: 0x0200026D RID: 621
		private interface IDictionaryEnumerator
		{
			// Token: 0x0600162A RID: 5674
			IEnumerator<KeyValuePair<string, object>> GetEnumerator(object value);
		}

		// Token: 0x0200026E RID: 622
		internal sealed class DictionaryEnumerator<TKey, TValue> : ObjectReflectionCache.IDictionaryEnumerator
		{
			// Token: 0x0600162B RID: 5675 RVA: 0x0003A3F8 File Offset: 0x000385F8
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator(object value)
			{
				IDictionary<TKey, TValue> dictionary;
				if ((dictionary = value as IDictionary<TKey, TValue>) != null)
				{
					return this.YieldEnumerator(dictionary);
				}
				return ObjectReflectionCache.EmptyDictionary.GetEnumerator();
			}

			// Token: 0x0600162C RID: 5676 RVA: 0x0003A421 File Offset: 0x00038621
			private IEnumerator<KeyValuePair<string, object>> YieldEnumerator(IDictionary<TKey, TValue> dictionary)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
				{
					TKey key = keyValuePair.Key;
					yield return new KeyValuePair<string, object>(key.ToString(), keyValuePair.Value);
				}
				IEnumerator<KeyValuePair<TKey, TValue>> enumerator = null;
				yield break;
				yield break;
			}
		}
	}
}
