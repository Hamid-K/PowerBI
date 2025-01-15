using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Cis.Eventing.Listeners
{
	// Token: 0x02000496 RID: 1174
	public class RDTextWriterTraceListener : TraceListener
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0007BACC File Offset: 0x00079CCC
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x0007BAD4 File Offset: 0x00079CD4
		private FileStream Writer { get; set; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x0007BADD File Offset: 0x00079CDD
		// (set) Token: 0x060028A8 RID: 10408 RVA: 0x0007BAE5 File Offset: 0x00079CE5
		private Encoding Encoding { get; set; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0007BAEE File Offset: 0x00079CEE
		// (set) Token: 0x060028AA RID: 10410 RVA: 0x0007BAF6 File Offset: 0x00079CF6
		private Queue<FileInfo> Files { get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x0007BAFF File Offset: 0x00079CFF
		// (set) Token: 0x060028AC RID: 10412 RVA: 0x0007BB07 File Offset: 0x00079D07
		private long CurrentFileSize { get; set; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x0007BB10 File Offset: 0x00079D10
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x0007BB18 File Offset: 0x00079D18
		private bool Initialized { get; set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x0007BB21 File Offset: 0x00079D21
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x0007BB29 File Offset: 0x00079D29
		private string DirectoryPath { get; set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0007BB32 File Offset: 0x00079D32
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x0007BB3A File Offset: 0x00079D3A
		private string FileNamePrefix { get; set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x0007BB43 File Offset: 0x00079D43
		// (set) Token: 0x060028B4 RID: 10420 RVA: 0x0007BB4B File Offset: 0x00079D4B
		private string FileExtension { get; set; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x0007BB54 File Offset: 0x00079D54
		// (set) Token: 0x060028B6 RID: 10422 RVA: 0x0007BB5C File Offset: 0x00079D5C
		private int MaxFileCount { get; set; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x0007BB65 File Offset: 0x00079D65
		// (set) Token: 0x060028B8 RID: 10424 RVA: 0x0007BB6D File Offset: 0x00079D6D
		private long MaxFileSize { get; set; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x0007BB76 File Offset: 0x00079D76
		// (set) Token: 0x060028BA RID: 10426 RVA: 0x0007BB7E File Offset: 0x00079D7E
		private bool ConditionalFlush { get; set; }

		// Token: 0x060028BB RID: 10427 RVA: 0x0007BB88 File Offset: 0x00079D88
		private static List<PropertyInfo> GetEventBasePropertiesForLogging()
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in typeof(RDEventBase).GetProperties())
			{
				if (propertyInfo.GetCustomAttributes(typeof(RDEventProperty), true).Any<object>())
				{
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0007BBE0 File Offset: 0x00079DE0
		private static string GenerateFileName(string fileName)
		{
			string directoryName = Path.GetDirectoryName(fileName);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
			string extension = Path.GetExtension(fileName);
			string text = DateTime.UtcNow.ToString("yyyy.MM.dd--HH.mm.ss");
			string text2 = Path.Combine(directoryName, fileNameWithoutExtension + "--" + text + extension);
			int num = 0;
			while (File.Exists(text2))
			{
				text2 = Path.Combine(directoryName, string.Concat(new string[]
				{
					fileNameWithoutExtension,
					"--",
					text,
					"--",
					num.ToString(),
					extension
				}));
				num++;
			}
			return text2;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0007BC84 File Offset: 0x00079E84
		private void Initialize()
		{
			if (this.Initialized)
			{
				return;
			}
			if (base.Attributes.ContainsKey("directory"))
			{
				this.DirectoryPath = base.Attributes["directory"];
			}
			if (base.Attributes.ContainsKey("fileNamePrefix"))
			{
				this.FileNamePrefix = base.Attributes["fileNamePrefix"];
			}
			if (base.Attributes.ContainsKey("fileExtension"))
			{
				this.FileExtension = base.Attributes["fileExtension"];
			}
			int num;
			if (base.Attributes.ContainsKey("maxFileCount") && int.TryParse(base.Attributes["maxFileCount"], out num))
			{
				this.MaxFileCount = num;
			}
			long num2;
			if (base.Attributes.ContainsKey("maxFileSize") && long.TryParse(base.Attributes["maxFileSize"], out num2))
			{
				this.MaxFileSize = num2;
			}
			bool flag;
			if (base.Attributes.ContainsKey("conditionalFlush") && bool.TryParse(base.Attributes["conditionalFlush"], out flag))
			{
				this.ConditionalFlush = flag;
			}
			if (string.IsNullOrEmpty(this.DirectoryPath))
			{
				this.DirectoryPath = Environment.CurrentDirectory;
			}
			if (this.FileNamePrefix == null)
			{
				this.FileNamePrefix = "RDTextWriter";
			}
			if (this.FileExtension == null)
			{
				this.FileExtension = ".log";
			}
			if (!this.FileExtension.StartsWith("."))
			{
				this.FileExtension = "." + this.FileExtension;
			}
			if (this.MaxFileCount <= 0)
			{
				this.MaxFileCount = 10;
			}
			if (this.MaxFileSize <= 0L)
			{
				this.MaxFileSize = 10485760L;
			}
			try
			{
				string text = string.Format("{0}*{1}", this.FileNamePrefix, this.FileExtension);
				string[] files = Directory.GetFiles(this.DirectoryPath, text);
				this.Files = new Queue<FileInfo>(files.Select((string x) => new FileInfo(x)));
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (PathTooLongException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (IOException)
			{
			}
			if (this.Files == null)
			{
				this.Files = new Queue<FileInfo>();
			}
			this.Encoding = (Encoding)new UTF8Encoding(false).Clone();
			this.Encoding.EncoderFallback = EncoderFallback.ReplacementFallback;
			this.Encoding.DecoderFallback = DecoderFallback.ReplacementFallback;
			this.Initialized = true;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0007BF1C File Offset: 0x0007A11C
		private bool EnforceLimits()
		{
			bool flag = true;
			if (this.Writer != null && this.Writer.Length > this.MaxFileSize)
			{
				this.Writer.Flush();
				this.Writer.Close();
				this.Writer = null;
			}
			if (this.Writer == null)
			{
				flag = false;
				int num = 4096;
				if (this.MaxFileSize < (long)num)
				{
					num = (int)this.MaxFileSize;
				}
				string text = DateTime.UtcNow.ToString("yyyy.MM.dd--HH.mm.ss.fff");
				string text2 = this.FileNamePrefix + "--" + text + this.FileExtension;
				string text3 = Path.Combine(this.DirectoryPath, text2);
				FileStream fileStream = null;
				int num2 = 0;
				while (num2 < 2 && !flag)
				{
					try
					{
						fileStream = new FileStream(text3, FileMode.CreateNew, FileAccess.Write, FileShare.Read, num, FileOptions.None);
						flag = true;
					}
					catch (IOException)
					{
						text2 = string.Concat(new string[]
						{
							this.FileNamePrefix,
							"--",
							text,
							"--",
							Guid.NewGuid().ToString(),
							this.FileExtension
						});
						text3 = Path.Combine(this.DirectoryPath, text2);
					}
					catch (Exception)
					{
					}
					num2++;
				}
				if (flag)
				{
					this.Writer = fileStream;
					this.Files.Enqueue(new FileInfo(text3));
				}
			}
			while (this.Files.Count > this.MaxFileCount)
			{
				FileInfo fileInfo = this.Files.Dequeue();
				if (fileInfo.Exists)
				{
					try
					{
						fileInfo.Delete();
					}
					catch (Exception)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0007C0D4 File Offset: 0x0007A2D4
		private bool EnsureWriter()
		{
			this.Initialize();
			return this.EnforceLimits();
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0007C0E4 File Offset: 0x0007A2E4
		private string Format(IEnumerable<string> strings)
		{
			if (strings == null || !strings.Any<string>())
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in strings)
			{
				if (text.Contains(","))
				{
					stringBuilder.AppendFormat("\" {0}\",", text);
				}
				else
				{
					stringBuilder.AppendFormat(" {0},", text);
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0007C184 File Offset: 0x0007A384
		private string Escape(string s)
		{
			s = s.Replace("\"", "\"\"");
			if (s.Contains(",") || s.Contains("\""))
			{
				s = '"' + s + '"';
			}
			return s;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0007C1D4 File Offset: 0x0007A3D4
		private string Reduce(string s)
		{
			StringBuilder stringBuilder = new StringBuilder(s);
			stringBuilder.Replace("[TAB]", "\\[TAB\\]");
			stringBuilder.Replace("[NLN]", "\\[NLN\\]");
			stringBuilder.Replace("\t", "[TAB]");
			stringBuilder.Replace("\r\n", "[NLN]");
			stringBuilder.Replace("\n", "[NLN]");
			return stringBuilder.ToString();
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x0007C244 File Offset: 0x0007A444
		private IEnumerable<string> Flatten(Dictionary<string, object> dictionary)
		{
			foreach (KeyValuePair<string, object> kv in dictionary)
			{
				KeyValuePair<string, object> keyValuePair = kv;
				yield return keyValuePair.Key;
				KeyValuePair<string, object> keyValuePair2 = kv;
				string text;
				if (keyValuePair2.Value != null)
				{
					KeyValuePair<string, object> keyValuePair3 = kv;
					text = keyValuePair3.Value.ToString();
				}
				else
				{
					text = string.Empty;
				}
				yield return text;
			}
			yield break;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0007C268 File Offset: 0x0007A468
		private IEnumerable<string> Flatten(object data)
		{
			if (data is RDEventBase)
			{
				foreach (PropertyInfo property in RDTextWriterTraceListener.EventBasePropertiesForLogging)
				{
					yield return property.Name;
					object value = property.GetValue(data, null);
					yield return (value == null) ? string.Empty : value.ToString();
				}
			}
			foreach (PropertyInfo property2 in data.GetType().GetProperties())
			{
				if (property2.GetCustomAttributes(typeof(RDEventProperty), false).Any<object>() && property2.DeclaringType != typeof(RDEventBase))
				{
					yield return property2.Name;
					object value2 = property2.GetValue(data, null);
					yield return (value2 == null) ? string.Empty : value2.ToString();
				}
			}
			yield break;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0007C28C File Offset: 0x0007A48C
		private void Trace(TraceEventCache eventCache, TraceEventType eventType, string id, string message)
		{
			string text = eventCache.DateTime.ToUniversalTime().ToString("yyyy/MM/dd");
			string text2 = eventCache.DateTime.ToUniversalTime().ToString("HH:mm:ss");
			string text3 = eventType.ToString();
			this.WriteLine(string.Format("{0}, {1}, {2,-11}, {3,-20}, ,{4}", new object[] { text, text2, text3, id, message }));
			if (this.ConditionalFlush && (eventType == TraceEventType.Critical || eventType == TraceEventType.Error))
			{
				this.Writer.Flush();
			}
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0007C330 File Offset: 0x0007A530
		protected override string[] GetSupportedAttributes()
		{
			return new string[] { "directory", "fileNamePrefix", "fileExtension", "maxFileCount", "maxFileSize", "conditionalFlush" };
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0007C375 File Offset: 0x0007A575
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0007C380 File Offset: 0x0007A580
		public override void Close()
		{
			if (this.EnsureWriter())
			{
				this.Writer.Flush();
				this.Writer.Close();
			}
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0007C3A0 File Offset: 0x0007A5A0
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
			{
				if (data == null)
				{
					this.Trace(eventCache, eventType, id.ToString(), string.Empty);
					return;
				}
				Dictionary<string, object> dictionary = data as Dictionary<string, object>;
				if (dictionary != null)
				{
					this.Trace(eventCache, eventType, id.ToString(), this.Format(this.Flatten(dictionary)));
					return;
				}
				if (data is MessageEvent)
				{
					MessageEvent messageEvent = (MessageEvent)data;
					string text = string.Format(" OrigTS, {0}, {1}", messageEvent.OrigTS, this.Escape(messageEvent.Message));
					this.Trace(eventCache, eventType, id.ToString(), text);
					return;
				}
				if (data is FormattedMessageEvent)
				{
					FormattedMessageEvent formattedMessageEvent = (FormattedMessageEvent)data;
					string text2 = string.Format(" OrigTS, {0}, {1}", formattedMessageEvent.OrigTS, this.Escape(formattedMessageEvent.Message));
					this.Trace(eventCache, eventType, id.ToString(), text2);
					return;
				}
				RDEvent[] array = (RDEvent[])data.GetType().GetCustomAttributes(typeof(RDEvent), true);
				if (array.Any<RDEvent>())
				{
					string text3 = data.GetType().Name;
					int num = text3.LastIndexOf("Event");
					if (num > 0)
					{
						text3 = text3.Remove(num, "Event".Length);
					}
					this.Trace(eventCache, eventType, text3, this.Format(this.Flatten(data)));
					return;
				}
				this.TraceEvent(eventCache, source, eventType, id, this.Escape(data.ToString()));
			}
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0007C520 File Offset: 0x0007A720
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				string text = string.Empty;
				if (data != null)
				{
					text = this.Format(data.Select(delegate(object o)
					{
						if (o != null)
						{
							return o.ToString();
						}
						return string.Empty;
					}));
				}
				this.Trace(eventCache, eventType, id.ToString(), text);
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x00079F61 File Offset: 0x00078161
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.TraceEvent(eventCache, source, eventType, id, string.Empty);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0007C594 File Offset: 0x0007A794
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
			{
				string text = string.Empty;
				if (format != null && args != null)
				{
					try
					{
						text = string.Format(format, args);
						goto IL_0044;
					}
					catch (FormatException)
					{
						goto IL_0044;
					}
				}
				if (format != null)
				{
					text = format;
				}
				IL_0044:
				this.Trace(eventCache, eventType, id.ToString(), this.Escape(text));
			}
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0007C60C File Offset: 0x0007A80C
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
			{
				message = ((message == null) ? string.Empty : message);
				this.Trace(eventCache, eventType, id.ToString(), this.Escape(message));
			}
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0007C660 File Offset: 0x0007A860
		public override void Write(string message)
		{
			if (this.EnsureWriter())
			{
				byte[] bytes = RDTextWriterTraceListener.OutputEncoding.GetBytes(this.Reduce(message));
				this.Writer.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0007C698 File Offset: 0x0007A898
		public override void WriteLine(string message)
		{
			if (this.EnsureWriter())
			{
				byte[] bytes = RDTextWriterTraceListener.OutputEncoding.GetBytes(this.Reduce(message) + Environment.NewLine);
				this.Writer.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x040017E0 RID: 6112
		private const string DefaultFileNamePrefix = "RDTextWriter";

		// Token: 0x040017E1 RID: 6113
		private const string DefaultFileExtension = ".log";

		// Token: 0x040017E2 RID: 6114
		private const int DefaultMaxFileCount = 10;

		// Token: 0x040017E3 RID: 6115
		private const long DefaultMaxFileSize = 10485760L;

		// Token: 0x040017E4 RID: 6116
		private const int MaxBufferSize = 4096;

		// Token: 0x040017E5 RID: 6117
		private static List<PropertyInfo> EventBasePropertiesForLogging = RDTextWriterTraceListener.GetEventBasePropertiesForLogging();

		// Token: 0x040017E6 RID: 6118
		private static Encoding OutputEncoding = Encoding.UTF8;
	}
}
