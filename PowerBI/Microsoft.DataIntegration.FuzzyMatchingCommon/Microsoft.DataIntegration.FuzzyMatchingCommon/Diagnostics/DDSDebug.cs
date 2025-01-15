using System;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	internal class DDSDebug
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0001319B File Offset: 0x0001139B
		protected static IEventSink Initialize()
		{
			if (SqlContext.IsAvailable)
			{
				return new SqlEventSink();
			}
			return new AppEventSink();
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000131AF File Offset: 0x000113AF
		public static IEventSink EventSink
		{
			get
			{
				return DDSDebug.m_EventSink;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000131B6 File Offset: 0x000113B6
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				DDSDebug.EventSink.Assert(condition);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000131C6 File Offset: 0x000113C6
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				DDSDebug.EventSink.Assert(condition, message);
			}
		}

		// Token: 0x0400005F RID: 95
		protected static readonly IEventSink m_EventSink = DDSDebug.Initialize();
	}
}
