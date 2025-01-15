using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DF RID: 223
	[LayoutRenderer("processinfo")]
	[ThreadSafe]
	public class ProcessInfoLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000219CD File Offset: 0x0001FBCD
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x000219D5 File Offset: 0x0001FBD5
		[DefaultValue("Id")]
		[DefaultParameter]
		public ProcessInfoProperty Property { get; set; } = ProcessInfoProperty.Id;

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x000219DE File Offset: 0x0001FBDE
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x000219E6 File Offset: 0x0001FBE6
		[DefaultValue(null)]
		public string Format { get; set; }

		// Token: 0x06000D33 RID: 3379 RVA: 0x000219F0 File Offset: 0x0001FBF0
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			PropertyInfo property = typeof(Process).GetProperty(this.Property.ToString());
			if (property == null)
			{
				throw new ArgumentException(string.Format("Property '{0}' not found in System.Diagnostics.Process", this.Property));
			}
			this._lateBoundPropertyGet = ReflectionHelpers.CreateLateBoundMethod(property.GetGetMethod());
			this._process = Process.GetCurrentProcess();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00021A67 File Offset: 0x0001FC67
		protected override void CloseLayoutRenderer()
		{
			if (this._process != null)
			{
				this._process.Close();
				this._process = null;
			}
			base.CloseLayoutRenderer();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00021A8C File Offset: 0x0001FC8C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value = this.GetValue();
			if (value != null)
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				builder.AppendFormattedValue(value, this.Format, formatProvider);
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00021ABA File Offset: 0x0001FCBA
		private object GetValue()
		{
			ReflectionHelpers.LateBoundMethod lateBoundPropertyGet = this._lateBoundPropertyGet;
			if (lateBoundPropertyGet == null)
			{
				return null;
			}
			return lateBoundPropertyGet(this._process, null);
		}

		// Token: 0x0400035E RID: 862
		private Process _process;

		// Token: 0x0400035F RID: 863
		private ReflectionHelpers.LateBoundMethod _lateBoundPropertyGet;
	}
}
