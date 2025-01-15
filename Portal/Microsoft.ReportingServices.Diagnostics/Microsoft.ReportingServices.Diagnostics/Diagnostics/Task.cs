using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200006E RID: 110
	internal sealed class Task
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public Task(Guid id)
		{
			if (id != Guid.Empty)
			{
				this.m_id = id;
				return;
			}
			this.m_id = Guid.NewGuid();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public Task(IDataRecord record, int indexStart, bool hasCreator)
		{
			int num7;
			int num8;
			int num9;
			int num10;
			int num11;
			int num12;
			int num13;
			int num14;
			int num17;
			int num18;
			int num19;
			int num20;
			int num21;
			int num22;
			int num23;
			int num24;
			TaskTrigger trigger;
			checked
			{
				int num = 1 + indexStart;
				int num2 = 2 + indexStart;
				int num3 = 3 + indexStart;
				int num4 = 4 + indexStart;
				int num5 = 5 + indexStart;
				int num6 = 6 + indexStart;
				num7 = 7 + indexStart;
				num8 = 8 + indexStart;
				num9 = 9 + indexStart;
				num10 = 10 + indexStart;
				num11 = 11 + indexStart;
				num12 = 12 + indexStart;
				num13 = 13 + indexStart;
				num14 = 14 + indexStart;
				int num15 = 15 + indexStart;
				int num16 = 16 + indexStart;
				num17 = 17 + indexStart;
				num18 = 18 + indexStart;
				num19 = 19 + indexStart;
				num20 = 20 + indexStart;
				num21 = 21 + indexStart;
				num22 = 22 + indexStart;
				num23 = 23 + indexStart;
				num24 = 24 + indexStart;
				this.m_id = record.GetGuid(indexStart);
				this.Name = record.GetString(num);
				trigger = this.Trigger;
				trigger.StartDate = record.GetDateTime(num2);
				this.Flags = (TaskFlags)record.GetInt32(num3);
				if (!record.IsDBNull(num6))
				{
					trigger.EndDate = record.GetDateTime(num6);
				}
				if (!record.IsDBNull(num15))
				{
					this.DatabaseState = (TaskState)record.GetInt32(num15);
				}
				if (!record.IsDBNull(num16))
				{
					this.LastRunStatus = record.GetString(num16);
				}
				if (!record.IsDBNull(num4))
				{
					this.NextRunTime = record.GetDateTime(num4);
				}
				if (!record.IsDBNull(num5))
				{
					this.LastRunTime = record.GetDateTime(num5);
				}
			}
			if (!record.IsDBNull(num17))
			{
				this.MaxRunTime = (long)record.GetInt32(num17);
			}
			this.EventType = record.GetString(num18);
			if (!record.IsDBNull(num19))
			{
				this.EventData = record.GetString(num19);
			}
			if (!record.IsDBNull(num21))
			{
				this.Path = new CatalogItemPath(record.GetString(num21));
			}
			RecurrenceType recurrenceType = RecurrenceType.Once;
			if (!record.IsDBNull(num7))
			{
				recurrenceType = (RecurrenceType)record.GetInt32(num7);
			}
			long num25 = 0L;
			uint num26 = 0U;
			uint num27 = 0U;
			uint num28 = 0U;
			switch (recurrenceType)
			{
			case RecurrenceType.Minutes:
			{
				int num29 = 0;
				if (!record.IsDBNull(num8))
				{
					num29 = record.GetInt32(num8);
				}
				trigger.SetToMinutes(num29);
				break;
			}
			case RecurrenceType.Daily:
				if (!record.IsDBNull(num9))
				{
					trigger.SetToDaily((long)record.GetInt32(num9));
				}
				break;
			case RecurrenceType.Weekly:
				if (!record.IsDBNull(num10))
				{
					num25 = (long)record.GetInt32(num10);
				}
				if (!record.IsDBNull(num11))
				{
					num26 = (uint)record.GetInt32(num11);
				}
				trigger.SetToWeekly(num25, num26);
				break;
			case RecurrenceType.MonthlyDate:
				if (!record.IsDBNull(num12))
				{
					num26 = (uint)record.GetInt32(num12);
				}
				if (!record.IsDBNull(num13))
				{
					num27 = (uint)record.GetInt32(num13);
				}
				trigger.SetToMonthly(num26, num27);
				break;
			case RecurrenceType.MonthlyDOW:
				if (!record.IsDBNull(num14))
				{
					num28 = (uint)record.GetInt32(num14);
				}
				if (!record.IsDBNull(num11))
				{
					num26 = (uint)record.GetInt32(num11);
				}
				if (!record.IsDBNull(num13))
				{
					num27 = (uint)record.GetInt32(num13);
				}
				trigger.SetToMonthlyDOW(num28, num26, num27);
				break;
			}
			this.Type = (ScheduleType)record.GetInt32(num20);
			if (hasCreator)
			{
				string userNameBySid = UserUtil.GetUserNameBySid(record, num22, num23);
				AuthenticationType @int = (AuthenticationType)record.GetInt32(num24);
				this.Creator = new UserContext(userNameBySid, null, @int);
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		private static bool IsSharedSchedule(string xml)
		{
			try
			{
				new Guid(xml);
				return true;
			}
			catch (Exception ex)
			{
				if (!(ex is ArgumentException) && !(ex is FormatException) && !(ex is OverflowException))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000E228 File Offset: 0x0000C428
		public static string FromToXml(string xml)
		{
			if (string.IsNullOrEmpty(xml) || Task.IsSharedSchedule(xml))
			{
				return xml;
			}
			Task task = new Task(Guid.Empty);
			task.FromXml(xml);
			return task.ToXml();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000E254 File Offset: 0x0000C454
		public static string ConvertXmlCulture(string xml, Task.CultureConversion cultureConversion)
		{
			if (string.IsNullOrEmpty(xml) || Task.IsSharedSchedule(xml))
			{
				return xml;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextReader xmlTextReader = null;
			XmlTextWriter xmlTextWriter = null;
			try
			{
				xmlTextReader = XmlUtil.SafeCreateXmlTextReader(xml);
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
				xmlTextWriter = new XmlTextWriter(stringWriter);
				string text = string.Empty;
				CultureInfo cultureInfo;
				CultureInfo cultureInfo2;
				if (Task.CultureConversion.ToClientCulture == cultureConversion)
				{
					cultureInfo = CultureInfo.InvariantCulture;
					cultureInfo2 = Localization.ClientPrimaryCulture;
				}
				else
				{
					cultureInfo = Localization.ClientPrimaryCulture;
					cultureInfo2 = CultureInfo.InvariantCulture;
				}
				while (xmlTextReader.Read())
				{
					if (XmlNodeType.XmlDeclaration == xmlTextReader.NodeType)
					{
						xmlTextWriter.WriteStartDocument(true);
					}
					else if (xmlTextReader.NodeType == XmlNodeType.Element)
					{
						xmlTextWriter.WriteStartElement(xmlTextReader.Name);
						xmlTextWriter.WriteAttributes(xmlTextReader, false);
						text = xmlTextReader.Name;
					}
					else if (xmlTextReader.NodeType == XmlNodeType.Text)
					{
						string text2 = xmlTextReader.Value;
						if ("Days" == text)
						{
							text2 = text2.Replace(cultureInfo.TextInfo.ListSeparator, cultureInfo2.TextInfo.ListSeparator);
						}
						xmlTextWriter.WriteValue(text2);
					}
					else if (xmlTextReader.NodeType == XmlNodeType.EndElement)
					{
						xmlTextWriter.WriteEndElement();
					}
				}
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public static string EnsureValidScheduleXml(string savedData)
		{
			if (string.IsNullOrEmpty(savedData) || Task.IsSharedSchedule(savedData))
			{
				return savedData;
			}
			string text = savedData;
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				if (text.Contains("FIRST_WEEK"))
				{
					text = text.Replace("FIRST_WEEK", "FirstWeek");
				}
				if (text.Contains("SECOND_WEEK"))
				{
					text = text.Replace("SECOND_WEEK", "SecondWeek");
				}
				if (text.Contains("THIRD_WEEK"))
				{
					text = text.Replace("THIRD_WEEK", "ThirdWeek");
				}
				if (text.Contains("FOURTH_WEEK"))
				{
					text = text.Replace("FOURTH_WEEK", "FourthWeek");
				}
				if (text.Contains("LAST_WEEK"))
				{
					text = text.Replace("LAST_WEEK", "LastWeek");
				}
				text = text.Replace("True", "true");
				text = text.Replace("False", "false");
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(text);
					XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("EndDate");
					if (elementsByTagName != null && elementsByTagName.Count > 0)
					{
						elementsByTagName[0].InnerText = DateTime.Parse(elementsByTagName[0].InnerText, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
					}
					text = xmlDocument.InnerXml;
				}
				catch (XmlException ex)
				{
					throw new MalformedXmlException(ex);
				}
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
			return text;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000E540 File Offset: 0x0000C740
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000E548 File Offset: 0x0000C748
		public TaskTrigger Trigger
		{
			get
			{
				return this.m_trigger;
			}
			set
			{
				this.m_trigger = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000E551 File Offset: 0x0000C751
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000E559 File Offset: 0x0000C759
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000E562 File Offset: 0x0000C762
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000E56A File Offset: 0x0000C76A
		public string LastRunStatus
		{
			get
			{
				return this.m_lastRunStatus;
			}
			set
			{
				this.m_lastRunStatus = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000E573 File Offset: 0x0000C773
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000E57B File Offset: 0x0000C77B
		public TaskFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.m_flags = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000E584 File Offset: 0x0000C784
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000E58C File Offset: 0x0000C78C
		public long MaxRunTime
		{
			get
			{
				return this.m_maxRunTime;
			}
			set
			{
				this.m_maxRunTime = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000E595 File Offset: 0x0000C795
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000E59D File Offset: 0x0000C79D
		public bool IsFailing
		{
			get
			{
				return this.m_isFailing;
			}
			set
			{
				this.m_isFailing = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000E5AE File Offset: 0x0000C7AE
		public DateTime LastRunTime
		{
			get
			{
				return this.m_lastRunTime;
			}
			set
			{
				this.m_lastRunTime = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000E5B7 File Offset: 0x0000C7B7
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000E5BF File Offset: 0x0000C7BF
		public UserContext Creator
		{
			get
			{
				return this.m_creator;
			}
			set
			{
				this.m_creator = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		public string EventType
		{
			get
			{
				return this.m_eventType;
			}
			set
			{
				this.m_eventType = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000E5D9 File Offset: 0x0000C7D9
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000E5E1 File Offset: 0x0000C7E1
		public string EventData
		{
			get
			{
				return this.m_eventData;
			}
			set
			{
				this.m_eventData = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000E5EA File Offset: 0x0000C7EA
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000E5F2 File Offset: 0x0000C7F2
		public ScheduleType Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000E5FB File Offset: 0x0000C7FB
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000E603 File Offset: 0x0000C803
		public int ReportsCount
		{
			get
			{
				return this.m_reportsCount;
			}
			set
			{
				this.m_reportsCount = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000E60C File Offset: 0x0000C80C
		public Guid ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000E614 File Offset: 0x0000C814
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000E635 File Offset: 0x0000C835
		public bool IsRunning
		{
			get
			{
				bool flag = false;
				if ((this.m_state & TaskState.Running) > (TaskState)0)
				{
					flag = true;
				}
				return flag;
			}
			set
			{
				if (value)
				{
					this.m_state |= TaskState.Running;
					return;
				}
				this.m_state &= (TaskState)(-268435457);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000E660 File Offset: 0x0000C860
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000E685 File Offset: 0x0000C885
		public TaskState ScheduleState
		{
			get
			{
				TaskState taskState = this.m_state;
				if (this.IsRunning)
				{
					taskState &= (TaskState)(-268435457);
				}
				return taskState;
			}
			set
			{
				if ((value & TaskState.Paused) > (TaskState)0)
				{
					this.m_nextRunTime = DateTime.MinValue;
				}
				if (this.IsRunning)
				{
					this.m_state = value;
					this.m_state |= TaskState.Running;
					return;
				}
				this.m_state = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000E6C1 File Offset: 0x0000C8C1
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000E6C9 File Offset: 0x0000C8C9
		public TaskState DatabaseState
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000E6D2 File Offset: 0x0000C8D2
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public DateTime NextRunTime
		{
			get
			{
				return this.m_nextRunTime;
			}
			set
			{
				this.m_nextRunTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, 0, DateTimeKind.Local);
				if (this.m_nextRunTime == DateTime.MinValue)
				{
					if (this.m_state != TaskState.Paused)
					{
						this.m_state = TaskState.Expired;
						return;
					}
				}
				else if (this.ScheduleState != TaskState.Paused)
				{
					this.ScheduleState = TaskState.Ready;
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000E74D File Offset: 0x0000C94D
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000E755 File Offset: 0x0000C955
		public CatalogItemPath Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = value;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000E75E File Offset: 0x0000C95E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000E768 File Offset: 0x0000C968
		public bool EqualSqlSchedule(Task t)
		{
			bool flag = true;
			if (t.Flags != this.Flags)
			{
				flag = false;
			}
			else if (t.MaxRunTime != this.MaxRunTime)
			{
				flag = false;
			}
			else if (t.Trigger.EndDate != this.Trigger.EndDate)
			{
				flag = false;
			}
			else if (t.Trigger.RecurrenceType != this.Trigger.RecurrenceType)
			{
				flag = false;
			}
			else if (t.Trigger.TriggerData != null)
			{
				if (!t.Trigger.TriggerData.Equals(this.Trigger.TriggerData))
				{
					if (t.Trigger.EndDate.Year - DateTime.Now.Year < 1)
					{
						if (t.Trigger.RecurrenceType != RecurrenceType.MonthlyDate && t.Trigger.RecurrenceType != RecurrenceType.MonthlyDOW)
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
			}
			else if (this.Trigger.TriggerData != null)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000E868 File Offset: 0x0000CA68
		public override bool Equals(object task)
		{
			bool flag = true;
			if (!(task is Task))
			{
				return false;
			}
			Task task2 = (Task)task;
			if (task2.Flags != this.Flags)
			{
				flag = false;
			}
			else if (task2.MaxRunTime != this.MaxRunTime)
			{
				flag = false;
			}
			else if (task2.Trigger.EndDate != this.Trigger.EndDate)
			{
				flag = false;
			}
			else if (task2.Trigger.StartDate != this.Trigger.StartDate)
			{
				flag = false;
			}
			else if (task2.Trigger.RecurrenceType != this.Trigger.RecurrenceType)
			{
				flag = false;
			}
			else if (task2.Trigger.TriggerData != null)
			{
				if (!task2.Trigger.TriggerData.Equals(this.Trigger.TriggerData))
				{
					flag = false;
				}
			}
			else if (this.Trigger.TriggerData != null)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000E950 File Offset: 0x0000CB50
		public void FromXml(string xml)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				if (xml == null || xml == "")
				{
					throw new MalformedXmlException(null);
				}
				this.Trigger = new TaskTrigger();
				XmlTextReader xmlTextReader = null;
				try
				{
					xmlTextReader = XmlUtil.SafeCreateXmlTextReader(xml);
					xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
					if (!xmlTextReader.Read())
					{
						throw new InvalidXmlException();
					}
					if (xmlTextReader.NodeType == XmlNodeType.XmlDeclaration && !xmlTextReader.Read())
					{
						throw new InvalidXmlException();
					}
					if (xmlTextReader.Name != "ScheduleDefinition")
					{
						throw new InvalidXmlException();
					}
					while (xmlTextReader.Read())
					{
						if (xmlTextReader.NodeType != XmlNodeType.EndElement)
						{
							if (xmlTextReader.Name == "EndDate")
							{
								this.Trigger.EndDate = this.GetDateTimeValue(xmlTextReader);
							}
							else if (xmlTextReader.Name == "StartDateTime")
							{
								this.Trigger.StartDate = this.GetDateTimeValue(xmlTextReader);
							}
							else if (xmlTextReader.Name == "ScheduleFlags")
							{
								this.ReadTaskFlags(xmlTextReader);
							}
							else if (xmlTextReader.Name == "MinuteRecurrence")
							{
								this.ReadMinutes(xmlTextReader);
							}
							else if (xmlTextReader.Name == "DailyRecurrence")
							{
								this.ReadDaily(xmlTextReader);
							}
							else if (xmlTextReader.Name == "WeeklyRecurrence")
							{
								this.ReadWeekly(xmlTextReader);
							}
							else if (xmlTextReader.Name == "MonthlyRecurrence")
							{
								this.ReadMonthly(xmlTextReader);
							}
							else
							{
								if (!(xmlTextReader.Name == "MonthlyDOWRecurrence"))
								{
									throw new InvalidXmlException();
								}
								this.ReadMonthlyDOW(xmlTextReader);
							}
						}
					}
				}
				catch (XmlException ex)
				{
					throw new MalformedXmlException(ex);
				}
				finally
				{
					if (xmlTextReader != null)
					{
						xmlTextReader.Close();
					}
				}
				if (this.Trigger.StartDate == DateTime.MinValue)
				{
					throw new MissingElementException("StartDateTime");
				}
				if (this.Trigger.EndDate != DateTime.MinValue && this.Trigger.EndDate.Date < this.Trigger.StartDate.Date)
				{
					throw new InvalidElementException("EndDate");
				}
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		private void ReadTaskFlags(XmlTextReader reader)
		{
			throw new InvalidXmlException();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		private void ReadMinutes(XmlTextReader reader)
		{
			int num = 0;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "MinuteRecurrence")
					{
						break;
					}
				}
				else
				{
					if (!(reader.Name == "MinutesInterval"))
					{
						throw new InvalidXmlException();
					}
					num = this.GetIntElementValue(reader);
				}
			}
			this.Trigger.SetToMinutes(num);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		private void ReadDaily(XmlTextReader reader)
		{
			long num = 0L;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "DailyRecurrence")
					{
						break;
					}
				}
				else
				{
					if (!(reader.Name == "DaysInterval"))
					{
						throw new InvalidXmlException();
					}
					num = (long)this.GetIntElementValue(reader);
				}
			}
			this.Trigger.SetToDaily(num);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		private void ReadWeekly(XmlTextReader reader)
		{
			long num = 0L;
			uint num2 = 0U;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "WeeklyRecurrence")
					{
						break;
					}
				}
				else if (reader.Name == "WeeksInterval")
				{
					num = (long)this.GetIntElementValue(reader);
				}
				else
				{
					if (!(reader.Name == "DaysOfWeek"))
					{
						throw new InvalidXmlException();
					}
					num2 = this.ReadDaysOfWeek(reader);
				}
			}
			this.Trigger.SetToWeekly(num, num2);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		private void ReadMonthly(XmlTextReader reader)
		{
			string text = "";
			uint num = 0U;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "MonthlyRecurrence")
					{
						break;
					}
				}
				else if (reader.Name == "Days")
				{
					text = reader.ReadString();
				}
				else
				{
					if (!(reader.Name == "MonthsOfYear"))
					{
						throw new InvalidXmlException();
					}
					num = this.ReadMonthsOfYear(reader);
				}
			}
			uint dayBitMap = Monthly.GetDayBitMap(text, (Months)num);
			this.Trigger.SetToMonthly(dayBitMap, num);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		private void ReadMonthlyDOW(XmlTextReader reader)
		{
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "MonthlyDOWRecurrence")
					{
						break;
					}
				}
				else if (reader.Name == "DaysOfWeek")
				{
					num2 = this.ReadDaysOfWeek(reader);
				}
				else if (reader.Name == "MonthsOfYear")
				{
					num3 = this.ReadMonthsOfYear(reader);
				}
				else
				{
					if (reader.Name == "WhichWeek")
					{
						string text = reader.ReadString();
						uint num4 = global::<PrivateImplementationDetails>.ComputeStringHash(text);
						if (num4 > 2088990245U)
						{
							if (num4 > 3036074817U)
							{
								if (num4 != 3791283054U)
								{
									if (num4 != 3871370198U)
									{
										if (num4 != 4140556409U)
										{
											goto IL_01E8;
										}
										if (!(text == "LastWeek"))
										{
											goto IL_01E8;
										}
									}
									else
									{
										if (!(text == "FOURTH_WEEK"))
										{
											goto IL_01E8;
										}
										goto IL_01E0;
									}
								}
								else if (!(text == "LAST_WEEK"))
								{
									goto IL_01E8;
								}
								num = 5U;
								continue;
							}
							if (num4 != 2449133090U)
							{
								if (num4 != 3036074817U)
								{
									goto IL_01E8;
								}
								if (!(text == "FourthWeek"))
								{
									goto IL_01E8;
								}
							}
							else
							{
								if (!(text == "ThirdWeek"))
								{
									goto IL_01E8;
								}
								goto IL_01DC;
							}
							IL_01E0:
							num = 4U;
							continue;
						}
						if (num4 <= 818230193U)
						{
							if (num4 != 285186022U)
							{
								if (num4 != 818230193U)
								{
									goto IL_01E8;
								}
								if (!(text == "FirstWeek"))
								{
									goto IL_01E8;
								}
							}
							else if (!(text == "FIRST_WEEK"))
							{
								goto IL_01E8;
							}
							num = 1U;
							continue;
						}
						if (num4 != 944538970U)
						{
							if (num4 != 1651558067U)
							{
								if (num4 != 2088990245U)
								{
									goto IL_01E8;
								}
								if (!(text == "SecondWeek"))
								{
									goto IL_01E8;
								}
							}
							else
							{
								if (!(text == "THIRD_WEEK"))
								{
									goto IL_01E8;
								}
								goto IL_01DC;
							}
						}
						else if (!(text == "SECOND_WEEK"))
						{
							goto IL_01E8;
						}
						num = 2U;
						continue;
						IL_01DC:
						num = 3U;
						continue;
						IL_01E8:
						throw new InvalidElementException("WhichWeek");
					}
					throw new InvalidXmlException();
				}
			}
			this.Trigger.SetToMonthlyDOW(num, num2, num3);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		private uint ReadDaysOfWeek(XmlTextReader reader)
		{
			uint num = 0U;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "DaysOfWeek")
					{
						break;
					}
				}
				else if (reader.Name == "Sunday")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 1U;
					}
				}
				else if (reader.Name == "Monday")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 2U;
					}
				}
				else if (reader.Name == "Tuesday")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 4U;
					}
				}
				else if (reader.Name == "Wednesday")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 8U;
					}
				}
				else if (reader.Name == "Thursday")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 16U;
					}
				}
				else if (reader.Name == "Friday")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 32U;
					}
				}
				else
				{
					if (!(reader.Name == "Saturday"))
					{
						throw new InvalidXmlException();
					}
					if (this.IsTrueXmlValue(reader))
					{
						num |= 64U;
					}
				}
			}
			if (num == 0U)
			{
				throw new InvalidElementException("DaysOfWeek");
			}
			return num;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000F140 File Offset: 0x0000D340
		private bool IsTrueXmlValue(XmlTextReader reader)
		{
			string name = reader.Name;
			string text = reader.ReadString();
			bool flag2;
			try
			{
				bool flag = false;
				if (Convert.ToBoolean(text, CultureInfo.InvariantCulture))
				{
					flag = true;
				}
				flag2 = flag;
			}
			catch (FormatException)
			{
				throw new InvalidElementException(name);
			}
			return flag2;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000F18C File Offset: 0x0000D38C
		private int GetIntElementValue(XmlTextReader reader)
		{
			int num = 0;
			string name = reader.Name;
			string text = reader.ReadString();
			try
			{
				num = Convert.ToInt32(text, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new InvalidElementException(name);
			}
			return num;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		private DateTime GetDateTimeValue(XmlTextReader reader)
		{
			string name = reader.Name;
			string text = reader.ReadString();
			DateTime dateTime;
			try
			{
				dateTime = DateTime.Parse(text, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new ElementTypeMismatchException(name);
			}
			return dateTime;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000F214 File Offset: 0x0000D414
		private uint ReadMonthsOfYear(XmlTextReader reader)
		{
			uint num = 0U;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "MonthsOfYear")
					{
						break;
					}
				}
				else if (reader.Name == "January")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 1U;
					}
				}
				else if (reader.Name == "February")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 2U;
					}
				}
				else if (reader.Name == "March")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 4U;
					}
				}
				else if (reader.Name == "April")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 8U;
					}
				}
				else if (reader.Name == "May")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 16U;
					}
				}
				else if (reader.Name == "June")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 32U;
					}
				}
				else if (reader.Name == "July")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 64U;
					}
				}
				else if (reader.Name == "August")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 128U;
					}
				}
				else if (reader.Name == "September")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 256U;
					}
				}
				else if (reader.Name == "October")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 512U;
					}
				}
				else if (reader.Name == "November")
				{
					if (this.IsTrueXmlValue(reader))
					{
						num |= 1024U;
					}
				}
				else
				{
					if (!(reader.Name == "December"))
					{
						throw new InvalidXmlException();
					}
					if (this.IsTrueXmlValue(reader))
					{
						num |= 2048U;
					}
				}
			}
			if (num == 0U)
			{
				throw new InvalidElementException("MonthsOfYear");
			}
			return num;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000F43F File Offset: 0x0000D63F
		public string ToXml()
		{
			return this.ToXml(false);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000F448 File Offset: 0x0000D648
		public string ToXml(bool xmlFragmentOnly)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = null;
			try
			{
				xmlTextWriter = new XmlTextWriter(stringWriter);
				if (!xmlFragmentOnly)
				{
					xmlTextWriter.WriteStartDocument(true);
				}
				this.WriteDefinitionXml(xmlTextWriter);
				if (!xmlFragmentOnly)
				{
					xmlTextWriter.WriteEndDocument();
				}
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
			return stringWriter.ToString();
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		public DateTime StartDateTime
		{
			get
			{
				return this.Trigger.StartDate;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000F4B5 File Offset: 0x0000D6B5
		public DateTime EndDate
		{
			get
			{
				return this.Trigger.EndDate;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000F4C2 File Offset: 0x0000D6C2
		public static string TaskNamespace
		{
			get
			{
				if (ProcessingContext.ReqContext != null)
				{
					return ProcessingContext.ReqContext.Namespace;
				}
				return XmlConsts.DefaultNamespace;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		public void WriteDefinitionXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("ScheduleDefinition");
			writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteElementString("StartDateTime", Task.TaskNamespace, Globals.ToPublicDateTimeFormat(this.Trigger.StartDate));
			if (this.Trigger.EndDate != DateTime.MinValue)
			{
				writer.WriteElementString("EndDate", Task.TaskNamespace, this.Trigger.EndDate.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo));
			}
			this.WriteFlagXml(writer);
			if (this.Trigger.TriggerData != null)
			{
				this.Trigger.TriggerData.ToXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public void WriteScheduleXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("Schedule");
			writer.WriteElementString("ScheduleID", this.ID.ToString());
			this.WriteDefinitionXml(writer);
			writer.WriteElementString("ScheduleDescription", this.Trigger.ScheduleDescription);
			writer.WriteElementString("Creator", this.Creator.UserName);
			string text;
			if (this.NextRunTime == DateTime.MinValue)
			{
				text = "Never";
			}
			else
			{
				text = Globals.ToPublicDateTimeFormat(this.NextRunTime);
			}
			writer.WriteElementString("NextRunTime", text);
			if (this.LastRunTime == DateTime.MinValue)
			{
				text = "Never";
			}
			else
			{
				text = Globals.ToPublicDateTimeFormat(this.LastRunTime);
			}
			writer.WriteElementString("LastRunTime", text);
			string text2 = "";
			if (this.IsFailing)
			{
				text2 += "Failing";
			}
			else if (this.IsRunning)
			{
				text2 += TaskState.Running.ToString();
			}
			else
			{
				text2 = this.ScheduleState.ToString();
			}
			writer.WriteElementString("State", text2);
			writer.WriteEndElement();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000F6EB File Offset: 0x0000D8EB
		private void WriteFlagXml(XmlTextWriter writer)
		{
			if (this.Flags == TaskFlags.None)
			{
				return;
			}
			writer.WriteStartElement("ScheduleFlags");
			writer.WriteEndElement();
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000F708 File Offset: 0x0000D908
		public int SQlFreqType
		{
			get
			{
				int num = 0;
				switch (this.Trigger.RecurrenceType)
				{
				case RecurrenceType.Once:
					num = 1;
					break;
				case RecurrenceType.Minutes:
				case RecurrenceType.Daily:
					num = 4;
					break;
				case RecurrenceType.Weekly:
					num = 8;
					break;
				case RecurrenceType.MonthlyDate:
					num = 16;
					break;
				case RecurrenceType.MonthlyDOW:
					num = 32;
					break;
				}
				return num;
			}
		}

		// Token: 0x04000344 RID: 836
		private TaskTrigger m_trigger = new TaskTrigger();

		// Token: 0x04000345 RID: 837
		private TaskState m_state = TaskState.Ready;

		// Token: 0x04000346 RID: 838
		private string m_name = "";

		// Token: 0x04000347 RID: 839
		private string m_lastRunStatus = "";

		// Token: 0x04000348 RID: 840
		private TaskFlags m_flags;

		// Token: 0x04000349 RID: 841
		private long m_maxRunTime;

		// Token: 0x0400034A RID: 842
		private bool m_isFailing;

		// Token: 0x0400034B RID: 843
		private DateTime m_lastRunTime = DateTime.MinValue;

		// Token: 0x0400034C RID: 844
		private UserContext m_creator;

		// Token: 0x0400034D RID: 845
		private string m_eventType = "";

		// Token: 0x0400034E RID: 846
		private string m_eventData = "";

		// Token: 0x0400034F RID: 847
		private ScheduleType m_type = ScheduleType.Scoped;

		// Token: 0x04000350 RID: 848
		private Guid m_id = Guid.Empty;

		// Token: 0x04000351 RID: 849
		private DateTime m_nextRunTime = DateTime.MinValue;

		// Token: 0x04000352 RID: 850
		private CatalogItemPath m_path;

		// Token: 0x04000353 RID: 851
		private int m_reportsCount;

		// Token: 0x04000354 RID: 852
		public const int ProjectionCount = 25;

		// Token: 0x020000A9 RID: 169
		public enum ScheduleProjection
		{
			// Token: 0x040003D6 RID: 982
			ScheduleId,
			// Token: 0x040003D7 RID: 983
			Name,
			// Token: 0x040003D8 RID: 984
			StartDate,
			// Token: 0x040003D9 RID: 985
			Flags,
			// Token: 0x040003DA RID: 986
			NextRunTime,
			// Token: 0x040003DB RID: 987
			LastRunTime,
			// Token: 0x040003DC RID: 988
			EndDate,
			// Token: 0x040003DD RID: 989
			RecurrenceType,
			// Token: 0x040003DE RID: 990
			MinutesInterval,
			// Token: 0x040003DF RID: 991
			DaysInterval,
			// Token: 0x040003E0 RID: 992
			WeeksInterval,
			// Token: 0x040003E1 RID: 993
			DaysOfWeek,
			// Token: 0x040003E2 RID: 994
			DaysOfMonth,
			// Token: 0x040003E3 RID: 995
			Month,
			// Token: 0x040003E4 RID: 996
			MonthlyWeek,
			// Token: 0x040003E5 RID: 997
			State,
			// Token: 0x040003E6 RID: 998
			LastRunStatus,
			// Token: 0x040003E7 RID: 999
			ScheduledRunTimeout,
			// Token: 0x040003E8 RID: 1000
			EventType,
			// Token: 0x040003E9 RID: 1001
			EventData,
			// Token: 0x040003EA RID: 1002
			Type,
			// Token: 0x040003EB RID: 1003
			SchedulePath,
			// Token: 0x040003EC RID: 1004
			CreatorUserNameBySid,
			// Token: 0x040003ED RID: 1005
			CreatorBackupUserName,
			// Token: 0x040003EE RID: 1006
			CreatorAuthType
		}

		// Token: 0x020000AA RID: 170
		public enum CultureConversion
		{
			// Token: 0x040003F0 RID: 1008
			ToNeutralCulture,
			// Token: 0x040003F1 RID: 1009
			ToClientCulture
		}
	}
}
