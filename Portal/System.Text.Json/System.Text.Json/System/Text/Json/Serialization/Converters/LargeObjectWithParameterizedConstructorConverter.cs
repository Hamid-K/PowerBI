using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000EC RID: 236
	internal class LargeObjectWithParameterizedConstructorConverter<T> : ObjectWithParameterizedConstructorConverter<T>
	{
		// Token: 0x06000C6F RID: 3183 RVA: 0x00031AE8 File Offset: 0x0002FCE8
		protected sealed override bool ReadAndCacheConstructorArgument([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonParameterInfo jsonParameterInfo)
		{
			object obj;
			bool flag = jsonParameterInfo.EffectiveConverter.TryReadAsObject(ref reader, jsonParameterInfo.ParameterType, jsonParameterInfo.Options, ref state, out obj);
			if (flag && (obj != null || !jsonParameterInfo.IgnoreNullTokensOnRead))
			{
				((object[])state.Current.CtorArgumentState.Arguments)[jsonParameterInfo.Position] = obj;
				state.Current.MarkRequiredPropertyAsRead(jsonParameterInfo.MatchingProperty);
			}
			return flag;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00031B50 File Offset: 0x0002FD50
		protected sealed override object CreateObject(ref ReadStackFrame frame)
		{
			object[] array = (object[])frame.CtorArgumentState.Arguments;
			frame.CtorArgumentState.Arguments = null;
			Func<object[], T> func = (Func<object[], T>)frame.JsonTypeInfo.CreateObjectWithArgs;
			object obj = func(array);
			ArrayPool<object>.Shared.Return(array, true);
			return obj;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00031BA8 File Offset: 0x0002FDA8
		protected sealed override void InitializeConstructorArgumentCaches(ref ReadStack state, JsonSerializerOptions options)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			List<KeyValuePair<string, JsonParameterInfo>> list = jsonTypeInfo.ParameterCache.List;
			object[] array = ArrayPool<object>.Shared.Rent(list.Count);
			for (int i = 0; i < jsonTypeInfo.ParameterCount; i++)
			{
				JsonParameterInfo value = list[i].Value;
				array[value.Position] = value.DefaultValue;
			}
			state.Current.CtorArgumentState.Arguments = array;
		}
	}
}
