using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005A RID: 90
	internal sealed class RunningRequests
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00002E32 File Offset: 0x00001032
		private RunningRequests()
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		public bool BeginRequest(string reqPath, string userName, int maxActiveRequests)
		{
			bool flag2;
			lock (this)
			{
				if (this.ActiveRequestsForUser(userName) > maxActiveRequests)
				{
					flag2 = false;
				}
				else
				{
					this.AddUserInfo(userName, reqPath);
					this.AddReqInfo(userName, reqPath);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public void RemoveRequest(string reqPath, string userName)
		{
			lock (this)
			{
				this.RemoveUserInfo(userName, reqPath);
				this.RemoveRequestInfo(userName, reqPath);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C540 File Offset: 0x0000A740
		public int ActiveRequestsForUser(string userName)
		{
			int num2;
			lock (this)
			{
				int num = 0;
				object obj = this.UserInfoProp[userName];
				if (obj != null)
				{
					UserInfo userInfo = (UserInfo)obj;
					num += userInfo.TotalRequests;
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public string UsersXml()
		{
			string text2;
			lock (this)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.WriteStartElement("Users");
				if (this.UserInfoProp.Keys.Count > 0)
				{
					foreach (object obj in this.UserInfoProp.Keys)
					{
						string text = (string)obj;
						xmlTextWriter.WriteStartElement("User");
						xmlTextWriter.WriteElementString("Name", text);
						((UserInfo)this.UserInfoProp[text]).ToXml(xmlTextWriter);
						xmlTextWriter.WriteEndElement();
					}
				}
				xmlTextWriter.WriteEndElement();
				text2 = stringWriter.ToString();
			}
			return text2;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C69C File Offset: 0x0000A89C
		private void AddUserInfo(string userName, string reqPath)
		{
			if (this.UserInfoProp[userName] == null)
			{
				this.UserInfoProp.Add(userName, new UserInfo(userName, reqPath));
				return;
			}
			((UserInfo)this.UserInfoProp[userName]).AddRequest(reqPath);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		private void AddReqInfo(string userName, string reqPath)
		{
			if (this.RequestInfoProp[reqPath] == null)
			{
				this.RequestInfoProp.Add(reqPath, new ReqInfo(userName, reqPath));
				return;
			}
			((ReqInfo)this.RequestInfoProp[reqPath]).AddRequest(userName);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C714 File Offset: 0x0000A914
		private void RemoveUserInfo(string userName, string reqPath)
		{
			object obj = this.UserInfoProp[userName];
			if (obj != null)
			{
				UserInfo userInfo = (UserInfo)obj;
				userInfo.RemoveRequest(reqPath);
				if (userInfo.IsEmpty())
				{
					this.UserInfoProp.Remove(userName);
				}
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000C754 File Offset: 0x0000A954
		private void RemoveRequestInfo(string userName, string reqPath)
		{
			object obj = this.RequestInfoProp[reqPath];
			if (obj != null)
			{
				ReqInfo reqInfo = (ReqInfo)obj;
				reqInfo.RemoveRequest(userName);
				if (reqInfo.IsEmpty())
				{
					this.RequestInfoProp.Remove(reqPath);
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000C791 File Offset: 0x0000A991
		public static RunningRequests Current
		{
			get
			{
				return RunningRequests.m_current;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C798 File Offset: 0x0000A998
		private Hashtable UserInfoProp
		{
			get
			{
				if (this.m_users == null)
				{
					this.m_users = new Hashtable();
				}
				return this.m_users;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000C7B3 File Offset: 0x0000A9B3
		private Hashtable RequestInfoProp
		{
			get
			{
				if (this.m_reqs == null)
				{
					this.m_reqs = new Hashtable();
				}
				return this.m_reqs;
			}
		}

		// Token: 0x040002D2 RID: 722
		private static RunningRequests m_current = new RunningRequests();

		// Token: 0x040002D3 RID: 723
		private Hashtable m_users;

		// Token: 0x040002D4 RID: 724
		private Hashtable m_reqs;
	}
}
