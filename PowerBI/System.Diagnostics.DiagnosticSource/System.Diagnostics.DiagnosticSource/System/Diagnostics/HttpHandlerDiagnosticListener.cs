using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x02000031 RID: 49
	internal sealed class HttpHandlerDiagnosticListener : DiagnosticListener
	{
		// Token: 0x060001AC RID: 428 RVA: 0x000073F0 File Offset: 0x000055F0
		public override IDisposable Subscribe(IObserver<KeyValuePair<string, object>> observer, Predicate<string> isEnabled)
		{
			IDisposable disposable = base.Subscribe(observer, isEnabled);
			this.Initialize();
			return disposable;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007410 File Offset: 0x00005610
		public override IDisposable Subscribe(IObserver<KeyValuePair<string, object>> observer, Func<string, object, object, bool> isEnabled)
		{
			IDisposable disposable = base.Subscribe(observer, isEnabled);
			this.Initialize();
			return disposable;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007430 File Offset: 0x00005630
		public override IDisposable Subscribe(IObserver<KeyValuePair<string, object>> observer)
		{
			IDisposable disposable = base.Subscribe(observer);
			this.Initialize();
			return disposable;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000744C File Offset: 0x0000564C
		private void Initialize()
		{
			lock (this)
			{
				if (!this.initialized)
				{
					try
					{
						this.initialized = true;
						HttpHandlerDiagnosticListener.PrepareReflectionObjects();
						HttpHandlerDiagnosticListener.PerformInjection();
					}
					catch (Exception ex)
					{
						this.Write("System.Net.Http.InitializationFailed", new
						{
							Exception = ex
						});
					}
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000074BC File Offset: 0x000056BC
		private HttpHandlerDiagnosticListener()
			: base("System.Net.Http.Desktop")
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000074CC File Offset: 0x000056CC
		private void RaiseRequestEvent(HttpWebRequest request)
		{
			if (request.Headers.Get("Request-Id") != null)
			{
				return;
			}
			if (this.IsEnabled("System.Net.Http.Desktop.HttpRequestOut", request, null))
			{
				Activity activity = new Activity("System.Net.Http.Desktop.HttpRequestOut");
				if (this.IsEnabled("System.Net.Http.Desktop.HttpRequestOut.Start"))
				{
					base.StartActivity(activity, new
					{
						Request = request
					});
				}
				else
				{
					activity.Start();
				}
				if (activity.IdFormat == ActivityIdFormat.W3C)
				{
					if (request.Headers.Get("traceparent") == null)
					{
						request.Headers.Add("traceparent", activity.Id);
						string traceStateString = activity.TraceStateString;
						if (traceStateString != null)
						{
							request.Headers.Add("tracestate", traceStateString);
						}
					}
				}
				else if (request.Headers.Get("Request-Id") == null)
				{
					request.Headers.Add("Request-Id", activity.Id);
				}
				if (request.Headers.Get("Correlation-Context") == null)
				{
					using (IEnumerator<KeyValuePair<string, string>> enumerator = activity.Baggage.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							StringBuilder stringBuilder = new StringBuilder();
							do
							{
								KeyValuePair<string, string> keyValuePair = enumerator.Current;
								stringBuilder.Append(WebUtility.UrlEncode(keyValuePair.Key)).Append('=').Append(WebUtility.UrlEncode(keyValuePair.Value))
									.Append(',');
							}
							while (enumerator.MoveNext());
							stringBuilder.Remove(stringBuilder.Length - 1, 1);
							request.Headers.Add("Correlation-Context", stringBuilder.ToString());
						}
					}
				}
				activity.Stop();
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000765C File Offset: 0x0000585C
		private void RaiseResponseEvent(HttpWebRequest request, HttpWebResponse response)
		{
			bool flag = request.Headers.Get("traceparent") != null || request.Headers.Get("Request-Id") != null;
			if (flag && this.IsLastResponse(request, response.StatusCode))
			{
				this.Write("System.Net.Http.Desktop.HttpRequestOut.Stop", new
				{
					Request = request,
					Response = response
				});
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000076B6 File Offset: 0x000058B6
		private void RaiseResponseEvent(HttpWebRequest request, HttpStatusCode statusCode, WebHeaderCollection headers)
		{
			if (request.Headers.Get("Request-Id") != null && this.IsLastResponse(request, statusCode))
			{
				this.Write("System.Net.Http.Desktop.HttpRequestOut.Ex.Stop", new
				{
					Request = request,
					StatusCode = statusCode,
					Headers = headers
				});
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000076E8 File Offset: 0x000058E8
		private bool IsLastResponse(HttpWebRequest request, HttpStatusCode statusCode)
		{
			return !request.AllowAutoRedirect || (statusCode != HttpStatusCode.MultipleChoices && statusCode != HttpStatusCode.MovedPermanently && statusCode != HttpStatusCode.Found && statusCode != HttpStatusCode.SeeOther && statusCode != HttpStatusCode.TemporaryRedirect && statusCode != (HttpStatusCode)308) || HttpHandlerDiagnosticListener.s_autoRedirectsAccessor(request) >= request.MaximumAutomaticRedirections;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007748 File Offset: 0x00005948
		private static void PrepareReflectionObjects()
		{
			Assembly assembly = typeof(ServicePoint).Assembly;
			HttpHandlerDiagnosticListener.s_connectionGroupListField = typeof(ServicePoint).GetField("m_ConnectionGroupList", BindingFlags.Instance | BindingFlags.NonPublic);
			HttpHandlerDiagnosticListener.s_connectionGroupType = ((assembly != null) ? assembly.GetType("System.Net.ConnectionGroup") : null);
			Type type = HttpHandlerDiagnosticListener.s_connectionGroupType;
			HttpHandlerDiagnosticListener.s_connectionListField = ((type != null) ? type.GetField("m_ConnectionList", BindingFlags.Instance | BindingFlags.NonPublic) : null);
			HttpHandlerDiagnosticListener.s_connectionType = ((assembly != null) ? assembly.GetType("System.Net.Connection") : null);
			Type type2 = HttpHandlerDiagnosticListener.s_connectionType;
			HttpHandlerDiagnosticListener.s_writeListField = ((type2 != null) ? type2.GetField("m_WriteList", BindingFlags.Instance | BindingFlags.NonPublic) : null);
			HttpHandlerDiagnosticListener.s_httpResponseAccessor = HttpHandlerDiagnosticListener.CreateFieldGetter<HttpWebRequest, HttpWebResponse>("_HttpResponse", BindingFlags.Instance | BindingFlags.NonPublic);
			HttpHandlerDiagnosticListener.s_autoRedirectsAccessor = HttpHandlerDiagnosticListener.CreateFieldGetter<HttpWebRequest, int>("_AutoRedirects", BindingFlags.Instance | BindingFlags.NonPublic);
			HttpHandlerDiagnosticListener.s_coreResponseAccessor = HttpHandlerDiagnosticListener.CreateFieldGetter<HttpWebRequest, object>("_CoreResponse", BindingFlags.Instance | BindingFlags.NonPublic);
			HttpHandlerDiagnosticListener.s_coreResponseDataType = ((assembly != null) ? assembly.GetType("System.Net.CoreResponseData") : null);
			if (HttpHandlerDiagnosticListener.s_coreResponseDataType != null)
			{
				HttpHandlerDiagnosticListener.s_coreStatusCodeAccessor = HttpHandlerDiagnosticListener.CreateFieldGetter<HttpStatusCode>(HttpHandlerDiagnosticListener.s_coreResponseDataType, "m_StatusCode", BindingFlags.Instance | BindingFlags.Public);
				HttpHandlerDiagnosticListener.s_coreHeadersAccessor = HttpHandlerDiagnosticListener.CreateFieldGetter<WebHeaderCollection>(HttpHandlerDiagnosticListener.s_coreResponseDataType, "m_ResponseHeaders", BindingFlags.Instance | BindingFlags.Public);
			}
			if (HttpHandlerDiagnosticListener.s_connectionGroupListField == null || HttpHandlerDiagnosticListener.s_connectionGroupType == null || HttpHandlerDiagnosticListener.s_connectionListField == null || HttpHandlerDiagnosticListener.s_connectionType == null || HttpHandlerDiagnosticListener.s_writeListField == null || HttpHandlerDiagnosticListener.s_httpResponseAccessor == null || HttpHandlerDiagnosticListener.s_autoRedirectsAccessor == null || HttpHandlerDiagnosticListener.s_coreResponseDataType == null || HttpHandlerDiagnosticListener.s_coreStatusCodeAccessor == null || HttpHandlerDiagnosticListener.s_coreHeadersAccessor == null)
			{
				throw new InvalidOperationException(SR.UnableToInitialize);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000078E0 File Offset: 0x00005AE0
		private static void PerformInjection()
		{
			FieldInfo field = typeof(ServicePointManager).GetField("s_ServicePointTable", BindingFlags.Static | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new InvalidOperationException(SR.UnableAccessServicePointTable);
			}
			Hashtable hashtable = field.GetValue(null) as Hashtable;
			HttpHandlerDiagnosticListener.ServicePointHashtable servicePointHashtable = new HttpHandlerDiagnosticListener.ServicePointHashtable(hashtable ?? new Hashtable());
			field.SetValue(null, servicePointHashtable);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007940 File Offset: 0x00005B40
		private static Func<TClass, TField> CreateFieldGetter<TClass, TField>(string fieldName, BindingFlags flags) where TClass : class
		{
			FieldInfo field = typeof(TClass).GetField(fieldName, flags);
			if (field != null)
			{
				string text = field.ReflectedType.FullName + ".get_" + field.Name;
				DynamicMethod dynamicMethod = new DynamicMethod(text, typeof(TField), new Type[] { typeof(TClass) }, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, field);
				ilgenerator.Emit(OpCodes.Ret);
				return (Func<TClass, TField>)dynamicMethod.CreateDelegate(typeof(Func<TClass, TField>));
			}
			return null;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000079EC File Offset: 0x00005BEC
		private static Func<object, TField> CreateFieldGetter<TField>(Type classType, string fieldName, BindingFlags flags)
		{
			FieldInfo field = classType.GetField(fieldName, flags);
			if (field != null)
			{
				string text = classType.FullName + ".get_" + field.Name;
				DynamicMethod dynamicMethod = new DynamicMethod(text, typeof(TField), new Type[] { typeof(object) }, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Castclass, classType);
				ilgenerator.Emit(OpCodes.Ldfld, field);
				ilgenerator.Emit(OpCodes.Ret);
				return (Func<object, TField>)dynamicMethod.CreateDelegate(typeof(Func<object, TField>));
			}
			return null;
		}

		// Token: 0x040000A8 RID: 168
		internal static HttpHandlerDiagnosticListener s_instance = new HttpHandlerDiagnosticListener();

		// Token: 0x040000A9 RID: 169
		private const string DiagnosticListenerName = "System.Net.Http.Desktop";

		// Token: 0x040000AA RID: 170
		private const string ActivityName = "System.Net.Http.Desktop.HttpRequestOut";

		// Token: 0x040000AB RID: 171
		private const string RequestStartName = "System.Net.Http.Desktop.HttpRequestOut.Start";

		// Token: 0x040000AC RID: 172
		private const string RequestStopName = "System.Net.Http.Desktop.HttpRequestOut.Stop";

		// Token: 0x040000AD RID: 173
		private const string RequestStopExName = "System.Net.Http.Desktop.HttpRequestOut.Ex.Stop";

		// Token: 0x040000AE RID: 174
		private const string InitializationFailed = "System.Net.Http.InitializationFailed";

		// Token: 0x040000AF RID: 175
		private const string RequestIdHeaderName = "Request-Id";

		// Token: 0x040000B0 RID: 176
		private const string CorrelationContextHeaderName = "Correlation-Context";

		// Token: 0x040000B1 RID: 177
		private const string TraceParentHeaderName = "traceparent";

		// Token: 0x040000B2 RID: 178
		private const string TraceStateHeaderName = "tracestate";

		// Token: 0x040000B3 RID: 179
		private bool initialized;

		// Token: 0x040000B4 RID: 180
		private static FieldInfo s_connectionGroupListField;

		// Token: 0x040000B5 RID: 181
		private static Type s_connectionGroupType;

		// Token: 0x040000B6 RID: 182
		private static FieldInfo s_connectionListField;

		// Token: 0x040000B7 RID: 183
		private static Type s_connectionType;

		// Token: 0x040000B8 RID: 184
		private static FieldInfo s_writeListField;

		// Token: 0x040000B9 RID: 185
		private static Func<HttpWebRequest, HttpWebResponse> s_httpResponseAccessor;

		// Token: 0x040000BA RID: 186
		private static Func<HttpWebRequest, int> s_autoRedirectsAccessor;

		// Token: 0x040000BB RID: 187
		private static Func<HttpWebRequest, object> s_coreResponseAccessor;

		// Token: 0x040000BC RID: 188
		private static Func<object, HttpStatusCode> s_coreStatusCodeAccessor;

		// Token: 0x040000BD RID: 189
		private static Func<object, WebHeaderCollection> s_coreHeadersAccessor;

		// Token: 0x040000BE RID: 190
		private static Type s_coreResponseDataType;

		// Token: 0x02000086 RID: 134
		private class HashtableWrapper : Hashtable, IEnumerable
		{
			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0000CE01 File Offset: 0x0000B001
			public override int Count
			{
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0000CE0E File Offset: 0x0000B00E
			public override bool IsReadOnly
			{
				get
				{
					return this._table.IsReadOnly;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CE1B File Offset: 0x0000B01B
			public override bool IsFixedSize
			{
				get
				{
					return this._table.IsFixedSize;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CE28 File Offset: 0x0000B028
			public override bool IsSynchronized
			{
				get
				{
					return this._table.IsSynchronized;
				}
			}

			// Token: 0x170000C0 RID: 192
			public override object this[object key]
			{
				get
				{
					return this._table[key];
				}
				set
				{
					this._table[key] = value;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000CE52 File Offset: 0x0000B052
			public override object SyncRoot
			{
				get
				{
					return this._table.SyncRoot;
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0000CE5F File Offset: 0x0000B05F
			public override ICollection Keys
			{
				get
				{
					return this._table.Keys;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000CE6C File Offset: 0x0000B06C
			public override ICollection Values
			{
				get
				{
					return this._table.Values;
				}
			}

			// Token: 0x06000366 RID: 870 RVA: 0x0000CE79 File Offset: 0x0000B079
			internal HashtableWrapper(Hashtable table)
			{
				this._table = table;
			}

			// Token: 0x06000367 RID: 871 RVA: 0x0000CE88 File Offset: 0x0000B088
			public override void Add(object key, object value)
			{
				this._table.Add(key, value);
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0000CE97 File Offset: 0x0000B097
			public override void Clear()
			{
				this._table.Clear();
			}

			// Token: 0x06000369 RID: 873 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
			public override bool Contains(object key)
			{
				return this._table.Contains(key);
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000CEB2 File Offset: 0x0000B0B2
			public override bool ContainsKey(object key)
			{
				return this._table.ContainsKey(key);
			}

			// Token: 0x0600036B RID: 875 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
			public override bool ContainsValue(object key)
			{
				return this._table.ContainsValue(key);
			}

			// Token: 0x0600036C RID: 876 RVA: 0x0000CECE File Offset: 0x0000B0CE
			public override void CopyTo(Array array, int arrayIndex)
			{
				this._table.CopyTo(array, arrayIndex);
			}

			// Token: 0x0600036D RID: 877 RVA: 0x0000CEDD File Offset: 0x0000B0DD
			public override object Clone()
			{
				return new HttpHandlerDiagnosticListener.HashtableWrapper((Hashtable)this._table.Clone());
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x0600036F RID: 879 RVA: 0x0000CF01 File Offset: 0x0000B101
			public override IDictionaryEnumerator GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0000CF0E File Offset: 0x0000B10E
			public override void Remove(object key)
			{
				this._table.Remove(key);
			}

			// Token: 0x0400019E RID: 414
			protected Hashtable _table;
		}

		// Token: 0x02000087 RID: 135
		private sealed class ServicePointHashtable : HttpHandlerDiagnosticListener.HashtableWrapper
		{
			// Token: 0x06000371 RID: 881 RVA: 0x0000CF1C File Offset: 0x0000B11C
			public ServicePointHashtable(Hashtable table)
				: base(table)
			{
			}

			// Token: 0x170000C4 RID: 196
			public override object this[object key]
			{
				get
				{
					return base[key];
				}
				set
				{
					WeakReference weakReference = value as WeakReference;
					if (weakReference != null && weakReference.IsAlive)
					{
						ServicePoint servicePoint = weakReference.Target as ServicePoint;
						if (servicePoint != null)
						{
							Hashtable hashtable = HttpHandlerDiagnosticListener.s_connectionGroupListField.GetValue(servicePoint) as Hashtable;
							HttpHandlerDiagnosticListener.ConnectionGroupHashtable connectionGroupHashtable = new HttpHandlerDiagnosticListener.ConnectionGroupHashtable(hashtable ?? new Hashtable());
							HttpHandlerDiagnosticListener.s_connectionGroupListField.SetValue(servicePoint, connectionGroupHashtable);
						}
					}
					base[key] = value;
				}
			}
		}

		// Token: 0x02000088 RID: 136
		private sealed class ConnectionGroupHashtable : HttpHandlerDiagnosticListener.HashtableWrapper
		{
			// Token: 0x06000374 RID: 884 RVA: 0x0000CF93 File Offset: 0x0000B193
			public ConnectionGroupHashtable(Hashtable table)
				: base(table)
			{
			}

			// Token: 0x170000C5 RID: 197
			public override object this[object key]
			{
				get
				{
					return base[key];
				}
				set
				{
					if (HttpHandlerDiagnosticListener.s_connectionGroupType.IsInstanceOfType(value))
					{
						ArrayList arrayList = HttpHandlerDiagnosticListener.s_connectionListField.GetValue(value) as ArrayList;
						HttpHandlerDiagnosticListener.ConnectionArrayList connectionArrayList = new HttpHandlerDiagnosticListener.ConnectionArrayList(arrayList ?? new ArrayList());
						HttpHandlerDiagnosticListener.s_connectionListField.SetValue(value, connectionArrayList);
					}
					base[key] = value;
				}
			}
		}

		// Token: 0x02000089 RID: 137
		private class ArrayListWrapper : ArrayList
		{
			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000377 RID: 887 RVA: 0x0000CFF7 File Offset: 0x0000B1F7
			// (set) Token: 0x06000378 RID: 888 RVA: 0x0000D004 File Offset: 0x0000B204
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					this._list.Capacity = value;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000379 RID: 889 RVA: 0x0000D012 File Offset: 0x0000B212
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600037A RID: 890 RVA: 0x0000D01F File Offset: 0x0000B21F
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600037B RID: 891 RVA: 0x0000D02C File Offset: 0x0000B22C
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600037C RID: 892 RVA: 0x0000D039 File Offset: 0x0000B239
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x170000CB RID: 203
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600037F RID: 895 RVA: 0x0000D063 File Offset: 0x0000B263
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000D070 File Offset: 0x0000B270
			internal ArrayListWrapper(ArrayList list)
			{
				this._list = list;
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000D07F File Offset: 0x0000B27F
			public override int Add(object value)
			{
				return this._list.Add(value);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000D08D File Offset: 0x0000B28D
			public override void AddRange(ICollection c)
			{
				this._list.AddRange(c);
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0000D09B File Offset: 0x0000B29B
			public override int BinarySearch(object value)
			{
				return this._list.BinarySearch(value);
			}

			// Token: 0x06000384 RID: 900 RVA: 0x0000D0A9 File Offset: 0x0000B2A9
			public override int BinarySearch(object value, IComparer comparer)
			{
				return this._list.BinarySearch(value, comparer);
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000D0CA File Offset: 0x0000B2CA
			public override void Clear()
			{
				this._list.Clear();
			}

			// Token: 0x06000387 RID: 903 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
			public override object Clone()
			{
				return new HttpHandlerDiagnosticListener.ArrayListWrapper((ArrayList)this._list.Clone());
			}

			// Token: 0x06000388 RID: 904 RVA: 0x0000D0EE File Offset: 0x0000B2EE
			public override bool Contains(object item)
			{
				return this._list.Contains(item);
			}

			// Token: 0x06000389 RID: 905 RVA: 0x0000D0FC File Offset: 0x0000B2FC
			public override void CopyTo(Array array)
			{
				this._list.CopyTo(array);
			}

			// Token: 0x0600038A RID: 906 RVA: 0x0000D10A File Offset: 0x0000B30A
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x0600038B RID: 907 RVA: 0x0000D119 File Offset: 0x0000B319
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x0600038C RID: 908 RVA: 0x0000D12B File Offset: 0x0000B32B
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x0600038D RID: 909 RVA: 0x0000D138 File Offset: 0x0000B338
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x0600038E RID: 910 RVA: 0x0000D147 File Offset: 0x0000B347
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x0600038F RID: 911 RVA: 0x0000D155 File Offset: 0x0000B355
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06000390 RID: 912 RVA: 0x0000D164 File Offset: 0x0000B364
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06000391 RID: 913 RVA: 0x0000D174 File Offset: 0x0000B374
			public override void Insert(int index, object value)
			{
				this._list.Insert(index, value);
			}

			// Token: 0x06000392 RID: 914 RVA: 0x0000D183 File Offset: 0x0000B383
			public override void InsertRange(int index, ICollection c)
			{
				this._list.InsertRange(index, c);
			}

			// Token: 0x06000393 RID: 915 RVA: 0x0000D192 File Offset: 0x0000B392
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06000394 RID: 916 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06000395 RID: 917 RVA: 0x0000D1AF File Offset: 0x0000B3AF
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06000396 RID: 918 RVA: 0x0000D1BF File Offset: 0x0000B3BF
			public override void Remove(object value)
			{
				this._list.Remove(value);
			}

			// Token: 0x06000397 RID: 919 RVA: 0x0000D1CD File Offset: 0x0000B3CD
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
			}

			// Token: 0x06000398 RID: 920 RVA: 0x0000D1DB File Offset: 0x0000B3DB
			public override void RemoveRange(int index, int count)
			{
				this._list.RemoveRange(index, count);
			}

			// Token: 0x06000399 RID: 921 RVA: 0x0000D1EA File Offset: 0x0000B3EA
			public override void Reverse(int index, int count)
			{
				this._list.Reverse(index, count);
			}

			// Token: 0x0600039A RID: 922 RVA: 0x0000D1F9 File Offset: 0x0000B3F9
			public override void SetRange(int index, ICollection c)
			{
				this._list.SetRange(index, c);
			}

			// Token: 0x0600039B RID: 923 RVA: 0x0000D208 File Offset: 0x0000B408
			public override ArrayList GetRange(int index, int count)
			{
				return this._list.GetRange(index, count);
			}

			// Token: 0x0600039C RID: 924 RVA: 0x0000D217 File Offset: 0x0000B417
			public override void Sort()
			{
				this._list.Sort();
			}

			// Token: 0x0600039D RID: 925 RVA: 0x0000D224 File Offset: 0x0000B424
			public override void Sort(IComparer comparer)
			{
				this._list.Sort(comparer);
			}

			// Token: 0x0600039E RID: 926 RVA: 0x0000D232 File Offset: 0x0000B432
			public override void Sort(int index, int count, IComparer comparer)
			{
				this._list.Sort(index, count, comparer);
			}

			// Token: 0x0600039F RID: 927 RVA: 0x0000D242 File Offset: 0x0000B442
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x0000D24F File Offset: 0x0000B44F
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x0000D25D File Offset: 0x0000B45D
			public override void TrimToSize()
			{
				this._list.TrimToSize();
			}

			// Token: 0x0400019F RID: 415
			private ArrayList _list;
		}

		// Token: 0x0200008A RID: 138
		private sealed class ConnectionArrayList : HttpHandlerDiagnosticListener.ArrayListWrapper
		{
			// Token: 0x060003A2 RID: 930 RVA: 0x0000D26A File Offset: 0x0000B46A
			public ConnectionArrayList(ArrayList list)
				: base(list)
			{
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x0000D274 File Offset: 0x0000B474
			public override int Add(object value)
			{
				if (HttpHandlerDiagnosticListener.s_connectionType.IsInstanceOfType(value))
				{
					ArrayList arrayList = HttpHandlerDiagnosticListener.s_writeListField.GetValue(value) as ArrayList;
					HttpHandlerDiagnosticListener.HttpWebRequestArrayList httpWebRequestArrayList = new HttpHandlerDiagnosticListener.HttpWebRequestArrayList(arrayList ?? new ArrayList());
					HttpHandlerDiagnosticListener.s_writeListField.SetValue(value, httpWebRequestArrayList);
				}
				return base.Add(value);
			}
		}

		// Token: 0x0200008B RID: 139
		private sealed class HttpWebRequestArrayList : HttpHandlerDiagnosticListener.ArrayListWrapper
		{
			// Token: 0x060003A4 RID: 932 RVA: 0x0000D2C2 File Offset: 0x0000B4C2
			public HttpWebRequestArrayList(ArrayList list)
				: base(list)
			{
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x0000D2CC File Offset: 0x0000B4CC
			public override int Add(object value)
			{
				HttpWebRequest httpWebRequest = value as HttpWebRequest;
				if (httpWebRequest != null)
				{
					HttpHandlerDiagnosticListener.s_instance.RaiseRequestEvent(httpWebRequest);
				}
				return base.Add(value);
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
			public override void RemoveAt(int index)
			{
				HttpWebRequest httpWebRequest = base[index] as HttpWebRequest;
				if (httpWebRequest != null)
				{
					HttpWebResponse httpWebResponse = HttpHandlerDiagnosticListener.s_httpResponseAccessor(httpWebRequest);
					if (httpWebResponse != null)
					{
						HttpHandlerDiagnosticListener.s_instance.RaiseResponseEvent(httpWebRequest, httpWebResponse);
					}
					else
					{
						object obj = HttpHandlerDiagnosticListener.s_coreResponseAccessor(httpWebRequest);
						if (obj != null && HttpHandlerDiagnosticListener.s_coreResponseDataType.IsInstanceOfType(obj))
						{
							HttpStatusCode httpStatusCode = HttpHandlerDiagnosticListener.s_coreStatusCodeAccessor(obj);
							WebHeaderCollection webHeaderCollection = HttpHandlerDiagnosticListener.s_coreHeadersAccessor(obj);
							HttpHandlerDiagnosticListener.s_instance.RaiseResponseEvent(httpWebRequest, httpStatusCode, webHeaderCollection);
						}
					}
				}
				base.RemoveAt(index);
			}
		}
	}
}
