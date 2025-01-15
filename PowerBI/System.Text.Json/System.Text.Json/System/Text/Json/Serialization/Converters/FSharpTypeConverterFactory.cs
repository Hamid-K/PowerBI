using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000DE RID: 222
	[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
	internal sealed class FSharpTypeConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x0002FC81 File Offset: 0x0002DE81
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpTypeConverterFactory()
		{
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002FC89 File Offset: 0x0002DE89
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The ctor is marked RequiresUnreferencedCode.")]
		public override bool CanConvert(Type typeToConvert)
		{
			return FSharpCoreReflectionProxy.IsFSharpType(typeToConvert) && FSharpCoreReflectionProxy.Instance.DetectFSharpKind(typeToConvert) != FSharpCoreReflectionProxy.FSharpKind.Unrecognized;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002FCA8 File Offset: 0x0002DEA8
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The ctor is marked RequiresUnreferencedCode.")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2055:MakeGenericType", Justification = "The ctor is marked RequiresUnreferencedCode.")]
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			object[] array = null;
			Type type2;
			switch (FSharpCoreReflectionProxy.Instance.DetectFSharpKind(typeToConvert))
			{
			case FSharpCoreReflectionProxy.FSharpKind.Option:
			{
				Type type = typeToConvert.GetGenericArguments()[0];
				type2 = typeof(FSharpOptionConverter<, >).MakeGenericType(new Type[] { typeToConvert, type });
				array = new object[] { options.GetConverterInternal(type) };
				break;
			}
			case FSharpCoreReflectionProxy.FSharpKind.ValueOption:
			{
				Type type = typeToConvert.GetGenericArguments()[0];
				type2 = typeof(FSharpValueOptionConverter<, >).MakeGenericType(new Type[] { typeToConvert, type });
				array = new object[] { options.GetConverterInternal(type) };
				break;
			}
			case FSharpCoreReflectionProxy.FSharpKind.List:
			{
				Type type = typeToConvert.GetGenericArguments()[0];
				type2 = typeof(FSharpListConverter<, >).MakeGenericType(new Type[] { typeToConvert, type });
				break;
			}
			case FSharpCoreReflectionProxy.FSharpKind.Set:
			{
				Type type = typeToConvert.GetGenericArguments()[0];
				type2 = typeof(FSharpSetConverter<, >).MakeGenericType(new Type[] { typeToConvert, type });
				break;
			}
			case FSharpCoreReflectionProxy.FSharpKind.Map:
			{
				Type[] genericArguments = typeToConvert.GetGenericArguments();
				Type type3 = genericArguments[0];
				Type type4 = genericArguments[1];
				type2 = typeof(FSharpMapConverter<, , >).MakeGenericType(new Type[] { typeToConvert, type3, type4 });
				break;
			}
			case FSharpCoreReflectionProxy.FSharpKind.Record:
			{
				ObjectConverterFactory objectConverterFactory;
				if ((objectConverterFactory = this._recordConverterFactory) == null)
				{
					objectConverterFactory = (this._recordConverterFactory = new ObjectConverterFactory(false));
				}
				ObjectConverterFactory objectConverterFactory2 = objectConverterFactory;
				return objectConverterFactory2.CreateConverter(typeToConvert, options);
			}
			case FSharpCoreReflectionProxy.FSharpKind.Union:
				return UnsupportedTypeConverterFactory.CreateUnsupportedConverterForType(typeToConvert, SR.FSharpDiscriminatedUnionsNotSupported);
			default:
				throw new Exception();
			}
			return (JsonConverter)Activator.CreateInstance(type2, array);
		}

		// Token: 0x040003FD RID: 1021
		private ObjectConverterFactory _recordConverterFactory;
	}
}
