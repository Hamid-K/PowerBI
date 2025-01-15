using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000433 RID: 1075
	internal static class Utility
	{
		// Token: 0x06002570 RID: 9584 RVA: 0x00072A51 File Offset: 0x00070C51
		public static string ConfigValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00072A60 File Offset: 0x00070C60
		public static string ConfigValue(string key, string defaultValue)
		{
			string text = Utility.ConfigValue(key);
			if (text == null)
			{
				text = defaultValue;
			}
			return text;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00072A7A File Offset: 0x00070C7A
		public static bool IsAffirm(string target)
		{
			return string.Compare(target, "yes", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(target, "true", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00072A9B File Offset: 0x00070C9B
		public static int ToInt(string target)
		{
			return Utility.ToInt(target, 0);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00072AA4 File Offset: 0x00070CA4
		public static int ToInt(string target, int defaultReturnValue)
		{
			double num;
			if (!double.TryParse(target, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return defaultReturnValue;
			}
			return (int)num;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x00072AC5 File Offset: 0x00070CC5
		public static T ParseEnum<T>(string val)
		{
			return (T)((object)Enum.Parse(typeof(T), val));
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00072ADC File Offset: 0x00070CDC
		public static object CreateInstanceByReflection(string classSpec)
		{
			if (classSpec == null)
			{
				throw new ArgumentNullException("classSpec");
			}
			string[] array = null;
			if (classSpec.EndsWith(")", StringComparison.Ordinal))
			{
				int num = classSpec.LastIndexOf('(');
				if (num > 0)
				{
					array = classSpec.Substring(num + 1, classSpec.Length - num - 2).Trim().Split(new char[] { ',' });
					classSpec = classSpec.Substring(0, num);
				}
			}
			classSpec = Utility.ConfigValue("customType." + classSpec, classSpec);
			Type type = Type.GetType(classSpec);
			if (type == null)
			{
				return null;
			}
			return Activator.CreateInstance(type, array);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x00072B70 File Offset: 0x00070D70
		public static object ParseValue(Type type, string value)
		{
			object obj;
			if (type == typeof(TimeSpan))
			{
				obj = TimeSpan.FromSeconds(double.Parse(value, CultureInfo.InvariantCulture));
			}
			else if (type == typeof(int))
			{
				obj = int.Parse(value, CultureInfo.InvariantCulture);
			}
			else if (type == typeof(bool))
			{
				obj = Utility.IsAffirm(value);
			}
			else
			{
				if (type != typeof(string))
				{
					throw new ArgumentException("Type can't be parsed: " + type);
				}
				obj = value;
			}
			return obj;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00072C00 File Offset: 0x00070E00
		public static void SetValue(Type type, string name, object obj, string value)
		{
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (obj == null)
			{
				bindingFlags |= BindingFlags.Static;
				obj = type;
			}
			else
			{
				bindingFlags |= BindingFlags.Instance;
			}
			FieldInfo field = type.GetField(name, bindingFlags);
			if (field != null)
			{
				field.SetValue(obj, Utility.ParseValue(field.FieldType, value));
				return;
			}
			PropertyInfo property = type.GetProperty(name, bindingFlags);
			if (property != null)
			{
				property.SetValue(obj, Utility.ParseValue(property.PropertyType, value), null);
				return;
			}
			throw new ArgumentException("Field not found: " + name);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00072C74 File Offset: 0x00070E74
		public static string AddMember(string list, string member, string separator)
		{
			if (separator == null)
			{
				throw new ArgumentNullException("separator");
			}
			if (string.IsNullOrEmpty(list))
			{
				return member;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			string[] array = list.Split(new char[] { separator[0] });
			bool flag = false;
			foreach (string text in array)
			{
				if (!flag && string.Compare(member, text, StringComparison.Ordinal) < 0)
				{
					stringBuilder.Append(member).Append(separator);
					flag = true;
				}
				stringBuilder.Append(text).Append(separator);
			}
			if (!flag)
			{
				stringBuilder.Append(member).Append(separator);
			}
			stringBuilder.Length--;
			return stringBuilder.ToString();
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00072D30 File Offset: 0x00070F30
		public static bool IsMember(string list, string member, char separator)
		{
			if (list == null)
			{
				return false;
			}
			if (member == null)
			{
				return false;
			}
			int num = list.IndexOf(member, StringComparison.Ordinal);
			return num >= 0 && (num <= 0 || list[num - 1] == separator) && (num + member.Length >= list.Length || list[num + member.Length] == separator);
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00072D8C File Offset: 0x00070F8C
		public static bool IsDisjoint(IEnumerable set1, IEnumerable set2)
		{
			if (set1 == null || set2 == null)
			{
				return true;
			}
			foreach (object obj in set1)
			{
				foreach (object obj2 in set2)
				{
					if (object.Equals(obj, obj2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x00072E30 File Offset: 0x00071030
		public static bool CollectionEqual(ICollection c1, ICollection c2)
		{
			if (c1.Count != c2.Count)
			{
				return false;
			}
			IEnumerator enumerator = c1.GetEnumerator();
			IEnumerator enumerator2 = c2.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator2.MoveNext();
				if (!object.Equals(enumerator.Current, enumerator2.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00072E84 File Offset: 0x00071084
		public static Uri GetPartialUri(Uri originalUri, int level)
		{
			if (originalUri == null)
			{
				throw new ArgumentNullException("originalUri");
			}
			ReleaseAssert.IsTrue(level < originalUri.Segments.Length);
			if (level == originalUri.Segments.Length - 1)
			{
				return originalUri;
			}
			string text = originalUri.Scheme + ":";
			for (int i = 0; i <= level; i++)
			{
				text += originalUri.Segments[i];
			}
			return new Uri(text);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00072EF8 File Offset: 0x000710F8
		public static string[] ShiftArgs(string[] args, int shift)
		{
			string[] array = new string[args.Length - shift];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = args[i + shift];
			}
			return array;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00072F27 File Offset: 0x00071127
		public static string GetActionSuffix(string action)
		{
			return action.Substring(action.LastIndexOf('/') + 1);
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00072F3C File Offset: 0x0007113C
		public static double RandomDouble(double average, double range)
		{
			Random random = new Random();
			double num = random.NextDouble() * (range * 2.0) + 1.0 - range;
			return num * average;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00072F74 File Offset: 0x00071174
		public static int[] GenerateShuffledArray(int n)
		{
			int[] array = new int[n];
			for (int i = 0; i < n; i++)
			{
				array[i] = i;
			}
			Random random = new Random();
			for (int j = 0; j < n - 1; j++)
			{
				int num = random.Next(j, n);
				int num2 = array[num];
				array[num] = array[j];
				array[j] = num2;
			}
			return array;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00072FCC File Offset: 0x000711CC
		public static void GenerateShuffledArray<T>(IList<T> list)
		{
			Random random = new Random();
			for (int i = 0; i < list.Count; i++)
			{
				int num = random.Next(i, list.Count);
				T t = list[num];
				list[num] = list[i];
				list[i] = t;
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0007301C File Offset: 0x0007121C
		public static string FormatTime(DateTime t)
		{
			return t.ToString("yyyy-M-d HH:mm:ss.fff", CultureInfo.InvariantCulture);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0007302F File Offset: 0x0007122F
		public static bool IsCommunicationException(Exception e)
		{
			while (e is OperationCompletedException)
			{
				e = e.InnerException;
			}
			return e is CommunicationException || e is SocketException || e is TimeoutException || e is ObjectDisposedException || e is OperationContextAbortedException;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0007306E File Offset: 0x0007126E
		public static bool IsException<T>(Exception e) where T : Exception
		{
			while (e is OperationCompletedException)
			{
				e = e.InnerException;
			}
			return e is T;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x0007308C File Offset: 0x0007128C
		public static void CloseChannelAsync(ICommunicationObject channel)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			if (channel.State == CommunicationState.Closed)
			{
				return;
			}
			if (channel.State == CommunicationState.Faulted)
			{
				channel.Abort();
				return;
			}
			try
			{
				channel.BeginClose(Utility.CloseChannelCallback, channel);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000730F0 File Offset: 0x000712F0
		private static void StaticChannelClosedCallback(IAsyncResult ar)
		{
			IChannel channel = (IChannel)ar.AsyncState;
			try
			{
				channel.EndClose(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00073130 File Offset: 0x00071330
		public static void AddEventHandler(ref EventHandler location, EventHandler value)
		{
			if (value == null)
			{
				return;
			}
			EventHandler eventHandler = location;
			EventHandler eventHandler3;
			do
			{
				EventHandler eventHandler2 = (EventHandler)Delegate.Combine(eventHandler, value);
				eventHandler3 = eventHandler;
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref location, eventHandler2, eventHandler3);
			}
			while (!object.ReferenceEquals(eventHandler, eventHandler3));
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00073168 File Offset: 0x00071368
		public static void RemoveEventHandler(ref EventHandler location, EventHandler value)
		{
			EventHandler eventHandler = location;
			for (;;)
			{
				EventHandler eventHandler2 = (EventHandler)Delegate.Remove(eventHandler, value);
				if (object.ReferenceEquals(eventHandler2, eventHandler))
				{
					break;
				}
				EventHandler eventHandler3 = eventHandler;
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref location, eventHandler2, eventHandler3);
				if (object.ReferenceEquals(eventHandler, eventHandler3))
				{
					return;
				}
			}
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000731A4 File Offset: 0x000713A4
		public static void ReadThroughParentEndElement(XmlReader reader)
		{
			if (reader.NodeType != XmlNodeType.EndElement)
			{
				int num = reader.Depth - 1;
				do
				{
					reader.Read();
				}
				while (reader.NodeType != XmlNodeType.EndElement || reader.Depth != num);
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000731E4 File Offset: 0x000713E4
		public static IAsyncResult BeginOpenTcpChannel(IChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new Utility.OpenContext(channel, callback, timeout, state);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000731F0 File Offset: 0x000713F0
		public static void EndOpenTcpChannel(IAsyncResult ar)
		{
			Utility.OpenContext openContext = (Utility.OpenContext)ar;
			openContext.End();
		}

		// Token: 0x040016B1 RID: 5809
		private static AsyncCallback CloseChannelCallback = new AsyncCallback(Utility.StaticChannelClosedCallback);

		// Token: 0x02000434 RID: 1076
		internal class OpenContext : OperationContext
		{
			// Token: 0x0600258E RID: 9614 RVA: 0x0007321D File Offset: 0x0007141D
			public OpenContext(ICommunicationObject obj, AsyncCallback callback, TimeSpan timeout, object state)
				: base(callback, state, timeout)
			{
				this.m_obj = obj;
				this.m_obj.BeginOpen(Utility.OpenContext.StaticOpenCallback, this);
				base.StartTimer();
			}

			// Token: 0x0600258F RID: 9615 RVA: 0x00073248 File Offset: 0x00071448
			private static void OpenCallback(IAsyncResult ar)
			{
				Utility.OpenContext openContext = (Utility.OpenContext)ar.AsyncState;
				Exception ex = null;
				try
				{
					openContext.m_obj.EndOpen(ar);
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				openContext.CompleteOperation(ar.CompletedSynchronously, ex);
			}

			// Token: 0x06002590 RID: 9616 RVA: 0x00073294 File Offset: 0x00071494
			protected override void OnTimerExpired()
			{
				this.m_obj.Abort();
				base.OnTimerExpired();
			}

			// Token: 0x040016B2 RID: 5810
			private ICommunicationObject m_obj;

			// Token: 0x040016B3 RID: 5811
			private static AsyncCallback StaticOpenCallback = new AsyncCallback(Utility.OpenContext.OpenCallback);
		}
	}
}
