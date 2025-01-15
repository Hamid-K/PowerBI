using System;
using System.Resources;
using FxResources.System.Threading.Tasks.Dataflow;

namespace System
{
	// Token: 0x0200000A RID: 10
	internal static class SR
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020A5 File Offset: 0x000002A5
		private static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		internal static string GetResourceString(string resourceKey)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceKey;
			}
			string text = null;
			try
			{
				text = global::System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			return text;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E8 File Offset: 0x000002E8
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = global::System.SR.GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000210B File Offset: 0x0000030B
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002162 File Offset: 0x00000362
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002195 File Offset: 0x00000395
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C1 File Offset: 0x000003C1
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021EB File Offset: 0x000003EB
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000221A File Offset: 0x0000041A
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002250 File Offset: 0x00000450
		internal static string Format(IFormatProvider provider, string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(provider, resourceFormat, args);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000227D File Offset: 0x0000047D
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Threading.Tasks.Dataflow.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000229D File Offset: 0x0000049D
		internal static string ArgumentOutOfRange_BatchSizeMustBeNoGreaterThanBoundedCapacity
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_BatchSizeMustBeNoGreaterThanBoundedCapacity");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022A9 File Offset: 0x000004A9
		internal static string ArgumentOutOfRange_GenericPositive
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_GenericPositive");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022B5 File Offset: 0x000004B5
		internal static string ArgumentOutOfRange_NeedNonNegOrNegative1
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022C1 File Offset: 0x000004C1
		internal static string Argument_BoundedCapacityNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_BoundedCapacityNotSupported");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022CD File Offset: 0x000004CD
		internal static string Argument_CantConsumeFromANullSource
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_CantConsumeFromANullSource");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022D9 File Offset: 0x000004D9
		internal static string Argument_InvalidMessageHeader
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidMessageHeader");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022E5 File Offset: 0x000004E5
		internal static string Argument_InvalidMessageId
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidMessageId");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022F1 File Offset: 0x000004F1
		internal static string Argument_NonGreedyNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_NonGreedyNotSupported");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022FD File Offset: 0x000004FD
		internal static string InvalidOperation_DataNotAvailableForReceive
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_DataNotAvailableForReceive");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002309 File Offset: 0x00000509
		internal static string InvalidOperation_FailedToConsumeReservedMessage
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_FailedToConsumeReservedMessage");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002315 File Offset: 0x00000515
		internal static string InvalidOperation_MessageNotReservedByTarget
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_MessageNotReservedByTarget");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002321 File Offset: 0x00000521
		internal static string NotSupported_MemberNotNeeded
		{
			get
			{
				return global::System.SR.GetResourceString("NotSupported_MemberNotNeeded");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000232D File Offset: 0x0000052D
		internal static string ConcurrentCollection_SyncRoot_NotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002339 File Offset: 0x00000539
		internal static string InvalidOperation_ErrorDuringCleanup
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_ErrorDuringCleanup");
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002348 File Offset: 0x00000548
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x04000005 RID: 5
		private static readonly bool s_usingResourceKeys;

		// Token: 0x04000006 RID: 6
		private static ResourceManager s_resourceManager;
	}
}
