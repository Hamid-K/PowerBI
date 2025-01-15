using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E6 RID: 230
	internal static class Util
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x00020845 File Offset: 0x0001EA45
		internal static Version GetVersionFromMaxProtocolVersion(ODataProtocolVersion maxProtocolVersion)
		{
			return Util.ODataVersion4;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000B028 File Offset: 0x00009228
		[Conditional("DEBUG")]
		internal static void DebugInjectFault(string state)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002084E File Offset: 0x0001EA4E
		internal static T CheckArgumentNull<T>([Util.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			return value;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00020860 File Offset: 0x0001EA60
		internal static void CheckArgumentNullAndEmpty([Util.ValidatedNotNullAttribute] string value, string parameterName)
		{
			Util.CheckArgumentNull<string>(value, parameterName);
			Util.CheckArgumentNotEmpty(value, parameterName);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00020871 File Offset: 0x0001EA71
		internal static void CheckArgumentNotEmpty(string value, string parameterName)
		{
			if (value != null && value.Length == 0)
			{
				throw Error.Argument(Strings.Util_EmptyString, parameterName);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002088C File Offset: 0x0001EA8C
		internal static void CheckArgumentNotEmpty<T>(T[] value, string parameterName) where T : class
		{
			Util.CheckArgumentNull<T[]>(value, parameterName);
			if (value.Length == 0)
			{
				throw Error.Argument(Strings.Util_EmptyArray, parameterName);
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == null)
				{
					throw Error.Argument(Strings.Util_NullArrayElement, parameterName);
				}
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000208D9 File Offset: 0x0001EAD9
		internal static EntityParameterSendOption CheckEnumerationValue(EntityParameterSendOption value, string parameterName)
		{
			if (value == EntityParameterSendOption.SendFullProperties || value == EntityParameterSendOption.SendOnlySetProperties)
			{
				return value;
			}
			throw Error.ArgumentOutOfRange(parameterName);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000208EA File Offset: 0x0001EAEA
		internal static MergeOption CheckEnumerationValue(MergeOption value, string parameterName)
		{
			switch (value)
			{
			case MergeOption.AppendOnly:
			case MergeOption.OverwriteChanges:
			case MergeOption.PreserveChanges:
			case MergeOption.NoTracking:
				return value;
			default:
				throw Error.ArgumentOutOfRange(parameterName);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002090C File Offset: 0x0001EB0C
		internal static ODataProtocolVersion CheckEnumerationValue(ODataProtocolVersion value, string parameterName)
		{
			if (value == ODataProtocolVersion.V4)
			{
				return value;
			}
			throw Error.ArgumentOutOfRange(parameterName);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002090C File Offset: 0x0001EB0C
		internal static HttpStack CheckEnumerationValue(HttpStack value, string parameterName)
		{
			if (value == HttpStack.Auto)
			{
				return value;
			}
			throw Error.ArgumentOutOfRange(parameterName);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002091C File Offset: 0x0001EB1C
		internal static char[] GetWhitespaceForTracing(int depth)
		{
			char[] array = Util.whitespaceForTracing;
			while (array.Length <= depth)
			{
				char[] array2 = new char[2 * array.Length];
				array2[0] = '\r';
				array2[1] = '\n';
				for (int i = 2; i < array2.Length; i++)
				{
					array2[i] = ' ';
				}
				Interlocked.CompareExchange<char[]>(ref Util.whitespaceForTracing, array2, array);
				array = array2;
			}
			return array;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002096F File Offset: 0x0001EB6F
		internal static void Dispose<T>(ref T disposable) where T : class, IDisposable
		{
			Util.Dispose<T>(disposable);
			disposable = default(T);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00020983 File Offset: 0x0001EB83
		internal static void Dispose<T>(T disposable) where T : class, IDisposable
		{
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00020998 File Offset: 0x0001EB98
		internal static bool IsKnownClientExcption(Exception ex)
		{
			return ex is DataServiceClientException || ex is DataServiceQueryException || ex is DataServiceRequestException;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000209B5 File Offset: 0x0001EBB5
		internal static T NullCheck<T>(T value, InternalError errorcode) where T : class
		{
			if (value == null)
			{
				Error.ThrowInternalError(errorcode);
			}
			return value;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000209C8 File Offset: 0x0001EBC8
		internal static bool DoesNullAttributeSayTrue(XmlReader reader)
		{
			string attribute = reader.GetAttribute("null", "http://docs.oasis-open.org/odata/ns/metadata");
			return attribute != null && XmlConvert.ToBoolean(attribute);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000209F4 File Offset: 0x0001EBF4
		internal static void SetNextLinkForCollection(object collection, DataServiceQueryContinuation continuation)
		{
			foreach (PropertyInfo propertyInfo in collection.GetType().GetPublicProperties(true))
			{
				if (!(propertyInfo.Name != "Continuation") && propertyInfo.CanWrite && typeof(DataServiceQueryContinuation).IsAssignableFrom(propertyInfo.PropertyType))
				{
					propertyInfo.SetValue(collection, continuation, null);
				}
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00020A7C File Offset: 0x0001EC7C
		internal static bool IsNullableType(Type t)
		{
			return t.IsClass() || Nullable.GetUnderlyingType(t) != null;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00020A99 File Offset: 0x0001EC99
		internal static object ActivatorCreateInstance(Type type, params object[] arguments)
		{
			if (arguments.Length == 0)
			{
				return Activator.CreateInstance(type);
			}
			return Activator.CreateInstance(type, arguments);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00020AAD File Offset: 0x0001ECAD
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static object ConstructorInvoke(ConstructorInfo constructor, object[] arguments)
		{
			if (constructor == null)
			{
				throw new MissingMethodException();
			}
			return constructor.Invoke(arguments);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00020AC5 File Offset: 0x0001ECC5
		internal static bool IsFlagSet(SaveChangesOptions options, SaveChangesOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00020ACD File Offset: 0x0001ECCD
		internal static bool IsBatch(SaveChangesOptions options)
		{
			return Util.IsBatchWithSingleChangeset(options) || Util.IsBatchWithIndependentOperations(options);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00020ADF File Offset: 0x0001ECDF
		internal static bool IsBatchWithSingleChangeset(SaveChangesOptions options)
		{
			return Util.IsFlagSet(options, SaveChangesOptions.BatchWithSingleChangeset);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00020AED File Offset: 0x0001ECED
		internal static bool IsBatchWithIndependentOperations(SaveChangesOptions options)
		{
			return Util.IsFlagSet(options, SaveChangesOptions.BatchWithIndependentOperations);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00020AFC File Offset: 0x0001ECFC
		internal static bool IncludeLinkState(EntityStates x)
		{
			return EntityStates.Modified == x || EntityStates.Unchanged == x;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00020B0C File Offset: 0x0001ED0C
		[Conditional("TRACE")]
		internal static void TraceElement(XmlReader reader, TextWriter writer)
		{
			if (writer != null)
			{
				writer.Write(Util.GetWhitespaceForTracing(2 + reader.Depth), 0, 2 + reader.Depth);
				writer.Write("<{0}", reader.Name);
				if (reader.MoveToFirstAttribute())
				{
					do
					{
						writer.Write(" {0}=\"{1}\"", reader.Name, reader.Value);
					}
					while (reader.MoveToNextAttribute());
					reader.MoveToElement();
				}
				writer.Write(reader.IsEmptyElement ? " />" : ">");
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00020B91 File Offset: 0x0001ED91
		[Conditional("TRACE")]
		internal static void TraceEndElement(XmlReader reader, TextWriter writer, bool indent)
		{
			if (writer != null)
			{
				if (indent)
				{
					writer.Write(Util.GetWhitespaceForTracing(2 + reader.Depth), 0, 2 + reader.Depth);
				}
				writer.Write("</{0}>", reader.Name);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00020BC6 File Offset: 0x0001EDC6
		[Conditional("TRACE")]
		internal static void TraceText(TextWriter writer, string value)
		{
			if (writer != null)
			{
				writer.Write(value);
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		internal static IEnumerable<T> GetEnumerable<T>(IEnumerable enumerable, Func<object, T> valueConverter)
		{
			List<T> list = new List<T>();
			foreach (object obj in enumerable)
			{
				list.Add(valueConverter(obj));
			}
			return list;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00020C30 File Offset: 0x0001EE30
		internal static Version ToVersion(this ODataProtocolVersion protocolVersion)
		{
			if (protocolVersion == ODataProtocolVersion.V4)
			{
				return Util.ODataVersion4;
			}
			if (protocolVersion != ODataProtocolVersion.V401)
			{
				return null;
			}
			return Util.ODataVersion401;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00020C48 File Offset: 0x0001EE48
		internal static Dictionary<string, string> SerializeOperationParameters(this DataServiceContext context, UriOperationParameter[] parameters)
		{
			return parameters.ToDictionary((UriOperationParameter parameter) => parameter.Name, (UriOperationParameter parameter) => Serializer.GetParameterValue(context, parameter));
		}

		// Token: 0x0400036F RID: 879
		internal const string CodeGeneratorToolName = "Microsoft.OData.Service.Design";

		// Token: 0x04000370 RID: 880
		internal const string LoadPropertyMethodName = "LoadProperty";

		// Token: 0x04000371 RID: 881
		internal const string ExecuteMethodName = "Execute";

		// Token: 0x04000372 RID: 882
		internal const string ExecuteMethodNameForVoidResults = "ExecuteVoid";

		// Token: 0x04000373 RID: 883
		internal const string SaveChangesMethodName = "SaveChanges";

		// Token: 0x04000374 RID: 884
		internal const int ODataVersionFieldCount = 2;

		// Token: 0x04000375 RID: 885
		internal static readonly Version ODataVersionEmpty = new Version(0, 0);

		// Token: 0x04000376 RID: 886
		internal static readonly Version ODataVersion4 = new Version(4, 0);

		// Token: 0x04000377 RID: 887
		internal static readonly Version ODataVersion401 = new Version(4, 1);

		// Token: 0x04000378 RID: 888
		internal static readonly Version[] SupportedResponseVersions = new Version[]
		{
			Util.ODataVersion4,
			Util.ODataVersion401
		};

		// Token: 0x04000379 RID: 889
		private static char[] whitespaceForTracing = new char[] { '\r', '\n', ' ', ' ', ' ', ' ', ' ' };

		// Token: 0x020001AF RID: 431
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
