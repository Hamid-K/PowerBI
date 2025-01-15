using System;
using System.Diagnostics;
using System.Globalization;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D9 RID: 729
	internal class AppSettingsBaseProvider : IAppSettingsProvider, IIdentifiable
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x000438E4 File Offset: 0x00041AE4
		protected AppSettingsBaseProvider(string id, AppSettingsBaseProvider.OnAppSettingsChanged onAppSettingsChanged)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentNullException("id");
			}
			if (onAppSettingsChanged == null)
			{
				throw new ArgumentNullException("onAppSettingsChanged");
			}
			this.m_trace = new DefaultTraceListener();
			this.m_locker = new object();
			this.m_id = id;
			this.m_onAppSettingsChanged = onAppSettingsChanged;
			this.m_switches = new NameValueDictionary();
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00043948 File Offset: 0x00041B48
		[StringFormatMethod("format")]
		protected void Trace([NotNull] string format, params object[] args)
		{
			if (Debugger.IsAttached)
			{
				DefaultTraceListener trace = this.m_trace;
				lock (trace)
				{
					this.m_trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Tweaks: " + format, args));
				}
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000439AC File Offset: 0x00041BAC
		protected void SetAppSettings(NameValueDictionary switches)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_switches = switches;
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000439F0 File Offset: 0x00041BF0
		protected void SetNameValue(string name, string value)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				NameValueDictionary nameValueDictionary = new NameValueDictionary(this.m_switches, 16);
				if (nameValueDictionary.ContainsKey(name))
				{
					nameValueDictionary[name] = value;
				}
				else
				{
					nameValueDictionary.Add(name, value);
				}
				this.m_switches = nameValueDictionary;
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00043A5C File Offset: 0x00041C5C
		protected void InvokedOnAppSettingsChanged()
		{
			this.m_onAppSettingsChanged();
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00043A69 File Offset: 0x00041C69
		protected int CountForDebugging
		{
			get
			{
				return this.m_switches.Count;
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00043A78 File Offset: 0x00041C78
		public NameValueDictionary GetAppSettings()
		{
			object locker = this.m_locker;
			NameValueDictionary switches;
			lock (locker)
			{
				switches = this.m_switches;
			}
			return switches;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00043ABC File Offset: 0x00041CBC
		public string Name
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x04000751 RID: 1873
		private DefaultTraceListener m_trace;

		// Token: 0x04000752 RID: 1874
		private object m_locker;

		// Token: 0x04000753 RID: 1875
		private string m_id;

		// Token: 0x04000754 RID: 1876
		private AppSettingsBaseProvider.OnAppSettingsChanged m_onAppSettingsChanged;

		// Token: 0x04000755 RID: 1877
		private NameValueDictionary m_switches;

		// Token: 0x02000785 RID: 1925
		// (Invoke) Token: 0x060030A1 RID: 12449
		public delegate void OnAppSettingsChanged();
	}
}
