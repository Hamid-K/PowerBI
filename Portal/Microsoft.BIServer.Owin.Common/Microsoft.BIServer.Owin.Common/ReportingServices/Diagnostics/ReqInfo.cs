using System;
using System.Collections;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002B RID: 43
	internal sealed class ReqInfo
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004472 File Offset: 0x00002672
		internal ReqInfo(string userName, string path)
		{
			this.m_externalPath = path;
			this.m_users = new Hashtable();
			this.m_users.Add(userName, 1);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000044A0 File Offset: 0x000026A0
		internal bool AddRequest(string userName)
		{
			object obj = this.m_users[userName];
			if (obj == null)
			{
				this.m_users.Add(userName, 1);
			}
			else
			{
				int num = (int)obj;
				this.m_users[userName] = num + 1;
			}
			return true;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000044F0 File Offset: 0x000026F0
		internal void RemoveRequest(string userName)
		{
			object obj = this.m_users[userName];
			if (obj != null)
			{
				int num = (int)obj;
				if (num > 0)
				{
					this.m_users[userName] = num - 1;
					return;
				}
				this.m_users.Remove(userName);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000453C File Offset: 0x0000273C
		internal int TotalRequests
		{
			get
			{
				int num = 0;
				foreach (object obj in this.m_users.Values)
				{
					int num2 = (int)obj;
					num += num2;
				}
				return num;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000459C File Offset: 0x0000279C
		internal int RequestsForUser(string userName)
		{
			int num = 0;
			object obj = this.m_users[userName];
			if (obj != null)
			{
				num += (int)obj;
			}
			return num;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000045C8 File Offset: 0x000027C8
		internal bool IsEmpty()
		{
			using (IEnumerator enumerator = this.m_users.Values.GetEnumerator())
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

		// Token: 0x060000BD RID: 189 RVA: 0x00004628 File Offset: 0x00002828
		internal void ToXml(XmlTextWriter writer)
		{
			if (this.m_users.Keys.Count == 0)
			{
				return;
			}
			writer.WriteStartElement("Users");
			foreach (object obj in this.m_users.Keys)
			{
				string text = (string)obj;
				writer.WriteElementString("User", text);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000097 RID: 151
		private string m_externalPath;

		// Token: 0x04000098 RID: 152
		private Hashtable m_users;
	}
}
