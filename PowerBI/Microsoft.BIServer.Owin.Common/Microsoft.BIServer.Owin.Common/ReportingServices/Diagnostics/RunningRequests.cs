using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002A RID: 42
	internal sealed class RunningRequests
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000026B7 File Offset: 0x000008B7
		private RunningRequests()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004138 File Offset: 0x00002338
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

		// Token: 0x060000AC RID: 172 RVA: 0x00004190 File Offset: 0x00002390
		public void RemoveRequest(string reqPath, string userName)
		{
			lock (this)
			{
				this.RemoveUserInfo(userName, reqPath);
				this.RemoveRequestInfo(userName, reqPath);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000041D8 File Offset: 0x000023D8
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

		// Token: 0x060000AE RID: 174 RVA: 0x00004238 File Offset: 0x00002438
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

		// Token: 0x060000AF RID: 175 RVA: 0x00004334 File Offset: 0x00002534
		private void AddUserInfo(string userName, string reqPath)
		{
			if (this.UserInfoProp[userName] == null)
			{
				this.UserInfoProp.Add(userName, new UserInfo(userName, reqPath));
				return;
			}
			((UserInfo)this.UserInfoProp[userName]).AddRequest(reqPath);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004370 File Offset: 0x00002570
		private void AddReqInfo(string userName, string reqPath)
		{
			if (this.RequestInfoProp[reqPath] == null)
			{
				this.RequestInfoProp.Add(reqPath, new ReqInfo(userName, reqPath));
				return;
			}
			((ReqInfo)this.RequestInfoProp[reqPath]).AddRequest(userName);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000043AC File Offset: 0x000025AC
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

		// Token: 0x060000B2 RID: 178 RVA: 0x000043EC File Offset: 0x000025EC
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004429 File Offset: 0x00002629
		public static RunningRequests Current
		{
			get
			{
				return RunningRequests.m_current;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004430 File Offset: 0x00002630
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000444B File Offset: 0x0000264B
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

		// Token: 0x04000094 RID: 148
		private static RunningRequests m_current = new RunningRequests();

		// Token: 0x04000095 RID: 149
		private Hashtable m_users;

		// Token: 0x04000096 RID: 150
		private Hashtable m_reqs;
	}
}
