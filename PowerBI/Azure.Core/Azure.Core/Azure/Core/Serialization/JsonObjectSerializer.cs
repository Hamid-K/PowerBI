using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Serialization
{
	// Token: 0x020000C5 RID: 197
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresUnreferencedCode("This class uses reflection-based JSON serialization and deserialization that is not compatible with trimming.")]
	[RequiresDynamicCode("This class uses reflection-based JSON serialization and deserialization that is not compatible with trimming.")]
	public class JsonObjectSerializer : ObjectSerializer, IMemberNameConverter
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001658E File Offset: 0x0001478E
		public static JsonObjectSerializer Default { get; } = new JsonObjectSerializer();

		// Token: 0x0600068B RID: 1675 RVA: 0x00016595 File Offset: 0x00014795
		public JsonObjectSerializer()
			: this(new JsonSerializerOptions())
		{
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000165A2 File Offset: 0x000147A2
		public JsonObjectSerializer(JsonSerializerOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this._options = options;
			this._cache = new ConcurrentDictionary<MemberInfo, string>();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000165CC File Offset: 0x000147CC
		public override void Serialize(Stream stream, [Nullable(2)] object value, Type inputType, CancellationToken cancellationToken)
		{
			byte[] array = JsonSerializer.SerializeToUtf8Bytes(value, inputType, this._options);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000165F4 File Offset: 0x000147F4
		public override async ValueTask SerializeAsync(Stream stream, [Nullable(2)] object value, Type inputType, CancellationToken cancellationToken)
		{
			await JsonSerializer.SerializeAsync(stream, value, inputType, this._options, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00016658 File Offset: 0x00014858
		[return: Nullable(2)]
		public override object Deserialize(Stream stream, Type returnType, CancellationToken cancellationToken)
		{
			object obj;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				obj = JsonSerializer.Deserialize(memoryStream.ToArray(), returnType, this._options);
			}
			return obj;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000166A8 File Offset: 0x000148A8
		[return: Nullable(new byte[] { 0, 2 })]
		public override ValueTask<object> DeserializeAsync(Stream stream, Type returnType, CancellationToken cancellationToken)
		{
			return JsonSerializer.DeserializeAsync(stream, returnType, this._options, cancellationToken);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000166B8 File Offset: 0x000148B8
		[NullableContext(2)]
		[return: Nullable(1)]
		public override BinaryData Serialize(object value, Type inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SerializeToBinaryDataInternal(value, inputType);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000166C2 File Offset: 0x000148C2
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public override ValueTask<BinaryData> SerializeAsync(object value, Type inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ValueTask<BinaryData>(this.SerializeToBinaryDataInternal(value, inputType));
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000166D1 File Offset: 0x000148D1
		[NullableContext(2)]
		[return: Nullable(1)]
		private BinaryData SerializeToBinaryDataInternal(object value, Type inputType)
		{
			return new BinaryData(JsonSerializer.SerializeToUtf8Bytes(value, inputType ?? ((value != null) ? value.GetType() : null) ?? typeof(object), this._options));
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00016703 File Offset: 0x00014903
		[return: Nullable(2)]
		string IMemberNameConverter.ConvertMemberName(MemberInfo member)
		{
			Argument.AssertNotNull<MemberInfo>(member, "member");
			return this._cache.GetOrAdd(member, delegate(MemberInfo m)
			{
				PropertyInfo propertyInfo = m as PropertyInfo;
				if (propertyInfo != null)
				{
					if (propertyInfo.GetIndexParameters().Length != 0)
					{
						return null;
					}
					MethodInfo getMethod = propertyInfo.GetMethod;
					if (getMethod == null || !getMethod.IsPublic)
					{
						MethodInfo setMethod = propertyInfo.SetMethod;
						if (setMethod == null || !setMethod.IsPublic)
						{
							goto IL_0064;
						}
					}
					JsonIgnoreAttribute customAttribute = propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>();
					if (customAttribute != null && JsonObjectSerializer.GetCondition(customAttribute) == 1)
					{
						return null;
					}
					if (propertyInfo.GetCustomAttribute<JsonExtensionDataAttribute>() != null)
					{
						return null;
					}
					return this.GetPropertyName(propertyInfo);
				}
				IL_0064:
				return null;
			});
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00016728 File Offset: 0x00014928
		private static int GetCondition(JsonIgnoreAttribute attribute)
		{
			if (!JsonObjectSerializer.s_jsonIgnoreAttributeConditionInitialized)
			{
				JsonObjectSerializer.s_jsonIgnoreAttributeCondition = typeof(JsonIgnoreAttribute).GetProperty("Condition", BindingFlags.Instance | BindingFlags.Public);
				JsonObjectSerializer.s_jsonIgnoreAttributeConditionInitialized = true;
			}
			if (JsonObjectSerializer.s_jsonIgnoreAttributeCondition != null)
			{
				return (int)JsonObjectSerializer.s_jsonIgnoreAttributeCondition.GetValue(attribute);
			}
			return 1;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001677C File Offset: 0x0001497C
		private string GetPropertyName(MemberInfo memberInfo)
		{
			JsonPropertyNameAttribute customAttribute = memberInfo.GetCustomAttribute(false);
			if (customAttribute != null)
			{
				string name = customAttribute.Name;
				if (name == null)
				{
					throw new InvalidOperationException(string.Format("The JSON property name for '{0}.{1}' cannot be null.", memberInfo.DeclaringType, memberInfo.Name));
				}
				return name;
			}
			else
			{
				if (this._options.PropertyNamingPolicy == null)
				{
					return memberInfo.Name;
				}
				string text = this._options.PropertyNamingPolicy.ConvertName(memberInfo.Name);
				if (text == null)
				{
					throw new InvalidOperationException(string.Format("The JSON property name for '{0}.{1}' cannot be null.", memberInfo.DeclaringType, memberInfo.Name));
				}
				return text;
			}
		}

		// Token: 0x04000283 RID: 643
		private const int JsonIgnoreConditionAlways = 1;

		// Token: 0x04000284 RID: 644
		[Nullable(2)]
		private static PropertyInfo s_jsonIgnoreAttributeCondition;

		// Token: 0x04000285 RID: 645
		private static bool s_jsonIgnoreAttributeConditionInitialized;

		// Token: 0x04000286 RID: 646
		[Nullable(new byte[] { 1, 1, 2 })]
		private readonly ConcurrentDictionary<MemberInfo, string> _cache;

		// Token: 0x04000287 RID: 647
		private readonly JsonSerializerOptions _options;
	}
}
