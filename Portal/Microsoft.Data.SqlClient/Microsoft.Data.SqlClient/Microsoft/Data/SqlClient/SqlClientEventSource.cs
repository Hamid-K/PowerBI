using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200005C RID: 92
	[EventSource(Name = "Microsoft.Data.SqlClient.EventSource")]
	internal class SqlClientEventSource : SqlClientEventSourceBase
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x00013494 File Offset: 0x00011694
		private SqlClientEventSource()
		{
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001349C File Offset: 0x0001169C
		[NonEvent]
		internal bool IsExecutionTraceEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)1L);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000134AB File Offset: 0x000116AB
		[NonEvent]
		internal bool IsTraceEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)2L);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000134BA File Offset: 0x000116BA
		[NonEvent]
		internal bool IsScopeEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)4L);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000134C9 File Offset: 0x000116C9
		[NonEvent]
		internal bool IsNotificationTraceEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)8L);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000134D8 File Offset: 0x000116D8
		[NonEvent]
		internal bool IsNotificationScopeEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000134E8 File Offset: 0x000116E8
		[NonEvent]
		internal bool IsPoolerTraceEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)32L);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000134F8 File Offset: 0x000116F8
		[NonEvent]
		internal bool IsPoolerScopeEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)64L);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00013508 File Offset: 0x00011708
		[NonEvent]
		internal bool IsAdvancedTraceOn()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords)128L);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001351B File Offset: 0x0001171B
		[NonEvent]
		internal bool IsCorrelationEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)512L);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001352E File Offset: 0x0001172E
		[NonEvent]
		internal bool IsStateDumpEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)1024L);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00013541 File Offset: 0x00011741
		[NonEvent]
		internal bool IsSNITraceEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)2048L);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00013554 File Offset: 0x00011754
		[NonEvent]
		internal bool IsSNIScopeEnabled()
		{
			return SqlClientEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)4096L);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00013567 File Offset: 0x00011767
		private string GetFormattedMessage(string className, string memberName, string eventType, string message)
		{
			return new StringBuilder(className).Append(".").Append(memberName).Append(eventType)
				.Append(message)
				.ToString();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00013594 File Offset: 0x00011794
		[NonEvent]
		internal void TraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_0033;
				}
			}
			obj = ptr.ToString();
			IL_0033:
			object obj2 = obj ?? "null";
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj3;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj3 = null;
					goto IL_006D;
				}
			}
			obj3 = ptr2.ToString();
			IL_006D:
			this.Trace(string.Format(message, obj2, obj3 ?? "null"));
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00013624 File Offset: 0x00011824
		[NonEvent]
		internal void TraceEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_0033;
				}
			}
			obj = ptr.ToString();
			IL_0033:
			object obj2 = obj ?? "null";
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj3;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj3 = null;
					goto IL_006D;
				}
			}
			obj3 = ptr2.ToString();
			IL_006D:
			object obj4 = obj3 ?? "null";
			ref T2 ptr3 = ref args2;
			T2 t3 = default(T2);
			object obj5;
			if (t3 == null)
			{
				t3 = args2;
				ptr3 = ref t3;
				if (t3 == null)
				{
					obj5 = null;
					goto IL_00A7;
				}
			}
			obj5 = ptr3.ToString();
			IL_00A7:
			this.Trace(string.Format(message, obj2, obj4, obj5 ?? "null"));
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000136EC File Offset: 0x000118EC
		[NonEvent]
		internal void TraceEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			object[] array = new object[4];
			int num = 0;
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_003B;
				}
			}
			obj = ptr.ToString();
			IL_003B:
			array[num] = obj ?? "null";
			int num2 = 1;
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj2;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj2 = null;
					goto IL_0078;
				}
			}
			obj2 = ptr2.ToString();
			IL_0078:
			array[num2] = obj2 ?? "null";
			int num3 = 2;
			ref T2 ptr3 = ref args2;
			T2 t3 = default(T2);
			object obj3;
			if (t3 == null)
			{
				t3 = args2;
				ptr3 = ref t3;
				if (t3 == null)
				{
					obj3 = null;
					goto IL_00B5;
				}
			}
			obj3 = ptr3.ToString();
			IL_00B5:
			array[num3] = obj3 ?? "null";
			int num4 = 3;
			ref T3 ptr4 = ref args3;
			T3 t4 = default(T3);
			object obj4;
			if (t4 == null)
			{
				t4 = args3;
				ptr4 = ref t4;
				if (t4 == null)
				{
					obj4 = null;
					goto IL_00F2;
				}
			}
			obj4 = ptr4.ToString();
			IL_00F2:
			array[num4] = obj4 ?? "null";
			this.Trace(string.Format(message, array));
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000137FF File Offset: 0x000119FF
		[NonEvent]
		internal void TryTraceEvent(string message)
		{
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				this.Trace(message);
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00013814 File Offset: 0x00011A14
		[NonEvent]
		internal void TryTraceEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				this.Trace(string.Format(message, obj ?? "null"));
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00013874 File Offset: 0x00011A74
		[NonEvent]
		internal void TryTraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				this.Trace(string.Format(message, obj2, obj3 ?? "null"));
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00013910 File Offset: 0x00011B10
		[NonEvent]
		internal void TryTraceEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				this.Trace(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000139E8 File Offset: 0x00011BE8
		[NonEvent]
		internal void TryTraceEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				this.Trace(string.Format(message, array));
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00013B0C File Offset: 0x00011D0C
		[NonEvent]
		internal void TryTraceEvent<T0, T1, T2, T3, T4>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4)
		{
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				object[] array = new object[5];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				this.Trace(string.Format(message, array));
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00013C70 File Offset: 0x00011E70
		[NonEvent]
		internal void TryTraceEvent<T0, T1, T2, T3, T4, T5>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4, T5 args5)
		{
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				object[] array = new object[6];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				int num6 = 5;
				ref T5 ptr6 = ref args5;
				T5 t6 = default(T5);
				object obj6;
				if (t6 == null)
				{
					t6 = args5;
					ptr6 = ref t6;
					if (t6 == null)
					{
						obj6 = null;
						goto IL_0181;
					}
				}
				obj6 = ptr6.ToString();
				IL_0181:
				array[num6] = obj6 ?? "null";
				this.Trace(string.Format(message, array));
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00013E14 File Offset: 0x00012014
		[NonEvent]
		internal long TryScopeEnterEvent(string className, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsScopeEnabled())
			{
				StringBuilder stringBuilder = new StringBuilder(className);
				stringBuilder.Append(".").Append(memberName).Append(" | INFO | SCOPE | Entering Scope {0}");
				return this.SNIScopeEnter(stringBuilder.ToString());
			}
			return 0L;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00013E60 File Offset: 0x00012060
		[NonEvent]
		internal long TryScopeEnterEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsScopeEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				return this.ScopeEnter(string.Format(message, obj ?? "null"));
			}
			return 0L;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00013EC4 File Offset: 0x000120C4
		[NonEvent]
		internal long TryScopeEnterEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			if (SqlClientEventSource.Log.IsScopeEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				return this.ScopeEnter(string.Format(message, obj2, obj3 ?? "null"));
			}
			return 0L;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00013F64 File Offset: 0x00012164
		[NonEvent]
		internal long TryScopeEnterEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			if (SqlClientEventSource.Log.IsScopeEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				return this.ScopeEnter(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
			return 0L;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00014040 File Offset: 0x00012240
		[NonEvent]
		internal long TryScopeEnterEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			if (SqlClientEventSource.Log.IsScopeEnabled())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				return this.ScopeEnter(string.Format(message, array));
			}
			return 0L;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00014165 File Offset: 0x00012365
		[NonEvent]
		internal void TryScopeLeaveEvent(long scopeId)
		{
			if (SqlClientEventSource.Log.IsScopeEnabled())
			{
				this.ScopeLeave(string.Format("Exit Scope {0}", scopeId));
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001418C File Offset: 0x0001238C
		[NonEvent]
		internal void TryBeginExecuteEvent(int objectId, string dataSource, string database, string commandText, Guid? connectionId, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsExecutionTraceEnabled())
			{
				this.BeginExecute(objectId, dataSource, database, commandText, this.GetFormattedMessage("SqlCommand", memberName, " | INFO | ", string.Format("Object Id {0}, Client connection Id {1}, Command Text {2}", objectId, connectionId, commandText)));
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000141DC File Offset: 0x000123DC
		[NonEvent]
		internal void TryEndExecuteEvent(int objectId, int compositeState, int sqlExceptionNumber, Guid? connectionId, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsExecutionTraceEnabled())
			{
				this.EndExecute(objectId, compositeState, sqlExceptionNumber, this.GetFormattedMessage("SqlCommand", memberName, " | INFO | ", string.Format("Object Id {0}, Client Connection Id {1}, Composite State {2}, Sql Exception Number {3}", new object[] { objectId, connectionId, compositeState, sqlExceptionNumber })));
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00014248 File Offset: 0x00012448
		[NonEvent]
		internal void NotificationTraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_0033;
				}
			}
			obj = ptr.ToString();
			IL_0033:
			object obj2 = obj ?? "null";
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj3;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj3 = null;
					goto IL_006D;
				}
			}
			obj3 = ptr2.ToString();
			IL_006D:
			this.NotificationTrace(string.Format(message, obj2, obj3 ?? "null"));
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000142D5 File Offset: 0x000124D5
		[NonEvent]
		internal void TryNotificationTraceEvent(string message)
		{
			if (SqlClientEventSource.Log.IsNotificationTraceEnabled())
			{
				this.NotificationTrace(message);
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000142EC File Offset: 0x000124EC
		[NonEvent]
		internal void TryNotificationTraceEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsNotificationTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				this.NotificationTrace(string.Format(message, obj ?? "null"));
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001434C File Offset: 0x0001254C
		[NonEvent]
		internal void TryNotificationTraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			if (SqlClientEventSource.Log.IsNotificationTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				this.NotificationTrace(string.Format(message, obj2, obj3 ?? "null"));
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000143E8 File Offset: 0x000125E8
		[NonEvent]
		internal void TryNotificationTraceEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			if (SqlClientEventSource.Log.IsNotificationTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				this.NotificationTrace(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000144C0 File Offset: 0x000126C0
		[NonEvent]
		internal void TryNotificationTraceEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			if (SqlClientEventSource.Log.IsNotificationTraceEnabled())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				this.NotificationTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000145E4 File Offset: 0x000127E4
		[NonEvent]
		internal long TryNotificationScopeEnterEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsNotificationScopeEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				return this.NotificationScopeEnter(string.Format(message, obj ?? "null"));
			}
			return 0L;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00014648 File Offset: 0x00012848
		[NonEvent]
		internal long TryNotificationScopeEnterEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			if (SqlClientEventSource.Log.IsNotificationScopeEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				return this.NotificationScopeEnter(string.Format(message, obj2, obj3 ?? "null"));
			}
			return 0L;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000146E8 File Offset: 0x000128E8
		[NonEvent]
		internal long TryNotificationScopeEnterEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			if (SqlClientEventSource.Log.IsNotificationScopeEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				return this.NotificationScopeEnter(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
			return 0L;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000147C4 File Offset: 0x000129C4
		[NonEvent]
		internal long TryNotificationScopeEnterEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			if (SqlClientEventSource.Log.IsNotificationScopeEnabled())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				return this.NotificationScopeEnter(string.Format(message, array));
			}
			return 0L;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000148E9 File Offset: 0x00012AE9
		[NonEvent]
		internal void TryNotificationScopeLeaveEvent(long scopeId)
		{
			if (SqlClientEventSource.Log.IsNotificationScopeEnabled())
			{
				this.NotificationScopeLeave(string.Format("Exit Notification Scope {0}", scopeId));
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00014910 File Offset: 0x00012B10
		[NonEvent]
		internal void TryPoolerTraceEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsPoolerTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				this.PoolerTrace(string.Format(message, obj ?? "null"));
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00014970 File Offset: 0x00012B70
		[NonEvent]
		internal void TryPoolerTraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			if (SqlClientEventSource.Log.IsPoolerTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				this.PoolerTrace(string.Format(message, obj2, obj3 ?? "null"));
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00014A0C File Offset: 0x00012C0C
		[NonEvent]
		internal void TryPoolerTraceEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			if (SqlClientEventSource.Log.IsPoolerTraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				this.PoolerTrace(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00014AE4 File Offset: 0x00012CE4
		[NonEvent]
		internal void TryPoolerTraceEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			if (SqlClientEventSource.Log.IsPoolerTraceEnabled())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				this.PoolerTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00014C08 File Offset: 0x00012E08
		[NonEvent]
		internal long TryPoolerScopeEnterEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsPoolerScopeEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				return this.PoolerScopeEnter(string.Format(message, obj ?? "null"));
			}
			return 0L;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00014C6A File Offset: 0x00012E6A
		[NonEvent]
		internal void TryPoolerScopeLeaveEvent(long scopeId)
		{
			if (SqlClientEventSource.Log.IsPoolerScopeEnabled())
			{
				this.PoolerScopeLeave(string.Format("Exit Pooler Scope {0}", scopeId));
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00014C90 File Offset: 0x00012E90
		[NonEvent]
		internal void AdvancedTraceEvent<T0>(string message, T0 args0)
		{
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_0033;
				}
			}
			obj = ptr.ToString();
			IL_0033:
			this.AdvancedTrace(string.Format(message, obj ?? "null"));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00014CE4 File Offset: 0x00012EE4
		[NonEvent]
		internal void AdvancedTraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_0033;
				}
			}
			obj = ptr.ToString();
			IL_0033:
			object obj2 = obj ?? "null";
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj3;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj3 = null;
					goto IL_006D;
				}
			}
			obj3 = ptr2.ToString();
			IL_006D:
			this.AdvancedTrace(string.Format(message, obj2, obj3 ?? "null"));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00014D74 File Offset: 0x00012F74
		[NonEvent]
		internal void AdvancedTraceEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_0033;
				}
			}
			obj = ptr.ToString();
			IL_0033:
			object obj2 = obj ?? "null";
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj3;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj3 = null;
					goto IL_006D;
				}
			}
			obj3 = ptr2.ToString();
			IL_006D:
			object obj4 = obj3 ?? "null";
			ref T2 ptr3 = ref args2;
			T2 t3 = default(T2);
			object obj5;
			if (t3 == null)
			{
				t3 = args2;
				ptr3 = ref t3;
				if (t3 == null)
				{
					obj5 = null;
					goto IL_00A7;
				}
			}
			obj5 = ptr3.ToString();
			IL_00A7:
			this.AdvancedTrace(string.Format(message, obj2, obj4, obj5 ?? "null"));
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00014E3C File Offset: 0x0001303C
		[NonEvent]
		internal void AdvancedTraceEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			object[] array = new object[4];
			int num = 0;
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_003B;
				}
			}
			obj = ptr.ToString();
			IL_003B:
			array[num] = obj ?? "null";
			int num2 = 1;
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj2;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj2 = null;
					goto IL_0078;
				}
			}
			obj2 = ptr2.ToString();
			IL_0078:
			array[num2] = obj2 ?? "null";
			int num3 = 2;
			ref T2 ptr3 = ref args2;
			T2 t3 = default(T2);
			object obj3;
			if (t3 == null)
			{
				t3 = args2;
				ptr3 = ref t3;
				if (t3 == null)
				{
					obj3 = null;
					goto IL_00B5;
				}
			}
			obj3 = ptr3.ToString();
			IL_00B5:
			array[num3] = obj3 ?? "null";
			int num4 = 3;
			ref T3 ptr4 = ref args3;
			T3 t4 = default(T3);
			object obj4;
			if (t4 == null)
			{
				t4 = args3;
				ptr4 = ref t4;
				if (t4 == null)
				{
					obj4 = null;
					goto IL_00F2;
				}
			}
			obj4 = ptr4.ToString();
			IL_00F2:
			array[num4] = obj4 ?? "null";
			this.AdvancedTrace(string.Format(message, array));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00014F4F File Offset: 0x0001314F
		[NonEvent]
		internal void TryAdvancedTraceEvent(string message)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				this.AdvancedTrace(message);
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00014F64 File Offset: 0x00013164
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				this.AdvancedTrace(string.Format(message, obj ?? "null"));
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00014FC4 File Offset: 0x000131C4
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				this.AdvancedTrace(string.Format(message, obj2, obj3 ?? "null"));
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00015060 File Offset: 0x00013260
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				this.AdvancedTrace(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00015138 File Offset: 0x00013338
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				this.AdvancedTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001525C File Offset: 0x0001345C
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0, T1, T2, T3, T4>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				object[] array = new object[5];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				this.AdvancedTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000153C0 File Offset: 0x000135C0
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0, T1, T2, T3, T4, T5>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4, T5 args5)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				object[] array = new object[6];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				int num6 = 5;
				ref T5 ptr6 = ref args5;
				T5 t6 = default(T5);
				object obj6;
				if (t6 == null)
				{
					t6 = args5;
					ptr6 = ref t6;
					if (t6 == null)
					{
						obj6 = null;
						goto IL_0181;
					}
				}
				obj6 = ptr6.ToString();
				IL_0181:
				array[num6] = obj6 ?? "null";
				this.AdvancedTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00015564 File Offset: 0x00013764
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0, T1, T2, T3, T4, T5, T6, T7>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4, T5 args5, T6 args6, T7 args7)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				object[] array = new object[8];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				int num6 = 5;
				ref T5 ptr6 = ref args5;
				T5 t6 = default(T5);
				object obj6;
				if (t6 == null)
				{
					t6 = args5;
					ptr6 = ref t6;
					if (t6 == null)
					{
						obj6 = null;
						goto IL_0181;
					}
				}
				obj6 = ptr6.ToString();
				IL_0181:
				array[num6] = obj6 ?? "null";
				int num7 = 6;
				ref T6 ptr7 = ref args6;
				T6 t7 = default(T6);
				object obj7;
				if (t7 == null)
				{
					t7 = args6;
					ptr7 = ref t7;
					if (t7 == null)
					{
						obj7 = null;
						goto IL_01C1;
					}
				}
				obj7 = ptr7.ToString();
				IL_01C1:
				array[num7] = obj7 ?? "null";
				int num8 = 7;
				ref T7 ptr8 = ref args7;
				T7 t8 = default(T7);
				object obj8;
				if (t8 == null)
				{
					t8 = args7;
					ptr8 = ref t8;
					if (t8 == null)
					{
						obj8 = null;
						goto IL_0201;
					}
				}
				obj8 = ptr8.ToString();
				IL_0201:
				array[num8] = obj8 ?? "null";
				this.AdvancedTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00015788 File Offset: 0x00013988
		[NonEvent]
		internal void TryAdvancedTraceEvent<T0, T1, T2, T3, T4, T5, T6>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4, T5 args5, T6 args6)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				object[] array = new object[7];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				int num6 = 5;
				ref T5 ptr6 = ref args5;
				T5 t6 = default(T5);
				object obj6;
				if (t6 == null)
				{
					t6 = args5;
					ptr6 = ref t6;
					if (t6 == null)
					{
						obj6 = null;
						goto IL_0181;
					}
				}
				obj6 = ptr6.ToString();
				IL_0181:
				array[num6] = obj6 ?? "null";
				int num7 = 6;
				ref T6 ptr7 = ref args6;
				T6 t7 = default(T6);
				object obj7;
				if (t7 == null)
				{
					t7 = args6;
					ptr7 = ref t7;
					if (t7 == null)
					{
						obj7 = null;
						goto IL_01C1;
					}
				}
				obj7 = ptr7.ToString();
				IL_01C1:
				array[num7] = obj7 ?? "null";
				this.AdvancedTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001596C File Offset: 0x00013B6C
		[NonEvent]
		internal long TryAdvancedScopeEnterEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				return this.AdvancedScopeEnter(string.Format(message, obj ?? "null"));
			}
			return 0L;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000159CE File Offset: 0x00013BCE
		[NonEvent]
		internal void TryAdvanceScopeLeave(long scopeId)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				this.AdvancedScopeLeave(string.Format("Exit Advanced Scope {0}", scopeId));
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000159F4 File Offset: 0x00013BF4
		[NonEvent]
		internal void TryAdvancedTraceBinEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T1 ptr3 = ref args1;
				t2 = default(T1);
				object obj5;
				if (t2 == null)
				{
					t2 = args1;
					ptr3 = ref t2;
					if (t2 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				this.AdvancedTraceBin(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00015ACC File Offset: 0x00013CCC
		[NonEvent]
		internal void TryAdvancedTraceErrorEvent<T0, T1, T2, T3, T4>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				object[] array = new object[5];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				this.AdvancedTraceError(string.Format(message, array));
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00015C30 File Offset: 0x00013E30
		[NonEvent]
		internal void TryCorrelationTraceEvent<T0>(string message, T0 args0)
		{
			if (SqlClientEventSource.Log.IsCorrelationEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_003F;
					}
				}
				obj = ptr.ToString();
				IL_003F:
				this.CorrelationTrace(string.Format(message, obj ?? "null"));
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00015C90 File Offset: 0x00013E90
		[NonEvent]
		internal void TryCorrelationTraceEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			if (SqlClientEventSource.Log.IsCorrelationEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				this.CorrelationTrace(string.Format(message, obj2, obj3 ?? "null"));
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00015D2C File Offset: 0x00013F2C
		[NonEvent]
		internal void TryCorrelationTraceEvent<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
		{
			if (SqlClientEventSource.Log.IsCorrelationEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0042;
					}
				}
				obj = ptr.ToString();
				IL_0042:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_007C;
					}
				}
				obj3 = ptr2.ToString();
				IL_007C:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00B6;
					}
				}
				obj5 = ptr3.ToString();
				IL_00B6:
				this.CorrelationTrace(string.Format(message, obj2, obj4, obj5 ?? "null"));
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00015E04 File Offset: 0x00014004
		[NonEvent]
		internal void TryCorrelationTraceEvent<T0, T1, T2, T3>(string message, T0 args0, T1 args1, T2 args2, T3 args3)
		{
			if (SqlClientEventSource.Log.IsCorrelationEnabled())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				this.CorrelationTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00015F28 File Offset: 0x00014128
		[NonEvent]
		internal void TryCorrelationTraceEvent<T0, T1, T2, T3, T4>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4)
		{
			if (SqlClientEventSource.Log.IsCorrelationEnabled())
			{
				object[] array = new object[5];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				this.CorrelationTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001608C File Offset: 0x0001428C
		[NonEvent]
		internal void TryCorrelationTraceEvent<T0, T1, T2, T3, T4, T5>(string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4, T5 args5)
		{
			if (SqlClientEventSource.Log.IsCorrelationEnabled())
			{
				object[] array = new object[6];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004A;
					}
				}
				obj = ptr.ToString();
				IL_004A:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_0087;
					}
				}
				obj2 = ptr2.ToString();
				IL_0087:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C4;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C4:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0101;
					}
				}
				obj4 = ptr4.ToString();
				IL_0101:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0141;
					}
				}
				obj5 = ptr5.ToString();
				IL_0141:
				array[num5] = obj5 ?? "null";
				int num6 = 5;
				ref T5 ptr6 = ref args5;
				T5 t6 = default(T5);
				object obj6;
				if (t6 == null)
				{
					t6 = args5;
					ptr6 = ref t6;
					if (t6 == null)
					{
						obj6 = null;
						goto IL_0181;
					}
				}
				obj6 = ptr6.ToString();
				IL_0181:
				array[num6] = obj6 ?? "null";
				this.CorrelationTrace(string.Format(message, array));
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00016230 File Offset: 0x00014430
		[NonEvent]
		internal void StateDumpEvent<T0, T1>(string message, T0 args0, T1 args1)
		{
			ref T0 ptr = ref args0;
			T0 t = default(T0);
			object obj;
			if (t == null)
			{
				t = args0;
				ptr = ref t;
				if (t == null)
				{
					obj = null;
					goto IL_0033;
				}
			}
			obj = ptr.ToString();
			IL_0033:
			object obj2 = obj ?? "null";
			ref T1 ptr2 = ref args1;
			T1 t2 = default(T1);
			object obj3;
			if (t2 == null)
			{
				t2 = args1;
				ptr2 = ref t2;
				if (t2 == null)
				{
					obj3 = null;
					goto IL_006D;
				}
			}
			obj3 = ptr2.ToString();
			IL_006D:
			this.StateDump(string.Format(message, obj2, obj3 ?? "null"));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000162BD File Offset: 0x000144BD
		[NonEvent]
		internal void TrySNITraceEvent(string className, string eventType, string message, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNITraceEnabled())
			{
				this.SNITrace(this.GetFormattedMessage(className, memberName, eventType, message));
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000162DC File Offset: 0x000144DC
		[NonEvent]
		internal void TrySNITraceEvent<T0>(string className, string eventType, string message, T0 args0, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNITraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0044;
					}
				}
				obj = ptr.ToString();
				IL_0044:
				this.SNITrace(this.GetFormattedMessage(className, memberName, eventType, string.Format(message, obj ?? "null")));
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00016348 File Offset: 0x00014548
		[NonEvent]
		internal void TrySNITraceEvent<T0, T1>(string className, string eventType, string message, T0 args0, T1 args1, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNITraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0047;
					}
				}
				obj = ptr.ToString();
				IL_0047:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_0081;
					}
				}
				obj3 = ptr2.ToString();
				IL_0081:
				this.SNITrace(this.GetFormattedMessage(className, memberName, eventType, string.Format(message, obj2, obj3 ?? "null")));
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000163F0 File Offset: 0x000145F0
		[NonEvent]
		internal void TrySNITraceEvent<T0, T1, T2>(string className, string eventType, string message, T0 args0, T1 args1, T2 args2, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNITraceEnabled())
			{
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0047;
					}
				}
				obj = ptr.ToString();
				IL_0047:
				object obj2 = obj ?? "null";
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj3;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj3 = null;
						goto IL_0081;
					}
				}
				obj3 = ptr2.ToString();
				IL_0081:
				object obj4 = obj3 ?? "null";
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj5;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj5 = null;
						goto IL_00BB;
					}
				}
				obj5 = ptr3.ToString();
				IL_00BB:
				this.SNITrace(this.GetFormattedMessage(className, memberName, eventType, string.Format(message, obj2, obj4, obj5 ?? "null")));
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000164D0 File Offset: 0x000146D0
		[NonEvent]
		internal void TrySNITraceEvent<T0, T1, T2, T3>(string className, string eventType, string message, T0 args0, T1 args1, T2 args2, T3 args3, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNITraceEnabled())
			{
				object[] array = new object[4];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004F;
					}
				}
				obj = ptr.ToString();
				IL_004F:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_008C;
					}
				}
				obj2 = ptr2.ToString();
				IL_008C:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C9;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C9:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0106;
					}
				}
				obj4 = ptr4.ToString();
				IL_0106:
				array[num4] = obj4 ?? "null";
				this.SNITrace(this.GetFormattedMessage(className, memberName, eventType, string.Format(message, array)));
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000165FC File Offset: 0x000147FC
		[NonEvent]
		internal void TrySNITraceEvent<T0, T1, T2, T3, T4>(string className, string eventType, string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNITraceEnabled())
			{
				object[] array = new object[5];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004F;
					}
				}
				obj = ptr.ToString();
				IL_004F:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_008C;
					}
				}
				obj2 = ptr2.ToString();
				IL_008C:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C9;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C9:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0106;
					}
				}
				obj4 = ptr4.ToString();
				IL_0106:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0146;
					}
				}
				obj5 = ptr5.ToString();
				IL_0146:
				array[num5] = obj5 ?? "null";
				this.SNITrace(this.GetFormattedMessage(className, memberName, eventType, string.Format(message, array)));
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00016768 File Offset: 0x00014968
		[NonEvent]
		internal void TrySNITraceEvent<T0, T1, T2, T3, T4, T5>(string className, string eventType, string message, T0 args0, T1 args1, T2 args2, T3 args3, T4 args4, T5 args5, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNITraceEnabled())
			{
				object[] array = new object[6];
				int num = 0;
				ref T0 ptr = ref args0;
				T0 t = default(T0);
				object obj;
				if (t == null)
				{
					t = args0;
					ptr = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_004F;
					}
				}
				obj = ptr.ToString();
				IL_004F:
				array[num] = obj ?? "null";
				int num2 = 1;
				ref T1 ptr2 = ref args1;
				T1 t2 = default(T1);
				object obj2;
				if (t2 == null)
				{
					t2 = args1;
					ptr2 = ref t2;
					if (t2 == null)
					{
						obj2 = null;
						goto IL_008C;
					}
				}
				obj2 = ptr2.ToString();
				IL_008C:
				array[num2] = obj2 ?? "null";
				int num3 = 2;
				ref T2 ptr3 = ref args2;
				T2 t3 = default(T2);
				object obj3;
				if (t3 == null)
				{
					t3 = args2;
					ptr3 = ref t3;
					if (t3 == null)
					{
						obj3 = null;
						goto IL_00C9;
					}
				}
				obj3 = ptr3.ToString();
				IL_00C9:
				array[num3] = obj3 ?? "null";
				int num4 = 3;
				ref T3 ptr4 = ref args3;
				T3 t4 = default(T3);
				object obj4;
				if (t4 == null)
				{
					t4 = args3;
					ptr4 = ref t4;
					if (t4 == null)
					{
						obj4 = null;
						goto IL_0106;
					}
				}
				obj4 = ptr4.ToString();
				IL_0106:
				array[num4] = obj4 ?? "null";
				int num5 = 4;
				ref T4 ptr5 = ref args4;
				T4 t5 = default(T4);
				object obj5;
				if (t5 == null)
				{
					t5 = args4;
					ptr5 = ref t5;
					if (t5 == null)
					{
						obj5 = null;
						goto IL_0146;
					}
				}
				obj5 = ptr5.ToString();
				IL_0146:
				array[num5] = obj5 ?? "null";
				int num6 = 5;
				ref T5 ptr6 = ref args5;
				T5 t6 = default(T5);
				object obj6;
				if (t6 == null)
				{
					t6 = args5;
					ptr6 = ref t6;
					if (t6 == null)
					{
						obj6 = null;
						goto IL_0186;
					}
				}
				obj6 = ptr6.ToString();
				IL_0186:
				array[num6] = obj6 ?? "null";
				this.SNITrace(this.GetFormattedMessage(className, memberName, eventType, string.Format(message, array)));
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00016914 File Offset: 0x00014B14
		[NonEvent]
		internal long TrySNIScopeEnterEvent(string className, [CallerMemberName] string memberName = "")
		{
			if (SqlClientEventSource.Log.IsSNIScopeEnabled())
			{
				long num = Interlocked.Increment(ref SqlClientEventSource.s_nextSNIScopeId);
				base.WriteEvent(20, string.Format("{0}.{1}  | SNI | INFO | SCOPE | Entering Scope {2}", className, memberName, num));
				return num;
			}
			return 0L;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00016956 File Offset: 0x00014B56
		[NonEvent]
		internal void TrySNIScopeLeaveEvent(long scopeId)
		{
			if (SqlClientEventSource.Log.IsSNIScopeEnabled())
			{
				this.SNIScopeLeave(string.Format("Exit SNI Scope {0}", scopeId));
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001697A File Offset: 0x00014B7A
		[Event(1, Keywords = (EventKeywords)1L, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		internal void BeginExecute(int objectId, string dataSource, string database, string commandText, string message)
		{
			base.WriteEvent(1, new object[] { objectId, dataSource, database, commandText, message });
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000169A4 File Offset: 0x00014BA4
		[Event(2, Keywords = (EventKeywords)1L, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		internal void EndExecute(int objectId, int compositestate, int sqlExceptionNumber, string message)
		{
			base.WriteEvent(2, new object[] { objectId, compositestate, sqlExceptionNumber, message });
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000169D3 File Offset: 0x00014BD3
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)2L)]
		internal void Trace(string message)
		{
			base.WriteEvent(3, message);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000169E0 File Offset: 0x00014BE0
		[Event(4, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Start, Keywords = (EventKeywords)4L)]
		internal long ScopeEnter(string message)
		{
			long num = Interlocked.Increment(ref SqlClientEventSource.s_nextScopeId);
			base.WriteEvent(4, string.Format(message, num));
			return num;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00016A0C File Offset: 0x00014C0C
		[Event(5, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Stop, Keywords = (EventKeywords)4L)]
		internal void ScopeLeave(string message)
		{
			base.WriteEvent(5, message);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00016A16 File Offset: 0x00014C16
		[Event(8, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		internal void NotificationTrace(string message)
		{
			base.WriteEvent(8, message);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00016A20 File Offset: 0x00014C20
		[Event(6, Level = EventLevel.Informational, Task = (EventTask)4, Opcode = EventOpcode.Start, Keywords = (EventKeywords)16L)]
		internal long NotificationScopeEnter(string message)
		{
			long num = Interlocked.Increment(ref SqlClientEventSource.s_nextNotificationScopeId);
			base.WriteEvent(6, string.Format(message, num));
			return num;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00016A4C File Offset: 0x00014C4C
		[Event(7, Level = EventLevel.Informational, Task = (EventTask)4, Opcode = EventOpcode.Stop, Keywords = (EventKeywords)16L)]
		internal void NotificationScopeLeave(string message)
		{
			base.WriteEvent(7, message);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00016A56 File Offset: 0x00014C56
		[Event(11, Level = EventLevel.Informational, Keywords = (EventKeywords)32L)]
		internal void PoolerTrace(string message)
		{
			base.WriteEvent(11, message);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00016A64 File Offset: 0x00014C64
		[Event(9, Level = EventLevel.Informational, Task = (EventTask)3, Opcode = EventOpcode.Start, Keywords = (EventKeywords)64L)]
		internal long PoolerScopeEnter(string message)
		{
			long num = Interlocked.Increment(ref SqlClientEventSource.s_nextPoolerScopeId);
			base.WriteEvent(9, string.Format(message, num));
			return num;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00016A91 File Offset: 0x00014C91
		[Event(10, Level = EventLevel.Informational, Task = (EventTask)3, Opcode = EventOpcode.Stop, Keywords = (EventKeywords)64L)]
		internal void PoolerScopeLeave(string message)
		{
			base.WriteEvent(10, message);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00016A9C File Offset: 0x00014C9C
		[Event(12, Level = EventLevel.Verbose, Keywords = (EventKeywords)128L)]
		internal void AdvancedTrace(string message)
		{
			base.WriteEvent(12, message);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00016AA8 File Offset: 0x00014CA8
		[Event(13, Level = EventLevel.Verbose, Opcode = EventOpcode.Start, Keywords = (EventKeywords)128L)]
		internal long AdvancedScopeEnter(string message)
		{
			long num = Interlocked.Increment(ref SqlClientEventSource.s_nextScopeId);
			base.WriteEvent(13, string.Format(message, num));
			return num;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00016AD5 File Offset: 0x00014CD5
		[Event(14, Level = EventLevel.Verbose, Opcode = EventOpcode.Stop, Keywords = (EventKeywords)128L)]
		internal void AdvancedScopeLeave(string message)
		{
			base.WriteEvent(14, message);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00016AE0 File Offset: 0x00014CE0
		[Event(15, Level = EventLevel.Verbose, Keywords = (EventKeywords)256L)]
		internal void AdvancedTraceBin(string message)
		{
			base.WriteEvent(15, message);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00016AEB File Offset: 0x00014CEB
		[Event(16, Level = EventLevel.Error, Keywords = (EventKeywords)128L)]
		internal void AdvancedTraceError(string message)
		{
			base.WriteEvent(16, message);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00016AF6 File Offset: 0x00014CF6
		[Event(17, Level = EventLevel.Informational, Keywords = (EventKeywords)512L)]
		internal void CorrelationTrace(string message)
		{
			base.WriteEvent(17, message);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00016B01 File Offset: 0x00014D01
		[Event(18, Level = EventLevel.Verbose, Keywords = (EventKeywords)1024L)]
		internal void StateDump(string message)
		{
			base.WriteEvent(18, message);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00016B0C File Offset: 0x00014D0C
		[Event(19, Level = EventLevel.Informational, Keywords = (EventKeywords)2048L)]
		internal void SNITrace(string message)
		{
			base.WriteEvent(19, message);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00016B18 File Offset: 0x00014D18
		[Event(20, Level = EventLevel.Informational, Task = (EventTask)5, Opcode = EventOpcode.Start, Keywords = (EventKeywords)4096L)]
		internal long SNIScopeEnter(string message)
		{
			long num = Interlocked.Increment(ref SqlClientEventSource.s_nextSNIScopeId);
			base.WriteEvent(20, string.Format(message, num));
			return num;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00016B45 File Offset: 0x00014D45
		[Event(21, Level = EventLevel.Informational, Task = (EventTask)5, Opcode = EventOpcode.Stop, Keywords = (EventKeywords)4096L)]
		internal void SNIScopeLeave(string message)
		{
			base.WriteEvent(21, message);
		}

		// Token: 0x04000140 RID: 320
		internal static readonly SqlClientEventSource Log = new SqlClientEventSource();

		// Token: 0x04000141 RID: 321
		private const string NullStr = "null";

		// Token: 0x04000142 RID: 322
		private const string SqlCommand_ClassName = "SqlCommand";

		// Token: 0x04000143 RID: 323
		private static long s_nextScopeId = 0L;

		// Token: 0x04000144 RID: 324
		private static long s_nextNotificationScopeId = 0L;

		// Token: 0x04000145 RID: 325
		private static long s_nextPoolerScopeId = 0L;

		// Token: 0x04000146 RID: 326
		private static long s_nextSNIScopeId = 0L;

		// Token: 0x04000147 RID: 327
		private const int BeginExecuteEventId = 1;

		// Token: 0x04000148 RID: 328
		private const int EndExecuteEventId = 2;

		// Token: 0x04000149 RID: 329
		private const int TraceEventId = 3;

		// Token: 0x0400014A RID: 330
		private const int ScopeEnterId = 4;

		// Token: 0x0400014B RID: 331
		private const int ScopeExitId = 5;

		// Token: 0x0400014C RID: 332
		private const int NotificationScopeEnterId = 6;

		// Token: 0x0400014D RID: 333
		private const int NotificationScopeExitId = 7;

		// Token: 0x0400014E RID: 334
		private const int NotificationTraceId = 8;

		// Token: 0x0400014F RID: 335
		private const int PoolerScopeEnterId = 9;

		// Token: 0x04000150 RID: 336
		private const int PoolerScopeExitId = 10;

		// Token: 0x04000151 RID: 337
		private const int PoolerTraceId = 11;

		// Token: 0x04000152 RID: 338
		private const int AdvancedTraceId = 12;

		// Token: 0x04000153 RID: 339
		private const int AdvancedScopeEnterId = 13;

		// Token: 0x04000154 RID: 340
		private const int AdvancedScopeExitId = 14;

		// Token: 0x04000155 RID: 341
		private const int AdvancedTraceBinId = 15;

		// Token: 0x04000156 RID: 342
		private const int AdvancedTraceErrorId = 16;

		// Token: 0x04000157 RID: 343
		private const int CorrelationTraceId = 17;

		// Token: 0x04000158 RID: 344
		private const int StateDumpEventId = 18;

		// Token: 0x04000159 RID: 345
		private const int SNITraceEventId = 19;

		// Token: 0x0400015A RID: 346
		private const int SNIScopeEnterId = 20;

		// Token: 0x0400015B RID: 347
		private const int SNIScopeExitId = 21;

		// Token: 0x020001BA RID: 442
		public class Keywords
		{
			// Token: 0x04001313 RID: 4883
			internal const EventKeywords ExecutionTrace = (EventKeywords)1L;

			// Token: 0x04001314 RID: 4884
			internal const EventKeywords Trace = (EventKeywords)2L;

			// Token: 0x04001315 RID: 4885
			internal const EventKeywords Scope = (EventKeywords)4L;

			// Token: 0x04001316 RID: 4886
			internal const EventKeywords NotificationTrace = (EventKeywords)8L;

			// Token: 0x04001317 RID: 4887
			internal const EventKeywords NotificationScope = (EventKeywords)16L;

			// Token: 0x04001318 RID: 4888
			internal const EventKeywords PoolerTrace = (EventKeywords)32L;

			// Token: 0x04001319 RID: 4889
			internal const EventKeywords PoolerScope = (EventKeywords)64L;

			// Token: 0x0400131A RID: 4890
			internal const EventKeywords AdvancedTrace = (EventKeywords)128L;

			// Token: 0x0400131B RID: 4891
			internal const EventKeywords AdvancedTraceBin = (EventKeywords)256L;

			// Token: 0x0400131C RID: 4892
			internal const EventKeywords CorrelationTrace = (EventKeywords)512L;

			// Token: 0x0400131D RID: 4893
			internal const EventKeywords StateDump = (EventKeywords)1024L;

			// Token: 0x0400131E RID: 4894
			internal const EventKeywords SNITrace = (EventKeywords)2048L;

			// Token: 0x0400131F RID: 4895
			internal const EventKeywords SNIScope = (EventKeywords)4096L;
		}

		// Token: 0x020001BB RID: 443
		public static class Tasks
		{
			// Token: 0x04001320 RID: 4896
			public const EventTask ExecuteCommand = (EventTask)1;

			// Token: 0x04001321 RID: 4897
			public const EventTask Scope = (EventTask)2;

			// Token: 0x04001322 RID: 4898
			public const EventTask PoolerScope = (EventTask)3;

			// Token: 0x04001323 RID: 4899
			public const EventTask NotificationScope = (EventTask)4;

			// Token: 0x04001324 RID: 4900
			public const EventTask SNIScope = (EventTask)5;
		}
	}
}
