using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000EA RID: 234
	internal static class WebUtil
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00023CF0 File Offset: 0x00021EF0
		private static bool DataServiceCollectionAvailable
		{
			get
			{
				if (WebUtil.dataServiceCollectionAvailable == null)
				{
					try
					{
						WebUtil.dataServiceCollectionAvailable = new bool?(WebUtil.GetDataServiceCollectionOfTType() != null);
					}
					catch (FileNotFoundException)
					{
						WebUtil.dataServiceCollectionAvailable = new bool?(false);
					}
				}
				return WebUtil.dataServiceCollectionAvailable.Value;
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00023D48 File Offset: 0x00021F48
		internal static long CopyStream(Stream input, Stream output, ref byte[] refBuffer)
		{
			long num = 0L;
			byte[] array = refBuffer;
			if (array == null)
			{
				refBuffer = (array = new byte[1000]);
			}
			int num2;
			while (input.CanRead && 0 < (num2 = input.Read(array, 0, array.Length)))
			{
				output.Write(array, 0, num2);
				num += (long)num2;
			}
			return num;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00023D98 File Offset: 0x00021F98
		internal static InvalidOperationException GetHttpWebResponse(InvalidOperationException exception, ref IODataResponseMessage response)
		{
			if (response == null)
			{
				DataServiceTransportException ex = exception as DataServiceTransportException;
				if (ex != null)
				{
					response = ex.Response;
					return (InvalidOperationException)ex.InnerException;
				}
			}
			return exception;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00023DC8 File Offset: 0x00021FC8
		internal static bool SuccessStatusCode(HttpStatusCode status)
		{
			return HttpStatusCode.OK <= status && status < HttpStatusCode.MultipleChoices;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00023DDC File Offset: 0x00021FDC
		internal static bool IsCLRTypeCollection(Type type, ClientEdmModel model)
		{
			if (!PrimitiveType.IsKnownNullableType(type))
			{
				Type implementationType = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>));
				if (implementationType != null && !ClientTypeUtil.TypeIsEntity(implementationType.GetGenericArguments()[0], model))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00023E1E File Offset: 0x0002201E
		internal static bool IsWireTypeCollection(string wireTypeName)
		{
			return CommonUtil.GetCollectionItemTypeName(wireTypeName, false) != null;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00023E2A File Offset: 0x0002202A
		internal static string GetCollectionItemWireTypeName(string wireTypeName)
		{
			return CommonUtil.GetCollectionItemTypeName(wireTypeName, false);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00023E34 File Offset: 0x00022034
		internal static Type GetBackingTypeForCollectionProperty(Type collectionPropertyType)
		{
			Type type;
			if (collectionPropertyType.IsInterface())
			{
				type = typeof(ObservableCollection<>).MakeGenericType(new Type[] { collectionPropertyType.GetGenericArguments()[0] });
			}
			else
			{
				type = collectionPropertyType;
			}
			return type;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00023E71 File Offset: 0x00022071
		internal static T CheckArgumentNull<T>([WebUtil.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			return value;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00023E84 File Offset: 0x00022084
		internal static void ValidateCollection(Type collectionItemType, object propertyValue, string propertyName, bool isDynamicProperty)
		{
			if (!PrimitiveType.IsKnownNullableType(collectionItemType))
			{
				if (collectionItemType.GetInterfaces().SingleOrDefault((Type t) => t == typeof(IEnumerable)) != null)
				{
					throw Error.InvalidOperation(Strings.ClientType_CollectionOfCollectionNotSupported);
				}
			}
			if (propertyValue == null)
			{
				if (propertyName == null)
				{
					throw Error.InvalidOperation(Strings.Collection_NullNonPropertyCollectionNotSupported(collectionItemType));
				}
				if (!isDynamicProperty)
				{
					throw Error.InvalidOperation(Strings.Collection_NullCollectionNotSupported(propertyName));
				}
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00023EF8 File Offset: 0x000220F8
		internal static void ValidatePrimitiveCollectionItem(object itemValue, string propertyName, Type collectionItemType)
		{
			Type type = itemValue.GetType();
			if (!PrimitiveType.IsKnownNullableType(type))
			{
				throw Error.InvalidOperation(Strings.Collection_CollectionTypesInCollectionOfPrimitiveTypesNotAllowed);
			}
			if (collectionItemType.IsAssignableFrom(type))
			{
				return;
			}
			if (propertyName != null)
			{
				throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInCollection(propertyName));
			}
			throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInNonPropertyCollection(collectionItemType));
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00023F44 File Offset: 0x00022144
		internal static void ValidateComplexCollectionItem(object itemValue, string propertyName, Type collectionItemType)
		{
			Type type = itemValue.GetType();
			if (PrimitiveType.IsKnownNullableType(type))
			{
				throw Error.InvalidOperation(Strings.Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed);
			}
			if (collectionItemType.IsAssignableFrom(type))
			{
				return;
			}
			if (propertyName != null)
			{
				throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInCollection(propertyName));
			}
			throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInNonPropertyCollection(collectionItemType));
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00023F90 File Offset: 0x00022190
		internal static Uri ValidateIdentityValue(string identityValue)
		{
			Uri uri;
			try
			{
				uri = UriUtil.CreateUri(identityValue, UriKind.Absolute);
			}
			catch (FormatException)
			{
				throw Error.InvalidOperation(Strings.Context_TrackingExpectsAbsoluteUri);
			}
			return uri;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00023FC4 File Offset: 0x000221C4
		internal static Uri ValidateLocationHeader(string location)
		{
			Uri uri = UriUtil.CreateUri(location, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				throw Error.InvalidOperation(Strings.Context_LocationHeaderExpectsAbsoluteUri);
			}
			return uri;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00023FF0 File Offset: 0x000221F0
		internal static string GetPreferHeaderAndRequestVersion(DataServiceResponsePreference responsePreference, ref Version requestVersion)
		{
			string text = null;
			if (responsePreference != DataServiceResponsePreference.None)
			{
				if (responsePreference == DataServiceResponsePreference.IncludeContent)
				{
					text = "return=representation";
				}
				else
				{
					text = "return=minimal";
				}
				WebUtil.RaiseVersion(ref requestVersion, Util.ODataVersion4);
			}
			return text;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00024020 File Offset: 0x00022220
		internal static void RaiseVersion(ref Version version, Version minimalVersion)
		{
			if (version == null || version < minimalVersion)
			{
				version = minimalVersion;
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00024039 File Offset: 0x00022239
		internal static bool IsDataServiceCollectionType(Type t)
		{
			return WebUtil.DataServiceCollectionAvailable && t == WebUtil.GetDataServiceCollectionOfTType();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002404F File Offset: 0x0002224F
		internal static Type GetDataServiceCollectionOfT(params Type[] typeArguments)
		{
			if (WebUtil.DataServiceCollectionAvailable)
			{
				return WebUtil.GetDataServiceCollectionOfTType().MakeGenericType(typeArguments);
			}
			return null;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00024065 File Offset: 0x00022265
		internal static object GetDefaultValue(Type type)
		{
			return WebUtil.getDefaultValueMethodInfo.MakeGenericMethod(new Type[] { type }).Invoke(null, null);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00024084 File Offset: 0x00022284
		internal static T GetDefaultValue<T>()
		{
			return default(T);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002409C File Offset: 0x0002229C
		internal static void DisposeMessage(IODataResponseMessage responseMessage)
		{
			IDisposable disposable = responseMessage as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000240B9 File Offset: 0x000222B9
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Type GetDataServiceCollectionOfTType()
		{
			return typeof(DataServiceCollection<>);
		}

		// Token: 0x040003A9 RID: 937
		internal const int DefaultBufferSizeForStreamCopy = 65536;

		// Token: 0x040003AA RID: 938
		private static bool? dataServiceCollectionAvailable = null;

		// Token: 0x040003AB RID: 939
		private static MethodInfo getDefaultValueMethodInfo = (MethodInfo)typeof(WebUtil).GetMember("GetDefaultValue", BindingFlags.Static | BindingFlags.NonPublic).Single((MemberInfo m) => ((MethodInfo)m).GetGenericArguments().Count<Type>() == 1);

		// Token: 0x020001C0 RID: 448
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
