using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000049 RID: 73
	internal sealed class XmlaResult
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001E038 File Offset: 0x0001C238
		internal XmlaResult()
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001E056 File Offset: 0x0001C256
		internal XmlaResult(XmlaError error)
		{
			this.Messages.Add(error);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001E080 File Offset: 0x0001C280
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001E088 File Offset: 0x0001C288
		public XmlaMessageCollection Messages
		{
			get
			{
				return this.m_messages;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001E090 File Offset: 0x0001C290
		internal bool ContainsErrors
		{
			get
			{
				int i = 0;
				int count = this.m_messages.Count;
				while (i < count)
				{
					if (this.m_messages[i] is XmlaError)
					{
						return true;
					}
					i++;
				}
				return false;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001E0CC File Offset: 0x0001C2CC
		internal bool ContainsInvalidSessionError
		{
			get
			{
				int i = 0;
				int count = this.m_messages.Count;
				while (i < count)
				{
					XmlaError xmlaError = this.m_messages[i] as XmlaError;
					if (xmlaError != null && xmlaError.IsInvalidSession)
					{
						return true;
					}
					i++;
				}
				return false;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001E111 File Offset: 0x0001C311
		internal void SetValue(string value)
		{
			this.m_value = value;
		}

		// Token: 0x040003BF RID: 959
		private string m_value = string.Empty;

		// Token: 0x040003C0 RID: 960
		private XmlaMessageCollection m_messages = new XmlaMessageCollection();
	}
}
