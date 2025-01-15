using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public sealed class XmlaResult
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00021D2C File Offset: 0x0001FF2C
		internal XmlaResult()
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00021D4A File Offset: 0x0001FF4A
		internal XmlaResult(XmlaError error)
		{
			this.Messages.Add(error);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00021D74 File Offset: 0x0001FF74
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00021D7C File Offset: 0x0001FF7C
		public XmlaMessageCollection Messages
		{
			get
			{
				return this.m_messages;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00021D84 File Offset: 0x0001FF84
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00021DC0 File Offset: 0x0001FFC0
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

		// Token: 0x0600058C RID: 1420 RVA: 0x00021E05 File Offset: 0x00020005
		internal void SetValue(string value)
		{
			this.m_value = value;
		}

		// Token: 0x040003FB RID: 1019
		private string m_value = string.Empty;

		// Token: 0x040003FC RID: 1020
		private XmlaMessageCollection m_messages = new XmlaMessageCollection();
	}
}
