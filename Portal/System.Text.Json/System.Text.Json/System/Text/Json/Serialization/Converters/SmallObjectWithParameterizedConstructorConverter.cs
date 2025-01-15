using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000ED RID: 237
	internal sealed class SmallObjectWithParameterizedConstructorConverter<T, TArg0, TArg1, TArg2, TArg3> : ObjectWithParameterizedConstructorConverter<T>
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x00031C2C File Offset: 0x0002FE2C
		protected override object CreateObject(ref ReadStackFrame frame)
		{
			JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3> parameterizedConstructorDelegate = (JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3>)frame.JsonTypeInfo.CreateObjectWithArgs;
			Arguments<TArg0, TArg1, TArg2, TArg3> arguments = (Arguments<TArg0, TArg1, TArg2, TArg3>)frame.CtorArgumentState.Arguments;
			return parameterizedConstructorDelegate(arguments.Arg0, arguments.Arg1, arguments.Arg2, arguments.Arg3);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00031C80 File Offset: 0x0002FE80
		protected override bool ReadAndCacheConstructorArgument([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonParameterInfo jsonParameterInfo)
		{
			Arguments<TArg0, TArg1, TArg2, TArg3> arguments = (Arguments<TArg0, TArg1, TArg2, TArg3>)state.Current.CtorArgumentState.Arguments;
			bool flag;
			switch (jsonParameterInfo.Position)
			{
			case 0:
				flag = SmallObjectWithParameterizedConstructorConverter<T, TArg0, TArg1, TArg2, TArg3>.TryRead<TArg0>(ref state, ref reader, jsonParameterInfo, out arguments.Arg0);
				break;
			case 1:
				flag = SmallObjectWithParameterizedConstructorConverter<T, TArg0, TArg1, TArg2, TArg3>.TryRead<TArg1>(ref state, ref reader, jsonParameterInfo, out arguments.Arg1);
				break;
			case 2:
				flag = SmallObjectWithParameterizedConstructorConverter<T, TArg0, TArg1, TArg2, TArg3>.TryRead<TArg2>(ref state, ref reader, jsonParameterInfo, out arguments.Arg2);
				break;
			case 3:
				flag = SmallObjectWithParameterizedConstructorConverter<T, TArg0, TArg1, TArg2, TArg3>.TryRead<TArg3>(ref state, ref reader, jsonParameterInfo, out arguments.Arg3);
				break;
			default:
				throw new InvalidOperationException();
			}
			return flag;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00031D10 File Offset: 0x0002FF10
		private static bool TryRead<TArg>([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonParameterInfo jsonParameterInfo, out TArg arg)
		{
			JsonParameterInfo<TArg> jsonParameterInfo2 = (JsonParameterInfo<TArg>)jsonParameterInfo;
			TArg targ;
			bool flag2;
			bool flag = jsonParameterInfo2.EffectiveConverter.TryRead(ref reader, jsonParameterInfo2.ParameterType, jsonParameterInfo2.Options, ref state, out targ, out flag2);
			arg = ((targ == null && jsonParameterInfo.IgnoreNullTokensOnRead) ? jsonParameterInfo2.DefaultValue : targ);
			if (flag)
			{
				state.Current.MarkRequiredPropertyAsRead(jsonParameterInfo.MatchingProperty);
			}
			return flag;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00031D78 File Offset: 0x0002FF78
		protected override void InitializeConstructorArgumentCaches(ref ReadStack state, JsonSerializerOptions options)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			Arguments<TArg0, TArg1, TArg2, TArg3> arguments = new Arguments<TArg0, TArg1, TArg2, TArg3>();
			List<KeyValuePair<string, JsonParameterInfo>> list = jsonTypeInfo.ParameterCache.List;
			for (int i = 0; i < jsonTypeInfo.ParameterCount; i++)
			{
				JsonParameterInfo value = list[i].Value;
				switch (value.Position)
				{
				case 0:
					arguments.Arg0 = ((JsonParameterInfo<TArg0>)value).DefaultValue;
					break;
				case 1:
					arguments.Arg1 = ((JsonParameterInfo<TArg1>)value).DefaultValue;
					break;
				case 2:
					arguments.Arg2 = ((JsonParameterInfo<TArg2>)value).DefaultValue;
					break;
				case 3:
					arguments.Arg3 = ((JsonParameterInfo<TArg3>)value).DefaultValue;
					break;
				default:
					throw new InvalidOperationException();
				}
			}
			state.Current.CtorArgumentState.Arguments = arguments;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00031E55 File Offset: 0x00030055
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal override void ConfigureJsonTypeInfoUsingReflection(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			jsonTypeInfo.CreateObjectWithArgs = DefaultJsonTypeInfoResolver.MemberAccessor.CreateParameterizedConstructor<T, TArg0, TArg1, TArg2, TArg3>(base.ConstructorInfo);
		}
	}
}
