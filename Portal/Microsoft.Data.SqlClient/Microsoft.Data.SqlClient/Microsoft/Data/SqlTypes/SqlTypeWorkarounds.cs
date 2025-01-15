using System;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.SqlTypes
{
	// Token: 0x02000016 RID: 22
	internal static class SqlTypeWorkarounds
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0000B4BC File Offset: 0x000096BC
		internal static XmlReader SqlXmlCreateSqlXmlReader(Stream stream, bool closeInput, bool async)
		{
			XmlReaderSettings xmlReaderSettings = (closeInput ? (async ? SqlTypeWorkarounds.s_defaultXmlReaderSettingsAsyncCloseInput : SqlTypeWorkarounds.s_defaultXmlReaderSettingsCloseInput) : SqlTypeWorkarounds.s_defaultXmlReaderSettings);
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000B4EC File Offset: 0x000096EC
		internal static XmlReader SqlXmlCreateSqlXmlReader(TextReader textReader, bool closeInput, bool async)
		{
			XmlReaderSettings xmlReaderSettings = (closeInput ? (async ? SqlTypeWorkarounds.s_defaultXmlReaderSettingsAsyncCloseInput : SqlTypeWorkarounds.s_defaultXmlReaderSettingsCloseInput) : SqlTypeWorkarounds.s_defaultXmlReaderSettings);
			return XmlReader.Create(textReader, xmlReaderSettings);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000B51C File Offset: 0x0000971C
		internal static DateTime SqlDateTimeToDateTime(int daypart, int timepart)
		{
			if ((uint)((long)daypart + 53690L) > 3012153U || timepart > 25919999)
			{
				SqlTypeWorkarounds.ThrowOverflowException();
			}
			long num = (long)daypart * 864000000000L;
			double num2 = (double)timepart / 0.3;
			num2 += 0.5;
			long num3 = (long)num2 * 10000L;
			long num4 = 599266080000000000L + num + num3;
			return new DateTime(num4);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000B58C File Offset: 0x0000978C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception ThrowOverflowException()
		{
			throw SQL.DateTimeOverflow();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000B594 File Offset: 0x00009794
		internal static SqlMoney SqlMoneyCtor(long value, int ignored)
		{
			SqlMoney sqlMoney;
			if (SqlTypeWorkarounds.s_sqlMoneyfactory != null)
			{
				sqlMoney = SqlTypeWorkarounds.s_sqlMoneyfactory(value);
			}
			else
			{
				sqlMoney = new SqlMoney(value / 10000m);
			}
			return sqlMoney;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000B5D3 File Offset: 0x000097D3
		internal static long SqlMoneyToSqlInternalRepresentation(SqlMoney money)
		{
			return SqlTypeWorkarounds.SqlMoneyHelper.s_sqlMoneyToLong(ref money);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000B5E1 File Offset: 0x000097E1
		internal static void SqlDecimalExtractData(SqlDecimal d, out uint data1, out uint data2, out uint data3, out uint data4)
		{
			SqlTypeWorkarounds.SqlDecimalHelper.s_decompose(d, out data1, out data2, out data3, out data4);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000B5F4 File Offset: 0x000097F4
		internal static SqlBinary SqlBinaryCtor(byte[] value, bool ignored)
		{
			SqlBinary sqlBinary;
			if (SqlTypeWorkarounds.s_sqlBinaryfactory != null)
			{
				sqlBinary = SqlTypeWorkarounds.s_sqlBinaryfactory(value);
			}
			else
			{
				sqlBinary = new SqlBinary(value);
			}
			return sqlBinary;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000B620 File Offset: 0x00009820
		internal static SqlGuid SqlGuidCtor(byte[] value, bool ignored)
		{
			SqlGuid sqlGuid;
			if (SqlTypeWorkarounds.s_sqlGuidfactory != null)
			{
				sqlGuid = SqlTypeWorkarounds.s_sqlGuidfactory(value);
			}
			else
			{
				sqlGuid = new SqlGuid(value);
			}
			return sqlGuid;
		}

		// Token: 0x04000033 RID: 51
		private static readonly XmlReaderSettings s_defaultXmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x04000034 RID: 52
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsCloseInput = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x04000035 RID: 53
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsAsyncCloseInput = new XmlReaderSettings
		{
			Async = true,
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x04000036 RID: 54
		internal const SqlCompareOptions SqlStringValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x04000037 RID: 55
		private static readonly Func<long, SqlMoney> s_sqlMoneyfactory = SqlTypeWorkarounds.CtorHelper.CreateFactory<SqlMoney, long, int>();

		// Token: 0x04000038 RID: 56
		private static readonly Func<byte[], SqlBinary> s_sqlBinaryfactory = SqlTypeWorkarounds.CtorHelper.CreateFactory<SqlBinary, byte[], bool>();

		// Token: 0x04000039 RID: 57
		private static readonly Func<byte[], SqlGuid> s_sqlGuidfactory = SqlTypeWorkarounds.CtorHelper.CreateFactory<SqlGuid, byte[], bool>();

		// Token: 0x02000190 RID: 400
		private static class SqlMoneyHelper
		{
			// Token: 0x06001D43 RID: 7491 RVA: 0x0007800C File Offset: 0x0007620C
			internal static SqlTypeWorkarounds.SqlMoneyHelper.SqlMoneyToLongDelegate GetSqlMoneyToLong()
			{
				SqlTypeWorkarounds.SqlMoneyHelper.SqlMoneyToLongDelegate sqlMoneyToLongDelegate = null;
				try
				{
					sqlMoneyToLongDelegate = SqlTypeWorkarounds.SqlMoneyHelper.GetFastSqlMoneyToLong();
				}
				catch
				{
				}
				return sqlMoneyToLongDelegate ?? new SqlTypeWorkarounds.SqlMoneyHelper.SqlMoneyToLongDelegate(SqlTypeWorkarounds.SqlMoneyHelper.FallbackSqlMoneyToLong);
			}

			// Token: 0x06001D44 RID: 7492 RVA: 0x00078048 File Offset: 0x00076248
			private static SqlTypeWorkarounds.SqlMoneyHelper.SqlMoneyToLongDelegate GetFastSqlMoneyToLong()
			{
				MethodInfo method = typeof(SqlMoney).GetMethod("ToSqlInternalRepresentation", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding, null, CallingConventions.Any, new Type[0], null);
				if (method != null && method.ReturnType == typeof(long))
				{
					method.Invoke(new SqlMoney(0), new object[0]);
					return (SqlTypeWorkarounds.SqlMoneyHelper.SqlMoneyToLongDelegate)method.CreateDelegate(typeof(SqlTypeWorkarounds.SqlMoneyHelper.SqlMoneyToLongDelegate), null);
				}
				SqlClientEventSource.Log.TryTraceEvent("SqlTypeWorkarounds.GetFastSqlMoneyToLong | Info | SqlMoney.ToSqlInternalRepresentation() not found. Less efficient fallback method will be used.");
				return null;
			}

			// Token: 0x06001D45 RID: 7493 RVA: 0x000780D4 File Offset: 0x000762D4
			private static long FallbackSqlMoneyToLong(ref SqlMoney value)
			{
				if (value.IsNull)
				{
					return 0L;
				}
				decimal num = value.ToDecimal();
				return (long)(num * 10000m);
			}

			// Token: 0x04001246 RID: 4678
			internal static readonly SqlTypeWorkarounds.SqlMoneyHelper.SqlMoneyToLongDelegate s_sqlMoneyToLong = SqlTypeWorkarounds.SqlMoneyHelper.GetSqlMoneyToLong();

			// Token: 0x02000291 RID: 657
			// (Invoke) Token: 0x06001F55 RID: 8021
			internal delegate long SqlMoneyToLongDelegate(ref SqlMoney @this);
		}

		// Token: 0x02000191 RID: 401
		private static class SqlDecimalHelper
		{
			// Token: 0x06001D47 RID: 7495 RVA: 0x00078114 File Offset: 0x00076314
			private static SqlTypeWorkarounds.SqlDecimalHelper.Decomposer GetDecomposer()
			{
				SqlTypeWorkarounds.SqlDecimalHelper.Decomposer decomposer = null;
				try
				{
					decomposer = SqlTypeWorkarounds.SqlDecimalHelper.GetFastDecomposer();
				}
				catch
				{
				}
				return decomposer ?? new SqlTypeWorkarounds.SqlDecimalHelper.Decomposer(SqlTypeWorkarounds.SqlDecimalHelper.FallbackDecomposer);
			}

			// Token: 0x06001D48 RID: 7496 RVA: 0x00078150 File Offset: 0x00076350
			private static SqlTypeWorkarounds.SqlDecimalHelper.Decomposer GetFastDecomposer()
			{
				if (!typeof(SqlDecimal).IsSerializable)
				{
					SqlClientEventSource.Log.TryTraceEvent("SqlTypeWorkarounds.SqlDecimalHelper.GetFastDecomposer | Info | SqlDecimal isn't Serializable. Less efficient fallback method will be used.");
					return null;
				}
				if (typeof(ISerializable).IsAssignableFrom(typeof(SqlDecimal)))
				{
					SqlClientEventSource.Log.TryTraceEvent("SqlTypeWorkarounds.SqlDecimalHelper.GetFastDecomposer | Info | SqlDecimal is ISerializable. Less efficient fallback method will be used.");
					return null;
				}
				foreach (MethodInfo methodInfo in typeof(SqlDecimal).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.IsDefined(typeof(OnDeserializingAttribute)) || methodInfo.IsDefined(typeof(OnDeserializedAttribute)))
					{
						SqlClientEventSource.Log.TryTraceEvent("SqlTypeWorkarounds.SqlDecimalHelper.GetFastDecomposer | Info | SqlDecimal contains custom serialization logic. Less efficient fallback method will be used.");
						return null;
					}
				}
				FieldInfo fieldInfo = null;
				FieldInfo fieldInfo2 = null;
				FieldInfo fieldInfo3 = null;
				FieldInfo fieldInfo4 = null;
				foreach (MemberInfo memberInfo in FormatterServices.GetSerializableMembers(typeof(SqlDecimal)))
				{
					FieldInfo fieldInfo5 = memberInfo as FieldInfo;
					if (fieldInfo5 != null && fieldInfo5.FieldType == typeof(uint))
					{
						if (fieldInfo5.Name == "m_data1")
						{
							fieldInfo = fieldInfo5;
						}
						else if (fieldInfo5.Name == "m_data2")
						{
							fieldInfo2 = fieldInfo5;
						}
						else if (fieldInfo5.Name == "m_data3")
						{
							fieldInfo3 = fieldInfo5;
						}
						else if (fieldInfo5.Name == "m_data4")
						{
							fieldInfo4 = fieldInfo5;
						}
					}
				}
				if (fieldInfo == null || fieldInfo2 == null || fieldInfo3 == null || fieldInfo4 == null)
				{
					SqlClientEventSource.Log.TryTraceEvent("SqlTypeWorkarounds.SqlDecimalHelper.GetFastDecomposer | Info | Expected SqlDecimal fields are missing. Less efficient fallback method will be used.");
					return null;
				}
				Type type = typeof(uint).MakeByRefType();
				DynamicMethod dynamicMethod = new DynamicMethod("sqldecimal-decomposer", typeof(void), new Type[]
				{
					typeof(SqlDecimal),
					type,
					type,
					type,
					type
				}, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.Emit(OpCodes.Stind_I4);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo2);
				ilgenerator.Emit(OpCodes.Stind_I4);
				ilgenerator.Emit(OpCodes.Ldarg_3);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo3);
				ilgenerator.Emit(OpCodes.Stind_I4);
				ilgenerator.Emit(OpCodes.Ldarg_S, 4);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo4);
				ilgenerator.Emit(OpCodes.Stind_I4);
				ilgenerator.Emit(OpCodes.Ret);
				return (SqlTypeWorkarounds.SqlDecimalHelper.Decomposer)dynamicMethod.CreateDelegate(typeof(SqlTypeWorkarounds.SqlDecimalHelper.Decomposer), null);
			}

			// Token: 0x06001D49 RID: 7497 RVA: 0x00078428 File Offset: 0x00076628
			private static void FallbackDecomposer(SqlDecimal value, out uint data1, out uint data2, out uint data3, out uint data4)
			{
				if (value.IsNull)
				{
					data1 = 0U;
					data2 = 0U;
					data3 = 0U;
					data4 = 0U;
					return;
				}
				int[] data5 = value.Data;
				data4 = (uint)data5[3];
				data3 = (uint)data5[2];
				data2 = (uint)data5[1];
				data1 = (uint)data5[0];
			}

			// Token: 0x04001247 RID: 4679
			internal static readonly SqlTypeWorkarounds.SqlDecimalHelper.Decomposer s_decompose = SqlTypeWorkarounds.SqlDecimalHelper.GetDecomposer();

			// Token: 0x02000292 RID: 658
			// (Invoke) Token: 0x06001F59 RID: 8025
			internal delegate void Decomposer(SqlDecimal value, out uint data1, out uint data2, out uint data3, out uint data4);
		}

		// Token: 0x02000192 RID: 402
		private static class CtorHelper
		{
			// Token: 0x06001D4B RID: 7499 RVA: 0x00078478 File Offset: 0x00076678
			internal static Func<TValue, TInstance> CreateFactory<TInstance, TValue, TIgnored>() where TInstance : struct
			{
				try
				{
					ConstructorInfo constructor = typeof(TInstance).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding, null, new Type[]
					{
						typeof(TValue),
						typeof(TIgnored)
					}, null);
					if (constructor != null)
					{
						SqlTypeWorkarounds.CtorHelper.<>c__DisplayClass0_0<TInstance, TValue, TIgnored> CS$<>8__locals1 = new SqlTypeWorkarounds.CtorHelper.<>c__DisplayClass0_0<TInstance, TValue, TIgnored>();
						CS$<>8__locals1.fnPtr = (IntPtr)ldftn(<CreateFactory>g__DummyNoOp|0_0<TInstance, TValue, TIgnored>);
						CS$<>8__locals1.<CreateFactory>g__InvocationHelper|1(default(TValue));
						CS$<>8__locals1.fnPtr = constructor.MethodHandle.GetFunctionPointer();
						return new Func<TValue, TInstance>(CS$<>8__locals1.<CreateFactory>g__InvocationHelper|1);
					}
				}
				catch
				{
				}
				SqlClientEventSource.Log.TryTraceEvent<string, string, string>("SqlTypeWorkarounds.CtorHelper.CreateFactory | Info | {0}..ctor({1}, {2}) not found. Less efficient fallback method will be used.", typeof(TInstance).Name, typeof(TValue).Name, typeof(TIgnored).Name);
				return null;
			}

			// Token: 0x06001D4C RID: 7500 RVA: 0x0000BB08 File Offset: 0x00009D08
			[CompilerGenerated]
			internal static void <CreateFactory>g__DummyNoOp|0_0<TInstance, TValue, TIgnored>(ref TInstance @this, TValue value, TIgnored ignored) where TInstance : struct
			{
			}
		}
	}
}
