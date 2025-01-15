using System;
using System.Collections;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002C RID: 44
	internal sealed class UserInfo
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000046B0 File Offset: 0x000028B0
		internal UserInfo(string userName, string reqPath)
		{
			this.m_userName = userName;
			this.m_requests = new Hashtable();
			this.m_requests.Add(reqPath, 1);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000046DC File Offset: 0x000028DC
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

		// Token: 0x060000C0 RID: 192 RVA: 0x0000472C File Offset: 0x0000292C
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004778 File Offset: 0x00002978
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

		// Token: 0x060000C2 RID: 194 RVA: 0x000047D8 File Offset: 0x000029D8
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

		// Token: 0x060000C3 RID: 195 RVA: 0x00004838 File Offset: 0x00002A38
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

		// Token: 0x04000099 RID: 153
		private string m_userName;

		// Token: 0x0400009A RID: 154
		private Hashtable m_requests;
	}
}
