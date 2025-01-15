using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Diagnostics.Contracts.Internal;
using Microsoft.Diagnostics.Tracing.Internal;
using Microsoft.Reflection;
using Microsoft.Win32;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000014 RID: 20
	public class EventSource : IDisposable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003841 File Offset: 0x00001A41
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003849 File Offset: 0x00001A49
		public Guid Guid
		{
			get
			{
				return this.m_guid;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003851 File Offset: 0x00001A51
		public bool IsEnabled()
		{
			return this.m_eventSourceEnabled;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003859 File Offset: 0x00001A59
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return this.IsEnabled(level, keywords, EventChannel.None);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003864 File Offset: 0x00001A64
		public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
		{
			return this.m_eventSourceEnabled && this.IsEnabledCommon(this.m_eventSourceEnabled, this.m_level, this.m_matchAnyKeyword, level, keywords, channel);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003890 File Offset: 0x00001A90
		public EventSourceSettings Settings
		{
			get
			{
				return this.m_config;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003898 File Offset: 0x00001A98
		public static Guid GetGuid(Type eventSourceType)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			Contract.EndContractBlock();
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), EventManifestOptions.None);
			string text = eventSourceType.Name;
			if (eventSourceAttribute != null)
			{
				if (eventSourceAttribute.Guid != null)
				{
					Guid empty = Guid.Empty;
					try
					{
						return new Guid(eventSourceAttribute.Guid);
					}
					catch (Exception)
					{
					}
				}
				if (eventSourceAttribute.Name != null)
				{
					text = eventSourceAttribute.Name;
				}
			}
			if (text == null)
			{
				throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("Argument_InvalidTypeName", Array.Empty<object>()), "eventSourceType");
			}
			return EventSource.GenerateGuidFromName(text.ToUpperInvariant());
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003948 File Offset: 0x00001B48
		public static string GetName(Type eventSourceType)
		{
			return EventSource.GetName(eventSourceType, EventManifestOptions.None);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003951 File Offset: 0x00001B51
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
		{
			return EventSource.GenerateManifest(eventSourceType, assemblyPathToIncludeInManifest, EventManifestOptions.None);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000395C File Offset: 0x00001B5C
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			Contract.EndContractBlock();
			byte[] array = EventSource.CreateManifestAndDescriptors(eventSourceType, assemblyPathToIncludeInManifest, null, flags);
			if (array != null)
			{
				return Encoding.UTF8.GetString(array, 0, array.Length);
			}
			return null;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000039A0 File Offset: 0x00001BA0
		public static IEnumerable<EventSource> GetSources()
		{
			List<EventSource> list = new List<EventSource>();
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null && !eventSource.IsDisposed)
					{
						list.Add(eventSource);
					}
				}
			}
			return list;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A38 File Offset: 0x00001C38
		public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (command <= EventCommand.Update && command != EventCommand.SendManifest)
			{
				throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_InvalidCommand", Array.Empty<object>()), "command");
			}
			eventSource.SendCommand(null, 0, 0, command, true, EventLevel.LogAlways, EventKeywords.None, commandArguments);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A84 File Offset: 0x00001C84
		[SecuritySafeCritical]
		public static void SetCurrentThreadActivityId(Guid activityId)
		{
			UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref activityId);
			if (TplEtwProvider.Log != null)
			{
				TplEtwProvider.Log.SetActivityId(activityId);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003AA1 File Offset: 0x00001CA1
		[SecuritySafeCritical]
		public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			oldActivityThatWillContinue = activityId;
			UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref oldActivityThatWillContinue);
			if (TplEtwProvider.Log != null)
			{
				TplEtwProvider.Log.SetActivityId(activityId);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public static Guid CurrentThreadActivityId
		{
			[SecuritySafeCritical]
			get
			{
				Guid guid = default(Guid);
				UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_ID, ref guid);
				return guid;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public Exception ConstructionException
		{
			get
			{
				return this.m_constructionException;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003AEC File Offset: 0x00001CEC
		public string GetTrait(string key)
		{
			if (this.m_traits != null)
			{
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i] == key)
					{
						return this.m_traits[i + 1];
					}
				}
			}
			return null;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B32 File Offset: 0x00001D32
		public override string ToString()
		{
			return Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_ToString", new object[] { this.Name, this.Guid });
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003B5B File Offset: 0x00001D5B
		protected EventSource()
			: this(EventSourceSettings.EtwManifestEventFormat)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003B64 File Offset: 0x00001D64
		protected EventSource(bool throwOnEventWriteErrors)
			: this(EventSourceSettings.EtwManifestEventFormat | (throwOnEventWriteErrors ? EventSourceSettings.ThrowOnEventWriteErrors : EventSourceSettings.Default))
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003B75 File Offset: 0x00001D75
		protected EventSource(EventSourceSettings settings)
			: this(settings, null)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003B80 File Offset: 0x00001D80
		protected EventSource(EventSourceSettings settings, params string[] traits)
		{
			this.m_config = this.ValidateSettings(settings);
			Type type = base.GetType();
			this.Initialize(EventSource.GetGuid(type), EventSource.GetName(type), traits);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003BBA File Offset: 0x00001DBA
		protected virtual void OnEventCommand(EventCommandEventArgs command)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003BBC File Offset: 0x00001DBC
		[SecuritySafeCritical]
		protected void WriteEvent(int eventId)
		{
			this.WriteEventCore(eventId, 0, null);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003BC8 File Offset: 0x00001DC8
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003C08 File Offset: 0x00001E08
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003C6C File Offset: 0x00001E6C
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003CF8 File Offset: 0x00001EF8
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003D38 File Offset: 0x00001F38
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003D9C File Offset: 0x00001F9C
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2, long arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 8;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003E28 File Offset: 0x00002028
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					this.WriteEventCore(eventId, 1, ptr2);
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003E8C File Offset: 0x0000208C
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
							ptr3->DataPointer = (IntPtr)((void*)ptr);
						}
						ptr3->Size = (arg1.Length + 1) * 2;
						ptr3[1].DataPointer = (IntPtr)((void*)ptr2);
						ptr3[1].Size = (arg2.Length + 1) * 2;
						this.WriteEventCore(eventId, 2, ptr3);
					}
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003F40 File Offset: 0x00002140
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							EventSource.EventData* ptr4;
							checked
							{
								ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
								ptr4->DataPointer = (IntPtr)((void*)ptr);
							}
							ptr4->Size = (arg1.Length + 1) * 2;
							ptr4[1].DataPointer = (IntPtr)((void*)ptr2);
							ptr4[1].Size = (arg2.Length + 1) * 2;
							ptr4[2].DataPointer = (IntPtr)((void*)ptr3);
							ptr4[2].Size = (arg3.Length + 1) * 2;
							this.WriteEventCore(eventId, 3, ptr4);
						}
					}
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000404C File Offset: 0x0000224C
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000040D4 File Offset: 0x000022D4
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)(&arg3));
					ptr2[2].Size = 4;
					this.WriteEventCore(eventId, 3, ptr2);
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004188 File Offset: 0x00002388
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, long arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 8;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004210 File Offset: 0x00002410
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004298 File Offset: 0x00002498
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 4;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004320 File Offset: 0x00002520
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, byte[] arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = new byte[0];
				}
				int num = arg1.Length;
				fixed (byte* ptr = &arg1[0])
				{
					byte* ptr2 = ptr;
					EventSource.EventData* ptr3;
					checked
					{
						ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr3->DataPointer = (IntPtr)((void*)(&num));
					ptr3->Size = 4;
					ptr3[1].DataPointer = (IntPtr)((void*)ptr2);
					ptr3[1].Size = num;
					this.WriteEventCore(eventId, 2, ptr3);
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000043A0 File Offset: 0x000025A0
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, byte[] arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = new byte[0];
				}
				int num = arg2.Length;
				fixed (byte* ptr = &arg2[0])
				{
					byte* ptr2 = ptr;
					EventSource.EventData* ptr3;
					checked
					{
						ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr3->DataPointer = (IntPtr)((void*)(&arg1));
					ptr3->Size = 8;
					ptr3[1].DataPointer = (IntPtr)((void*)(&num));
					ptr3[1].Size = 4;
					ptr3[2].DataPointer = (IntPtr)((void*)ptr2);
					ptr3[2].Size = num;
					this.WriteEventCore(eventId, 3, ptr3);
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004449 File Offset: 0x00002649
		[SecurityCritical]
		[CLSCompliant(false)]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
			this.WriteEventWithRelatedActivityIdCore(eventId, null, eventDataCount, data);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004458 File Offset: 0x00002658
		[SecurityCritical]
		[CLSCompliant(false)]
		protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
		{
			if (this.m_eventSourceEnabled)
			{
				try
				{
					Contract.Assert(this.m_eventData != null);
					if (relatedActivityId != null)
					{
						this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId]);
					}
					if (this.m_eventData[eventId].EnabledForETW)
					{
						EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
						EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
						Guid* ptr = null;
						Guid empty = Guid.Empty;
						Guid empty2 = Guid.Empty;
						if (opcode != EventOpcode.Info && relatedActivityId == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
						{
							if (opcode == EventOpcode.Start)
							{
								this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions);
							}
							else if (opcode == EventOpcode.Stop)
							{
								this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty);
							}
							if (empty != Guid.Empty)
							{
								ptr = &empty;
							}
							if (empty2 != Guid.Empty)
							{
								relatedActivityId = &empty2;
							}
						}
						if (!this.SelfDescribingEvents)
						{
							if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, ptr, relatedActivityId, eventDataCount, (IntPtr)((void*)data)))
							{
								this.ThrowEventSourceException(null);
							}
						}
						else
						{
							TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
							if (traceLoggingEventTypes == null)
							{
								traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, this.m_eventData[eventId].Tags, this.m_eventData[eventId].Parameters);
								Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
							}
							EventSourceOptions eventSourceOptions = new EventSourceOptions
							{
								Keywords = (EventKeywords)this.m_eventData[eventId].Descriptor.Keywords,
								Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
								Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
							};
							this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, ptr, relatedActivityId, data);
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						this.WriteToAllListeners(eventId, relatedActivityId, eventDataCount, data);
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(ex);
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004764 File Offset: 0x00002964
		[SecuritySafeCritical]
		protected void WriteEvent(int eventId, params object[] args)
		{
			this.WriteEventVarargs(eventId, null, args);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004770 File Offset: 0x00002970
		[SecuritySafeCritical]
		protected unsafe void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
		{
			this.WriteEventVarargs(eventId, &relatedActivityId, args);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000477D File Offset: 0x0000297D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000478C File Offset: 0x0000298C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_eventSourceEnabled)
				{
					try
					{
						this.SendManifest(this.m_rawManifest);
					}
					catch (Exception)
					{
					}
					this.m_eventSourceEnabled = false;
				}
				if (this.m_provider != null)
				{
					this.m_provider.Dispose();
					this.m_provider = null;
				}
			}
			this.m_eventSourceEnabled = false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000047F8 File Offset: 0x000029F8
		~EventSource()
		{
			this.Dispose(false);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004828 File Offset: 0x00002A28
		[SecurityCritical]
		private unsafe void WriteEventRaw(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			if (this.m_provider == null)
			{
				this.ThrowEventSourceException(null);
				return;
			}
			if (!this.m_provider.WriteEventRaw(ref eventDescriptor, activityID, relatedActivityID, dataCount, data))
			{
				this.ThrowEventSourceException(null);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004859 File Offset: 0x00002A59
		internal EventSource(Guid eventSourceGuid, string eventSourceName)
			: this(eventSourceGuid, eventSourceName, EventSourceSettings.EtwManifestEventFormat, null)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004865 File Offset: 0x00002A65
		internal EventSource(Guid eventSourceGuid, string eventSourceName, EventSourceSettings settings, string[] traits = null)
		{
			this.m_config = this.ValidateSettings(settings);
			this.Initialize(eventSourceGuid, eventSourceName, traits);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004884 File Offset: 0x00002A84
		[SecuritySafeCritical]
		private unsafe void Initialize(Guid eventSourceGuid, string eventSourceName, string[] traits)
		{
			try
			{
				this.m_traits = traits;
				if (this.m_traits != null && this.m_traits.Length % 2 != 0)
				{
					throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("TraitEven", Array.Empty<object>()), "traits");
				}
				if (eventSourceGuid == Guid.Empty)
				{
					throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NeedGuid", Array.Empty<object>()));
				}
				if (eventSourceName == null)
				{
					throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NeedName", Array.Empty<object>()));
				}
				this.m_name = eventSourceName;
				this.m_guid = eventSourceGuid;
				this.m_activityTracker = ActivityTracker.Instance;
				this.InitializeProviderMetadata();
				EventSource.OverideEventProvider overideEventProvider = new EventSource.OverideEventProvider(this);
				overideEventProvider.Register(eventSourceGuid);
				EventListener.AddEventSource(this);
				this.m_provider = overideEventProvider;
				try
				{
					byte[] array;
					void* ptr;
					if ((array = this.providerMetadata) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = (void*)(&array[0]);
					}
					this.m_provider.SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS.SetTraits, ptr, this.providerMetadata.Length);
				}
				finally
				{
					byte[] array = null;
				}
				Contract.Assert(!this.m_eventSourceEnabled);
				this.m_completelyInited = true;
			}
			catch (Exception ex)
			{
				if (this.m_constructionException == null)
				{
					this.m_constructionException = ex;
				}
				this.ReportOutOfBandMessage("ERROR: Exception during construction of EventSource " + this.Name + ": " + ex.Message, true);
			}
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				while (this.m_deferredCommands != null)
				{
					this.DoCommand(this.m_deferredCommands);
					this.m_deferredCommands = this.m_deferredCommands.nextCommand;
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004A34 File Offset: 0x00002C34
		private static string GetName(Type eventSourceType, EventManifestOptions flags)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			Contract.EndContractBlock();
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
			if (eventSourceAttribute != null && eventSourceAttribute.Name != null)
			{
				return eventSourceAttribute.Name;
			}
			return eventSourceType.Name;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A8C File Offset: 0x00002C8C
		private static Guid GenerateGuidFromName(string name)
		{
			byte[] bytes = Encoding.BigEndianUnicode.GetBytes(name);
			EventSource.Sha1ForNonSecretPurposes sha1ForNonSecretPurposes = default(EventSource.Sha1ForNonSecretPurposes);
			sha1ForNonSecretPurposes.Start();
			sha1ForNonSecretPurposes.Append(EventSource.namespaceBytes);
			sha1ForNonSecretPurposes.Append(bytes);
			Array.Resize<byte>(ref bytes, 16);
			sha1ForNonSecretPurposes.Finish(bytes);
			bytes[7] = (bytes[7] & 15) | 80;
			return new Guid(bytes);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004AEC File Offset: 0x00002CEC
		[SecurityCritical]
		private unsafe object DecodeObject(int eventId, int parameterId, ref EventSource.EventData* data)
		{
			IntPtr intPtr = data.DataPointer;
			data += (IntPtr)sizeof(EventSource.EventData);
			Type type = this.m_eventData[eventId].Parameters[parameterId].ParameterType;
			while (!(type == typeof(IntPtr)))
			{
				if (type == typeof(int))
				{
					return *(int*)(void*)intPtr;
				}
				if (type == typeof(uint))
				{
					return *(uint*)(void*)intPtr;
				}
				if (type == typeof(long))
				{
					return *(long*)(void*)intPtr;
				}
				if (type == typeof(ulong))
				{
					return (ulong)(*(long*)(void*)intPtr);
				}
				if (type == typeof(byte))
				{
					return *(byte*)(void*)intPtr;
				}
				if (type == typeof(sbyte))
				{
					return *(sbyte*)(void*)intPtr;
				}
				if (type == typeof(short))
				{
					return *(short*)(void*)intPtr;
				}
				if (type == typeof(ushort))
				{
					return *(ushort*)(void*)intPtr;
				}
				if (type == typeof(float))
				{
					return *(float*)(void*)intPtr;
				}
				if (type == typeof(double))
				{
					return *(double*)(void*)intPtr;
				}
				if (type == typeof(decimal))
				{
					return *(decimal*)(void*)intPtr;
				}
				if (type == typeof(bool))
				{
					if (*(int*)(void*)intPtr == 1)
					{
						return true;
					}
					return false;
				}
				else
				{
					if (type == typeof(Guid))
					{
						return *(Guid*)(void*)intPtr;
					}
					if (type == typeof(char))
					{
						return (char)(*(ushort*)(void*)intPtr);
					}
					if (type == typeof(DateTime))
					{
						return DateTime.FromFileTimeUtc(*(long*)(void*)intPtr);
					}
					if (type == typeof(byte[]))
					{
						int num = *(int*)(void*)intPtr;
						byte[] array = new byte[num];
						intPtr = data.DataPointer;
						data += (IntPtr)sizeof(EventSource.EventData);
						for (int i = 0; i < num; i++)
						{
							array[i] = *(byte*)(void*)intPtr;
						}
						return array;
					}
					if (type == typeof(byte*))
					{
						return null;
					}
					if (!type.IsEnum())
					{
						return Marshal.PtrToStringUni(intPtr);
					}
					type = Enum.GetUnderlyingType(type);
				}
			}
			return *(IntPtr*)(void*)intPtr;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004DB0 File Offset: 0x00002FB0
		private EventDispatcher GetDispatcher(EventListener listener)
		{
			EventDispatcher eventDispatcher;
			for (eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				if (eventDispatcher.m_Listener == listener)
				{
					return eventDispatcher;
				}
			}
			return eventDispatcher;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004DE0 File Offset: 0x00002FE0
		[SecurityCritical]
		private unsafe void WriteEventVarargs(int eventId, Guid* childActivityID, object[] args)
		{
			if (this.m_eventSourceEnabled)
			{
				try
				{
					Contract.Assert(this.m_eventData != null);
					if (childActivityID != null)
					{
						this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId]);
					}
					if (this.m_eventData[eventId].EnabledForETW)
					{
						Guid* ptr = null;
						Guid empty = Guid.Empty;
						Guid empty2 = Guid.Empty;
						EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
						EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
						if (childActivityID == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
						{
							if (opcode == EventOpcode.Start)
							{
								this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions);
							}
							else if (opcode == EventOpcode.Stop)
							{
								this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty);
							}
							if (empty != Guid.Empty)
							{
								ptr = &empty;
							}
							if (empty2 != Guid.Empty)
							{
								childActivityID = &empty2;
							}
						}
						if (!this.SelfDescribingEvents)
						{
							if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, ptr, childActivityID, args))
							{
								this.ThrowEventSourceException(null);
							}
						}
						else
						{
							TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
							if (traceLoggingEventTypes == null)
							{
								traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
								Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
							}
							EventSourceOptions eventSourceOptions = new EventSourceOptions
							{
								Keywords = (EventKeywords)this.m_eventData[eventId].Descriptor.Keywords,
								Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
								Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
							};
							this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, ptr, childActivityID, args);
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						object[] array = this.SerializeEventArgs(eventId, args);
						this.WriteToAllListeners(eventId, childActivityID, array);
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(ex);
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000050D8 File Offset: 0x000032D8
		[SecurityCritical]
		private object[] SerializeEventArgs(int eventId, object[] args)
		{
			TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
			if (traceLoggingEventTypes == null)
			{
				traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
				Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
			}
			object[] array = new object[traceLoggingEventTypes.typeInfos.Length];
			for (int i = 0; i < traceLoggingEventTypes.typeInfos.Length; i++)
			{
				array[i] = traceLoggingEventTypes.typeInfos[i].GetData(args[i]);
			}
			return array;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000517C File Offset: 0x0000337C
		[SecurityCritical]
		private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, int eventDataCount, EventSource.EventData* data)
		{
			int num = this.m_eventData[eventId].Parameters.Length;
			if (eventDataCount != num)
			{
				this.ReportOutOfBandMessage(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventParametersMismatch", new object[] { eventId, eventDataCount, num }), true);
				num = Math.Min(num, eventDataCount);
			}
			object[] array = new object[num];
			EventSource.EventData* ptr = data;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.DecodeObject(eventId, i, ref ptr);
			}
			this.WriteToAllListeners(eventId, childActivityID, array);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000520C File Offset: 0x0000340C
		[SecurityCritical]
		private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, params object[] args)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = eventId;
			if (childActivityID != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *childActivityID;
			}
			eventWrittenEventArgs.EventName = this.m_eventData[eventId].Name;
			eventWrittenEventArgs.Message = this.m_eventData[eventId].Message;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(args);
			this.DisptachToAllListeners(eventId, childActivityID, eventWrittenEventArgs);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005280 File Offset: 0x00003480
		[SecurityCritical]
		private unsafe void DisptachToAllListeners(int eventId, Guid* childActivityID, EventWrittenEventArgs eventCallbackArgs)
		{
			Exception ex = null;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				Contract.Assert(eventDispatcher.m_EventEnabled != null);
				if (eventId == -1 || eventDispatcher.m_EventEnabled[eventId])
				{
					try
					{
						eventDispatcher.m_Listener.OnEventWritten(eventCallbackArgs);
					}
					catch (Exception ex2)
					{
						this.ReportOutOfBandMessage("ERROR: Exception during EventSource.OnEventWritten: " + ex2.Message, false);
						ex = ex2;
					}
				}
			}
			if (ex != null)
			{
				throw new EventSourceException(ex);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005308 File Offset: 0x00003508
		[SecuritySafeCritical]
		private unsafe void WriteEventString(EventLevel level, long keywords, string msgString)
		{
			if (this.m_provider != null)
			{
				string text = "EventSourceMessage";
				if (this.SelfDescribingEvents)
				{
					EventSourceOptions eventSourceOptions = new EventSourceOptions
					{
						Keywords = (EventKeywords)keywords,
						Level = level
					};
					var <>f__AnonymousType = new
					{
						message = msgString
					};
					TraceLoggingEventTypes traceLoggingEventTypes = new TraceLoggingEventTypes(text, EventTags.None, new Type[] { <>f__AnonymousType.GetType() });
					this.WriteMultiMergeInner(text, ref eventSourceOptions, traceLoggingEventTypes, null, null, new object[] { <>f__AnonymousType });
					return;
				}
				if (this.m_rawManifest == null && this.m_outOfBandMessageCount == 1)
				{
					ManifestBuilder manifestBuilder = new ManifestBuilder(this.Name, this.Guid, this.Name, null, EventManifestOptions.None);
					manifestBuilder.StartEvent(text, new EventAttribute(0)
					{
						Level = EventLevel.LogAlways,
						Task = (EventTask)65534
					});
					manifestBuilder.AddEventParameter(typeof(string), "message");
					manifestBuilder.EndEvent();
					this.SendManifest(manifestBuilder.CreateManifest());
				}
				fixed (string text2 = msgString)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventDescriptor eventDescriptor = new EventDescriptor(0, 0, 0, (byte)level, 0, 0, keywords);
					EventProvider.EventData eventData = default(EventProvider.EventData);
					eventData.Ptr = ptr;
					eventData.Size = (uint)(2 * (msgString.Length + 1));
					eventData.Reserved = 0U;
					this.m_provider.WriteEvent(ref eventDescriptor, null, null, 1, (IntPtr)((void*)(&eventData)));
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000546C File Offset: 0x0000366C
		private void WriteStringToAllListeners(string eventName, string msg)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = 0;
			eventWrittenEventArgs.Message = msg;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(new List<object> { msg });
			eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>(new List<string> { "message" });
			eventWrittenEventArgs.EventName = eventName;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				bool flag = false;
				if (eventDispatcher.m_EventEnabled == null)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < eventDispatcher.m_EventEnabled.Length; i++)
					{
						if (eventDispatcher.m_EventEnabled[i])
						{
							flag = true;
							break;
						}
					}
				}
				try
				{
					if (flag)
					{
						eventDispatcher.m_Listener.OnEventWritten(eventWrittenEventArgs);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005530 File Offset: 0x00003730
		private bool IsEnabledByDefault(int eventNum, bool enable, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword)
		{
			if (!enable)
			{
				return false;
			}
			EventLevel level = (EventLevel)this.m_eventData[eventNum].Descriptor.Level;
			EventKeywords eventKeywords = (EventKeywords)(this.m_eventData[eventNum].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords()));
			EventChannel channel = (EventChannel)this.m_eventData[eventNum].Descriptor.Channel;
			return this.IsEnabledCommon(enable, currentLevel, currentMatchAnyKeyword, level, eventKeywords, channel);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000055AC File Offset: 0x000037AC
		private bool IsEnabledCommon(bool enabled, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword, EventLevel eventLevel, EventKeywords eventKeywords, EventChannel eventChannel)
		{
			if (!enabled)
			{
				return false;
			}
			if (currentLevel != EventLevel.LogAlways && currentLevel < eventLevel)
			{
				return false;
			}
			if (currentMatchAnyKeyword != EventKeywords.None && eventKeywords != EventKeywords.None)
			{
				if (eventChannel != EventChannel.None && this.m_channelData != null && this.m_channelData.Length > (int)eventChannel)
				{
					EventKeywords eventKeywords2 = (EventKeywords)(this.m_channelData[(int)eventChannel] | (ulong)eventKeywords);
					if (eventKeywords2 != EventKeywords.None && (eventKeywords2 & currentMatchAnyKeyword) == EventKeywords.None)
					{
						return false;
					}
				}
				else if ((eventKeywords & currentMatchAnyKeyword) == EventKeywords.None)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005610 File Offset: 0x00003810
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ThrowEventSourceException(Exception innerEx = null)
		{
			if (EventSource.m_EventSourceExceptionRecurenceCount > 0)
			{
				return;
			}
			try
			{
				EventSource.m_EventSourceExceptionRecurenceCount += 1;
				switch (EventProvider.GetLastWriteEventError())
				{
				case EventProvider.WriteEventErrorCode.NoFreeBuffers:
					this.ReportOutOfBandMessage("EventSourceException: " + Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NoFreeBuffers", Array.Empty<object>()), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NoFreeBuffers", Array.Empty<object>()), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.EventTooBig:
					this.ReportOutOfBandMessage("EventSourceException: " + Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventTooBig", Array.Empty<object>()), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventTooBig", Array.Empty<object>()), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.NullInput:
					this.ReportOutOfBandMessage("EventSourceException: " + Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NullInput", Array.Empty<object>()), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NullInput", Array.Empty<object>()), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.TooManyArgs:
					this.ReportOutOfBandMessage("EventSourceException: " + Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_TooManyArgs", Array.Empty<object>()), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_TooManyArgs", Array.Empty<object>()), innerEx);
					}
					break;
				default:
					if (innerEx != null)
					{
						string text = "EventSourceException: ";
						Type type = innerEx.GetType();
						this.ReportOutOfBandMessage(text + ((type != null) ? type.ToString() : null) + ":" + innerEx.Message, true);
					}
					else
					{
						this.ReportOutOfBandMessage("EventSourceException", true);
					}
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(innerEx);
					}
					break;
				}
			}
			finally
			{
				EventSource.m_EventSourceExceptionRecurenceCount -= 1;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000057D4 File Offset: 0x000039D4
		private void ValidateEventOpcodeForTransfer(ref EventSource.EventMetadata eventData)
		{
			if (eventData.Descriptor.Opcode != 9 && eventData.Descriptor.Opcode != 240)
			{
				this.ThrowEventSourceException(null);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000057FE File Offset: 0x000039FE
		internal static EventOpcode GetOpcodeWithDefault(EventOpcode opcode, string eventName)
		{
			if (opcode == EventOpcode.Info && eventName != null)
			{
				if (eventName.EndsWith("Start"))
				{
					return EventOpcode.Start;
				}
				if (eventName.EndsWith("Stop"))
				{
					return EventOpcode.Stop;
				}
			}
			return opcode;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005828 File Offset: 0x00003A28
		internal void SendCommand(EventListener listener, int perEventSourceSessionId, int etwSessionId, EventCommand command, bool enable, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> commandArguments)
		{
			EventCommandEventArgs eventCommandEventArgs = new EventCommandEventArgs(command, commandArguments, this, listener, perEventSourceSessionId, etwSessionId, enable, level, matchAnyKeyword);
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (this.m_completelyInited)
				{
					this.DoCommand(eventCommandEventArgs);
				}
				else
				{
					eventCommandEventArgs.nextCommand = this.m_deferredCommands;
					this.m_deferredCommands = eventCommandEventArgs;
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000589C File Offset: 0x00003A9C
		internal void DoCommand(EventCommandEventArgs commandArgs)
		{
			Contract.Assert(this.m_completelyInited);
			if (this.m_provider == null)
			{
				return;
			}
			this.m_outOfBandMessageCount = 0;
			if (commandArgs.perEventSourceSessionId > 0)
			{
				bool flag = (long)commandArgs.perEventSourceSessionId <= 4L;
			}
			try
			{
				this.EnsureDescriptorsInitialized();
				Contract.Assert(this.m_eventData != null);
				commandArgs.dispatcher = this.GetDispatcher(commandArgs.listener);
				if (commandArgs.dispatcher == null && commandArgs.listener != null)
				{
					throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_ListenerNotFound", Array.Empty<object>()));
				}
				if (commandArgs.Arguments == null)
				{
					commandArgs.Arguments = new Dictionary<string, string>();
				}
				if (commandArgs.Command == EventCommand.Update)
				{
					for (int i = 0; i < this.m_eventData.Length; i++)
					{
						this.EnableEventForDispatcher(commandArgs.dispatcher, i, this.IsEnabledByDefault(i, commandArgs.enable, commandArgs.level, commandArgs.matchAnyKeyword));
					}
					if (commandArgs.enable)
					{
						if (!this.m_eventSourceEnabled)
						{
							this.m_level = commandArgs.level;
							this.m_matchAnyKeyword = commandArgs.matchAnyKeyword;
						}
						else
						{
							if (commandArgs.level > this.m_level)
							{
								this.m_level = commandArgs.level;
							}
							if (commandArgs.matchAnyKeyword == EventKeywords.None)
							{
								this.m_matchAnyKeyword = EventKeywords.None;
							}
							else if (this.m_matchAnyKeyword != EventKeywords.None)
							{
								this.m_matchAnyKeyword |= commandArgs.matchAnyKeyword;
							}
						}
					}
					bool flag2 = commandArgs.perEventSourceSessionId >= 0;
					if (commandArgs.perEventSourceSessionId == 0 && !commandArgs.enable)
					{
						flag2 = false;
					}
					if (commandArgs.listener == null)
					{
						if (!flag2)
						{
							commandArgs.perEventSourceSessionId = -commandArgs.perEventSourceSessionId;
						}
						commandArgs.perEventSourceSessionId--;
					}
					commandArgs.Command = (flag2 ? EventCommand.Enable : EventCommand.Disable);
					Contract.Assert(commandArgs.perEventSourceSessionId >= -1 && (long)commandArgs.perEventSourceSessionId <= 4L);
					if (flag2 && commandArgs.dispatcher == null && !this.SelfDescribingEvents)
					{
						this.SendManifest(this.m_rawManifest);
					}
					if (commandArgs.enable)
					{
						Contract.Assert(this.m_eventData != null);
						this.m_eventSourceEnabled = true;
					}
					this.OnEventCommand(commandArgs);
					if (!commandArgs.enable)
					{
						for (int j = 0; j < this.m_eventData.Length; j++)
						{
							bool flag3 = false;
							for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
							{
								if (eventDispatcher.m_EventEnabled[j])
								{
									flag3 = true;
									break;
								}
							}
							this.m_eventData[j].EnabledForAnyListener = flag3;
						}
						if (!this.AnyEventEnabled())
						{
							this.m_level = EventLevel.LogAlways;
							this.m_matchAnyKeyword = EventKeywords.None;
							this.m_eventSourceEnabled = false;
						}
					}
				}
				else
				{
					if (commandArgs.Command == EventCommand.SendManifest && this.m_rawManifest != null)
					{
						this.SendManifest(this.m_rawManifest);
					}
					this.OnEventCommand(commandArgs);
				}
			}
			catch (Exception ex)
			{
				this.ReportOutOfBandMessage("ERROR: Exception in Command Processing for EventSource " + this.Name + ": " + ex.Message, true);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005BA0 File Offset: 0x00003DA0
		internal bool EnableEventForDispatcher(EventDispatcher dispatcher, int eventId, bool value)
		{
			if (dispatcher == null)
			{
				if (eventId >= this.m_eventData.Length)
				{
					return false;
				}
				if (this.m_provider != null)
				{
					this.m_eventData[eventId].EnabledForETW = value;
				}
			}
			else
			{
				if (eventId >= dispatcher.m_EventEnabled.Length)
				{
					return false;
				}
				dispatcher.m_EventEnabled[eventId] = value;
				if (value)
				{
					this.m_eventData[eventId].EnabledForAnyListener = true;
				}
			}
			return true;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005C10 File Offset: 0x00003E10
		private bool AnyEventEnabled()
		{
			for (int i = 0; i < this.m_eventData.Length; i++)
			{
				if (this.m_eventData[i].EnabledForETW || this.m_eventData[i].EnabledForAnyListener)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005C5F File Offset: 0x00003E5F
		private bool IsDisposed
		{
			get
			{
				return this.m_provider == null || this.m_provider.m_disposed;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005C7C File Offset: 0x00003E7C
		[SecuritySafeCritical]
		private void EnsureDescriptorsInitialized()
		{
			if (this.m_eventData == null)
			{
				Contract.Assert(this.m_rawManifest == null);
				this.m_rawManifest = EventSource.CreateManifestAndDescriptors(base.GetType(), this.Name, this, EventManifestOptions.None);
				Contract.Assert(this.m_eventData != null);
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null && eventSource.Guid == this.m_guid && !eventSource.IsDisposed && eventSource != this)
					{
						throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventSourceGuidInUse", new object[] { this.m_guid }));
					}
				}
				for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
				{
					if (eventDispatcher.m_EventEnabled == null)
					{
						eventDispatcher.m_EventEnabled = new bool[this.m_eventData.Length];
					}
				}
			}
			if (EventSource.s_currentPid == 0U)
			{
				new SecurityPermission(PermissionState.Unrestricted).Assert();
				EventSource.s_currentPid = Win32Native.GetCurrentProcessId();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005DB0 File Offset: 0x00003FB0
		[SecuritySafeCritical]
		private unsafe bool SendManifest(byte[] rawManifest)
		{
			bool flag = true;
			if (rawManifest == null)
			{
				return false;
			}
			Contract.Assert(!this.SelfDescribingEvents);
			fixed (byte[] array = rawManifest)
			{
				byte* ptr;
				if (rawManifest == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				EventDescriptor eventDescriptor = new EventDescriptor(65534, 1, 0, 0, 254, 65534, 72057594037927935L);
				ManifestEnvelope manifestEnvelope = default(ManifestEnvelope);
				manifestEnvelope.Format = ManifestEnvelope.ManifestFormats.SimpleXmlFormat;
				manifestEnvelope.MajorVersion = 1;
				manifestEnvelope.MinorVersion = 0;
				manifestEnvelope.Magic = 91;
				int i = rawManifest.Length;
				manifestEnvelope.ChunkNumber = 0;
				EventProvider.EventData* ptr2;
				checked
				{
					ptr2 = stackalloc EventProvider.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventProvider.EventData)];
					ptr2->Ptr = &manifestEnvelope;
					ptr2->Size = (uint)sizeof(ManifestEnvelope);
					ptr2->Reserved = 0U;
				}
				ptr2[1].Ptr = ptr;
				ptr2[1].Reserved = 0U;
				int num = 65280;
				for (;;)
				{
					IL_00D8:
					manifestEnvelope.TotalChunks = (ushort)((i + (num - 1)) / num);
					while (i > 0)
					{
						ptr2[1].Size = (uint)Math.Min(i, num);
						if (this.m_provider != null && !this.m_provider.WriteEvent(ref eventDescriptor, null, null, 2, (IntPtr)((void*)ptr2)))
						{
							if (EventProvider.GetLastWriteEventError() == EventProvider.WriteEventErrorCode.EventTooBig && manifestEnvelope.ChunkNumber == 0 && num > 256)
							{
								num /= 2;
								goto IL_00D8;
							}
							goto IL_014F;
						}
						else
						{
							i -= num;
							ptr2[1].Ptr += (ulong)num;
							manifestEnvelope.ChunkNumber += 1;
						}
					}
					goto IL_0193;
				}
				IL_014F:
				flag = false;
				if (this.ThrowOnEventWriteErrors)
				{
					this.ThrowEventSourceException(null);
				}
				IL_0193:;
			}
			return flag;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005F54 File Offset: 0x00004154
		internal static Attribute GetCustomAttributeHelper(MemberInfo member, Type attributeType, EventManifestOptions flags = EventManifestOptions.None)
		{
			if (!member.Module.Assembly.ReflectionOnly() && (flags & EventManifestOptions.AllowEventSourceOverride) == EventManifestOptions.None)
			{
				Attribute attribute = null;
				object[] customAttributes = member.GetCustomAttributes(attributeType, false);
				int num = 0;
				if (num < customAttributes.Length)
				{
					attribute = (Attribute)customAttributes[num];
				}
				return attribute;
			}
			string fullName = attributeType.FullName;
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(member))
			{
				if (EventSource.AttributeTypeNamesMatch(attributeType, customAttributeData.Constructor.ReflectedType))
				{
					Attribute attribute2 = null;
					Contract.Assert(customAttributeData.ConstructorArguments.Count <= 1);
					if (customAttributeData.ConstructorArguments.Count == 1)
					{
						attribute2 = (Attribute)Activator.CreateInstance(attributeType, new object[] { customAttributeData.ConstructorArguments[0].Value });
					}
					else if (customAttributeData.ConstructorArguments.Count == 0)
					{
						attribute2 = (Attribute)Activator.CreateInstance(attributeType);
					}
					if (attribute2 != null)
					{
						Type type = attribute2.GetType();
						foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
						{
							PropertyInfo property = type.GetProperty(customAttributeNamedArgument.MemberInfo.Name, BindingFlags.Instance | BindingFlags.Public);
							object obj = customAttributeNamedArgument.TypedValue.Value;
							if (property.PropertyType.IsEnum)
							{
								obj = Enum.Parse(property.PropertyType, obj.ToString());
							}
							property.SetValue(attribute2, obj, null);
						}
						return attribute2;
					}
				}
			}
			return null;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006130 File Offset: 0x00004330
		private static bool AttributeTypeNamesMatch(Type attributeType, Type reflectedAttributeType)
		{
			return attributeType == reflectedAttributeType || string.Equals(attributeType.FullName, reflectedAttributeType.FullName, StringComparison.Ordinal) || (string.Equals(attributeType.Name, reflectedAttributeType.Name, StringComparison.Ordinal) && attributeType.Namespace.EndsWith("Diagnostics.Tracing") && reflectedAttributeType.Namespace.EndsWith("Diagnostics.Tracing"));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006194 File Offset: 0x00004394
		private static Type GetEventSourceBaseType(Type eventSourceType, bool allowEventSourceOverride, bool reflectionOnly)
		{
			if (eventSourceType.BaseType() == null)
			{
				return null;
			}
			do
			{
				eventSourceType = eventSourceType.BaseType();
			}
			while (eventSourceType != null && eventSourceType.IsAbstract());
			if (eventSourceType != null)
			{
				if (!allowEventSourceOverride)
				{
					if ((reflectionOnly && eventSourceType.FullName != typeof(EventSource).FullName) || (!reflectionOnly && eventSourceType != typeof(EventSource)))
					{
						return null;
					}
				}
				else if (eventSourceType.Name != "EventSource")
				{
					return null;
				}
			}
			return eventSourceType;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006224 File Offset: 0x00004424
		private static byte[] CreateManifestAndDescriptors(Type eventSourceType, string eventSourceDllName, EventSource source, EventManifestOptions flags = EventManifestOptions.None)
		{
			ManifestBuilder manifestBuilder = null;
			bool flag = source == null || !source.SelfDescribingEvents;
			Exception ex = null;
			byte[] array = null;
			if (eventSourceType.IsAbstract() && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
			{
				return null;
			}
			try
			{
				MethodInfo[] methods = eventSourceType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				int num = 1;
				EventSource.EventMetadata[] array2 = null;
				Dictionary<string, string> dictionary = null;
				if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					array2 = new EventSource.EventMetadata[methods.Length + 1];
					array2[0].Name = "";
				}
				ResourceManager resourceManager = null;
				EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
				if (eventSourceAttribute != null && eventSourceAttribute.LocalizationResources != null)
				{
					resourceManager = new ResourceManager(eventSourceAttribute.LocalizationResources, eventSourceType.Assembly());
				}
				manifestBuilder = new ManifestBuilder(EventSource.GetName(eventSourceType, flags), EventSource.GetGuid(eventSourceType), eventSourceDllName, resourceManager, flags);
				manifestBuilder.StartEvent("EventSourceMessage", new EventAttribute(0)
				{
					Level = EventLevel.LogAlways,
					Task = (EventTask)65534
				});
				manifestBuilder.AddEventParameter(typeof(string), "message");
				manifestBuilder.EndEvent();
				if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					if (!(EventSource.GetEventSourceBaseType(eventSourceType, (flags & EventManifestOptions.AllowEventSourceOverride) > EventManifestOptions.None, eventSourceType.Assembly().ReflectionOnly()) != null))
					{
						manifestBuilder.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_TypeMustDeriveFromEventSource", Array.Empty<object>()), false);
					}
					if (!eventSourceType.IsAbstract() && !eventSourceType.IsSealed())
					{
						manifestBuilder.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_TypeMustBeSealedOrAbstract", Array.Empty<object>()), false);
					}
				}
				foreach (string text in new string[] { "Keywords", "Tasks", "Opcodes" })
				{
					Type nestedType = eventSourceType.GetNestedType(text);
					if (nestedType != null)
					{
						if (eventSourceType.IsAbstract())
						{
							manifestBuilder.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_AbstractMustNotDeclareKTOC", new object[] { nestedType.Name }), false);
						}
						else
						{
							foreach (FieldInfo fieldInfo in nestedType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
							{
								EventSource.AddProviderEnumKind(manifestBuilder, fieldInfo, text);
							}
						}
					}
				}
				manifestBuilder.AddKeyword("Session3", 17592186044416UL);
				manifestBuilder.AddKeyword("Session2", 35184372088832UL);
				manifestBuilder.AddKeyword("Session1", 70368744177664UL);
				manifestBuilder.AddKeyword("Session0", 140737488355328UL);
				if (eventSourceType.Name != "EventSource")
				{
					foreach (MethodInfo methodInfo in methods)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						EventAttribute eventAttribute = (EventAttribute)EventSource.GetCustomAttributeHelper(methodInfo, typeof(EventAttribute), flags);
						if (!methodInfo.IsStatic)
						{
							if (eventSourceType.IsAbstract())
							{
								if (eventAttribute != null)
								{
									manifestBuilder.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_AbstractMustNotDeclareEventMethods", new object[] { methodInfo.Name, eventAttribute.EventId }), false);
								}
							}
							else
							{
								if (eventAttribute == null)
								{
									if (methodInfo.ReturnType != typeof(void) || methodInfo.IsVirtual || EventSource.GetCustomAttributeHelper(methodInfo, typeof(NonEventAttribute), flags) != null)
									{
										goto IL_0638;
									}
									eventAttribute = new EventAttribute(num);
								}
								else if (eventAttribute.EventId <= 0)
								{
									manifestBuilder.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NeedPositiveId", new object[] { methodInfo.Name }), true);
									goto IL_0638;
								}
								if (methodInfo.Name.LastIndexOf('.') >= 0)
								{
									manifestBuilder.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventMustNotBeExplicitImplementation", new object[] { methodInfo.Name, eventAttribute.EventId }), false);
								}
								num++;
								string name = methodInfo.Name;
								if (eventAttribute.Opcode == EventOpcode.Info)
								{
									bool flag2 = eventAttribute.Task == EventTask.None;
									if (flag2)
									{
										eventAttribute.Task = (EventTask)65534 - eventAttribute.EventId;
									}
									if (!eventAttribute.IsOpcodeSet)
									{
										eventAttribute.Opcode = EventSource.GetOpcodeWithDefault(EventOpcode.Info, name);
									}
									if (flag2)
									{
										if (eventAttribute.Opcode == EventOpcode.Start)
										{
											string text2 = name.Substring(0, name.Length - "Start".Length);
											if (string.Compare(name, 0, text2, 0, text2.Length) == 0 && string.Compare(name, text2.Length, "Start", 0, Math.Max(name.Length - text2.Length, "Start".Length)) == 0)
											{
												manifestBuilder.AddTask(text2, (int)eventAttribute.Task);
											}
										}
										else if (eventAttribute.Opcode == EventOpcode.Stop)
										{
											int num2 = eventAttribute.EventId - 1;
											if (num2 < array2.Length)
											{
												Contract.Assert(0 <= num2);
												EventSource.EventMetadata eventMetadata = array2[num2];
												string text3 = name.Substring(0, name.Length - "Stop".Length);
												if (eventMetadata.Descriptor.Opcode == 1 && string.Compare(eventMetadata.Name, 0, text3, 0, text3.Length) == 0 && string.Compare(eventMetadata.Name, text3.Length, "Start", 0, Math.Max(eventMetadata.Name.Length - text3.Length, "Start".Length)) == 0)
												{
													eventAttribute.Task = (EventTask)eventMetadata.Descriptor.Task;
													flag2 = false;
												}
											}
											if (flag2 && (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
											{
												throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_StopsFollowStarts", Array.Empty<object>()));
											}
										}
									}
								}
								EventSource.RemoveFirstArgIfRelatedActivityId(ref parameters);
								if (source == null || !source.SelfDescribingEvents)
								{
									manifestBuilder.StartEvent(name, eventAttribute);
									for (int l = 0; l < parameters.Length; l++)
									{
										manifestBuilder.AddEventParameter(parameters[l].ParameterType, parameters[l].Name);
									}
									manifestBuilder.EndEvent();
								}
								if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
								{
									EventSource.DebugCheckEvent(ref dictionary, array2, methodInfo, eventAttribute, manifestBuilder);
									if (eventAttribute.Channel != EventChannel.None)
									{
										eventAttribute.Keywords |= (EventKeywords)manifestBuilder.GetChannelKeyword(eventAttribute.Channel);
									}
									string text4 = "event_" + name;
									string localizedMessage = manifestBuilder.GetLocalizedMessage(text4, CultureInfo.CurrentUICulture, false);
									if (localizedMessage != null)
									{
										eventAttribute.Message = localizedMessage;
									}
									EventSource.AddEventDescriptor(ref array2, name, eventAttribute, parameters);
								}
							}
						}
						IL_0638:;
					}
				}
				NameInfo.ReserveEventIDsBelow(num);
				if (source != null)
				{
					EventSource.TrimEventDescriptors(ref array2);
					source.m_eventData = array2;
					source.m_channelData = manifestBuilder.GetChannelData();
				}
				if (!eventSourceType.IsAbstract() && (source == null || !source.SelfDescribingEvents))
				{
					flag = (flags & EventManifestOptions.OnlyIfNeededForRegistration) == EventManifestOptions.None || manifestBuilder.GetChannelData().Length != 0;
					if (!flag && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
					{
						return null;
					}
					array = manifestBuilder.CreateManifest();
				}
			}
			catch (Exception ex2)
			{
				if ((flags & EventManifestOptions.Strict) == EventManifestOptions.None)
				{
					throw;
				}
				ex = ex2;
			}
			if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None && (manifestBuilder.Errors.Count > 0 || ex != null))
			{
				string text5 = string.Empty;
				if (manifestBuilder.Errors.Count > 0)
				{
					bool flag3 = true;
					using (IEnumerator<string> enumerator = manifestBuilder.Errors.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text6 = enumerator.Current;
							if (!flag3)
							{
								text5 += Microsoft.Diagnostics.Tracing.Internal.Environment.NewLine;
							}
							flag3 = false;
							text5 += text6;
						}
						goto IL_0750;
					}
				}
				text5 = "Unexpected error: " + ex.Message;
				IL_0750:
				throw new ArgumentException(text5, ex);
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000069C8 File Offset: 0x00004BC8
		private static void RemoveFirstArgIfRelatedActivityId(ref ParameterInfo[] args)
		{
			if (args.Length != 0 && args[0].ParameterType == typeof(Guid) && string.Compare(args[0].Name, "relatedActivityId", StringComparison.OrdinalIgnoreCase) == 0)
			{
				ParameterInfo[] array = new ParameterInfo[args.Length - 1];
				Array.Copy(args, 1, array, 0, args.Length - 1);
				args = array;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006A2C File Offset: 0x00004C2C
		private static void AddProviderEnumKind(ManifestBuilder manifest, FieldInfo staticField, string providerEnumKind)
		{
			bool flag = staticField.Module.Assembly.ReflectionOnly();
			Type fieldType = staticField.FieldType;
			if ((!flag && fieldType == typeof(EventOpcode)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventOpcode)))
			{
				if (!(providerEnumKind != "Opcodes"))
				{
					int num = (int)staticField.GetRawConstantValue();
					manifest.AddOpcode(staticField.Name, num);
					return;
				}
			}
			else
			{
				if ((flag || !(fieldType == typeof(EventTask))) && !EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventTask)))
				{
					if ((!flag && fieldType == typeof(EventKeywords)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventKeywords)))
					{
						if (providerEnumKind != "Keywords")
						{
							goto IL_0107;
						}
						ulong num2 = (ulong)((long)staticField.GetRawConstantValue());
						manifest.AddKeyword(staticField.Name, num2);
					}
					return;
				}
				if (!(providerEnumKind != "Tasks"))
				{
					int num3 = (int)staticField.GetRawConstantValue();
					manifest.AddTask(staticField.Name, num3);
					return;
				}
			}
			IL_0107:
			manifest.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EnumKindMismatch", new object[]
			{
				staticField.Name,
				staticField.FieldType.Name,
				providerEnumKind
			}), false);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006B74 File Offset: 0x00004D74
		private static void AddEventDescriptor(ref EventSource.EventMetadata[] eventData, string eventName, EventAttribute eventAttribute, ParameterInfo[] eventParameters)
		{
			if (eventData == null || eventData.Length <= eventAttribute.EventId)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[Math.Max(eventData.Length + 16, eventAttribute.EventId + 1)];
				Array.Copy(eventData, array, eventData.Length);
				eventData = array;
			}
			eventData[eventAttribute.EventId].Descriptor = new EventDescriptor(eventAttribute.EventId, eventAttribute.Version, (byte)eventAttribute.Channel, (byte)eventAttribute.Level, (byte)eventAttribute.Opcode, (int)eventAttribute.Task, (long)(eventAttribute.Keywords | (EventKeywords)SessionMask.All.ToEventKeywords()));
			eventData[eventAttribute.EventId].Tags = eventAttribute.Tags;
			eventData[eventAttribute.EventId].Name = eventName;
			eventData[eventAttribute.EventId].Parameters = eventParameters;
			eventData[eventAttribute.EventId].Message = eventAttribute.Message;
			eventData[eventAttribute.EventId].ActivityOptions = eventAttribute.ActivityOptions;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006C7C File Offset: 0x00004E7C
		private static void TrimEventDescriptors(ref EventSource.EventMetadata[] eventData)
		{
			int num = eventData.Length;
			while (0 < num)
			{
				num--;
				if (eventData[num].Descriptor.EventId != 0)
				{
					break;
				}
			}
			if (eventData.Length - num > 2)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[num + 1];
				Array.Copy(eventData, array, array.Length);
				eventData = array;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006CCC File Offset: 0x00004ECC
		internal void AddListener(EventListener listener)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				bool[] array = null;
				if (this.m_eventData != null)
				{
					array = new bool[this.m_eventData.Length];
				}
				this.m_Dispatchers = new EventDispatcher(this.m_Dispatchers, array, listener);
				listener.OnEventSourceCreated(this);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006D40 File Offset: 0x00004F40
		private static void DebugCheckEvent(ref Dictionary<string, string> eventsByName, EventSource.EventMetadata[] eventData, MethodInfo method, EventAttribute eventAttribute, ManifestBuilder manifest)
		{
			int eventId = eventAttribute.EventId;
			string name = method.Name;
			int helperCallFirstArg = EventSource.GetHelperCallFirstArg(method);
			if (helperCallFirstArg >= 0 && eventId != helperCallFirstArg)
			{
				manifest.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_MismatchIdToWriteEvent", new object[] { name, eventId, helperCallFirstArg }), true);
			}
			if (eventId < eventData.Length && eventData[eventId].Descriptor.EventId != 0)
			{
				manifest.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventIdReused", new object[]
				{
					name,
					eventId,
					eventData[eventId].Name
				}), true);
			}
			for (int i = 0; i < eventData.Length; i++)
			{
				if (eventData[i].Descriptor.Task == (int)eventAttribute.Task && (EventOpcode)eventData[i].Descriptor.Opcode == eventAttribute.Opcode)
				{
					manifest.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_TaskOpcodePairReused", new object[]
					{
						name,
						eventId,
						eventData[i].Name,
						i
					}), false);
				}
			}
			if (eventAttribute.Opcode != EventOpcode.Info)
			{
				bool flag = false;
				if (eventAttribute.Task == EventTask.None)
				{
					flag = true;
				}
				else
				{
					EventTask eventTask = (EventTask)65534 - eventId;
					if (eventAttribute.Opcode != EventOpcode.Start && eventAttribute.Opcode != EventOpcode.Stop && eventAttribute.Task == eventTask)
					{
						flag = true;
					}
				}
				if (flag)
				{
					manifest.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventMustHaveTaskIfNonDefaultOpcode", new object[] { name, eventId }), false);
				}
			}
			if (eventsByName == null)
			{
				eventsByName = new Dictionary<string, string>();
			}
			if (eventsByName.ContainsKey(name))
			{
				manifest.ManifestError(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_EventNameReused", new object[] { name }), false);
			}
			eventsByName[name] = name;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006F08 File Offset: 0x00005108
		[SecuritySafeCritical]
		private static int GetHelperCallFirstArg(MethodInfo method)
		{
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			byte[] ilasByteArray = method.GetMethodBody().GetILAsByteArray();
			int num = -1;
			for (int i = 0; i < ilasByteArray.Length; i++)
			{
				byte b = ilasByteArray[i];
				if (b <= 110)
				{
					switch (b)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 20:
					case 37:
						break;
					case 14:
					case 16:
						i++;
						break;
					case 15:
					case 17:
					case 18:
					case 19:
					case 33:
					case 34:
					case 35:
					case 36:
					case 38:
					case 39:
					case 41:
					case 42:
					case 43:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
						return -1;
					case 21:
					case 22:
					case 23:
					case 24:
					case 25:
					case 26:
					case 27:
					case 28:
					case 29:
					case 30:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)(ilasByteArray[i] - 22);
						}
						break;
					case 31:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)ilasByteArray[i + 1];
						}
						i++;
						break;
					case 32:
						i += 4;
						break;
					case 40:
						i += 4;
						if (num >= 0)
						{
							for (int j = i + 1; j < ilasByteArray.Length; j++)
							{
								if (ilasByteArray[j] == 42)
								{
									return num;
								}
								if (ilasByteArray[j] != 0)
								{
									break;
								}
							}
						}
						num = -1;
						break;
					case 44:
					case 45:
						num = -1;
						i++;
						break;
					case 57:
					case 58:
						num = -1;
						i += 4;
						break;
					default:
						if (b - 103 > 3 && b - 109 > 1)
						{
							return -1;
						}
						break;
					}
				}
				else if (b - 140 > 1)
				{
					if (b != 162)
					{
						if (b != 254)
						{
							return -1;
						}
						i++;
						if (i >= ilasByteArray.Length || ilasByteArray[i] >= 6)
						{
							return -1;
						}
					}
				}
				else
				{
					i += 4;
				}
			}
			return -1;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000711C File Offset: 0x0000531C
		internal void ReportOutOfBandMessage(string msg, bool flush)
		{
			try
			{
				Debugger.Log(0, null, msg + "\r\n");
				if (this.m_outOfBandMessageCount < 254)
				{
					this.m_outOfBandMessageCount += 1;
				}
				else
				{
					if (this.m_outOfBandMessageCount == 255)
					{
						return;
					}
					this.m_outOfBandMessageCount = byte.MaxValue;
					msg = "Reached message limit.   End of EventSource error messages.";
				}
				this.WriteEventString(EventLevel.LogAlways, -1L, msg);
				this.WriteStringToAllListeners("EventSourceMessage", msg);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000071A8 File Offset: 0x000053A8
		private EventSourceSettings ValidateSettings(EventSourceSettings settings)
		{
			EventSourceSettings eventSourceSettings = EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat;
			if ((settings & eventSourceSettings) == eventSourceSettings)
			{
				throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_InvalidEventFormat", Array.Empty<object>()), "settings");
			}
			if ((settings & eventSourceSettings) == EventSourceSettings.Default)
			{
				settings |= EventSourceSettings.EtwSelfDescribingEventFormat;
			}
			return settings;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000071E3 File Offset: 0x000053E3
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000071F0 File Offset: 0x000053F0
		private bool ThrowOnEventWriteErrors
		{
			get
			{
				return (this.m_config & EventSourceSettings.ThrowOnEventWriteErrors) > EventSourceSettings.Default;
			}
			set
			{
				if (value)
				{
					this.m_config |= EventSourceSettings.ThrowOnEventWriteErrors;
					return;
				}
				this.m_config &= ~EventSourceSettings.ThrowOnEventWriteErrors;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00007213 File Offset: 0x00005413
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00007240 File Offset: 0x00005440
		private bool SelfDescribingEvents
		{
			get
			{
				Contract.Assert((this.m_config & EventSourceSettings.EtwManifestEventFormat) > EventSourceSettings.Default != (this.m_config & EventSourceSettings.EtwSelfDescribingEventFormat) > EventSourceSettings.Default);
				return (this.m_config & EventSourceSettings.EtwSelfDescribingEventFormat) > EventSourceSettings.Default;
			}
			set
			{
				if (!value)
				{
					this.m_config |= EventSourceSettings.EtwManifestEventFormat;
					this.m_config &= ~EventSourceSettings.EtwSelfDescribingEventFormat;
					return;
				}
				this.m_config |= EventSourceSettings.EtwSelfDescribingEventFormat;
				this.m_config &= ~EventSourceSettings.EtwManifestEventFormat;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007280 File Offset: 0x00005480
		public EventSource(string eventSourceName)
			: this(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000728A File Offset: 0x0000548A
		public EventSource(string eventSourceName, EventSourceSettings config)
			: this(eventSourceName, config, null)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007298 File Offset: 0x00005498
		public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits)
			: this((eventSourceName == null) ? default(Guid) : EventSource.GenerateGuidFromName(eventSourceName.ToUpperInvariant()), eventSourceName, config, traits)
		{
			if (eventSourceName == null)
			{
				throw new ArgumentNullException("eventSourceName");
			}
			Contract.EndContractBlock();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000072DC File Offset: 0x000054DC
		[SecuritySafeCritical]
		public void Write(string eventName)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			Contract.EndContractBlock();
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.WriteImpl<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct, null, null);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007324 File Offset: 0x00005524
		[SecuritySafeCritical]
		public void Write(string eventName, EventSourceOptions options)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			Contract.EndContractBlock();
			if (!this.IsEnabled())
			{
				return;
			}
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.WriteImpl<EmptyStruct>(eventName, ref options, ref emptyStruct, null, null);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007364 File Offset: 0x00005564
		[SecuritySafeCritical]
		public void Write<T>(string eventName, T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.WriteImpl<T>(eventName, ref eventSourceOptions, ref data, null, null);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007391 File Offset: 0x00005591
		[SecuritySafeCritical]
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl<T>(eventName, ref options, ref data, null, null);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000073AB File Offset: 0x000055AB
		[SecuritySafeCritical]
		public void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl<T>(eventName, ref options, ref data, null, null);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000073C4 File Offset: 0x000055C4
		[SecuritySafeCritical]
		public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (Guid* ptr = &activityId)
			{
				Guid* ptr2 = ptr;
				fixed (Guid* ptr3 = &relatedActivityId)
				{
					Guid* ptr4 = ptr3;
					this.WriteImpl<T>(eventName, ref options, ref data, ptr2, (relatedActivityId == Guid.Empty) ? null : ptr4);
					ptr = null;
				}
				return;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007410 File Offset: 0x00005610
		[SecuritySafeCritical]
		private unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventTypes.level);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords);
			if (this.IsEnabled((EventLevel)b, eventKeywords))
			{
				this.WriteMultiMergeInner(eventName, ref options, eventTypes, activityID, childActivityID, values);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007474 File Offset: 0x00005674
		[SecuritySafeCritical]
		private unsafe void WriteMultiMergeInner(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventTypes.level);
			byte b2 = (((options.valuesSet & 8) != 0) ? options.opcode : eventTypes.opcode);
			EventTags eventTags = (((options.valuesSet & 2) != 0) ? options.tags : eventTypes.Tags);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords);
			NameInfo nameInfo = eventTypes.GetNameInfo(eventName ?? eventTypes.Name, eventTags);
			if (nameInfo == null)
			{
				return;
			}
			int identity = nameInfo.identity;
			EventDescriptor eventDescriptor = new EventDescriptor(identity, b, b2, (long)eventKeywords);
			int pinCount = eventTypes.pinCount;
			byte* ptr = stackalloc byte[(UIntPtr)eventTypes.scratchSize];
			EventSource.EventData* ptr2;
			GCHandle* ptr3;
			byte[] array;
			byte[] array2;
			byte* ptr5;
			byte[] array3;
			byte* ptr6;
			checked
			{
				ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)(eventTypes.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
				ptr3 = stackalloc GCHandle[unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle)];
				byte* ptr4;
				if ((array = this.providerMetadata) == null || array.Length == 0)
				{
					ptr4 = null;
				}
				else
				{
					ptr4 = &array[0];
				}
				if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
				{
					ptr5 = null;
				}
				else
				{
					ptr5 = &array2[0];
				}
				if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
				{
					ptr6 = null;
				}
				else
				{
					ptr6 = &array3[0];
				}
				ptr2->SetMetadata(ptr4, this.providerMetadata.Length, 2);
			}
			ptr2[1].SetMetadata(ptr5, nameInfo.nameMetadata.Length, 1);
			ptr2[2].SetMetadata(ptr6, eventTypes.typeMetadata.Length, 1);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				DataCollector.ThreadInstance.Enable(ptr, eventTypes.scratchSize, ptr2 + 3, eventTypes.dataCount, ptr3, pinCount);
				for (int i = 0; i < eventTypes.typeInfos.Length; i++)
				{
					eventTypes.typeInfos[i].WriteObjectData(TraceLoggingDataCollector.Instance, values[i]);
				}
				this.WriteEventRaw(ref eventDescriptor, activityID, childActivityID, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr2)), (IntPtr)((void*)ptr2));
			}
			finally
			{
				this.WriteCleanup(ptr3, pinCount);
			}
			array = null;
			array2 = null;
			array3 = null;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000076A4 File Offset: 0x000058A4
		[SecuritySafeCritical]
		internal unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, EventSource.EventData* data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (EventSourceOptions* ptr = &options)
			{
				EventDescriptor eventDescriptor;
				NameInfo nameInfo = this.UpdateDescriptor(eventName, eventTypes, ref options, out eventDescriptor);
				if (nameInfo == null)
				{
					return;
				}
				EventSource.EventData* ptr2;
				byte[] array;
				byte[] array2;
				byte* ptr4;
				byte[] array3;
				byte* ptr5;
				checked
				{
					ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)(eventTypes.dataCount + eventTypes.typeInfos.Length * 2 + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
					byte* ptr3;
					if ((array = this.providerMetadata) == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
					{
						ptr4 = null;
					}
					else
					{
						ptr4 = &array2[0];
					}
					if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
					{
						ptr5 = null;
					}
					else
					{
						ptr5 = &array3[0];
					}
					ptr2->SetMetadata(ptr3, this.providerMetadata.Length, 2);
				}
				ptr2[1].SetMetadata(ptr4, nameInfo.nameMetadata.Length, 1);
				ptr2[2].SetMetadata(ptr5, eventTypes.typeMetadata.Length, 1);
				int num = 3;
				for (int i = 0; i < eventTypes.typeInfos.Length; i++)
				{
					if (eventTypes.typeInfos[i].DataType == typeof(string))
					{
						ptr2[num].m_Ptr = &ptr2[num + 1].m_Size;
						ptr2[num].m_Size = 2;
						num++;
						ptr2[num].m_Ptr = data[i].m_Ptr;
						ptr2[num].m_Size = data[i].m_Size - 2;
						num++;
					}
					else
					{
						ptr2[num].m_Ptr = data[i].m_Ptr;
						ptr2[num].m_Size = data[i].m_Size;
						if (data[i].m_Size == 4 && eventTypes.typeInfos[i].DataType == typeof(bool))
						{
							ptr2[num].m_Size = 1;
						}
						num++;
					}
				}
				this.WriteEventRaw(ref eventDescriptor, activityID, childActivityID, num, (IntPtr)((void*)ptr2));
				array = null;
				array2 = null;
				array3 = null;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000791C File Offset: 0x00005B1C
		[SecuritySafeCritical]
		private unsafe void WriteImpl<T>(string eventName, ref EventSourceOptions options, ref T data, Guid* pActivityId, Guid* pRelatedActivityId)
		{
			try
			{
				SimpleEventTypes<T> instance = SimpleEventTypes<T>.Instance;
				try
				{
					fixed (EventSourceOptions* ptr = &options)
					{
						options.Opcode = (options.IsOpcodeSet ? options.Opcode : EventSource.GetOpcodeWithDefault(options.Opcode, eventName));
						EventDescriptor eventDescriptor;
						NameInfo nameInfo = this.UpdateDescriptor(eventName, instance, ref options, out eventDescriptor);
						if (nameInfo != null)
						{
							int pinCount = instance.pinCount;
							byte* ptr2 = stackalloc byte[(UIntPtr)instance.scratchSize];
							EventSource.EventData* ptr3;
							GCHandle* ptr4;
							checked
							{
								ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)(instance.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
								ptr4 = stackalloc GCHandle[unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle)];
							}
							try
							{
								byte[] array;
								byte* ptr5;
								if ((array = this.providerMetadata) == null || array.Length == 0)
								{
									ptr5 = null;
								}
								else
								{
									ptr5 = &array[0];
								}
								byte[] array2;
								byte* ptr6;
								if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
								{
									ptr6 = null;
								}
								else
								{
									ptr6 = &array2[0];
								}
								byte[] array3;
								byte* ptr7;
								if ((array3 = instance.typeMetadata) == null || array3.Length == 0)
								{
									ptr7 = null;
								}
								else
								{
									ptr7 = &array3[0];
								}
								ptr3->SetMetadata(ptr5, this.providerMetadata.Length, 2);
								ptr3[1].SetMetadata(ptr6, nameInfo.nameMetadata.Length, 1);
								ptr3[2].SetMetadata(ptr7, instance.typeMetadata.Length, 1);
								RuntimeHelpers.PrepareConstrainedRegions();
								EventOpcode opcode = (EventOpcode)eventDescriptor.Opcode;
								Guid empty = Guid.Empty;
								Guid empty2 = Guid.Empty;
								if (pActivityId == null && pRelatedActivityId == null && (options.ActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
								{
									if (opcode == EventOpcode.Start)
									{
										this.m_activityTracker.OnStart(this.m_name, eventName, 0, ref empty, ref empty2, options.ActivityOptions);
									}
									else if (opcode == EventOpcode.Stop)
									{
										this.m_activityTracker.OnStop(this.m_name, eventName, 0, ref empty);
									}
									if (empty != Guid.Empty)
									{
										pActivityId = &empty;
									}
									if (empty2 != Guid.Empty)
									{
										pRelatedActivityId = &empty2;
									}
								}
								try
								{
									DataCollector.ThreadInstance.Enable(ptr2, instance.scratchSize, ptr3 + 3, instance.dataCount, ptr4, pinCount);
									instance.typeInfo.WriteData(TraceLoggingDataCollector.Instance, ref data);
									this.WriteEventRaw(ref eventDescriptor, pActivityId, pRelatedActivityId, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr3)), (IntPtr)((void*)ptr3));
									if (this.m_Dispatchers != null)
									{
										EventPayload eventPayload = (EventPayload)instance.typeInfo.GetData(data);
										this.WriteToAllListeners(eventName, ref eventDescriptor, nameInfo.tags, pActivityId, eventPayload);
									}
								}
								catch (Exception ex)
								{
									if (ex is EventSourceException)
									{
										throw;
									}
									this.ThrowEventSourceException(ex);
								}
								finally
								{
									this.WriteCleanup(ptr4, pinCount);
								}
							}
							finally
							{
								byte[] array = null;
								byte[] array2 = null;
								byte[] array3 = null;
							}
						}
					}
				}
				finally
				{
					EventSourceOptions* ptr = null;
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is EventSourceException)
				{
					throw;
				}
				this.ThrowEventSourceException(ex2);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007C4C File Offset: 0x00005E4C
		[SecurityCritical]
		private unsafe void WriteToAllListeners(string eventName, ref EventDescriptor eventDescriptor, EventTags tags, Guid* pActivityId, EventPayload payload)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventName = eventName;
			eventWrittenEventArgs.m_keywords = (EventKeywords)eventDescriptor.Keywords;
			eventWrittenEventArgs.m_opcode = (EventOpcode)eventDescriptor.Opcode;
			eventWrittenEventArgs.m_tags = tags;
			eventWrittenEventArgs.EventId = -1;
			if (pActivityId != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *pActivityId;
			}
			if (payload != null)
			{
				eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>((IList<object>)payload.Values);
				eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>((IList<string>)payload.Keys);
			}
			this.DisptachToAllListeners(-1, pActivityId, eventWrittenEventArgs);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007CDC File Offset: 0x00005EDC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[NonEvent]
		private unsafe void WriteCleanup(GCHandle* pPins, int cPins)
		{
			DataCollector.ThreadInstance.Disable();
			for (int num = 0; num != cPins; num++)
			{
				if (IntPtr.Zero != (IntPtr)pPins[num])
				{
					pPins[num].Free();
				}
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007D30 File Offset: 0x00005F30
		private void InitializeProviderMetadata()
		{
			if (this.m_traits != null)
			{
				List<byte> list = new List<byte>(100);
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i].StartsWith("ETW_"))
					{
						string text = this.m_traits[i].Substring(4);
						byte b;
						if (!byte.TryParse(text, out b))
						{
							if (!(text == "GROUP"))
							{
								throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("UnknownEtwTrait", new object[] { text }), "traits");
							}
							b = 1;
						}
						string text2 = this.m_traits[i + 1];
						int count = list.Count;
						list.Add(0);
						list.Add(0);
						list.Add(b);
						int num = EventSource.AddValueToMetaData(list, text2) + 3;
						list[count] = (byte)num;
						list[count + 1] = (byte)(num >> 8);
					}
				}
				this.providerMetadata = Statics.MetadataForString(this.Name, 0, list.Count, 0);
				int num2 = this.providerMetadata.Length - list.Count;
				using (List<byte>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						byte b2 = enumerator.Current;
						this.providerMetadata[num2++] = b2;
					}
					return;
				}
			}
			this.providerMetadata = Statics.MetadataForString(this.Name, 0, 0, 0);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007EA4 File Offset: 0x000060A4
		private static int AddValueToMetaData(List<byte> metaData, string value)
		{
			if (value.Length == 0)
			{
				return 0;
			}
			int count = metaData.Count;
			char c = value[0];
			if (c == '@')
			{
				metaData.AddRange(Encoding.UTF8.GetBytes(value.Substring(1)));
			}
			else if (c == '{')
			{
				metaData.AddRange(new Guid(value).ToByteArray());
			}
			else if (c == '#')
			{
				for (int i = 1; i < value.Length; i++)
				{
					if (value[i] != ' ')
					{
						if (i + 1 >= value.Length)
						{
							throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EvenHexDigits", Array.Empty<object>()), "traits");
						}
						metaData.Add((byte)(EventSource.HexDigit(value[i]) * 16 + EventSource.HexDigit(value[i + 1])));
						i++;
					}
				}
			}
			else
			{
				if ('A' > c && ' ' != c)
				{
					throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("IllegalValue", new object[] { value }), "traits");
				}
				metaData.AddRange(Encoding.UTF8.GetBytes(value));
			}
			return metaData.Count - count;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007FC0 File Offset: 0x000061C0
		private static int HexDigit(char c)
		{
			if ('0' <= c && c <= '9')
			{
				return (int)(c - '0');
			}
			if ('a' <= c)
			{
				c -= ' ';
			}
			if ('A' <= c && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			throw new ArgumentException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("BadHexDigit", new object[] { c }), "traits");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008020 File Offset: 0x00006220
		private NameInfo UpdateDescriptor(string name, TraceLoggingEventTypes eventInfo, ref EventSourceOptions options, out EventDescriptor descriptor)
		{
			NameInfo nameInfo = null;
			int num = 0;
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventInfo.level);
			byte b2 = (((options.valuesSet & 8) != 0) ? options.opcode : eventInfo.opcode);
			EventTags eventTags = (((options.valuesSet & 2) != 0) ? options.tags : eventInfo.Tags);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventInfo.keywords);
			if (this.IsEnabled((EventLevel)b, eventKeywords))
			{
				nameInfo = eventInfo.GetNameInfo(name ?? eventInfo.Name, eventTags);
				num = nameInfo.identity;
			}
			descriptor = new EventDescriptor(num, b, b2, (long)eventKeywords);
			return nameInfo;
		}

		// Token: 0x04000036 RID: 54
		private string m_name;

		// Token: 0x04000037 RID: 55
		internal int m_id;

		// Token: 0x04000038 RID: 56
		private Guid m_guid;

		// Token: 0x04000039 RID: 57
		internal volatile EventSource.EventMetadata[] m_eventData;

		// Token: 0x0400003A RID: 58
		private volatile byte[] m_rawManifest;

		// Token: 0x0400003B RID: 59
		private EventSourceSettings m_config;

		// Token: 0x0400003C RID: 60
		private bool m_eventSourceEnabled;

		// Token: 0x0400003D RID: 61
		internal EventLevel m_level;

		// Token: 0x0400003E RID: 62
		internal EventKeywords m_matchAnyKeyword;

		// Token: 0x0400003F RID: 63
		internal volatile EventDispatcher m_Dispatchers;

		// Token: 0x04000040 RID: 64
		private volatile EventSource.OverideEventProvider m_provider;

		// Token: 0x04000041 RID: 65
		private bool m_completelyInited;

		// Token: 0x04000042 RID: 66
		private Exception m_constructionException;

		// Token: 0x04000043 RID: 67
		private byte m_outOfBandMessageCount;

		// Token: 0x04000044 RID: 68
		private EventCommandEventArgs m_deferredCommands;

		// Token: 0x04000045 RID: 69
		private string[] m_traits;

		// Token: 0x04000046 RID: 70
		internal static uint s_currentPid;

		// Token: 0x04000047 RID: 71
		[ThreadStatic]
		private static byte m_EventSourceExceptionRecurenceCount = 0;

		// Token: 0x04000048 RID: 72
		internal volatile ulong[] m_channelData;

		// Token: 0x04000049 RID: 73
		private ActivityTracker m_activityTracker;

		// Token: 0x0400004A RID: 74
		internal const string s_ActivityStartSuffix = "Start";

		// Token: 0x0400004B RID: 75
		internal const string s_ActivityStopSuffix = "Stop";

		// Token: 0x0400004C RID: 76
		private static readonly byte[] namespaceBytes = new byte[]
		{
			72, 44, 45, 178, 195, 144, 71, 200, 135, 248,
			26, 21, 191, 193, 48, 251
		};

		// Token: 0x0400004D RID: 77
		private byte[] providerMetadata;

		// Token: 0x02000083 RID: 131
		protected internal struct EventData
		{
			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060002FC RID: 764 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
			// (set) Token: 0x060002FD RID: 765 RVA: 0x0000E9BD File Offset: 0x0000CBBD
			public IntPtr DataPointer
			{
				get
				{
					return (IntPtr)this.m_Ptr;
				}
				set
				{
					this.m_Ptr = (long)value;
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060002FE RID: 766 RVA: 0x0000E9CB File Offset: 0x0000CBCB
			// (set) Token: 0x060002FF RID: 767 RVA: 0x0000E9D3 File Offset: 0x0000CBD3
			public int Size
			{
				get
				{
					return this.m_Size;
				}
				set
				{
					this.m_Size = value;
				}
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000E9DC File Offset: 0x0000CBDC
			[SecurityCritical]
			internal unsafe void SetMetadata(byte* pointer, int size, int reserved)
			{
				this.m_Ptr = (long)(ulong)((UIntPtr)((void*)pointer));
				this.m_Size = size;
				this.m_Reserved = reserved;
			}

			// Token: 0x0400019B RID: 411
			internal long m_Ptr;

			// Token: 0x0400019C RID: 412
			internal int m_Size;

			// Token: 0x0400019D RID: 413
			internal int m_Reserved;
		}

		// Token: 0x02000084 RID: 132
		private struct Sha1ForNonSecretPurposes
		{
			// Token: 0x06000301 RID: 769 RVA: 0x0000EA00 File Offset: 0x0000CC00
			public void Start()
			{
				if (this.w == null)
				{
					this.w = new uint[85];
				}
				this.length = 0L;
				this.pos = 0;
				this.w[80] = 1732584193U;
				this.w[81] = 4023233417U;
				this.w[82] = 2562383102U;
				this.w[83] = 271733878U;
				this.w[84] = 3285377520U;
			}

			// Token: 0x06000302 RID: 770 RVA: 0x0000EA78 File Offset: 0x0000CC78
			public void Append(byte input)
			{
				this.w[this.pos / 4] = (this.w[this.pos / 4] << 8) | (uint)input;
				int num = 64;
				int num2 = this.pos + 1;
				this.pos = num2;
				if (num == num2)
				{
					this.Drain();
				}
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
			public void Append(byte[] input)
			{
				foreach (byte b in input)
				{
					this.Append(b);
				}
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000EAEC File Offset: 0x0000CCEC
			public void Finish(byte[] output)
			{
				long num = this.length + (long)(8 * this.pos);
				this.Append(128);
				while (this.pos != 56)
				{
					this.Append(0);
				}
				this.Append((byte)(num >> 56));
				this.Append((byte)(num >> 48));
				this.Append((byte)(num >> 40));
				this.Append((byte)(num >> 32));
				this.Append((byte)(num >> 24));
				this.Append((byte)(num >> 16));
				this.Append((byte)(num >> 8));
				this.Append((byte)num);
				int num2 = ((output.Length < 20) ? output.Length : 20);
				for (int num3 = 0; num3 != num2; num3++)
				{
					uint num4 = this.w[80 + num3 / 4];
					output[num3] = (byte)(num4 >> 24);
					this.w[80 + num3 / 4] = num4 << 8;
				}
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
			private void Drain()
			{
				for (int num = 16; num != 80; num++)
				{
					this.w[num] = EventSource.Sha1ForNonSecretPurposes.Rol1(this.w[num - 3] ^ this.w[num - 8] ^ this.w[num - 14] ^ this.w[num - 16]);
				}
				uint num2 = this.w[80];
				uint num3 = this.w[81];
				uint num4 = this.w[82];
				uint num5 = this.w[83];
				uint num6 = this.w[84];
				for (int num7 = 0; num7 != 20; num7++)
				{
					uint num8 = (num3 & num4) | (~num3 & num5);
					uint num9 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num8 + num6 + 1518500249U + this.w[num7];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num9;
				}
				for (int num10 = 20; num10 != 40; num10++)
				{
					uint num11 = num3 ^ num4 ^ num5;
					uint num12 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num11 + num6 + 1859775393U + this.w[num10];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num12;
				}
				for (int num13 = 40; num13 != 60; num13++)
				{
					uint num14 = (num3 & num4) | (num3 & num5) | (num4 & num5);
					uint num15 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num14 + num6 + 2400959708U + this.w[num13];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num15;
				}
				for (int num16 = 60; num16 != 80; num16++)
				{
					uint num17 = num3 ^ num4 ^ num5;
					uint num18 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num17 + num6 + 3395469782U + this.w[num16];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num18;
				}
				this.w[80] += num2;
				this.w[81] += num3;
				this.w[82] += num4;
				this.w[83] += num5;
				this.w[84] += num6;
				this.length += 512L;
				this.pos = 0;
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
			private static uint Rol1(uint input)
			{
				return (input << 1) | (input >> 31);
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000EDEE File Offset: 0x0000CFEE
			private static uint Rol5(uint input)
			{
				return (input << 5) | (input >> 27);
			}

			// Token: 0x06000308 RID: 776 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
			private static uint Rol30(uint input)
			{
				return (input << 30) | (input >> 2);
			}

			// Token: 0x0400019E RID: 414
			private long length;

			// Token: 0x0400019F RID: 415
			private uint[] w;

			// Token: 0x040001A0 RID: 416
			private int pos;
		}

		// Token: 0x02000085 RID: 133
		private class OverideEventProvider : EventProvider
		{
			// Token: 0x06000309 RID: 777 RVA: 0x0000EE02 File Offset: 0x0000D002
			public OverideEventProvider(EventSource eventSource)
			{
				this.m_eventSource = eventSource;
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000EE14 File Offset: 0x0000D014
			protected override void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int perEventSourceSessionId, int etwSessionId)
			{
				EventListener eventListener = null;
				this.m_eventSource.SendCommand(eventListener, perEventSourceSessionId, etwSessionId, (EventCommand)command, base.IsEnabled(), base.Level, base.MatchAnyKeyword, arguments);
			}

			// Token: 0x040001A1 RID: 417
			private EventSource m_eventSource;
		}

		// Token: 0x02000086 RID: 134
		internal struct EventMetadata
		{
			// Token: 0x040001A2 RID: 418
			public EventDescriptor Descriptor;

			// Token: 0x040001A3 RID: 419
			public EventTags Tags;

			// Token: 0x040001A4 RID: 420
			public bool EnabledForAnyListener;

			// Token: 0x040001A5 RID: 421
			public bool EnabledForETW;

			// Token: 0x040001A6 RID: 422
			public byte TriggersActivityTracking;

			// Token: 0x040001A7 RID: 423
			public string Name;

			// Token: 0x040001A8 RID: 424
			public string Message;

			// Token: 0x040001A9 RID: 425
			public ParameterInfo[] Parameters;

			// Token: 0x040001AA RID: 426
			public TraceLoggingEventTypes TraceLoggingEventTypes;

			// Token: 0x040001AB RID: 427
			public EventActivityOptions ActivityOptions;
		}
	}
}
