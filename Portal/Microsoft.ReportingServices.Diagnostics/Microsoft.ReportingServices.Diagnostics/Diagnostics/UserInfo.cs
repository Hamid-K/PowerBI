using System;
using System.Collections;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005C RID: 92
	internal sealed class UserInfo
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000CA18 File Offset: 0x0000AC18
		internal UserInfo(string userName, string reqPath)
		{
			this.m_userName = userName;
			this.m_requests = new Hashtable();
			this.m_requests.Add(reqPath, 1);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CA44 File Offset: 0x0000AC44
		internal bool AddRequest(string reqPath)
		{
			object obj = this.m_requests[reqPath];
			if (obj == null)
			{
				this.m_requests.Add(reqPath, 1);
			}
			else
			{
				int num = (int)obj;
				this.m_requests[reqPath] = num + 1;
			}
			return true;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CA94 File Offset: 0x0000AC94
		internal void RemoveRequest(string reqPath)
		{
			object obj = this.m_requests[reqPath];
			if (obj != null)
			{
				int num = (int)obj;
				if (num > 0)
				{
					this.m_requests[reqPath] = num - 1;
					return;
				}
				this.m_requests.Remove(reqPath);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		internal int TotalRequests
		{
			get
			{
				int num = 0;
				foreach (object obj in this.m_requests.Values)
				{
					int num2 = (int)obj;
					num += num2;
				}
				return num;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CB40 File Offset: 0x0000AD40
		internal bool IsEmpty()
		{
			using (IEnumerator enumerator = this.m_requests.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((int)enumerator.Current > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		internal void ToXml(XmlTextWriter writer)
		{
			if (this.m_requests.Keys.Count == 0)
			{
				return;
			}
			writer.WriteStartElement("Paths");
			foreach (object obj in this.m_requests.Keys)
			{
				string text = (string)obj;
				writer.WriteElementString("Path", text);
				writer.WriteElementString("NrReq", this.m_requests[text].ToString());
			}
			writer.WriteEndElement();
		}

		// Token: 0x040002D7 RID: 727
		private string m_userName;

		// Token: 0x040002D8 RID: 728
		private Hashtable m_requests;
	}
}
