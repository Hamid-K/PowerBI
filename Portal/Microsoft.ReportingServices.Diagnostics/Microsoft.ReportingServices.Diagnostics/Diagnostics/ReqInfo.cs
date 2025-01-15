using System;
using System.Collections;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005B RID: 91
	internal sealed class ReqInfo
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000C7DA File Offset: 0x0000A9DA
		internal ReqInfo(string userName, string path)
		{
			this.m_externalPath = path;
			this.m_users = new Hashtable();
			this.m_users.Add(userName, 1);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C808 File Offset: 0x0000AA08
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

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C858 File Offset: 0x0000AA58
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
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

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C904 File Offset: 0x0000AB04
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

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C930 File Offset: 0x0000AB30
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

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C990 File Offset: 0x0000AB90
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

		// Token: 0x040002D5 RID: 725
		private string m_externalPath;

		// Token: 0x040002D6 RID: 726
		private Hashtable m_users;
	}
}
