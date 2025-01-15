using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x02000761 RID: 1889
	public class LogListener
	{
		// Token: 0x06002867 RID: 10343 RVA: 0x0007296A File Offset: 0x00070B6A
		public LogListener(LogInfo includedInfo = LogInfo.All, IFeature scoreFeature = null)
		{
			this.IncludedInfo = includedInfo;
			this.ScoreFeature = scoreFeature;
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x000729A0 File Offset: 0x00070BA0
		public LogInfo IncludedInfo { get; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x000729A8 File Offset: 0x00070BA8
		public IFeature ScoreFeature { get; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x000729B0 File Offset: 0x00070BB0
		// (set) Token: 0x0600286B RID: 10347 RVA: 0x000729B8 File Offset: 0x00070BB8
		internal EventNode CurrentEvent { get; private set; } = new EventNode("Root", Array.Empty<EventNode>());

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x000729C1 File Offset: 0x00070BC1
		internal EventNode RootEvent
		{
			get
			{
				return this.CurrentEvent.Root;
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000729CE File Offset: 0x00070BCE
		internal bool Includes(LogInfo info)
		{
			return (this.IncludedInfo & info) > LogInfo.None;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000729DB File Offset: 0x00070BDB
		internal virtual void AppendChildEvent(EventNode newChild)
		{
			if (newChild == null)
			{
				return;
			}
			this.CurrentEvent.AddChild(newChild);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000729ED File Offset: 0x00070BED
		internal virtual void EnterEvent(EventNode newChild)
		{
			if (newChild == null)
			{
				this._skippedEventLevel.Push(true);
				return;
			}
			this._skippedEventLevel.Push(false);
			this.AppendChildEvent(newChild);
			this.CurrentEvent = newChild;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00072A19 File Offset: 0x00070C19
		internal virtual void ExitEvent()
		{
			if (this._skippedEventLevel.Pop())
			{
				return;
			}
			this.CurrentEvent.Stopwatch.Stop();
			if (this.CurrentEvent.Parent != null)
			{
				this.CurrentEvent = this.CurrentEvent.Parent;
			}
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00072A58 File Offset: 0x00070C58
		public void SaveLogToXML(string filename)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				Indent = true
			};
			using (FileStream fileStream = new FileStream(filename, FileMode.Create))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(fileStream, xmlWriterSettings))
				{
					this.RootEvent.ToXML().WriteTo(xmlWriter);
				}
			}
		}

		// Token: 0x040013A8 RID: 5032
		private readonly Stack<bool> _skippedEventLevel = new Stack<bool>();
	}
}
