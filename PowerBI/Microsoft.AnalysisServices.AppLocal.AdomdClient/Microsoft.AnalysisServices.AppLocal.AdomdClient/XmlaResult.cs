using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000049 RID: 73
	internal sealed class XmlaResult
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0001E368 File Offset: 0x0001C568
		internal XmlaResult()
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001E386 File Offset: 0x0001C586
		internal XmlaResult(XmlaError error)
		{
			this.Messages.Add(error);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		public XmlaMessageCollection Messages
		{
			get
			{
				return this.m_messages;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001E3FC File Offset: 0x0001C5FC
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

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001E441 File Offset: 0x0001C641
		internal void SetValue(string value)
		{
			this.m_value = value;
		}

		// Token: 0x040003CC RID: 972
		private string m_value = string.Empty;

		// Token: 0x040003CD RID: 973
		private XmlaMessageCollection m_messages = new XmlaMessageCollection();
	}
}
